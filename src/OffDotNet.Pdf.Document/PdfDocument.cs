﻿// <copyright file="PdfDocument.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Document;

using System.Collections.Immutable;
using Core.Common;
using Core.CommonDataStructures;
using Core.ContentStreamAndResources;
using Core.DocumentStructure;
using Core.FileStructure;
using Core.Primitives;
using Core.Text;
using Core.Text.Fonts;
using Core.Text.Operations.TextPosition;
using Core.Text.Operations.TextShowing;
using Core.Text.Operations.TextState;

public sealed class PdfDocument : IDisposable, IAsyncDisposable
{
    private readonly IPdfIndirectIdentifier<IPdfStream> contentStreamIndirect;
    private readonly Stream stream;

    // ReSharper disable once UnusedParameter.Local
    public PdfDocument(Stream stream, IPdfDocumentOptions options)
    {
        var objectNumber = 0;
        this.stream = stream;
        this.DocumentCatalog = new PdfIndirect<IDocumentCatalog>(++objectNumber).ToPdfIndirectIdentifier();
        this.RootPageTree = new PdfIndirect<IPageTreeNode>(++objectNumber).ToPdfIndirectIdentifier();
        this.Pages = new List<IPdfIndirectIdentifier<IPageObject>>(1) { new PdfIndirect<IPageObject>(++objectNumber).ToPdfIndirectIdentifier() }.ToImmutableList();
        this.contentStreamIndirect = new PdfIndirect<IPdfStream>(++objectNumber).ToPdfIndirectIdentifier();
        this.Fonts = new List<IPdfIndirectIdentifier<IType1Font>>(1) { new PdfIndirect<IType1Font>(++objectNumber).ToPdfIndirectIdentifier() }.ToImmutableList();

        IPdfOperation[] pdfOperations =
        [
            new FontOperation("F1", 24),
            new MoveTextOperation(100, 100),
            new ShowTextOperation("Hello World")
        ];

        ITextObject textObject = new TextObject(pdfOperations);
        var fontDictionary = new Dictionary<PdfName, IPdfIndirectIdentifier<IType1Font>> { { "F1", this.Fonts[0] } }.ToPdfDictionary();

        this.DocumentCatalog.PdfIndirect.Value = new DocumentCatalog(documentCatalogOptions => documentCatalogOptions.Pages = this.RootPageTree);
        this.RootPageTree.PdfIndirect.Value = new PageTreeNode(pageTreeNodeOptions => pageTreeNodeOptions.Kids = this.Pages.ToPdfArray());

        foreach (var pageObjectIndirect in this.Pages)
        {
            pageObjectIndirect.PdfIndirect.Value = new PageObject(pageObjectOptions =>
            {
                pageObjectOptions.Parent = this.RootPageTree;
                pageObjectOptions.MediaBox = new Rectangle(0, 0, 612, 792);
                pageObjectOptions.Contents = new(this.contentStreamIndirect);
                pageObjectOptions.Resources = new ResourceDictionary(resourceDictionaryOptions => resourceDictionaryOptions.Font = fontDictionary);
            });
        }

        this.contentStreamIndirect.PdfIndirect.Value = new PdfStream(textObject.Content.AsMemory());
        this.Fonts[0].PdfIndirect.Value = StandardFonts.Helvetica;

        // XTable
        var byteOffset = 0;
        List<IXRefEntry> xRefEntries = new(objectNumber) { new XRefEntry(byteOffset, 65535, XRefEntryType.Free) };

        byteOffset += FileHeader.Bytes.Length;
        xRefEntries.Add(new XRefEntry(byteOffset, 0, XRefEntryType.InUse));

        byteOffset += this.DocumentCatalog.PdfIndirect.Bytes.Length;
        xRefEntries.Add(new XRefEntry(byteOffset, 0, XRefEntryType.InUse));

        byteOffset += this.RootPageTree.PdfIndirect.Bytes.Length;
        xRefEntries.Add(new XRefEntry(byteOffset, 0, XRefEntryType.InUse));

        foreach (var pageObjectIndirect in this.Pages)
        {
            byteOffset += pageObjectIndirect.PdfIndirect.Bytes.Length;
            xRefEntries.Add(new XRefEntry(byteOffset, 0, XRefEntryType.InUse));
        }

        byteOffset += this.contentStreamIndirect.PdfIndirect.Bytes.Length;
        xRefEntries.Add(new XRefEntry(byteOffset, 0, XRefEntryType.InUse));

        for (var index = 0; index < this.Fonts.Count; index++)
        {
            var fontIndirect = this.Fonts[index];
            byteOffset += fontIndirect.PdfIndirect.Bytes.Length;

            if (index == this.Fonts.Count - 1)
            {
                break;
            }

            xRefEntries.Add(new XRefEntry(byteOffset, 0, XRefEntryType.InUse));
        }

        this.XRefTable = xRefEntries.ToXRefTable(0);

        this.FileTrailer = new FileTrailer(byteOffset, fileTrailerOptions =>
        {
            fileTrailerOptions.Size = xRefEntries.Count - 1;
            fileTrailerOptions.Root = this.DocumentCatalog;
        });
    }

    public PdfDocument(Stream stream, Action<IPdfDocumentOptions> options)
        : this(stream, GetOptions(options))
    {
    }

    public static FileHeader FileHeader => FileHeader.PdfVersion17;

    public IPdfIndirectIdentifier<IDocumentCatalog> DocumentCatalog { get; }

    public IPdfIndirectIdentifier<IPageTreeNode> RootPageTree { get; }

    public IImmutableList<IPdfIndirectIdentifier<IPageObject>> Pages { get; }

    public IImmutableList<IPdfIndirectIdentifier<IType1Font>> Fonts { get; }

    public IXRefTable XRefTable { get; }

    public IFileTrailer FileTrailer { get; }

    public async Task GenerateOutputStream(CancellationToken cancellationToken = default)
    {
        await this.stream.WriteAsync(FileHeader.Bytes, cancellationToken).ConfigureAwait(false);
        await this.stream.WriteAsync(this.DocumentCatalog.PdfIndirect.Bytes, cancellationToken).ConfigureAwait(false);
        await this.stream.WriteAsync(this.RootPageTree.PdfIndirect.Bytes, cancellationToken).ConfigureAwait(false);

        foreach (var pageObjectIndirect in this.Pages)
        {
            await this.stream.WriteAsync(pageObjectIndirect.PdfIndirect.Bytes, cancellationToken).ConfigureAwait(false);
        }

        await this.stream.WriteAsync(this.contentStreamIndirect.PdfIndirect.Bytes, cancellationToken).ConfigureAwait(false);

        foreach (var fontIndirect in this.Fonts)
        {
            await this.stream.WriteAsync(fontIndirect.PdfIndirect.Bytes, cancellationToken).ConfigureAwait(false);
        }

        await this.stream.WriteAsync(this.XRefTable.Bytes, cancellationToken).ConfigureAwait(false);
        await this.stream.WriteAsync(this.FileTrailer.Bytes, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        this.stream.Dispose();
    }

    /// <inheritdoc/>
    public ValueTask DisposeAsync()
    {
        return this.stream.DisposeAsync();
    }

    private static IPdfDocumentOptions GetOptions(Action<IPdfDocumentOptions> optionsFunc)
    {
        PdfDocumentOptions options = new();
        optionsFunc.Invoke(options);
        return options;
    }
}

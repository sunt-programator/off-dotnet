// <copyright file="PdfDocument.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Immutable;
using OffDotNet.Pdf.Core.CommonDataStructures;
using OffDotNet.Pdf.Core.ContentStreamAndResources;
using OffDotNet.Pdf.Core.DocumentStructure;
using OffDotNet.Pdf.Core.FileStructure;
using OffDotNet.Pdf.Core.Primitives;
using OffDotNet.Pdf.Core.Text;
using OffDotNet.Pdf.Core.Text.Fonts;
using OffDotNet.Pdf.Core.Text.Operations.TextPosition;
using OffDotNet.Pdf.Core.Text.Operations.TextShowing;
using OffDotNet.Pdf.Core.Text.Operations.TextState;

namespace OffDotNet.Pdf.Document;

public sealed class PdfDocument : IDisposable, IAsyncDisposable
{
    private readonly IPdfIndirectIdentifier<IPdfStream> contentStreamIndirect;
    private readonly Stream stream;

    // ReSharper disable once UnusedParameter.Local
    public PdfDocument(Stream stream, IPdfDocumentOptions options)
    {
        int objectNumber = 0;
        this.stream = stream;
        this.DocumentCatalog = new PdfIndirect<IDocumentCatalog>(++objectNumber).ToPdfIndirectIdentifier();
        this.RootPageTree = new PdfIndirect<IPageTreeNode>(++objectNumber).ToPdfIndirectIdentifier();
        this.Pages = new List<IPdfIndirectIdentifier<IPageObject>>(1) { new PdfIndirect<IPageObject>(++objectNumber).ToPdfIndirectIdentifier() }.ToImmutableList();
        this.contentStreamIndirect = new PdfIndirect<IPdfStream>(++objectNumber).ToPdfIndirectIdentifier();
        this.Fonts = new List<IPdfIndirectIdentifier<Type1Font>>(1) { new PdfIndirect<Type1Font>(++objectNumber).ToPdfIndirectIdentifier() }.ToImmutableList();

        PdfOperation[] pdfOperations =
        {
            new FontOperation("F1", 24),
            new MoveTextOperation(100, 100),
            new ShowTextOperation("Hello World"),
        };

        TextObject textObject = new(pdfOperations);
        IPdfDictionary<IPdfIndirectIdentifier<Type1Font>> fontDictionary = new Dictionary<PdfName, IPdfIndirectIdentifier<Type1Font>> { { "F1", this.Fonts[0] } }.ToPdfDictionary();

        this.DocumentCatalog.PdfIndirect.Value = new DocumentCatalog(documentCatalogOptions => documentCatalogOptions.Pages = this.RootPageTree);
        this.RootPageTree.PdfIndirect.Value = new PageTreeNode(pageTreeNodeOptions => pageTreeNodeOptions.Kids = this.Pages.ToPdfArray());

        foreach (IPdfIndirectIdentifier<IPageObject> pageObjectIndirect in this.Pages)
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
        int byteOffset = 0;
        List<IXRefEntry> xRefEntries = new(objectNumber) { new XRefEntry(byteOffset, 65535, XRefEntryType.Free) };

        byteOffset += this.FileHeader.Bytes.Length;
        xRefEntries.Add(new XRefEntry(byteOffset, 0, XRefEntryType.InUse));

        byteOffset += this.DocumentCatalog.PdfIndirect.Bytes.Length;
        xRefEntries.Add(new XRefEntry(byteOffset, 0, XRefEntryType.InUse));

        byteOffset += this.RootPageTree.PdfIndirect.Bytes.Length;
        xRefEntries.Add(new XRefEntry(byteOffset, 0, XRefEntryType.InUse));

        foreach (IPdfIndirectIdentifier<IPageObject> pageObjectIndirect in this.Pages)
        {
            byteOffset += pageObjectIndirect.PdfIndirect.Bytes.Length;
            xRefEntries.Add(new XRefEntry(byteOffset, 0, XRefEntryType.InUse));
        }

        byteOffset += this.contentStreamIndirect.PdfIndirect.Bytes.Length;
        xRefEntries.Add(new XRefEntry(byteOffset, 0, XRefEntryType.InUse));

        for (int index = 0; index < this.Fonts.Count; index++)
        {
            IPdfIndirectIdentifier<Type1Font> fontIndirect = this.Fonts[index];
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

    public FileHeader FileHeader => FileHeader.PdfVersion17;

    public IPdfIndirectIdentifier<IDocumentCatalog> DocumentCatalog { get; }

    public IPdfIndirectIdentifier<IPageTreeNode> RootPageTree { get; }

    public IImmutableList<IPdfIndirectIdentifier<IPageObject>> Pages { get; }

    public IImmutableList<IPdfIndirectIdentifier<Type1Font>> Fonts { get; }

    public IXRefTable XRefTable { get; }

    public IFileTrailer FileTrailer { get; }

    public async Task GenerateOutputStream(CancellationToken cancellationToken = default)
    {
        await this.stream.WriteAsync(this.FileHeader.Bytes, cancellationToken).ConfigureAwait(false);
        await this.stream.WriteAsync(this.DocumentCatalog.PdfIndirect.Bytes, cancellationToken).ConfigureAwait(false);
        await this.stream.WriteAsync(this.RootPageTree.PdfIndirect.Bytes, cancellationToken).ConfigureAwait(false);

        foreach (IPdfIndirectIdentifier<IPageObject> pageObjectIndirect in this.Pages)
        {
            await this.stream.WriteAsync(pageObjectIndirect.PdfIndirect.Bytes, cancellationToken).ConfigureAwait(false);
        }

        await this.stream.WriteAsync(this.contentStreamIndirect.PdfIndirect.Bytes, cancellationToken).ConfigureAwait(false);

        foreach (IPdfIndirectIdentifier<Type1Font> fontIndirect in this.Fonts)
        {
            await this.stream.WriteAsync(fontIndirect.PdfIndirect.Bytes, cancellationToken).ConfigureAwait(false);
        }

        await this.stream.WriteAsync(this.XRefTable.Bytes, cancellationToken).ConfigureAwait(false);
        await this.stream.WriteAsync(this.FileTrailer.Bytes, cancellationToken).ConfigureAwait(false);
    }

    public void Dispose()
    {
        this.stream.Dispose();
    }

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

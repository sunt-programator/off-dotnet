// <copyright file="SimplePdfBenchmarks.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Benchmarks;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using OffDotNet.Pdf.Core.Common;
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

[MemoryDiagnoser]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class SimplePdfBenchmarks
{
    private MemoryStream? memoryStream;
    private BinaryWriter? streamWriter;

    [GlobalSetup]
    public void Setup()
    {
        this.memoryStream = new MemoryStream();
        this.streamWriter = new BinaryWriter(this.memoryStream);
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        this.memoryStream?.Dispose();
        this.streamWriter?.Dispose();
    }

    [Benchmark]
    [BenchmarkCategory("Same Content Stream")]
    [Arguments(1)]
    [Arguments(10)]
    [Arguments(100)]
    [Arguments(1000)]
    [Arguments(10000)]
    public MemoryStream? BasicPdfWithMultiplePagesAndSameContentStream(int pagesCount)
    {
        if (this.streamWriter == null)
        {
            return this.memoryStream;
        }

        FileHeader fileHeader = FileHeader.PdfVersion17;

        int objectNumber = 0;
        IPdfIndirectIdentifier<IDocumentCatalog> documentCatalogIndirect = new PdfIndirect<IDocumentCatalog>(++objectNumber).ToPdfIndirectIdentifier();
        IPdfIndirectIdentifier<IPageTreeNode> rootPageTreeNodeIndirect = new PdfIndirect<IPageTreeNode>(++objectNumber).ToPdfIndirectIdentifier();

        List<IPdfIndirectIdentifier<IPageObject>> pageObjectsIndirectList = new(pagesCount);
        for (int i = 0; i < pagesCount; i++)
        {
            pageObjectsIndirectList.Add(new PdfIndirect<IPageObject>(++objectNumber).ToPdfIndirectIdentifier());
        }

        IPdfIndirectIdentifier<IPdfStream> contentStreamIndirect = new PdfIndirect<IPdfStream>(++objectNumber).ToPdfIndirectIdentifier();
        IPdfIndirectIdentifier<IType1Font> fontIndirect = new PdfIndirect<IType1Font>(objectNumber).ToPdfIndirectIdentifier();

        IPdfOperation[] pdfOperations =
        {
            new FontOperation("F1", 24),
            new MoveTextOperation(100, 100),
            new ShowTextOperation("Hello World"),
        };

        ITextObject textObject = new TextObject(pdfOperations);

        IPdfDictionary<IPdfIndirectIdentifier<IType1Font>> fontDictionary = new Dictionary<PdfName, IPdfIndirectIdentifier<IType1Font>> { { "F1", fontIndirect } }.ToPdfDictionary();

        documentCatalogIndirect.PdfIndirect.Value = new DocumentCatalog(options => options.Pages = rootPageTreeNodeIndirect);
        rootPageTreeNodeIndirect.PdfIndirect.Value = new PageTreeNode(options => options.Kids = pageObjectsIndirectList.ToPdfArray());

        foreach (IPdfIndirectIdentifier<IPageObject> pageObjectIndirect in pageObjectsIndirectList)
        {
            pageObjectIndirect.PdfIndirect.Value = new PageObject(options =>
            {
                options.Parent = rootPageTreeNodeIndirect;
                options.MediaBox = new Rectangle(0, 0, 612, 792);
                options.Contents = new(contentStreamIndirect);
                options.Resources = new ResourceDictionary(resourceDictionaryOptions => resourceDictionaryOptions.Font = fontDictionary);
            });
        }

        contentStreamIndirect.PdfIndirect.Value = new PdfStream(textObject.Content.AsMemory());
        fontIndirect.PdfIndirect.Value = StandardFonts.Helvetica;

        // XTable
        int byteOffset = 0;
        List<IXRefEntry> xRefEntries = new(5 + pagesCount) { new XRefEntry(byteOffset, 65535, XRefEntryType.Free) };

        byteOffset += fileHeader.Bytes.Length;
        xRefEntries.Add(new XRefEntry(byteOffset, 0, XRefEntryType.InUse));

        byteOffset += documentCatalogIndirect.PdfIndirect.Bytes.Length;
        xRefEntries.Add(new XRefEntry(byteOffset, 0, XRefEntryType.InUse));

        byteOffset += rootPageTreeNodeIndirect.PdfIndirect.Bytes.Length;
        xRefEntries.Add(new XRefEntry(byteOffset, 0, XRefEntryType.InUse));

        foreach (IPdfIndirectIdentifier<IPageObject> pageObjectIndirect in pageObjectsIndirectList)
        {
            byteOffset += pageObjectIndirect.PdfIndirect.Bytes.Length;
            xRefEntries.Add(new XRefEntry(byteOffset, 0, XRefEntryType.InUse));
        }

        byteOffset += contentStreamIndirect.PdfIndirect.Bytes.Length;
        xRefEntries.Add(new XRefEntry(byteOffset, 0, XRefEntryType.InUse));

        byteOffset += fontIndirect.PdfIndirect.Bytes.Length;
        IXRefTable xRefTable = xRefEntries.ToXRefTable(0);

        IFileTrailer fileTrailer = new FileTrailer(byteOffset, options =>
        {
            options.Size = 5;
            options.Root = documentCatalogIndirect;
        });

        this.streamWriter.Write(fileHeader.Bytes.Span);
        this.streamWriter.Write(documentCatalogIndirect.PdfIndirect.Bytes.Span);
        this.streamWriter.Write(rootPageTreeNodeIndirect.PdfIndirect.Bytes.Span);

        foreach (IPdfIndirectIdentifier<IPageObject> pageObjectIndirect in pageObjectsIndirectList)
        {
            this.streamWriter.Write(pageObjectIndirect.PdfIndirect.Bytes.Span);
        }

        this.streamWriter.Write(contentStreamIndirect.PdfIndirect.Bytes.Span);
        this.streamWriter.Write(fontIndirect.PdfIndirect.Bytes.Span);
        this.streamWriter.Write(xRefTable.Bytes.Span);
        this.streamWriter.Write(fileTrailer.Bytes.Span);

        return this.memoryStream;
    }

    [Benchmark]
    [BenchmarkCategory("Different Content Streams")]
    [Arguments(1)]
    [Arguments(10)]
    [Arguments(100)]
    [Arguments(1000)]
    [Arguments(10000)]
    public MemoryStream? BasicPdfWithMultiplePagesAndDifferentContentStreams(int pagesCount)
    {
        if (this.streamWriter == null)
        {
            return this.memoryStream;
        }

        FileHeader fileHeader = FileHeader.PdfVersion17;

        int objectNumber = 0;
        IPdfIndirectIdentifier<IDocumentCatalog> documentCatalogIndirect = new PdfIndirect<IDocumentCatalog>(++objectNumber).ToPdfIndirectIdentifier();
        IPdfIndirectIdentifier<IPageTreeNode> rootPageTreeNodeIndirect = new PdfIndirect<IPageTreeNode>(++objectNumber).ToPdfIndirectIdentifier();

        List<IPdfIndirectIdentifier<IPageObject>> pageObjectsIndirectList = new(pagesCount);
        List<IPdfIndirectIdentifier<IPdfStream>> contentStreamsIndirectList = new(pagesCount);
        for (int i = 0; i < pagesCount; i++)
        {
            pageObjectsIndirectList.Add(new PdfIndirect<IPageObject>(++objectNumber).ToPdfIndirectIdentifier());
        }

        for (int i = 0; i < pagesCount; i++)
        {
            contentStreamsIndirectList.Add(new PdfIndirect<IPdfStream>(++objectNumber).ToPdfIndirectIdentifier());
        }

        IPdfIndirectIdentifier<IType1Font> fontIndirect = new PdfIndirect<IType1Font>(objectNumber).ToPdfIndirectIdentifier();

        IPdfOperation[] pdfOperations =
        {
            new FontOperation("F1", 24),
            new MoveTextOperation(100, 100),
            new ShowTextOperation("Hello World"),
        };

        ITextObject textObject = new TextObject(pdfOperations);

        IPdfDictionary<IPdfIndirectIdentifier<IType1Font>> fontDictionary = new Dictionary<PdfName, IPdfIndirectIdentifier<IType1Font>> { { "F1", fontIndirect } }.ToPdfDictionary();

        documentCatalogIndirect.PdfIndirect.Value = new DocumentCatalog(options => options.Pages = rootPageTreeNodeIndirect);
        rootPageTreeNodeIndirect.PdfIndirect.Value = new PageTreeNode(options => options.Kids = pageObjectsIndirectList.ToPdfArray());

        for (int i = 0; i < pageObjectsIndirectList.Count; i++)
        {
            int index = i;
            pageObjectsIndirectList[i].PdfIndirect.Value = new PageObject(options =>
            {
                options.Parent = rootPageTreeNodeIndirect;
                options.MediaBox = new Rectangle(0, 0, 612, 792);
                options.Contents = new(contentStreamsIndirectList[index]);
                options.Resources = new ResourceDictionary(resourceDictionaryOptions => resourceDictionaryOptions.Font = fontDictionary);
            });
        }

        foreach (IPdfIndirectIdentifier<IPdfStream> contentStreamIndirect in contentStreamsIndirectList)
        {
            contentStreamIndirect.PdfIndirect.Value = new PdfStream(textObject.Content.AsMemory());
        }

        fontIndirect.PdfIndirect.Value = StandardFonts.Helvetica;

        // XTable
        int byteOffset = 0;
        List<IXRefEntry> xRefEntries = new(5 + (pagesCount * 2)) { new XRefEntry(byteOffset, 65535, XRefEntryType.Free) };

        byteOffset += fileHeader.Bytes.Length;
        xRefEntries.Add(new XRefEntry(byteOffset, 0, XRefEntryType.InUse));

        byteOffset += documentCatalogIndirect.PdfIndirect.Bytes.Length;
        xRefEntries.Add(new XRefEntry(byteOffset, 0, XRefEntryType.InUse));

        byteOffset += rootPageTreeNodeIndirect.PdfIndirect.Bytes.Length;
        xRefEntries.Add(new XRefEntry(byteOffset, 0, XRefEntryType.InUse));

        foreach (IPdfIndirectIdentifier<IPageObject> pageObjectIndirect in pageObjectsIndirectList)
        {
            byteOffset += pageObjectIndirect.PdfIndirect.Bytes.Length;
            xRefEntries.Add(new XRefEntry(byteOffset, 0, XRefEntryType.InUse));
        }

        foreach (IPdfIndirectIdentifier<IPdfStream> contentStreamIndirect in contentStreamsIndirectList)
        {
            byteOffset += contentStreamIndirect.PdfIndirect.Bytes.Length;
            xRefEntries.Add(new XRefEntry(byteOffset, 0, XRefEntryType.InUse));
        }

        byteOffset += fontIndirect.PdfIndirect.Bytes.Length;
        IXRefTable xRefTable = xRefEntries.ToXRefTable(0);

        IFileTrailer fileTrailer = new FileTrailer(byteOffset, options =>
        {
            options.Size = 5;
            options.Root = documentCatalogIndirect;
        });

        this.streamWriter.Write(fileHeader.Bytes.Span);
        this.streamWriter.Write(documentCatalogIndirect.PdfIndirect.Bytes.Span);
        this.streamWriter.Write(rootPageTreeNodeIndirect.PdfIndirect.Bytes.Span);

        foreach (IPdfIndirectIdentifier<IPageObject> pageObjectIndirect in pageObjectsIndirectList)
        {
            this.streamWriter.Write(pageObjectIndirect.PdfIndirect.Bytes.Span);
        }

        foreach (IPdfIndirectIdentifier<IPdfStream> contentStreamIndirect in contentStreamsIndirectList)
        {
            this.streamWriter.Write(contentStreamIndirect.PdfIndirect.Bytes.Span);
        }

        this.streamWriter.Write(fontIndirect.PdfIndirect.Bytes.Span);
        this.streamWriter.Write(xRefTable.Bytes.Span);
        this.streamWriter.Write(fileTrailer.Bytes.Span);

        return this.memoryStream;
    }
}

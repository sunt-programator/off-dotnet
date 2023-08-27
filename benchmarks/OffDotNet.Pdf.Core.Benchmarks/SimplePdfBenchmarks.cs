// <copyright file="SimplePdfBenchmarks.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
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

namespace OffDotNet.Pdf.Core.Benchmarks;

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
        PdfIndirectIdentifier<DocumentCatalog> documentCatalogIndirect = new PdfIndirect<DocumentCatalog>(++objectNumber).ToPdfIndirectIdentifier();
        PdfIndirectIdentifier<PageTreeNode> rootPageTreeNodeIndirect = new PdfIndirect<PageTreeNode>(++objectNumber).ToPdfIndirectIdentifier();

        List<PdfIndirectIdentifier<PageObject>> pageObjectsIndirectList = new(pagesCount);
        for (int i = 0; i < pagesCount; i++)
        {
            pageObjectsIndirectList.Add(new PdfIndirect<PageObject>(++objectNumber).ToPdfIndirectIdentifier());
        }

        PdfIndirectIdentifier<PdfStream> contentStreamIndirect = new PdfIndirect<PdfStream>(++objectNumber).ToPdfIndirectIdentifier();
        PdfIndirectIdentifier<Type1Font> fontIndirect = new PdfIndirect<Type1Font>(objectNumber).ToPdfIndirectIdentifier();

        PdfOperation[] pdfOperations =
        {
            new FontOperation("F1", 24),
            new MoveTextOperation(100, 100),
            new ShowTextOperation("Hello World"),
        };

        TextObject textObject = new(pdfOperations);

        PdfDictionary<PdfIndirectIdentifier<Type1Font>> fontDictionary = new Dictionary<PdfName, PdfIndirectIdentifier<Type1Font>> { { "F1", fontIndirect } }.ToPdfDictionary();

        documentCatalogIndirect.PdfIndirect.Value = new DocumentCatalog(options => options.Pages = rootPageTreeNodeIndirect);
        rootPageTreeNodeIndirect.PdfIndirect.Value = new PageTreeNode(options => options.Kids = pageObjectsIndirectList.ToPdfArray());

        foreach (PdfIndirectIdentifier<PageObject> pageObjectIndirect in pageObjectsIndirectList)
        {
            pageObjectIndirect.PdfIndirect.Value = new PageObject(options =>
            {
                options.Parent = rootPageTreeNodeIndirect;
                options.MediaBox = new Rectangle(0, 0, 612, 792);
                options.Contents = contentStreamIndirect;
                options.Resources = new ResourceDictionary(resourceDictionaryOptions => resourceDictionaryOptions.Font = fontDictionary);
            });
        }

        contentStreamIndirect.PdfIndirect.Value = new PdfStream(textObject.Content.AsMemory());
        fontIndirect.PdfIndirect.Value = StandardFonts.Helvetica;

        // XTable
        int byteOffset = 0;
        List<XRefEntry> xRefEntries = new(5 + pagesCount) { new XRefEntry(byteOffset, 65535, XRefEntryType.Free) };

        byteOffset += fileHeader.Length;
        xRefEntries.Add(new XRefEntry(byteOffset, 0, XRefEntryType.InUse));

        byteOffset += documentCatalogIndirect.PdfIndirect.Length;
        xRefEntries.Add(new XRefEntry(byteOffset, 0, XRefEntryType.InUse));

        byteOffset += rootPageTreeNodeIndirect.PdfIndirect.Length;
        xRefEntries.Add(new XRefEntry(byteOffset, 0, XRefEntryType.InUse));

        foreach (PdfIndirectIdentifier<PageObject> pageObjectIndirect in pageObjectsIndirectList)
        {
            byteOffset += pageObjectIndirect.PdfIndirect.Length;
            xRefEntries.Add(new XRefEntry(byteOffset, 0, XRefEntryType.InUse));
        }

        byteOffset += contentStreamIndirect.PdfIndirect.Length;
        xRefEntries.Add(new XRefEntry(byteOffset, 0, XRefEntryType.InUse));

        byteOffset += fontIndirect.PdfIndirect.Length;
        XRefTable xRefTable = xRefEntries.ToXRefTable(0);

        FileTrailer fileTrailer = new(byteOffset, options =>
        {
            options.Size = 5;
            options.Root = documentCatalogIndirect;
        });

        this.streamWriter.Write(fileHeader.Bytes.Span);
        this.streamWriter.Write(documentCatalogIndirect.PdfIndirect.Bytes.Span);
        this.streamWriter.Write(rootPageTreeNodeIndirect.PdfIndirect.Bytes.Span);

        foreach (PdfIndirectIdentifier<PageObject> pageObjectIndirect in pageObjectsIndirectList)
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
        PdfIndirectIdentifier<DocumentCatalog> documentCatalogIndirect = new PdfIndirect<DocumentCatalog>(++objectNumber).ToPdfIndirectIdentifier();
        PdfIndirectIdentifier<PageTreeNode> rootPageTreeNodeIndirect = new PdfIndirect<PageTreeNode>(++objectNumber).ToPdfIndirectIdentifier();

        List<PdfIndirectIdentifier<PageObject>> pageObjectsIndirectList = new(pagesCount);
        List<PdfIndirectIdentifier<PdfStream>> contentStreamsIndirectList = new(pagesCount);
        for (int i = 0; i < pagesCount; i++)
        {
            pageObjectsIndirectList.Add(new PdfIndirect<PageObject>(++objectNumber).ToPdfIndirectIdentifier());
        }

        for (int i = 0; i < pagesCount; i++)
        {
            contentStreamsIndirectList.Add(new PdfIndirect<PdfStream>(++objectNumber).ToPdfIndirectIdentifier());
        }

        PdfIndirectIdentifier<Type1Font> fontIndirect = new PdfIndirect<Type1Font>(objectNumber).ToPdfIndirectIdentifier();

        PdfOperation[] pdfOperations =
        {
            new FontOperation("F1", 24),
            new MoveTextOperation(100, 100),
            new ShowTextOperation("Hello World"),
        };

        TextObject textObject = new(pdfOperations);

        PdfDictionary<PdfIndirectIdentifier<Type1Font>> fontDictionary = new Dictionary<PdfName, PdfIndirectIdentifier<Type1Font>> { { "F1", fontIndirect } }.ToPdfDictionary();

        documentCatalogIndirect.PdfIndirect.Value = new DocumentCatalog(options => options.Pages = rootPageTreeNodeIndirect);
        rootPageTreeNodeIndirect.PdfIndirect.Value = new PageTreeNode(options => options.Kids = pageObjectsIndirectList.ToPdfArray());

        for (int i = 0; i < pageObjectsIndirectList.Count; i++)
        {
            int index = i;
            pageObjectsIndirectList[i].PdfIndirect.Value = new PageObject(options =>
            {
                options.Parent = rootPageTreeNodeIndirect;
                options.MediaBox = new Rectangle(0, 0, 612, 792);
                options.Contents = contentStreamsIndirectList[index];
                options.Resources = new ResourceDictionary(resourceDictionaryOptions => resourceDictionaryOptions.Font = fontDictionary);
            });
        }

        foreach (PdfIndirectIdentifier<PdfStream> contentStreamIndirect in contentStreamsIndirectList)
        {
            contentStreamIndirect.PdfIndirect.Value = new PdfStream(textObject.Content.AsMemory());
        }

        fontIndirect.PdfIndirect.Value = StandardFonts.Helvetica;

        // XTable
        int byteOffset = 0;
        List<XRefEntry> xRefEntries = new(5 + (pagesCount * 2)) { new XRefEntry(byteOffset, 65535, XRefEntryType.Free) };

        byteOffset += fileHeader.Length;
        xRefEntries.Add(new XRefEntry(byteOffset, 0, XRefEntryType.InUse));

        byteOffset += documentCatalogIndirect.PdfIndirect.Length;
        xRefEntries.Add(new XRefEntry(byteOffset, 0, XRefEntryType.InUse));

        byteOffset += rootPageTreeNodeIndirect.PdfIndirect.Length;
        xRefEntries.Add(new XRefEntry(byteOffset, 0, XRefEntryType.InUse));

        foreach (PdfIndirectIdentifier<PageObject> pageObjectIndirect in pageObjectsIndirectList)
        {
            byteOffset += pageObjectIndirect.PdfIndirect.Length;
            xRefEntries.Add(new XRefEntry(byteOffset, 0, XRefEntryType.InUse));
        }

        foreach (PdfIndirectIdentifier<PdfStream> contentStreamIndirect in contentStreamsIndirectList)
        {
            byteOffset += contentStreamIndirect.PdfIndirect.Length;
            xRefEntries.Add(new XRefEntry(byteOffset, 0, XRefEntryType.InUse));
        }

        byteOffset += fontIndirect.PdfIndirect.Length;
        XRefTable xRefTable = xRefEntries.ToXRefTable(0);

        FileTrailer fileTrailer = new(byteOffset, options =>
        {
            options.Size = 5;
            options.Root = documentCatalogIndirect;
        });

        this.streamWriter.Write(fileHeader.Bytes.Span);
        this.streamWriter.Write(documentCatalogIndirect.PdfIndirect.Bytes.Span);
        this.streamWriter.Write(rootPageTreeNodeIndirect.PdfIndirect.Bytes.Span);

        foreach (PdfIndirectIdentifier<PageObject> pageObjectIndirect in pageObjectsIndirectList)
        {
            this.streamWriter.Write(pageObjectIndirect.PdfIndirect.Bytes.Span);
        }

        foreach (PdfIndirectIdentifier<PdfStream> contentStreamIndirect in contentStreamsIndirectList)
        {
            this.streamWriter.Write(contentStreamIndirect.PdfIndirect.Bytes.Span);
        }

        this.streamWriter.Write(fontIndirect.PdfIndirect.Bytes.Span);
        this.streamWriter.Write(xRefTable.Bytes.Span);
        this.streamWriter.Write(fileTrailer.Bytes.Span);

        return this.memoryStream;
    }
}

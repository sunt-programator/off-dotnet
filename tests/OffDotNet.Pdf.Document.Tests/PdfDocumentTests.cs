// <copyright file="PdfDocumentTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Immutable;
using System.Text;
using OffDotNet.Pdf.Core.CommonDataStructures;
using OffDotNet.Pdf.Core.ContentStreamAndResources;
using OffDotNet.Pdf.Core.DocumentStructure;
using OffDotNet.Pdf.Core.FileStructure;
using OffDotNet.Pdf.Core.Primitives;
using OffDotNet.Pdf.Core.Text.Fonts;
using Xunit;

namespace OffDotNet.Pdf.Document.Tests;

public class PdfDocumentTests
{
    [Fact(DisplayName = $"The {nameof(PdfDocument)} constructor without arguments should generate a valid PDF output stream")]
    public async Task PdfDocument_ConstructorWithoutArguments_ShouldGenerateValidOutputStream()
    {
        // Arrange
        const string expectedContent =
            "%PDF-1.7\n" +
            "1 0 obj\n<</Type /Catalog /Pages 2 0 R>>\nendobj\n" +
            "2 0 obj\n<</Type /Pages /Kids [3 0 R] /Count 1>>\nendobj\n" +
            "3 0 obj\n<</Type /Page /Parent 2 0 R /Resources <</Font <</F1 5 0 R>> /ProcSet [/PDF /Text /ImageB /ImageC /ImageI]>> /MediaBox [0 0 612 792] /Contents 4 0 R>>\nendobj\n" +
            "4 0 obj\n<</Length 44>>\nstream\nBT\n/F1 24 Tf\n100 100 Td\n(Hello World) Tj\nET\nendstream\nendobj\n" +
            "5 0 obj\n<</Type /Font /Subtype /Type1 /Name /Helvetica /BaseFont /Helvetica>>\nendobj\n" +
            "xref\n0 6\n0000000000 65535 f \n0000000009 00000 n \n0000000056 00000 n \n0000000111 00000 n \n0000000277 00000 n \n0000000368 00000 n \n" +
            "trailer\n<</Size 5 /Root 1 0 R>>\n" +
            "startxref\n453\n%%EOF";

        byte[] expectedBytes = Encoding.ASCII.GetBytes(expectedContent);
        MemoryStream stream = new();
        PdfDocument pdfDocument = new(stream, _ => { });

        // Act
        await using (stream.ConfigureAwait(false))
        await using (pdfDocument.ConfigureAwait(false))
        {
#pragma warning disable CA2007
            await pdfDocument.GenerateOutputStream();
#pragma warning restore CA2007

            // Assert
            Assert.Equal(expectedBytes, stream.ToArray());
        }
    }

    [Fact(DisplayName = $"The {nameof(PdfDocument.FileHeader)} property should return {nameof(FileHeader.PdfVersion17)} by default")]
    public async Task PdfDocument_ConstructorWithoutArguments_FileHeader_ShouldReturnPdf17()
    {
        // Arrange
        MemoryStream stream = new();
        PdfDocument pdfDocument = new(stream, new PdfDocumentOptions());

        // Act
        await using (stream.ConfigureAwait(false))
        await using (pdfDocument.ConfigureAwait(false))
        {
            // Assert
            Assert.Equal(FileHeader.PdfVersion17, pdfDocument.FileHeader);
        }
    }

    [Fact(DisplayName = $"The {nameof(PdfDocument.DocumentCatalog)} property should be predefined by default")]
    public async Task PdfDocument_ConstructorWithoutArguments_DocumentCatalog_ShouldBePredefined()
    {
        // Arrange
        var rootPageTree = new PdfIndirect<PageTreeNode>(2).ToPdfIndirectIdentifier();
        IPdfIndirectIdentifier<DocumentCatalog> expectedDocumentCatalog = new DocumentCatalog(documentCatalogOptions => documentCatalogOptions.Pages = rootPageTree)
            .ToPdfIndirect(1)
            .ToPdfIndirectIdentifier();

        MemoryStream stream = new();
        PdfDocument pdfDocument = new(stream, new PdfDocumentOptions());

        // Act
        await using (stream.ConfigureAwait(false))
        await using (pdfDocument.ConfigureAwait(false))
        {
            // Assert
            Assert.Equal(expectedDocumentCatalog, pdfDocument.DocumentCatalog);
            Assert.Equal(expectedDocumentCatalog.Content, pdfDocument.DocumentCatalog.Content);
            Assert.Equal(expectedDocumentCatalog.PdfIndirect.Content, pdfDocument.DocumentCatalog.PdfIndirect.Content);
        }
    }

    [Fact(DisplayName = $"The {nameof(PdfDocument.RootPageTree)} property should be predefined by default")]
    public async Task PdfDocument_ConstructorWithoutArguments_RootPageTree_ShouldBePredefined()
    {
        // Arrange
        var pages = new List<IPdfIndirectIdentifier<PageObject>>(1) { new PdfIndirect<PageObject>(3).ToPdfIndirectIdentifier() }.ToImmutableList();
        var expectedPageTree = new PageTreeNode(pageTreeNodeOptions => pageTreeNodeOptions.Kids = pages.ToPdfArray()).ToPdfIndirect(2).ToPdfIndirectIdentifier();

        MemoryStream stream = new();
        PdfDocument pdfDocument = new(stream, new PdfDocumentOptions());

        // Act
        await using (stream.ConfigureAwait(false))
        await using (pdfDocument.ConfigureAwait(false))
        {
            // Assert
            Assert.Equal(expectedPageTree, pdfDocument.RootPageTree);
            Assert.Equal(expectedPageTree.Content, pdfDocument.RootPageTree.Content);
            Assert.Equal(expectedPageTree.PdfIndirect.Content, pdfDocument.RootPageTree.PdfIndirect.Content);
        }
    }

    [Fact(DisplayName = $"The {nameof(PdfDocument.Pages)} property should be predefined by default")]
    public async Task PdfDocument_ConstructorWithoutArguments_Pages_ShouldBePredefined()
    {
        // Arrange
        PageObject pageObject = new(pageObjectOptions =>
        {
            pageObjectOptions.Parent = new PdfIndirect<PageTreeNode>(2).ToPdfIndirectIdentifier();
            pageObjectOptions.MediaBox = new Rectangle(0, 0, 612, 792);
            pageObjectOptions.Contents = new(new PdfIndirect<IPdfStream>(4).ToPdfIndirectIdentifier());
            pageObjectOptions.Resources = new ResourceDictionary(resourceDictionaryOptions =>
                resourceDictionaryOptions.Font = new Dictionary<PdfName, IPdfIndirectIdentifier<Type1Font>>
                {
                    { "F1", new PdfIndirect<Type1Font>(5).ToPdfIndirectIdentifier() },
                }.ToPdfDictionary());
        });

        var expectedPages = new List<IPdfIndirectIdentifier<PageObject>>(1)
        {
            pageObject.ToPdfIndirect(3).ToPdfIndirectIdentifier(),
        }.ToImmutableList();

        MemoryStream stream = new();
        PdfDocument pdfDocument = new(stream, new PdfDocumentOptions());

        // Act
        await using (stream.ConfigureAwait(false))
        await using (pdfDocument.ConfigureAwait(false))
        {
            // Assert
            Assert.Single(pdfDocument.Pages);
            Assert.Equal(expectedPages[0], pdfDocument.Pages[0]);
            Assert.Equal(expectedPages[0].Content, pdfDocument.Pages[0].Content);
            Assert.Equal(expectedPages[0].PdfIndirect.Content, pdfDocument.Pages[0].PdfIndirect.Content);
        }
    }

    [Fact(DisplayName = $"The {nameof(PdfDocument.Fonts)} property should be predefined by default")]
    public async Task PdfDocument_ConstructorWithoutArguments_Fonts_ShouldBePredefined()
    {
        // Arrange
        var expectedFonts = new List<IPdfIndirectIdentifier<Type1Font>>(1) { StandardFonts.Helvetica.ToPdfIndirect(5).ToPdfIndirectIdentifier() }.ToImmutableList();

        MemoryStream stream = new();
        PdfDocument pdfDocument = new(stream, new PdfDocumentOptions());

        // Act
        await using (stream.ConfigureAwait(false))
        await using (pdfDocument.ConfigureAwait(false))
        {
            // Assert
            Assert.Single(expectedFonts);
            Assert.Equal(expectedFonts[0], pdfDocument.Fonts[0]);
            Assert.Equal(expectedFonts[0].Content, pdfDocument.Fonts[0].Content);
            Assert.Equal(expectedFonts[0].PdfIndirect.Content, pdfDocument.Fonts[0].PdfIndirect.Content);
        }
    }

    [Fact(DisplayName = $"The {nameof(PdfDocument.XRefTable)} property should be predefined by default")]
    public async Task PdfDocument_ConstructorWithoutArguments_XRefTable_ShouldBePredefined()
    {
        // Arrange
        IXRefTable expectedXRefTable = new List<IXRefEntry>
        {
            new XRefEntry(0, 65535, XRefEntryType.Free),
            new XRefEntry(9, 0, XRefEntryType.InUse),
            new XRefEntry(56, 0, XRefEntryType.InUse),
            new XRefEntry(111, 0, XRefEntryType.InUse),
            new XRefEntry(277, 0, XRefEntryType.InUse),
            new XRefEntry(368, 0, XRefEntryType.InUse),
        }.ToXRefTable(0);

        MemoryStream stream = new();
        PdfDocument pdfDocument = new(stream, new PdfDocumentOptions());

        // Act
        await using (stream.ConfigureAwait(false))
        await using (pdfDocument.ConfigureAwait(false))
        {
            // Assert
            Assert.Equal(expectedXRefTable, pdfDocument.XRefTable);
            Assert.Equal(expectedXRefTable.Content, pdfDocument.XRefTable.Content);
        }
    }

    [Fact(DisplayName = $"The {nameof(PdfDocument.FileTrailer)} property should be predefined by default")]
    public async Task PdfDocument_ConstructorWithoutArguments_FileTrailer_ShouldBePredefined()
    {
        // Arrange
        var expectedFileTrailer = new FileTrailer(453, fileTrailerOptions =>
        {
            fileTrailerOptions.Size = 5;
            fileTrailerOptions.Root = new PdfIndirect<DocumentCatalog>(1).ToPdfIndirectIdentifier();
        });

        MemoryStream stream = new();
        PdfDocument pdfDocument = new(stream, new PdfDocumentOptions());

        // Act
        await using (stream.ConfigureAwait(false))
        await using (pdfDocument.ConfigureAwait(false))
        {
            // Assert
            Assert.Equal(expectedFileTrailer.Content, pdfDocument.FileTrailer.Content);
            Assert.Equal(expectedFileTrailer, pdfDocument.FileTrailer);
        }
    }
}

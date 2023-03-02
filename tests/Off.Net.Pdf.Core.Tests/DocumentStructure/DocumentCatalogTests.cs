using System;
using System.Collections.Generic;
using Off.Net.Pdf.Core.DocumentStructure;
using Off.Net.Pdf.Core.Interfaces;
using Off.Net.Pdf.Core.Primitives;
using Xunit;

namespace Off.Net.Pdf.Core.Tests.DocumentStructure;

public class DocumentCatalogTests
{
    [Fact(DisplayName = $"Constructor with an empty {nameof(DocumentCatalogOptions.Pages)} dictionary should throw an {nameof(ArgumentOutOfRangeException)}")]
    public void DocumentCatalog_ConstructorWithEmptyPagesDictionary_ShouldThrowException()
    {
        // Arrange
        DocumentCatalogOptions documentCatalogOptions = new() { Pages = new Dictionary<PdfName, IPdfObject>(0).ToPdfDictionary() };

        // Act
        DocumentCatalog DocumentCatalogFunction() => new(documentCatalogOptions);

        // Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(DocumentCatalogFunction);
        Assert.StartsWith(Resource.DocumentCatalog_Pages_MustNotBeEmpty, exception.Message);
    }

    [Fact(DisplayName = $"The {nameof(DocumentCatalog.Content)} should return a valid value")]
    public void DocumentCatalog_Content_ShouldReturnValidValue()
    {
        // Arrange
        const string expectedContent = "<</Type /Catalog /Pages <</TestName 0>>>>";
        PdfDictionary<IPdfObject> pages = new Dictionary<PdfName, IPdfObject> { { "TestName", new PdfInteger(0) } }.ToPdfDictionary();
        DocumentCatalog documentCatalog = new(options => options.Pages = pages);

        // Act
        string actualContent = documentCatalog.Content;

        // Assert
        Assert.Equal(expectedContent, actualContent);
    }
}

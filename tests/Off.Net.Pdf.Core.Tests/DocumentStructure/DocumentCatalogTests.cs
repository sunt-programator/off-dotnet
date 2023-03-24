using Off.Net.Pdf.Core.DocumentStructure;
using Off.Net.Pdf.Core.Interfaces;
using Off.Net.Pdf.Core.Primitives;
using Xunit;

namespace Off.Net.Pdf.Core.Tests.DocumentStructure;

public class DocumentCatalogTests
{
    [Fact(DisplayName = $"Constructor with an empty {nameof(DocumentCatalogOptions.Pages)} dictionary should throw an {nameof(ArgumentNullException)}")]
    public void DocumentCatalog_ConstructorWithEmptyPagesDictionary_ShouldThrowException()
    {
        // Arrange
        DocumentCatalogOptions documentCatalogOptions = new() { Pages = null! };

        // Act
        DocumentCatalog DocumentCatalogFunction() => new(documentCatalogOptions);

        // Assert
        Assert.Throws<ArgumentNullException>(DocumentCatalogFunction);
    }

    [Fact(DisplayName = $"The {nameof(DocumentCatalog.Content)} should return a valid value")]
    public void DocumentCatalog_Content_ShouldReturnValidValue()
    {
        // Arrange
        const string expectedContent = "<</Type /Catalog /Pages 3 0 R>>";
        PdfIndirectIdentifier<PageTreeNode> pages = new PageTreeNode(options => options.Kids = Array.Empty<PdfIndirectIdentifier<PageObject>>().ToPdfArray())
            .ToPdfIndirect(3)
            .ToPdfIndirectIdentifier();

        DocumentCatalog documentCatalog = new(options => options.Pages = pages);

        // Act
        string actualContent = documentCatalog.Content;

        // Assert
        Assert.Equal(expectedContent, actualContent);
    }
}

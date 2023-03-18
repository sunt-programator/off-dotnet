using Off.Net.Pdf.Core.CommonDataStructures;
using Off.Net.Pdf.Core.ContentStreamAndResources;
using Off.Net.Pdf.Core.DocumentStructure;
using Off.Net.Pdf.Core.Primitives;
using Off.Net.Pdf.Core.Text.Fonts;
using Xunit;

namespace Off.Net.Pdf.Core.Tests.DocumentStructure;

public class PageObjectTests
{
    [Fact(DisplayName = $"Constructor with a null {nameof(PageObjectOptions.Parent)} indirect reference should throw an {nameof(ArgumentNullException)}")]
    public void PageObject_ConstructorWithNullParent_ShouldThrowException()
    {
        // Arrange
        PageObjectOptions documentCatalogOptions = new() { Parent = null! };

        // Act
        PageObject PageObjectFunction() => new(documentCatalogOptions);

        // Assert
        Assert.Throws<ArgumentNullException>(PageObjectFunction);
    }

    [Fact(DisplayName = $"Constructor with a null {nameof(PageObjectOptions.Resources)} indirect reference should throw an {nameof(ArgumentNullException)}")]
    public void PageObject_ConstructorWithNullResourceDictionary_ShouldThrowException()
    {
        // Arrange
        PageObjectOptions documentCatalogOptions = new() { Parent = new PdfNull().ToPdfIndirect(1).ToPdfIndirectIdentifier(), Resources = null! };

        // Act
        PageObject PageObjectFunction() => new(documentCatalogOptions);

        // Assert
        Assert.Throws<ArgumentNullException>(PageObjectFunction);
    }

    [Fact(DisplayName = $"Constructor with a null {nameof(PageObjectOptions.MediaBox)} indirect reference should throw an {nameof(ArgumentNullException)}")]
    public void PageObject_ConstructorWithNullMediaBox_ShouldThrowException()
    {
        // Arrange
        ResourceDictionary resourceDictionary = new(new ResourceDictionaryOptions());
        PageObjectOptions documentCatalogOptions = new() { Parent = new PdfNull().ToPdfIndirect(1).ToPdfIndirectIdentifier(), Resources = resourceDictionary, MediaBox = null! };

        // Act
        PageObject PageObjectFunction() => new(documentCatalogOptions);

        // Assert
        Assert.Throws<ArgumentNullException>(PageObjectFunction);
    }

    [Theory(DisplayName = $"The {nameof(PageObject.Content)} property should return a valid value")]
    [MemberData(nameof(PageObjectTestsDataGenerator.PageObject_Content_TestCases), MemberType = typeof(PageObjectTestsDataGenerator))]
    public void PageObject_Content_ShouldReturnValidValue(PageObjectOptions pageObjectOptions, string expectedContent)
    {
        // Arrange
        PageObject pageObject1 = new(pageObjectOptions); // Options as a class
        PageObject pageObject2 = new(options =>
        {
            options.Parent = pageObjectOptions.Parent;
            options.Resources = pageObjectOptions.Resources;
            options.MediaBox = pageObjectOptions.MediaBox;
        }); // Options as a delegate

        // Act
        string actualContent1 = pageObject1.Content;
        string actualContent2 = pageObject2.Content;

        // Assert
        Assert.Equal(expectedContent, actualContent1);
        Assert.Equal(expectedContent, actualContent2);
    }
}

internal static class PageObjectTestsDataGenerator
{
    public static IEnumerable<object[]> PageObject_Content_TestCases()
    {
        yield return new object[]
        {
            new PageObjectOptions
            {
                Parent = new PdfNull().ToPdfIndirect(4).ToPdfIndirectIdentifier(),
                Resources = new ResourceDictionary(options => options.Font = new Dictionary<PdfName, PdfIndirectIdentifier<Type1Font>>
                {
                    { "F3", StandardFonts.TimesRoman.ToPdfIndirect(7).ToPdfIndirectIdentifier() },
                    { "F5", StandardFonts.TimesRoman.ToPdfIndirect(9).ToPdfIndirectIdentifier() },
                    { "F7", StandardFonts.TimesRoman.ToPdfIndirect(11).ToPdfIndirectIdentifier() },
                }.ToPdfDictionary()),
                MediaBox = new Rectangle(0, 0, 612, 792),
            },
            "<</Type /Page /Parent 4 0 R /Resources <</Font <</F3 7 0 R /F5 9 0 R /F7 11 0 R>>>> /MediaBox [0 0 612 792]>>"
        };
    }
}

﻿using Off.Net.Pdf.Core.ContentStreamAndResources;
using Off.Net.Pdf.Core.Primitives;
using Off.Net.Pdf.Core.Text.Fonts;
using Xunit;

namespace Off.Net.Pdf.Core.Tests.ContentStreamAndResources;

public class ResourceDictionaryTests
{
    [Theory(DisplayName = $"The {nameof(ResourceDictionary)} with provided {nameof(ResourceDictionaryOptions.Font)} sub-dictionary should return a valid value")]
    [MemberData(nameof(ResourceDictionaryTestsDataGenerator.ResourceDictionary_Content_TestCases), MemberType = typeof(ResourceDictionaryTestsDataGenerator))]
    public void ResourceDictionary_Content_ShouldReturnValidValue(ResourceDictionaryOptions resourceDictionaryOptions, string expectedContent)
    {
        // Arrange
        ResourceDictionary resourceDictionary1 = new(resourceDictionaryOptions); // Options as a class
        ResourceDictionary resourceDictionary2 = new(options => options.Font = resourceDictionaryOptions.Font); // Options as a delegate

        // Act
        string actualContent1 = resourceDictionary1.Content;
        string actualContent2 = resourceDictionary2.Content;

        // Assert
        Assert.Equal(expectedContent, actualContent1);
        Assert.Equal(expectedContent, actualContent2);
    }
}

internal static class ResourceDictionaryTestsDataGenerator
{
    public static IEnumerable<object[]> ResourceDictionary_Content_TestCases()
    {
        yield return new object[]
        {
            new ResourceDictionaryOptions
            {
                Font = new Dictionary<PdfName, PdfIndirectIdentifier<Type1Font>>
                {
                    { "F5", StandardFonts.TimesRoman.ToPdfIndirect(6).ToPdfIndirectIdentifier() },
                    { "F6", StandardFonts.TimesRoman.ToPdfIndirect(8).ToPdfIndirectIdentifier() },
                    { "F7", StandardFonts.TimesRoman.ToPdfIndirect(10).ToPdfIndirectIdentifier() },
                    { "F8", StandardFonts.TimesRoman.ToPdfIndirect(12).ToPdfIndirectIdentifier() },
                }.ToPdfDictionary()
            },
            "<</Font <</F5 6 0 R /F6 8 0 R /F7 10 0 R /F8 12 0 R>>>>"
        };
    }
}
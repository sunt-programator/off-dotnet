// <copyright file="ResourceDictionaryTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.Core.ContentStreamAndResources;
using OffDotNet.Pdf.Core.Primitives;
using OffDotNet.Pdf.Core.Text.Fonts;
using Xunit;

namespace OffDotNet.Pdf.Core.Tests.ContentStreamAndResources;

public class ResourceDictionaryTests
{
    [Theory(DisplayName = $"The {nameof(ResourceDictionary)} with provided {nameof(ResourceDictionaryOptions.Font)} sub-dictionary should return a valid value")]
    [MemberData(nameof(ResourceDictionaryTestsDataGenerator.ResourceDictionary_Content_TestCases), MemberType = typeof(ResourceDictionaryTestsDataGenerator))]
    public void ResourceDictionary_Content_ShouldReturnValidValue(ResourceDictionaryOptions resourceDictionaryOptions, string expectedContent)
    {
        // Arrange
        ResourceDictionary resourceDictionary1 = new(resourceDictionaryOptions); // Options as a class
        ResourceDictionary resourceDictionary2 = new(options =>
        {
            options.Font = resourceDictionaryOptions.Font;
            options.ProcSet = resourceDictionaryOptions.ProcSet;
        }); // Options as a delegate

        // Act
        string actualContent1 = resourceDictionary1.Content;
        string actualContent2 = resourceDictionary2.Content;

        // Assert
        Assert.Equal(expectedContent, actualContent1);
        Assert.Equal(expectedContent, actualContent2);
    }
}

[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "TestData generator class can be in the same file")]
internal static class ResourceDictionaryTestsDataGenerator
{
    public static IEnumerable<object[]> ResourceDictionary_Content_TestCases()
    {
        yield return new object[]
        {
            new ResourceDictionaryOptions
            {
                Font = new Dictionary<PdfName, IPdfIndirectIdentifier<Type1Font>>
                {
                    { "F5", StandardFonts.TimesRoman.ToPdfIndirect(6).ToPdfIndirectIdentifier() },
                    { "F6", StandardFonts.TimesRoman.ToPdfIndirect(8).ToPdfIndirectIdentifier() },
                    { "F7", StandardFonts.TimesRoman.ToPdfIndirect(10).ToPdfIndirectIdentifier() },
                    { "F8", StandardFonts.TimesRoman.ToPdfIndirect(12).ToPdfIndirectIdentifier() },
                }.ToPdfDictionary(),
            },
            "<</Font <</F5 6 0 R /F6 8 0 R /F7 10 0 R /F8 12 0 R>> /ProcSet [/PDF /Text /ImageB /ImageC /ImageI]>>",
        };
        yield return new object[] { new ResourceDictionaryOptions { ProcSet = new[] { ResourceDictionaryOptions.ProcSetPdf }.ToPdfArray() }, "<</ProcSet [/PDF]>>" };
        yield return new object[]
        {
            new ResourceDictionaryOptions { ProcSet = new[] { ResourceDictionaryOptions.ProcSetPdf, ResourceDictionaryOptions.ProcSetText }.ToPdfArray() }, "<</ProcSet [/PDF /Text]>>",
        };
        yield return new object[]
        {
            new ResourceDictionaryOptions
            {
                ProcSet = new[] { ResourceDictionaryOptions.ProcSetImageB, ResourceDictionaryOptions.ProcSetImageC, ResourceDictionaryOptions.ProcSetImageI }.ToPdfArray(),
            },
            "<</ProcSet [/ImageB /ImageC /ImageI]>>",
        };
    }
}

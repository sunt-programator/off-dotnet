// <copyright file="ResourceDictionaryTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Tests.ContentStreamAndResources;

using OffDotNet.Pdf.Core.ContentStreamAndResources;
using OffDotNet.Pdf.Core.Primitives;
using OffDotNet.Pdf.Core.Text.Fonts;

public class ResourceDictionaryTests
{
    [Theory(DisplayName = $"The {nameof(ResourceDictionary)} with provided {nameof(ResourceDictionaryOptions.Font)} sub-dictionary should return a valid value")]
    [InlineData(null, "[/PDF]", "<</ProcSet [/PDF]>>")]
    [InlineData(null, "[/PDF /Text]", "<</ProcSet [/PDF /Text]>>")]
    [InlineData(null, "[/ImageB /ImageC /ImageI]", "<</ProcSet [/ImageB /ImageC /ImageI]>>")]
    [InlineData(
        "<</F5 6 0 R /F6 8 0 R /F7 10 0 R /F8 12 0 R>>",
        "[/PDF /Text /ImageB /ImageC /ImageI]",
        "<</Font <</F5 6 0 R /F6 8 0 R /F7 10 0 R /F8 12 0 R>> /ProcSet [/PDF /Text /ImageB /ImageC /ImageI]>>")]
    public void ResourceDictionary_Content_ShouldReturnValidValue(string? fontsDictionaryContent, string? procSetArrayContent, string expectedContent)
    {
        // Arrange
        var fontsDictionary = string.IsNullOrWhiteSpace(fontsDictionaryContent) ? null : Substitute.For<IPdfDictionary<IPdfIndirectIdentifier<IType1Font>>>();
        var procSetArray = string.IsNullOrWhiteSpace(procSetArrayContent) ? null : Substitute.For<IPdfArray<PdfName>>();

        fontsDictionary?.Content.Returns(fontsDictionaryContent);
        procSetArray?.Content.Returns(procSetArrayContent);

        ResourceDictionaryOptions resourceDictionaryOptions = new()
        {
            Font = fontsDictionary,
            ProcSet = procSetArray,
        };

        IResourceDictionary resourceDictionary1 = new ResourceDictionary(resourceDictionaryOptions); // Options as a class
        IResourceDictionary resourceDictionary2 = new ResourceDictionary(options =>
        {
            options.Font = resourceDictionaryOptions.Font;
            options.ProcSet = resourceDictionaryOptions.ProcSet;
        }); // Options as a delegate

        // Act
        var actualContent1 = resourceDictionary1.Content;
        var actualContent2 = resourceDictionary2.Content;

        // Assert
        Assert.Equal(expectedContent, actualContent1);
        Assert.Equal(expectedContent, actualContent2);
    }
}

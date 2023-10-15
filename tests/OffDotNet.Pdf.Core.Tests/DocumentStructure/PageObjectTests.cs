// <copyright file="PageObjectTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using NSubstitute;
using OffDotNet.Pdf.Core.CommonDataStructures;
using OffDotNet.Pdf.Core.ContentStreamAndResources;
using OffDotNet.Pdf.Core.DocumentStructure;
using OffDotNet.Pdf.Core.Primitives;
using Xunit;

namespace OffDotNet.Pdf.Core.Tests.DocumentStructure;

public class PageObjectTests
{
    [Fact(DisplayName = $"Constructor with a null {nameof(PageObjectOptions.Parent)} indirect reference should throw an {nameof(ArgumentNullException)}")]
    public void PageObject_ConstructorWithNullParent_ShouldThrowException()
    {
        // Arrange
        PageObjectOptions documentCatalogOptions = new() { Parent = null! };

        // Act
        IPageObject PageObjectFunction()
        {
            return new PageObject(documentCatalogOptions);
        }

        // Assert
        Assert.Throws<ArgumentNullException>(PageObjectFunction);
    }

    [Fact(DisplayName = $"Constructor with a null {nameof(PageObjectOptions.Resources)} indirect reference should throw an {nameof(ArgumentNullException)}")]
    public void PageObject_ConstructorWithNullResourceDictionary_ShouldThrowException()
    {
        // Arrange
        IPdfIndirectIdentifier<IPageTreeNode> parent = new PageTreeNode(options => options.Kids = Array.Empty<IPdfIndirectIdentifier<IPageObject>>().ToPdfArray())
            .ToPdfIndirect<IPageTreeNode>(1)
            .ToPdfIndirectIdentifier();

        PageObjectOptions documentCatalogOptions = new() { Parent = parent, Resources = null! };

        // Act
        IPageObject PageObjectFunction()
        {
            return new PageObject(documentCatalogOptions);
        }

        // Assert
        Assert.Throws<ArgumentNullException>(PageObjectFunction);
    }

    [Fact(DisplayName = $"Constructor with a null {nameof(PageObjectOptions.MediaBox)} indirect reference should throw an {nameof(ArgumentNullException)}")]
    public void PageObject_ConstructorWithNullMediaBox_ShouldThrowException()
    {
        // Arrange
        IPdfIndirectIdentifier<IPageTreeNode> parent = new PageTreeNode(options => options.Kids = Array.Empty<IPdfIndirectIdentifier<IPageObject>>().ToPdfArray())
            .ToPdfIndirect<IPageTreeNode>(1)
            .ToPdfIndirectIdentifier();

        IResourceDictionary resourceDictionary = new ResourceDictionary(new ResourceDictionaryOptions());
        PageObjectOptions documentCatalogOptions = new() { Parent = parent, Resources = resourceDictionary, MediaBox = null! };

        // Act
        IPageObject PageObjectFunction()
        {
            return new PageObject(documentCatalogOptions);
        }

        // Assert
        Assert.Throws<ArgumentNullException>(PageObjectFunction);
    }

    [Theory(DisplayName = $"The {nameof(PageObject.Content)} property should return a valid value")]
    [InlineData(
        "4 0 R",
        "<</Font <</F3 7 0 R /F5 9 0 R /F7 11 0 R>> /ProcSet [/PDF /Text /ImageB /ImageC /ImageI]>>",
        "[0 0 612 792]",
        null,
        "<</Type /Page /Parent 4 0 R /Resources <</Font <</F3 7 0 R /F5 9 0 R /F7 11 0 R>> /ProcSet [/PDF /Text /ImageB /ImageC /ImageI]>> /MediaBox [0 0 612 792]>>")]
    [InlineData(
        "4 0 R",
        "<</Font <</F3 7 0 R /F5 9 0 R /F7 11 0 R>> /ProcSet [/PDF /Text /ImageB /ImageC /ImageI]>>",
        "[0 0 612 792]",
        "4 2 R",
        "<</Type /Page /Parent 4 0 R /Resources <</Font <</F3 7 0 R /F5 9 0 R /F7 11 0 R>> /ProcSet [/PDF /Text /ImageB /ImageC /ImageI]>> /MediaBox [0 0 612 792] /Contents 4 2 R>>")]
    public void PageObject_Content_ShouldReturnValidValue(string parentRefContent, string resourcesDictionaryContent, string mediaBoxContent, string? pageContentsRefContent, string expectedContent)
    {
        // Arrange
        var parentRef = Substitute.For<IPdfIndirectIdentifier<IPageTreeNode>>();
        var resourceDictionary = Substitute.For<IResourceDictionary>();
        var mediaBox = Substitute.For<IRectangle>();
        var pageContentRef = string.IsNullOrWhiteSpace(pageContentsRefContent) ? null : Substitute.For<IPdfIndirectIdentifier<IPdfStream>>();

        parentRef.Content.Returns(parentRefContent);
        resourceDictionary.Content.Returns(resourcesDictionaryContent);
        mediaBox.Content.Returns(mediaBoxContent);
        pageContentRef?.Content.Returns(pageContentsRefContent);

        PageObjectOptions pageObjectOptions = new()
        {
            Parent = parentRef,
            Resources = resourceDictionary,
            MediaBox = mediaBox,
            Contents = pageContentRef == null ? null : new(pageContentRef),
        };

        IPageObject pageObject1 = new PageObject(pageObjectOptions); // Options as a class
        IPageObject pageObject2 = new PageObject(options =>
        {
            options.Parent = pageObjectOptions.Parent;
            options.Resources = pageObjectOptions.Resources;
            options.MediaBox = pageObjectOptions.MediaBox;
            options.Contents = pageObjectOptions.Contents;
        }); // Options as a delegate

        // Act
        string actualContent1 = pageObject1.Content;
        string actualContent2 = pageObject2.Content;

        // Assert
        Assert.Equal(expectedContent, actualContent1);
        Assert.Equal(expectedContent, actualContent2);
    }
}

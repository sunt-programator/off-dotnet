// <copyright file="PageTreeNodeTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Tests.DocumentStructure;

using OffDotNet.Pdf.Core.DocumentStructure;
using OffDotNet.Pdf.Core.Primitives;

public class PageTreeNodeTests
{
    [Fact(DisplayName = $"Constructor with a null {nameof(PageTreeNodeOptions.Kids)} indirect reference should throw an {nameof(ArgumentNullException)}")]
    public void PageTreeNode_ConstructorWithNullParent_ShouldThrowException()
    {
        // Arrange
        PageTreeNodeOptions pageTreeNodeOptions = new() { Kids = null! };

        // Act
        IPageTreeNode PageTreeNodeFunction()
        {
            return new PageTreeNode(pageTreeNodeOptions);
        }

        // Assert
        Assert.Throws<ArgumentNullException>(PageTreeNodeFunction);
    }

    [Theory(DisplayName = $"The {nameof(PageTreeNode.Content)} property should return a valid value")]
    [InlineData(null, new[] { "5 3 R", "3 1 R" }, "<</Type /Pages /Kids [5 3 R 3 1 R] /Count 2>>")]
    [InlineData(null, new[] { "5 3 R", "3 1 R", "12 6 R" }, "<</Type /Pages /Kids [5 3 R 3 1 R 12 6 R] /Count 3>>")]
    [InlineData("8 0 R", new[] { "5 3 R", "3 1 R", "12 6 R" }, "<</Type /Pages /Parent 8 0 R /Kids [5 3 R 3 1 R 12 6 R] /Count 3>>")]
    public void PageTreeNode_Content_ShouldReturnValidValue(string? parentRefContent, string[] kidsRefContent, string expectedContent)
    {
        // Arrange
        var parentRef = string.IsNullOrWhiteSpace(parentRefContent) ? null : Substitute.For<IPdfIndirectIdentifier<IPageTreeNode>>();
        parentRef?.Content.Returns(parentRefContent);

        var kids = new List<IPdfIndirectIdentifier<IPageObject>>();

        foreach (var kidRefContent in kidsRefContent)
        {
            var kidContent = Substitute.For<IPdfIndirectIdentifier<IPageObject>>();
            kidContent.Content.Returns(kidRefContent);

            kids.Add(kidContent);
        }

        PageTreeNodeOptions pageObjectOptions = new()
        {
            Parent = parentRef,
            Kids = kids.ToPdfArray(),
        };

        IPageTreeNode pageObject1 = new PageTreeNode(pageObjectOptions); // Options as a class
        IPageTreeNode pageObject2 = new PageTreeNode(options =>
        {
            options.Parent = pageObjectOptions.Parent;
            options.Kids = pageObjectOptions.Kids;
        }); // Options as a delegate

        // Act
        var actualContent1 = pageObject1.Content;
        var actualContent2 = pageObject2.Content;

        // Assert
        Assert.Equal(expectedContent, actualContent1);
        Assert.Equal(expectedContent, actualContent2);
    }
}

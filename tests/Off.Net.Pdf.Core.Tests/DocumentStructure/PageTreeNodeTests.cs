﻿using Off.Net.Pdf.Core.CommonDataStructures;
using Off.Net.Pdf.Core.ContentStreamAndResources;
using Off.Net.Pdf.Core.DocumentStructure;
using Off.Net.Pdf.Core.Primitives;
using Off.Net.Pdf.Core.Text.Fonts;
using Xunit;

namespace Off.Net.Pdf.Core.Tests.DocumentStructure;

public class PageTreeNodeTests
{
    [Fact(DisplayName = $"Constructor with a null {nameof(PageTreeNodeOptions.Kids)} indirect reference should throw an {nameof(ArgumentNullException)}")]
    public void PageTreeNode_ConstructorWithNullParent_ShouldThrowException()
    {
        // Arrange
        PageTreeNodeOptions pageTreeNodeOptions = new() { Kids = null! };

        // Act
        PageTreeNode PageTreeNodeFunction() => new(pageTreeNodeOptions);

        // Assert
        Assert.Throws<ArgumentNullException>(PageTreeNodeFunction);
    }

    [Theory(DisplayName = $"The {nameof(PageTreeNode.Content)} property should return a valid value")]
    [MemberData(nameof(PageTreeNodeTestsDataGenerator.PageTreeNode_Content_TestCases), MemberType = typeof(PageTreeNodeTestsDataGenerator))]
    public void PageTreeNode_Content_ShouldReturnValidValue(PageTreeNodeOptions pageObjectOptions, string expectedContent)
    {
        // Arrange
        PageTreeNode pageObject1 = new(pageObjectOptions); // Options as a class
        PageTreeNode pageObject2 = new(options =>
        {
            options.Parent = pageObjectOptions.Parent;
            options.Kids = pageObjectOptions.Kids;
        }); // Options as a delegate

        // Act
        string actualContent1 = pageObject1.Content;
        string actualContent2 = pageObject2.Content;

        // Assert
        Assert.Equal(expectedContent, actualContent1);
        Assert.Equal(expectedContent, actualContent2);
    }
}

internal static class PageTreeNodeTestsDataGenerator
{
    public static IEnumerable<object[]> PageTreeNode_Content_TestCases()
    {
        PageObjectOptions pageObjectOptions = new()
        {
            Parent = new PageTreeNode(options => options.Kids = Array.Empty<PdfIndirectIdentifier<PageObject>>().ToPdfArray()).ToPdfIndirect(3, 6).ToPdfIndirectIdentifier(),
            Resources = new ResourceDictionary(options => options.Font = new Dictionary<PdfName, PdfIndirectIdentifier<Type1Font>>
            {
                { "F3", StandardFonts.TimesRoman.ToPdfIndirect(7).ToPdfIndirectIdentifier() },
                { "F5", StandardFonts.TimesRoman.ToPdfIndirect(9).ToPdfIndirectIdentifier() },
                { "F7", StandardFonts.TimesRoman.ToPdfIndirect(11).ToPdfIndirectIdentifier() },
            }.ToPdfDictionary()),
            MediaBox = new Rectangle(0, 0, 612, 792),
        };

        yield return new object[]
        {
            new PageTreeNodeOptions { Kids = new[] { new PageObject(pageObjectOptions).ToPdfIndirect(5, 3).ToPdfIndirectIdentifier() }.ToPdfArray() }, "<</Type /Pages /Kids [5 3 R] /Count 1>>"
        };
        yield return new object[]
        {
            new PageTreeNodeOptions
            {
                Kids = new[]
                {
                    new PageObject(pageObjectOptions).ToPdfIndirect(5, 3).ToPdfIndirectIdentifier(), new PageObject(pageObjectOptions).ToPdfIndirect(3, 1).ToPdfIndirectIdentifier()
                }.ToPdfArray()
            },
            "<</Type /Pages /Kids [5 3 R 3 1 R] /Count 2>>"
        };
        yield return new object[]
        {
            new PageTreeNodeOptions
            {
                Kids = new[]
                {
                    new PageObject(pageObjectOptions).ToPdfIndirect(5, 3).ToPdfIndirectIdentifier(),
                    new PageObject(pageObjectOptions).ToPdfIndirect(3, 1).ToPdfIndirectIdentifier(),
                    new PageObject(pageObjectOptions).ToPdfIndirect(12, 6).ToPdfIndirectIdentifier()
                }.ToPdfArray()
            },
            "<</Type /Pages /Kids [5 3 R 3 1 R 12 6 R] /Count 3>>"
        };
        yield return new object[]
        {
            new PageTreeNodeOptions
            {
                Kids = new[]
                {
                    new PageObject(pageObjectOptions).ToPdfIndirect(5, 3).ToPdfIndirectIdentifier(),
                    new PageObject(pageObjectOptions).ToPdfIndirect(3, 1).ToPdfIndirectIdentifier(),
                    new PageObject(pageObjectOptions).ToPdfIndirect(12, 6).ToPdfIndirectIdentifier()
                }.ToPdfArray(),
                Parent = new PageTreeNode(options => options.Kids = new[] { new PageObject(pageObjectOptions).ToPdfIndirect(5, 3).ToPdfIndirectIdentifier() }.ToPdfArray())
                    .ToPdfIndirect(8)
                    .ToPdfIndirectIdentifier()
            },
            "<</Type /Pages /Parent 8 0 R /Kids [5 3 R 3 1 R 12 6 R] /Count 3>>"
        };
    }
}
// <copyright file="DocumentCatalogTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Tests.DocumentStructure;

using OffDotNet.Pdf.Core.DocumentStructure;
using OffDotNet.Pdf.Core.Primitives;

public class DocumentCatalogTests
{
    [Fact(DisplayName = $"Constructor with an empty {nameof(DocumentCatalogOptions.Pages)} dictionary should throw an {nameof(ArgumentNullException)}")]
    public void DocumentCatalog_ConstructorWithEmptyPagesDictionary_ShouldThrowException()
    {
        // Arrange
        DocumentCatalogOptions documentCatalogOptions = new() { Pages = null! };

        // Act
        IDocumentCatalog DocumentCatalogFunction()
        {
            return new DocumentCatalog(documentCatalogOptions);
        }

        // Assert
        Assert.Throws<ArgumentNullException>(DocumentCatalogFunction);
    }

    [Fact(DisplayName = $"The {nameof(DocumentCatalog.Content)} should return a valid value")]
    public void DocumentCatalog_Content_ShouldReturnValidValue()
    {
        // Arrange
        const string ExpectedContent = "<</Type /Catalog /Pages 3 0 R>>";
        IPdfIndirectIdentifier<IPageTreeNode> pages = new PageTreeNode(options => options.Kids = Array.Empty<IPdfIndirectIdentifier<IPageObject>>().ToPdfArray())
            .ToPdfIndirect<IPageTreeNode>(3)
            .ToPdfIndirectIdentifier();

        IDocumentCatalog documentCatalog = new DocumentCatalog(options => options.Pages = pages);

        // Act
        var actualContent = documentCatalog.Content;

        // Assert
        Assert.Equal(ExpectedContent, actualContent);
    }
}

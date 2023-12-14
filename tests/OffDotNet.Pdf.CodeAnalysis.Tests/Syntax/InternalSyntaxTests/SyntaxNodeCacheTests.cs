// <copyright file="SyntaxNodeCacheTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Collections;
using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using SyntaxToken = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxToken;

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxTests;

public class SyntaxNodeCacheTests
{
    [Fact(DisplayName = "The TryGetNode() method must return null and negative hash if the node cannot be cached.")]
    public void TryGetNodeMethod_MoreThan3Children_MustReturnNullAndNegativeHash()
    {
        // Arrange
        const SyntaxKind syntaxKind = SyntaxKind.TrueKeyword;
        SyntaxToken trueKeywordToken = SyntaxFactory.Token(syntaxKind);

        ArrayElement<GreenNode>[] list = new ArrayElement<GreenNode>[4];
        list[0].Value = trueKeywordToken;
        list[1].Value = trueKeywordToken;
        list[2].Value = trueKeywordToken;
        list[3].Value = trueKeywordToken;

        GreenNode greenNodeList = SyntaxFactory.List(list);

        // Act
        GreenNode? node = SyntaxNodeCache.TryGetNode(syntaxKind, greenNodeList, out int hash);

        // Assert
        Assert.Null(node);
        Assert.Equal(-1, hash);
    }

    [Fact(DisplayName = "The TryGetNode() method must return null and calculated hash if the node can be cached but not found in cache list.")]
    public void TryGetNodeMethod_MustReturnNullAndCalculatedHash()
    {
        // Arrange
        const SyntaxKind syntaxKind = SyntaxKind.TrueKeyword;
        SyntaxToken trueKeywordToken = SyntaxFactory.Token(syntaxKind);

        // Act
        GreenNode? node = SyntaxNodeCache.TryGetNode(syntaxKind, trueKeywordToken, out int hash);

        // Assert
        Assert.Null(node);
        Assert.True(hash > 0);
    }

    [Fact(DisplayName = "The AddNode() method must add node to cache list.")]
    public void AddNodeNodeMethod_MustAddToCacheList()
    {
        // Arrange
        const SyntaxKind syntaxKind = SyntaxKind.TrueLiteralExpression;
        SyntaxToken trueKeywordToken = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        LiteralExpressionSyntax literalExpression = new(SyntaxKind.TrueLiteralExpression, trueKeywordToken);

        // Act
        GreenNode? node = SyntaxNodeCache.TryGetNode(syntaxKind, trueKeywordToken, out int hash);
        Assert.Null(node);
        Assert.True(hash > 0);

        SyntaxNodeCache.AddNode(literalExpression, hash);
        node = SyntaxNodeCache.TryGetNode(syntaxKind, trueKeywordToken, out int actualHash);

        // Assert
        Assert.Equal(literalExpression, node);
        Assert.Equal(hash, actualHash);
    }

    [Fact(DisplayName = "The TryGetNode() method with two nodes must be cached.")]
    public void TryGetNodeMethod_TwoNodes()
    {
        // Arrange
        const SyntaxKind syntaxKind = SyntaxKind.List;
        SyntaxToken trueKeywordToken = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        SyntaxToken falseKeywordToken = SyntaxFactory.Token(SyntaxKind.FalseKeyword);
        LiteralExpressionSyntax literalExpression1 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, trueKeywordToken);
        LiteralExpressionSyntax literalExpression2 = SyntaxFactory.LiteralExpression(SyntaxKind.FalseLiteralExpression, falseKeywordToken);
        SyntaxList.WithTwoChildren list = new(literalExpression1, literalExpression2);

        SyntaxNodeCache.TryGetNode(SyntaxKind.TrueLiteralExpression, trueKeywordToken, out int hash);
        SyntaxNodeCache.AddNode(literalExpression1, hash);

        SyntaxNodeCache.TryGetNode(SyntaxKind.FalseLiteralExpression, falseKeywordToken, out hash);
        SyntaxNodeCache.AddNode(literalExpression2, hash);

        // Act
        GreenNode? node = SyntaxNodeCache.TryGetNode(syntaxKind, literalExpression1, literalExpression2, out hash);
        Assert.Null(node);
        Assert.True(hash > 0);

        SyntaxNodeCache.AddNode(list, hash);
        node = SyntaxNodeCache.TryGetNode(syntaxKind, literalExpression1, literalExpression2, out int actualHash);

        // Assert
        Assert.Equal(list, node);
        Assert.Equal(hash, actualHash);
    }

    [Fact(DisplayName = "The TryGetNode() method with three nodes must be cached.")]
    public void TryGetNodeMethod_ThreeNodes()
    {
        // Arrange
        const SyntaxKind syntaxKind = SyntaxKind.List;
        SyntaxToken trueKeywordToken = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        SyntaxToken falseKeywordToken = SyntaxFactory.Token(SyntaxKind.FalseKeyword);
        SyntaxToken nullKeywordToken = SyntaxFactory.Token(SyntaxKind.NullKeyword);
        LiteralExpressionSyntax literalExpression1 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, trueKeywordToken);
        LiteralExpressionSyntax literalExpression2 = SyntaxFactory.LiteralExpression(SyntaxKind.FalseLiteralExpression, falseKeywordToken);
        LiteralExpressionSyntax literalExpression3 = SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression, nullKeywordToken);
        SyntaxList.WithThreeChildren list = new(literalExpression1, literalExpression2, literalExpression3);

        SyntaxNodeCache.TryGetNode(SyntaxKind.TrueLiteralExpression, trueKeywordToken, out int hash);
        SyntaxNodeCache.AddNode(literalExpression1, hash);

        SyntaxNodeCache.TryGetNode(SyntaxKind.FalseLiteralExpression, falseKeywordToken, out hash);
        SyntaxNodeCache.AddNode(literalExpression2, hash);

        SyntaxNodeCache.TryGetNode(SyntaxKind.NullLiteralExpression, nullKeywordToken, out hash);
        SyntaxNodeCache.AddNode(literalExpression3, hash);

        // Act
        GreenNode? node = SyntaxNodeCache.TryGetNode(syntaxKind, literalExpression1, literalExpression2, literalExpression3, out hash);
        Assert.Null(node);
        Assert.True(hash > 0);

        SyntaxNodeCache.AddNode(list, hash);
        node = SyntaxNodeCache.TryGetNode(syntaxKind, literalExpression1, literalExpression2, literalExpression3, out int actualHash);

        // Assert
        Assert.Equal(list, node);
        Assert.Equal(hash, actualHash);
    }
}

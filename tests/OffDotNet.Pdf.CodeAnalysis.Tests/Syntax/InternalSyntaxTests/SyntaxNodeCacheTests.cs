// <copyright file="SyntaxNodeCacheTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxTests;

using OffDotNet.Pdf.CodeAnalysis.Collections;
using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntaxNodes;
using SyntaxFactory = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxFactory;

public class SyntaxNodeCacheTests
{
    [Fact(DisplayName = "The TryGetNode() method must return null and negative hash if the node cannot be cached.")]
    public void TryGetNodeMethod_MoreThan3Children_MustReturnNullAndNegativeHash()
    {
        // Arrange
        const SyntaxKind SyntaxKind = SyntaxKind.TrueKeyword;
        var trueKeywordToken = SyntaxFactory.Token(SyntaxKind);

        ArrayElement<GreenNode>[] list = new ArrayElement<GreenNode>[4];
        list[0]._value = trueKeywordToken;
        list[1]._value = trueKeywordToken;
        list[2]._value = trueKeywordToken;
        list[3]._value = trueKeywordToken;

        var greenNodeList = SyntaxFactory.List(list);

        // Act
        var node = SyntaxNodeCache.TryGetNode(SyntaxKind, greenNodeList, out var hash);

        // Assert
        Assert.Null(node);
        Assert.Equal(-1, hash);
    }

    [Fact(DisplayName = "The TryGetNode() method must return null and calculated hash if the node can be cached but not found in cache list.")]
    public void TryGetNodeMethod_MustReturnNullAndCalculatedHash()
    {
        // Arrange
        const SyntaxKind SyntaxKind = SyntaxKind.TrueKeyword;
        var trueKeywordToken = SyntaxFactory.Token(SyntaxKind);

        // Act
        var node = SyntaxNodeCache.TryGetNode(SyntaxKind, trueKeywordToken, out var hash);

        // Assert
        Assert.Null(node);
        Assert.True(hash > 0);
    }

    [Fact(DisplayName = "The AddNode() method must add node to cache list.")]
    public void AddNodeNodeMethod_MustAddToCacheList()
    {
        // Arrange
        const SyntaxKind SyntaxKind = SyntaxKind.TrueLiteralExpression;
        var trueKeywordToken = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        LiteralExpressionSyntax literalExpression = new(SyntaxKind.TrueLiteralExpression, trueKeywordToken);

        // Act
        var node = SyntaxNodeCache.TryGetNode(SyntaxKind, trueKeywordToken, out var hash);
        Assert.Null(node);
        Assert.True(hash > 0);

        SyntaxNodeCache.AddNode(literalExpression, hash);
        node = SyntaxNodeCache.TryGetNode(SyntaxKind, trueKeywordToken, out var actualHash);

        // Assert
        Assert.Same(literalExpression, node);
        Assert.Equal(hash, actualHash);
    }

    [Fact(DisplayName = "The TryGetNode() method with two nodes must be cached.")]
    public void TryGetNodeMethod_TwoNodes()
    {
        // Arrange
        const SyntaxKind SyntaxKind = SyntaxKind.List;
        var trueKeywordToken = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        var falseKeywordToken = SyntaxFactory.Token(SyntaxKind.FalseKeyword);
        var literalExpression1 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, trueKeywordToken);
        var literalExpression2 = SyntaxFactory.LiteralExpression(SyntaxKind.FalseLiteralExpression, falseKeywordToken);
        SyntaxList.WithTwoChildren list = new(literalExpression1, literalExpression2);

        SyntaxNodeCache.TryGetNode(SyntaxKind.TrueLiteralExpression, trueKeywordToken, out var hash);
        SyntaxNodeCache.AddNode(literalExpression1, hash);

        SyntaxNodeCache.TryGetNode(SyntaxKind.FalseLiteralExpression, falseKeywordToken, out hash);
        SyntaxNodeCache.AddNode(literalExpression2, hash);

        // Act
        var node = SyntaxNodeCache.TryGetNode(SyntaxKind, literalExpression1, literalExpression2, out hash);
        Assert.Null(node);
        Assert.True(hash > 0);

        SyntaxNodeCache.AddNode(list, hash);
        node = SyntaxNodeCache.TryGetNode(SyntaxKind, literalExpression1, literalExpression2, out var actualHash);

        // Assert
        Assert.Same(list, node);
        Assert.Equal(hash, actualHash);
    }

    [Fact(DisplayName = "The TryGetNode() method with three nodes must be cached.")]
    public void TryGetNodeMethod_ThreeNodes()
    {
        // Arrange
        const SyntaxKind SyntaxKind = SyntaxKind.List;
        var trueKeywordToken = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        var falseKeywordToken = SyntaxFactory.Token(SyntaxKind.FalseKeyword);
        var nullKeywordToken = SyntaxFactory.Token(SyntaxKind.NullKeyword);
        var literalExpression1 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, trueKeywordToken);
        var literalExpression2 = SyntaxFactory.LiteralExpression(SyntaxKind.FalseLiteralExpression, falseKeywordToken);
        var literalExpression3 = SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression, nullKeywordToken);
        SyntaxList.WithThreeChildren list = new(literalExpression1, literalExpression2, literalExpression3);

        SyntaxNodeCache.TryGetNode(SyntaxKind.TrueLiteralExpression, trueKeywordToken, out var hash);
        SyntaxNodeCache.AddNode(literalExpression1, hash);

        SyntaxNodeCache.TryGetNode(SyntaxKind.FalseLiteralExpression, falseKeywordToken, out hash);
        SyntaxNodeCache.AddNode(literalExpression2, hash);

        SyntaxNodeCache.TryGetNode(SyntaxKind.NullLiteralExpression, nullKeywordToken, out hash);
        SyntaxNodeCache.AddNode(literalExpression3, hash);

        // Act
        var node = SyntaxNodeCache.TryGetNode(SyntaxKind, literalExpression1, literalExpression2, literalExpression3, out hash);
        Assert.Null(node);
        Assert.True(hash > 0);

        SyntaxNodeCache.AddNode(list, hash);
        node = SyntaxNodeCache.TryGetNode(SyntaxKind, literalExpression1, literalExpression2, literalExpression3, out var actualHash);

        // Assert
        Assert.Same(list, node);
        Assert.Equal(hash, actualHash);
    }
}

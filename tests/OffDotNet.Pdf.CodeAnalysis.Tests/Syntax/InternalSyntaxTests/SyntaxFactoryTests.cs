// <copyright file="SyntaxFactoryTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using SyntaxToken = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxToken;

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxTests;

public class SyntaxFactoryTests
{
    [Theory(DisplayName = "The LiteralExpression() method must create the object if the expression node kind is valid.")]
    [InlineData(SyntaxKind.TrueLiteralExpression)]
    [InlineData(SyntaxKind.FalseLiteralExpression)]
    [InlineData(SyntaxKind.NumericLiteralExpression)]
    [InlineData(SyntaxKind.StringLiteralExpression)]
    [InlineData(SyntaxKind.NullLiteralExpression)]
    [InlineData(SyntaxKind.IndirectReferenceLiteralExpression)]
    public void LiteralExpression_ValidExpressionKind_MustCreateObject(SyntaxKind kind)
    {
        // Arrange
        SyntaxToken keyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        LiteralExpressionSyntax literalExpression = SyntaxFactory.LiteralExpression(kind, keyword);

        // Act
        SyntaxKind actualKind = literalExpression.Kind;

        // Assert
        Assert.Equal(kind, actualKind);
    }

    [Fact(DisplayName = "The LiteralExpression() method must throw ArgumentException if the expression node kind is invalid.")]
    public void LiteralExpression_InvalidExpressionKind_MustThrowArgumentException()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.None;
        SyntaxToken keyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword);

        // Act
        LiteralExpressionSyntax LiteralExpressionFunc()
        {
            return SyntaxFactory.LiteralExpression(kind, keyword);
        }

        // Assert
        Assert.Throws<ArgumentException>(LiteralExpressionFunc);
    }

    [Theory(DisplayName = "The LiteralExpression() method must create the object if the token kind is valid.")]
    [InlineData(SyntaxKind.TrueKeyword)]
    [InlineData(SyntaxKind.FalseKeyword)]
    [InlineData(SyntaxKind.NumericLiteralToken)]
    [InlineData(SyntaxKind.StringLiteralToken)]
    [InlineData(SyntaxKind.NullKeyword)]
    [InlineData(SyntaxKind.IndirectReferenceKeyword)]
    public void LiteralExpression_ValidTokenKind_MustCreateObject(SyntaxKind kind)
    {
        // Arrange
        SyntaxToken keyword = SyntaxFactory.Token(kind);
        LiteralExpressionSyntax literalExpression = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, keyword);

        // Act
        SyntaxKind actualKind = literalExpression.Token.Kind;

        // Assert
        Assert.Equal(kind, actualKind);
    }

    [Fact(DisplayName = "The LiteralExpression() method must throw ArgumentException if the token kind is invalid.")]
    public void LiteralExpression_InvalidTokenKind_MustThrowArgumentException()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.None;
        SyntaxToken keyword = SyntaxFactory.Token(kind);

        // Act
        LiteralExpressionSyntax LiteralExpressionFunc()
        {
            return SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, keyword);
        }

        // Assert
        Assert.Throws<ArgumentException>(LiteralExpressionFunc);
    }
}

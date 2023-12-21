// <copyright file="SyntaxListTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxTests;

public class SyntaxListTests
{
    [Fact(DisplayName = $"The {nameof(SyntaxList.Kind)} property must be equal to {nameof(SyntaxKind.List)} kind.")]
    public void KindProperty_FromGreenNode_MustBeList()
    {
        // Arrange
        LiteralExpressionSyntax literalExpression = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        GreenNode syntaxList = SyntaxFactory.List(literalExpression, literalExpression);

        // Act
        SyntaxKind actualKind = syntaxList.Kind;

        // Assert
        Assert.Equal(SyntaxKind.List, actualKind);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxList)} with one child must return the node itself.")]
    public void OneChild_MustReturnTheNode()
    {
        // Arrange
        LiteralExpressionSyntax literalExpression = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));

        // Act
        GreenNode syntaxList = SyntaxFactory.List(literalExpression);

        // Assert
        Assert.Equal(literalExpression, syntaxList);
    }

    [Fact(DisplayName = "The ToString() method must not include the trivia.")]
    public void ToStringMethod_MustNotIncludeTrivia()
    {
        // Arrange
        GreenNode trivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, "   ");
        LiteralExpressionSyntax literalExpression = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword, trivia, trivia));
        GreenNode syntaxList = SyntaxFactory.List(literalExpression);

        // Act
        string actualString = syntaxList.ToString();

        // Assert
        Assert.Equal("true", actualString);
    }

    [Fact(DisplayName = "The ToFullString() method must include the trivia.")]
    public void ToFullStringMethod_MustIncludeTrivia()
    {
        // Arrange
        GreenNode trivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, "   ");
        LiteralExpressionSyntax literalExpression = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword, trivia, trivia));
        GreenNode syntaxList = SyntaxFactory.List(literalExpression);

        // Act
        string actualString = syntaxList.ToFullString();

        // Assert
        Assert.Equal("   true   ", actualString);
    }
}

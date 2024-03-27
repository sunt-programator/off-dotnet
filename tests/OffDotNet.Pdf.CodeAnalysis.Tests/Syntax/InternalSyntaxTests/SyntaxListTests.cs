// <copyright file="SyntaxListTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxTests;

using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

public class SyntaxListTests
{
    [Fact(DisplayName = $"The {nameof(SyntaxList.Kind)} property must be equal to {nameof(SyntaxKind.List)} kind.")]
    public void KindProperty_FromGreenNode_MustBeList()
    {
        // Arrange
        var literalExpression = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        var syntaxList = SyntaxFactory.List(literalExpression, literalExpression);

        // Act
        var actualKind = syntaxList.Kind;

        // Assert
        Assert.Equal(SyntaxKind.List, actualKind);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxList)} with one child must return the node itself.")]
    public void OneChild_MustReturnTheNode()
    {
        // Arrange
        var literalExpression = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));

        // Act
        var syntaxList = SyntaxFactory.List(literalExpression);

        // Assert
        Assert.Equal(literalExpression, syntaxList);
    }

    [Fact(DisplayName = "The ToString() method must not include the trivia.")]
    public void ToStringMethod_MustNotIncludeTrivia()
    {
        // Arrange
        GreenNode trivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, "   ");
        var literalExpression = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword, trivia, trivia));
        var syntaxList = SyntaxFactory.List(literalExpression);

        // Act
        var actualString = syntaxList.ToString();

        // Assert
        Assert.Equal("true", actualString);
    }

    [Fact(DisplayName = "The ToFullString() method must include the trivia.")]
    public void ToFullStringMethod_MustIncludeTrivia()
    {
        // Arrange
        GreenNode trivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, "   ");
        var literalExpression = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword, trivia, trivia));
        var syntaxList = SyntaxFactory.List(literalExpression);

        // Act
        var actualString = syntaxList.ToFullString();

        // Assert
        Assert.Equal("   true   ", actualString);
    }
}

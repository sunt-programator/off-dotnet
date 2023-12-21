// <copyright file="LiteralExpressionSyntaxTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using SyntaxToken = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxToken;
using SyntaxTrivia = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxTrivia;

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxTests;

public class LiteralExpressionSyntaxTests
{
    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.Token)} property must be assigned from constructor.")]
    public void KeywordProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        SyntaxToken keyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        LiteralExpressionSyntax literalExpression = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, keyword);

        // Act
        SyntaxToken actualToken = literalExpression.Token;

        // Assert
        Assert.Equal(keyword, actualToken);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.SlotCount)} property must be equal to 1.")]
    public void SlotCountProperty_MustBeEqualTo1()
    {
        // Arrange
        SyntaxToken keyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        LiteralExpressionSyntax literalExpression = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, keyword);

        // Act
        int actualSlotCount = literalExpression.SlotCount;

        // Assert
        Assert.Equal(1, actualSlotCount);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 0 must return {nameof(LiteralExpressionSyntax.Token)} property.")]
    public void GetSlotMethod_Index0_MustReturnKeywordProperty()
    {
        // Arrange
        SyntaxToken keyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        LiteralExpressionSyntax literalExpression = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, keyword);

        // Act
        GreenNode? actualSlot = literalExpression.GetSlot(0);

        // Assert
        Assert.Equal(keyword, actualSlot);
    }

    [Fact(DisplayName = "The GetSlot() method with index 1 must return null.")]
    public void GetSlotMethod_Index1_MustReturnNull()
    {
        // Arrange
        SyntaxToken keyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        LiteralExpressionSyntax literalExpression = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, keyword);

        // Act
        GreenNode? actualSlot = literalExpression.GetSlot(1);

        // Assert
        Assert.Null(actualSlot);
    }

    [Fact(DisplayName = "The ToString() method must not include the trivia.")]
    public void ToStringMethod_MustNotIncludeTrivia()
    {
        // Arrange
        const string expectedString = "true";
        SyntaxTrivia trivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " ");
        SyntaxToken keyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword, trivia, trivia);
        LiteralExpressionSyntax literalExpression = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, keyword);

        // Act
        string actualString = literalExpression.ToString();

        // Assert
        Assert.Equal(expectedString, actualString);
    }

    [Fact(DisplayName = "The ToFullString() method must include the trivia.")]
    public void ToFullStringMethod_MustIncludeTrivia()
    {
        // Arrange
        const string expectedString = " true ";
        SyntaxTrivia trivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " ");
        SyntaxToken keyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword, trivia, trivia);
        LiteralExpressionSyntax literalExpression = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, keyword);

        // Act
        string actualString = literalExpression.ToFullString();

        // Assert
        Assert.Equal(expectedString, actualString);
    }
}

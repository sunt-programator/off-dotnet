// <copyright file="SyntaxTokenTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using SyntaxToken = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxToken;
using SyntaxTrivia = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxTrivia;

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxTests;

public class SyntaxTokenTests
{
    [Fact(DisplayName = $"The Create() method must set the {nameof(SyntaxToken.Kind)} property.")]
    public void CreateMethod_MustSetKindProperty()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.NumericLiteralToken;
        SyntaxToken token = SyntaxFactory.Token(kind);

        // Act
        SyntaxKind actualSyntaxKind = token.Kind;

        // Assert
        Assert.Equal(kind, actualSyntaxKind);
    }

    [Theory(DisplayName = $"The Create() method must set the {nameof(SyntaxToken.Text)} property.")]
    [InlineData(SyntaxKind.None, "")]
    [InlineData(SyntaxKind.LeftParenthesisToken, "(")]
    [InlineData(SyntaxKind.RightParenthesisToken, ")")]
    [InlineData(SyntaxKind.LessThanToken, "<")]
    [InlineData(SyntaxKind.GreaterThanToken, ">")]
    [InlineData(SyntaxKind.LeftSquareBracketToken, "[")]
    [InlineData(SyntaxKind.RightSquareBracketToken, "]")]
    [InlineData(SyntaxKind.LeftCurlyBracketToken, "{")]
    [InlineData(SyntaxKind.RightCurlyBracketToken, "}")]
    [InlineData(SyntaxKind.SolidusToken, "/")]
    [InlineData(SyntaxKind.PercentSignToken, "%")]
    [InlineData(SyntaxKind.LessThanLessThanToken, "<<")]
    [InlineData(SyntaxKind.GreaterThanGreaterThanToken, ">>")]
    [InlineData(SyntaxKind.PlusToken, "+")]
    [InlineData(SyntaxKind.MinusToken, "-")]
    [InlineData(SyntaxKind.TrueKeyword, "true")]
    [InlineData(SyntaxKind.FalseKeyword, "false")]
    [InlineData(SyntaxKind.NullKeyword, "null")]
    [InlineData(SyntaxKind.StartObjectKeyword, "obj")]
    [InlineData(SyntaxKind.EndObjectKeyword, "endobj")]
    [InlineData(SyntaxKind.IndirectReferenceKeyword, "R")]
    [InlineData(SyntaxKind.StartStreamKeyword, "stream")]
    [InlineData(SyntaxKind.EndStreamKeyword, "endstream")]
    [InlineData(SyntaxKind.XRefKeyword, "xref")]
    [InlineData(SyntaxKind.TrailerKeyword, "trailer")]
    [InlineData(SyntaxKind.StartXrefKeyword, "startxref")]
    public void CreateMethod_MustSetTextProperty(SyntaxKind kind, string text)
    {
        // Arrange
        SyntaxToken token = SyntaxFactory.Token(kind);

        // Act
        string actualText = token.Text;

        // Assert
        Assert.Equal(text, actualText);
    }

    [Theory(DisplayName = $"Two SyntaxTokens with the same {nameof(SyntaxToken.Kind)} must have the same reference on {nameof(SyntaxToken.Value)} property.")]
    [InlineData(SyntaxKind.TrueKeyword, true)]
    [InlineData(SyntaxKind.FalseKeyword, false)]
    [InlineData(SyntaxKind.NullKeyword, null)]
    [InlineData(SyntaxKind.None, null)]
    public void TwoSyntaxTokens_Keywords_MustHaveSameReferenceValue(SyntaxKind kind, object? value)
    {
        // Arrange
        SyntaxToken token1 = SyntaxFactory.Token(kind);
        SyntaxToken token2 = SyntaxFactory.Token(kind);

        // Act
        object? actualValue1 = token1.Value;
        object? actualValue2 = token2.Value;

        // Assert
        Assert.Equal(value, actualValue1);
        Assert.Equal(value, actualValue2);
        Assert.True(ReferenceEquals(actualValue1, actualValue2));
    }

    [Fact(DisplayName = $"The ToString() method must return the {nameof(SyntaxToken.Text)} property value.")]
    public void ToStringMethod_MustReturnTheTextPropertyValue()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.TrueKeyword;
        const string expectedToStringValue = "true";

        SyntaxToken token = SyntaxFactory.Token(kind);

        // Act
        string actualToStringValue = token.ToString();

        // Assert
        Assert.Equal(expectedToStringValue, actualToStringValue);
    }

    [Fact(DisplayName = $"The Create() method must set the {nameof(SyntaxToken.FullWidth)} property with the computed {nameof(SyntaxToken.Text)} property length.")]
    public void CreateMethod_MustSetFullWidthPropertyWithTextPropertyLength()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.TrueKeyword;
        const int expectedFullWidth = 4;
        SyntaxToken token = SyntaxFactory.Token(kind);

        // Act
        int actualFullWidth = token.FullWidth;

        // Assert
        Assert.Equal(expectedFullWidth, actualFullWidth);
    }

    [Fact(DisplayName = $"The Create() method must set the {nameof(SyntaxToken.Width)} property the same value as the {nameof(SyntaxToken.FullWidth)} property.")]
    public void CreateMethod_NoTrivia_MustSetToFullWidthTheWithProperty()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.TrueKeyword;
        const int expectedWidth = 4;
        SyntaxToken token = SyntaxFactory.Token(kind);

        // Act
        int actualWidth = token.Width;

        // Assert
        Assert.Equal(expectedWidth, actualWidth);
    }

    [Fact(DisplayName = $"The Create() method must set the {nameof(SyntaxToken.LeadingTrivia)} property to null.")]
    public void CreateMethod_MustSetLeadingTriviaToNull()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.TrueKeyword;
        GreenNode token = SyntaxFactory.Token(kind);

        // Act
        GreenNode? actualLeadingTrivia = token.LeadingTrivia;

        // Assert
        Assert.Null(actualLeadingTrivia);
    }

    [Fact(DisplayName = $"The Create() method must set the {nameof(SyntaxToken.TrailingTrivia)} property to null.")]
    public void CreateMethod_MustSetTrailingTriviaToNull()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.TrueKeyword;
        GreenNode token = SyntaxFactory.Token(kind);

        // Act
        GreenNode? actualTrailingTrivia = token.TrailingTrivia;

        // Assert
        Assert.Null(actualTrailingTrivia);
    }

    [Fact(DisplayName = $"The SyntaxToken with no trivia must have the {nameof(SyntaxToken.LeadingTriviaWidth)} set to 0.")]
    public void CreateMethod_NoTrivia_MustSetLeadingTriviaWidthToZero()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.TrueKeyword;
        GreenNode token = SyntaxFactory.Token(kind);

        // Act
        int actualLeadingTriviaWidth = token.LeadingTriviaWidth;

        // Assert
        Assert.Equal(0, actualLeadingTriviaWidth);
    }

    [Fact(DisplayName = $"The SyntaxToken with no trivia must have the {nameof(SyntaxToken.TrailingTriviaWidth)} set to 0.")]
    public void CreateMethod_NoTrivia_MustSetTrailingTriviaWidthToZero()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.TrueKeyword;
        GreenNode token = SyntaxFactory.Token(kind);

        // Act
        int actualTrailingTriviaWidth = token.TrailingTriviaWidth;

        // Assert
        Assert.Equal(0, actualTrailingTriviaWidth);
    }

    [Fact(DisplayName = $"The default SyntaxToken must have the {nameof(SyntaxToken.SlotCount)} set to 0.")]
    public void DefaultSyntaxToken_MustSetSlotCountToZero()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.TrueKeyword;
        GreenNode token = SyntaxFactory.Token(kind);

        // Act
        int actualSlotCount = token.SlotCount;

        // Assert
        Assert.Equal(0, actualSlotCount);
    }

    [Fact(DisplayName = $"The GetSlot() method must throw an {nameof(InvalidOperationException)}")]
    public void GetSlot_MustThrowInvalidOperationException()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.TrueKeyword;
        GreenNode token = SyntaxFactory.Token(kind);

        // Act
        GreenNode? ActualSlotFunc()
        {
            return token.GetSlot(0);
        }

        // Assert
        Assert.Throws<InvalidOperationException>(ActualSlotFunc);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxTrivia.IsTrivia)} property must return false.")]
    public void IsTriviaProperty_MustReturnFalse()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.TrueKeyword;
        GreenNode token = SyntaxFactory.Token(kind);

        // Act
        bool actualIsTrivia = token.IsTrivia;

        // Assert
        Assert.False(actualIsTrivia);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxTrivia.IsTrivia)} property must return true.")]
    public void IsTokenProperty_MustReturnTrue()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.TrueKeyword;
        GreenNode token = SyntaxFactory.Token(kind);

        // Act
        bool actualIsToken = token.IsToken;

        // Assert
        Assert.True(actualIsToken);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxTrivia.LeadingTrivia)} property must be assigned from constructor.")]
    public void LeadingTriviaProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.TrueKeyword;
        GreenNode leadingTrivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, "  ");
        GreenNode token = SyntaxFactory.Token(kind, leadingTrivia, null);

        // Act
        GreenNode? actualLeadingTrivia = token.LeadingTrivia;

        // Assert
        Assert.Equal(leadingTrivia, actualLeadingTrivia);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxTrivia.TrailingTrivia)} property must be assigned from constructor.")]
    public void TrailingTriviaProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.TrueKeyword;
        GreenNode trailingTrivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, "  ");
        GreenNode token = SyntaxFactory.Token(kind, null, trailingTrivia);

        // Act
        GreenNode? actualTrailingTrivia = token.TrailingTrivia;

        // Assert
        Assert.Equal(trailingTrivia, actualTrailingTrivia);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxToken.FullWidth)} property must take into account the leading and trailing trivia.")]
    public void FullWidthProperty_WithLeadingAndTrailingTrivia_MustTakeIntoAccountLeadingAndTrailingTrivia()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.TrueKeyword; // FullWidth = 4
        GreenNode leadingTrivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, "   "); // FullWidth = 3
        GreenNode trailingTrivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, "  "); // FullWidth = 2
        GreenNode token = SyntaxFactory.Token(kind, leadingTrivia, trailingTrivia);

        // Act
        int actualFullWidth = token.FullWidth;

        // Assert
        Assert.Equal(9, actualFullWidth);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxToken.Width)} property must not take into account the leading and trailing trivia.")]
    public void WidthProperty_WithLeadingAndTrailingTrivia_MustNotTakeIntoAccountLeadingAndTrailingTrivia()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.TrueKeyword; // FullWidth = 4
        GreenNode leadingTrivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, "   "); // FullWidth = 3
        GreenNode trailingTrivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, "  "); // FullWidth = 2
        GreenNode token = SyntaxFactory.Token(kind, leadingTrivia, trailingTrivia);

        // Act
        int actualWidth = token.Width;

        // Assert
        Assert.Equal(4, actualWidth);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxToken.LeadingTriviaWidth)} property must be assigned from {nameof(SyntaxTrivia)}.")]
    public void LeadingTriviaWidthProperty_MustBeAssignedFromSyntaxTrivia()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.TrueKeyword; // FullWidth = 4
        GreenNode leadingTrivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, "   "); // FullWidth = 3
        GreenNode trailingTrivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, "  "); // FullWidth = 2
        GreenNode token = SyntaxFactory.Token(kind, leadingTrivia, trailingTrivia);

        // Act
        int leadingTriviaWidth = token.LeadingTriviaWidth;

        // Assert
        Assert.Equal(3, leadingTriviaWidth);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxToken.TrailingTrivia)} property must be assigned from {nameof(SyntaxTrivia)}.")]
    public void TrailingTriviaWidthProperty_MustBeAssignedFromSyntaxTrivia()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.TrueKeyword; // FullWidth = 4
        GreenNode leadingTrivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, "   "); // FullWidth = 3
        GreenNode trailingTrivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, "  "); // FullWidth = 2
        GreenNode token = SyntaxFactory.Token(kind, leadingTrivia, trailingTrivia);

        // Act
        int trailingTriviaWidth = token.TrailingTriviaWidth;

        // Assert
        Assert.Equal(2, trailingTriviaWidth);
    }
}

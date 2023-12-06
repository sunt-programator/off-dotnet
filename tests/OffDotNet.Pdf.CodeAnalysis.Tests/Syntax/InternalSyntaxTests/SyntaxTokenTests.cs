// <copyright file="SyntaxTokenTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using InternalSyntax = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxTests;

public class SyntaxTokenTests
{
    [Fact(DisplayName = $"The Create() method must set the {nameof(InternalSyntax.SyntaxToken.Kind)} property.")]
    public void CreateMethod_MustSetKindProperty()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.NumericLiteralToken;
        InternalSyntax.SyntaxToken token = InternalSyntax.SyntaxToken.Create(kind);

        // Act
        SyntaxKind actualSyntaxKind = token.Kind;

        // Assert
        Assert.Equal(kind, actualSyntaxKind);
    }

    [Theory(DisplayName = $"The Create() method must set the {nameof(InternalSyntax.SyntaxToken.Text)} property.")]
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
    [InlineData(SyntaxKind.XrefKeyword, "xref")]
    [InlineData(SyntaxKind.TrailerKeyword, "trailer")]
    [InlineData(SyntaxKind.StartXrefKeyword, "startxref")]
    public void CreateMethod_MustSetTextProperty(SyntaxKind kind, string text)
    {
        // Arrange
        InternalSyntax.SyntaxToken token = InternalSyntax.SyntaxToken.Create(kind);

        // Act
        string actualText = token.Text;

        // Assert
        Assert.Equal(text, actualText);
    }

    [Theory(DisplayName = $"Two SyntaxTokens with the same {nameof(InternalSyntax.SyntaxToken.Kind)} must have the same reference on {nameof(InternalSyntax.SyntaxToken.Value)} property.")]
    [InlineData(SyntaxKind.TrueKeyword, true)]
    [InlineData(SyntaxKind.FalseKeyword, false)]
    [InlineData(SyntaxKind.NullKeyword, null)]
    [InlineData(SyntaxKind.None, null)]
    public void TwoSyntaxTokens_Keywords_MustHaveSameReferenceValue(SyntaxKind kind, object? value)
    {
        // Arrange
        InternalSyntax.SyntaxToken token1 = InternalSyntax.SyntaxToken.Create(kind);
        InternalSyntax.SyntaxToken token2 = InternalSyntax.SyntaxToken.Create(kind);

        // Act
        object? actualValue1 = token1.Value;
        object? actualValue2 = token2.Value;

        // Assert
        Assert.Equal(value, actualValue1);
        Assert.Equal(value, actualValue2);
        Assert.True(ReferenceEquals(actualValue1, actualValue2));
    }

    [Fact(DisplayName = $"The ToString() method must return the {nameof(InternalSyntax.SyntaxToken.Text)} property value.")]
    public void ToStringMethod_MustReturnTheTextPropertyValue()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.TrueKeyword;
        const string expectedToStringValue = "true";

        InternalSyntax.SyntaxToken token = InternalSyntax.SyntaxToken.Create(kind);

        // Act
        string actualToStringValue = token.ToString();

        // Assert
        Assert.Equal(expectedToStringValue, actualToStringValue);
    }

    [Fact(DisplayName = $"The Create() method must set the {nameof(InternalSyntax.SyntaxToken.FullWidth)} property with the computed {nameof(InternalSyntax.SyntaxToken.Text)} property length.")]
    public void CreateMethod_MustSetFullWidthPropertyWithTextPropertyLength()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.TrueKeyword;
        const int expectedFullWidth = 4;
        InternalSyntax.SyntaxToken token = InternalSyntax.SyntaxToken.Create(kind);

        // Act
        int actualFullWidth = token.FullWidth;

        // Assert
        Assert.Equal(expectedFullWidth, actualFullWidth);
    }

    [Fact(DisplayName = $"The Create() method must set the {nameof(InternalSyntax.SyntaxToken.Width)} property the same value as the {nameof(InternalSyntax.SyntaxToken.FullWidth)} property.")]
    public void CreateMethod_NoTrivia_MustSetToFullWidthTheWithProperty()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.TrueKeyword;
        const int expectedWidth = 4;
        InternalSyntax.SyntaxToken token = InternalSyntax.SyntaxToken.Create(kind);

        // Act
        int actualWidth = token.Width;

        // Assert
        Assert.Equal(expectedWidth, actualWidth);
    }

    [Fact(DisplayName = $"The Create() method must set the {nameof(InternalSyntax.SyntaxToken.LeadingTrivia)} property to null.")]
    public void CreateMethod_MustSetLeadingTriviaToNull()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.TrueKeyword;
        GreenNode token = InternalSyntax.SyntaxToken.Create(kind);

        // Act
        GreenNode? actualLeadingTrivia = token.LeadingTrivia;

        // Assert
        Assert.Null(actualLeadingTrivia);
    }

    [Fact(DisplayName = $"The Create() method must set the {nameof(InternalSyntax.SyntaxToken.TrailingTrivia)} property to null.")]
    public void CreateMethod_MustSetTrailingTriviaToNull()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.TrueKeyword;
        GreenNode token = InternalSyntax.SyntaxToken.Create(kind);

        // Act
        GreenNode? actualTrailingTrivia = token.TrailingTrivia;

        // Assert
        Assert.Null(actualTrailingTrivia);
    }

    [Fact(DisplayName = $"The SyntaxToken with no trivia must have the {nameof(InternalSyntax.SyntaxToken.LeadingTriviaWidth)} set to 0.")]
    public void CreateMethod_NoTrivia_MustSetLeadingTriviaWidthToZero()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.TrueKeyword;
        GreenNode token = InternalSyntax.SyntaxToken.Create(kind);

        // Act
        int actualLeadingTriviaWidth = token.LeadingTriviaWidth;

        // Assert
        Assert.Equal(0, actualLeadingTriviaWidth);
    }

    [Fact(DisplayName = $"The SyntaxToken with no trivia must have the {nameof(InternalSyntax.SyntaxToken.TrailingTriviaWidth)} set to 0.")]
    public void CreateMethod_NoTrivia_MustSetTrailingTriviaWidthToZero()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.TrueKeyword;
        GreenNode token = InternalSyntax.SyntaxToken.Create(kind);

        // Act
        int actualTrailingTriviaWidth = token.TrailingTriviaWidth;

        // Assert
        Assert.Equal(0, actualTrailingTriviaWidth);
    }

    [Fact(DisplayName = $"The default SyntaxToken must have the {nameof(InternalSyntax.SyntaxToken.SlotCount)} set to 0.")]
    public void DefaultSyntaxToken_MustSetSlotCountToZero()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.TrueKeyword;
        GreenNode token = InternalSyntax.SyntaxToken.Create(kind);

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
        GreenNode token = InternalSyntax.SyntaxToken.Create(kind);

        // Act
        GreenNode? ActualSlotFunc()
        {
            return token.GetSlot(0);
        }

        // Assert
        Assert.Throws<InvalidOperationException>(ActualSlotFunc);
    }

    [Fact(DisplayName = $"The {nameof(InternalSyntax.SyntaxTrivia.IsTrivia)} property must return false.")]
    public void IsTriviaProperty_MustReturnFalse()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.TrueKeyword;
        GreenNode token = InternalSyntax.SyntaxToken.Create(kind);

        // Act
        bool actualIsTrivia = token.IsTrivia;

        // Assert
        Assert.False(actualIsTrivia);
    }

    [Fact(DisplayName = $"The {nameof(InternalSyntax.SyntaxTrivia.IsTrivia)} property must return true.")]
    public void IsTokenProperty_MustReturnTrue()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.TrueKeyword;
        GreenNode token = InternalSyntax.SyntaxToken.Create(kind);

        // Act
        bool actualIsToken = token.IsToken;

        // Assert
        Assert.True(actualIsToken);
    }

    [Fact(DisplayName = $"The {nameof(InternalSyntax.SyntaxTrivia.LeadingTrivia)} property must be assigned from constructor.")]
    public void LeadingTriviaProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.TrueKeyword;
        GreenNode leadingTrivia = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, "  ");
        GreenNode token = InternalSyntax.SyntaxToken.Create(kind, leadingTrivia, null);

        // Act
        GreenNode? actualLeadingTrivia = token.LeadingTrivia;

        // Assert
        Assert.Equal(leadingTrivia, actualLeadingTrivia);
    }

    [Fact(DisplayName = $"The {nameof(InternalSyntax.SyntaxTrivia.TrailingTrivia)} property must be assigned from constructor.")]
    public void TrailingTriviaProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.TrueKeyword;
        GreenNode trailingTrivia = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, "  ");
        GreenNode token = InternalSyntax.SyntaxToken.Create(kind, null, trailingTrivia);

        // Act
        GreenNode? actualTrailingTrivia = token.TrailingTrivia;

        // Assert
        Assert.Equal(trailingTrivia, actualTrailingTrivia);
    }

    [Fact(DisplayName = $"The {nameof(InternalSyntax.SyntaxToken.FullWidth)} property must take into account the leading and trailing trivia.")]
    public void FullWidthProperty_WithLeadingAndTrailingTrivia_MustTakeIntoAccountLeadingAndTrailingTrivia()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.TrueKeyword; // FullWidth = 4
        GreenNode leadingTrivia = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, "   "); // FullWidth = 3
        GreenNode trailingTrivia = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, "  "); // FullWidth = 2
        GreenNode token = InternalSyntax.SyntaxToken.Create(kind, leadingTrivia, trailingTrivia);

        // Act
        int actualFullWidth = token.FullWidth;

        // Assert
        Assert.Equal(9, actualFullWidth);
    }

    [Fact(DisplayName = $"The {nameof(InternalSyntax.SyntaxToken.Width)} property must not take into account the leading and trailing trivia.")]
    public void WidthProperty_WithLeadingAndTrailingTrivia_MustNotTakeIntoAccountLeadingAndTrailingTrivia()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.TrueKeyword; // FullWidth = 4
        GreenNode leadingTrivia = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, "   "); // FullWidth = 3
        GreenNode trailingTrivia = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, "  "); // FullWidth = 2
        GreenNode token = InternalSyntax.SyntaxToken.Create(kind, leadingTrivia, trailingTrivia);

        // Act
        int actualWidth = token.Width;

        // Assert
        Assert.Equal(4, actualWidth);
    }

    [Fact(DisplayName = $"The {nameof(InternalSyntax.SyntaxToken.LeadingTriviaWidth)} property must be assigned from {nameof(InternalSyntax.SyntaxTrivia)}.")]
    public void LeadingTriviaWidthProperty_MustBeAssignedFromSyntaxTrivia()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.TrueKeyword; // FullWidth = 4
        GreenNode leadingTrivia = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, "   "); // FullWidth = 3
        GreenNode trailingTrivia = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, "  "); // FullWidth = 2
        GreenNode token = InternalSyntax.SyntaxToken.Create(kind, leadingTrivia, trailingTrivia);

        // Act
        int leadingTriviaWidth = token.LeadingTriviaWidth;

        // Assert
        Assert.Equal(3, leadingTriviaWidth);
    }

    [Fact(DisplayName = $"The {nameof(InternalSyntax.SyntaxToken.TrailingTrivia)} property must be assigned from {nameof(InternalSyntax.SyntaxTrivia)}.")]
    public void TrailingTriviaWidthProperty_MustBeAssignedFromSyntaxTrivia()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.TrueKeyword; // FullWidth = 4
        GreenNode leadingTrivia = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, "   "); // FullWidth = 3
        GreenNode trailingTrivia = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, "  "); // FullWidth = 2
        GreenNode token = InternalSyntax.SyntaxToken.Create(kind, leadingTrivia, trailingTrivia);

        // Act
        int trailingTriviaWidth = token.TrailingTriviaWidth;

        // Assert
        Assert.Equal(2, trailingTriviaWidth);
    }
}

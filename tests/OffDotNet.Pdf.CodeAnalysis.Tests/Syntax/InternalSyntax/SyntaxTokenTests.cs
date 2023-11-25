// <copyright file="SyntaxTokenTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntax;

public class SyntaxTokenTests
{
    [Fact(DisplayName = $"The CreateWithKind() method must set the {nameof(SyntaxToken.Kind)} property.")]
    public void CreateWithKindMethod_MustSetKindProperty()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.NumericLiteralToken;
        SyntaxToken token = SyntaxToken.CreateWithKind(kind);

        // Act
        SyntaxKind actualSyntaxKind = token.Kind;

        // Assert
        Assert.Equal(kind, actualSyntaxKind);
    }

    [Theory(DisplayName = $"The CreateWithKind() method must set the {nameof(SyntaxToken.Text)} property.")]
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
    public void CreateWithKindMethod_MustSetTextProperty(SyntaxKind kind, string text)
    {
        // Arrange
        SyntaxToken token = SyntaxToken.CreateWithKind(kind);

        // Act
        string actualText = token.Text;

        // Assert
        Assert.Equal(text, actualText);
    }

    [Theory(DisplayName = $"Two SyntaxTokens with the same {nameof(SyntaxToken.Kind)} must have the same reference on {nameof(SyntaxToken.Value)} property.")]
    [InlineData(SyntaxKind.TrueKeyword, true)]
    [InlineData(SyntaxKind.FalseKeyword, false)]
    [InlineData(SyntaxKind.NullKeyword, null)]
    public void TwoSyntaxTokens_Keywords_MustHaveSameReferenceValue(SyntaxKind kind, object? value)
    {
        // Arrange
        SyntaxToken token1 = SyntaxToken.CreateWithKind(kind);
        SyntaxToken token2 = SyntaxToken.CreateWithKind(kind);

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

        SyntaxToken token = SyntaxToken.CreateWithKind(kind);

        // Act
        string actualToStringValue = token.ToString();

        // Assert
        Assert.Equal(expectedToStringValue, actualToStringValue);
    }

    [Fact(DisplayName = $"The CreateWithKind() method must set the {nameof(SyntaxToken.FullWidth)} property with the computed {nameof(SyntaxToken.Text)} property length.")]
    public void CreateWithKindMethod_MustSetFullWidthPropertyWithTextPropertyLength()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.TrueKeyword;
        const int expectedFullWidth = 4;
        SyntaxToken token = SyntaxToken.CreateWithKind(kind);

        // Act
        int actualFullWidth = token.FullWidth;

        // Assert
        Assert.Equal(expectedFullWidth, actualFullWidth);
    }

    [Fact(DisplayName = $"The CreateWithKindAndFullWidth() method must set the {nameof(SyntaxToken.Kind)} and {nameof(SyntaxToken.FullWidth)} properties.")]
    public void CreateWithKindAndFullWidthMethod_MustSetKindAndWidthProperties()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.NumericLiteralToken;
        const int fullWidth = 3;
        SyntaxToken token = SyntaxToken.CreateWithKindAndFullWidth(kind, fullWidth);

        // Act
        int actualFullWidth = token.FullWidth;

        // Assert
        Assert.Equal(fullWidth, actualFullWidth);
    }

    [Fact(DisplayName = $"The CreateWithKind() method must set the {nameof(SyntaxToken.Width)} property the same value as the {nameof(SyntaxToken.FullWidth)} property.")]
    public void CreateWithKindMethod_NoTrivia_MustSetToFullWidthTheWithProperty()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.TrueKeyword;
        const int expectedWidth = 4;
        SyntaxToken token = SyntaxToken.CreateWithKind(kind);

        // Act
        int actualWidth = token.Width;

        // Assert
        Assert.Equal(expectedWidth, actualWidth);
    }

    [Fact(DisplayName = $"The CreateWithKind() method must set the {nameof(SyntaxToken.LeadingTrivia)} property to null.")]
    public void CreateWithKindMethod_MustSetLeadingTriviaToNull()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.TrueKeyword;
        GreenNode token = SyntaxToken.CreateWithKind(kind);

        // Act
        GreenNode? actualLeadingTrivia = token.LeadingTrivia;

        // Assert
        Assert.Null(actualLeadingTrivia);
    }

    [Fact(DisplayName = $"The CreateWithKind() method must set the {nameof(SyntaxToken.TrailingTrivia)} property to null.")]
    public void CreateWithKindMethod_MustSetTrailingTriviaToNull()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.TrueKeyword;
        GreenNode token = SyntaxToken.CreateWithKind(kind);

        // Act
        GreenNode? actualTrailingTrivia = token.TrailingTrivia;

        // Assert
        Assert.Null(actualTrailingTrivia);
    }

    [Fact(DisplayName = $"The SyntaxToken with no trivia must have the {nameof(SyntaxToken.LeadingTriviaWidth)} set to 0.")]
    public void CreateWithKindMethod_NoTrivia_MustSetLeadingTriviaWidthToZero()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.TrueKeyword;
        GreenNode token = SyntaxToken.CreateWithKind(kind);

        // Act
        int actualLeadingTriviaWidth = token.LeadingTriviaWidth;

        // Assert
        Assert.Equal(0, actualLeadingTriviaWidth);
    }

    [Fact(DisplayName = $"The SyntaxToken with no trivia must have the {nameof(SyntaxToken.TrailingTriviaWidth)} set to 0.")]
    public void CreateWithKindMethod_NoTrivia_MustSetTrailingTriviaWidthToZero()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.TrueKeyword;
        GreenNode token = SyntaxToken.CreateWithKind(kind);

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
        GreenNode token = SyntaxToken.CreateWithKind(kind);

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
        GreenNode token = SyntaxToken.CreateWithKind(kind);

        // Act
        GreenNode? ActualSlotFunc()
        {
            return token.GetSlot(0);
        }

        // Assert
        Assert.Throws<InvalidOperationException>(ActualSlotFunc);
    }
}

// <copyright file="SyntaxTokenTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxTests;

using OffDotNet.Pdf.CodeAnalysis.Diagnostic;
using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using SyntaxFactory = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxFactory;
using SyntaxToken = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxToken;
using SyntaxTrivia = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxTrivia;

public class SyntaxTokenTests
{
    [Fact(DisplayName = $"The Create() method must set the {nameof(SyntaxToken.Kind)} property.")]
    public void CreateMethod_MustSetKindProperty()
    {
        // Arrange
        const SyntaxKind Kind = SyntaxKind.NumericLiteralToken;
        var token = SyntaxFactory.Token(Kind);

        // Act
        var actualSyntaxKind = token.Kind;

        // Assert
        Assert.Equal(Kind, actualSyntaxKind);
    }

    [Theory(DisplayName = $"The Create() method must set the {nameof(SyntaxToken.Text)} property.")]
    [InlineData(SyntaxKind.None, "")]
    [InlineData(SyntaxKind.LeftSquareBracketToken, "[")]
    [InlineData(SyntaxKind.RightSquareBracketToken, "]")]
    [InlineData(SyntaxKind.LeftCurlyBracketToken, "{")]
    [InlineData(SyntaxKind.RightCurlyBracketToken, "}")]
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
    [InlineData(SyntaxKind.StartXRefKeyword, "startxref")]
    [InlineData(SyntaxKind.FreeXRefEntryKeyword, "f")]
    [InlineData(SyntaxKind.InUseXRefEntryKeyword, "n")]
    public void CreateMethod_MustSetTextProperty(SyntaxKind kind, string text)
    {
        // Arrange
        var token = SyntaxFactory.Token(kind);

        // Act
        var actualText = token.Text;

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
        var token1 = SyntaxFactory.Token(kind);
        var token2 = SyntaxFactory.Token(kind);

        // Act
        var actualValue1 = token1.Value;
        var actualValue2 = token2.Value;

        // Assert
        Assert.Equal(value, actualValue1);
        Assert.Equal(value, actualValue2);
        Assert.True(ReferenceEquals(actualValue1, actualValue2));
    }

    [Fact(DisplayName = $"The ToString() method must return the {nameof(SyntaxToken.Text)} property value.")]
    public void ToStringMethod_MustReturnTheTextPropertyValue()
    {
        // Arrange
        const SyntaxKind Kind = SyntaxKind.TrueKeyword;
        const string ExpectedToStringValue = "true";

        var token = SyntaxFactory.Token(Kind);

        // Act
        var actualToStringValue = token.ToString();

        // Assert
        Assert.Equal(ExpectedToStringValue, actualToStringValue);
    }

    [Fact(DisplayName = $"The Create() method must set the {nameof(SyntaxToken.FullWidth)} property with the computed {nameof(SyntaxToken.Text)} property length.")]
    public void CreateMethod_MustSetFullWidthPropertyWithTextPropertyLength()
    {
        // Arrange
        const SyntaxKind Kind = SyntaxKind.TrueKeyword;
        const int ExpectedFullWidth = 4;
        var token = SyntaxFactory.Token(Kind);

        // Act
        var actualFullWidth = token.FullWidth;

        // Assert
        Assert.Equal(ExpectedFullWidth, actualFullWidth);
    }

    [Fact(DisplayName = $"The Create() method must set the {nameof(SyntaxToken.Width)} property the same value as the {nameof(SyntaxToken.FullWidth)} property.")]
    public void CreateMethod_NoTrivia_MustSetToFullWidthTheWithProperty()
    {
        // Arrange
        const SyntaxKind Kind = SyntaxKind.TrueKeyword;
        const int ExpectedWidth = 4;
        var token = SyntaxFactory.Token(Kind);

        // Act
        var actualWidth = token.Width;

        // Assert
        Assert.Equal(ExpectedWidth, actualWidth);
    }

    [Fact(DisplayName = $"The Create() method must set the {nameof(SyntaxToken.LeadingTrivia)} property to null.")]
    public void CreateMethod_MustSetLeadingTriviaToNull()
    {
        // Arrange
        const SyntaxKind Kind = SyntaxKind.TrueKeyword;
        GreenNode token = SyntaxFactory.Token(Kind);

        // Act
        var actualLeadingTrivia = token.LeadingTrivia;

        // Assert
        Assert.Null(actualLeadingTrivia);
    }

    [Fact(DisplayName = $"The Create() method must set the {nameof(SyntaxToken.TrailingTrivia)} property to null.")]
    public void CreateMethod_MustSetTrailingTriviaToNull()
    {
        // Arrange
        const SyntaxKind Kind = SyntaxKind.TrueKeyword;
        GreenNode token = SyntaxFactory.Token(Kind);

        // Act
        var actualTrailingTrivia = token.TrailingTrivia;

        // Assert
        Assert.Null(actualTrailingTrivia);
    }

    [Fact(DisplayName = $"The SyntaxToken with no trivia must have the {nameof(SyntaxToken.LeadingTriviaWidth)} set to 0.")]
    public void CreateMethod_NoTrivia_MustSetLeadingTriviaWidthToZero()
    {
        // Arrange
        const SyntaxKind Kind = SyntaxKind.TrueKeyword;
        GreenNode token = SyntaxFactory.Token(Kind);

        // Act
        var actualLeadingTriviaWidth = token.LeadingTriviaWidth;

        // Assert
        Assert.Equal(0, actualLeadingTriviaWidth);
    }

    [Fact(DisplayName = $"The SyntaxToken with no trivia must have the {nameof(SyntaxToken.TrailingTriviaWidth)} set to 0.")]
    public void CreateMethod_NoTrivia_MustSetTrailingTriviaWidthToZero()
    {
        // Arrange
        const SyntaxKind Kind = SyntaxKind.TrueKeyword;
        GreenNode token = SyntaxFactory.Token(Kind);

        // Act
        var actualTrailingTriviaWidth = token.TrailingTriviaWidth;

        // Assert
        Assert.Equal(0, actualTrailingTriviaWidth);
    }

    [Fact(DisplayName = $"The default SyntaxToken must have the {nameof(SyntaxToken.SlotCount)} set to 0.")]
    public void DefaultSyntaxToken_MustSetSlotCountToZero()
    {
        // Arrange
        const SyntaxKind Kind = SyntaxKind.TrueKeyword;
        GreenNode token = SyntaxFactory.Token(Kind);

        // Act
        var actualSlotCount = token.SlotCount;

        // Assert
        Assert.Equal(0, actualSlotCount);
    }

    [Fact(DisplayName = $"The GetSlot() method must throw an {nameof(InvalidOperationException)}")]
    public void GetSlot_MustThrowInvalidOperationException()
    {
        // Arrange
        const SyntaxKind Kind = SyntaxKind.TrueKeyword;
        GreenNode token = SyntaxFactory.Token(Kind);

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
        const SyntaxKind Kind = SyntaxKind.TrueKeyword;
        GreenNode token = SyntaxFactory.Token(Kind);

        // Act
        var actualIsTrivia = token.IsTrivia;

        // Assert
        Assert.False(actualIsTrivia);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxTrivia.IsTrivia)} property must return true.")]
    public void IsTokenProperty_MustReturnTrue()
    {
        // Arrange
        const SyntaxKind Kind = SyntaxKind.TrueKeyword;
        GreenNode token = SyntaxFactory.Token(Kind);

        // Act
        var actualIsToken = token.IsToken;

        // Assert
        Assert.True(actualIsToken);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxTrivia.LeadingTrivia)} property must be assigned from constructor.")]
    public void LeadingTriviaProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        const SyntaxKind Kind = SyntaxKind.TrueKeyword;
        GreenNode leadingTrivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, "  ");
        GreenNode token = SyntaxFactory.Token(Kind, leadingTrivia, null);

        // Act
        var actualLeadingTrivia = token.LeadingTrivia;

        // Assert
        Assert.Equal(leadingTrivia, actualLeadingTrivia);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxTrivia.TrailingTrivia)} property must be assigned from constructor.")]
    public void TrailingTriviaProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        const SyntaxKind Kind = SyntaxKind.TrueKeyword;
        GreenNode trailingTrivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, "  ");
        GreenNode token = SyntaxFactory.Token(Kind, null, trailingTrivia);

        // Act
        var actualTrailingTrivia = token.TrailingTrivia;

        // Assert
        Assert.Equal(trailingTrivia, actualTrailingTrivia);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxToken.FullWidth)} property must take into account the leading and trailing trivia.")]
    public void FullWidthProperty_WithLeadingAndTrailingTrivia_MustTakeIntoAccountLeadingAndTrailingTrivia()
    {
        // Arrange
        const SyntaxKind Kind = SyntaxKind.TrueKeyword; // FullWidth = 4
        GreenNode leadingTrivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, "   "); // FullWidth = 3
        GreenNode trailingTrivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, "  "); // FullWidth = 2
        GreenNode token = SyntaxFactory.Token(Kind, leadingTrivia, trailingTrivia);

        // Act
        var actualFullWidth = token.FullWidth;

        // Assert
        Assert.Equal(9, actualFullWidth);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxToken.Width)} property must not take into account the leading and trailing trivia.")]
    public void WidthProperty_WithLeadingAndTrailingTrivia_MustNotTakeIntoAccountLeadingAndTrailingTrivia()
    {
        // Arrange
        const SyntaxKind Kind = SyntaxKind.TrueKeyword; // FullWidth = 4
        GreenNode leadingTrivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, "   "); // FullWidth = 3
        GreenNode trailingTrivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, "  "); // FullWidth = 2
        GreenNode token = SyntaxFactory.Token(Kind, leadingTrivia, trailingTrivia);

        // Act
        var actualWidth = token.Width;

        // Assert
        Assert.Equal(4, actualWidth);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxToken.LeadingTriviaWidth)} property must be assigned from {nameof(SyntaxTrivia)}.")]
    public void LeadingTriviaWidthProperty_MustBeAssignedFromSyntaxTrivia()
    {
        // Arrange
        const SyntaxKind Kind = SyntaxKind.TrueKeyword; // FullWidth = 4
        GreenNode leadingTrivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, "   "); // FullWidth = 3
        GreenNode trailingTrivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, "  "); // FullWidth = 2
        GreenNode token = SyntaxFactory.Token(Kind, leadingTrivia, trailingTrivia);

        // Act
        var leadingTriviaWidth = token.LeadingTriviaWidth;

        // Assert
        Assert.Equal(3, leadingTriviaWidth);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxToken.TrailingTrivia)} property must be assigned from {nameof(SyntaxTrivia)}.")]
    public void TrailingTriviaWidthProperty_MustBeAssignedFromSyntaxTrivia()
    {
        // Arrange
        const SyntaxKind Kind = SyntaxKind.TrueKeyword; // FullWidth = 4
        GreenNode leadingTrivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, "   "); // FullWidth = 3
        GreenNode trailingTrivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, "  "); // FullWidth = 2
        GreenNode token = SyntaxFactory.Token(Kind, leadingTrivia, trailingTrivia);

        // Act
        var trailingTriviaWidth = token.TrailingTriviaWidth;

        // Assert
        Assert.Equal(2, trailingTriviaWidth);
    }

    [Fact(DisplayName = "The ToString() method must not include the trivia.")]
    public void ToStringMethod_MustNotIncludeTrivia()
    {
        // Arrange
        const SyntaxKind Kind = SyntaxKind.TrueKeyword;
        GreenNode leadingTrivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, "   ");
        GreenNode trailingTrivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, "  ");
        GreenNode token = SyntaxFactory.Token(Kind, leadingTrivia, trailingTrivia);

        // Act
        var actualString = token.ToString();

        // Assert
        Assert.Equal("true", actualString);
    }

    [Fact(DisplayName = "The ToFullString() method must include the trivia.")]
    public void ToFullStringMethod_MustIncludeTrivia()
    {
        // Arrange
        const SyntaxKind Kind = SyntaxKind.TrueKeyword;
        GreenNode leadingTrivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, "   ");
        GreenNode trailingTrivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, "  ");
        GreenNode token = SyntaxFactory.Token(Kind, leadingTrivia, trailingTrivia);

        // Act
        var actualString = token.ToFullString();

        // Assert
        Assert.Equal("   true  ", actualString);
    }

    [Fact(DisplayName = "The SetDiagnostics() method must set the diagnostics and return a new instance.")]
    public void SetDiagnosticsMethod_MustSetDiagnosticsAndReturnNewInstance()
    {
        // Arrange
        const SyntaxKind Kind = SyntaxKind.TrueKeyword;
        GreenNode token = SyntaxFactory.Token(Kind);

        DiagnosticInfo expectedDiagnostic = new(Substitute.For<IMessageProvider>(), DiagnosticCode.ERR_InvalidPDF);
        DiagnosticInfo[] diagnostics = [expectedDiagnostic];

        // Act
        var actualToken = token.SetDiagnostics(diagnostics);

        // Assert
        Assert.NotSame(token, actualToken);
        Assert.Equal(diagnostics, actualToken.GetDiagnostics());
        Assert.True(actualToken.ContainsFlags(NodeFlags.ContainsDiagnostics));
    }
}

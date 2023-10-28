// <copyright file="LexerTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Globalization;
using System.Text;
using OffDotNet.Pdf.CodeAnalysis.Errors;
using OffDotNet.Pdf.CodeAnalysis.Syntax;
using Xunit;

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Parser;

public class LexerTests
{
    [Theory(DisplayName = "Test the PDF punctuation")]
    [Trait("Feature", "Punctuation")]
    [InlineData(SyntaxKind.RightParenthesisToken)]
    [InlineData(SyntaxKind.LessThanToken)]
    [InlineData(SyntaxKind.GreaterThanToken)]
    [InlineData(SyntaxKind.LeftSquareBracketToken)]
    [InlineData(SyntaxKind.RightSquareBracketToken)]
    [InlineData(SyntaxKind.LeftCurlyBracketToken)]
    [InlineData(SyntaxKind.RightCurlyBracketToken)]
    [InlineData(SyntaxKind.SolidusToken)]
    [InlineData(SyntaxKind.LessThanLessThanToken)]
    [InlineData(SyntaxKind.GreaterThanGreaterThanToken)]
    [InlineData(SyntaxKind.PlusToken)]
    [InlineData(SyntaxKind.MinusToken)]
    public void TestPunctuation_ShouldReturnValidToken(SyntaxKind kind)
    {
        // Arrange
        string text = SyntaxFacts.GetText(kind);

        // Act
        var token = LexToken(text);

        // Assert
        Assert.Equal(kind, token.Kind);
        Assert.Equal(text, token.Text);
        Assert.Equal(text, token.Value);
        Assert.Empty(token.Errors());
    }

    [Fact(DisplayName = $"The numeric literal should return a {nameof(SyntaxKind.NumericLiteralToken)}")]
    [Trait("Feature", "Literals")]
    public void TestNumericLiteral_IntegerValue_ShouldReturnNumericLiteralToken()
    {
        // Arrange
        const int value = 123;
        const string text = "123";

        // Act
        var token = LexToken(text);

        // Assert
        Assert.NotEqual(default, token);
        Assert.Equal(SyntaxKind.NumericLiteralToken, token.Kind);
        Assert.Equal(text, token.Text);
        Assert.Equal(value, token.Value);
        Assert.Empty(token.Errors());
    }

    [Fact(DisplayName = $"The numeric literal with decimal point should return a {nameof(SyntaxKind.NumericLiteralToken)}")]
    [Trait("Feature", "Literals")]
    public void TestNumericLiteral_RealValue_ShouldReturnNumericLiteralToken()
    {
        // Arrange
        const float value = 123.456f;
        const string text = "123.456";

        // Act
        var token = LexToken(text);

        // Assert
        Assert.NotEqual(default, token);
        Assert.Equal(SyntaxKind.NumericLiteralToken, token.Kind);
        Assert.Equal(text, token.Text);
        Assert.Equal(value, token.Value);
        Assert.Empty(token.Errors());
    }

    [Fact(DisplayName = $"The numeric literal with huge decimal part should return a {nameof(SyntaxKind.NumericLiteralToken)}")]
    [Trait("Feature", "Literals")]
    public void TestNumericLiteral_RealValueWithHugeDecimal_ShouldReturnNumericLiteralToken()
    {
        // Arrange
        const float value = 123.45632434234234234234234234324234234234f;
        const string text = "123.45632434234234234234234234324234234234";

        // Act
        var token = LexToken(text);

        // Assert
        Assert.NotEqual(default, token);
        Assert.Equal(SyntaxKind.NumericLiteralToken, token.Kind);
        Assert.Equal(text, token.Text);
        Assert.Equal(value, token.Value);
        Assert.Empty(token.Errors());
    }

    [Fact(DisplayName = $"The numeric literal with huge number and a decimal part should return a {nameof(SyntaxKind.NumericLiteralToken)}")]
    [Trait("Feature", "Literals")]
    public void TestNumericLiteral_RealValueWithHugeNumberAndDecimal_ShouldReturnNumericLiteralToken()
    {
        // Arrange
        const float value = 12332434234234234234234234324234234234.456f;
        const string text = "12332434234234234234234234324234234234.456";

        // Act
        var token = LexToken(text);

        // Assert
        Assert.NotEqual(default, token);
        Assert.Equal(SyntaxKind.NumericLiteralToken, token.Kind);
        Assert.Equal(text, token.Text);
        Assert.Equal(value, token.Value);
        Assert.Empty(token.Errors());
    }

    [Fact(DisplayName = $"The numeric literal with huge number and huge decimal part should return a {nameof(SyntaxKind.NumericLiteralToken)}")]
    [Trait("Feature", "Literals")]
    public void TestNumericLiteral_RealValueWithHugeNumberAndHugeDecimal_ShouldReturnNumericLiteralToken()
    {
        // Arrange
        const float value = 12332434234234234234234234324234234234.45623423423423423423423423423423423423f;
        const string text = "12332434234234234234234234324234234234.45623423423423423423423423423423423423";

        // Act
        var token = LexToken(text);

        // Assert
        Assert.NotEqual(default, token);
        Assert.Equal(SyntaxKind.NumericLiteralToken, token.Kind);
        Assert.Equal(text, token.Text);
        Assert.Equal(value, token.Value);
        Assert.Empty(token.Errors());
    }

    [Fact(DisplayName = $"The numeric literal that starts with decimal point should return a {nameof(SyntaxKind.NumericLiteralToken)}")]
    [Trait("Feature", "Literals")]
    public void TestNumericLiteral_RealValueStartsWithDecimal_ShouldReturnNumericLiteralToken()
    {
        // Arrange
        const float value = .456f;
        const string text = ".456";

        // Act
        var token = LexToken(text);

        // Assert
        Assert.NotEqual(default, token);
        Assert.Equal(SyntaxKind.NumericLiteralToken, token.Kind);
        Assert.Equal(text, token.Text);
        Assert.Equal(value, token.Value);
        Assert.Empty(token.Errors());
    }

    [Fact(DisplayName = $"The numeric literal that ends with decimal point should return a {nameof(SyntaxKind.NumericLiteralToken)}")]
    [Trait("Feature", "Literals")]
    public void TestNumericLiteral_RealValueEndsWithDecimal_ShouldReturnNumericLiteralToken()
    {
        // Arrange
        const float value = 123.0f;
        const string text = "123.";

        // Act
        var token = LexToken(text);

        // Assert
        Assert.NotEqual(default, token);
        Assert.Equal(SyntaxKind.NumericLiteralToken, token.Kind);
        Assert.Equal(text, token.Text);
        Assert.Equal(value, token.Value);
        Assert.Empty(token.Errors());
    }

    [Fact(DisplayName = $"The numeric literal with overflowed value should return a {nameof(SyntaxKind.NumericLiteralToken)} with an error")]
    [Trait("Feature", "Literals")]
    public void TestNumericLiteral_IntegerValueWithOverflow_ShouldReturnNumericLiteralToken()
    {
        // Arrange
        const string text = "2147483648";

        // Act
        var token = LexToken(text);

        // Assert
        Assert.NotEqual(default, token);
        Assert.Equal(SyntaxKind.NumericLiteralToken, token.Kind);
        Assert.Equal(text, token.Text);
        var errors = token.Errors();
        var error = Assert.Single(errors);
        Assert.Equal(ErrorCode.ErrorIntOverflow, error.Code);
        Assert.Equal("error PDF0100: Integer value is too large", error.ToString(CultureInfo.InvariantCulture));
    }

    [Theory(DisplayName = "Test the PDF keywords")]
    [Trait("Feature", "Keywords")]
    [InlineData("true", SyntaxKind.TrueKeyword)]
    [InlineData("false", SyntaxKind.FalseKeyword)]
    [InlineData("null", SyntaxKind.NullKeyword)]
    [InlineData("obj", SyntaxKind.StartObjectKeyword)]
    [InlineData("endobj", SyntaxKind.EndObjectKeyword)]
    [InlineData("R", SyntaxKind.IndirectReferenceKeyword)]
    [InlineData("stream", SyntaxKind.StartStreamKeyword)]
    [InlineData("endstream", SyntaxKind.EndStreamKeyword)]
    [InlineData("xref", SyntaxKind.XRefKeyword)]
    [InlineData("trailer", SyntaxKind.TrailerKeyword)]
    [InlineData("startxref", SyntaxKind.StartXRefKeyword)]
    public void TestKeywords_ShouldReturnValidToken(string text, SyntaxKind expectedKind)
    {
        // Arrange

        // Act
        var token = LexToken(text);

        // Assert
        Assert.Equal(expectedKind, token.Kind);
        Assert.Equal(text, token.Text);
        Assert.Equal(text, token.Value);
        Assert.Empty(token.Errors());
    }

    [Theory(DisplayName = $"The string literal with should return a {nameof(SyntaxKind.StringLiteralToken)}")]
    [Trait("Feature", "Literals")]
    [InlineData("(This is a string)")]
    [InlineData("(Strings can contain newlines \nand such.)")]
    [InlineData("(Strings can contain balanced parentheses () \r\nand special characters ( * ! & } ^ %and so on) .)")]
    [InlineData("(The following is an empty string .)")]
    [InlineData("()")]
    [InlineData("(It has zero (0) length.)")]
    public void TestStringLiteral_ShouldReturnStringLiteralToken(string text)
    {
        // Arrange
        string expectedText = text.TrimStart('(').TrimEnd(')');

        // Act
        var token = LexToken(text);

        // Assert
        Assert.NotEqual(default, token);
        Assert.Equal(SyntaxKind.StringLiteralToken, token.Kind);
        Assert.Equal(expectedText, token.Text);
        Assert.Equal(text, token.Value);
        Assert.Empty(token.Errors());
    }

    [Fact(DisplayName = "Test the PDF comment")]
    [Trait("Feature", "Literals")]
    public void TestComment_WithoutTrivia_ShouldReturnValidToken()
    {
        // Arrange
        const string text = "%comment";

        // Act
        var token = LexToken(text);

        // Assert
        Assert.Equal(SyntaxKind.EndOfFileToken, token.Kind);
        Assert.Equal(text, token.ToFullString());
        Assert.Empty(token.Errors());

        SyntaxTrivia trivia = token.LeadingTrivia;
        Assert.Equal(SyntaxKind.SingleLineCommentTrivia, trivia.Kind);
        Assert.Equal(text, trivia.ToFullString());
        Assert.Equal(text, trivia.ToString());
        Assert.Empty(trivia.Errors());
    }

    [Fact(DisplayName = "Test the bad token")]
    [Trait("Feature", "BadToken")]
    public void TestBadInput_ShouldReturnBadToken()
    {
        // Arrange
        string text = $"{(char)255}";

        // Act
        var token = LexToken(text);

        // Assert
        Assert.NotEqual(default, token);
        Assert.Equal(SyntaxKind.BadToken, token.Kind);
        Assert.Equal(text, token.Text);

        var errors = token.Errors();
        var error = Assert.Single(errors);
        Assert.Equal(ErrorCode.ErrorUnexpectedCharacter, error.Code);
    }

    private static SyntaxToken LexToken(string source)
    {
        return LexToken(Encoding.Latin1.GetBytes(source));
    }

    private static SyntaxToken LexToken(byte[] source)
    {
        SyntaxToken result = default;
        foreach (var token in SyntaxFactory.ParseTokens(source))
        {
            if (result.Kind == SyntaxKind.None)
            {
                result = token;
                continue;
            }

            if (token.Kind != SyntaxKind.EndOfFileToken)
            {
                Assert.Fail("More than one token was lexed: " + token);
            }
        }

        if (result.Kind == SyntaxKind.None)
        {
            Assert.Fail("No tokens were lexed");
        }

        return result;
    }
}

// <copyright file="LexerTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Text;
using OffDotNet.Pdf.CodeAnalysis.Syntax;
using Xunit;

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Parser;

public class LexerTests
{
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
    }

    private static SyntaxToken LexToken(string source)
    {
        return LexToken(Encoding.UTF8.GetBytes(source));
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

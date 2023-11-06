// <copyright file="LexerTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.InputReaders;
using OffDotNet.Pdf.CodeAnalysis.LexicalAnalysis;
using OffDotNet.Pdf.CodeAnalysis.Syntax;

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Lexer;

public class LexerTests
{
    [Fact(DisplayName = $"The integer literal value should be converted to a {nameof(SyntaxKind.NumericLiteralToken)}")]
    [Trait("Feature", "Literals")]
    public void TestIntegerValue_ShouldReturnNumericLiteralToken()
    {
        // Arrange
        const int value = 123;
        const string text = "123";

        // Act
        var token = LexToken(text);

        // Assert
        Assert.Multiple(
            () => Assert.NotEqual(default, token),
            () => Assert.Equal(SyntaxKind.NumericLiteralToken, token.Kind),
            () => Assert.Equal(text, token.Text),
            () => Assert.Equal(value, token.Value));
    }

    [Fact(DisplayName = $"The integer literal value with whitespaces should be converted to a {nameof(SyntaxKind.NumericLiteralToken)} with trivia")]
    [Trait("Feature", "Literals")]
    public void TestIntegerValue_WithTrivia_ShouldReturnNumericLiteralToken()
    {
        // Arrange
        const int expectedValue = 123;
        const string expectedText = "123";

        byte[] bytes =
        {
            (byte)CharacterKind.Null,
            (byte)CharacterKind.HorizontalTab,
            (byte)CharacterKind.LineFeed,
            (byte)CharacterKind.Digit1,
            (byte)CharacterKind.Digit2,
            (byte)CharacterKind.Digit3,
            (byte)CharacterKind.FormFeed,
            (byte)CharacterKind.CarriageReturn,
            (byte)CharacterKind.Space,
        };

        // Act
        var token = LexToken(bytes);

        // Assert
        Assert.NotEqual(default, token);
        Assert.Equal(SyntaxKind.NumericLiteralToken, token.Kind);
        Assert.Equal(expectedText, token.Text);
        Assert.Equal(expectedValue, token.Value);

        var leadingTriviaList = token.LeadingTrivia.ToArray();
        var trailingTriviaList = token.TrailingTrivia.ToArray();

        Assert.Equal(3, leadingTriviaList.Length); // \0 \t \n
        Assert.Equal(3, trailingTriviaList.Length); // \f \r \s
        Assert.All(leadingTriviaList, trivia => Assert.Equal(SyntaxKind.WhitespaceTrivia, trivia.Kind));
        Assert.All(trailingTriviaList, trivia => Assert.Equal(SyntaxKind.WhitespaceTrivia, trivia.Kind));
        Assert.Equal("\0\t\n", leadingTriviaList.ToString());
        Assert.Equal("\f\r ", trailingTriviaList.ToString());
        Assert.Equivalent(new[] { "\0", "\t", "\n" }, leadingTriviaList.Select(x => x.ToString()));
        Assert.Equivalent(new[] { "\f", "\r", " " }, trailingTriviaList.Select(x => x.ToString()));
    }

    private static SyntaxToken LexToken(string source)
    {
        return LexToken(Encoding.Latin1.GetBytes(source));
    }

    private static SyntaxToken LexToken(byte[] source)
    {
        SyntaxToken result = default;
        foreach (var token in LexerExtensions.ParseTokens(source))
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

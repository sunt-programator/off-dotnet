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
    [Fact(DisplayName = $"The numeric literal {nameof(SyntaxToken.Kind)} should be {nameof(SyntaxKind.NumericLiteralToken)}")]
    [Trait("Feature", "Literals")]
    public void TestNumericLiteral_ShouldReturnNumericLiteralToken()
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

    private static SyntaxToken LexToken(string source)
    {
        return LexToken(Encoding.ASCII.GetBytes(source));
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

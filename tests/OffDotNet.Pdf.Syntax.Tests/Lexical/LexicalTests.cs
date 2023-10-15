// <copyright file="LexicalTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.Syntax.Syntax;
using Xunit;

namespace OffDotNet.Pdf.Syntax.Tests.Lexical;

public class LexicalTests
{
    [Fact]
    [Trait("Feature", "Literals")]
    public void NumericLiteral_ShouldParseSuccessfully()
    {
        // Arrange
        const int value = 123;
        const string text = "123";

        // Act
        var token = PdfLexToken(text);

        // Assert
        Assert.NotEqual(default, token);
        Assert.Equal(SyntaxKind.NumericLiteralToken, token.Kind);
        Assert.Equal(text, token.Text);
        Assert.Equal(value, token.Value);
    }

    private static IEnumerable<SyntaxToken> PdfLexer(string text)
    {
        return SyntaxFactory.ParseTokens(text);
    }

    private static SyntaxToken PdfLexToken(string text)
    {
        SyntaxToken result = default;
        foreach (var token in PdfLexer(text))
        {
            if (result.Kind == SyntaxKind.None)
            {
                result = token;
                continue;
            }

            Assert.Fail("More than one PDF token was lexed: " + token);
        }

        if (result.Kind == SyntaxKind.None)
        {
            Assert.Fail("No tokens were lexed");
        }

        return result;
    }
}

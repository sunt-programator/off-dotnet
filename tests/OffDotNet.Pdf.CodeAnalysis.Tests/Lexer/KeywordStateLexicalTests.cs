// <copyright file="KeywordStateLexicalTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Lexer;

using OffDotNet.Pdf.CodeAnalysis.Diagnostic;
using OffDotNet.Pdf.CodeAnalysis.Lexer;
using OffDotNet.Pdf.CodeAnalysis.Syntax;

public class KeywordStateLexicalTests
{
    [Theory(DisplayName = "The keyword state must parse the PDF keyword token.")]
    [InlineData("true", SyntaxKind.TrueKeyword)]
    [InlineData("false", SyntaxKind.FalseKeyword)]
    public void KeywordState_MustParseKeywordToken(string input, SyntaxKind expectedKind)
    {
        Test(input: input, expectedKind: expectedKind, expectedText: input);
    }

    [Theory(DisplayName = $"The keyword that cannot be recognized must be parsed as {nameof(SyntaxKind.UnknownKeyword)}.")]
    [InlineData("xyztoken")]
    [InlineData("startxreflongname")]
    public void KeywordState_NotRecognized_MustParseAsUnknownKeyword(string input)
    {
        Test(
            input: input,
            expectedKind: SyntaxKind.UnknownKeyword,
            expectedText: input,
            expectedErrors: [DiagnosticCode.ERR_InvalidKeyword]);
    }

    private static void Test(
        string input,
        SyntaxKind expectedKind,
        string expectedText)
    {
        Test(input, expectedKind, expectedText, []);
    }

    private static void Test(
        string input,
        SyntaxKind expectedKind,
        string expectedText,
        IEnumerable<DiagnosticCode> expectedErrors)
    {
        // Arrange
        ReadOnlySpan<byte> text = Encoding.UTF8.GetBytes(input);
        var textWindow = text.ToTextWindow();
        using var context = new LexerContext(textWindow);

        // Act
        textWindow.StartParsingLexeme();
        context.TransitionTo(DefaultState.Instance);

        // Assert
        ref var tokenInfo = ref context.GetTokenInfo();
        Assert.NotEqual(default, tokenInfo);
        Assert.Equal(expectedKind, tokenInfo._kind);
        Assert.Equal(expectedText, tokenInfo._text);
        Assert.False(textWindow.IsLexemeMode);
        Assert.Equivalent(expectedErrors, context.Errors.Select(x => x.Code));
    }
}

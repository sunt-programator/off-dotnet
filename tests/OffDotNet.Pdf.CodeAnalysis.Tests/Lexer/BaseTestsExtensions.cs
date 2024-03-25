// <copyright file="BaseTestsExtensions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Lexer;

using OffDotNet.Pdf.CodeAnalysis.Diagnostic;
using OffDotNet.Pdf.CodeAnalysis.Lexer;
using OffDotNet.Pdf.CodeAnalysis.Parser;
using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Text;
using SyntaxToken = CodeAnalysis.Syntax.InternalSyntax.SyntaxToken;

internal static class BaseTestsExtensions
{
    internal static SyntaxToken Lex(this string input)
    {
        // Arrange
        ReadOnlySpan<byte> text = Encoding.UTF8.GetBytes(input);
        var textWindow = text.ToTextWindow();
        using var context = new Lexer(textWindow);

        // Act
        var token = context.LexSyntaxToken();

        // Assert
        Assert.False(textWindow.IsLexemeMode);

        return token;
    }

    internal static SyntaxToken WithKind(this SyntaxToken token, SyntaxKind expectedKind)
    {
        Assert.Equal(expectedKind, token.Kind);
        return token;
    }

    internal static SyntaxToken WithText(this SyntaxToken token, string expectedText)
    {
        Assert.Equal(expectedText, token.Text);
        return token;
    }

    internal static SyntaxToken WithValue<T>(this SyntaxToken token, T? expectedValue)
    {
        Assert.Equal(expectedValue, token.Value);
        return token;
    }

    internal static SyntaxToken WithLeadingTrivia(
        this SyntaxToken token,
        string expectedLeadingTrivia,
        bool skipIfNull = false)
    {
        if (skipIfNull && string.IsNullOrWhiteSpace(expectedLeadingTrivia))
        {
            return token;
        }

        Assert.NotNull(token.LeadingTrivia);
        Assert.Equal(expectedLeadingTrivia, token.LeadingTrivia.ToFullString());
        return token;
    }

    internal static SyntaxToken WithTrailingTrivia(
        this SyntaxToken token,
        string expectedTrailingTrivia,
        bool skipIfNull = false)
    {
        if (skipIfNull && string.IsNullOrWhiteSpace(expectedTrailingTrivia))
        {
            return token;
        }

        Assert.NotNull(token.TrailingTrivia);
        Assert.Equal(expectedTrailingTrivia, token.TrailingTrivia.ToFullString());
        return token;
    }

    internal static SyntaxToken WithFullText(this SyntaxToken token, string expectedFullText)
    {
        Assert.Equal(expectedFullText, token.ToFullString());
        return token;
    }

    internal static SyntaxToken WithErrors(this SyntaxToken token, IEnumerable<DiagnosticCode> expectedErrors)
    {
        Assert.Equivalent(expectedErrors, token.GetDiagnostics().Select(x => x.Code));
        return token;
    }

    private static SlidingTextWindow ToTextWindow(this ReadOnlySpan<byte> text)
    {
        var sourceText = SourceText.From(text);
        return new SlidingTextWindow(sourceText);
    }
}

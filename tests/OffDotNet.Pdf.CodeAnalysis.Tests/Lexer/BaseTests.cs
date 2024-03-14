// <copyright file="BaseTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Lexer;

using System.Diagnostics.CodeAnalysis;
using OffDotNet.Pdf.CodeAnalysis.Diagnostic;
using OffDotNet.Pdf.CodeAnalysis.Lexer;
using OffDotNet.Pdf.CodeAnalysis.Syntax;

[SuppressMessage(
    "Roslynator",
    "RCS1102:Make class static",
    Justification = "This class is used as a base class for other test classes.")]
public class BaseTests
{
    protected BaseTests()
    {
    }

    internal static void Test(
        string input,
        SyntaxKind expectedKind,
        string expectedText)
    {
        Test(input, expectedKind, expectedText, []);
    }

    internal static void Test(
        string input,
        SyntaxKind expectedKind,
        string expectedText,
        IEnumerable<DiagnosticCode> expectedErrors)
    {
        Test(input, expectedKind, expectedText, tokenInfo => tokenInfo._intValue, 0, expectedErrors);
    }

    internal static void Test<T>(
        string input,
        SyntaxKind expectedKind,
        string expectedText,
        Func<LexerContext.TokenInfo, T>? getValue = null,
        T? expectedValue = default,
        IEnumerable<DiagnosticCode>? expectedErrors = null)
    {
        // Arrange
        ReadOnlySpan<byte> text = Encoding.UTF8.GetBytes(input);
        var textWindow = text.ToTextWindow();
        using var context = new LexerContext(textWindow);

        // Act
        context.TransitionTo(DefaultState.Instance);

        // Assert
        ref var tokenInfo = ref context.GetTokenInfo();
        Assert.NotEqual(default, tokenInfo);
        Assert.Equal(expectedKind, tokenInfo._kind);
        Assert.Equal(expectedText, tokenInfo._text);

        if (getValue is not null && expectedValue is not null)
        {
            Assert.Equal(expectedValue, getValue(tokenInfo));
        }

        Assert.False(textWindow.IsLexemeMode);
        Assert.Equivalent(expectedErrors ?? [], context.Errors.Select(x => x.Code));
    }
}

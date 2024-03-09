// <copyright file="NumericLiteralLexicalTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Lexer;

using OffDotNet.Pdf.CodeAnalysis.Diagnostic;
using OffDotNet.Pdf.CodeAnalysis.Lexer;
using OffDotNet.Pdf.CodeAnalysis.Syntax;

public class NumericLiteralLexicalTests
{
    [Fact(DisplayName = "The integer value must be parsed.")]
    public void NumericLiteral_MustParseIntegerValue()
    {
        Test("123", SyntaxKind.NumericLiteralToken, "123", tokenInfo => tokenInfo._intValue, 123);
    }

    [Theory(DisplayName = "The real value must be parsed.")]
    [InlineData("34.5", 34.5)]
    [InlineData("4.", 4)]
    [InlineData(".002", 0.002)]
    public void NumericLiteral_MustParseRealValue(string input, double expected)
    {
        Test(input, SyntaxKind.NumericLiteralToken, input, tokenInfo => tokenInfo._realValue, expected);
    }

    [Fact(DisplayName = "The out of range integer value must be treated as real number.")]
    public void NumericLiteral_OutOfRangeIntegerValue_MustBeTreatedAsRealNumber()
    {
        const string Input = "2147483648";
        const double Expected = 2147483648;

        Test(
            input: Input,
            expectedKind: SyntaxKind.NumericLiteralToken,
            expectedText: Input,
            getValue: tokenInfo => tokenInfo._intValue,
            expectedValue: 0);

        Test(
            input: Input,
            expectedKind: SyntaxKind.NumericLiteralToken,
            expectedText: Input,
            getValue: tokenInfo => tokenInfo._realValue,
            expectedValue: Expected);
    }

    [Fact(DisplayName = "The out of range real value must be reported as an error.")]
    public void NumericLiteral_OutOfRangeRealValue_MustBeReportedAsError()
    {
        var input = double.MaxValue.ToString("F0") + "0";

        Test(
            input: input,
            expectedKind: SyntaxKind.NumericLiteralToken,
            expectedText: input,
            getValue: tokenInfo => tokenInfo._realValue,
            expectedValue: 0,
            expectedErrors: [DiagnosticCode.ERR_RealOverflow]);
    }

    private static void Test<T>(
        string input,
        SyntaxKind expectedKind,
        string expectedText,
        Func<LexerContext.TokenInfo, T> getValue,
        T expectedValue)
    {
        Test(input, expectedKind, expectedText, getValue, expectedValue, []);
    }

    private static void Test<T>(
        string input,
        SyntaxKind expectedKind,
        string expectedText,
        Func<LexerContext.TokenInfo, T> getValue,
        T expectedValue,
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
        Assert.Equal(expectedValue, getValue(tokenInfo));
        Assert.False(textWindow.IsLexemeMode);
        Assert.Equivalent(expectedErrors, context.Errors.Select(x => x.Code));
    }
}

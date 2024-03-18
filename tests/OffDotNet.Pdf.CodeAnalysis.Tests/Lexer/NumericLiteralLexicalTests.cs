// <copyright file="NumericLiteralLexicalTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Lexer;

using OffDotNet.Pdf.CodeAnalysis.Diagnostic;
using OffDotNet.Pdf.CodeAnalysis.Syntax;

public class NumericLiteralLexicalTests
{
    [Fact(DisplayName = "The integer value must be lexed.")]
    public void NumericLiteral_MustLexIntegerValue()
    {
        "123".Lex()
            .WithKind(SyntaxKind.NumericLiteralToken)
            .WithText("123")
            .WithValue(123);
    }

    [Theory(DisplayName = "The real value must be lexed.")]
    [InlineData("34.5", 34.5)]
    [InlineData("4.", 4)]
    [InlineData(".002", 0.002)]
    public void NumericLiteral_MustLexRealValue(string input, double expected)
    {
        input.Lex()
            .WithKind(SyntaxKind.NumericLiteralToken)
            .WithText(input)
            .WithValue(expected);
    }

    [Fact(DisplayName = "The out of range integer value must be treated as real number.")]
    public void NumericLiteral_OutOfRangeIntegerValue_MustBeTreatedAsRealNumber()
    {
        const string Input = "2147483648";
        const double Expected = 2147483648;

        Input.Lex()
            .WithKind(SyntaxKind.NumericLiteralToken)
            .WithText(Input)
            .WithValue(Expected);
    }

    [Fact(DisplayName = "The out of range real value must be reported as an error.")]
    public void NumericLiteral_OutOfRangeRealValue_MustBeReportedAsError()
    {
        var input = double.MaxValue.ToString("F0") + "0";

        input.Lex()
            .WithKind(SyntaxKind.NumericLiteralToken)
            .WithText(input)
            .WithValue(0)
            .WithErrors([DiagnosticCode.ERR_RealOverflow]);
    }
}

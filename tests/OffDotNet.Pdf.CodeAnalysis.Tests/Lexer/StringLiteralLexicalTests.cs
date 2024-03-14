﻿// <copyright file="StringLiteralLexicalTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Lexer;

using OffDotNet.Pdf.CodeAnalysis.Diagnostic;
using OffDotNet.Pdf.CodeAnalysis.Syntax;

public class StringLiteralLexicalTests : BaseTests
{
    [Theory(DisplayName = "The general string literal must be parsed.")]
    [InlineData(
        "(The following is an empty string.)",
        "The following is an empty string.")]
    [InlineData(
        "()",
        "")]
    [InlineData(
        "(Strings can contain (balanced (parentheses)))",
        "Strings can contain (balanced (parentheses))")]
    [InlineData(
        "(Strings can contain\r newlines\n and\r\n such.)",
        "Strings can contain\n newlines\n and\n such.")]
    [InlineData(
        "(Strings can contain special characters ( * ! & } ^ %and so on) .)",
        "Strings can contain special characters ( * ! & } ^ %and so on) .")]
    [InlineData(
        "(String with \\r escape character)",
        "String with \r escape character")]
    [InlineData(
        "(String with \\n escape character)",
        "String with \n escape character")]
    [InlineData(
        "(String with \\t escape character)",
        "String with \t escape character")]
    [InlineData(
        "(String with \\b escape character)",
        "String with \b escape character")]
    [InlineData(
        "(String with \\f escape character)",
        "String with \f escape character")]
    [InlineData(
        "(String with \\( escape character)",
        "String with ( escape character")]
    [InlineData(
        "(String with \\) escape character)",
        "String with ) escape character")]
    [InlineData(
        "(String with non-valid\\z escape character must be ignored)",
        "String with non-valid escape character must be ignored")]
    [InlineData(
        "(Inline string with solidus and\\\r end-of-line marker)",
        "Inline string with solidus and end-of-line marker")]
    [InlineData(
        "(Inline string with solidus and\\\n end-of-line marker)",
        "Inline string with solidus and end-of-line marker")]
    [InlineData(
        "(Inline string with solidus and\\\r\n end-of-line marker)",
        "Inline string with solidus and end-of-line marker")]
    [InlineData(
        "(Treat \n end-of-line maker as \\n)",
        "Treat \n end-of-line maker as \n")]
    [InlineData(
        "(Treat \r end-of-line maker as \\n)",
        "Treat \n end-of-line maker as \n")]
    [InlineData(
        "(Treat \r\n end-of-line maker as \\n)",
        "Treat \n end-of-line maker as \n")]
    [InlineData(
        "(Treat \\105 octal character as 'E')",
        "Treat E octal character as 'E'")]
    [InlineData(
        "(Treat \\12 octal character as '\n')",
        "Treat \n octal character as '\n'")]
    [InlineData(
        "(\\400 oct should consider only 40 oct (20 hex) as 400 oct is the high-order overflow case (256 dec))",
        "\x00200 oct should consider only 40 oct (20 hex) as 400 oct is the high-order overflow case (256 dec)")]
    [InlineData(
        "(\\0053 should be treated as Control-E followed by 3)",
        "\x00053 should be treated as Control-E followed by 3")]
    [InlineData(
        "(\\053 should be treated as + sign)",
        "+ should be treated as + sign")]
    [InlineData(
        "(\\53 should be treated as + sign)",
        "+ should be treated as + sign")]
    public void StringLiteral_MustParseGeneralStringLiteral(string input, string expectedValue)
    {
        Test(
            input: input,
            expectedKind: SyntaxKind.StringLiteralToken,
            expectedText: input,
            getValue: tokenInfo => tokenInfo._stringValue,
            expectedValue: expectedValue);
    }

    [Fact(DisplayName = "String literal must ignore everything after the closing parenthesis.")]
    public void StringLiteral_MustIgnoreEverythingAfterClosingParenthesis()
    {
        Test(
            input: "(This is a string with a comment after the closing parenthesis.) % This is a comment",
            expectedKind: SyntaxKind.StringLiteralToken,
            expectedText: "(This is a string with a comment after the closing parenthesis.)",
            getValue: tokenInfo => tokenInfo._stringValue,
            expectedValue: "This is a string with a comment after the closing parenthesis.");
    }

    [Theory(DisplayName = "Test string literal errors.")]
    [InlineData(
        "(non-terminated string",
        "non-terminated string",
        DiagnosticCode.ERR_UnbalancedStringLiteral)]
    [InlineData(
        "(()",
        "()",
        DiagnosticCode.ERR_UnbalancedStringLiteral)]
    [InlineData(
        "(()skip after the closing parenthesis",
        "()skip after the closing parenthesis",
        DiagnosticCode.ERR_UnbalancedStringLiteral)]
    public void StringLiteral_MustGetError(string input, string expectedValue, DiagnosticCode expectedCode)
    {
        Test(
            input: input,
            expectedKind: SyntaxKind.StringLiteralToken,
            expectedText: input,
            getValue: tokenInfo => tokenInfo._stringValue,
            expectedValue: expectedValue,
            [expectedCode]);
    }
}
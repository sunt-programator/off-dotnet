// <copyright file="HexStringLiteralLexicalTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Lexer;

using OffDotNet.Pdf.CodeAnalysis.Diagnostic;
using OffDotNet.Pdf.CodeAnalysis.Syntax;

public class HexStringLiteralLexicalTests : BaseTests
{
    [Theory(DisplayName = "The hex string literal must be parsed.")]
    [InlineData(
        "<>",
        "")]
    [InlineData(
        "<54>",
        "T")]
    [InlineData(
        "<546573742068657820737472696E6721>",
        "Test hex string!")]
    [InlineData(
        "<54657874206F6464206E756D626572206F662068657820737472696E67205>", // missing 0 at the end
        "Text odd number of hex string P")]
    [InlineData(
        "<546\0 5787420\t 77697468\f 2077686974657370\r 61636573207368\n 6F756C642062652069676\r\n E6F726564>",
        "Text with whitespaces should be ignored")]
    public void HexStringLiteral_MustParse(string input, string expectedValue)
    {
        Test(
            input: input,
            expectedKind: SyntaxKind.HexStringLiteralToken,
            expectedText: input,
            getValue: tokenInfo => tokenInfo._stringValue,
            expectedValue: expectedValue);
    }

    [Theory(DisplayName = "Test string literal errors.")]
    [InlineData(
        "<54657874",
        "Text",
        DiagnosticCode.ERR_InvalidHexStringLiteral)]
    [InlineData(
        "<546578742 xyz !@#$  07769746820696E76616C696420686578206368617273>",
        "Text with invalid hex chars",
        DiagnosticCode.ERR_InvalidHexStringLiteral)]
    public void HexStringLiteral_MustGetError(string input, string expectedValue, DiagnosticCode expectedCode)
    {
        Test(
            input: input,
            expectedKind: SyntaxKind.HexStringLiteralToken,
            expectedText: input,
            getValue: tokenInfo => tokenInfo._stringValue,
            expectedValue: expectedValue,
            [expectedCode]);
    }
}

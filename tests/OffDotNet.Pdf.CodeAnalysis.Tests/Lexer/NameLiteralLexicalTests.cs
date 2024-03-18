// <copyright file="NameLiteralLexicalTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Lexer;

using OffDotNet.Pdf.CodeAnalysis.Diagnostic;
using OffDotNet.Pdf.CodeAnalysis.Syntax;

public class NameLiteralLexicalTests
{
    [Theory(DisplayName = "The name literal must be lexed.")]
    [InlineData("/Name1", "Name1")]
    [InlineData("/ASomewhatLongerName", "ASomewhatLongerName")]
    [InlineData("/A;Name_With-Various***Characters?", "A;Name_With-Various***Characters?")]
    [InlineData("/1.2", "1.2")]
    [InlineData("/$$", "$$")]
    [InlineData("/@pattern", "@pattern")]
    [InlineData("/.notdef", ".notdef")]
    [InlineData("/Lime#20Green", "Lime Green")]
    [InlineData("/paired#28#29parentheses", "paired()parentheses")]
    [InlineData("/The_Key_of_F#23_Minor", "The_Key_of_F#_Minor")]
    [InlineData("/A#42", "AB")]
    public void NameLiteral_MustLex(string input, string expectedValue)
    {
        input.Lex()
            .WithKind(SyntaxKind.NameLiteralToken)
            .WithText(input)
            .WithValue(expectedValue);
    }

    [Theory(DisplayName = "Test string literal errors.")]
    [InlineData("/\0", "/\0", "", DiagnosticCode.ERR_InvalidNameLiteral)]
    [InlineData("/NullChar\0", "/NullChar\0", "NullChar", DiagnosticCode.ERR_InvalidNameLiteral)]
    [InlineData("/NullChar\0123", "/NullChar\0123", "NullChar123", DiagnosticCode.ERR_InvalidNameLiteral)]
    [InlineData(
        "/#23_sign_without_2digit_hex_code#",
        "/#23_sign_without_2digit_hex_code#",
        "#_sign_without_2digit_hex_code",
        DiagnosticCode.ERR_InvalidNameLiteral)]
    [InlineData(
        "/#23_sign_without_2digit_hex_code#_xyz",
        "/#23_sign_without_2digit_hex_code#_xyz",
        "#_sign_without_2digit_hex_code_xyz",
        DiagnosticCode.ERR_InvalidNameLiteral)]
    [InlineData(
        "/#23_sign_without_2digit_hex_code#2_xyz",
        "/#23_sign_without_2digit_hex_code#2_xyz",
        "#_sign_without_2digit_hex_code2_xyz",
        DiagnosticCode.ERR_InvalidNameLiteral)]
    [InlineData(
        "/name_should_not_have_more_than_#31#32#37_characters_zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz",
        "/name_should_not_have_more_than_#31#32#37_characters_zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz",
        "name_should_not_have_more_than_127_characters_zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz",
        DiagnosticCode.ERR_InvalidNameLiteral)]
    public void NameLiteral_MustGetError(
        string input,
        string expectedText,
        string expectedValue,
        DiagnosticCode expectedCode)
    {
        input.Lex()
            .WithKind(SyntaxKind.NameLiteralToken)
            .WithText(expectedText)
            .WithValue(expectedValue)
            .WithErrors([expectedCode]);
    }

    [Theory(DisplayName = "The second name literal must not be parsed.")]
    [InlineData("/Delimit/Ignore", "/Delimit", "Delimit")]
    [InlineData("/Delimit\t/Ignore", "/Delimit", "Delimit")]
    [InlineData("/Delimit\f/Ignore", "/Delimit", "Delimit")]
    [InlineData("/Delimit\r/Ignore", "/Delimit", "Delimit")]
    [InlineData("/Delimit\n/Ignore", "/Delimit", "Delimit")]
    [InlineData("/Delimit\r\n/Ignore", "/Delimit", "Delimit")]
    [InlineData("/Delimit /Ignore", "/Delimit", "Delimit")]
    public void TwoNameLiterals_MustLexOnlyFirstToken(string input, string expectedText, string expectedValue)
    {
        input.Lex()
            .WithKind(SyntaxKind.NameLiteralToken)
            .WithText(expectedText)
            .WithValue(expectedValue);
    }
}

// <copyright file="TriviaLexicalTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Lexer;

public class TriviaLexicalTests
{
    [Theory(DisplayName = "Test the trivia in tokens.")]
    [InlineData(
        " (string with leading and trailing space) ",
        "(string with leading and trailing space)",
        " ",
        " ")]
    [InlineData(
        "   (string with leading and trailing spaces)     ",
        "(string with leading and trailing spaces)",
        "   ",
        "     ")]
    [InlineData(
        " <737472696E672077697468206C656164696E6720616E6420747261696C696E67207370616365> ",
        "<737472696E672077697468206C656164696E6720616E6420747261696C696E67207370616365>",
        " ",
        " ")]
    [InlineData(
        "   <737472696E672077697468206C656164696E6720616E6420747261696C696E67207370616365>     ",
        "<737472696E672077697468206C656164696E6720616E6420747261696C696E67207370616365>",
        "   ",
        "     ")]
    [InlineData(
        " /Name1 ",
        "/Name1",
        " ",
        " ")]
    [InlineData(
        "   /Name1     ",
        "/Name1",
        "   ",
        "     ")]
    [InlineData(
        " startxref ",
        "startxref",
        " ",
        " ")]
    [InlineData(
        "   startxref     ",
        "startxref",
        "   ",
        "     ")]
    [InlineData(
        " 123 ",
        "123",
        " ",
        " ")]
    [InlineData(
        "   123.456     ",
        "123.456",
        "   ",
        "     ")]
    [InlineData(
        " << ",
        "<<",
        " ",
        " ")]
    [InlineData(
        "   >>     ",
        ">>",
        "   ",
        "     ")]
    [InlineData(
        "\0(string with null characters in trivia)\0\0",
        "(string with null characters in trivia)",
        "\0",
        "\0\0")]
    [InlineData(
        "\t(string with horizontal tab characters in trivia)\t\t",
        "(string with horizontal tab characters in trivia)",
        "\t",
        "\t\t")]
    [InlineData(
        "\f(string with form feed characters in trivia)\f\f",
        "(string with form feed characters in trivia)",
        "\f",
        "\f\f")]
    [InlineData(
        "\r(characters after new line are ignored)\r(this is ignored)",
        "(characters after new line are ignored)",
        "\r",
        "\r",
        "\r(characters after new line are ignored)\r")]
    [InlineData(
        "\n(characters after new line are ignored)\n(this is ignored)",
        "(characters after new line are ignored)",
        "\n",
        "\n",
        "\n(characters after new line are ignored)\n")]
    [InlineData(
        "\r\n(characters after new line are ignored)\r\n(this is ignored)",
        "(characters after new line are ignored)",
        "\r\n",
        "\r\n",
        "\r\n(characters after new line are ignored)\r\n")]
    [InlineData(
        "\0\t\r\n (test mix of whitespace + new line characters) \n\r\t\0(this is ignored)",
        "(test mix of whitespace + new line characters)",
        "\0\t\r\n ",
        " \n\r\t\0",
        "\0\t\r\n (test mix of whitespace + new line characters) \n\r\t\0")]
    [InlineData(
        "% leading comment \n(characters after new line are ignored)%trailing comment (/%) blah\r\n",
        "(characters after new line are ignored)",
        "% leading comment \n",
        "%trailing comment (/%) blah\r\n",
        "% leading comment \n(characters after new line are ignored)%trailing comment (/%) blah\r\n")]
    public void TestSyntaxTrivia(
        string input,
        string expectedText,
        string expectedLeadingTrivia,
        string expectedTrailingTrivia,
        string? expectedFullText = null)
    {
        input.Lex()
            .WithText(expectedText)
            .WithLeadingTrivia(expectedLeadingTrivia, skipIfNull: true)
            .WithTrailingTrivia(expectedTrailingTrivia, skipIfNull: true)
            .WithFullText(expectedFullText ?? input);
    }
}

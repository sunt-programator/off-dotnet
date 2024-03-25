// <copyright file="SimpleTokenLexicalTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Lexer;

using OffDotNet.Pdf.CodeAnalysis.Syntax;

public class SimpleTokenLexicalTests
{
    [Theory(DisplayName = "The simple token must be lexed.")]
    [InlineData("[", SyntaxKind.LeftSquareBracketToken)]
    [InlineData("]", SyntaxKind.RightSquareBracketToken)]
    [InlineData("{", SyntaxKind.LeftCurlyBracketToken)]
    [InlineData("}", SyntaxKind.RightCurlyBracketToken)]
    [InlineData("<<", SyntaxKind.LessThanLessThanToken)]
    [InlineData(">>", SyntaxKind.GreaterThanGreaterThanToken)]
    [InlineData("+", SyntaxKind.PlusToken)]
    [InlineData("-", SyntaxKind.MinusToken)]
    public void SimpleTokenState_MustBeLexed(string input, SyntaxKind expectedKind)
    {
        input.Lex()
            .WithKind(expectedKind)
            .WithText(input);
    }
}

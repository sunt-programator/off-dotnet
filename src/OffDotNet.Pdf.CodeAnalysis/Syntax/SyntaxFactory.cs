// <copyright file="SyntaxFactory.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax;

internal static class SyntaxFactory
{
    public static IEnumerable<SyntaxToken> ParseTokens(byte[] source)
    {
        Lexer.Lexer lexer = new(source);
        var token = lexer.Lex();
        yield return new SyntaxToken(token);
    }
}

// <copyright file="SyntaxFactory.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Old.Parser;

namespace OffDotNet.Pdf.CodeAnalysis.Old.Syntax;

internal static class SyntaxFactory
{
    public static IEnumerable<SyntaxToken> ParseTokens(byte[] source, int offset = 0, int initialTokenPosition = 0)
    {
        Lexer lexer = new(source);
        var token = lexer.Lex();
        yield return new SyntaxToken(parent: null, token: token, position: 0, index: 0);
    }
}

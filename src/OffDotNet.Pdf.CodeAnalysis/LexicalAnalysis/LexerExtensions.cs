// <copyright file="LexerExtensions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Syntax;

namespace OffDotNet.Pdf.CodeAnalysis.LexicalAnalysis;

public static class LexerExtensions
{
    public static IEnumerable<SyntaxToken> ParseTokens(ReadOnlyMemory<byte> source)
    {
        using var lexer = new Lexer(source);
        while (true)
        {
            var token = lexer.NextToken();
            yield return token;

            if (token.Kind == SyntaxKind.EndOfFileToken)
            {
                break;
            }
        }
    }
}

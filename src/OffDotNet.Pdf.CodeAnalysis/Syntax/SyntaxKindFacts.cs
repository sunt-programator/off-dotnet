// <copyright file="SyntaxKindFacts.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax;

internal static class SyntaxKindFacts
{
    public static string GetText(SyntaxKind kind)
    {
        return kind switch
        {
            SyntaxKind.LeftParenthesisToken => "(",
            SyntaxKind.RightParenthesisToken => ")",
            SyntaxKind.LessThanToken => "<",
            SyntaxKind.GreaterThanToken => ">",
            SyntaxKind.LeftSquareBracketToken => "[",
            SyntaxKind.RightSquareBracketToken => "]",
            SyntaxKind.LeftCurlyBracketToken => "{",
            SyntaxKind.RightCurlyBracketToken => "}",
            SyntaxKind.SolidusToken => "/",
            SyntaxKind.PercentSignToken => "%",
            SyntaxKind.LessThanLessThanToken => "<<",
            SyntaxKind.GreaterThanGreaterThanToken => ">>",
            SyntaxKind.PlusToken => "+",
            SyntaxKind.MinusToken => "-",
            SyntaxKind.TrueKeyword => "true",
            SyntaxKind.FalseKeyword => "false",
            SyntaxKind.NullKeyword => "null",
            SyntaxKind.StartObjectKeyword => "obj",
            SyntaxKind.EndObjectKeyword => "endobj",
            SyntaxKind.IndirectReferenceKeyword => "R",
            SyntaxKind.StartStreamKeyword => "stream",
            SyntaxKind.EndStreamKeyword => "endstream",
            SyntaxKind.XrefKeyword => "xref",
            SyntaxKind.TrailerKeyword => "trailer",
            SyntaxKind.StartXrefKeyword => "startxref",
            _ => string.Empty,
        };
    }
}

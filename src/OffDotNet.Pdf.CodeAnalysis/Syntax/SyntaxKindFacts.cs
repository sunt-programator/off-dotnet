// <copyright file="SyntaxKindFacts.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax;

using InternalUtilities;

internal static class SyntaxKindFacts
{
    internal static readonly Dictionary<string, SyntaxKind> SyntaxKindDictionary = new()
    {
        { "true", SyntaxKind.TrueKeyword }, { "false", SyntaxKind.FalseKeyword },
    };

    public static string GetText(this SyntaxKind kind)
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
            SyntaxKind.XRefKeyword => "xref",
            SyntaxKind.TrailerKeyword => "trailer",
            SyntaxKind.StartXRefKeyword => "startxref",
            SyntaxKind.FreeXRefEntryKeyword => "f",
            SyntaxKind.InUseXRefEntryKeyword => "n",
            _ => string.Empty,
        };
    }

    public static object? GetValue(this SyntaxKind kind)
    {
        return kind switch
        {
            SyntaxKind.TrueKeyword => BoxedValuesUtilities.BoxedTrue,
            SyntaxKind.FalseKeyword => BoxedValuesUtilities.BoxedFalse,
            SyntaxKind.NullKeyword => null,
            _ => null,
        };
    }
}

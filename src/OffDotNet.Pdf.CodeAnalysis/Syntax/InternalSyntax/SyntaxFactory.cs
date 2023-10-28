// <copyright file="SyntaxFactory.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

internal static class SyntaxFactory
{
    internal static SyntaxToken Literal(GreenNode? leading, string text, float value, GreenNode? trailing)
    {
        return SyntaxToken.WithValue(SyntaxKind.NumericLiteralToken, leading, text, value, trailing);
    }

    internal static SyntaxToken Literal(GreenNode? leading, string text, int value, GreenNode? trailing)
    {
        return SyntaxToken.WithValue(SyntaxKind.NumericLiteralToken, leading, text, value, trailing);
    }

    internal static SyntaxToken Literal(SyntaxKind kind, GreenNode? leading, string text, string? value, GreenNode? trailing)
    {
        return SyntaxToken.WithValue(kind, leading, text, value ?? string.Empty, trailing);
    }

    internal static SyntaxToken Token(GreenNode? leading, SyntaxKind kind, GreenNode? trailing)
    {
        return SyntaxToken.Create(kind, leading, trailing);
    }

    internal static SyntaxToken BadToken(GreenNode? leading, string text, GreenNode? trailing)
    {
        return SyntaxToken.WithValue(SyntaxKind.BadToken, leading, text, text, trailing);
    }

    internal static SyntaxTrivia Comment(string text)
    {
        return SyntaxTrivia.Create(SyntaxKind.SingleLineCommentTrivia, text);
    }
}

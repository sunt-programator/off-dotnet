// <copyright file="InternalSyntaxFactory.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

internal static class InternalSyntaxFactory
{
    internal static InternalSyntaxToken Literal(string text, float value)
    {
        return WithValue(SyntaxKind.NumericLiteralToken, text, value);
    }

    internal static InternalSyntaxToken Literal(string text, int value)
    {
        return WithValue(SyntaxKind.NumericLiteralToken, text, value);
    }

    internal static InternalSyntaxToken Create(SyntaxKind kind)
    {
        return new InternalSyntaxToken(kind);
    }

    private static InternalSyntaxToken WithValue<T>(SyntaxKind kind, string text, T value)
        where T : notnull
    {
        return new InternalSyntaxToken.SyntaxTokenWithValue<T>(kind, text, value);
    }
}

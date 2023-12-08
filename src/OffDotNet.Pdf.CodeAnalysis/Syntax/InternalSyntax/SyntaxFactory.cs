// <copyright file="SyntaxFactory.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Diagnostics.CodeAnalysis;

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

internal static class SyntaxFactory
{
    public static IndirectObjectSyntax Object(LiteralExpressionSyntax objectNumber, LiteralExpressionSyntax generationNumber, SyntaxToken objectKeyword, GreenNode content, SyntaxToken endObjectKeyword)
    {
        return new IndirectObjectSyntax(SyntaxKind.IndirectObject, objectNumber, generationNumber, objectKeyword, content, endObjectKeyword);
    }

    public static IndirectReferenceSyntax IndirectReference(LiteralExpressionSyntax objectNumber, LiteralExpressionSyntax generationNumber, SyntaxToken referenceKeyword)
    {
        return new IndirectReferenceSyntax(SyntaxKind.IndirectReference, objectNumber, generationNumber, referenceKeyword);
    }

    [SuppressMessage("ReSharper", "SwitchStatementHandlesSomeKnownEnumValuesWithDefault", Justification = "The switch statement is used to validate the kind of the literal expression.")]
    public static LiteralExpressionSyntax LiteralExpression(SyntaxKind kind, SyntaxToken token)
    {
        switch (kind)
        {
            case SyntaxKind.TrueLiteralExpression:
            case SyntaxKind.FalseLiteralExpression:
            case SyntaxKind.NumericLiteralExpression:
            case SyntaxKind.StringLiteralExpression:
            case SyntaxKind.NullLiteralExpression:
                break;
            default: throw new ArgumentException($"The {nameof(kind)} must be a literal expression kind.", nameof(kind));
        }

#if DEBUG
        switch (token.Kind)
        {
            case SyntaxKind.TrueKeyword:
            case SyntaxKind.FalseKeyword:
            case SyntaxKind.NumericLiteralToken:
            case SyntaxKind.StringLiteralToken:
            case SyntaxKind.NullKeyword:
                break;
            default: throw new ArgumentException("The token kind must be a literal token.", nameof(token));
        }
#endif

        return new LiteralExpressionSyntax(kind, token);
    }

    public static SyntaxToken Token(SyntaxKind kind)
    {
        string text = kind.GetText();
        int fullWidth = text.Length;
        object? value = kind.GetValue();
        return new SyntaxToken(kind, text, value, null, null, fullWidth);
    }

    public static SyntaxToken Token(SyntaxKind kind, GreenNode? leading, GreenNode? trailing)
    {
        string text = kind.GetText();
        object? value = kind.GetValue();

        int leadingFullWidth = leading?.FullWidth ?? 0;
        int trailingFullWidth = trailing?.FullWidth ?? 0;
        int nodeFullWidth = text.Length + leadingFullWidth + trailingFullWidth;

        return new SyntaxToken(kind, text, value, leading, trailing, nodeFullWidth);
    }

    public static SyntaxToken Token<T>(SyntaxKind kind, string text, T? value, GreenNode? leading, GreenNode? trailing)
    {
        int leadingFullWidth = leading?.FullWidth ?? 0;
        int trailingFullWidth = trailing?.FullWidth ?? 0;
        int nodeFullWidth = text.Length + leadingFullWidth + trailingFullWidth;

        return new SyntaxToken(kind, text, value, leading, trailing, nodeFullWidth);
    }

    public static SyntaxTrivia Trivia(SyntaxKind kind, string text)
    {
        return new SyntaxTrivia(kind, text);
    }
}

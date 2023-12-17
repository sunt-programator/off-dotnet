// <copyright file="SyntaxFactory.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Diagnostics.CodeAnalysis;
using OffDotNet.Pdf.CodeAnalysis.Collections;

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

internal static class SyntaxFactory
{
    public static IndirectObjectSyntax Object(IndirectObjectHeaderSyntax header, GreenNode content, SyntaxToken endObjectKeyword)
    {
        GreenNode? cached = SyntaxNodeCache.TryGetNode(SyntaxKind.IndirectObject, header, content, endObjectKeyword, out int hash);
        if (cached != null)
        {
            return (IndirectObjectSyntax)cached;
        }

        IndirectObjectSyntax result = new(SyntaxKind.IndirectObject, header, content, endObjectKeyword);
        if (hash > 0)
        {
            SyntaxNodeCache.AddNode(result, hash);
        }

        return result;
    }

    public static IndirectObjectHeaderSyntax IndirectObjectHeader(LiteralExpressionSyntax objectNumber, LiteralExpressionSyntax generationNumber, SyntaxToken startObjectKeyword)
    {
        GreenNode? cached = SyntaxNodeCache.TryGetNode(SyntaxKind.IndirectObjectHeader, objectNumber, generationNumber, startObjectKeyword, out int hash);
        if (cached != null)
        {
            return (IndirectObjectHeaderSyntax)cached;
        }

        IndirectObjectHeaderSyntax result = new(SyntaxKind.IndirectObjectHeader, objectNumber, generationNumber, startObjectKeyword);
        if (hash > 0)
        {
            SyntaxNodeCache.AddNode(result, hash);
        }

        return result;
    }

    public static IndirectReferenceSyntax IndirectReference(LiteralExpressionSyntax objectNumber, LiteralExpressionSyntax generationNumber, SyntaxToken referenceKeyword)
    {
        GreenNode? cached = SyntaxNodeCache.TryGetNode(SyntaxKind.IndirectReference, objectNumber, generationNumber, referenceKeyword, out int hash);
        if (cached != null)
        {
            return (IndirectReferenceSyntax)cached;
        }

        IndirectReferenceSyntax result = new(SyntaxKind.IndirectReference, objectNumber, generationNumber, referenceKeyword);
        if (hash > 0)
        {
            SyntaxNodeCache.AddNode(result, hash);
        }

        return result;
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
            case SyntaxKind.NameLiteralExpression:
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
            case SyntaxKind.NameLiteralToken:
            case SyntaxKind.NullKeyword:
                break;
            default: throw new ArgumentException("The token kind must be a literal token.", nameof(token));
        }
#endif

        GreenNode? cached = SyntaxNodeCache.TryGetNode(kind, token, out int hash);
        if (cached != null)
        {
            return (LiteralExpressionSyntax)cached;
        }

        LiteralExpressionSyntax result = new(kind, token);
        if (hash > 0)
        {
            SyntaxNodeCache.AddNode(result, hash);
        }

        return result;
    }

    public static ArrayExpressionSyntax ArrayExpression(SyntaxToken openBracketToken, SyntaxList<ArrayElementSyntax> elements, SyntaxToken closeBracketToken)
    {
#if DEBUG
        if (openBracketToken == null)
        {
            throw new ArgumentNullException(nameof(openBracketToken), "The open bracket token must not be null.");
        }

        if (openBracketToken.Kind != SyntaxKind.LeftSquareBracketToken)
        {
            throw new ArgumentException("The open bracket token must be a left square bracket token.", nameof(openBracketToken));
        }

        if (closeBracketToken == null)
        {
            throw new ArgumentNullException(nameof(closeBracketToken), "The close bracket token must not be null.");
        }

        if (closeBracketToken.Kind != SyntaxKind.RightSquareBracketToken)
        {
            throw new ArgumentException("The close bracket token must be a right square bracket token.", nameof(closeBracketToken));
        }
#endif

        GreenNode? cached = SyntaxNodeCache.TryGetNode(SyntaxKind.ArrayExpression, openBracketToken, elements.Node, closeBracketToken, out int hash);
        if (cached != null)
        {
            return (ArrayExpressionSyntax)cached;
        }

        ArrayExpressionSyntax result = new(SyntaxKind.ArrayExpression, openBracketToken, elements.Node, closeBracketToken);
        if (hash > 0)
        {
            SyntaxNodeCache.AddNode(result, hash);
        }

        return result;
    }

    public static ArrayElementSyntax ArrayElement(ExpressionSyntax expression)
    {
#if DEBUG
        if (expression == null)
        {
            throw new ArgumentNullException(nameof(expression), "The expression must not be null.");
        }
#endif

        GreenNode? cached = SyntaxNodeCache.TryGetNode(SyntaxKind.ArrayElement, expression, out int hash);
        if (cached != null)
        {
            return (ArrayElementSyntax)cached;
        }

        ArrayElementSyntax result = new(SyntaxKind.ArrayElement, expression);
        if (hash > 0)
        {
            SyntaxNodeCache.AddNode(result, hash);
        }

        return result;
    }

    public static GreenNode List(GreenNode child)
    {
        return child;
    }

    public static GreenNode List(GreenNode child1, GreenNode child2)
    {
        GreenNode? cached = SyntaxNodeCache.TryGetNode(SyntaxKind.List, child1, child2, out int hash);
        if (cached != null)
        {
            return (SyntaxList.WithTwoChildren)cached;
        }

        SyntaxList.WithTwoChildren result = new(child1, child2);
        if (hash > 0)
        {
            SyntaxNodeCache.AddNode(result, hash);
        }

        return result;
    }

    public static GreenNode List(GreenNode child1, GreenNode child2, GreenNode child3)
    {
        GreenNode? cached = SyntaxNodeCache.TryGetNode(SyntaxKind.List, child1, child2, child3, out int hash);
        if (cached != null)
        {
            return (SyntaxList.WithThreeChildren)cached;
        }

        SyntaxList.WithThreeChildren result = new(child1, child2, child3);
        if (hash > 0)
        {
            SyntaxNodeCache.AddNode(result, hash);
        }

        return result;
    }

    public static GreenNode List(ArrayElement<GreenNode>[] children)
    {
        return children.Length < 10
            ? new SyntaxList.WithManyChildren(children)
            : new SyntaxList.WithLotsOfChildren(children);
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

    public static SyntaxToken Token(SyntaxKind kind, string text, GreenNode? leading, GreenNode? trailing)
    {
        int leadingFullWidth = leading?.FullWidth ?? 0;
        int trailingFullWidth = trailing?.FullWidth ?? 0;
        int nodeFullWidth = text.Length + leadingFullWidth + trailingFullWidth;

        return new SyntaxToken(kind, text, text, leading, trailing, nodeFullWidth);
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

// <copyright file="SyntaxFactory.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

using System.Diagnostics.CodeAnalysis;
using Collections;
using InternalSyntaxNodes;

internal static class SyntaxFactory
{
    internal static readonly SyntaxTrivia Space = Trivia(SyntaxKind.WhitespaceTrivia, " ");
    internal static readonly SyntaxTrivia CarriageReturnLineFeed = Trivia(SyntaxKind.EndOfLineTrivia, "\r\n");
    internal static readonly SyntaxTrivia CarriageReturn = Trivia(SyntaxKind.EndOfLineTrivia, "\r");
    internal static readonly SyntaxTrivia LineFeed = Trivia(SyntaxKind.EndOfLineTrivia, "\n");

    public static XRefEntryExpressionSyntax XRefEntry(LiteralExpressionSyntax offset, LiteralExpressionSyntax generationNumber, SyntaxToken entryType)
    {
#if DEBUG
        switch (entryType.Kind)
        {
            case SyntaxKind.FreeXRefEntryKeyword:
            case SyntaxKind.InUseXRefEntryKeyword:
                break;
            default: throw new ArgumentException("The entry type must be a valid xref entry type.", nameof(entryType));
        }
#endif

        var cached = SyntaxNodeCache.TryGetNode(SyntaxKind.XRefEntry, offset, generationNumber, entryType, out var hash);
        if (cached != null)
        {
            return (XRefEntryExpressionSyntax)cached;
        }

        XRefEntryExpressionSyntax result = new(SyntaxKind.XRefEntry, offset, generationNumber, entryType);
        if (hash > 0)
        {
            SyntaxNodeCache.AddNode(result, hash);
        }

        return result;
    }

    public static XRefSubSectionExpressionSyntax XRefSubSection(LiteralExpressionSyntax objectNumber, LiteralExpressionSyntax numberOfEntries, SyntaxList<XRefEntryExpressionSyntax> entries)
    {
        var cached = SyntaxNodeCache.TryGetNode(SyntaxKind.XRefSubSection, objectNumber, numberOfEntries, entries.Node, out var hash);
        if (cached != null)
        {
            return (XRefSubSectionExpressionSyntax)cached;
        }

        XRefSubSectionExpressionSyntax result = new(SyntaxKind.XRefSubSection, objectNumber, numberOfEntries, entries.Node);
        if (hash > 0)
        {
            SyntaxNodeCache.AddNode(result, hash);
        }

        return result;
    }

    public static XRefSectionExpressionSyntax XRefSection(SyntaxToken xRefKeyword, SyntaxList<XRefSubSectionExpressionSyntax> subSections)
    {
        var cached = SyntaxNodeCache.TryGetNode(SyntaxKind.XRefSection, xRefKeyword, subSections.Node, out var hash);
        if (cached != null)
        {
            return (XRefSectionExpressionSyntax)cached;
        }

        XRefSectionExpressionSyntax result = new(SyntaxKind.XRefSection, xRefKeyword, subSections.Node);
        if (hash > 0)
        {
            SyntaxNodeCache.AddNode(result, hash);
        }

        return result;
    }

    public static IndirectObjectSyntax Object(
        LiteralExpressionSyntax objectNumber,
        LiteralExpressionSyntax generationNumber,
        SyntaxToken startObjectKeyword,
        GreenNode content,
        SyntaxToken endObjectKeyword)
    {
        return new IndirectObjectSyntax(SyntaxKind.IndirectObject, objectNumber, generationNumber, startObjectKeyword, content, endObjectKeyword);
    }

    public static IndirectReferenceSyntax IndirectReference(LiteralExpressionSyntax objectNumber, LiteralExpressionSyntax generationNumber, SyntaxToken referenceKeyword)
    {
        var cached = SyntaxNodeCache.TryGetNode(SyntaxKind.IndirectReference, objectNumber, generationNumber, referenceKeyword, out var hash);
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

    public static FileTrailerSyntax FileTrailer(SyntaxToken trailerKeyword, DictionaryExpressionSyntax trailerDictionary, SyntaxToken startXRefKeyword, LiteralExpressionSyntax byteOffset)
    {
        return new FileTrailerSyntax(SyntaxKind.FileTrailer, trailerKeyword, trailerDictionary, startXRefKeyword, byteOffset);
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

        var cached = SyntaxNodeCache.TryGetNode(kind, token, out var hash);
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

    public static ArrayExpressionSyntax ArrayExpression(SyntaxToken openToken, SyntaxList<ArrayElementSyntax> elements, SyntaxToken closeToken)
    {
#if DEBUG
        var isValidArray = openToken.Kind == SyntaxKind.LeftSquareBracketToken && closeToken.Kind == SyntaxKind.RightSquareBracketToken;

        if (!isValidArray)
        {
            throw new ArgumentException("The open and close bracket tokens must be valid for array.", nameof(openToken));
        }
#endif

        var cached = SyntaxNodeCache.TryGetNode(SyntaxKind.ArrayExpression, openToken, elements.Node, closeToken, out var hash);
        if (cached != null)
        {
            return (ArrayExpressionSyntax)cached;
        }

        ArrayExpressionSyntax result = new(SyntaxKind.ArrayExpression, openToken, elements.Node, closeToken);
        if (hash > 0)
        {
            SyntaxNodeCache.AddNode(result, hash);
        }

        return result;
    }

    public static DictionaryExpressionSyntax DictionaryExpression(SyntaxToken openToken, SyntaxList<DictionaryElementSyntax> elements, SyntaxToken closeToken)
    {
#if DEBUG
        var isValidDictionary = openToken.Kind == SyntaxKind.LessThanLessThanToken && closeToken.Kind == SyntaxKind.GreaterThanGreaterThanToken;

        if (!isValidDictionary)
        {
            throw new ArgumentException("The open and close bracket tokens must be valid for dictionary.", nameof(openToken));
        }
#endif

        var cached = SyntaxNodeCache.TryGetNode(SyntaxKind.DictionaryExpression, openToken, elements.Node, closeToken, out var hash);
        if (cached != null)
        {
            return (DictionaryExpressionSyntax)cached;
        }

        DictionaryExpressionSyntax result = new(SyntaxKind.DictionaryExpression, openToken, elements.Node, closeToken);
        if (hash > 0)
        {
            SyntaxNodeCache.AddNode(result, hash);
        }

        return result;
    }

    public static ArrayElementSyntax ArrayElement(ExpressionSyntax expression)
    {
        var cached = SyntaxNodeCache.TryGetNode(SyntaxKind.ArrayElement, expression, out var hash);
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

    public static DictionaryElementSyntax DictionaryElement(LiteralExpressionSyntax key, ExpressionSyntax value)
    {
#if DEBUG
        if (key.Kind != SyntaxKind.NameLiteralExpression)
        {
            throw new ArgumentException("The key must be a name literal expression.", nameof(key));
        }
#endif

        var cached = SyntaxNodeCache.TryGetNode(SyntaxKind.DictionaryElement, key, value, out var hash);
        if (cached != null)
        {
            return (DictionaryElementSyntax)cached;
        }

        DictionaryElementSyntax result = new(SyntaxKind.DictionaryElement, key, value);
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
        var cached = SyntaxNodeCache.TryGetNode(SyntaxKind.List, child1, child2, out var hash);
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
        var cached = SyntaxNodeCache.TryGetNode(SyntaxKind.List, child1, child2, child3, out var hash);
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
        var text = kind.GetText();
        var fullWidth = text.Length;
        var value = kind.GetValue();
        return new SyntaxToken(kind, text, value, null, null, fullWidth);
    }

    public static SyntaxToken Token(SyntaxKind kind, GreenNode? leading, GreenNode? trailing)
    {
        var text = kind.GetText();
        var value = kind.GetValue();

        var leadingFullWidth = leading?.FullWidth ?? 0;
        var trailingFullWidth = trailing?.FullWidth ?? 0;
        var nodeFullWidth = text.Length + leadingFullWidth + trailingFullWidth;

        return new SyntaxToken(kind, text, value, leading, trailing, nodeFullWidth);
    }

    public static SyntaxToken Token(SyntaxKind kind, string text, GreenNode? leading, GreenNode? trailing)
    {
        var leadingFullWidth = leading?.FullWidth ?? 0;
        var trailingFullWidth = trailing?.FullWidth ?? 0;
        var nodeFullWidth = text.Length + leadingFullWidth + trailingFullWidth;

        return new SyntaxToken(kind, text, text, leading, trailing, nodeFullWidth);
    }

    public static SyntaxToken Token<T>(SyntaxKind kind, string text, T? value, GreenNode? leading, GreenNode? trailing)
    {
        var leadingFullWidth = leading?.FullWidth ?? 0;
        var trailingFullWidth = trailing?.FullWidth ?? 0;
        var nodeFullWidth = text.Length + leadingFullWidth + trailingFullWidth;

        return new SyntaxToken(kind, text, value, leading, trailing, nodeFullWidth);
    }

    public static SyntaxTrivia Trivia(SyntaxKind kind, string text)
    {
        return new SyntaxTrivia(kind, text);
    }
}

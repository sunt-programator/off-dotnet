// <copyright file="DictionaryExpressionSyntax.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

internal sealed class DictionaryExpressionSyntax : CollectionExpressionSyntax
{
    internal DictionaryExpressionSyntax(SyntaxKind kind, SyntaxToken openToken, GreenNode? elements, SyntaxToken closeToken)
        : base(kind, openToken, elements, closeToken)
    {
    }
}
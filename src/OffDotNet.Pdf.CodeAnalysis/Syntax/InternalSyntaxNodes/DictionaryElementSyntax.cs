// <copyright file="DictionaryElementSyntax.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

internal sealed class DictionaryElementSyntax : CollectionElementSyntax
{
    internal DictionaryElementSyntax(SyntaxKind kind, LiteralExpressionSyntax key, ExpressionSyntax value)
        : base(kind)
    {
        this.Key = key;
        this.Value = value;
        this.SlotCount = 2;
        this.FullWidth = this.Key.FullWidth + this.Value.FullWidth;
    }

    public LiteralExpressionSyntax Key { get; }

    public ExpressionSyntax Value { get; }

    internal override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => this.Key,
            1 => this.Value,
            _ => null,
        };
    }
}

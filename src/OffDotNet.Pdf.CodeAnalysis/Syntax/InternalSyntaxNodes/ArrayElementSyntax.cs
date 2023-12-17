// <copyright file="ArrayElementSyntax.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

internal sealed class ArrayElementSyntax : CollectionElementSyntax
{
    internal ArrayElementSyntax(SyntaxKind kind, ExpressionSyntax expression)
        : base(kind)
    {
        this.Expression = expression;
        this.SlotCount = 1;
        this.FullWidth = this.Expression.FullWidth;
    }

    public ExpressionSyntax Expression { get; }

    internal override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => this.Expression,
            _ => null,
        };
    }
}

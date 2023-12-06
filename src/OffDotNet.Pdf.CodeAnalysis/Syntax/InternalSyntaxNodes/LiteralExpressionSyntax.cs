// <copyright file="LiteralExpressionSyntax.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

internal sealed class LiteralExpressionSyntax : GreenNode
{
    internal LiteralExpressionSyntax(SyntaxKind kind, SyntaxToken token)
        : base(kind, token.FullWidth)
    {
        this.Token = token;
        this.SlotCount = 1;
    }

    public SyntaxToken Token { get; }

    internal override GreenNode? GetSlot(int index)
    {
        return index == 0 ? this.Token : null;
    }
}

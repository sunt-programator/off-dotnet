// <copyright file="ArrayExpressionSyntax.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

internal sealed class ArrayExpressionSyntax : ExpressionSyntax
{
    internal ArrayExpressionSyntax(SyntaxKind kind, SyntaxToken openBracketToken, GreenNode? elements, SyntaxToken closeBracketToken)
        : base(kind)
    {
        this.OpenBracketToken = openBracketToken;
        this.Elements = elements;
        this.CloseBracketToken = closeBracketToken;
        this.SlotCount = 3;
        this.FullWidth = this.OpenBracketToken.FullWidth + this.Elements?.FullWidth ?? 0 + this.CloseBracketToken.FullWidth;
    }

    public SyntaxToken OpenBracketToken { get; }

    public GreenNode? Elements { get; }

    public SyntaxToken CloseBracketToken { get; }

    internal override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => this.OpenBracketToken,
            1 => this.Elements,
            2 => this.CloseBracketToken,
            _ => null,
        };
    }
}

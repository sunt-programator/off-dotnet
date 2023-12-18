// <copyright file="XRefSectionExpressionSyntax.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

internal sealed class XRefSectionExpressionSyntax : GreenNode
{
    internal XRefSectionExpressionSyntax(SyntaxKind kind, SyntaxToken xRefKeyword, GreenNode? subSections)
        : base(kind)
    {
        this.XRefKeyword = xRefKeyword;
        this.SubSections = subSections;
        this.SlotCount = 2;
        this.FullWidth = this.XRefKeyword.FullWidth + this.SubSections?.FullWidth ?? 0;
    }

    public SyntaxToken XRefKeyword { get; }

    public GreenNode? SubSections { get; }

    internal override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => this.XRefKeyword,
            1 => this.SubSections,
            _ => null,
        };
    }
}

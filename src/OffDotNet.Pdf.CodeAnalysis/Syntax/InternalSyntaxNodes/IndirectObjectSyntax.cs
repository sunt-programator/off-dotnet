// <copyright file="IndirectObjectSyntax.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

internal sealed class IndirectObjectSyntax : GreenNode
{
    internal IndirectObjectSyntax(SyntaxKind kind, IndirectObjectHeaderSyntax header, GreenNode content, SyntaxToken endObjectKeyword)
        : base(kind)
    {
        this.Header = header;
        this.Content = content;
        this.EndObjectKeyword = endObjectKeyword;
        this.SlotCount = 3;
        this.FullWidth = this.Header.FullWidth + this.Content.FullWidth + this.EndObjectKeyword.FullWidth;
    }

    public IndirectObjectHeaderSyntax Header { get; }

    public GreenNode Content { get; }

    public SyntaxToken EndObjectKeyword { get; }

    internal override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => this.Header,
            1 => this.Content,
            2 => this.EndObjectKeyword,
            _ => null,
        };
    }
}

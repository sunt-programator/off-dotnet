// <copyright file="IndirectObjectSyntax.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

using Diagnostic;

internal sealed class IndirectObjectSyntax : GreenNode
{
    internal IndirectObjectSyntax(
        SyntaxKind kind,
        LiteralExpressionSyntax objectNumber,
        LiteralExpressionSyntax generationNumber,
        SyntaxToken startObjectKeyword,
        GreenNode content,
        SyntaxToken endObjectKeyword,
        DiagnosticInfo[]? diagnostics = null)
        : base(kind, diagnostics)
    {
        this.ObjectNumber = objectNumber;
        this.GenerationNumber = generationNumber;
        this.StartObjectKeyword = startObjectKeyword;
        this.Content = content;
        this.EndObjectKeyword = endObjectKeyword;
        this.SlotCount = 5;
        this.FullWidth = this.ObjectNumber.FullWidth + this.GenerationNumber.FullWidth + this.StartObjectKeyword.FullWidth + this.Content.FullWidth + this.EndObjectKeyword.FullWidth;
    }

    public LiteralExpressionSyntax ObjectNumber { get; }

    public LiteralExpressionSyntax GenerationNumber { get; }

    public SyntaxToken StartObjectKeyword { get; }

    public GreenNode Content { get; }

    public SyntaxToken EndObjectKeyword { get; }

    /// <inheritdoc/>
    internal override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => this.ObjectNumber,
            1 => this.GenerationNumber,
            2 => this.StartObjectKeyword,
            3 => this.Content,
            4 => this.EndObjectKeyword,
            _ => null,
        };
    }

    /// <inheritdoc cref="GreenNode.SetDiagnostics"/>
    internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new IndirectObjectSyntax(this.Kind, this.ObjectNumber, this.GenerationNumber, this.StartObjectKeyword, this.Content, this.EndObjectKeyword, diagnostics);
    }
}

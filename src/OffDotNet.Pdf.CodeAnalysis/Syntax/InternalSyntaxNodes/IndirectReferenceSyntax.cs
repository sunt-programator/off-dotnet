// <copyright file="IndirectReferenceSyntax.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntaxNodes;

using OffDotNet.Pdf.CodeAnalysis.Diagnostic;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

internal sealed class IndirectReferenceSyntax : ExpressionSyntax
{
    internal IndirectReferenceSyntax(
        SyntaxKind kind,
        LiteralExpressionSyntax objectNumber,
        LiteralExpressionSyntax generationNumber,
        InternalSyntax.SyntaxToken referenceKeyword,
        DiagnosticInfo[]? diagnostics = null)
        : base(kind, diagnostics)
    {
        this.ObjectNumber = objectNumber;
        this.GenerationNumber = generationNumber;
        this.ReferenceKeyword = referenceKeyword;
        this.SlotCount = 3;
        this.FullWidth = this.ObjectNumber.FullWidth + this.GenerationNumber.FullWidth + this.ReferenceKeyword.FullWidth;
    }

    public LiteralExpressionSyntax ObjectNumber { get; }

    public LiteralExpressionSyntax GenerationNumber { get; }

    public InternalSyntax.SyntaxToken ReferenceKeyword { get; }

    /// <inheritdoc/>
    internal override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => this.ObjectNumber,
            1 => this.GenerationNumber,
            2 => this.ReferenceKeyword,
            _ => null,
        };
    }

    /// <inheritdoc cref="GreenNode.SetDiagnostics"/>
    internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new IndirectReferenceSyntax(this.Kind, this.ObjectNumber, this.GenerationNumber, this.ReferenceKeyword, diagnostics);
    }
}

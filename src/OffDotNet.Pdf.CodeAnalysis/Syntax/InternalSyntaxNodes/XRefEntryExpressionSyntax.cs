// <copyright file="XRefEntryExpressionSyntax.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Diagnostic;

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

internal sealed class XRefEntryExpressionSyntax : GreenNode
{
    internal XRefEntryExpressionSyntax(SyntaxKind kind, LiteralExpressionSyntax offset, LiteralExpressionSyntax generationNumber, SyntaxToken entryType, DiagnosticInfo[]? diagnostics = null)
        : base(kind, diagnostics)
    {
        this.Offset = offset;
        this.GenerationNumber = generationNumber;
        this.EntryType = entryType;
        this.SlotCount = 3;
        this.FullWidth = this.Offset.FullWidth + this.GenerationNumber.FullWidth + this.EntryType.FullWidth;
    }

    public LiteralExpressionSyntax Offset { get; }

    public LiteralExpressionSyntax GenerationNumber { get; }

    public SyntaxToken EntryType { get; }

    internal override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => this.Offset,
            1 => this.GenerationNumber,
            2 => this.EntryType,
            _ => null,
        };
    }

    /// <inheritdoc cref="GreenNode.SetDiagnostics"/>
    internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new XRefEntryExpressionSyntax(this.Kind, this.Offset, this.GenerationNumber, this.EntryType, diagnostics);
    }
}

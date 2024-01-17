// <copyright file="XRefSubSectionExpressionSyntax.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Diagnostic;

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

internal sealed class XRefSubSectionExpressionSyntax : GreenNode
{
    internal XRefSubSectionExpressionSyntax(SyntaxKind kind, LiteralExpressionSyntax objectNumber, LiteralExpressionSyntax numberOfEntries, GreenNode? entries, DiagnosticInfo[]? diagnostics = null)
        : base(kind, diagnostics)
    {
        this.ObjectNumber = objectNumber;
        this.NumberOfEntries = numberOfEntries;
        this.Entries = entries;
        this.SlotCount = 3;
        this.FullWidth = this.ObjectNumber.FullWidth + this.NumberOfEntries.FullWidth + this.Entries?.FullWidth ?? 0;
    }

    public LiteralExpressionSyntax ObjectNumber { get; }

    public LiteralExpressionSyntax NumberOfEntries { get; }

    public GreenNode? Entries { get; }

    internal override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => this.ObjectNumber,
            1 => this.NumberOfEntries,
            2 => this.Entries,
            _ => null,
        };
    }

    /// <inheritdoc cref="GreenNode.SetDiagnostics"/>
    internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new XRefSubSectionExpressionSyntax(this.Kind, this.ObjectNumber, this.NumberOfEntries, this.Entries, diagnostics);
    }
}

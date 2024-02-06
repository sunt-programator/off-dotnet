// <copyright file="ArrayElementSyntax.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

using OffDotNet.Pdf.CodeAnalysis.Diagnostic;

internal sealed class ArrayElementSyntax : CollectionElementSyntax
{
    internal ArrayElementSyntax(SyntaxKind kind, ExpressionSyntax expression, DiagnosticInfo[]? diagnostics = null)
        : base(kind, diagnostics)
    {
        this.Expression = expression;
        this.SlotCount = 1;
        this.FullWidth = this.Expression.FullWidth;
    }

    public ExpressionSyntax Expression { get; }

    /// <inheritdoc cref="GreenNode.SetDiagnostics"/>
    internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new ArrayElementSyntax(this.Kind, this.Expression, diagnostics);
    }

    /// <inheritdoc/>
    internal override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => this.Expression,
            _ => null,
        };
    }
}

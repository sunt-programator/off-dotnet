// <copyright file="DictionaryElementSyntax.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

using Diagnostic;

internal sealed class DictionaryElementSyntax : CollectionElementSyntax
{
    internal DictionaryElementSyntax(SyntaxKind kind, LiteralExpressionSyntax key, ExpressionSyntax value, DiagnosticInfo[]? diagnostics = null)
        : base(kind, diagnostics)
    {
        this.Key = key;
        this.Value = value;
        this.SlotCount = 2;
        this.FullWidth = this.Key.FullWidth + this.Value.FullWidth;
    }

    public LiteralExpressionSyntax Key { get; }

    public ExpressionSyntax Value { get; }

    /// <inheritdoc/>
    internal override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => this.Key,
            1 => this.Value,
            _ => null,
        };
    }

    /// <inheritdoc cref="GreenNode.SetDiagnostics"/>
    internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new DictionaryElementSyntax(this.Kind, this.Key, this.Value, diagnostics);
    }
}

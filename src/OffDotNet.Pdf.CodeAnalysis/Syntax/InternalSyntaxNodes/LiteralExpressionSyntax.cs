// <copyright file="LiteralExpressionSyntax.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Diagnostic;

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

internal sealed class LiteralExpressionSyntax : ExpressionSyntax
{
    internal LiteralExpressionSyntax(SyntaxKind kind, SyntaxToken token, DiagnosticInfo[]? diagnostics = null)
        : base(kind, diagnostics)
    {
        this.Token = token;
        this.SlotCount = 1;
        this.FullWidth = token.FullWidth;
    }

    public SyntaxToken Token { get; }

    internal override GreenNode? GetSlot(int index)
    {
        return index == 0 ? this.Token : null;
    }

    /// <inheritdoc cref="GreenNode.SetDiagnostics"/>
    internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new LiteralExpressionSyntax(this.Kind, this.Token, diagnostics);
    }
}

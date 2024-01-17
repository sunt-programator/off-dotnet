// <copyright file="ArrayExpressionSyntax.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Diagnostic;

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

internal sealed class ArrayExpressionSyntax : CollectionExpressionSyntax
{
    internal ArrayExpressionSyntax(SyntaxKind kind, SyntaxToken openToken, GreenNode? elements, SyntaxToken closeToken, DiagnosticInfo[]? diagnostics = null)
        : base(kind, openToken, elements, closeToken, diagnostics)
    {
    }

    /// <inheritdoc cref="GreenNode.SetDiagnostics"/>
    internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new ArrayExpressionSyntax(this.Kind, this.OpenToken, this.Elements, this.CloseToken, diagnostics);
    }
}

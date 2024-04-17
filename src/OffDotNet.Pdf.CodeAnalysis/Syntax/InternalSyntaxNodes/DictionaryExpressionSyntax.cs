// <copyright file="DictionaryExpressionSyntax.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntaxNodes;

using OffDotNet.Pdf.CodeAnalysis.Diagnostic;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

internal sealed class DictionaryExpressionSyntax : CollectionExpressionSyntax
{
    internal DictionaryExpressionSyntax(SyntaxKind kind, InternalSyntax.SyntaxToken openToken, GreenNode? elements, InternalSyntax.SyntaxToken closeToken, DiagnosticInfo[]? diagnostics = null)
        : base(kind, openToken, elements, closeToken, diagnostics)
    {
    }

    /// <inheritdoc/>
    internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new DictionaryExpressionSyntax(this.Kind, this.OpenToken, this.Elements, this.CloseToken, diagnostics);
    }
}

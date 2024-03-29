﻿// <copyright file="CollectionExpressionSyntax.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

using Diagnostic;

internal abstract class CollectionExpressionSyntax : ExpressionSyntax
{
    protected CollectionExpressionSyntax(SyntaxKind kind, SyntaxToken openToken, GreenNode? elements, SyntaxToken closeToken, DiagnosticInfo[]? diagnostics = null)
        : base(kind, diagnostics)
    {
        this.OpenToken = openToken;
        this.Elements = elements;
        this.CloseToken = closeToken;
        this.SlotCount = 3;
        this.FullWidth = this.OpenToken.FullWidth + this.Elements?.FullWidth ?? 0 + this.CloseToken.FullWidth;
    }

    public SyntaxToken OpenToken { get; }

    public GreenNode? Elements { get; }

    public SyntaxToken CloseToken { get; }

    /// <inheritdoc/>
    internal override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => this.OpenToken,
            1 => this.Elements,
            2 => this.CloseToken,
            _ => null,
        };
    }
}

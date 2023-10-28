// <copyright file="GreenNode.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Runtime.CompilerServices;
using System.Text;
using OffDotNet.Pdf.CodeAnalysis.Errors;

namespace OffDotNet.Pdf.CodeAnalysis.Syntax;

internal abstract class GreenNode
{
    private static readonly ConditionalWeakTable<GreenNode, DiagnosticInfo[]> DiagnosticsTable = new();

    protected GreenNode(SyntaxKind kind, int fullWidth, DiagnosticInfo[]? diagnostics)
    {
        this.Kind = kind;
        this.FullWidth = fullWidth;

        if (diagnostics?.Length > 0)
        {
            DiagnosticsTable.Add(this, diagnostics);
        }
    }

    public SyntaxKind Kind { get; }

    public virtual object? Value => string.Empty;

    public int FullWidth { get; }

    public virtual GreenNode? LeadingTrivia => null;

    public override string ToString()
    {
        return string.Empty;
    }

    public virtual string ToFullString()
    {
        StringBuilder stringBuilder = new();

        if (this.LeadingTrivia != null)
        {
            stringBuilder.Append(this.LeadingTrivia);
        }

        return stringBuilder.Append(this).ToString();
    }

    internal DiagnosticInfo[] GetDiagnostics()
    {
        return DiagnosticsTable.TryGetValue(this, out var diagnostics) ? diagnostics : Array.Empty<DiagnosticInfo>();
    }

    internal abstract GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics);
}

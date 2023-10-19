// <copyright file="SyntaxToken.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Runtime.CompilerServices;
using OffDotNet.Pdf.CodeAnalysis.Errors;

namespace OffDotNet.Pdf.CodeAnalysis.Syntax;

public class SyntaxToken
{
    private static readonly ConditionalWeakTable<SyntaxToken, DiagnosticInfo[]> DiagnosticsTable = new();

    internal SyntaxToken()
    {
        this.Text = string.Empty;
    }

    internal SyntaxToken(SyntaxKind kind, string text, object? value)
    {
        this.Kind = kind;
        this.Text = text;
        this.Value = value;
    }

    public SyntaxKind Kind { get; }

    public string Text { get; }

    public object? Value { get; }

    internal SyntaxToken SetDiagnostics(DiagnosticInfo[] diagnostics)
    {
        DiagnosticsTable.Add(this, diagnostics);
        return this;
    }

    internal DiagnosticInfo[] GetDiagnostics()
    {
        return DiagnosticsTable.TryGetValue(this, out var diagnostics) ? diagnostics : Array.Empty<DiagnosticInfo>();
    }
}

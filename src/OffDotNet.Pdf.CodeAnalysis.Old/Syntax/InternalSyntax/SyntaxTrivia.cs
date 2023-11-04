// <copyright file="SyntaxTrivia.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Old.Errors;

namespace OffDotNet.Pdf.CodeAnalysis.Old.Syntax.InternalSyntax;

internal class SyntaxTrivia : GreenNode
{
    internal SyntaxTrivia(SyntaxKind kind, string text, DiagnosticInfo[]? diagnostics = null)
        : base(kind, text.Length, diagnostics)
    {
        this.Text = text;
    }

    public string Text { get; }

    public override string ToFullString()
    {
        return this.Text;
    }

    public override string ToString()
    {
        return this.Text;
    }

    internal static SyntaxTrivia Create(SyntaxKind kind, string text)
    {
        return new SyntaxTrivia(kind, text);
    }

    internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        throw new NotImplementedException();
    }
}

// <copyright file="SyntaxToken.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Errors;

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

internal partial class SyntaxToken : GreenNode
{
    internal SyntaxToken(SyntaxKind kind, int fullWidth, DiagnosticInfo[]? diagnostics = null)
        : base(kind, fullWidth, diagnostics)
    {
    }

    public virtual string Text => SyntaxFacts.GetText(this.Kind);

    public override object Value => this.Kind switch
    {
        _ => this.Text,
    };

    public override string ToString()
    {
        return this.Text;
    }

    internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new SyntaxToken(this.Kind, this.FullWidth, diagnostics);
    }

    internal static SyntaxToken Create(SyntaxKind kind, GreenNode? leading, GreenNode? trailing)
    {
        return new SyntaxTokenWithTrivia(kind, leading, trailing);
    }

    internal static SyntaxToken WithValue<T>(SyntaxKind kind, GreenNode? leading, string text, T value, GreenNode? trailing)
        where T : notnull
    {
        return new SyntaxTokenWithValueAndTrivia<T>(kind, text, value, leading, trailing);
    }
}

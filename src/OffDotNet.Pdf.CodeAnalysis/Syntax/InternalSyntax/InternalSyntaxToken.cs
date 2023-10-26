// <copyright file="InternalSyntaxToken.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Errors;

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

internal partial class InternalSyntaxToken : AbstractSyntaxToken
{
    internal InternalSyntaxToken(SyntaxKind kind)
        : base(kind, 0, null)
    {
    }

    internal InternalSyntaxToken(SyntaxKind kind, int fullWidth, DiagnosticInfo[]? diagnostics)
        : base(kind, fullWidth, diagnostics)
    {
    }

    public virtual string Text => SyntaxFacts.GetText(this.Kind);

    public override object Value => this.Kind switch
    {
        _ => this.Text,
    };

    public override string ValueText => this.Text;

    public override string ToString()
    {
        return this.Text;
    }

    internal override AbstractSyntaxToken SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new InternalSyntaxToken(this.Kind, this.FullWidth, diagnostics);
    }
}

// <copyright file="SyntaxToken.SyntaxLiteral.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Errors;

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

internal partial class SyntaxToken
{
    internal class SyntaxTokenWithValue<T> : SyntaxToken
        where T : notnull
    {
        private readonly T valueField;

        internal SyntaxTokenWithValue(SyntaxKind kind, string text, T value, DiagnosticInfo[]? diagnostics = null)
            : base(kind, text.Length, diagnostics)
        {
            this.Text = text;
            this.valueField = value;
        }

        public override string Text { get; }

        public override object Value => this.valueField;

        public override string ToString()
        {
            return this.Text;
        }

        internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
        {
            return new SyntaxTokenWithValue<T>(this.Kind, this.Text, this.valueField, diagnostics);
        }
    }
}

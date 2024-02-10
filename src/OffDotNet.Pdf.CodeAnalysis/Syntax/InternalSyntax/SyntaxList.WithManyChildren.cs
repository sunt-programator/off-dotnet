// <copyright file="SyntaxList.WithManyChildren.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

using Collections;
using Diagnostic;

internal abstract partial class SyntaxList
{
    internal class WithManyChildren : WithManyChildrenBase
    {
        public WithManyChildren(ArrayElement<GreenNode>[] children, DiagnosticInfo[]? diagnostics = null)
            : base(children, diagnostics)
        {
        }

        /// <inheritdoc/>
        internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
        {
            return new WithManyChildren(this.Children, diagnostics);
        }
    }
}

// <copyright file="SyntaxList.WithTwoChildren.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

using OffDotNet.Pdf.CodeAnalysis.Diagnostic;

internal abstract partial class SyntaxList
{
    internal class WithTwoChildren : SyntaxList
    {
        private readonly GreenNode child0;
        private readonly GreenNode child1;

        internal WithTwoChildren(GreenNode child0, GreenNode child1, DiagnosticInfo[]? diagnostics = null)
            : base(diagnostics)
        {
            this.SlotCount = 2;
            this.child0 = child0;
            this.child1 = child1;
            this.FullWidth = child0.FullWidth + child1.FullWidth;
        }

        /// <inheritdoc/>
        internal override GreenNode? GetSlot(int index)
        {
            return index switch
            {
                0 => this.child0,
                1 => this.child1,
                _ => null,
            };
        }

        /// <inheritdoc cref="GreenNode.SetDiagnostics"/>
        internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
        {
            return new WithTwoChildren(this.child0, this.child1, diagnostics);
        }
    }
}

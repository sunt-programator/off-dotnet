// <copyright file="SyntaxList.WithThreeChildren.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

using Diagnostic;

internal abstract partial class SyntaxList
{
    internal class WithThreeChildren : SyntaxList
    {
        private readonly GreenNode child0;
        private readonly GreenNode child1;
        private readonly GreenNode child2;

        internal WithThreeChildren(GreenNode child0, GreenNode child1, GreenNode child2, DiagnosticInfo[]? diagnostics = null)
            : base(diagnostics)
        {
            this.SlotCount = 3;
            this.child0 = child0;
            this.child1 = child1;
            this.child2 = child2;
            this.FullWidth = child0.FullWidth + child1.FullWidth + child2.FullWidth;
        }

        /// <inheritdoc/>
        internal override GreenNode? GetSlot(int index)
        {
            return index switch
            {
                0 => this.child0,
                1 => this.child1,
                2 => this.child2,
                _ => null,
            };
        }

        /// <inheritdoc cref="GreenNode.SetDiagnostics"/>
        internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
        {
            return new WithThreeChildren(this.child0, this.child1, this.child2, diagnostics);
        }
    }
}

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
        private readonly GreenNode _child1;
        private readonly GreenNode _child2;
        private readonly GreenNode _child3;

        internal WithThreeChildren(GreenNode child1, GreenNode child2, GreenNode child3, DiagnosticInfo[]? diagnostics = null)
            : base(diagnostics)
        {
            this.SlotCount = 3;
            _child1 = child1;
            _child2 = child2;
            _child3 = child3;
            this.FullWidth = child1.FullWidth + child2.FullWidth + child3.FullWidth;
        }

        /// <inheritdoc/>
        internal override GreenNode? GetSlot(int index)
        {
            return index switch
            {
                0 => _child1,
                1 => _child2,
                2 => _child3,
                _ => null,
            };
        }

        /// <inheritdoc cref="GreenNode.SetDiagnostics"/>
        internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
        {
            return new WithThreeChildren(_child1, _child2, _child3, diagnostics);
        }
    }
}

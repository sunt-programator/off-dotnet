// <copyright file="SyntaxList.WithTwoChildren.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

using Diagnostic;

internal abstract partial class SyntaxList
{
    internal class WithTwoChildren : SyntaxList
    {
        private readonly GreenNode _child0;
        private readonly GreenNode _child1;

        internal WithTwoChildren(GreenNode child0, GreenNode child1, DiagnosticInfo[]? diagnostics = null)
            : base(diagnostics)
        {
            this.SlotCount = 2;
            _child0 = child0;
            _child1 = child1;
            this.FullWidth = child0.FullWidth + child1.FullWidth;
        }

        /// <inheritdoc/>
        internal override GreenNode? GetSlot(int index)
        {
            return index switch
            {
                0 => _child0,
                1 => _child1,
                _ => null,
            };
        }

        /// <inheritdoc cref="GreenNode.SetDiagnostics"/>
        internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
        {
            return new WithTwoChildren(_child0, _child1, diagnostics);
        }
    }
}

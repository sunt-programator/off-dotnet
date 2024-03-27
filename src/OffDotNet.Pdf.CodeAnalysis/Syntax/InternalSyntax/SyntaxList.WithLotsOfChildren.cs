// <copyright file="SyntaxList.WithLotsOfChildren.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

using Collections;
using Diagnostic;

internal abstract partial class SyntaxList
{
    internal class WithLotsOfChildren : WithManyChildrenBase
    {
        private readonly int[] _childOffsets;

        public WithLotsOfChildren(ArrayElement<GreenNode>[] children, DiagnosticInfo[]? diagnostics = null)
            : base(children, diagnostics)
        {
            _childOffsets = CalculateOffsets(children);
        }

        /// <inheritdoc/>
        public override int GetSlotOffset(int index)
        {
            return index < _childOffsets.Length
                ? _childOffsets[index]
                : _childOffsets[^1];
        }

        /// <inheritdoc/>
        internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
        {
            return new WithLotsOfChildren(this.Children, diagnostics);
        }

        private static int[] CalculateOffsets(ArrayElement<GreenNode>[] children)
        {
            var offsets = new int[children.Length];
            var offset = 0;
            for (var i = 0; i < children.Length; i++)
            {
                offsets[i] = offset;
                offset += children[i]._value.FullWidth;
            }

            return offsets;
        }
    }
}

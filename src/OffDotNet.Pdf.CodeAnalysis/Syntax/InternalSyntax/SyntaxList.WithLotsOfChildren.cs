// <copyright file="SyntaxList.WithLotsOfChildren.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Collections;
using OffDotNet.Pdf.CodeAnalysis.Diagnostic;

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

internal abstract partial class SyntaxList
{
    internal class WithLotsOfChildren : WithManyChildrenBase
    {
        private readonly int[] childOffsets;

        public WithLotsOfChildren(ArrayElement<GreenNode>[] children, DiagnosticInfo[]? diagnostics = null)
            : base(children, diagnostics)
        {
            this.childOffsets = CalculateOffsets(children);
        }

        public override int GetSlotOffset(int index)
        {
            return index < this.childOffsets.Length
                ? this.childOffsets[index]
                : this.childOffsets[^1];
        }

        internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
        {
            return new WithLotsOfChildren(this.Children, diagnostics);
        }

        private static int[] CalculateOffsets(ArrayElement<GreenNode>[] children)
        {
            int[] offsets = new int[children.Length];
            int offset = 0;
            for (int i = 0; i < children.Length; i++)
            {
                offsets[i] = offset;
                offset += children[i].Value.FullWidth;
            }

            return offsets;
        }
    }
}

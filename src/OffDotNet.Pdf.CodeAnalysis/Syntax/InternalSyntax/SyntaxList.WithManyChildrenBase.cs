// <copyright file="SyntaxList.WithManyChildrenBase.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Collections;
using OffDotNet.Pdf.CodeAnalysis.Diagnostic;

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

internal abstract partial class SyntaxList
{
    internal abstract class WithManyChildrenBase : SyntaxList
    {
        protected WithManyChildrenBase(ArrayElement<GreenNode>[] children, DiagnosticInfo[]? diagnostics = null)
            : base(diagnostics)
        {
            this.Children = children;
            this.SlotCount = children.Length > byte.MaxValue ? byte.MaxValue : children.Length;

            for (int i = 0; i < this.Children.Length; i++)
            {
                this.FullWidth += this.Children[i].Value.FullWidth;
            }
        }

        protected ArrayElement<GreenNode>[] Children { get; }

        internal override GreenNode? GetSlot(int index)
        {
            return index >= this.Children.Length
                ? null
                : this.Children[index].Value;
        }

        protected override int GetSlotCount()
        {
            return this.Children.Length;
        }
    }
}

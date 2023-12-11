// <copyright file="SyntaxList.WithManyChildrenBase.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Collections;

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

internal abstract partial class SyntaxList
{
    internal abstract class WithManyChildrenBase : SyntaxList
    {
        private readonly ArrayElement<GreenNode>[] children;

        protected WithManyChildrenBase(ArrayElement<GreenNode>[] children)
        {
            this.children = children;
            this.SlotCount = children.Length > byte.MaxValue ? byte.MaxValue : children.Length;

            for (int i = 0; i < this.children.Length; i++)
            {
                this.FullWidth += this.children[i].Value.FullWidth;
            }
        }

        internal override GreenNode? GetSlot(int index)
        {
            return index >= this.children.Length
                ? null
                : this.children[index].Value;
        }

        protected override int GetSlotCount()
        {
            return this.children.Length;
        }
    }
}

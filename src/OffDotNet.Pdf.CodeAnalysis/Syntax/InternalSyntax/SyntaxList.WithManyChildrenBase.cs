// <copyright file="SyntaxList.WithManyChildrenBase.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

using Collections;
using Diagnostic;

internal abstract partial class SyntaxList
{
    internal abstract class WithManyChildrenBase : SyntaxList
    {
        protected WithManyChildrenBase(ArrayElement<GreenNode>[] children, DiagnosticInfo[]? diagnostics = null)
            : base(diagnostics)
        {
            this.Children = children;
            this.Count = children.Length > byte.MaxValue ? byte.MaxValue : children.Length;

            for (var i = 0; i < this.Children.Length; i++)
            {
                this.FullWidth += this.Children[i]._value.FullWidth;
            }
        }

        protected ArrayElement<GreenNode>[] Children { get; }

        /// <inheritdoc/>
        internal override GreenNode? GetSlot(int index)
        {
            return index >= this.Children.Length
                ? null
                : this.Children[index]._value;
        }

        /// <inheritdoc/>
        protected override int GetSlotCount()
        {
            return this.Children.Length;
        }
    }
}

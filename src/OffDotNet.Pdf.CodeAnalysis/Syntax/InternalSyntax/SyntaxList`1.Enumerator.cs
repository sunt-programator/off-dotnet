// <copyright file="SyntaxList`1.Enumerator.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

using System.Collections;

internal readonly partial struct SyntaxList<TNode>
    where TNode : GreenNode
{
    internal struct Enumerator : IEnumerator<TNode>
    {
        private readonly SyntaxList<TNode> list;
        private int index;

        internal Enumerator(SyntaxList<TNode> list)
        {
            this.list = list;
            this.index = -1;
        }

        /// <inheritdoc/>
        public TNode Current => this.list[this.index]!;

        /// <inheritdoc/>
        object IEnumerator.Current => this.Current;

        /// <inheritdoc/>
        public bool MoveNext()
        {
            var newIndex = this.index + 1;
            if (newIndex < this.list.Count)
            {
                this.index = newIndex;
                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        public void Reset()
        {
            this.index = -1;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Reset();
        }
    }
}

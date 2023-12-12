// <copyright file="SyntaxList`1.Enumerator.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections;

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

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

        public TNode Current => this.list[this.index]!;

        object IEnumerator.Current => this.Current;

        public bool MoveNext()
        {
            int newIndex = this.index + 1;
            if (newIndex < this.list.Count)
            {
                this.index = newIndex;
                return true;
            }

            return false;
        }

        public void Reset()
        {
            this.index = -1;
        }

        public void Dispose()
        {
            this.Reset();
        }
    }
}

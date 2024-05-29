// <copyright file="GreenNode.Enumerator.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

using System.Collections;
using System.Runtime.InteropServices;

/// <summary>Represents the enumerators for the green node.</summary>
internal abstract partial class GreenNode
{
    /// <summary>Provides an enumerator for the green node.</summary>
    [StructLayout(LayoutKind.Auto)]
    public struct Enumerator : IEnumerator<GreenNode>
    {
        private readonly GreenNode listOrSingleNode;
        private readonly int count;

        private int index;
        private GreenNode? current;

        /// <summary>Initializes a new instance of the <see cref="Enumerator"/> struct.</summary>
        /// <param name="listOrSingleNode">The green node list or single node.</param>
        public Enumerator(in GreenNode? listOrSingleNode)
        {
            this.listOrSingleNode = listOrSingleNode ?? None;
            count = this.listOrSingleNode.Count;
            Reset();
        }

        /// <inheritdoc/>
        public GreenNode Current => current ?? throw new InvalidOperationException();

        /// <inheritdoc/>
        object IEnumerator.Current => Current;

        /// <inheritdoc/>
        public bool MoveNext()
        {
            var newIndex = index + 1;

            if (listOrSingleNode.Kind is not SyntaxKind.List || newIndex >= count)
            {
                Reset();
                return false;
            }

            index = newIndex;
            current = listOrSingleNode.GetSlot(index);
            return true;
        }

        /// <inheritdoc/>
        public void Reset()
        {
            index = -1;
            current = null;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
        }
    }
}

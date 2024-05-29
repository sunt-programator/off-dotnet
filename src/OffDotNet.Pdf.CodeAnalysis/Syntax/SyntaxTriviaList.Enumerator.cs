// <copyright file="SyntaxTriviaList.Enumerator.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax;

using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using InternalSyntax;

/// <summary>Represents the enumerators for the list of syntax trivia.</summary>
public readonly partial struct SyntaxTriviaList
{
    /// <summary>Provides an enumerator for the list of syntax trivia.</summary>
    [StructLayout(LayoutKind.Auto)]
    public struct Enumerator : IEnumerator<SyntaxTrivia>
    {
        private readonly SyntaxToken token;
        private readonly GreenNode? underlyingNode;
        private readonly int basePosition;
        private readonly int baseIndex;
        private readonly int count;

        private int index;
        private GreenNode? current;
        private int position;

        /// <summary>Initializes a new instance of the <see cref="Enumerator"/> struct.</summary>
        /// <param name="list">The list of syntax trivia.</param>
        internal Enumerator(in SyntaxTriviaList list)
        {
            token = list.Token;
            underlyingNode = list.UnderlyingUnderlyingNode;
            baseIndex = list.Index;
            basePosition = position = list.Position;
            count = list.Count;
            Reset();
        }

        /// <inheritdoc/>
        public SyntaxTrivia Current => current != null
            ? new SyntaxTrivia(token, current, position, baseIndex + index)
            : throw new InvalidOperationException();

        /// <inheritdoc/>
        object IEnumerator.Current => Current;

        /// <inheritdoc/>
        public bool MoveNext()
        {
            var newIndex = index + 1;
            if (newIndex >= count)
            {
                Reset();
                return false;
            }

            index = newIndex;

            if (current != null)
            {
                position += current.FullWidth;
            }

            Debug.Assert(underlyingNode != null, "The underlying node must not be null.");
            Debug.Assert(
                underlyingNode.Kind == SyntaxKind.List || (newIndex == 0 && underlyingNode.Kind != SyntaxKind.List),
                "The underlying node must be a list or a single node.");

            current = underlyingNode.Kind == SyntaxKind.List ? underlyingNode.GetSlot(newIndex) : underlyingNode;
            return true;
        }

        /// <inheritdoc/>
        public void Reset()
        {
            index = -1;
            current = null;
            position = basePosition;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
        }
    }
}

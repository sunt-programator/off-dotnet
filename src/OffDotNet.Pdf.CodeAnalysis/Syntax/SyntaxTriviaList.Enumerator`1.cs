// <copyright file="SyntaxTriviaList.Enumerator`1.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax;

using System.Collections;

/// <summary>Represents the enumerators for the list of syntax trivia.</summary>
public readonly partial struct SyntaxTriviaList
{
    /// <summary>Provides an enumerator for the list of syntax trivia.</summary>
    internal sealed class EnumeratorImpl : IEnumerator<SyntaxTrivia>
    {
        private Enumerator _enumerator;

        /// <summary>Initializes a new instance of the <see cref="EnumeratorImpl"/> class.</summary>
        /// <param name="list">The list of syntax trivia.</param>
        internal EnumeratorImpl(in SyntaxTriviaList list)
        {
            _enumerator = new Enumerator(list);
        }

        /// <inheritdoc/>
        public SyntaxTrivia Current => _enumerator.Current;

        /// <inheritdoc/>
        object IEnumerator.Current => Current;

        /// <inheritdoc/>
        public bool MoveNext() => _enumerator.MoveNext();

        /// <inheritdoc/>
        public void Reset() => _enumerator.Reset();

        /// <inheritdoc/>
        public void Dispose() => _enumerator.Dispose();
    }
}

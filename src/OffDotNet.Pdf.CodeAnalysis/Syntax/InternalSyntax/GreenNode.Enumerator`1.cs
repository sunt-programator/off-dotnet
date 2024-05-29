// <copyright file="GreenNode.Enumerator`1.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

using System.Collections;

/// <summary>Represents the enumerators for the green node.</summary>
internal abstract partial class GreenNode
{
    /// <summary>Provides an enumerator for the green node.</summary>
    internal sealed class EnumeratorImpl : IEnumerator<GreenNode>
    {
        private Enumerator _enumerator;

        /// <summary>Initializes a new instance of the <see cref="EnumeratorImpl"/> class.</summary>
        /// <param name="listOrSingleNode">The green node list or single node.</param>
        public EnumeratorImpl(in GreenNode? listOrSingleNode)
        {
            _enumerator = new Enumerator(listOrSingleNode);
        }

        /// <inheritdoc/>
        public GreenNode Current => _enumerator.Current;

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

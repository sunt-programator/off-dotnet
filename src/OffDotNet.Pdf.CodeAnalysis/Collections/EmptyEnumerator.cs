// <copyright file="EmptyEnumerator.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Collections;

using System.Collections;

/// <summary>Represents an empty enumerator.</summary>
internal class EmptyEnumerator : IEnumerator
{
    /// <summary>Gets the instance of the empty enumerator.</summary>
    internal static readonly IEnumerator Instance = new EmptyEnumerator();

    /// <summary>Initializes a new instance of the <see cref="EmptyEnumerator"/> class.</summary>
    protected EmptyEnumerator()
    {
    }

    /// <summary>Gets the current element in the collection.</summary>
    /// <exception cref="InvalidOperationException">Not supported.</exception>
    public object Current => throw new InvalidOperationException();

    /// <summary>Advances the enumerator to the next element of the collection.</summary>
    /// <returns><see langword="false" />.</returns>
    public bool MoveNext() => false;

    /// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
    /// <exception cref="InvalidOperationException">Not supported.</exception>
    public void Reset() => throw new InvalidOperationException();
}

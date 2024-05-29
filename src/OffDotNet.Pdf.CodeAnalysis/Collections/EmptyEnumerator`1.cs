// <copyright file="EmptyEnumerator`1.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Collections;

using System.Diagnostics.CodeAnalysis;

/// <summary>Represents an empty enumerator.</summary>
/// <typeparam name="T">The type of the elements in the enumerator.</typeparam>
[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:File name should match first type name", Justification = "Verified")]
internal sealed class EmptyEnumerator<T> : EmptyEnumerator, IEnumerator<T>
{
    /// <summary>Gets the instance of the empty enumerator.</summary>
    internal static new readonly IEnumerator<T> Instance = new EmptyEnumerator<T>();

    private EmptyEnumerator()
    {
    }

    /// <summary>Gets the current element in the collection.</summary>
    /// <exception cref="InvalidOperationException">Not supported.</exception>
    public new T Current => throw new InvalidOperationException();

    /// <summary>Disposes the enumerator.</summary>
    public void Dispose()
    {
    }
}

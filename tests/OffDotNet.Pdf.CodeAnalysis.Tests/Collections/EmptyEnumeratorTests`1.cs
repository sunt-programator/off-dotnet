// <copyright file="EmptyEnumeratorTests`1.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Collections;

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using OffDotNet.Pdf.CodeAnalysis.Collections;

[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:File name should match first type name", Justification = "Verified")]
public class EmptyEnumeratorTests1
{
    [Fact(DisplayName = $"The class must implement the {nameof(IEnumerator<int>)} interface")]
    public void ImplementsIEnumerator()
    {
        // Arrange

        // Act

        // Assert
        Assert.IsAssignableFrom<IEnumerator>(EmptyEnumerator<int>.Instance);
    }

    [Fact(DisplayName = $"The {nameof(EmptyEnumerator.Current)} property must throw an {nameof(InvalidOperationException)}")]
    public void CurrentPropertyThrowsInvalidOperationException()
    {
        // Arrange

        // Act
        void Act() => _ = EmptyEnumerator<int>.Instance.Current;

        // Assert
        Assert.Throws<InvalidOperationException>(Act);
    }

    [Fact(DisplayName = "The MoveNext() method must return false")]
    public void MoveNextReturnsFalse()
    {
        // Arrange

        // Act
        var result = EmptyEnumerator<int>.Instance.MoveNext();

        // Assert
        Assert.False(result);
    }

    [Fact(DisplayName = "The Reset() method must throw an {nameof(InvalidOperationException)}")]
    public void ResetThrowsInvalidOperationException()
    {
        // Arrange

        // Act
        void Act() => EmptyEnumerator<int>.Instance.Reset();

        // Assert
        Assert.Throws<InvalidOperationException>(Act);
    }
}

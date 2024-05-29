// <copyright file="EmptyEnumeratorTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Collections;

using System.Collections;
using OffDotNet.Pdf.CodeAnalysis.Collections;

public class EmptyEnumeratorTests
{
    [Fact(DisplayName = $"The class must implement the {nameof(IEnumerator)} interface")]
    public void Class_MustImplementIEnumerator()
    {
        // Arrange

        // Act

        // Assert
        Assert.IsAssignableFrom<IEnumerator>(EmptyEnumerator.Instance);
    }

    [Fact(DisplayName = $"The {nameof(EmptyEnumerator.Current)} property must throw an {nameof(InvalidOperationException)}")]
    public void CurrentProperty_MustThrowInvalidOperationException()
    {
        // Arrange

        // Act
        void Act() => _ = EmptyEnumerator.Instance.Current;

        // Assert
        Assert.Throws<InvalidOperationException>(Act);
    }

    [Fact(DisplayName = "The MoveNext() method must return false")]
    public void MoveNextMethod_MustReturnFalse()
    {
        // Arrange

        // Act
        var result = EmptyEnumerator.Instance.MoveNext();

        // Assert
        Assert.False(result);
    }

    [Fact(DisplayName = "The Reset() method must throw an {nameof(InvalidOperationException)}")]
    public void ResetMethod_MustThrowInvalidOperationException()
    {
        // Arrange

        // Act
        void Act() => EmptyEnumerator.Instance.Reset();

        // Assert
        Assert.Throws<InvalidOperationException>(Act);
    }

    [Fact(DisplayName = "The Dispose() method must not throw an exception")]
    public void DisposeMethod_MustNotThrowException()
    {
        // Arrange

        // Act
        EmptyEnumerator<int>.Instance.Dispose();

        // Assert
        Assert.True(true);
    }
}

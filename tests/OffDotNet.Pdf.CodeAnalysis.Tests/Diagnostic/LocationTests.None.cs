// <copyright file="LocationTests.None.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Diagnostic;

using System.Diagnostics.CodeAnalysis;
using OffDotNet.Pdf.CodeAnalysis.Diagnostic;

[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:File name should match first type name", Justification = "Nested class.")]
public class LocationNoneTests
{
    private readonly Location _location = Location.NoLocation.Instance;

    [Fact(DisplayName = $"The {nameof(Location.NoLocation.Kind)} property must return {nameof(LocationKind.None)}.")]
    public void KindProperty_MustReturnNone()
    {
        // Arrange

        // Act
        var actualKind = _location.Kind;

        // Assert
        Assert.Equal(LocationKind.None, actualKind);
    }

    [Fact(DisplayName = "The GetHashCode() method must be an arbitrary number.")]
    public void GetHashCodeMethod_MustIncludeLineAndCharacter()
    {
        // Arrange
        const int ExpectedHashCode = 0x16487756;

        // Act
        var actualHashCode = _location.GetHashCode();

        // Assert
        Assert.Equal(ExpectedHashCode, actualHashCode);
    }

    [Fact(DisplayName = "The Equals() method must return true.")]
    public void EqualsMethod_MustReturnTrue()
    {
        // Arrange
        var location2 = Location.NoLocation.Instance;

        // Act
        var actualEquals1 = _location.Equals(location2);

        // Assert
        Assert.True(actualEquals1);
    }
}

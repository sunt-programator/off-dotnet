// <copyright file="FixedLocalizableStringTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Diagnostic;

using System.Diagnostics.CodeAnalysis;
using OffDotNet.Pdf.CodeAnalysis.Diagnostic;

public class FixedLocalizableStringTests
{
    [Fact(DisplayName = $"The {nameof(LocalizableString)} class must implement the {nameof(IFormattable)} interface.")]
    public void Class_MustImplementIFormattableInterface()
    {
        // Arrange
        const string value = "123";

        // Act
        LocalizableString localizableString = value;

        // Assert
        Assert.IsAssignableFrom<IFormattable>(localizableString);
    }

    [Fact(DisplayName = $"The {nameof(LocalizableString)} class must have an implicit and explicit operator to convert from string to {nameof(LocalizableString)} and vice versa.")]
    public void Class_MustHaveImplicitAndExplicitOperators()
    {
        // Arrange
        const string value = "123";

        // Act
        LocalizableString localizableString = value;
        string? actualString = (string?)localizableString;

        // Assert
        Assert.Equal(value, actualString);
    }

    [Fact(DisplayName = $"The ToString() method must return the same reference if two instances of {nameof(LocalizableString)} are created with the string.Empty.")]
    public void ToStringMethod_TwoInstancesWithStringEmpty_MustReturnTheSameReference()
    {
        // Arrange
        const string value = "";

        // Act
        LocalizableString localizableString1 = value;
        LocalizableString localizableString2 = value;
        bool areEqual = ReferenceEquals((string?)localizableString1, (string?)localizableString2);

        // Assert
        Assert.True(areEqual);
    }

    [Fact(DisplayName = $"The ToString() method must return the string value of {nameof(LocalizableString)} class.")]
    public void ToStringMethod_MustReturnStringValue()
    {
        // Arrange
        const string value = "123";

        // Act
        LocalizableString localizableString = value;
        string actualString = localizableString.ToString(null);

        // Assert
        Assert.Equal(value, actualString);
    }

    [Fact(DisplayName = "The generic ToString() method must be overriden.")]
    [SuppressMessage("Style", "CA1305: Specify IFormatProvider", Justification = "Reviewed.")]
    public void ToStringMethod_MustBeOverriden()
    {
        // Arrange
        const string value = "123";

        // Act
        LocalizableString localizableString = value;
        string actualString = localizableString.ToString();

        // Assert
        Assert.Equal(value, actualString);
    }

    [Fact(DisplayName = $"The {nameof(LocalizableString.CanThrowExceptions)} property must return false.")]
    public void CanThrowExceptionsProperty_MustReturnFalse()
    {
        // Arrange
        const string value = "123";

        // Act
        LocalizableString localizableString = value;
        bool actualCanThrowExceptions = localizableString.CanThrowExceptions;

        // Assert
        Assert.False(actualCanThrowExceptions);
    }

    [Fact(DisplayName = $"The {nameof(LocalizableString)} class must implement the {nameof(IEquatable<LocalizableString>)} interface.")]
    public void Class_MustImplementIEquatableInterface()
    {
        // Arrange
        const string value = "123";

        // Act
        LocalizableString localizableString = value;

        // Assert
        Assert.IsAssignableFrom<IEquatable<LocalizableString>>(localizableString);
    }

    [Fact(DisplayName = "The generic ToString() method must return the calculated value.")]
    public void GetHashCodeMethod_MustReturnCalculatedValue()
    {
        // Arrange
        const string value = "123";
        int expectedHashCode = value.GetHashCode();

        // Act
        LocalizableString localizableString = value;
        int actualHashCode = localizableString.GetHashCode();

        // Assert
        Assert.Equal(expectedHashCode, actualHashCode);
    }

    [Fact(DisplayName = "The Equals() method must return true.")]
    public void EqualsMethod_MustReturnTrue()
    {
        // Arrange
        const string value1 = "123";
        const string value2 = "123";

        // Act
        LocalizableString localizableString1 = value1;
        LocalizableString localizableString2 = value2;
        bool actualEquals1 = localizableString1.Equals(localizableString2);
        bool actualEquals2 = localizableString1.Equals((object?)localizableString2);
        bool actualEquals3 = localizableString1 == localizableString2;
        bool actualEquals4 = localizableString1 != localizableString2;

        // Assert
        Assert.True(actualEquals1);
        Assert.True(actualEquals2);
        Assert.True(actualEquals3);
        Assert.False(actualEquals4);
    }

    [Fact(DisplayName = "The Equals() method must return false.")]
    public void EqualsMethod_MustReturnFalse()
    {
        // Arrange
        const string value1 = "123";
        const string value2 = "456";

        // Act
        LocalizableString localizableString1 = value1;
        LocalizableString localizableString2 = value2;
        bool actualEquals1 = localizableString1.Equals(localizableString2);
        bool actualEquals2 = localizableString1.Equals((object?)localizableString2);
        bool actualEquals3 = localizableString1 == localizableString2;
        bool actualEquals4 = localizableString1 != localizableString2;

        // Assert
        Assert.False(actualEquals1);
        Assert.False(actualEquals2);
        Assert.False(actualEquals3);
        Assert.True(actualEquals4);
    }
}

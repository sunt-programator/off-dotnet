// <copyright file="TextSpanTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Text;

using OffDotNet.Pdf.CodeAnalysis.Text;

public class TextSpanTests
{
    [Fact(DisplayName = $"The {nameof(TextSpan)} constructor with negative 'start' argument must throw {nameof(ArgumentOutOfRangeException)} exception.")]
    public void Constructor_NegativeStartArgument_MustThrowArgumentOutOfRangeException()
    {
        // Arrange
        const int Start = -1;
        const int Length = 0;
        const string ExpectedParamName = "start";

        // Act
        static object? ActualTextSpanFunc()
        {
            return new TextSpan(Start, Length);
        }

        // Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(ActualTextSpanFunc);
        Assert.Equal(ExpectedParamName, exception.ParamName);
    }

    [Fact(DisplayName = $"The {nameof(TextSpan)} constructor with negative 'length' argument must throw {nameof(ArgumentOutOfRangeException)} exception.")]
    public void Constructor_NegativeLengthArgument_MustThrowArgumentOutOfRangeException()
    {
        // Arrange
        const int Start = 0;
        const int Length = -1;
        const string ExpectedParamName = "length";

        // Act
        static object? ActualTextSpanFunc()
        {
            return new TextSpan(Start, Length);
        }

        // Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(ActualTextSpanFunc);
        Assert.Equal(ExpectedParamName, exception.ParamName);
    }

    [Fact(DisplayName = $"The {nameof(TextSpan)} constructor must set the {nameof(TextSpan.Start)} property.")]
    public void Constructor_WithStartArgument_MustSetTheValueToStartProperty()
    {
        // Arrange
        const int Start = 150;
        const int Length = 0;

        TextSpan textSpan = new(Start, Length);

        // Act
        var actualStart = textSpan.Start;

        // Assert
        Assert.Equal(Start, actualStart);
    }

    [Fact(DisplayName = $"The {nameof(TextSpan)} constructor must set the {nameof(TextSpan.Length)} property.")]
    public void Constructor_WithLengthArgument_MustSetTheValueToLengthProperty()
    {
        // Arrange
        const int Start = 150;
        const int Length = 3;

        TextSpan textSpan = new(Start, Length);

        // Act
        var actualLength = textSpan.Length;

        // Assert
        Assert.Equal(Length, actualLength);
    }

    [Fact(DisplayName = $"The {nameof(TextSpan.End)} property must be computed by adding {nameof(TextSpan.Start)} and {nameof(TextSpan.Length)} properties.")]
    public void EndProperty_MustBeComputedByAddingStartAndLengthProperties()
    {
        // Arrange
        const int Start = 150;
        const int Length = 3;
        const int ExpectedEnd = 153;

        TextSpan textSpan = new(Start, Length);

        // Act
        var actualEnd = textSpan.End;

        // Assert
        Assert.Equal(ExpectedEnd, actualEnd);
    }

    [Fact(DisplayName = $"The {nameof(TextSpan.ToString)} method must return span interval.")]
    public void ToStringMethod_MustReturnSpanInterval()
    {
        // Arrange
        const int Start = 150;
        const int Length = 3;
        const string ExpectedString = "[150..153)";

        TextSpan textSpan = new(Start, Length);

        // Act
        var actualStringValue = textSpan.ToString();

        // Assert
        Assert.Equal(ExpectedString, actualStringValue);
    }

    [Fact(DisplayName = $"The {nameof(TextSpan)} struct must implement {nameof(IEquatable<TextSpan>)} interface.")]
    public void TextSpan_MustImplementIEquatableInterface()
    {
        // Arrange
        const int Start = 150;
        const int Length = 3;

        // Act
        TextSpan textSpan = new(Start, Length);

        // Assert
        Assert.IsAssignableFrom<IEquatable<TextSpan>>(textSpan);
    }

    [Fact(DisplayName = $"The Equals() method must return true when comparing two {nameof(TextSpan)} instances.")]
    public void EqualsMethod_MustReturnTrue()
    {
        // Arrange
        const int Start = 150;
        const int Length = 3;

        TextSpan textSpan1 = new(Start, Length);
        TextSpan textSpan2 = new(Start, Length);

        // Act
        var equals = textSpan1.Equals(textSpan2);

        // Assert
        Assert.True(equals);
    }

    [Fact(DisplayName = $"The Equals() method must return false when comparing two {nameof(TextSpan)} instances.")]
    public void EqualsMethod_MustReturnFalse()
    {
        // Arrange
        const int Start1 = 150;
        const int Start2 = 159;
        const int Length = 3;

        TextSpan textSpan1 = new(Start1, Length);
        TextSpan textSpan2 = new(Start2, Length);

        // Act
        var equals = textSpan1.Equals(textSpan2);

        // Assert
        Assert.False(equals);
    }

    [Fact(DisplayName = $"The Equals() method must return true when comparing two {nameof(TextSpan)} instances.")]
    public void EqualsMethod_ObjectParam_MustReturnTrue()
    {
        // Arrange
        const int Start = 150;
        const int Length = 3;

        TextSpan textSpan1 = new(Start, Length);
        object textSpan2 = new TextSpan(Start, Length);

        // Act
        var equals = textSpan1.Equals(textSpan2);

        // Assert
        Assert.True(equals);
    }

    [Fact(DisplayName = $"The Equals() method must return false when comparing two {nameof(TextSpan)} instances.")]
    public void EqualsMethod_ObjectParam_MustReturnFalse()
    {
        // Arrange
        const int Start = 150;
        const int Length = 3;

        TextSpan textSpan1 = new(Start, Length);
        object? textSpan2 = null;

        // Act
        var equals = textSpan1.Equals(textSpan2);

        // Assert
        Assert.False(equals);
    }

    [Fact(DisplayName = $"The GetHashCode() method must be computed by combining {nameof(TextSpan.Start)} and {nameof(TextSpan.Length)} values.")]
    public void GetHashCodeMethod_MustBeComputedByCombiningStartAndLengthValues()
    {
        // Arrange
        const int Start = 150;
        const int Length = 3;

        var hashCode = HashCode.Combine(Start, Length);
        TextSpan textSpan = new(Start, Length);

        // Act
        var actualHashCode = textSpan.GetHashCode();

        // Assert
        Assert.Equal(hashCode, actualHashCode);
    }

    [Fact(DisplayName = $"The '==' operator must return true when comparing two {nameof(TextSpan)} instances.")]
    public void EqualsOperator_MustReturnTrue()
    {
        // Arrange
        const int Start1 = 150;
        const int Start2 = 150;
        const int Length = 3;

        TextSpan textSpan1 = new(Start1, Length);
        TextSpan textSpan2 = new(Start2, Length);

        // Act
        var equals = textSpan1 == textSpan2;

        // Assert
        Assert.True(equals);
    }

    [Fact(DisplayName = $"The '!=' operator must return true when comparing two {nameof(TextSpan)} instances.")]
    public void NotEqualOperator_MustReturnTrue()
    {
        // Arrange
        const int Start1 = 150;
        const int Start2 = 153;
        const int Length = 3;

        TextSpan textSpan1 = new(Start1, Length);
        TextSpan textSpan2 = new(Start2, Length);

        // Act
        var equals = textSpan1 != textSpan2;

        // Assert
        Assert.True(equals);
    }

    [Fact(DisplayName = $"The {nameof(TextSpan)} struct must implement {nameof(IComparable<TextSpan>)} interface.")]
    public void TextSpan_MustImplementIComparableInterface()
    {
        // Arrange
        const int Start = 150;
        const int Length = 3;

        // Act
        TextSpan textSpan = new(Start, Length);

        // Assert
        Assert.IsAssignableFrom<IComparable<TextSpan>>(textSpan);
    }

    [Fact(DisplayName = $"The CompareTo() method must return the subtracted {nameof(TextSpan.Start)} properties.")]
    public void CompareToMethod_MustReturnSubtractedStartProperties()
    {
        // Arrange
        const int Start1 = 150;
        const int Start2 = 140;
        const int Length = 3;
        const int ExpectedCompareTo = 10;

        TextSpan textSpan1 = new(Start1, Length);
        TextSpan textSpan2 = new(Start2, Length);

        // Act
        var actualCompareTo = textSpan1.CompareTo(textSpan2);

        // Assert
        Assert.Equal(ExpectedCompareTo, actualCompareTo);
    }

    [Fact(DisplayName = $"The CompareTo() method must return the subtracted {nameof(TextSpan.Length)} properties if {nameof(TextSpan.Start)} properties are equal.")]
    public void CompareToMethod_MustReturnSubtractedLengthPropertiesIfStartPropertiesAreEqual()
    {
        // Arrange
        const int Start = 150;
        const int Length1 = 3;
        const int Length2 = 5;
        const int ExpectedCompareTo = -2;

        TextSpan textSpan1 = new(Start, Length1);
        TextSpan textSpan2 = new(Start, Length2);

        // Act
        var actualCompareTo = textSpan1.CompareTo(textSpan2);

        // Assert
        Assert.Equal(ExpectedCompareTo, actualCompareTo);
    }

    [Theory(DisplayName = $"The {nameof(TextSpan.IsEmpty)} property must return correct value.")]
    [InlineData(0, true)]
    [InlineData(1, false)]
    public void IsEmptyProperty_MustReturnCorrectValue(int length, bool isEmpty)
    {
        // Arrange
        const int Start = 150;

        TextSpan textSpan = new(Start, length);

        // Act
        var actualIsEmpty = textSpan.IsEmpty;

        // Assert
        Assert.Equal(isEmpty, actualIsEmpty);
    }
}

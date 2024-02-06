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
        const int start = -1;
        const int length = 0;
        const string expectedParamName = "start";

        // Act
        static object? ActualTextSpanFunc()
        {
            return new TextSpan(start, length);
        }

        // Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(ActualTextSpanFunc);
        Assert.Equal(expectedParamName, exception.ParamName);
    }

    [Fact(DisplayName = $"The {nameof(TextSpan)} constructor with negative 'length' argument must throw {nameof(ArgumentOutOfRangeException)} exception.")]
    public void Constructor_NegativeLengthArgument_MustThrowArgumentOutOfRangeException()
    {
        // Arrange
        const int start = 0;
        const int length = -1;
        const string expectedParamName = "length";

        // Act
        static object? ActualTextSpanFunc()
        {
            return new TextSpan(start, length);
        }

        // Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(ActualTextSpanFunc);
        Assert.Equal(expectedParamName, exception.ParamName);
    }

    [Fact(DisplayName = $"The {nameof(TextSpan)} constructor must set the {nameof(TextSpan.Start)} property.")]
    public void Constructor_WithStartArgument_MustSetTheValueToStartProperty()
    {
        // Arrange
        const int start = 150;
        const int length = 0;

        TextSpan textSpan = new(start, length);

        // Act
        int actualStart = textSpan.Start;

        // Assert
        Assert.Equal(start, actualStart);
    }

    [Fact(DisplayName = $"The {nameof(TextSpan)} constructor must set the {nameof(TextSpan.Length)} property.")]
    public void Constructor_WithLengthArgument_MustSetTheValueToLengthProperty()
    {
        // Arrange
        const int start = 150;
        const int length = 3;

        TextSpan textSpan = new(start, length);

        // Act
        int actualLength = textSpan.Length;

        // Assert
        Assert.Equal(length, actualLength);
    }

    [Fact(DisplayName = $"The {nameof(TextSpan.End)} property must be computed by adding {nameof(TextSpan.Start)} and {nameof(TextSpan.Length)} properties.")]
    public void EndProperty_MustBeComputedByAddingStartAndLengthProperties()
    {
        // Arrange
        const int start = 150;
        const int length = 3;
        const int expectedEnd = 153;

        TextSpan textSpan = new(start, length);

        // Act
        int actualEnd = textSpan.End;

        // Assert
        Assert.Equal(expectedEnd, actualEnd);
    }

    [Fact(DisplayName = $"The {nameof(TextSpan.ToString)} method must return span interval.")]
    public void ToStringMethod_MustReturnSpanInterval()
    {
        // Arrange
        const int start = 150;
        const int length = 3;
        const string expectedString = "[150..153)";

        TextSpan textSpan = new(start, length);

        // Act
        string actualStringValue = textSpan.ToString();

        // Assert
        Assert.Equal(expectedString, actualStringValue);
    }

    [Fact(DisplayName = $"The {nameof(TextSpan)} struct must implement {nameof(IEquatable<TextSpan>)} interface.")]
    public void TextSpan_MustImplementIEquatableInterface()
    {
        // Arrange
        const int start = 150;
        const int length = 3;

        // Act
        TextSpan textSpan = new(start, length);

        // Assert
        Assert.IsAssignableFrom<IEquatable<TextSpan>>(textSpan);
    }

    [Fact(DisplayName = $"The Equals() method must return true when comparing two {nameof(TextSpan)} instances.")]
    public void EqualsMethod_MustReturnTrue()
    {
        // Arrange
        const int start = 150;
        const int length = 3;

        TextSpan textSpan1 = new(start, length);
        TextSpan textSpan2 = new(start, length);

        // Act
        bool equals = textSpan1.Equals(textSpan2);

        // Assert
        Assert.True(equals);
    }

    [Fact(DisplayName = $"The Equals() method must return false when comparing two {nameof(TextSpan)} instances.")]
    public void EqualsMethod_MustReturnFalse()
    {
        // Arrange
        const int start1 = 150;
        const int start2 = 159;
        const int length = 3;

        TextSpan textSpan1 = new(start1, length);
        TextSpan textSpan2 = new(start2, length);

        // Act
        bool equals = textSpan1.Equals(textSpan2);

        // Assert
        Assert.False(equals);
    }

    [Fact(DisplayName = $"The Equals() method must return true when comparing two {nameof(TextSpan)} instances.")]
    public void EqualsMethod_ObjectParam_MustReturnTrue()
    {
        // Arrange
        const int start = 150;
        const int length = 3;

        TextSpan textSpan1 = new(start, length);
        object textSpan2 = new TextSpan(start, length);

        // Act
        bool equals = textSpan1.Equals(textSpan2);

        // Assert
        Assert.True(equals);
    }

    [Fact(DisplayName = $"The Equals() method must return false when comparing two {nameof(TextSpan)} instances.")]
    public void EqualsMethod_ObjectParam_MustReturnFalse()
    {
        // Arrange
        const int start = 150;
        const int length = 3;

        TextSpan textSpan1 = new(start, length);
        object? textSpan2 = null;

        // Act
        bool equals = textSpan1.Equals(textSpan2);

        // Assert
        Assert.False(equals);
    }

    [Fact(DisplayName = $"The GetHashCode() method must be computed by combining {nameof(TextSpan.Start)} and {nameof(TextSpan.Length)} values.")]
    public void GetHashCodeMethod_MustBeComputedByCombiningStartAndLengthValues()
    {
        // Arrange
        const int start = 150;
        const int length = 3;

        int hashCode = HashCode.Combine(start, length);
        TextSpan textSpan = new(start, length);

        // Act
        int actualHashCode = textSpan.GetHashCode();

        // Assert
        Assert.Equal(hashCode, actualHashCode);
    }

    [Fact(DisplayName = $"The '==' operator must return true when comparing two {nameof(TextSpan)} instances.")]
    public void EqualsOperator_MustReturnTrue()
    {
        // Arrange
        const int start1 = 150;
        const int start2 = 150;
        const int length = 3;

        TextSpan textSpan1 = new(start1, length);
        TextSpan textSpan2 = new(start2, length);

        // Act
        bool equals = textSpan1 == textSpan2;

        // Assert
        Assert.True(equals);
    }

    [Fact(DisplayName = $"The '!=' operator must return true when comparing two {nameof(TextSpan)} instances.")]
    public void NotEqualOperator_MustReturnTrue()
    {
        // Arrange
        const int start1 = 150;
        const int start2 = 153;
        const int length = 3;

        TextSpan textSpan1 = new(start1, length);
        TextSpan textSpan2 = new(start2, length);

        // Act
        bool equals = textSpan1 != textSpan2;

        // Assert
        Assert.True(equals);
    }

    [Fact(DisplayName = $"The {nameof(TextSpan)} struct must implement {nameof(IComparable<TextSpan>)} interface.")]
    public void TextSpan_MustImplementIComparableInterface()
    {
        // Arrange
        const int start = 150;
        const int length = 3;

        // Act
        TextSpan textSpan = new(start, length);

        // Assert
        Assert.IsAssignableFrom<IComparable<TextSpan>>(textSpan);
    }

    [Fact(DisplayName = $"The CompareTo() method must return the subtracted {nameof(TextSpan.Start)} properties.")]
    public void CompareToMethod_MustReturnSubtractedStartProperties()
    {
        // Arrange
        const int start1 = 150;
        const int start2 = 140;
        const int length = 3;
        const int expectedCompareTo = 10;

        TextSpan textSpan1 = new(start1, length);
        TextSpan textSpan2 = new(start2, length);

        // Act
        int actualCompareTo = textSpan1.CompareTo(textSpan2);

        // Assert
        Assert.Equal(expectedCompareTo, actualCompareTo);
    }

    [Fact(DisplayName = $"The CompareTo() method must return the subtracted {nameof(TextSpan.Length)} properties if {nameof(TextSpan.Start)} properties are equal.")]
    public void CompareToMethod_MustReturnSubtractedLengthPropertiesIfStartPropertiesAreEqual()
    {
        // Arrange
        const int start = 150;
        const int length1 = 3;
        const int length2 = 5;
        const int expectedCompareTo = -2;

        TextSpan textSpan1 = new(start, length1);
        TextSpan textSpan2 = new(start, length2);

        // Act
        int actualCompareTo = textSpan1.CompareTo(textSpan2);

        // Assert
        Assert.Equal(expectedCompareTo, actualCompareTo);
    }

    [Theory(DisplayName = $"The {nameof(TextSpan.IsEmpty)} property must return correct value.")]
    [InlineData(0, true)]
    [InlineData(1, false)]
    public void IsEmptyProperty_MustReturnCorrectValue(int length, bool isEmpty)
    {
        // Arrange
        const int start = 150;

        TextSpan textSpan = new(start, length);

        // Act
        bool actualIsEmpty = textSpan.IsEmpty;

        // Assert
        Assert.Equal(isEmpty, actualIsEmpty);
    }
}

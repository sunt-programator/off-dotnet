// <copyright file="LinePositionTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Diagnostics.CodeAnalysis;
using OffDotNet.Pdf.CodeAnalysis.Text;

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Text;

public class LinePositionTests
{
    [Fact(DisplayName = $"The constructor must throw an {nameof(ArgumentOutOfRangeException)} when the {nameof(LinePosition.Line)} is negative.")]
    [SuppressMessage("ReSharper", "UnusedVariable", Justification = "The variable is used in the assertion.")]
    [SuppressMessage("Minor Code Smell", "S1481:Unused local variables should be removed", Justification = "The variable is used in the assertion.")]
    public void Constructor_NegativeLine_MustThrowArgumentOutOfRangeException()
    {
        // Arrange
        const int line = -1;

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(LinePositionFunc);

        // Act
        static void LinePositionFunc()
        {
            LinePosition linePosition = new(line, 2);
        }
    }

    [Fact(DisplayName = $"The constructor must throw an {nameof(ArgumentOutOfRangeException)} when the {nameof(LinePosition.Character)} is negative.")]
    [SuppressMessage("ReSharper", "UnusedVariable", Justification = "The variable is used in the assertion.")]
    [SuppressMessage("Minor Code Smell", "S1481:Unused local variables should be removed", Justification = "The variable is used in the assertion.")]
    public void Constructor_NegativeCharacter_MustThrowArgumentOutOfRangeException()
    {
        // Arrange
        const int character = -1;

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(LinePositionFunc);

        // Act
        static void LinePositionFunc()
        {
            LinePosition linePosition = new(0, character);
        }
    }

    [Fact(DisplayName = $"The {nameof(LinePosition.Line)} property must be assigned from the constructor.")]
    public void LineProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        const int line = 1;
        LinePosition linePosition = new(line, 2);

        // Act
        int actualLine = linePosition.Line;

        // Assert
        Assert.Equal(line, actualLine);
    }

    [Fact(DisplayName = $"The {nameof(LinePosition.Character)} property must be assigned from the constructor.")]
    public void CharacterProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        const int character = 2;
        LinePosition linePosition = new(1, character);

        // Act
        int actualCharacter = linePosition.Character;

        // Assert
        Assert.Equal(character, actualCharacter);
    }

    [Fact(DisplayName = $"The GetHashCode() method must include the {nameof(LinePosition.Line)} and {nameof(LinePosition.Character)} properties.")]
    public void GetHashCodeMethod_MustIncludeLineAndCharacter()
    {
        // Arrange
        LinePosition linePosition = new(1, 2);
        int expectedHashCode = HashCode.Combine(linePosition.Line, linePosition.Character);

        // Act
        int actualHashCode = linePosition.GetHashCode();

        // Assert
        Assert.Equal(expectedHashCode, actualHashCode);
    }

    [Fact(DisplayName = $"The struct must implement the {nameof(IEquatable<LinePosition>)} interface.")]
    public void Struct_MustImplementIEquatable()
    {
        // Arrange

        // Act
        LinePosition linePosition = new(1, 2);

        // Assert
        Assert.IsAssignableFrom<IEquatable<LinePosition>>(linePosition);
    }

    [Fact(DisplayName = "The Equals() method must return true.")]
    public void EqualsMethod_MustReturnTrue()
    {
        // Arrange
        LinePosition linePosition1 = new(1, 2);
        LinePosition linePosition2 = new(1, 2);

        // Act
        bool actualEquals1 = linePosition1.Equals(linePosition2);
        bool actualEquals2 = linePosition2.Equals((object?)linePosition1);
        bool actualEquals3 = linePosition1 == linePosition2;
        bool actualEquals4 = linePosition1 != linePosition2;

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
        LinePosition linePosition1 = new(1, 2);
        LinePosition linePosition2 = new(3, 4);

        // Act
        bool actualEquals1 = linePosition1.Equals(linePosition2);
        bool actualEquals2 = linePosition2.Equals((object?)linePosition1);
        bool actualEquals3 = linePosition1 == linePosition2;
        bool actualEquals4 = linePosition1 != linePosition2;

        // Assert
        Assert.False(actualEquals1);
        Assert.False(actualEquals2);
        Assert.False(actualEquals3);
        Assert.True(actualEquals4);
    }

    [Fact(DisplayName = $"The struct must implement the {nameof(IComparable<LinePosition>)} interface.")]
    public void Struct_MustImplementIComparable()
    {
        // Arrange

        // Act
        LinePosition linePosition = new(1, 2);

        // Assert
        Assert.IsAssignableFrom<IComparable<LinePosition>>(linePosition);
    }

    [Theory(DisplayName = $"The CompareToCode() method must include the {nameof(LinePosition.Line)} and {nameof(LinePosition.Character)} properties.")]
    [InlineData(1, 1, 2, 2, 0)]
    [InlineData(1, 2, 2, 2, -1)]
    [InlineData(2, 1, 2, 2, 1)]
    [InlineData(2, 3, 2, 2, -1)]
    [InlineData(3, 2, 2, 2, 1)]
    public void CompareToMethod_MustIncludeLineAndCharacter(int line1, int line2, int char1, int char2, int compareTo)
    {
        // Arrange
        LinePosition linePosition1 = new(line1, char1);
        LinePosition linePosition2 = new(line2, char2);

        // Act
        int actualCompareTo = linePosition1.CompareTo(linePosition2);

        // Assert
        Assert.Equal(compareTo, actualCompareTo);
    }

    [Fact(DisplayName = "The comparison operators must return expected results.")]
    [SuppressMessage("ReSharper", "EqualExpressionComparison", Justification = "The comparison is used in the assertion.")]
    public void ComparisonOperators_MustReturnExpectedResults()
    {
        // Arrange

        // Act
        bool compare1 = new LinePosition(1, 1) > new LinePosition(1, 1);
        bool compare2 = new LinePosition(1, 1) < new LinePosition(1, 1);
        bool compare3 = new LinePosition(1, 1) >= new LinePosition(1, 1);
        bool compare4 = new LinePosition(1, 1) <= new LinePosition(1, 1);
        bool compare5 = new LinePosition(1, 1) > new LinePosition(2, 1);
        bool compare6 = new LinePosition(1, 1) < new LinePosition(2, 1);
        bool compare7 = new LinePosition(1, 1) >= new LinePosition(2, 1);
        bool compare8 = new LinePosition(1, 1) <= new LinePosition(2, 1);
        bool compare9 = new LinePosition(1, 1) > new LinePosition(1, 2);
        bool compare10 = new LinePosition(1, 1) < new LinePosition(1, 2);
        bool compare11 = new LinePosition(1, 1) >= new LinePosition(1, 2);
        bool compare12 = new LinePosition(1, 1) <= new LinePosition(1, 2);

        // Assert
        Assert.False(compare1);
        Assert.False(compare2);
        Assert.True(compare3);
        Assert.True(compare4);
        Assert.False(compare5);
        Assert.True(compare6);
        Assert.False(compare7);
        Assert.True(compare8);
        Assert.False(compare9);
        Assert.True(compare10);
        Assert.False(compare11);
        Assert.True(compare12);
    }

    [Fact(DisplayName = $"The ToString() method must include the {nameof(LinePosition.Line)} and {nameof(LinePosition.Character)} properties.")]
    public void ToStringMethod_MustIncludeLineAndCharacter()
    {
        // Arrange
        LinePosition linePosition = new(1, 2);
        const string expectedToString = "1,2";

        // Act
        string actualString = linePosition.ToString();

        // Assert
        Assert.Equal(expectedToString, actualString);
    }
}

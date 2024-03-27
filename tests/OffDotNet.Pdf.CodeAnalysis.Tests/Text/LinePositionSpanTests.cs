// <copyright file="LinePositionSpanTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Text;

using System.Diagnostics.CodeAnalysis;
using OffDotNet.Pdf.CodeAnalysis.Text;

public class LinePositionSpanTests
{
    [Fact(DisplayName = $"The constructor must throw an {nameof(ArgumentOutOfRangeException)} when the {nameof(LinePosition.Line)} is negative.")]
    [SuppressMessage("ReSharper", "UnusedVariable", Justification = "The variable is used in the assertion.")]
    [SuppressMessage("Minor Code Smell", "S1481:Unused local variables should be removed", Justification = "The variable is used in the assertion.")]
    [SuppressMessage("ReSharper", "MoveLocalFunctionAfterJumpStatement", Justification = "The local function is used in the assertion.")]
    public void Constructor_InvalidStartAndEndLinePositions_MustThrowArgumentOutOfRangeException()
    {
        // Arrange
        LinePosition start = new(5, 0);
        LinePosition end = new(0, 0);

        // Assert
        void LinePositionSpanFunc()
        {
            LinePositionSpan linePositionSpan = new(start, end);
        }

        // Act
        Assert.Throws<ArgumentException>(LinePositionSpanFunc);
    }

    [Fact(DisplayName = $"The {nameof(LinePositionSpan.Start)} property must be assigned from the constructor.")]
    public void StartProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        LinePosition start = new(1, 0);
        LinePosition end = new(5, 0);

        // Act
        LinePositionSpan linePositionSpan = new(start, end);
        var actualStart = linePositionSpan.Start;

        // Assert
        Assert.Equal(start, actualStart);
    }

    [Fact(DisplayName = $"The {nameof(LinePositionSpan.End)} property must be assigned from the constructor.")]
    public void EndProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        LinePosition start = new(1, 0);
        LinePosition end = new(5, 0);

        // Act
        LinePositionSpan linePositionSpan = new(start, end);
        var actualEnd = linePositionSpan.End;

        // Assert
        Assert.Equal(end, actualEnd);
    }

    [Fact(DisplayName = $"The GetHashCode() method must include the {nameof(LinePosition.Line)} and {nameof(LinePosition.Character)} properties.")]
    public void GetHashCodeMethod_MustIncludeLineAndCharacter()
    {
        // Arrange
        LinePosition start = new(1, 0);
        LinePosition end = new(5, 0);
        var expectedHashCode = HashCode.Combine(start, end);

        // Act
        LinePositionSpan linePositionSpan = new(start, end);
        var actualHashCode = linePositionSpan.GetHashCode();

        // Assert
        Assert.Equal(expectedHashCode, actualHashCode);
    }

    [Fact(DisplayName = $"The ToString() method must include the {nameof(LinePosition.Line)} and {nameof(LinePosition.Character)} properties.")]
    public void ToStringMethod_MustIncludeLineAndCharacter()
    {
        // Arrange
        LinePosition start = new(1, 0);
        LinePosition end = new(5, 0);
        var expectedToString = $"({start})-({end})";

        // Act
        LinePositionSpan linePositionSpan = new(start, end);
        var actualToString = linePositionSpan.ToString();

        // Assert
        Assert.Equal(expectedToString, actualToString);
    }

    [Fact(DisplayName = $"The struct must implement the {nameof(IEquatable<LinePositionSpan>)} interface.")]
    public void Struct_MustImplementIEquatable()
    {
        // Arrange
        LinePosition start = new(1, 0);
        LinePosition end = new(5, 0);

        // Act
        LinePositionSpan linePositionSpan = new(start, end);

        // Assert
        Assert.IsAssignableFrom<IEquatable<LinePositionSpan>>(linePositionSpan);
    }

    [Fact(DisplayName = "The Equals() method must return true.")]
    public void EqualsMethod_MustReturnTrue()
    {
        // Arrange
        LinePosition start1 = new(1, 0);
        LinePosition start2 = new(1, 0);
        LinePosition end1 = new(5, 0);
        LinePosition end2 = new(5, 0);
        LinePositionSpan linePosition1 = new(start1, end1);
        LinePositionSpan linePosition2 = new(start2, end2);

        // Act
        var actualEquals1 = linePosition1.Equals(linePosition2);
        var actualEquals2 = linePosition2.Equals((object?)linePosition1);
        var actualEquals3 = linePosition1 == linePosition2;
        var actualEquals4 = linePosition1 != linePosition2;

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
        LinePosition start1 = new(1, 0);
        LinePosition start2 = new(1, 1);
        LinePosition end1 = new(5, 0);
        LinePosition end2 = new(5, 1);
        LinePositionSpan linePosition1 = new(start1, end1);
        LinePositionSpan linePosition2 = new(start2, end2);

        // Act
        var actualEquals1 = linePosition1.Equals(linePosition2);
        var actualEquals2 = linePosition2.Equals((object?)linePosition1);
        var actualEquals3 = linePosition1 == linePosition2;
        var actualEquals4 = linePosition1 != linePosition2;

        // Assert
        Assert.False(actualEquals1);
        Assert.False(actualEquals2);
        Assert.False(actualEquals3);
        Assert.True(actualEquals4);
    }
}

// <copyright file="RectangleTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.Core.CommonDataStructures;
using Xunit;

namespace OffDotNet.Pdf.Core.Tests.CommonDataStructures;

public sealed class RectangleTests
{
    [Theory(DisplayName = $"Constructor with a negative points should throw an {nameof(ArgumentOutOfRangeException)}")]
    [InlineData(-1, 0, 1, 2)]
    [InlineData(0, -1, 1, 2)]
    [InlineData(0, 1, -1, 2)]
    [InlineData(0, 1, 2, -1)]
    public void Rectangle_ConstructorWithNegativePoints_ShouldThrowException(int lowerLeftX, int lowerLeftY, int upperRightX, int upperRightY)
    {
        // Arrange

        // Act
        Rectangle RectangleFunction()
        {
            return new(lowerLeftX, lowerLeftY, upperRightX, upperRightY);
        }

        // Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(RectangleFunction);
        Assert.StartsWith(Resource.Rectangle_PointMustBePositive, exception.Message);
    }

    [Theory(DisplayName = $"The properties of a ${nameof(Rectangle)} should match the values provided in the constructor")]
    [InlineData(0, 1, 2, 3)]
    [InlineData(123, 46, 456, 72)]
    public void Rectangle_PointsProperties_ShouldReturnValidValues(int lowerLeftX, int lowerLeftY, int upperRightX, int upperRightY)
    {
        // Arrange
        Rectangle rectangle = new(lowerLeftX, lowerLeftY, upperRightX, upperRightY);

        // Act
        int actualLowerLeftX = rectangle.LowerLeftX;
        int actualLowerLeftY = rectangle.LowerLeftY;
        int actualUpperRightX = rectangle.UpperRightX;
        int actualUpperRightY = rectangle.UpperRightY;

        // Assert
        Assert.Equal(actualLowerLeftX, lowerLeftX);
        Assert.Equal(actualLowerLeftY, lowerLeftY);
        Assert.Equal(actualUpperRightX, upperRightX);
        Assert.Equal(actualUpperRightY, upperRightY);
    }

    [Theory(DisplayName = $"The {nameof(Rectangle.Content)} property should return a valid value")]
    [InlineData(0, 1, 2, 3, "[0 1 2 3]")]
    [InlineData(123, 46, 456, 72, "[123 46 456 72]")]
    public void Rectangle_Content_ShouldReturnValidValue(int lowerLeftX, int lowerLeftY, int upperRightX, int upperRightY, string expectedContent)
    {
        // Arrange
        Rectangle rectangle = new(lowerLeftX, lowerLeftY, upperRightX, upperRightY);

        // Act
        string actualContent = rectangle.Content;

        // Assert
        Assert.Equal(expectedContent, actualContent);
    }
}

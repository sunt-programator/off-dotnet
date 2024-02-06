// <copyright file="RectangleTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Tests.CommonDataStructures;

using OffDotNet.Pdf.Core.CommonDataStructures;
using OffDotNet.Pdf.Core.Properties;

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
        IRectangle RectangleFunction()
        {
            return new Rectangle(lowerLeftX, lowerLeftY, upperRightX, upperRightY);
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
        IRectangle rectangle = new Rectangle(lowerLeftX, lowerLeftY, upperRightX, upperRightY);

        // Act
        float actualLowerLeftX = rectangle.LowerLeftX;
        float actualLowerLeftY = rectangle.LowerLeftY;
        float actualUpperRightX = rectangle.UpperRightX;
        float actualUpperRightY = rectangle.UpperRightY;

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
        IRectangle rectangle = new Rectangle(lowerLeftX, lowerLeftY, upperRightX, upperRightY);

        // Act
        string actualContent = rectangle.Content;

        // Assert
        Assert.Equal(expectedContent, actualContent);
    }
}

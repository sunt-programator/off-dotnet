// <copyright file="MoveTextOperationTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.Core.Primitives;
using OffDotNet.Pdf.Core.Text.Operations.TextPosition;
using Xunit;

namespace OffDotNet.Pdf.Core.Tests.Text.Operations.TextPosition;

public class MoveTextOperationTests
{
    [Fact(DisplayName = $"The {nameof(MoveTextOperation.PdfOperator)} property should return a valid value")]
    public void MoveTextOperation_PdfOperatorProperty_ShouldReturnValidValue()
    {
        // Arrange
        const string expectedOperator = "Td";
        IMoveTextOperation moveTextOperation = new MoveTextOperation(3, 6);

        // Act
        string actualPdfOperator = moveTextOperation.PdfOperator;

        // Assert
        Assert.Equal(expectedOperator, actualPdfOperator);
    }

    [Theory(DisplayName = $"The {nameof(MoveTextOperation.X)} and {nameof(MoveTextOperation.Y)} properties should return valid values")]
    [InlineData(3, 6)]
    [InlineData(3.2, 6)]
    [InlineData(3.2, 6.6)]
    [InlineData(3.2, -6.6)]
    [InlineData(-3.2, -6.6)]
    public void MoveTextOperation_XYProperties_ShouldReturnValidValues(float x, float y)
    {
        // Arrange
        IMoveTextOperation moveTextOperation = new MoveTextOperation(x, y);

        // Act
        float actualX = moveTextOperation.X;
        float actualY = moveTextOperation.Y;

        // Assert
        Assert.Equal(x, actualX);
        Assert.Equal(y, actualY);
    }

    [Theory(DisplayName = $"The {nameof(MoveTextOperation.Content)} property should return a valid value")]
    [InlineData(3, 6, "3 6 Td\n")]
    [InlineData(3.2, 6, "3.2 6 Td\n")]
    [InlineData(3.2, 6.6, "3.2 6.6 Td\n")]
    [InlineData(3.2, -6.6, "3.2 -6.6 Td\n")]
    [InlineData(-3.2, -6.6, "-3.2 -6.6 Td\n")]
    public void MoveTextOperation_Content_ShouldReturnValidValue(float x, float y, string expectedContent)
    {
        // Arrange
        IMoveTextOperation moveTextOperation = new MoveTextOperation(x, y);

        // Act
        string actualContent = moveTextOperation.Content;

        // Assert
        Assert.Equal(expectedContent, actualContent);
    }

    [Theory(DisplayName = $"The {nameof(MoveTextOperation.Bytes)} property should return a valid value")]
    [InlineData(3, 6, new byte[] { 0x33, 0x20, 0x36, 0x20, 0x54, 0x64, 0x0A })]
    [InlineData(3.2, 6, new byte[] { 0x33, 0x2E, 0x32, 0x20, 0x36, 0x20, 0x54, 0x64, 0x0A })]
    [InlineData(3.2, 6.6, new byte[] { 0x33, 0x2E, 0x32, 0x20, 0x36, 0x2E, 0x36, 0x20, 0x54, 0x64, 0x0A })]
    [InlineData(3.2, -6.6, new byte[] { 0x33, 0x2E, 0x32, 0x20, 0x2D, 0x36, 0x2E, 0x36, 0x20, 0x54, 0x64, 0x0A })]
    [InlineData(-3.2, -6.6, new byte[] { 0x2D, 0x33, 0x2E, 0x32, 0x20, 0x2D, 0x36, 0x2E, 0x36, 0x20, 0x54, 0x64, 0x0A })]
    public void MoveTextOperation_Bytes_ShouldReturnValidValue(float x, float y, byte[] expectedBytes)
    {
        // Arrange
        IMoveTextOperation moveTextOperation = new MoveTextOperation(x, y);

        // Act
        byte[] actualBytes = moveTextOperation.Bytes.ToArray();

        // Assert
        Assert.Equal(expectedBytes, actualBytes);
    }

    [Theory(DisplayName = "The GetHashCode method should return a valid value")]
    [InlineData(3, 6)]
    [InlineData(3.2, 6)]
    [InlineData(3.2, 6.6)]
    [InlineData(3.2, -6.6)]
    [InlineData(-3.2, -6.6)]
    public void MoveTextOperation_GetHashCode_ShouldReturnValidValue(float x, float y)
    {
        // Arrange
        int expectedHashCode = HashCode.Combine(nameof(MoveTextOperation), (PdfReal)x, (PdfReal)y, "Td");
        IMoveTextOperation moveTextOperation = new MoveTextOperation(x, y);

        // Act
        int actualHashCode = moveTextOperation.GetHashCode();

        // Assert
        Assert.Equal(expectedHashCode, actualHashCode);
    }

    [Theory(DisplayName = "The Equals property should return a valid value")]
    [InlineData(3f, 6f, 3f, 6f, true)]
    [InlineData(3f, 6f, -3f, 6f, false)]
    [InlineData(3f, 6f, -3f, -6f, false)]
    [InlineData(-3f, 6f, -3f, -6f, false)]
    [InlineData(-3.20f, 6f, -3.200f, 6f, true)]
    [InlineData(3.20f, 6f, null, null, false)]
    public void MoveTextOperation_Equals_ShouldReturnValidValue(float moveTextX1, float moveTextY1, float? moveTextX2, float? moveTextY2, bool expectedResult)
    {
        // Arrange
        IMoveTextOperation moveTextOperation1 = new MoveTextOperation(moveTextX1, moveTextY1);
        IMoveTextOperation? moveTextOperation2 = moveTextX2.HasValue && moveTextY2.HasValue ? new MoveTextOperation(moveTextX2.Value, moveTextY2.Value) : null;

        // Act
        bool actualResult = moveTextOperation1.Equals(moveTextOperation2);

        // Assert
        Assert.Equal(expectedResult, actualResult);
    }
}

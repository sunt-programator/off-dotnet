// <copyright file="FontOperationTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.Core.Primitives;
using OffDotNet.Pdf.Core.Text.Operations.TextState;
using Xunit;

namespace OffDotNet.Pdf.Core.Tests.Text.Operations.TextState;

public class FontOperationTests
{
    [Theory(DisplayName = $"Constructor with negative {nameof(FontOperation.FontSize)} should throw an {nameof(ArgumentOutOfRangeException)}")]
    [InlineData(-1)]
    [InlineData(-15)]
    [InlineData(-23)]
    public void FontOperation_NegativeByteOffset_ShouldThrowException(int fontSize)
    {
        // Arrange

        // Act
        FontOperation FontOperationFunction()
        {
            return new("F13", fontSize);
        }

        // Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(FontOperationFunction);
        Assert.StartsWith(Resource.FontOperation_FontSizeMustBePositive, exception.Message);
    }

    [Fact(DisplayName = $"The {nameof(FontOperation.PdfOperator)} property should return a valid value")]
    public void FontOperation_PdfOperatorProperty_ShouldReturnValidValue()
    {
        // Arrange
        const string expectedOperator = "Tf";
        FontOperation fontOperation = new("F13", 6);

        // Act
        string actualPdfOperator = fontOperation.PdfOperator;

        // Assert
        Assert.Equal(expectedOperator, actualPdfOperator);
    }

    [Theory(DisplayName = $"The {nameof(FontOperation.FontName)} property should return a valid values")]
    [InlineData("F1", 6)]
    [InlineData("F2", 24)]
    [InlineData("F3", 4)]
    [InlineData("F4", 3)]
    public void FontOperation_FontName_ShouldReturnValidValues(string fontName, int fontSize)
    {
        // Arrange
        PdfName expectedFontName = fontName;
        FontOperation fontOperation = new(expectedFontName, fontSize);

        // Act
        PdfName actualFontName = fontOperation.FontName;

        // Assert
        Assert.Equal(expectedFontName, actualFontName);
    }

    [Theory(DisplayName = $"The {nameof(FontOperation.FontSize)} property should return a valid values")]
    [InlineData("F1", 6)]
    [InlineData("F2", 24)]
    [InlineData("F3", 4)]
    [InlineData("F4", 3)]
    public void FontOperation_FontSize_ShouldReturnValidValues(string fontName, int fontSize)
    {
        // Arrange
        PdfInteger expectedFontSize = fontSize;
        FontOperation fontOperation = new(fontName, fontSize);

        // Act
        PdfInteger actualFontSize = fontOperation.FontSize;

        // Assert
        Assert.Equal(actualFontSize, expectedFontSize);
    }

    [Theory(DisplayName = $"The {nameof(FontOperation.Content)} property should return a valid value")]
    [InlineData("F1", 6, "/F1 6 Tf\n")]
    [InlineData("F2", 24, "/F2 24 Tf\n")]
    [InlineData("F3", 4, "/F3 4 Tf\n")]
    [InlineData("F4", 3, "/F4 3 Tf\n")]
    public void FontOperation_Content_ShouldReturnValidValue(string fontName, int fontSize, string expectedContent)
    {
        // Arrange
        FontOperation fontOperation = new(fontName, fontSize);

        // Act
        string actualContent = fontOperation.Content;

        // Assert
        Assert.Equal(expectedContent, actualContent);
    }

    [Theory(DisplayName = $"The {nameof(FontOperation.Length)} property should return a valid value")]
    [InlineData("F1", 6, 9)]
    [InlineData("F2", 24, 10)]
    [InlineData("F3", 4, 9)]
    [InlineData("F4", 3, 9)]
    public void FontOperation_Length_ShouldReturnValidValue(string fontName, int fontSize, int expectedLength)
    {
        // Arrange
        FontOperation fontOperation = new(fontName, fontSize);

        // Act
        int actualLength = fontOperation.Length;

        // Assert
        Assert.Equal(expectedLength, actualLength);
    }

    [Theory(DisplayName = $"The {nameof(FontOperation.Bytes)} property should return a valid value")]
    [InlineData("F1", 6, new byte[] { 0x2F, 0x46, 0x31, 0x20, 0x36, 0x20, 0x54, 0x66, 0x0A })]
    [InlineData("F2", 24, new byte[] { 0x2F, 0x46, 0x32, 0x20, 0x32, 0x34, 0x20, 0x54, 0x66, 0x0A })]
    [InlineData("F3", 4, new byte[] { 0x2F, 0x46, 0x33, 0x20, 0x34, 0x20, 0x54, 0x66, 0x0A })]
    [InlineData("F4", 3, new byte[] { 0x2F, 0x46, 0x34, 0x20, 0x33, 0x20, 0x54, 0x66, 0x0A })]
    public void FontOperation_Bytes_ShouldReturnValidValue(string fontName, int fontSize, byte[] expectedBytes)
    {
        // Arrange
        FontOperation fontOperation = new(fontName, fontSize);

        // Act
        byte[] actualBytes = fontOperation.Bytes.ToArray();

        // Assert
        Assert.Equal(expectedBytes, actualBytes);
    }

    [Theory(DisplayName = "The GetHashCode method should return a valid value")]
    [InlineData("F1", 6)]
    [InlineData("F2", 24)]
    [InlineData("F3", 4)]
    [InlineData("F4", 3)]
    public void FontOperation_GetHashCode_ShouldReturnValidValue(string fontName, int fontSize)
    {
        // Arrange
        PdfName pdfFontName = fontName;
        PdfInteger pdfFontSize = fontSize;
        int expectedHashCode = HashCode.Combine(nameof(FontOperation), pdfFontName, pdfFontSize, FontOperation.OperatorName);
        FontOperation fontOperation = new(fontName, fontSize);

        // Act
        int actualHashCode = fontOperation.GetHashCode();

        // Assert
        Assert.Equal(expectedHashCode, actualHashCode);
    }

    [Theory(DisplayName = "The Equals property should return a valid value")]
    [MemberData(nameof(FontOperationTestsDataGenerator.FontOperation_Equals_TestCases), MemberType = typeof(FontOperationTestsDataGenerator))]
    public void FontOperation_Equals_ShouldReturnValidValue(FontOperation fontOperation1, FontOperation fontOperation2, bool expectedResult)
    {
        // Arrange

        // Act
        bool actualResult = fontOperation1.Equals(fontOperation2);

        // Assert
        Assert.Equal(expectedResult, actualResult);
    }
}

[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "TestData generator class can be in the same file")]
internal static class FontOperationTestsDataGenerator
{
    public static IEnumerable<object[]> FontOperation_Equals_TestCases()
    {
        yield return new object[] { new FontOperation("F1", 6), new FontOperation("F1", 6), true };
        yield return new object[] { new FontOperation("F1", 6), new FontOperation("F2", 6), false };
        yield return new object[] { new FontOperation("F1", 6), new FontOperation("F2", 8), false };
        yield return new object[] { new FontOperation("F2", 6), new FontOperation("F2", 8), false };
        yield return new object[] { new FontOperation("F2", 8), new FontOperation("F2", 8), true };
        yield return new object[] { new FontOperation("F2", 8), null!, false };
    }
}

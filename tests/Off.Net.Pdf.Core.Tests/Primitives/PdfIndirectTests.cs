using System;
using Off.Net.Pdf.Core.Primitives;
using Xunit;

namespace Off.Net.Pdf.Core.Tests.Primitives;

public class PdfIndirectTests
{
    [Theory(DisplayName = "Constructor initializer check reference identifier")]
    [InlineData(0, 0, "0 0 R")]
    [InlineData(0, 65535, "0 65535 R")]
    [InlineData(21, 6, "21 6 R")]
    public void PdfIndirect_Constructor_CheckReferenceIdentifier(int objectNumber, int generationNumber, string expectedReferenceIdentifier)
    {
        // Arrange
        PdfIndirect pdfIndirect = new PdfString("Test").ToPdfIndirect(objectNumber, generationNumber);

        // Act
        string actualReferenceIdentifier = pdfIndirect.ReferenceIdentifier;

        // Assert
        Assert.Equal(expectedReferenceIdentifier, actualReferenceIdentifier);
        Assert.Equal(objectNumber, pdfIndirect.ObjectNumber);
        Assert.Equal(generationNumber, pdfIndirect.GenerationNumber);
    }

    [Theory(DisplayName = "Check Length property")]
    [InlineData(0, 0, "Test", 21)]
    [InlineData(12, 0, "Brillig", 25)]
    [InlineData(21, 6, "String1", 25)]
    public void PdfIndirect_Length_CheckValue(int objectNumber, int generationNumber, string actualStringValue, int expectedLength)
    {
        // Arrange
        PdfIndirect pdfIndirect = new PdfString(actualStringValue).ToPdfIndirect(objectNumber, generationNumber);

        // Act
        int actualLength = pdfIndirect.Length;

        // Assert
        Assert.Equal(expectedLength, actualLength);
    }

    [Theory(DisplayName = "Check Content property")]
    [InlineData(0, 0, "Test", "0 0 obj\n(Test)\nendobj")]
    [InlineData(12, 0, "Brillig", "12 0 obj\n(Brillig)\nendobj")]
    [InlineData(21, 6, "String1", "21 6 obj\n(String1)\nendobj")]
    public void PdfIndirect_Content_CheckValue(int objectNumber, int generationNumber, string actualStringValue, string expectedContent)
    {
        // Arrange
        PdfIndirect pdfIndirect = new PdfString(actualStringValue).ToPdfIndirect(objectNumber, generationNumber);

        // Act
        string actualContent = pdfIndirect.Content;

        // Assert
        Assert.Equal(expectedContent, actualContent);
    }

    [Theory(DisplayName = "Check Value property")]
    [InlineData(0, 0, "Test", "(Test)")]
    [InlineData(12, 0, "Brillig", "(Brillig)")]
    [InlineData(21, 6, "String1", "(String1)")]
    public void PdfIndirect_ValueString_CheckContent(int objectNumber, int generationNumber, string actualStringValue, string expectedValueContent)
    {
        // Arrange
        PdfIndirect pdfIndirect = new PdfString(actualStringValue).ToPdfIndirect(objectNumber, generationNumber);

        // Act
        string actualValueContent = pdfIndirect.Value.Content;

        // Assert
        Assert.Equal(expectedValueContent, actualValueContent);
    }

    [Theory(DisplayName = "Check GetHashCode method")]
    [InlineData(0, 0, "Test")]
    [InlineData(12, 0, "Brillig")]
    [InlineData(21, 6, "String1")]
    public void PdfIndirect_GetHashCode_CheckValidity(int objectNumber, int generationNumber, string actualStringValue)
    {
        // Arrange
        PdfIndirect pdfIndirect = new PdfString(actualStringValue).ToPdfIndirect(objectNumber, generationNumber);
        int expectedHashCode = HashCode.Combine(nameof(PdfIndirect).GetHashCode(), objectNumber.GetHashCode(), generationNumber.GetHashCode());

        // Act
        int actualHashCode = pdfIndirect.GetHashCode();

        // Assert
        Assert.Equal(expectedHashCode, actualHashCode);
    }

    [Fact(DisplayName = "Check Equals method")]
    public void PdfIndirect_Equals_Null_ShouldReturnFalse()
    {
        // Arrange
        PdfIndirect pdfIndirect = new PdfString("Test").ToPdfIndirect(0);

        // Act
        bool actualResult1 = pdfIndirect.Equals(null);
        bool actualResult2 = pdfIndirect!.Equals((object?)null);

        // Assert
        Assert.False(actualResult1);
        Assert.False(actualResult2);
    }

    [Theory(DisplayName = "Check Equals method")]
    [InlineData(0, 0, 0, 0, "Test1", "Test2", true)] // Indirect object equality is checked against object and generation number
    [InlineData(0, 0, 0, 0, "Test1", "Test1", true)]
    [InlineData(0, 1, 0, 0, "Test1", "Test1", false)]
    [InlineData(1, 1, 0, 0, "Test1", "Test1", true)]
    [InlineData(1, 1, 1, 0, "Test1", "Test1", false)]
    [InlineData(1, 1, 1, 1, "Test1", "Test1", true)]
    public void PdfIndirect_Equals_ShouldReturnFalse(int objectNumber1, int objectNumber2, int generationNumber1, int generationNumber2, string actualStringValue1, string actualStringValue2,
        bool expectedResult)
    {
        // Arrange
        PdfIndirect pdfIndirect1 = new PdfString(actualStringValue1).ToPdfIndirect(objectNumber1, generationNumber1);
        PdfIndirect pdfIndirect2 = new PdfString(actualStringValue2).ToPdfIndirect(objectNumber2, generationNumber2);

        // Act
        bool actualResult = pdfIndirect1.Equals(pdfIndirect2);

        // Assert
        Assert.Equal(expectedResult, actualResult);
    }

    [Theory(DisplayName = "Check multiple Value access")]
    [InlineData(0, 0, "Test")]
    [InlineData(12, 0, "Brillig")]
    [InlineData(21, 6, "String1")]
    public void PdfIndirect_Value_CheckValidity(int objectNumber, int generationNumber, string actualStringValue)
    {
        // Arrange
        PdfIndirect pdfIndirect = new PdfString(actualStringValue).ToPdfIndirect(objectNumber, generationNumber);

        // Act
        string actualContent1 = pdfIndirect.Content;
        string actualContent2 = pdfIndirect.Content;

        // Assert
        Assert.Equal(actualContent1, actualContent2);
        Assert.True(ReferenceEquals(actualContent1, actualContent2));
    }

    [Theory(DisplayName = "Check Bytes property")]
    [InlineData(0, 0, "Test", new byte[] { 0x30, 0x20, 0x30, 0x20, 0x6f, 0x62, 0x6a, 0x0a, 0x28, 0x54, 0x65, 0x73, 0x74, 0x29, 0x0a, 0x65, 0x6e, 0x64, 0x6f, 0x62, 0x6a })]
    [InlineData(12, 0, "Brillig", new byte[] { 0x31, 0x32, 0x20, 0x30, 0x20, 0x6f, 0x62, 0x6a, 0x0a, 0x28, 0x42, 0x72, 0x69, 0x6c, 0x6c, 0x69, 0x67, 0x29, 0x0a, 0x65, 0x6e, 0x64, 0x6f, 0x62, 0x6a })]
    [InlineData(21, 6, "String1", new byte[] { 0x32, 0x31, 0x20, 0x36, 0x20, 0x6f, 0x62, 0x6a, 0x0a, 0x28, 0x53, 0x74, 0x72, 0x69, 0x6e, 0x67, 0x31, 0x29, 0x0a, 0x65, 0x6e, 0x64, 0x6f, 0x62, 0x6a })]
    public void PdfIndirect_Bytes_CheckValue(int objectNumber, int generationNumber, string actualStringValue, byte[] expectedBytes)
    {
        // Arrange
        PdfIndirect pdfIndirect = new PdfString(actualStringValue).ToPdfIndirect(objectNumber, generationNumber);

        // Act
        byte[] actualBytes = pdfIndirect.Bytes;

        // Assert
        Assert.Equal(expectedBytes, actualBytes);
    }

    [Theory(DisplayName = "Throw exception if object number is not positive")]
    [InlineData(-1)]
    [InlineData(-7)]
    [InlineData(-2465)]
    public void PdfIndirect_Constructor_NegativeObjectNumber_ShouldThrowException(int objectNumber)
    {
        // Arrange
        PdfString pdfString = new("Test String");

        // Act
        Action pdfIndirectFunc = () => pdfString.ToPdfIndirect(objectNumber);

        // Assert
        Assert.Throws<ArgumentException>(pdfIndirectFunc);
    }

    [Theory(DisplayName = "Throw exception if generation number is not positive")]
    [InlineData(-1)]
    [InlineData(-7)]
    [InlineData(-2465)]
    public void PdfIndirect_Constructor_NegativeGenerationNumber_ShouldThrowException(int generationNumber)
    {
        // Arrange
        PdfString pdfString = new("Test String");

        // Act
        Action pdfIndirectFunc = () => pdfString.ToPdfIndirect(5, generationNumber);

        // Assert
        Assert.Throws<ArgumentException>(pdfIndirectFunc);
    }
}

using Off.Net.Pdf.Core.Primitives;
using Xunit;

namespace Off.Net.Pdf.Core.Tests.Primitives;

public class PdfIndirectIdentifierTests
{
    [Theory(DisplayName = "Check Length property")]
    [InlineData(0, 0, 5)]
    [InlineData(12, 0,  6)]
    [InlineData(21, 6, 6)]
    public void PdfIndirectIdentifier_Length_CheckValue(int objectNumber, int generationNumber, int expectedLength)
    {
        // Arrange
        PdfIndirectIdentifier<PdfString> pdfIndirect = new PdfString("Test").ToPdfIndirect(objectNumber, generationNumber).ToPdfIndirectIdentifier();

        // Act
        int actualLength = pdfIndirect.Length;

        // Assert
        Assert.Equal(expectedLength, actualLength);
    }

    [Theory(DisplayName = "Check Content property")]
    [InlineData(0, 0, "Test", "0 0 R")]
    [InlineData(12, 0, "Brillig", "12 0 R")]
    [InlineData(21, 6, "String1", "21 6 R")]
    public void PdfIndirectIdentifier_Content_CheckValue(int objectNumber, int generationNumber, string actualStringValue, string expectedContent)
    {
        // Arrange
        PdfIndirectIdentifier<PdfString> pdfIndirect = new PdfString(actualStringValue).ToPdfIndirect(objectNumber, generationNumber).ToPdfIndirectIdentifier();

        // Act
        string actualContent = pdfIndirect.Content;

        // Assert
        Assert.Equal(expectedContent, actualContent);
    }

    [Theory(DisplayName = "Check GetHashCode method")]
    [InlineData(0, 0, "Test")]
    [InlineData(12, 0, "Brillig")]
    [InlineData(21, 6, "String1")]
    public void PdfIndirectIdentifier_GetHashCode_CheckValidity(int objectNumber, int generationNumber, string actualStringValue)
    {
        // Arrange
        PdfIndirectIdentifier<PdfString> pdfIndirect = new PdfString(actualStringValue).ToPdfIndirect(objectNumber, generationNumber).ToPdfIndirectIdentifier();
        int expectedHashCode = HashCode.Combine(nameof(PdfIndirectIdentifier<PdfString>), objectNumber, generationNumber);

        // Act
        int actualHashCode = pdfIndirect.GetHashCode();

        // Assert
        Assert.Equal(expectedHashCode, actualHashCode);
    }

    [Fact(DisplayName = "Check Equals method")]
    public void PdfIndirectIdentifier_Equals_Null_ShouldReturnFalse()
    {
        // Arrange
        PdfIndirectIdentifier<PdfString> pdfIndirect = new PdfString("Test").ToPdfIndirect(0).ToPdfIndirectIdentifier();

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
    public void PdfIndirectIdentifier_Equals_ShouldReturnFalse(int objectNumber1, int objectNumber2, int generationNumber1, int generationNumber2, string actualStringValue1, string actualStringValue2,
        bool expectedResult)
    {
        // Arrange
        PdfIndirectIdentifier<PdfString> pdfIndirect1 = new PdfString(actualStringValue1).ToPdfIndirect(objectNumber1, generationNumber1).ToPdfIndirectIdentifier();
        PdfIndirectIdentifier<PdfString> pdfIndirect2 = new PdfString(actualStringValue2).ToPdfIndirect(objectNumber2, generationNumber2).ToPdfIndirectIdentifier();

        // Act
        bool actualResult = pdfIndirect1.Equals(pdfIndirect2);

        // Assert
        Assert.Equal(expectedResult, actualResult);
    }

    [Theory(DisplayName = "Check Equals method")]
    [InlineData(0, 0, 0, 0, "Test1", "Test2", true)] // Indirect object equality is checked against object and generation number
    [InlineData(0, 0, 0, 0, "Test1", "Test1", true)]
    [InlineData(0, 1, 0, 0, "Test1", "Test1", false)]
    [InlineData(1, 1, 0, 0, "Test1", "Test1", true)]
    [InlineData(1, 1, 1, 0, "Test1", "Test1", false)]
    [InlineData(1, 1, 1, 1, "Test1", "Test1", true)]
    public void PdfIndirectIdentifier_Equals2_ShouldReturnFalse(int objectNumber1, int objectNumber2, int generationNumber1, int generationNumber2, string actualStringValue1, string actualStringValue2,
        bool expectedResult)
    {
        // Arrange
        PdfIndirectIdentifier<PdfString> pdfIndirect1 = new PdfString(actualStringValue1).ToPdfIndirect(objectNumber1, generationNumber1).ToPdfIndirectIdentifier();
        PdfIndirectIdentifier<PdfString> pdfIndirect2 = new PdfString(actualStringValue2).ToPdfIndirect(objectNumber2, generationNumber2).ToPdfIndirectIdentifier();

        // Act
        bool actualResult = pdfIndirect1.Equals((object)pdfIndirect2);

        // Assert
        Assert.Equal(expectedResult, actualResult);
    }

    [Theory(DisplayName = "Check multiple Value access")]
    [InlineData(0, 0, "Test")]
    [InlineData(12, 0, "Brillig")]
    [InlineData(21, 6, "String1")]
    public void PdfIndirectIdentifier_Value_CheckValidity(int objectNumber, int generationNumber, string actualStringValue)
    {
        // Arrange
        PdfIndirectIdentifier<PdfString> pdfIndirect = new PdfString(actualStringValue).ToPdfIndirect(objectNumber, generationNumber).ToPdfIndirectIdentifier();

        // Act
        string actualContent1 = pdfIndirect.Content;
        string actualContent2 = pdfIndirect.Content;

        // Assert
        Assert.Equal(actualContent1, actualContent2);
        Assert.True(ReferenceEquals(actualContent1, actualContent2));
    }

    [Theory(DisplayName = "Check Bytes property")]
    [InlineData(0, 0,  new byte[] { 0x30,0x20,0x30,0x20,0x52 })]
    [InlineData(12, 0,  new byte[] { 0x31, 0x32, 0x20, 0x30, 0x20, 0x52 })]
    [InlineData(21, 6,  new byte[] { 0x32, 0x31, 0x20, 0x36, 0x20, 0x52 })]
    public void PdfIndirectIdentifier_Bytes_CheckValue(int objectNumber, int generationNumber, byte[] expectedBytes)
    {
        // Arrange
        PdfIndirectIdentifier<PdfString> pdfIndirect = new PdfString("Test").ToPdfIndirect(objectNumber, generationNumber).ToPdfIndirectIdentifier();

        // Act
         ReadOnlyMemory<byte> actualBytes = pdfIndirect.Bytes;

        // Assert
        Assert.True(actualBytes.Span.SequenceEqual(expectedBytes));
    }
}

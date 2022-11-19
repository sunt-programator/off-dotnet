using System;
using Off.Net.Pdf.Core.Primitives;
using Xunit;

namespace Off.Net.Pdf.Core.Tests.Primitives;

public class PdfNameTests
{
    [Theory(DisplayName = "Create an instance using parametrized constructor and check the Value property")]
    [InlineData("Name1", "Name1")]
    [InlineData("ASomewhatLongerName", "ASomewhatLongerName")]
    [InlineData("A;Name_With-Various***Characters?", "A;Name_With-Various***Characters?")]
    [InlineData("1.2", "1.2")]
    [InlineData("$$", "$$")]
    [InlineData("@pattern", "@pattern")]
    [InlineData(".notdef", ".notdef")]
    [InlineData("Lime Green", "Lime Green")]
    [InlineData("paired()parentheses", "paired()parentheses")]
    [InlineData("The_Key_of_F#_Minor", "The_Key_of_F#_Minor")]
    [InlineData("/NameWithSolidus", "/NameWithSolidus")]
    public void PdfName_ParameterizedConstructor_CheckValue(string inputValue, string expectedValue)
    {
        // Arrange
        PdfName pdfName = inputValue; // Use an implicit conversion from string to PdfName

        // Act

        // Assert
        Assert.Equal(expectedValue, pdfName.Value);
    }

    [Theory(DisplayName = "Constructor should throw and exception when the value is null or contain whitespaces")]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void PdfName_ParameterizedConstructor_NullOrWhitespace_ShouldThrowArgumentNullException(string inputValue)
    {
        // Arrange

        // Act
        // ReSharper disable once ObjectCreationAsStatement
        void PdfNameDelegate() => new PdfName(inputValue);

        // Assert
        Assert.Throws<ArgumentNullException>(PdfNameDelegate);
    }

    [Theory(DisplayName = "Check the length of the PDF name primitive")]
    [InlineData("Name1", 6)] // Solidus character + Name1
    [InlineData("ASomewhatLongerName", 20)]
    [InlineData("A;Name_With-Various***Characters?", 34)]
    [InlineData("1.2", 4)]
    [InlineData("$$", 3)]
    [InlineData("@pattern", 9)]
    [InlineData(".notdef", 8)]
    [InlineData("Lime Green", 13)]
    [InlineData("paired()parentheses", 24)]
    [InlineData("The_Key_of_F#_Minor", 22)]
    [InlineData("/NameWithSolidus", 19)]
    public void PdfName_Length_CheckValue(string value, int expectedLength)
    {
        // Arrange
        PdfName pdfName = value; // Use an implicit conversion from string to PdfName

        // Act
        int actualLength = pdfName.Length;

        // Assert
        Assert.Equal(expectedLength, actualLength);
    }

    [Fact(DisplayName = "Check Equals method if the argument is null")]
    public void PdfName_Equals_NullArgument_ShouldReturnFalse()
    {
        // Arrange
        PdfName pdfName1 = "Name1"; // Use an implicit conversion from string to PdfName

        // Act
        bool actualResult = pdfName1.Equals(null);

        // Assert
        Assert.False(actualResult);
    }

    [Fact(DisplayName = "Check Equals method if the argument is null")]
    public void PdfName_Equals2_NullArgument_ShouldReturnFalse()
    {
        // Arrange
        PdfName pdfName1 = "Name1"; // Use an implicit conversion from string to PdfName

        // Act
        bool actualResult = pdfName1.Equals((object?)null);

        // Assert
        Assert.False(actualResult);
    }

    [Theory(DisplayName = "Check Equals method with object type as an argument")]
    [InlineData("Name1", "Name1", true)]
    [InlineData("Name1", "name1", false)]
    [InlineData("Name1", "\\Name1", false)]
    public void PdfName_Equality_CheckEquals(string value1, string value2, bool expectedResult)
    {
        // Arrange
        PdfName pdfName1 = value1; // Use an implicit conversion from string to PdfName
        PdfName pdfName2 = value2; // Use an implicit conversion from string to PdfName

        // Act
        bool actualResult = pdfName1.Equals((object)pdfName2);

        // Assert
        Assert.Equal(expectedResult, actualResult);
    }

    [Theory(DisplayName = "Check if Bytes property returns valid data")]
    [InlineData("Name1", new byte[] { 47, 78, 97, 109, 101, 49 })]
    [InlineData("ASomewhatLongerName", new byte[] { 47, 65, 83, 111, 109, 101, 119, 104, 97, 116, 76, 111, 110, 103, 101, 114, 78, 97, 109, 101 })]
    [InlineData("A;Name_With-Various***Characters?", new byte[] { 47, 65, 59, 78, 97, 109, 101, 95, 87, 105, 116, 104, 45, 86, 97, 114, 105, 111, 117, 115, 42, 42, 42, 67, 104, 97, 114, 97, 99, 116, 101, 114, 115, 63 })]
    [InlineData("1.2", new byte[] { 47, 49, 46, 50 })]
    [InlineData("$$", new byte[] { 47, 36, 36 })]
    [InlineData("@pattern", new byte[] { 47, 64, 112, 97, 116, 116, 101, 114, 110 })]
    [InlineData(".notdef", new byte[] { 47, 46, 110, 111, 116, 100, 101, 102 })]
    [InlineData("Lime Green", new byte[] { 47, 76, 105, 109, 101, 35, 50, 48, 71, 114, 101, 101, 110 })]
    [InlineData("paired()parentheses", new byte[] { 47, 112, 97, 105, 114, 101, 100, 35, 50, 56, 35, 50, 57, 112, 97, 114, 101, 110, 116, 104, 101, 115, 101, 115 })]
    [InlineData("The_Key_of_F#_Minor", new byte[] { 47, 84, 104, 101, 95, 75, 101, 121, 95, 111, 102, 95, 70, 35, 50, 51, 95, 77, 105, 110, 111, 114 })]
    [InlineData("/NameWithSolidus", new byte[] { 47, 35, 50, 70, 78, 97, 109, 101, 87, 105, 116, 104, 83, 111, 108, 105, 100, 117, 115 })]
    public void PdfName_Bytes_CheckValidity(string value1, byte[] expectedBytes)
    {
        // Arrange
        PdfName pdfName1 = value1; // Use an implicit conversion from string to PdfName

        // Act
        byte[] actualBytes = pdfName1.Bytes;

        // Assert
        Assert.Equal(expectedBytes, actualBytes);
    }

    [Theory(DisplayName = "Check if GetHashCode method returns valid value")]
    [InlineData("Name1")]
    [InlineData("ASomewhatLongerName")]
    [InlineData("A;Name_With-Various***Characters?")]
    [InlineData("1.2")]
    [InlineData("$$")]
    [InlineData("@pattern")]
    [InlineData(".notdef")]
    [InlineData("Lime Green")]
    [InlineData("paired()parentheses")]
    [InlineData("The_Key_of_F#_Minor")]
    [InlineData("/NameWithSolidus")]
    public void PdfName_GetHashCode_CheckValidity(string value1)
    {
        // Arrange
        PdfName pdfName1 = value1; // Use an implicit conversion from string to PdfName
        int expectedHashCode = HashCode.Combine(nameof(PdfName).GetHashCode(), value1.GetHashCode());

        // Act
        int actualHashCode = pdfName1.GetHashCode();

        // Assert
        Assert.Equal(expectedHashCode, actualHashCode);
    }

    [Theory(DisplayName = "Compare the hash codes of two name objects.")]
    [InlineData("Name1", "Name1", true)]
    [InlineData("Name1", "name1", false)]
    [InlineData("Name1", "\\Name1", false)]
    public void PdfName_GetHashCode_CompareHashes(string value1, string value2, bool expectedResult)
    {
        // Arrange
        PdfName pdfName1 = value1; // Use an implicit conversion from string to PdfName
        PdfName pdfName2 = value2; // Use an implicit conversion from string to PdfName

        // Act
        int actualHashCode1 = pdfName1.GetHashCode();
        int actualHashCode2 = pdfName2.GetHashCode();
        bool areHashCodeEquals = actualHashCode1 == actualHashCode2;

        // Assert
        Assert.Equal(expectedResult, areHashCodeEquals);
    }

    [Theory(DisplayName = "Check if comparison operators works properly")]
    [InlineData("Name1", "Name1", true)]
    [InlineData("Name1", "name1", false)]
    [InlineData("Name1", "\\Name1", false)]
    public void PdfName_Operators_CheckEquals(string value1, string value2, bool expectedResult)
    {
        // Arrange
        PdfName pdfName1 = value1; // Use an implicit conversion from string to PdfName
        PdfName pdfName2 = value2; // Use an implicit conversion from string to PdfName

        // Act
        bool actualEqual = pdfName1 == pdfName2;

        // Assert
        Assert.Equal(actualEqual, expectedResult);
    }

    [Theory(DisplayName = "Check if comparison operators works properly")]
    [InlineData("Name1", "Name1", false)]
    [InlineData("Name1", "name1", true)]
    [InlineData("Name1", "\\Name1", true)]
    public void PdfName_Operators_CheckNotEquals(string value1, string value2, bool expectedResult)
    {
        // Arrange
        PdfName pdfName1 = value1; // Use an implicit conversion from string to PdfName
        PdfName pdfName2 = value2; // Use an implicit conversion from string to PdfName

        // Act
        bool actualEqual = pdfName1 != pdfName2;

        // Assert
        Assert.Equal(actualEqual, expectedResult);
    }

    [Theory(DisplayName = "Check if implicit operator works")]
    [InlineData("Name1")]
    [InlineData("ASomewhatLongerName")]
    [InlineData("A;Name_With-Various***Characters?")]
    [InlineData("1.2")]
    [InlineData("$$")]
    [InlineData("@pattern")]
    [InlineData(".notdef")]
    [InlineData("Lime Green")]
    [InlineData("paired()parentheses")]
    [InlineData("The_Key_of_F#_Minor")]
    [InlineData("/NameWithSolidus")]
    public void PdfName_CheckImplicitOperator(string value1)
    {
        // Arrange
        var pdfName1 = new PdfName(value1);

        // Act
        string actualValue = pdfName1; // Use an implicit conversion from PdfName to string
        string expectedValue = pdfName1.Value;

        // Assert
        Assert.Equal(expectedValue, actualValue);
    }

    [Theory(DisplayName = "Check Content property for equality")]
    [InlineData("Name1", "/Name1")]
    [InlineData("ASomewhatLongerName", "/ASomewhatLongerName")]
    [InlineData("A;Name_With-Various***Characters?", "/A;Name_With-Various***Characters?")]
    [InlineData("1.2", "/1.2")]
    [InlineData("$$", "/$$")]
    [InlineData("@pattern", "/@pattern")]
    [InlineData(".notdef", "/.notdef")]
    [InlineData("Lime Green", "/Lime#20Green")]
    [InlineData("paired()parentheses", "/paired#28#29parentheses")]
    [InlineData("The_Key_of_F#_Minor", "/The_Key_of_F#23_Minor")]
    [InlineData("/NameWithSolidus", "/#2FNameWithSolidus")]
    [InlineData("NameWith%Percent", "/NameWith#25Percent")]
    [InlineData("NameWith>GreaterThanChar", "/NameWith#3EGreaterThanChar")]
    [InlineData("NameWith<LessThanChar", "/NameWith#3CLessThanChar")]
    [InlineData("NameWith[LeftSquareBracket", "/NameWith#5BLeftSquareBracket")]
    [InlineData("NameWith]RightSquareBracket", "/NameWith#5DRightSquareBracket")]
    [InlineData("NameWith{LeftCurlyBracket", "/NameWith#7BLeftCurlyBracket")]
    [InlineData("NameWith}RightCurlyBracket", "/NameWith#7DRightCurlyBracket")]
    [InlineData("NameWith\x007FDeleteChar", "/NameWith#7FDeleteChar")]
    public void PdfName_Content_CheckEquality(string value1, string expectedPdfNameStringValue)
    {
        // Arrange
        PdfName pdfName1 = value1; // Use an implicit conversion from string to PdfName

        // Act
        string actualPdfNameStringValue = pdfName1.Content;

        // Assert
        Assert.Equal(expectedPdfNameStringValue, actualPdfNameStringValue);
    }

    [Fact(DisplayName = "Check Content property for multiple accessing")]
    public void PdfName_Content_CheckMultipleAccessing()
    {
        // Arrange
        PdfName pdfName1 = "CustomName"; // Use an implicit conversion from string to PdfName

        // Act
        string firstString = pdfName1.Content;
        string secondString = pdfName1.Content;

        // Assert
        Assert.Equal(firstString, secondString);
        Assert.True(ReferenceEquals(firstString, secondString));
    }
}

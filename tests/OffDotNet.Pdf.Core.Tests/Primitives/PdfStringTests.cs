// <copyright file="PdfStringTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Diagnostics.CodeAnalysis;
using OffDotNet.Pdf.Core.Primitives;
using Xunit;

namespace OffDotNet.Pdf.Core.Tests.Primitives;

public class PdfStringTests
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
    public void PdfString_ParameterizedConstructor_CheckValue(string inputValue, string expectedValue)
    {
        // Arrange
        PdfString pdfString = inputValue; // Use an implicit conversion from string to PdfString

        // Act

        // Assert
        Assert.Equal(expectedValue, pdfString.Value);
    }

    [Theory(DisplayName = "Check the length of the PDF string primitive")]
    [InlineData("Name1", false, 7)] // 5 characters + 2 parentheses
    [InlineData("ASomewhatLongerName", false, 21)] // 19 characters + 2 parentheses
    [InlineData("A;Name_With-Various***Characters?", false, 35)] // 33 characters + 2 parentheses
    [InlineData("1.2", false, 5)] // 3 characters + 2 parentheses
    [InlineData("$$", false, 4)] // 2 characters + 2 parentheses
    [InlineData("@pattern", false, 10)] // 8 characters + 2 parentheses
    [InlineData(".notdef", false, 9)] // 7 characters + 2 parentheses
    [InlineData("Lime Green", false, 12)] // 10 characters + 2 parentheses
    [InlineData("paired()parentheses", false, 21)] // 19 characters + 2 parentheses
    [InlineData("The_Key_of_F#_Minor", false, 21)] // 19 characters + 2 parentheses
    [InlineData("/NameWithSolidus", false, 18)] // 16 characters + 2 parentheses
    [InlineData("\t90\n1F\rA3\r\n\f", true, 14)] // 12 characters + 2 angled brackets
    [InlineData("901FA", true, 7)] // 5 characters + 2 angled brackets
    public void PdfString_Length_CheckValue(string value, bool isHexString, int expectedLength)
    {
        // Arrange
        PdfString pdfString = new(value, isHexString);

        // Act
        int actualLength = pdfString.Length;

        // Assert
        Assert.Equal(expectedLength, actualLength);
    }

    [Fact(DisplayName = "Check Equals method if the argument is null")]
    public void PdfString_Equals_NullArgument_ShouldReturnFalse()
    {
        // Arrange
        PdfString pdfString1 = "Name1"; // Use an implicit conversion from string to PdfString

        // Act
        bool actualResult = pdfString1.Equals(null);

        // Assert
        Assert.False(actualResult);
    }

    [Fact(DisplayName = "Check Equals method if the argument is null")]
    public void PdfString_Equals2_NullArgument_ShouldReturnFalse()
    {
        // Arrange
        PdfString pdfString1 = "Name1"; // Use an implicit conversion from string to PdfString

        // Act
        bool actualResult = pdfString1.Equals((object?)null);

        // Assert
        Assert.False(actualResult);
    }

    [Theory(DisplayName = "Check Equals method with object type as an argument")]
    [InlineData("Name1", "Name1", true)]
    [InlineData("Name1", "name1", false)]
    [InlineData("Name1", "Name2", false)]
    public void PdfString_Equality_CheckEquals(string value1, string value2, bool expectedResult)
    {
        // Arrange
        PdfString pdfString1 = value1; // Use an implicit conversion from string to PdfString
        PdfString pdfString2 = value2; // Use an implicit conversion from string to PdfString

        // Act
        bool actualResult = pdfString1.Equals((object)pdfString2);

        // Assert
        Assert.Equal(expectedResult, actualResult);
    }

    [Theory(DisplayName = "Check if Bytes property returns valid data")]
    [InlineData("Name1", new byte[] { 40, 78, 97, 109, 101, 49, 41 })]
    [InlineData("ASomewhatLongerName", new byte[] { 40, 65, 83, 111, 109, 101, 119, 104, 97, 116, 76, 111, 110, 103, 101, 114, 78, 97, 109, 101, 41 })]
    [InlineData(
        "A;Name_With-Various***Characters?",
        new byte[] { 40, 65, 59, 78, 97, 109, 101, 95, 87, 105, 116, 104, 45, 86, 97, 114, 105, 111, 117, 115, 42, 42, 42, 67, 104, 97, 114, 97, 99, 116, 101, 114, 115, 63, 41 })]
    [InlineData("1.2", new byte[] { 40, 49, 46, 50, 41 })]
    [InlineData("$$", new byte[] { 40, 36, 36, 41 })]
    [InlineData("@pattern", new byte[] { 40, 64, 112, 97, 116, 116, 101, 114, 110, 41 })]
    [InlineData(".notdef", new byte[] { 40, 46, 110, 111, 116, 100, 101, 102, 41 })]
    [InlineData("Lime Green", new byte[] { 40, 76, 105, 109, 101, 32, 71, 114, 101, 101, 110, 41 })]
    [InlineData("paired()parentheses", new byte[] { 40, 112, 97, 105, 114, 101, 100, 40, 41, 112, 97, 114, 101, 110, 116, 104, 101, 115, 101, 115, 41 })]
    [InlineData("The_Key_of_F#_Minor", new byte[] { 40, 84, 104, 101, 95, 75, 101, 121, 95, 111, 102, 95, 70, 35, 95, 77, 105, 110, 111, 114, 41 })]
    [InlineData("/NameWithSolidus", new byte[] { 40, 47, 78, 97, 109, 101, 87, 105, 116, 104, 83, 111, 108, 105, 100, 117, 115, 41 })]
    public void PdfString_Bytes_CheckValidity(string value1, byte[] expectedBytes)
    {
        // Arrange
        PdfString pdfString1 = value1; // Use an implicit conversion from string to PdfString

        // Act
        ReadOnlyMemory<byte> actualBytes = pdfString1.Bytes;

        // Assert
        Assert.True(actualBytes.Span.SequenceEqual(expectedBytes));
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
    public void PdfString_GetHashCode_CheckValidity(string value1)
    {
        // Arrange
        PdfString pdfString1 = value1; // Use an implicit conversion from string to PdfString
        int expectedHashCode = HashCode.Combine(nameof(PdfString), value1);

        // Act
        int actualHashCode = pdfString1.GetHashCode();

        // Assert
        Assert.Equal(expectedHashCode, actualHashCode);
    }

    [Theory(DisplayName = "Compare the hash codes of two name objects.")]
    [InlineData("Name1", "Name1", true)]
    [InlineData("Name1", "name1", false)]
    [InlineData("Name1", "Name2", false)]
    public void PdfString_GetHashCode_CompareHashes(string value1, string value2, bool expectedResult)
    {
        // Arrange
        PdfString pdfString1 = value1; // Use an implicit conversion from string to PdfString
        PdfString pdfString2 = value2; // Use an implicit conversion from string to PdfString

        // Act
        int actualHashCode1 = pdfString1.GetHashCode();
        int actualHashCode2 = pdfString2.GetHashCode();
        bool areHashCodeEquals = actualHashCode1 == actualHashCode2;

        // Assert
        Assert.Equal(expectedResult, areHashCodeEquals);
    }

    [Theory(DisplayName = "Check if comparison operators works properly")]
    [InlineData("Name1", "Name1", true)]
    [InlineData("Name1", "name1", false)]
    [InlineData("Name1", "Name2", false)]
    public void PdfString_Operators_CheckEquals(string value1, string value2, bool expectedResult)
    {
        // Arrange
        PdfString pdfString1 = value1; // Use an implicit conversion from string to PdfString
        PdfString pdfString2 = value2; // Use an implicit conversion from string to PdfString

        // Act
        bool actualEqual = pdfString1 == pdfString2;

        // Assert
        Assert.Equal(actualEqual, expectedResult);
    }

    [Theory(DisplayName = "Check if comparison operators works properly")]
    [InlineData("Name1", "Name1", false)]
    [InlineData("Name1", "name1", true)]
    [InlineData("Name1", "Name2", true)]
    public void PdfString_Operators_CheckNotEquals(string value1, string value2, bool expectedResult)
    {
        // Arrange
        PdfString pdfString1 = value1; // Use an implicit conversion from string to PdfString
        PdfString pdfString2 = value2; // Use an implicit conversion from string to PdfString

        // Act
        bool actualEqual = pdfString1 != pdfString2;

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
    public void PdfString_CheckImplicitOperator(string value1)
    {
        // Arrange
        PdfString pdfString1 = new(value1);

        // Act
        string actualValue = pdfString1; // Use an implicit conversion from PdfString to string
        string expectedValue = pdfString1.Value;

        // Assert
        Assert.Equal(expectedValue, actualValue);
    }

    [Theory(DisplayName = "Check Content property for equality")]
    [InlineData("This is a string", "(This is a string)", false)]
    [InlineData("Strings may contain newlines\r\nand such.", "(Strings may contain newlines\r\nand such.)", false)]
    [InlineData("", "()", false)]
    [InlineData("These \\\r\ntwo strings \\\r\nare the same.", "(These \\\r\ntwo strings \\\r\nare the same.)", false)]
    [InlineData("These \\\ntwo strings \\\rare the same.", "(These \\\ntwo strings \\\rare the same.)", false)]
    [InlineData("This string has an end-of-line at the end of it.\r\n", "(This string has an end-of-line at the end of it.\r\n)", false)]
    [InlineData("So does this one.\\\n", "(So does this one.\\\n)", false)]
    [InlineData("This string contains \\009two octal characters\\907.", "(This string contains \\009two octal characters\\907.)", false)]
    [InlineData("4E6F762073686D6F7A206B6120706F702E", "<4E6F762073686D6F7A206B6120706F702E>", true)]
    [InlineData("901FA3", "<901FA3>", true)]
    [InlineData("90 1F A3", "<90 1F A3>", true)]
    [InlineData("\t90\n1F\rA3\r\n\f", "<\t90\n1F\rA3\r\n\f>", true)] // White-spaces should be ignored in hex string
    [InlineData("901FA", "<901FA>", true)] // If the last digit is missing, the last digit is considered 0, i.e. 901FA0
    [InlineData("901fa", "<901fa>", true)] // If the last digit is missing, the last digit is considered 0, i.e. 901fa0
    [InlineData(
        "Strings may contain balanced parentheses ( ) and\r\nspecial characters (*!&}^% and so on).",
        "(Strings may contain balanced parentheses ( ) and\r\nspecial characters (*!&}^% and so on).)",
        false)]
    public void PdfString_Content_CheckEquality(string value1, string expectedPdfStringStringValue, bool isHexValue)
    {
        // Arrange
        PdfString pdfString1 = new(value1, isHexValue);

        // Act
        string actualPdfStringStringValue = pdfString1.Content;

        // Assert
        Assert.Equal(expectedPdfStringStringValue, actualPdfStringStringValue);
    }

    [Fact(DisplayName = "Check Content property for multiple accessing")]
    public void PdfString_Content_CheckMultipleAccessing()
    {
        // Arrange
        PdfString pdfString1 = "CustomString"; // Use an implicit conversion from string to PdfString

        // Act
        string firstString = pdfString1.Content;
        string secondString = pdfString1.Content;

        // Assert
        Assert.Equal(firstString, secondString);
        Assert.True(ReferenceEquals(firstString, secondString));
    }

    [Theory(DisplayName = "Check if constructor will throw an exception when solidus char is not followed by newline char")]
    [InlineData("Solidus without \\ new line symbol should throw exception.")]
    [InlineData("Solidus without new line symbol at the end of the line.\\")]
    public void PdfString_Constructor_SolidusCharWithoutNewLineSymbol_ShouldThrowException(string value1)
    {
        // Arrange

        // Act
        PdfString PdfStringDelegate()
        {
            return new(value1);
        }

        // Assert
        Assert.Throws<ArgumentException>(PdfStringDelegate);
    }

    [Theory(DisplayName = "Check if string with unbalanced parentheses will throw an exception")]
    [InlineData("Strings with unbalanced parentheses (( ( ), should throw an exception.")]
    [InlineData("Strings with unbalanced parentheses ))((, should throw an exception.")]
    [SuppressMessage("Major Code Smell", "S4144:Methods should not have identical implementations", Justification = "This is a different test")]
    public void PdfString_Constructor_UnbalancedParentheses_ShouldThrowException(string value1)
    {
        // Arrange

        // Act
        PdfString PdfStringDelegate()
        {
            return new(value1);
        }

        // Assert
        Assert.Throws<ArgumentException>(PdfStringDelegate);
    }

    [Theory(DisplayName = "Check if constructor will throw an exception when invalid Hex value is provided")]
    [InlineData("invalid_hex_chars")]
    [InlineData("FF99H")]
    public void PdfString_Constructor_InvalidHexValue_ShouldThrowArgumentException(string value1)
    {
        // Arrange

        // Act
        PdfString PdfStringDelegate()
        {
            return new(value1, isHexString: true);
        }

        // Assert
        Assert.Throws<ArgumentException>(PdfStringDelegate);
    }

    [Fact(DisplayName = "Check if constructor will throw an exception when invalid Hex value is provided")]
    public void PdfString_Constructor_InvalidHexValue_ShouldThrowArgumentNullException()
    {
        // Arrange

        // Act
        static PdfString PdfStringDelegate()
        {
            return new(string.Empty, true);
        }

        // Assert
        Assert.Throws<ArgumentNullException>(PdfStringDelegate);
    }
}

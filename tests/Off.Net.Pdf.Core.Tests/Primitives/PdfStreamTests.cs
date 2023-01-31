﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Off.Net.Pdf.Core.Interfaces;
using Off.Net.Pdf.Core.Primitives;
using Xunit;

namespace Off.Net.Pdf.Core.Tests.Primitives;

public class PdfStreamTests
{
    [Fact(DisplayName = $"Constructor with null options should return one {nameof(PdfStream.StreamExtent)} item")]
    public void PdfStream_ConstructorWithNullOptions_ShouldReturnOneStreamExtentItem()
    {
        // Arrange
        PdfStream pdfStream = new PdfString("Test").ToPdfStream();
        const int expectedDictionaryCount = 1;
        const string expectedKeyName = "Length";

        // Act
        PdfDictionary actualStreamExtent = pdfStream.StreamExtent;
        string? actualKeyName = actualStreamExtent.Value.Select(x => x.Key.Value).FirstOrDefault();

        // Assert
        Assert.Equal(expectedDictionaryCount, actualStreamExtent.Value.Count);
        Assert.Equal(expectedKeyName, actualKeyName);
    }

    [Theory(DisplayName = $"Constructor with filter option should return a valid {nameof(PdfStream.StreamExtent)} dictionary")]
    [InlineData("ASCIIHexDecode")]
    [InlineData("ASCII85Decode")]
    [InlineData("LZWDecode")]
    [InlineData("FlateDecode")]
    [InlineData("RunLengthDecode")]
    [InlineData("CCITTFaxDecode")]
    [InlineData("JBIG2Decode")]
    [InlineData("DCTDecode")]
    [InlineData("JPXDecode")]
    [InlineData("Crypt")]
    public void PdfStream_ConstructorWithFilter_ShouldReturnValidStreamExtend(string filterName)
    {
        // Arrange
        PdfName pdfOptionName = new(filterName);
        PdfStream pdfStream = new PdfString("Test").ToPdfStream(options => options.Filter = pdfOptionName);
        const int expectedDictionaryCount = 2; // Count + Current extent option
        const string expectedLengthKeyName = "Length";
        const string expectedOptionKeyName = "Filter";

        // Act
        PdfDictionary actualStreamExtent = pdfStream.StreamExtent;
        bool isLengthInDictionary = actualStreamExtent.Value.TryGetValue(expectedLengthKeyName, out IPdfObject _);
        bool isOptionInDictionary = actualStreamExtent.Value.TryGetValue(expectedOptionKeyName, out IPdfObject? optionValue);

        // Assert
        Assert.Equal(expectedDictionaryCount, actualStreamExtent.Value.Count);
        Assert.True(isLengthInDictionary);
        Assert.True(isOptionInDictionary);
        PdfName actualPdfName = Assert.IsType<PdfName>(optionValue);
        Assert.Equal(filterName, actualPdfName.Value);
    }

    [Theory(DisplayName = $"Constructor with file filter option should return a valid {nameof(PdfStream.StreamExtent)} dictionary")]
    [InlineData("ASCIIHexDecode")]
    [InlineData("ASCII85Decode")]
    [InlineData("LZWDecode")]
    [InlineData("FlateDecode")]
    [InlineData("RunLengthDecode")]
    [InlineData("CCITTFaxDecode")]
    [InlineData("JBIG2Decode")]
    [InlineData("DCTDecode")]
    [InlineData("JPXDecode")]
    [InlineData("Crypt")]
    public void PdfStream_ConstructorWithFileFilter_ShouldReturnValidStreamExtend(string fileFilterName)
    {
        // Arrange
        PdfName pdfOptionName = new(fileFilterName);
        PdfStream pdfStream = new PdfString("Test").ToPdfStream(options => options.FileFilter = new PdfName(fileFilterName));
        const int expectedDictionaryCount = 2; // Count + Current extent option
        const string expectedLengthKeyName = "Length";
        const string expectedOptionKeyName = "FFilter";

        // Act
        PdfDictionary actualStreamExtent = pdfStream.StreamExtent;
        bool isLengthInDictionary = actualStreamExtent.Value.TryGetValue(expectedLengthKeyName, out IPdfObject _);
        bool isOptionInDictionary = actualStreamExtent.Value.TryGetValue(expectedOptionKeyName, out IPdfObject? optionValue);

        // Assert
        Assert.Equal(expectedDictionaryCount, actualStreamExtent.Value.Count);
        Assert.True(isLengthInDictionary);
        Assert.True(isOptionInDictionary);
        PdfName actualPdfName = Assert.IsType<PdfName>(optionValue);
        Assert.Equal(fileFilterName, actualPdfName.Value);
    }

    [Theory(DisplayName = $"Constructor with array filter option should return a valid {nameof(PdfStream.StreamExtent)} dictionary")]
    [InlineData(new object[] { new[] { "ASCIIHexDecode", "ASCII85Decode", "LZWDecode" } })]
    [InlineData(new object[] { new[] { "FlateDecode", "RunLengthDecode", "CCITTFaxDecode" } })]
    [InlineData(new object[] { new[] { "JBIG2Decode", "DCTDecode", "JPXDecode" } })]
    [InlineData(new object[] { new[] { "Crypt" } })]
    public void PdfStream_ConstructorWithArrayFilter_ShouldReturnValidStreamExtend(string[] filterNames)
    {
        // Arrange
        IEnumerable<PdfName> pdfNames = filterNames.Select(filterName => new PdfName(filterName));
        PdfArray pdfOptionNames = pdfNames.ToPdfArray();
        PdfStream pdfStream = new PdfString("Test").ToPdfStream(options => options.Filter = pdfOptionNames);
        const int expectedDictionaryCount = 2; // Count + Current extent option
        const string expectedLengthKeyName = "Length";
        const string expectedOptionKeyName = "Filter";

        // Act
        PdfDictionary actualStreamExtent = pdfStream.StreamExtent;
        bool isLengthInDictionary = actualStreamExtent.Value.TryGetValue(expectedLengthKeyName, out IPdfObject _);
        bool isOptionInDictionary = actualStreamExtent.Value.TryGetValue(expectedOptionKeyName, out IPdfObject? optionValue);

        // Assert
        Assert.Equal(expectedDictionaryCount, actualStreamExtent.Value.Count);
        Assert.True(isLengthInDictionary);
        Assert.True(isOptionInDictionary);
        PdfArray actualPdfArray = Assert.IsType<PdfArray>(optionValue);
        Assert.Equal(filterNames.Length, actualPdfArray.Value.Count);
        Assert.Equal(pdfOptionNames.Value, actualPdfArray.Value);
    }

    [Theory(DisplayName = $"Constructor with array file filter option should return a valid {nameof(PdfStream.StreamExtent)} dictionary")]
    [InlineData(new object[] { new[] { "ASCIIHexDecode", "ASCII85Decode", "LZWDecode" } })]
    [InlineData(new object[] { new[] { "FlateDecode", "RunLengthDecode", "CCITTFaxDecode" } })]
    [InlineData(new object[] { new[] { "JBIG2Decode", "DCTDecode", "JPXDecode" } })]
    [InlineData(new object[] { new[] { "Crypt" } })]
    public void PdfStream_ConstructorWithArrayFileFilter_ShouldReturnValidStreamExtend(string[] fileFilterNames)
    {
        // Arrange
        IEnumerable<PdfName> pdfNames = fileFilterNames.Select(filterName => new PdfName(filterName));
        PdfArray pdfOptionNames = pdfNames.ToPdfArray();
        PdfStream pdfStream = new PdfString("Test").ToPdfStream(options => options.FileFilter = pdfOptionNames);
        const int expectedDictionaryCount = 2; // Count + Current extent option
        const string expectedLengthKeyName = "Length";
        const string expectedOptionKeyName = "FFilter";

        // Act
        PdfDictionary actualStreamExtent = pdfStream.StreamExtent;
        bool isLengthInDictionary = actualStreamExtent.Value.TryGetValue(expectedLengthKeyName, out IPdfObject _);
        bool isOptionInDictionary = actualStreamExtent.Value.TryGetValue(expectedOptionKeyName, out IPdfObject? optionValue);

        // Assert
        Assert.Equal(expectedDictionaryCount, actualStreamExtent.Value.Count);
        Assert.True(isLengthInDictionary);
        Assert.True(isOptionInDictionary);
        PdfArray actualPdfArray = Assert.IsType<PdfArray>(optionValue);
        Assert.Equal(fileFilterNames.Length, actualPdfArray.Value.Count);
        Assert.Equal(pdfOptionNames.Value, actualPdfArray.Value);
    }

    [Theory(DisplayName = $"Constructor with decode parameter array option should return a valid {nameof(PdfStream.StreamExtent)} dictionary")]
    [InlineData(new object[] { new[] { "Predictor", "Colors" } })]
    [InlineData(new object[] { new[] { "Columns", "EarlyChange" } })]
    public void PdfStream_ConstructorWithDecodeParameters_ShouldReturnValidStreamExtend(string[] decodeParameters)
    {
        // Arrange
        IEnumerable<PdfName> pdfNames = decodeParameters.Select(filterName => new PdfName(filterName));
        PdfArray pdfOptionNames = pdfNames.ToPdfArray();
        PdfStream pdfStream = new PdfString("Test").ToPdfStream(options => options.DecodeParameters = pdfOptionNames);
        const int expectedDictionaryCount = 2; // Count + Current extent option
        const string expectedLengthKeyName = "Length";
        const string expectedOptionKeyName = "DecodeParms";

        // Act
        PdfDictionary actualStreamExtent = pdfStream.StreamExtent;
        bool isLengthInDictionary = actualStreamExtent.Value.TryGetValue(expectedLengthKeyName, out IPdfObject _);
        bool isOptionInDictionary = actualStreamExtent.Value.TryGetValue(expectedOptionKeyName, out IPdfObject? optionValue);

        // Assert
        Assert.Equal(expectedDictionaryCount, actualStreamExtent.Value.Count);
        Assert.True(isLengthInDictionary);
        Assert.True(isOptionInDictionary);
        PdfArray actualPdfArray = Assert.IsType<PdfArray>(optionValue);
        Assert.Equal(decodeParameters.Length, actualPdfArray.Value.Count);
        Assert.Equal(pdfOptionNames.Value, actualPdfArray.Value);
    }

    [Theory(DisplayName = $"Constructor with file decode parameter array option should return a valid {nameof(PdfStream.StreamExtent)} dictionary")]
    [InlineData(new object[] { new[] { "Predictor", "Colors" } })]
    [InlineData(new object[] { new[] { "Columns", "EarlyChange" } })]
    public void PdfStream_ConstructorWithFileDecodeParameters_ShouldReturnValidStreamExtend(string[] decodeParameters)
    {
        // Arrange
        IEnumerable<PdfName> pdfNames = decodeParameters.Select(filterName => new PdfName(filterName));
        PdfArray pdfOptionNames = pdfNames.ToPdfArray();
        PdfStream pdfStream = new PdfString("Test").ToPdfStream(options => options.FileDecodeParameters = pdfOptionNames);
        const int expectedDictionaryCount = 2; // Count + Current extent option
        const string expectedLengthKeyName = "Length";
        const string expectedOptionKeyName = "FDecodeParms";

        // Act
        PdfDictionary actualStreamExtent = pdfStream.StreamExtent;
        bool isLengthInDictionary = actualStreamExtent.Value.TryGetValue(expectedLengthKeyName, out IPdfObject _);
        bool isOptionInDictionary = actualStreamExtent.Value.TryGetValue(expectedOptionKeyName, out IPdfObject? optionValue);

        // Assert
        Assert.Equal(expectedDictionaryCount, actualStreamExtent.Value.Count);
        Assert.True(isLengthInDictionary);
        Assert.True(isOptionInDictionary);
        PdfArray actualPdfArray = Assert.IsType<PdfArray>(optionValue);
        Assert.Equal(decodeParameters.Length, actualPdfArray.Value.Count);
        Assert.Equal(pdfOptionNames.Value, actualPdfArray.Value);
    }

    [Fact(DisplayName = $"Constructor with decode parameter dictionary option should return a valid {nameof(PdfStream.StreamExtent)} dictionary")]
    public void PdfStream_ConstructorWithDecodeParametersDictionary_ShouldReturnValidStreamExtend()
    {
        // Arrange
        IDictionary<PdfName, IPdfObject> decodeParameters = new Dictionary<PdfName, IPdfObject> { { "Colors", new PdfName("None") } };
        PdfDictionary pdfOptionNames = decodeParameters.ToPdfDictionary();
        PdfStream pdfStream = new PdfString("Test").ToPdfStream(options => options.DecodeParameters = pdfOptionNames);
        const int expectedDictionaryCount = 2; // Count + Current extent option
        const string expectedLengthKeyName = "Length";
        const string expectedOptionKeyName = "DecodeParms";

        // Act
        PdfDictionary actualStreamExtent = pdfStream.StreamExtent;
        bool isLengthInDictionary = actualStreamExtent.Value.TryGetValue(expectedLengthKeyName, out IPdfObject _);
        bool isOptionInDictionary = actualStreamExtent.Value.TryGetValue(expectedOptionKeyName, out IPdfObject? optionValue);

        // Assert
        Assert.Equal(expectedDictionaryCount, actualStreamExtent.Value.Count);
        Assert.True(isLengthInDictionary);
        Assert.True(isOptionInDictionary);
        PdfDictionary actualPdfDictionary = Assert.IsType<PdfDictionary>(optionValue);
        Assert.Equal(decodeParameters.Count, actualPdfDictionary.Value.Count);
        Assert.Equal(pdfOptionNames.Value, actualPdfDictionary.Value);
    }

    [Fact(DisplayName = $"Constructor with file decode parameter dictionary option should return a valid {nameof(PdfStream.StreamExtent)} dictionary")]
    public void PdfStream_ConstructorWithFileDecodeParametersDictionary_ShouldReturnValidStreamExtend()
    {
        // Arrange
        IDictionary<PdfName, IPdfObject> decodeParameters = new Dictionary<PdfName, IPdfObject> { { "Colors", new PdfName("None") } };
        PdfDictionary pdfOptionNames = decodeParameters.ToPdfDictionary();
        PdfStream pdfStream = new PdfString("Test").ToPdfStream(options => options.FileDecodeParameters = pdfOptionNames);
        const int expectedDictionaryCount = 2; // Count + Current extent option
        const string expectedLengthKeyName = "Length";
        const string expectedOptionKeyName = "FDecodeParms";

        // Act
        PdfDictionary actualStreamExtent = pdfStream.StreamExtent;
        bool isLengthInDictionary = actualStreamExtent.Value.TryGetValue(expectedLengthKeyName, out IPdfObject _);
        bool isOptionInDictionary = actualStreamExtent.Value.TryGetValue(expectedOptionKeyName, out IPdfObject? optionValue);

        // Assert
        Assert.Equal(expectedDictionaryCount, actualStreamExtent.Value.Count);
        Assert.True(isLengthInDictionary);
        Assert.True(isOptionInDictionary);
        PdfDictionary actualPdfDictionary = Assert.IsType<PdfDictionary>(optionValue);
        Assert.Equal(decodeParameters.Count, actualPdfDictionary.Value.Count);
        Assert.Equal(pdfOptionNames.Value, actualPdfDictionary.Value);
    }

    [Theory(DisplayName = $"Constructor with file file specification option should return a valid {nameof(PdfStream.StreamExtent)} dictionary")]
    [InlineData("1.txt")]
    [InlineData("abc.txt")]
    [InlineData("document.docx")]
    public void PdfStream_ConstructorWithFileSpecification_ShouldReturnValidStreamExtend(string fileSpecification)
    {
        // Arrange
        PdfStream pdfStream = new PdfString("Test").ToPdfStream(options => options.FileSpecification = fileSpecification);
        const int expectedDictionaryCount = 2; // Count + Current extent option
        const string expectedLengthKeyName = "Length";
        const string expectedOptionKeyName = "F";

        // Act
        PdfDictionary actualStreamExtent = pdfStream.StreamExtent;
        bool isLengthInDictionary = actualStreamExtent.Value.TryGetValue(expectedLengthKeyName, out IPdfObject _);
        bool isOptionInDictionary = actualStreamExtent.Value.TryGetValue(expectedOptionKeyName, out IPdfObject? optionValue);

        // Assert
        Assert.Equal(expectedDictionaryCount, actualStreamExtent.Value.Count);
        Assert.True(isLengthInDictionary);
        Assert.True(isOptionInDictionary);
        PdfString actualPdfString = Assert.IsType<PdfString>(optionValue);
        Assert.Equal(fileSpecification, actualPdfString.Value);
    }

    [Fact(DisplayName = $"{nameof(PdfStream.StreamExtent)} property, accessed multiple times, should return the same reference")]
    public void PdfStream_StreamExtent_MultipleAccesses_ShouldReturnSameReference()
    {
        // Arrange
        PdfStream pdfStream = new PdfString("Test").ToPdfStream();

        // Act
        PdfDictionary actualStreamExtent1 = pdfStream.StreamExtent;
        PdfDictionary actualStreamExtent2 = pdfStream.StreamExtent;

        // Assert
        Assert.True(ReferenceEquals(actualStreamExtent1, actualStreamExtent2));
    }

    [Theory(DisplayName = $"{nameof(PdfStream.Content)} property should return a valid value")]
    [InlineData("This is a PDF String wrapped in a Stream object")]
    [InlineData($"It should return a valid {nameof(PdfStream.Content)} property")]
    [InlineData("That will have the following format:")]
    [InlineData("StreamExtent dictionary + \n + stream + \n <content_byte_array> + \n + endstream")]
    public void PdfStream_Content_ShouldReturnValidValue(string pdfStringValue)
    {
        // Arrange
        PdfString pdfString = new(pdfStringValue);
        PdfStream pdfStream = pdfString.ToPdfStream();
        int expectedStreamExtentLengthValue = pdfString.Length + 17; // Stream content itself + wrapper content (stream + \n <content_byte_array> + \n + endstream)
        string expectedStringContent = $"<</Length {expectedStreamExtentLengthValue}>>\nstream\n{pdfString.Content}\nendstream";

        // Act
        string actualStreamContent = pdfStream.Content;

        // Assert
        Assert.Equal(expectedStringContent, actualStreamContent);
    }

    [Fact(DisplayName = $"{nameof(PdfStream.Content)} property, accessed multiple times, should return the same reference")]
    public void PdfStream_Content_MultipleAccesses_ShouldReturnSameReference()
    {
        // Arrange
        PdfStream pdfStream = new PdfString("Test").ToPdfStream();

        // Act
        string actualStreamContent1 = pdfStream.Content;
        string actualStreamContent2 = pdfStream.Content;

        // Assert
        Assert.True(ReferenceEquals(actualStreamContent1, actualStreamContent2));
    }

    [Theory(DisplayName = $"{nameof(PdfStream.Length)} property should return a valid value")]
    [InlineData("This is a PDF String wrapped in a Stream object")]
    [InlineData($"It should return a valid {nameof(PdfStream.Length)} property")]
    [InlineData($"It should include {nameof(PdfStream.StreamExtent)} dictionary's length and the byte array value's length")]
    public void PdfStream_Length_ShouldReturnValidValue(string pdfStringValue)
    {
        // Arrange
        PdfString pdfString = new(pdfStringValue);
        PdfStream pdfStream = pdfString.ToPdfStream();
        int expectedStreamExtentLengthValue = pdfString.Length + 17; // Stream content itself + wrapper content (stream + \n <content_byte_array> + \n + endstream)
        int expectedStreamExtentStringLengthValue = expectedStreamExtentLengthValue.ToString(CultureInfo.InvariantCulture).Length;
        int expectedLength = 13 + expectedStreamExtentLengthValue + expectedStreamExtentStringLengthValue;

        // Act
        int actualLength = pdfStream.Length;

        // Assert
        Assert.Equal(expectedLength, actualLength);
    }

    [Theory(DisplayName = $"{nameof(PdfStream.Bytes)} property should return a valid value")]
    [MemberData(nameof(PdfStreamTestDataGenerator.PdfStream_Bytes_TestCases), MemberType = typeof(PdfStreamTestDataGenerator))]
    public void PdfStream_Bytes_ShouldReturnValidValue(string pdfStringValue, byte[] expectedBytes)
    {
        // Arrange
        PdfStream pdfStream = new PdfString(pdfStringValue).ToPdfStream();

        // Act
         ReadOnlyMemory<byte> actualBytes = pdfStream.Bytes;

        // Assert
        Assert.True(actualBytes.Span.SequenceEqual(expectedBytes));
    }

    [Fact(DisplayName = "Check Equals method if the argument is null")]
    public void PdfStream_Equals_NullArgument_ShouldReturnFalse()
    {
        // Arrange
        PdfStream pdfStream = new PdfString("Test").ToPdfStream();

        // Act
        bool actualResult = pdfStream.Equals(null);

        // Assert
        Assert.False(actualResult);
    }

    [Fact(DisplayName = "Check Equals method if the argument is null")]
    public void PdfStream_Equals2_NullArgument_ShouldReturnFalse()
    {
        // Arrange
        PdfStream pdfStream = new PdfString("Test").ToPdfStream();

        // Act
        bool actualResult = pdfStream.Equals((object?)null);

        // Assert
        Assert.False(actualResult);
    }

    [Fact(DisplayName = "Check Equals method that has the same reference as a value")]
    public void PdfStream_Equals_SameReference_ShouldReturnTrue()
    {
        // Arrange
        PdfStream pdfStream1 = new PdfString("Test").ToPdfStream();
        PdfStream pdfStream2 = pdfStream1;

        // Act
        bool actualResult = pdfStream1.Equals(pdfStream2);

        // Assert
        Assert.True(actualResult);
    }

    [Fact(DisplayName = "Check Equals method that has different references as values")]
    public void PdfStream_Equals_DifferentReferences_ShouldReturnFalse()
    {
        // Arrange
        PdfStream pdfStream1 = new PdfString("Test").ToPdfStream();
        PdfStream pdfStream2 = new PdfString("Test").ToPdfStream();

        // Act
        bool actualResult = pdfStream1.Equals(pdfStream2);

        // Assert
        Assert.False(actualResult);
    }

    [Fact(DisplayName = "Check if GetHashCode method returns valid value")]
    public void PdfStream_GetHashCode_CheckValidity()
    {
        // Arrange
        PdfStream pdfStream = new PdfString("Test").ToPdfStream();
        int expectedHashCode = HashCode.Combine(nameof(PdfStream).GetHashCode(), pdfStream.Value.GetHashCode());

        // Act
        int actualHashCode = pdfStream.GetHashCode();

        // Assert
        Assert.Equal(expectedHashCode, actualHashCode);
    }
}

internal static class PdfStreamTestDataGenerator
{
    public static IEnumerable<object[]> PdfStream_Bytes_TestCases()
    {
        yield return new object[]
        {
            "This is a PDF String wrapped in a Stream object",
            new byte[]
            {
                60, 60, 47, 76, 101, 110, 103, 116, 104, 32, 54, 54, 62, 62, 10, 115, 116, 114, 101, 97, 109, 10, 40, 84, 104, 105, 115, 32, 105, 115, 32, 97, 32, 80, 68, 70, 32, 83, 116,
                114, 105, 110, 103, 32, 119, 114, 97, 112, 112, 101, 100, 32, 105, 110, 32, 97, 32, 83, 116, 114, 101, 97, 109, 32, 111, 98, 106, 101, 99, 116, 41, 10, 101, 110, 100, 115,
                116, 114, 101, 97, 109
            }
        };

        yield return new object[]
        {
            $"It should return a valid {nameof(PdfStream.Bytes)} property",
            new byte[]
            {
                60, 60, 47, 76, 101, 110, 103, 116, 104, 32, 53, 56, 62, 62, 10, 115, 116, 114, 101, 97, 109, 10, 40, 73, 116, 32, 115, 104, 111, 117, 108, 100, 32, 114, 101, 116, 117, 114,
                110, 32, 97, 32, 118, 97, 108, 105, 100, 32, 66, 121, 116, 101, 115, 32, 112, 114, 111, 112, 101, 114, 116, 121, 41, 10, 101, 110, 100, 115, 116, 114, 101, 97, 109

            }
        };
    }
}
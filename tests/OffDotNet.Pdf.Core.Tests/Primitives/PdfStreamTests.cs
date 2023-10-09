// <copyright file="PdfStreamTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.Core.Common;
using OffDotNet.Pdf.Core.Primitives;
using Xunit;

namespace OffDotNet.Pdf.Core.Tests.Primitives;

public class PdfStreamTests
{
    [Fact(DisplayName = $"Constructor with null options should return one {nameof(PdfStream.StreamExtent)} item")]
    public void PdfStream_ConstructorWithNullOptions_ShouldReturnOneStreamExtentItem()
    {
        // Arrange
        IPdfStream pdfStream = new PdfString("Test").ToPdfStream();
        const string expectedKeyName = "Length";

        // Act
        IPdfDictionary<IPdfObject> actualStreamExtent = pdfStream.StreamExtent;
        string? actualKeyName = actualStreamExtent.Value.Select(x => x.Key.Value).FirstOrDefault();

        // Assert
        Assert.Single(actualStreamExtent.Value);
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
        IPdfStream pdfStream = new PdfString("Test").ToPdfStream(options => options.Filter = pdfOptionName);
        const int expectedDictionaryCount = 2; // Count + Current extent option
        const string expectedLengthKeyName = "Length";
        const string expectedOptionKeyName = "Filter";

        // Act
        IPdfDictionary<IPdfObject> actualStreamExtent = pdfStream.StreamExtent;
        bool isLengthInDictionary = actualStreamExtent.Value.TryGetValue(expectedLengthKeyName, out IPdfObject? _);
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
        IPdfStream pdfStream = new PdfString("Test").ToPdfStream(options => options.FileFilter = new PdfName(fileFilterName));
        const int expectedDictionaryCount = 2; // Count + Current extent option
        const string expectedLengthKeyName = "Length";
        const string expectedOptionKeyName = "FFilter";

        // Act
        IPdfDictionary<IPdfObject> actualStreamExtent = pdfStream.StreamExtent;
        bool isLengthInDictionary = actualStreamExtent.Value.TryGetValue(expectedLengthKeyName, out IPdfObject? _);
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
        IPdfArray<PdfName> pdfOptionNames = pdfNames.ToPdfArray();
        IPdfStream pdfStream = new PdfString("Test").ToPdfStream(options => options.Filter = new(pdfOptionNames));
        const int expectedDictionaryCount = 2; // Count + Current extent option
        const string expectedLengthKeyName = "Length";
        const string expectedOptionKeyName = "Filter";

        // Act
        IPdfDictionary<IPdfObject> actualStreamExtent = pdfStream.StreamExtent;
        bool isLengthInDictionary = actualStreamExtent.Value.TryGetValue(expectedLengthKeyName, out IPdfObject? _);
        bool isOptionInDictionary = actualStreamExtent.Value.TryGetValue(expectedOptionKeyName, out IPdfObject? optionValue);

        // Assert
        Assert.Equal(expectedDictionaryCount, actualStreamExtent.Value.Count);
        Assert.True(isLengthInDictionary);
        Assert.True(isOptionInDictionary);
        IPdfArray<PdfName> actualPdfArray = Assert.IsAssignableFrom<IPdfArray<PdfName>>(optionValue);
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
        IPdfArray<PdfName> pdfOptionNames = pdfNames.ToPdfArray();
        IPdfStream pdfStream = new PdfString("Test").ToPdfStream(options => options.FileFilter = new(pdfOptionNames));
        const int expectedDictionaryCount = 2; // Count + Current extent option
        const string expectedLengthKeyName = "Length";
        const string expectedOptionKeyName = "FFilter";

        // Act
        IPdfDictionary<IPdfObject> actualStreamExtent = pdfStream.StreamExtent;
        bool isLengthInDictionary = actualStreamExtent.Value.TryGetValue(expectedLengthKeyName, out IPdfObject? _);
        bool isOptionInDictionary = actualStreamExtent.Value.TryGetValue(expectedOptionKeyName, out IPdfObject? optionValue);

        // Assert
        Assert.Equal(expectedDictionaryCount, actualStreamExtent.Value.Count);
        Assert.True(isLengthInDictionary);
        Assert.True(isOptionInDictionary);
        IPdfArray<PdfName> actualPdfArray = Assert.IsAssignableFrom<IPdfArray<PdfName>>(optionValue);
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
        IPdfArray<PdfName> pdfOptionNames = pdfNames.ToPdfArray();
        IPdfStream pdfStream = new PdfString("Test").ToPdfStream(options => options.DecodeParameters = new(pdfOptionNames));
        const int expectedDictionaryCount = 2; // Count + Current extent option
        const string expectedLengthKeyName = "Length";
        const string expectedOptionKeyName = "DecodeParms";

        // Act
        IPdfDictionary<IPdfObject> actualStreamExtent = pdfStream.StreamExtent;
        bool isLengthInDictionary = actualStreamExtent.Value.TryGetValue(expectedLengthKeyName, out IPdfObject? _);
        bool isOptionInDictionary = actualStreamExtent.Value.TryGetValue(expectedOptionKeyName, out IPdfObject? optionValue);

        // Assert
        Assert.Equal(expectedDictionaryCount, actualStreamExtent.Value.Count);
        Assert.True(isLengthInDictionary);
        Assert.True(isOptionInDictionary);
        IPdfArray<PdfName> actualPdfArray = Assert.IsAssignableFrom<IPdfArray<PdfName>>(optionValue);
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
        IPdfArray<PdfName> pdfOptionNames = pdfNames.ToPdfArray();
        IPdfStream pdfStream = new PdfString("Test").ToPdfStream(options => options.FileDecodeParameters = new(pdfOptionNames));
        const int expectedDictionaryCount = 2; // Count + Current extent option
        const string expectedLengthKeyName = "Length";
        const string expectedOptionKeyName = "FDecodeParms";

        // Act
        IPdfDictionary<IPdfObject> actualStreamExtent = pdfStream.StreamExtent;
        bool isLengthInDictionary = actualStreamExtent.Value.TryGetValue(expectedLengthKeyName, out IPdfObject? _);
        bool isOptionInDictionary = actualStreamExtent.Value.TryGetValue(expectedOptionKeyName, out IPdfObject? optionValue);

        // Assert
        Assert.Equal(expectedDictionaryCount, actualStreamExtent.Value.Count);
        Assert.True(isLengthInDictionary);
        Assert.True(isOptionInDictionary);
        IPdfArray<PdfName> actualPdfArray = Assert.IsAssignableFrom<IPdfArray<PdfName>>(optionValue);
        Assert.Equal(decodeParameters.Length, actualPdfArray.Value.Count);
        Assert.Equal(pdfOptionNames.Value, actualPdfArray.Value);
    }

    [Fact(DisplayName = $"Constructor with decode parameter dictionary option should return a valid {nameof(PdfStream.StreamExtent)} dictionary")]
    public void PdfStream_ConstructorWithDecodeParametersDictionary_ShouldReturnValidStreamExtend()
    {
        // Arrange
        IDictionary<PdfName, PdfName> decodeParameters = new Dictionary<PdfName, PdfName> { { "Colors", new PdfName("None") } };
        IPdfDictionary<PdfName> pdfOptionNames = decodeParameters.ToPdfDictionary();
        IPdfStream pdfStream = new PdfString("Test").ToPdfStream(options => options.DecodeParameters = new(pdfOptionNames));
        const int expectedDictionaryCount = 2; // Count + Current extent option
        const string expectedLengthKeyName = "Length";
        const string expectedOptionKeyName = "DecodeParms";

        // Act
        IPdfDictionary<IPdfObject> actualStreamExtent = pdfStream.StreamExtent;
        bool isLengthInDictionary = actualStreamExtent.Value.TryGetValue(expectedLengthKeyName, out IPdfObject? _);
        bool isOptionInDictionary = actualStreamExtent.Value.TryGetValue(expectedOptionKeyName, out IPdfObject? optionValue);

        // Assert
        Assert.Equal(expectedDictionaryCount, actualStreamExtent.Value.Count);
        Assert.True(isLengthInDictionary);
        Assert.True(isOptionInDictionary);
        IPdfDictionary<PdfName> actualPdfDictionary = Assert.IsAssignableFrom<IPdfDictionary<PdfName>>(optionValue);
        Assert.Equal(decodeParameters.Count, actualPdfDictionary.Value.Count);
        Assert.Equal(pdfOptionNames.Value, actualPdfDictionary.Value);
    }

    [Fact(DisplayName = $"Constructor with file decode parameter dictionary option should return a valid {nameof(PdfStream.StreamExtent)} dictionary")]
    public void PdfStream_ConstructorWithFileDecodeParametersDictionary_ShouldReturnValidStreamExtend()
    {
        // Arrange
        IDictionary<PdfName, PdfName> decodeParameters = new Dictionary<PdfName, PdfName> { { "Colors", new PdfName("None") } };
        IPdfDictionary<PdfName> pdfOptionNames = decodeParameters.ToPdfDictionary();
        IPdfStream pdfStream = new PdfString("Test").ToPdfStream(options => options.FileDecodeParameters = new(pdfOptionNames));
        const int expectedDictionaryCount = 2; // Count + Current extent option
        const string expectedLengthKeyName = "Length";
        const string expectedOptionKeyName = "FDecodeParms";

        // Act
        IPdfDictionary<IPdfObject> actualStreamExtent = pdfStream.StreamExtent;
        bool isLengthInDictionary = actualStreamExtent.Value.TryGetValue(expectedLengthKeyName, out IPdfObject? _);
        bool isOptionInDictionary = actualStreamExtent.Value.TryGetValue(expectedOptionKeyName, out IPdfObject? optionValue);

        // Assert
        Assert.Equal(expectedDictionaryCount, actualStreamExtent.Value.Count);
        Assert.True(isLengthInDictionary);
        Assert.True(isOptionInDictionary);
        IPdfDictionary<PdfName> actualPdfDictionary = Assert.IsAssignableFrom<IPdfDictionary<PdfName>>(optionValue);
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
        IPdfStream pdfStream = new PdfString("Test").ToPdfStream(options => options.FileSpecification = fileSpecification);
        const int expectedDictionaryCount = 2; // Count + Current extent option
        const string expectedLengthKeyName = "Length";
        const string expectedOptionKeyName = "F";

        // Act
        IPdfDictionary<IPdfObject> actualStreamExtent = pdfStream.StreamExtent;
        bool isLengthInDictionary = actualStreamExtent.Value.TryGetValue(expectedLengthKeyName, out IPdfObject? _);
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
        IPdfStream pdfStream = new PdfString("Test").ToPdfStream();

        // Act
        IPdfDictionary<IPdfObject> actualStreamExtent1 = pdfStream.StreamExtent;
        IPdfDictionary<IPdfObject> actualStreamExtent2 = pdfStream.StreamExtent;

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
        IPdfStream pdfStream = pdfString.ToPdfStream();
        int expectedStreamExtentLengthValue = pdfString.Bytes.Length;
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
        IPdfStream pdfStream = new PdfString("Test").ToPdfStream();

        // Act
        string actualStreamContent1 = pdfStream.Content;
        string actualStreamContent2 = pdfStream.Content;

        // Assert
        Assert.True(ReferenceEquals(actualStreamContent1, actualStreamContent2));
    }

    [Fact(DisplayName = $"{nameof(PdfStream.Bytes)} property should return a valid value")]
    public void PdfStream_Bytes_ShouldReturnValidValue()
    {
        // Arrange
        const string pdfStringValue = "This is a PDF String wrapped in a Stream object";
        byte[] expectedBytes =
        {
            0x3C, 0x3C, 0x2F, 0x4C, 0x65, 0x6E, 0x67, 0x74, 0x68, 0x20, 0x34, 0x39, 0x3E, 0x3E, 0x0A, 0x73, 0x74, 0x72, 0x65, 0x61, 0x6D, 0x0A, 0x28, 0x54, 0x68, 0x69, 0x73, 0x20, 0x69, 0x73,
            0x20, 0x61, 0x20, 0x50, 0x44, 0x46, 0x20, 0x53, 0x74, 0x72, 0x69, 0x6E, 0x67, 0x20, 0x77, 0x72, 0x61, 0x70, 0x70, 0x65, 0x64, 0x20, 0x69, 0x6E, 0x20, 0x61, 0x20, 0x53, 0x74, 0x72,
            0x65, 0x61, 0x6D, 0x20, 0x6F, 0x62, 0x6A, 0x65, 0x63, 0x74, 0x29, 0x0A, 0x65, 0x6E, 0x64, 0x73, 0x74, 0x72, 0x65, 0x61, 0x6D,
        };

        IPdfStream pdfStream = new PdfString(pdfStringValue).ToPdfStream();

        // Act
        ReadOnlyMemory<byte> actualBytes = pdfStream.Bytes;

        // Assert
        Assert.True(actualBytes.Span.SequenceEqual(expectedBytes));
    }

    [Fact(DisplayName = "Check Equals method if the argument is null")]
    public void PdfStream_Equals_NullArgument_ShouldReturnFalse()
    {
        // Arrange
        IPdfStream pdfStream = new PdfString("Test").ToPdfStream();

        // Act
        bool actualResult = pdfStream.Equals(null);

        // Assert
        Assert.False(actualResult);
    }

    [Fact(DisplayName = "Check Equals method that has the same reference as a value")]
    public void PdfStream_Equals_SameReference_ShouldReturnTrue()
    {
        // Arrange
        IPdfStream pdfStream1 = new PdfString("Test").ToPdfStream();
        IPdfStream pdfStream2 = pdfStream1;

        // Act
        bool actualResult = pdfStream1.Equals(pdfStream2);

        // Assert
        Assert.True(actualResult);
    }

    [Fact(DisplayName = "Check Equals method that has different references as values")]
    public void PdfStream_Equals_DifferentReferences_ShouldReturnFalse()
    {
        // Arrange
        IPdfStream pdfStream1 = new PdfString("Test").ToPdfStream();
        IPdfStream pdfStream2 = new PdfString("Test").ToPdfStream();

        // Act
        bool actualResult = pdfStream1.Equals(pdfStream2);

        // Assert
        Assert.False(actualResult);
    }

    [Fact(DisplayName = "Check if GetHashCode method returns valid value")]
    public void PdfStream_GetHashCode_CheckValidity()
    {
        // Arrange
        IPdfStream pdfStream = new PdfString("Test").ToPdfStream();
        int expectedHashCode = HashCode.Combine(nameof(PdfStream), pdfStream.Value);

        // Act
        int actualHashCode = pdfStream.GetHashCode();

        // Assert
        Assert.Equal(expectedHashCode, actualHashCode);
    }
}
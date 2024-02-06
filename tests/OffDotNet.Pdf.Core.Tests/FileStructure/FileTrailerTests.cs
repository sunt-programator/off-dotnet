// <copyright file="FileTrailerTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Tests.FileStructure;

using System.Diagnostics;
using OffDotNet.Pdf.Core.Common;
using OffDotNet.Pdf.Core.DocumentStructure;
using OffDotNet.Pdf.Core.FileStructure;
using OffDotNet.Pdf.Core.Primitives;
using OffDotNet.Pdf.Core.Properties;

public class FileTrailerTests
{
    private static readonly IPdfIndirectIdentifier<IPageTreeNode> Pages = new PageTreeNode(options => options.Kids = Array.Empty<IPdfIndirectIdentifier<IPageObject>>().ToPdfArray())
        .ToPdfIndirect<IPageTreeNode>(3)
        .ToPdfIndirectIdentifier();

    private static readonly IPdfIndirectIdentifier<IDocumentCatalog> RootDictionary =
        new DocumentCatalog(documentCatalogOptions => documentCatalogOptions.Pages = Pages).ToPdfIndirect<IDocumentCatalog>(2).ToPdfIndirectIdentifier();

    [Theory(DisplayName = $"Constructor with negative {nameof(FileTrailer.ByteOffset)} should throw an {nameof(ArgumentOutOfRangeException)}")]
    [InlineData(-1)]
    [InlineData(-34)]
    [InlineData(-124)]
    public void FileTrailer_ConstructorWithNegativeBytesOffset_ShouldThrowException(long byteOffset)
    {
        // Arrange
        FileTrailerOptions fileTrailerOptions = new() { Root = RootDictionary };

        // Act
        IFileTrailer FileTrailerFunction()
        {
            return fileTrailerOptions.ToFileTrailer(byteOffset);
        }

        // Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(FileTrailerFunction);
        Assert.StartsWith(Resource.FileTrailer_ByteOffsetMustBePositive, exception.Message);
    }

    [Theory(DisplayName = $"Constructor with negative {nameof(FileTrailerOptions.Size)} should throw an {nameof(ArgumentOutOfRangeException)}")]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-34)]
    [InlineData(-124)]
    public void FileTrailer_ConstructorWithNegativeSize_ShouldThrowException(int size)
    {
        // Arrange

        // Act
        IFileTrailer FileTrailerFunction()
        {
            return new FileTrailer(123, options => options.Size = size);
        }

        // Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(FileTrailerFunction);
        Assert.StartsWith(Resource.FileTrailer_SizeMustBeGreaterThanZero, exception.Message);
    }

    [Theory(DisplayName = $"Constructor with negative {nameof(FileTrailerOptions.Prev)} should throw an {nameof(ArgumentOutOfRangeException)}")]
    [InlineData(-1)]
    [InlineData(-34)]
    [InlineData(-124)]
    public void FileTrailer_ConstructorWithNegativePrev_ShouldThrowException(int? prev)
    {
        // Arrange

        // Act
        IFileTrailer FileTrailerFunction()
        {
            return new FileTrailer(123, options =>
            {
                options.Size = 456;
                options.Prev = prev;
            });
        }

        // Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(FileTrailerFunction);
        Assert.StartsWith(Resource.FileTrailer_PrevMustBePositive, exception.Message);
    }

    [Fact(DisplayName = $"Constructor with empty {nameof(FileTrailerOptions.Encrypt)} dictionary should throw an {nameof(ArgumentOutOfRangeException)}")]
    public void FileTrailer_ConstructorWithEmptyEncryptDictionary_ShouldThrowException()
    {
        // Arrange
        var encryptDictionary = new Dictionary<PdfName, IPdfObject>(1);

        // Act
        IFileTrailer FileTrailerFunction()
        {
            return new FileTrailer(123, options =>
            {
                options.Size = 456;
                options.Prev = 789;
                options.Root = RootDictionary;
                options.Encrypt = encryptDictionary.ToPdfDictionary();
            });
        }

        // Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(FileTrailerFunction);
        Assert.StartsWith(Resource.FileTrailer_EncryptMustHaveANonEmptyCollection, exception.Message);
    }

    [Fact(DisplayName = $"Constructor with an invalid {nameof(FileTrailerOptions.Id)}, when {nameof(FileTrailerOptions.Encrypt)} is present, should throw an {nameof(ArgumentOutOfRangeException)}")]
    public void FileTrailer_ConstructorWithInvalidIdWhenEncryptIsPresent_ShouldThrowException()
    {
        // Arrange
        var encryptDictionary = new Dictionary<PdfName, IPdfObject>(1) { { "Test", new PdfInteger(1) } };

        // Act
        IFileTrailer FileTrailerFunction()
        {
            return new FileTrailer(123, options =>
            {
                options.Size = 456;
                options.Prev = 789;
                options.Root = RootDictionary;
                options.Encrypt = encryptDictionary.ToPdfDictionary();
            });
        }

        // Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(FileTrailerFunction);
        Assert.StartsWith(Resource.FileTrailer_IdMustBeAnArrayOfTwoByteStrings, exception.Message);
    }

    [Theory(DisplayName = $"{nameof(FileTrailer.ByteOffset)} property should return a valid value")]
    [InlineData(123)]
    [InlineData(456)]
    [InlineData(789)]
    public void FileTrailer_ByteOffset_ShouldReturnValidValue(long byteOffset)
    {
        // Arrange
        FileTrailerOptions fileTrailerOptions = new()
        {
            Size = 456,
            Prev = 789,
            Root = RootDictionary,
        };
        IFileTrailer fileTrailer = new FileTrailer(byteOffset, fileTrailerOptions);

        // Act
        long actualByteOffset = fileTrailer.ByteOffset;

        // Assert
        Assert.Equal(byteOffset, actualByteOffset);
    }

    [Fact(DisplayName = $"{nameof(FileTrailer.FileTrailerDictionary)} property should return a valid value")]
    public void FileTrailer_FileTrailerDictionary_ShouldReturnValidValue()
    {
        // Arrange
        FileTrailerOptions fileTrailerOptions = new()
        {
            Size = 456,
            Prev = 789,
            Root = RootDictionary,
        };
        IFileTrailer fileTrailer = new FileTrailer(123, fileTrailerOptions);

        // Act
        IPdfDictionary<IPdfObject> fileTrailerDictionary = fileTrailer.FileTrailerDictionary;

        // Assert
        Assert.Contains("Size", fileTrailerDictionary.Value);
        Assert.Contains("Prev", fileTrailerDictionary.Value);
        Assert.Contains("Root", fileTrailerDictionary.Value);
        Assert.Equal(fileTrailerOptions.Size, fileTrailerDictionary.Value["Size"]);
        Assert.Equal(fileTrailerOptions.Prev, fileTrailerDictionary.Value["Prev"]);
        Assert.Equal(fileTrailerOptions.Root, fileTrailerDictionary.Value["Root"]);
    }

    [Fact(DisplayName = $"{nameof(FileTrailer.Content)} property should return a valid value")]
    public void FileTrailer_Content_ShouldReturnValidValue()
    {
        // Arrange
        const long byteOffset = 12345;
        const int size = 22;
        const int prev = 196;
        const string rootDictionaryRefContent = "2 0 R";
        const string encryptDictionaryContent = "<</Test1 1 /Test2 2>>";
        const string infoRefContent = "1 0 R";
        const string idArrayContent = "[<81b14aafa313db63dbd6f981e49f94f4> <81b14aafa313db63dbd6f981e49f94f4>]";
        const string expectedContent =
            "trailer\n<</Size 22 /Prev 196 /Root 2 0 R /Encrypt <</Test1 1 /Test2 2>> /Info 1 0 R /ID [<81b14aafa313db63dbd6f981e49f94f4> <81b14aafa313db63dbd6f981e49f94f4>]>>\nstartxref\n12345\n%%EOF";

        FileTrailerOptions fileTrailerOptions = GetFileTrailerOptions(size, prev, rootDictionaryRefContent, encryptDictionaryContent, infoRefContent, idArrayContent);
        FileTrailer fileTrailer = new(byteOffset, fileTrailerOptions);

        // Act
        string actualContent = fileTrailer.Content;

        // Assert
        Assert.Equal(expectedContent, actualContent);
    }

    [Fact(DisplayName = $"{nameof(FileTrailer.Content)} property, accessed multiple times, should return the same reference")]
    public void FileTrailer_Content_MultipleAccesses_ShouldReturnSameReference()
    {
        // Arrange
        const long byteOffset = 12345;
        const int size = 22;
        const int prev = 196;
        const string rootDictionaryRefContent = "2 0 R";
        const string encryptDictionaryContent = "<</Test1 1 /Test2 2>>";
        const string infoRefContent = "1 0 R";
        const string idArrayContent = "[<81b14aafa313db63dbd6f981e49f94f4> <81b14aafa313db63dbd6f981e49f94f4>]";

        FileTrailerOptions fileTrailerOptions = GetFileTrailerOptions(size, prev, rootDictionaryRefContent, encryptDictionaryContent, infoRefContent, idArrayContent);
        FileTrailer fileTrailer = new(byteOffset, fileTrailerOptions);

        // Act
        string actualContent1 = fileTrailer.Content;
        string actualContent2 = fileTrailer.Content;

        // Assert
        Assert.True(ReferenceEquals(actualContent1, actualContent2));
    }

    [Fact(DisplayName = $"{nameof(FileTrailer.Bytes)} property should return a valid value")]
    public void FileTrailer_Bytes_ShouldReturnValidValue()
    {
        // Arrange
        const long byteOffset = 12345;
        const int size = 22;
        const int prev = 196;
        const string rootDictionaryRefContent = "2 0 R";
        const string encryptDictionaryContent = "<</Test1 1 /Test2 2>>";
        const string infoRefContent = "1 0 R";
        const string idArrayContent = "[<81b14aafa313db63dbd6f981e49f94f4> <81b14aafa313db63dbd6f981e49f94f4>]";
        byte[] expectedBytes =
        {
            0x74, 0x72, 0x61, 0x69, 0x6C, 0x65, 0x72, 0x0A, 0x3C, 0x3C, 0x2F, 0x53, 0x69, 0x7A, 0x65, 0x20, 0x32, 0x32, 0x20, 0x2F, 0x50, 0x72, 0x65, 0x76, 0x20, 0x31, 0x39, 0x36, 0x20, 0x2F, 0x52,
            0x6F, 0x6F, 0x74, 0x20, 0x32, 0x20, 0x30, 0x20, 0x52, 0x20, 0x2F, 0x45, 0x6E, 0x63, 0x72, 0x79, 0x70, 0x74, 0x20, 0x3C, 0x3C, 0x2F, 0x54, 0x65, 0x73, 0x74, 0x31, 0x20, 0x31, 0x20, 0x2F,
            0x54, 0x65, 0x73, 0x74, 0x32, 0x20, 0x32, 0x3E, 0x3E, 0x20, 0x2F, 0x49, 0x6E, 0x66, 0x6F, 0x20, 0x31, 0x20, 0x30, 0x20, 0x52, 0x20, 0x2F, 0x49, 0x44, 0x20, 0x5B, 0x3C, 0x38, 0x31, 0x62,
            0x31, 0x34, 0x61, 0x61, 0x66, 0x61, 0x33, 0x31, 0x33, 0x64, 0x62, 0x36, 0x33, 0x64, 0x62, 0x64, 0x36, 0x66, 0x39, 0x38, 0x31, 0x65, 0x34, 0x39, 0x66, 0x39, 0x34, 0x66, 0x34, 0x3E, 0x20,
            0x3C, 0x38, 0x31, 0x62, 0x31, 0x34, 0x61, 0x61, 0x66, 0x61, 0x33, 0x31, 0x33, 0x64, 0x62, 0x36, 0x33, 0x64, 0x62, 0x64, 0x36, 0x66, 0x39, 0x38, 0x31, 0x65, 0x34, 0x39, 0x66, 0x39, 0x34,
            0x66, 0x34, 0x3E, 0x5D, 0x3E, 0x3E, 0x0A, 0x73, 0x74, 0x61, 0x72, 0x74, 0x78, 0x72, 0x65, 0x66, 0x0A, 0x31, 0x32, 0x33, 0x34, 0x35, 0x0A, 0x25, 0x25, 0x45, 0x4F, 0x46,
        };

        FileTrailerOptions fileTrailerOptions = GetFileTrailerOptions(size, prev, rootDictionaryRefContent, encryptDictionaryContent, infoRefContent, idArrayContent);
        FileTrailer fileTrailer = new(byteOffset, fileTrailerOptions);

        // Act
        byte[] actualBytes = fileTrailer.Bytes.ToArray();

        // Assert
        Assert.Equal(expectedBytes, actualBytes);
    }

    [Fact(DisplayName = "Check if GetHashCode method returns valid value")]
    public void XFileTrailer_GetHashCode_CheckValidity()
    {
        // Arrange
        const long byteOffset = 12345;
        const int size = 22;
        const int prev = 196;
        const string rootDictionaryRefContent = "2 0 R";
        const string encryptDictionaryContent = "<</Test1 1 /Test2 2>>";
        const string infoRefContent = "1 0 R";
        const string idArrayContent = "[<81b14aafa313db63dbd6f981e49f94f4> <81b14aafa313db63dbd6f981e49f94f4>]";

        FileTrailerOptions fileTrailerOptions = GetFileTrailerOptions(size, prev, rootDictionaryRefContent, encryptDictionaryContent, infoRefContent, idArrayContent);
        FileTrailer fileTrailer = new(byteOffset, fileTrailerOptions);

        int expectedHashCode = HashCode.Combine(nameof(FileTrailer), fileTrailer.Content);

        // Act
        int actualHashCode = fileTrailer.GetHashCode();

        // Assert
        Assert.Equal(expectedHashCode, actualHashCode);
    }

    [Theory(DisplayName = "Check if Equals returns a valid result")]
    [InlineData(18799, 18799, true)]
    [InlineData(18799, 12345, false)]
    [InlineData(12345, 12345, true)]
    public void FileTrailer_Equals_CheckValidity(long byteOffset1, long byteOffset2, bool expectedValue)
    {
        // Arrange
        const int size = 22;
        const int prev = 196;
        const string rootDictionaryRefContent = "2 0 R";
        const string encryptDictionaryContent = "<</Test1 1 /Test2 2>>";
        const string infoRefContent = "1 0 R";
        const string idArrayContent = "[<81b14aafa313db63dbd6f981e49f94f4> <81b14aafa313db63dbd6f981e49f94f4>]";

        FileTrailerOptions fileTrailerOptions1 = GetFileTrailerOptions(size, prev, rootDictionaryRefContent, encryptDictionaryContent, infoRefContent, idArrayContent);
        FileTrailerOptions fileTrailerOptions2 = GetFileTrailerOptions(size, prev, rootDictionaryRefContent, encryptDictionaryContent, infoRefContent, idArrayContent);

        FileTrailer fileTrailer1 = new(byteOffset1, fileTrailerOptions1);
        FileTrailer fileTrailer2 = new(byteOffset2, fileTrailerOptions2);

        // Act
        bool actualResult = fileTrailer1.Equals(fileTrailer2);

        // Assert
        Assert.Equal(expectedValue, actualResult);
    }

    [Fact(DisplayName = "Check if Equals method with null object returns always false")]
    public void FileTrailer_EqualsNullObject_CheckValidity()
    {
        // Arrange
        const long byteOffset = 12345;
        const int size = 22;
        const int prev = 196;
        const string rootDictionaryRefContent = "2 0 R";
        const string encryptDictionaryContent = "<</Test1 1 /Test2 2>>";
        const string infoRefContent = "1 0 R";
        const string idArrayContent = "[<81b14aafa313db63dbd6f981e49f94f4> <81b14aafa313db63dbd6f981e49f94f4>]";

        FileTrailerOptions fileTrailerOptions = GetFileTrailerOptions(size, prev, rootDictionaryRefContent, encryptDictionaryContent, infoRefContent, idArrayContent);
        FileTrailer fileTrailer = new(byteOffset, fileTrailerOptions);

        // Act
        bool actualResult1 = fileTrailer.Equals(null);

        Debug.Assert(fileTrailer != null, nameof(fileTrailer) + " != null");
        bool actualResult2 = fileTrailer.Equals(null);

        // Assert
        Assert.False(actualResult1);
        Assert.False(actualResult2);
    }

    private static FileTrailerOptions GetFileTrailerOptions(int size, int? prev, string rootDictionaryRefContent, string? encryptDictionaryContent, string? infoRefContent, string? idArrayContent)
    {
        var rootDictionary = Substitute.For<IPdfIndirectIdentifier<IDocumentCatalog>>();
        var encryptDictionary = string.IsNullOrWhiteSpace(encryptDictionaryContent)
            ? null
            : Substitute.For<IPdfDictionary<IPdfObject>>();

        var infoRef = string.IsNullOrWhiteSpace(infoRefContent)
            ? null
            : Substitute.For<IPdfIndirectIdentifier<IPdfDictionary<IPdfObject>>>();

        var idArray = string.IsNullOrWhiteSpace(idArrayContent) ? null : Substitute.For<IPdfArray<PdfString>>();

        rootDictionary.Content.Returns(rootDictionaryRefContent);
        encryptDictionary?.Content.Returns(encryptDictionaryContent);
        encryptDictionary?.Value.Count.Returns(2);
        infoRef?.Content.Returns(infoRefContent);
        idArray?.Content.Returns(idArrayContent);
        idArray?.Value.Count.Returns(2);

        FileTrailerOptions fileTrailerOptions = new()
        {
            Size = size,
            Prev = prev,
            Root = rootDictionary,
            Encrypt = encryptDictionary,
            Info = infoRef,
            Id = idArray,
        };
        return fileTrailerOptions;
    }
}

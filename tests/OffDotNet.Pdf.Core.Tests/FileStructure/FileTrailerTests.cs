// <copyright file="FileTrailerTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Diagnostics;
using OffDotNet.Pdf.Core.Common;
using OffDotNet.Pdf.Core.DocumentStructure;
using OffDotNet.Pdf.Core.FileStructure;
using OffDotNet.Pdf.Core.Primitives;
using OffDotNet.Pdf.Core.Properties;
using Xunit;

namespace OffDotNet.Pdf.Core.Tests.FileStructure;

public class FileTrailerTests
{
    private static readonly IPdfIndirectIdentifier<IPageTreeNode> Pages = new PageTreeNode(options => options.Kids = Array.Empty<IPdfIndirectIdentifier<IPageObject>>().ToPdfArray())
        .ToPdfIndirect<IPageTreeNode>(3)
        .ToPdfIndirectIdentifier();

    private static readonly IPdfIndirectIdentifier<DocumentCatalog> RootDictionary =
        new DocumentCatalog(documentCatalogOptions => documentCatalogOptions.Pages = Pages).ToPdfIndirect(2).ToPdfIndirectIdentifier();

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

    [Theory(DisplayName = $"{nameof(FileTrailer.Content)} property should return a valid value")]
    [MemberData(nameof(FileTrailerTestsDataGenerator.FileTrailer_Content_TestCases), MemberType = typeof(FileTrailerTestsDataGenerator))]
    public void FileTrailer_Content_ShouldReturnValidValue(IFileTrailer fileTrailer, string expectedContent)
    {
        // Arrange

        // Act
        string actualContent = fileTrailer.Content;

        // Assert
        Assert.Equal(expectedContent, actualContent);
    }

    [Theory(DisplayName = $"{nameof(FileTrailer.Content)} property, accessed multiple times, should return the same reference")]
    [MemberData(nameof(FileTrailerTestsDataGenerator.FileTrailer_NoExpectedData_TestCases), MemberType = typeof(FileTrailerTestsDataGenerator))]
    public void FileTrailer_Content_MultipleAccesses_ShouldReturnSameReference(IFileTrailer fileTrailer)
    {
        // Arrange

        // Act
        string actualContent1 = fileTrailer.Content;
        string actualContent2 = fileTrailer.Content;

        // Assert
        Assert.True(ReferenceEquals(actualContent1, actualContent2));
    }

    [Theory(DisplayName = $"{nameof(FileTrailer.Bytes)} property should return a valid value")]
    [MemberData(nameof(FileTrailerTestsDataGenerator.FileTrailer_Bytes_TestCases), MemberType = typeof(FileTrailerTestsDataGenerator))]
    public void FileTrailer_Bytes_ShouldReturnValidValue(IFileTrailer fileTrailer, byte[] expectedBytes)
    {
        // Arrange

        // Act
        byte[] actualBytes = fileTrailer.Bytes.ToArray();

        // Assert
        Assert.Equal(expectedBytes, actualBytes);
    }

    [Theory(DisplayName = "Check if GetHashCode method returns valid value")]
    [MemberData(nameof(FileTrailerTestsDataGenerator.FileTrailer_NoExpectedData_TestCases), MemberType = typeof(FileTrailerTestsDataGenerator))]
    public void XFileTrailer_GetHashCode_CheckValidity(IFileTrailer fileTrailer)
    {
        // Arrange
        int expectedHashCode = HashCode.Combine(nameof(FileTrailer), fileTrailer.Content);

        // Act
        int actualHashCode = fileTrailer.GetHashCode();

        // Assert
        Assert.Equal(expectedHashCode, actualHashCode);
    }

    [Theory(DisplayName = "Check if Equals returns a valid result")]
    [MemberData(nameof(FileTrailerTestsDataGenerator.FileTrailer_Equals_TestCases), MemberType = typeof(FileTrailerTestsDataGenerator))]
    public void FileTrailer_Equals_CheckValidity(IFileTrailer xRefEntry1, IFileTrailer xRefEntry2, bool expectedValue)
    {
        // Arrange

        // Act
        bool actualResult = xRefEntry1.Equals(xRefEntry2);

        // Assert
        Assert.Equal(expectedValue, actualResult);
    }

    [Theory(DisplayName = "Check if Equals method with null object returns always false")]
    [MemberData(nameof(FileTrailerTestsDataGenerator.FileTrailer_NoExpectedData_TestCases), MemberType = typeof(FileTrailerTestsDataGenerator))]
    public void FileTrailer_EqualsNullObject_CheckValidity(IFileTrailer xRefEntry)
    {
        // Arrange

        // Act
        bool actualResult1 = xRefEntry.Equals(null);

        Debug.Assert(xRefEntry != null, nameof(xRefEntry) + " != null");
        bool actualResult2 = xRefEntry.Equals(null);

        // Assert
        Assert.False(actualResult1);
        Assert.False(actualResult2);
    }
}

[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "TestData generator class can be in the same file")]
internal static class FileTrailerTestsDataGenerator
{
    private static readonly IPdfIndirectIdentifier<IPageTreeNode> Pages = new PageTreeNode(options => options.Kids = Array.Empty<IPdfIndirectIdentifier<IPageObject>>().ToPdfArray())
        .ToPdfIndirect<IPageTreeNode>(3)
        .ToPdfIndirectIdentifier();

    private static readonly IPdfIndirectIdentifier<DocumentCatalog> RootDictionary =
        new DocumentCatalog(documentCatalogOptions => documentCatalogOptions.Pages = Pages).ToPdfIndirect(2).ToPdfIndirectIdentifier();

    public static IEnumerable<object[]> FileTrailer_Content_TestCases()
    {
        yield return new object[]
        {
            new FileTrailer(18799, options =>
            {
                options.Size = 22;
                options.Root = RootDictionary;
                options.Info = new Dictionary<PdfName, IPdfObject>(1) { { "Test", new PdfInteger(1) } }
                    .ToPdfDictionary()
                    .ToPdfIndirect(1)
                    .ToPdfIndirectIdentifier();
                options.Id = new[] { new PdfString("81b14aafa313db63dbd6f981e49f94f4", isHexString: true), new PdfString("81b14aafa313db63dbd6f981e49f94f4", isHexString: true) }.ToPdfArray();
            }),
            "trailer\n<</Size 22 /Root 2 0 R /Info 1 0 R /ID [<81b14aafa313db63dbd6f981e49f94f4> <81b14aafa313db63dbd6f981e49f94f4>]>>\nstartxref\n18799\n%%EOF",
        };

        yield return new object[]
        {
            new FileTrailer(12345, options =>
            {
                options.Size = 22;
                options.Prev = 196;
                options.Root = RootDictionary;
                options.Info = new Dictionary<PdfName, IPdfObject>(1) { { "Test", new PdfInteger(1) } }
                    .ToPdfDictionary()
                    .ToPdfIndirect(1)
                    .ToPdfIndirectIdentifier();
                options.Id = new[] { new PdfString("81b14aafa313db63dbd6f981e49f94f4", isHexString: true), new PdfString("81b14aafa313db63dbd6f981e49f94f4", isHexString: true) }.ToPdfArray();
            }),
            "trailer\n<</Size 22 /Prev 196 /Root 2 0 R /Info 1 0 R /ID [<81b14aafa313db63dbd6f981e49f94f4> <81b14aafa313db63dbd6f981e49f94f4>]>>\nstartxref\n12345\n%%EOF",
        };

        yield return new object[]
        {
            new FileTrailer(12345, options =>
            {
                options.Size = 22;
                options.Prev = 196;
                options.Root = RootDictionary;
                options.Encrypt = new Dictionary<PdfName, IPdfObject>(2)
                    {
                        { "Test1", new PdfInteger(1) },
                        { "Test2", new PdfInteger(2) },
                    }
                    .ToPdfDictionary();
                options.Info = new Dictionary<PdfName, IPdfObject>(1) { { "Test", new PdfInteger(1) } }
                    .ToPdfDictionary()
                    .ToPdfIndirect(1)
                    .ToPdfIndirectIdentifier();
                options.Id = new[] { new PdfString("81b14aafa313db63dbd6f981e49f94f4", isHexString: true), new PdfString("81b14aafa313db63dbd6f981e49f94f4", isHexString: true) }.ToPdfArray();
            }),
            "trailer\n<</Size 22 /Prev 196 /Root 2 0 R /Encrypt <</Test1 1 /Test2 2>> /Info 1 0 R /ID [<81b14aafa313db63dbd6f981e49f94f4> <81b14aafa313db63dbd6f981e49f94f4>]>>\nstartxref\n12345\n%%EOF",
        };
    }

    public static IEnumerable<object[]> FileTrailer_Bytes_TestCases()
    {
        yield return new object[]
        {
            new FileTrailer(18799, options =>
            {
                options.Size = 22;
                options.Root = RootDictionary;
                options.Info = new Dictionary<PdfName, IPdfObject>(1) { { "Test", new PdfInteger(1) } }
                    .ToPdfDictionary()
                    .ToPdfIndirect(1)
                    .ToPdfIndirectIdentifier();
                options.Id = new[] { new PdfString("81b14aafa313db63dbd6f981e49f94f4", isHexString: true), new PdfString("81b14aafa313db63dbd6f981e49f94f4", isHexString: true) }.ToPdfArray();
            }),
            new byte[]
            {
                0x74, 0x72, 0x61, 0x69, 0x6C, 0x65, 0x72, 0x0A, 0x3C, 0x3C, 0x2F, 0x53, 0x69, 0x7A, 0x65, 0x20, 0x32, 0x32, 0x20, 0x2F, 0x52, 0x6F, 0x6F, 0x74, 0x20, 0x32, 0x20, 0x30, 0x20, 0x52,
                0x20, 0x2F, 0x49, 0x6E, 0x66, 0x6F, 0x20, 0x31, 0x20, 0x30, 0x20, 0x52, 0x20, 0x2F, 0x49, 0x44, 0x20, 0x5B, 0x3C, 0x38, 0x31, 0x62, 0x31, 0x34, 0x61, 0x61, 0x66, 0x61, 0x33, 0x31,
                0x33, 0x64, 0x62, 0x36, 0x33, 0x64, 0x62, 0x64, 0x36, 0x66, 0x39, 0x38, 0x31, 0x65, 0x34, 0x39, 0x66, 0x39, 0x34, 0x66, 0x34, 0x3E, 0x20, 0x3C, 0x38, 0x31, 0x62, 0x31, 0x34, 0x61,
                0x61, 0x66, 0x61, 0x33, 0x31, 0x33, 0x64, 0x62, 0x36, 0x33, 0x64, 0x62, 0x64, 0x36, 0x66, 0x39, 0x38, 0x31, 0x65, 0x34, 0x39, 0x66, 0x39, 0x34, 0x66, 0x34, 0x3E, 0x5D, 0x3E, 0x3E,
                0x0A, 0x73, 0x74, 0x61, 0x72, 0x74, 0x78, 0x72, 0x65, 0x66, 0x0A, 0x31, 0x38, 0x37, 0x39, 0x39, 0x0A, 0x25, 0x25, 0x45, 0x4F, 0x46,
            },
        };

        yield return new object[]
        {
            new FileTrailer(12345, options =>
            {
                options.Size = 22;
                options.Prev = 196;
                options.Root = RootDictionary;
                options.Info = new Dictionary<PdfName, IPdfObject>(1) { { "Test", new PdfInteger(1) } }
                    .ToPdfDictionary()
                    .ToPdfIndirect(1)
                    .ToPdfIndirectIdentifier();
                options.Id = new[] { new PdfString("81b14aafa313db63dbd6f981e49f94f4", isHexString: true), new PdfString("81b14aafa313db63dbd6f981e49f94f4", isHexString: true) }.ToPdfArray();
            }),
            new byte[]
            {
                0x74, 0x72, 0x61, 0x69, 0x6C, 0x65, 0x72, 0x0A, 0x3C, 0x3C, 0x2F, 0x53, 0x69, 0x7A, 0x65, 0x20, 0x32, 0x32, 0x20, 0x2F, 0x50, 0x72, 0x65, 0x76, 0x20, 0x31, 0x39, 0x36, 0x20, 0x2F,
                0x52, 0x6F, 0x6F, 0x74, 0x20, 0x32, 0x20, 0x30, 0x20, 0x52, 0x20, 0x2F, 0x49, 0x6E, 0x66, 0x6F, 0x20, 0x31, 0x20, 0x30, 0x20, 0x52, 0x20, 0x2F, 0x49, 0x44, 0x20, 0x5B, 0x3C, 0x38,
                0x31, 0x62, 0x31, 0x34, 0x61, 0x61, 0x66, 0x61, 0x33, 0x31, 0x33, 0x64, 0x62, 0x36, 0x33, 0x64, 0x62, 0x64, 0x36, 0x66, 0x39, 0x38, 0x31, 0x65, 0x34, 0x39, 0x66, 0x39, 0x34, 0x66,
                0x34, 0x3E, 0x20, 0x3C, 0x38, 0x31, 0x62, 0x31, 0x34, 0x61, 0x61, 0x66, 0x61, 0x33, 0x31, 0x33, 0x64, 0x62, 0x36, 0x33, 0x64, 0x62, 0x64, 0x36, 0x66, 0x39, 0x38, 0x31, 0x65, 0x34,
                0x39, 0x66, 0x39, 0x34, 0x66, 0x34, 0x3E, 0x5D, 0x3E, 0x3E, 0x0A, 0x73, 0x74, 0x61, 0x72, 0x74, 0x78, 0x72, 0x65, 0x66, 0x0A, 0x31, 0x32, 0x33, 0x34, 0x35, 0x0A, 0x25, 0x25, 0x45,
                0x4F, 0x46,
            },
        };

        yield return new object[]
        {
            new FileTrailer(12345, options =>
            {
                options.Size = 22;
                options.Prev = 196;
                options.Root = RootDictionary;
                options.Encrypt = new Dictionary<PdfName, IPdfObject>(2)
                    {
                        { "Test1", new PdfInteger(1) },
                        { "Test2", new PdfInteger(2) },
                    }
                    .ToPdfDictionary();
                options.Info = new Dictionary<PdfName, IPdfObject>(1) { { "Test", new PdfInteger(1) } }
                    .ToPdfDictionary()
                    .ToPdfIndirect(1)
                    .ToPdfIndirectIdentifier();
                options.Id = new[] { new PdfString("81b14aafa313db63dbd6f981e49f94f4", isHexString: true), new PdfString("81b14aafa313db63dbd6f981e49f94f4", isHexString: true) }.ToPdfArray();
            }),
            new byte[]
            {
                0x74, 0x72, 0x61, 0x69, 0x6C, 0x65, 0x72, 0x0A, 0x3C, 0x3C, 0x2F, 0x53, 0x69, 0x7A, 0x65, 0x20, 0x32, 0x32, 0x20, 0x2F, 0x50, 0x72, 0x65, 0x76, 0x20, 0x31, 0x39, 0x36, 0x20, 0x2F,
                0x52, 0x6F, 0x6F, 0x74, 0x20, 0x32, 0x20, 0x30, 0x20, 0x52, 0x20, 0x2F, 0x45, 0x6E, 0x63, 0x72, 0x79, 0x70, 0x74, 0x20, 0x3C, 0x3C, 0x2F, 0x54, 0x65, 0x73, 0x74, 0x31, 0x20, 0x31,
                0x20, 0x2F, 0x54, 0x65, 0x73, 0x74, 0x32, 0x20, 0x32, 0x3E, 0x3E, 0x20, 0x2F, 0x49, 0x6E, 0x66, 0x6F, 0x20, 0x31, 0x20, 0x30, 0x20, 0x52, 0x20, 0x2F, 0x49, 0x44, 0x20, 0x5B, 0x3C,
                0x38, 0x31, 0x62, 0x31, 0x34, 0x61, 0x61, 0x66, 0x61, 0x33, 0x31, 0x33, 0x64, 0x62, 0x36, 0x33, 0x64, 0x62, 0x64, 0x36, 0x66, 0x39, 0x38, 0x31, 0x65, 0x34, 0x39, 0x66, 0x39, 0x34,
                0x66, 0x34, 0x3E, 0x20, 0x3C, 0x38, 0x31, 0x62, 0x31, 0x34, 0x61, 0x61, 0x66, 0x61, 0x33, 0x31, 0x33, 0x64, 0x62, 0x36, 0x33, 0x64, 0x62, 0x64, 0x36, 0x66, 0x39, 0x38, 0x31, 0x65,
                0x34, 0x39, 0x66, 0x39, 0x34, 0x66, 0x34, 0x3E, 0x5D, 0x3E, 0x3E, 0x0A, 0x73, 0x74, 0x61, 0x72, 0x74, 0x78, 0x72, 0x65, 0x66, 0x0A, 0x31, 0x32, 0x33, 0x34, 0x35, 0x0A, 0x25, 0x25,
                0x45, 0x4F, 0x46,
            },
        };
    }

    public static IEnumerable<object[]> FileTrailer_NoExpectedData_TestCases()
    {
        yield return new object[]
        {
            new FileTrailer(18799, options =>
            {
                options.Size = 22;
                options.Root = RootDictionary;
                options.Info = new Dictionary<PdfName, IPdfObject>(1) { { "Test", new PdfInteger(1) } }
                    .ToPdfDictionary()
                    .ToPdfIndirect(1)
                    .ToPdfIndirectIdentifier();
                options.Id = new[] { new PdfString("81b14aafa313db63dbd6f981e49f94f4", isHexString: true), new PdfString("81b14aafa313db63dbd6f981e49f94f4", isHexString: true) }.ToPdfArray();
            }),
        };

        yield return new object[]
        {
            new FileTrailer(12345, options =>
            {
                options.Size = 22;
                options.Prev = 196;
                options.Root = RootDictionary;
                options.Info = new Dictionary<PdfName, IPdfObject>(1) { { "Test", new PdfInteger(1) } }
                    .ToPdfDictionary()
                    .ToPdfIndirect(1)
                    .ToPdfIndirectIdentifier();
                options.Id = new[] { new PdfString("81b14aafa313db63dbd6f981e49f94f4", isHexString: true), new PdfString("81b14aafa313db63dbd6f981e49f94f4", isHexString: true) }.ToPdfArray();
            }),
        };

        yield return new object[]
        {
            new FileTrailer(12345, options =>
            {
                options.Size = 22;
                options.Prev = 196;
                options.Root = RootDictionary;
                options.Encrypt = new Dictionary<PdfName, IPdfObject>(2)
                    {
                        { "Test1", new PdfInteger(1) },
                        { "Test2", new PdfInteger(2) },
                    }
                    .ToPdfDictionary();
                options.Info = new Dictionary<PdfName, IPdfObject>(1) { { "Test", new PdfInteger(1) } }
                    .ToPdfDictionary()
                    .ToPdfIndirect(1)
                    .ToPdfIndirectIdentifier();
                options.Id = new[] { new PdfString("81b14aafa313db63dbd6f981e49f94f4", isHexString: true), new PdfString("81b14aafa313db63dbd6f981e49f94f4", isHexString: true) }.ToPdfArray();
            }),
        };
    }

    public static IEnumerable<object[]> FileTrailer_Equals_TestCases()
    {
        yield return new object[]
        {
            new FileTrailer(18799, options =>
            {
                options.Size = 22;
                options.Root = RootDictionary;
                options.Info = new Dictionary<PdfName, IPdfObject>(1) { { "Test", new PdfInteger(1) } }
                    .ToPdfDictionary()
                    .ToPdfIndirect(1)
                    .ToPdfIndirectIdentifier();
                options.Id = new[] { new PdfString("81b14aafa313db63dbd6f981e49f94f4", isHexString: true), new PdfString("81b14aafa313db63dbd6f981e49f94f4", isHexString: true) }.ToPdfArray();
            }),
            new FileTrailer(18799, options =>
            {
                options.Size = 22;
                options.Root = RootDictionary;
                options.Info = new Dictionary<PdfName, IPdfObject>(1) { { "Test", new PdfInteger(1) } }
                    .ToPdfDictionary()
                    .ToPdfIndirect(1)
                    .ToPdfIndirectIdentifier();
                options.Id = new[] { new PdfString("81b14aafa313db63dbd6f981e49f94f4", isHexString: true), new PdfString("81b14aafa313db63dbd6f981e49f94f4", isHexString: true) }.ToPdfArray();
            }),
            true,
        };

        yield return new object[]
        {
            new FileTrailer(12345, options =>
            {
                options.Size = 22;
                options.Prev = 1;
                options.Root = RootDictionary;
                options.Info = new Dictionary<PdfName, IPdfObject>(1) { { "Test", new PdfInteger(1) } }
                    .ToPdfDictionary()
                    .ToPdfIndirect(1)
                    .ToPdfIndirectIdentifier();
                options.Id = new[] { new PdfString("81b14aafa313db63dbd6f981e49f94f4", isHexString: true), new PdfString("81b14aafa313db63dbd6f981e49f94f4", isHexString: true) }.ToPdfArray();
            }),
            new FileTrailer(12345, options =>
            {
                options.Size = 22;
                options.Prev = 196;
                options.Root = RootDictionary;
                options.Info = new Dictionary<PdfName, IPdfObject>(1) { { "Test", new PdfInteger(1) } }
                    .ToPdfDictionary()
                    .ToPdfIndirect(1)
                    .ToPdfIndirectIdentifier();
                options.Id = new[] { new PdfString("81b14aafa313db63dbd6f981e49f94f4", isHexString: true), new PdfString("81b14aafa313db63dbd6f981e49f94f4", isHexString: true) }.ToPdfArray();
            }),
            false,
        };

        yield return new object[]
        {
            new FileTrailer(12345, options =>
            {
                options.Size = 22;
                options.Prev = null;
                options.Root = RootDictionary;
                options.Encrypt = new Dictionary<PdfName, IPdfObject>(2)
                    {
                        { "Test1", new PdfInteger(1) },
                        { "Test2", new PdfInteger(2) },
                    }
                    .ToPdfDictionary();
                options.Info = new Dictionary<PdfName, IPdfObject>(1) { { "Test", new PdfInteger(1) } }
                    .ToPdfDictionary()
                    .ToPdfIndirect(1)
                    .ToPdfIndirectIdentifier();
                options.Id = new[] { new PdfString("81b14aafa313db63dbd6f981e49f94f4", isHexString: true), new PdfString("81b14aafa313db63dbd6f981e49f94f4", isHexString: true) }.ToPdfArray();
            }),
            new FileTrailer(0, options =>
            {
                options.Size = 22;
                options.Prev = 0;
                options.Root = RootDictionary;
                options.Encrypt = new Dictionary<PdfName, IPdfObject>(2)
                    {
                        { "Test1", new PdfInteger(1) },
                        { "Test2", new PdfInteger(2) },
                    }
                    .ToPdfDictionary();
                options.Info = new Dictionary<PdfName, IPdfObject>(1) { { "Test", new PdfInteger(1) } }
                    .ToPdfDictionary()
                    .ToPdfIndirect(1)
                    .ToPdfIndirectIdentifier();
                options.Id = new[] { new PdfString("81b14aafa313db63dbd6f981e49f94f4", isHexString: true), new PdfString("81b14aafa313db63dbd6f981e49f94f4", isHexString: true) }.ToPdfArray();
            }),
            false,
        };
    }
}

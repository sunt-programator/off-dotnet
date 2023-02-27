using System;
using System.Collections.Generic;
using Off.Net.Pdf.Core.FileStructure;
using Xunit;

namespace Off.Net.Pdf.Core.Tests.FileStructure;

public class FileHeaderTests
{
    [Theory(DisplayName = $"Constructor with an invalid {nameof(FileHeader.MajorVersion)} should throw an {nameof(ArgumentOutOfRangeException)}")]
    [InlineData(-1)]
    [InlineData(-15)]
    [InlineData(-23)]
    [InlineData(0)]
    [InlineData(3)]
    [InlineData(10)]
    [InlineData(999)]
    public void FileHeader_InvalidMajorVersion_ShouldThrowException(int majorVersion)
    {
        // Arrange

        // Act
        object FileHeaderFunction() => new FileHeader(majorVersion, 0);

        // Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(FileHeaderFunction);
        Assert.StartsWith(Resource.FileHeader_MajorVersionIsNotValid, exception.Message);
    }

    [Theory(DisplayName = $"Constructor with an invalid {nameof(FileHeader.MinorVersion)} should throw an {nameof(ArgumentOutOfRangeException)}")]
    [InlineData(-1)]
    [InlineData(-15)]
    [InlineData(-23)]
    [InlineData(10)]
    [InlineData(999)]
    public void FileHeader_InvalidMinorVersion_ShouldThrowException(int minorVersion)
    {
        // Arrange

        // Act
        object FileHeaderFunction() => new FileHeader(1, minorVersion);

        // Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(FileHeaderFunction);
        Assert.StartsWith(Resource.FileHeader_MinorVersionIsNotValid, exception.Message);
    }

    [Theory(DisplayName = $"{nameof(FileHeader.Content)} property should return a valid value")]
    [MemberData(nameof(FileHeaderTestsDataGenerator.FileHeader_Content_TestCases), MemberType = typeof(FileHeaderTestsDataGenerator))]
    public void FileHeader_Content_ShouldReturnValidValue(FileHeader fileHeader, string expectedContent)
    {
        // Arrange

        // Act
        string actualContent = fileHeader.Content;

        // Assert
        Assert.Equal(expectedContent, actualContent);
    }

    [Theory(DisplayName = $"{nameof(FileHeader.MajorVersion)} property should return a valid value")]
    [MemberData(nameof(FileHeaderTestsDataGenerator.FileHeader_MajorVersion_TestCases), MemberType = typeof(FileHeaderTestsDataGenerator))]
    public void FileHeader_MajorVersion_ShouldReturnValidValue(FileHeader fileHeader, int expectedMajorVersion)
    {
        // Arrange

        // Act
        int actualMajorVersion = fileHeader.MajorVersion;

        // Assert
        Assert.Equal(expectedMajorVersion, actualMajorVersion);
    }

    [Theory(DisplayName = $"{nameof(FileHeader.MinorVersion)} property should return a valid value")]
    [MemberData(nameof(FileHeaderTestsDataGenerator.FileHeader_MinorVersion_TestCases), MemberType = typeof(FileHeaderTestsDataGenerator))]
    public void FileHeader_MinorVersion_ShouldReturnValidValue(FileHeader fileHeader, int expectedMinorVersion)
    {
        // Arrange

        // Act
        int actualMinorVersion = fileHeader.MinorVersion;

        // Assert
        Assert.Equal(expectedMinorVersion, actualMinorVersion);
    }

    [Theory(DisplayName = $"{nameof(FileHeader.Length)} property should return a valid value")]
    [MemberData(nameof(FileHeaderTestsDataGenerator.FileHeader_Length_TestCases), MemberType = typeof(FileHeaderTestsDataGenerator))]
    public void FileHeader_Length_ShouldReturnValidValue(FileHeader fileHeader, int expectedLength)
    {
        // Arrange

        // Act
        int actualLength = fileHeader.Length;

        // Assert
        Assert.Equal(expectedLength, actualLength);
    }

    [Theory(DisplayName = $"{nameof(FileHeader.Bytes)} property should return a valid value")]
    [MemberData(nameof(FileHeaderTestsDataGenerator.FileHeader_Bytes_TestCases), MemberType = typeof(FileHeaderTestsDataGenerator))]
    public void FileHeader_Bytes_ShouldReturnValidValue(FileHeader fileHeader, byte[] expectedBytes)
    {
        // Arrange

        // Act
        byte[] actualBytes = fileHeader.Bytes.ToArray();

        // Assert
        Assert.Equal(expectedBytes, actualBytes);
    }

    [Theory(DisplayName = "Equals method should return a valid value")]
    [MemberData(nameof(FileHeaderTestsDataGenerator.FileHeader_Equals_TestCases), MemberType = typeof(FileHeaderTestsDataGenerator))]
    public void FileHeader_Equals_ShouldReturnValidValue(FileHeader fileHeader1, FileHeader fileHeader2, bool expectedResult)
    {
        // Arrange

        // Act
        bool actualResult = fileHeader1.Equals(fileHeader2);

        // Assert
        Assert.Equal(expectedResult, actualResult);
    }

    [Theory(DisplayName = "Equals method should return a valid value")]
    [MemberData(nameof(FileHeaderTestsDataGenerator.FileHeader_Equals_TestCases), MemberType = typeof(FileHeaderTestsDataGenerator))]
    public void FileHeader_EqualsObject_ShouldReturnValidValue(FileHeader fileHeader1, object? fileHeader2, bool expectedResult)
    {
        // Arrange

        // Act
        bool actualResult = fileHeader1.Equals(fileHeader2);

        // Assert
        Assert.Equal(expectedResult, actualResult);
    }

    [Theory(DisplayName = "The '==' operator should return a valid value")]
    [MemberData(nameof(FileHeaderTestsDataGenerator.FileHeader_Equals_TestCases), MemberType = typeof(FileHeaderTestsDataGenerator))]
    public void FileHeader_EqualOperator_ShouldReturnValidValue(FileHeader fileHeader1, FileHeader fileHeader2, bool expectedResult)
    {
        // Arrange

        // Act
        bool actualResult = fileHeader1 == fileHeader2;

        // Assert
        Assert.Equal(expectedResult, actualResult);
    }

    [Theory(DisplayName = "The '!=' operator should return a valid value")]
    [MemberData(nameof(FileHeaderTestsDataGenerator.FileHeader_Equals_TestCases), MemberType = typeof(FileHeaderTestsDataGenerator))]
    public void FileHeader_NotEqualOperator_ShouldReturnValidValue(FileHeader fileHeader1, FileHeader fileHeader2, bool expectedResult)
    {
        // Arrange

        // Act
        bool actualResult = fileHeader1 != fileHeader2;

        // Assert
        Assert.Equal(!expectedResult, actualResult);
    }

    [Theory(DisplayName = "GetHashCode method should return a valid value")]
    [MemberData(nameof(FileHeaderTestsDataGenerator.FileHeader_NoExpectedData_TestCases), MemberType = typeof(FileHeaderTestsDataGenerator))]
    public void FileHeader_GetHashCode_ShouldReturnValidValue(FileHeader fileHeader)
    {
        // Arrange
        int expectedHashCode = HashCode.Combine(nameof(FileHeader), fileHeader.MajorVersion, fileHeader.MinorVersion);

        // Act
        int actualHashCode = fileHeader.GetHashCode();

        // Assert
        Assert.Equal(expectedHashCode, actualHashCode);
    }
}

internal static class FileHeaderTestsDataGenerator
{
    public static IEnumerable<object[]> FileHeader_NoExpectedData_TestCases()
    {
        yield return new object[] { FileHeader.PdfVersion10 };
        yield return new object[] { FileHeader.PdfVersion11 };
        yield return new object[] { FileHeader.PdfVersion12 };
        yield return new object[] { FileHeader.PdfVersion13 };
        yield return new object[] { FileHeader.PdfVersion14 };
        yield return new object[] { FileHeader.PdfVersion15 };
        yield return new object[] { FileHeader.PdfVersion16 };
        yield return new object[] { FileHeader.PdfVersion17 };
        yield return new object[] { FileHeader.PdfVersion20 };
    }

    public static IEnumerable<object[]> FileHeader_Content_TestCases()
    {
        yield return new object[] { FileHeader.PdfVersion10, "%PDF-1.0\n" };
        yield return new object[] { FileHeader.PdfVersion11, "%PDF-1.1\n" };
        yield return new object[] { FileHeader.PdfVersion12, "%PDF-1.2\n" };
        yield return new object[] { FileHeader.PdfVersion13, "%PDF-1.3\n" };
        yield return new object[] { FileHeader.PdfVersion14, "%PDF-1.4\n" };
        yield return new object[] { FileHeader.PdfVersion15, "%PDF-1.5\n" };
        yield return new object[] { FileHeader.PdfVersion16, "%PDF-1.6\n" };
        yield return new object[] { FileHeader.PdfVersion17, "%PDF-1.7\n" };
        yield return new object[] { FileHeader.PdfVersion20, "%PDF-2.0\n" };
    }

    public static IEnumerable<object[]> FileHeader_MajorVersion_TestCases()
    {
        yield return new object[] { FileHeader.PdfVersion10, 1 };
        yield return new object[] { FileHeader.PdfVersion11, 1 };
        yield return new object[] { FileHeader.PdfVersion12, 1 };
        yield return new object[] { FileHeader.PdfVersion13, 1 };
        yield return new object[] { FileHeader.PdfVersion14, 1 };
        yield return new object[] { FileHeader.PdfVersion15, 1 };
        yield return new object[] { FileHeader.PdfVersion16, 1 };
        yield return new object[] { FileHeader.PdfVersion17, 1 };
        yield return new object[] { FileHeader.PdfVersion20, 2 };
    }

    public static IEnumerable<object[]> FileHeader_MinorVersion_TestCases()
    {
        yield return new object[] { FileHeader.PdfVersion10, 0 };
        yield return new object[] { FileHeader.PdfVersion11, 1 };
        yield return new object[] { FileHeader.PdfVersion12, 2 };
        yield return new object[] { FileHeader.PdfVersion13, 3 };
        yield return new object[] { FileHeader.PdfVersion14, 4 };
        yield return new object[] { FileHeader.PdfVersion15, 5 };
        yield return new object[] { FileHeader.PdfVersion16, 6 };
        yield return new object[] { FileHeader.PdfVersion17, 7 };
        yield return new object[] { FileHeader.PdfVersion20, 0 };
    }

    public static IEnumerable<object[]> FileHeader_Length_TestCases()
    {
        yield return new object[] { FileHeader.PdfVersion10, 9 };
        yield return new object[] { FileHeader.PdfVersion11, 9 };
        yield return new object[] { FileHeader.PdfVersion12, 9 };
        yield return new object[] { FileHeader.PdfVersion13, 9 };
        yield return new object[] { FileHeader.PdfVersion14, 9 };
        yield return new object[] { FileHeader.PdfVersion15, 9 };
        yield return new object[] { FileHeader.PdfVersion16, 9 };
        yield return new object[] { FileHeader.PdfVersion17, 9 };
        yield return new object[] { FileHeader.PdfVersion20, 9 };
    }

    public static IEnumerable<object[]> FileHeader_Bytes_TestCases()
    {
        yield return new object[] { FileHeader.PdfVersion10, new byte[] { 0x25, 0x50, 0x44, 0x46, 0x2D, 0x31, 0x2E, 0x30, 0x0A } };
        yield return new object[] { FileHeader.PdfVersion11, new byte[] { 0x25, 0x50, 0x44, 0x46, 0x2D, 0x31, 0x2E, 0x31, 0x0A } };
        yield return new object[] { FileHeader.PdfVersion12, new byte[] { 0x25, 0x50, 0x44, 0x46, 0x2D, 0x31, 0x2E, 0x32, 0x0A } };
        yield return new object[] { FileHeader.PdfVersion13, new byte[] { 0x25, 0x50, 0x44, 0x46, 0x2D, 0x31, 0x2E, 0x33, 0x0A } };
        yield return new object[] { FileHeader.PdfVersion14, new byte[] { 0x25, 0x50, 0x44, 0x46, 0x2D, 0x31, 0x2E, 0x34, 0x0A } };
        yield return new object[] { FileHeader.PdfVersion15, new byte[] { 0x25, 0x50, 0x44, 0x46, 0x2D, 0x31, 0x2E, 0x35, 0x0A } };
        yield return new object[] { FileHeader.PdfVersion16, new byte[] { 0x25, 0x50, 0x44, 0x46, 0x2D, 0x31, 0x2E, 0x36, 0x0A } };
        yield return new object[] { FileHeader.PdfVersion17, new byte[] { 0x25, 0x50, 0x44, 0x46, 0x2D, 0x31, 0x2E, 0x37, 0x0A } };
        yield return new object[] { FileHeader.PdfVersion20, new byte[] { 0x25, 0x50, 0x44, 0x46, 0x2D, 0x32, 0x2E, 0x30, 0x0A } };
    }

    public static IEnumerable<object[]> FileHeader_Equals_TestCases()
    {
        yield return new object[] { FileHeader.PdfVersion10, FileHeader.PdfVersion10, true };
        yield return new object[] { FileHeader.PdfVersion11, FileHeader.PdfVersion10, false };
        yield return new object[] { FileHeader.PdfVersion12, FileHeader.PdfVersion12, true };
        yield return new object[] { FileHeader.PdfVersion20, FileHeader.PdfVersion10, false };
    }

    public static IEnumerable<object[]> FileHeader_EqualsObject_TestCases()
    {
        yield return new object[] { FileHeader.PdfVersion10, FileHeader.PdfVersion10, true };
        yield return new object[] { FileHeader.PdfVersion11, FileHeader.PdfVersion10, false };
        yield return new object[] { FileHeader.PdfVersion12, FileHeader.PdfVersion12, true };
        yield return new object[] { FileHeader.PdfVersion20, FileHeader.PdfVersion10, false };
        yield return new object[] { FileHeader.PdfVersion20, null!, false };
    }
}

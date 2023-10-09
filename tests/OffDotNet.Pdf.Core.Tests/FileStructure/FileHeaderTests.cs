// <copyright file="FileHeaderTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.Core.FileStructure;
using OffDotNet.Pdf.Core.Properties;
using Xunit;

namespace OffDotNet.Pdf.Core.Tests.FileStructure;

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
        object FileHeaderFunction()
        {
            return new FileHeader(majorVersion, 0);
        }

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
        object FileHeaderFunction()
        {
            return new FileHeader(1, minorVersion);
        }

        // Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(FileHeaderFunction);
        Assert.StartsWith(Resource.FileHeader_MinorVersionIsNotValid, exception.Message);
    }

    [Theory(DisplayName = $"{nameof(FileHeader.Content)} property should return a valid value")]
    [InlineData(1, 0, "%PDF-1.0\n")]
    [InlineData(1, 1, "%PDF-1.1\n")]
    [InlineData(1, 2, "%PDF-1.2\n")]
    [InlineData(1, 3, "%PDF-1.3\n")]
    [InlineData(1, 4, "%PDF-1.4\n")]
    [InlineData(1, 5, "%PDF-1.5\n")]
    [InlineData(1, 6, "%PDF-1.6\n")]
    [InlineData(1, 7, "%PDF-1.7\n")]
    [InlineData(2, 0, "%PDF-2.0\n")]
    public void FileHeader_Content_ShouldReturnValidValue(int majorVersion, int minorVersion, string expectedContent)
    {
        // Arrange
        FileHeader fileHeader = new(majorVersion, minorVersion);

        // Act
        string actualContent = fileHeader.Content;

        // Assert
        Assert.Equal(expectedContent, actualContent);
    }

    [Theory(DisplayName = $"{nameof(FileHeader.MajorVersion)} property should return a valid value")]
    [InlineData(1, 0, 1)]
    [InlineData(1, 1, 1)]
    [InlineData(1, 2, 1)]
    [InlineData(1, 3, 1)]
    [InlineData(1, 4, 1)]
    [InlineData(1, 5, 1)]
    [InlineData(1, 6, 1)]
    [InlineData(1, 7, 1)]
    [InlineData(2, 0, 2)]
    public void FileHeader_MajorVersion_ShouldReturnValidValue(int majorVersion, int minorVersion, int expectedMajorVersion)
    {
        // Arrange
        FileHeader fileHeader = new(majorVersion, minorVersion);

        // Act
        int actualMajorVersion = fileHeader.MajorVersion;

        // Assert
        Assert.Equal(expectedMajorVersion, actualMajorVersion);
    }

    [Theory(DisplayName = $"{nameof(FileHeader.MinorVersion)} property should return a valid value")]
    [InlineData(1, 0, 0)]
    [InlineData(1, 1, 1)]
    [InlineData(1, 2, 2)]
    [InlineData(1, 3, 3)]
    [InlineData(1, 4, 4)]
    [InlineData(1, 5, 5)]
    [InlineData(1, 6, 6)]
    [InlineData(1, 7, 7)]
    [InlineData(2, 0, 0)]
    public void FileHeader_MinorVersion_ShouldReturnValidValue(int majorVersion, int minorVersion, int expectedMinorVersion)
    {
        // Arrange
        FileHeader fileHeader = new(majorVersion, minorVersion);

        // Act
        int actualMinorVersion = fileHeader.MinorVersion;

        // Assert
        Assert.Equal(expectedMinorVersion, actualMinorVersion);
    }

    [Theory(DisplayName = $"{nameof(FileHeader.Bytes)} property should return a valid value")]
    [InlineData(1, 0, new byte[] { 0x25, 0x50, 0x44, 0x46, 0x2D, 0x31, 0x2E, 0x30, 0x0A })]
    [InlineData(1, 1, new byte[] { 0x25, 0x50, 0x44, 0x46, 0x2D, 0x31, 0x2E, 0x31, 0x0A })]
    [InlineData(1, 2, new byte[] { 0x25, 0x50, 0x44, 0x46, 0x2D, 0x31, 0x2E, 0x32, 0x0A })]
    [InlineData(1, 3, new byte[] { 0x25, 0x50, 0x44, 0x46, 0x2D, 0x31, 0x2E, 0x33, 0x0A })]
    [InlineData(1, 4, new byte[] { 0x25, 0x50, 0x44, 0x46, 0x2D, 0x31, 0x2E, 0x34, 0x0A })]
    [InlineData(1, 5, new byte[] { 0x25, 0x50, 0x44, 0x46, 0x2D, 0x31, 0x2E, 0x35, 0x0A })]
    [InlineData(1, 6, new byte[] { 0x25, 0x50, 0x44, 0x46, 0x2D, 0x31, 0x2E, 0x36, 0x0A })]
    [InlineData(1, 7, new byte[] { 0x25, 0x50, 0x44, 0x46, 0x2D, 0x31, 0x2E, 0x37, 0x0A })]
    [InlineData(2, 0, new byte[] { 0x25, 0x50, 0x44, 0x46, 0x2D, 0x32, 0x2E, 0x30, 0x0A })]
    public void FileHeader_Bytes_ShouldReturnValidValue(int majorVersion, int minorVersion, byte[] expectedBytes)
    {
        // Arrange
        FileHeader fileHeader = new(majorVersion, minorVersion);

        // Act
        byte[] actualBytes = fileHeader.Bytes.ToArray();

        // Assert
        Assert.Equal(expectedBytes, actualBytes);
    }

    [Theory(DisplayName = "Equals method should return a valid value")]
    [InlineData(1, 0, 1, 0, true)]
    [InlineData(1, 1, 1, 0, false)]
    [InlineData(1, 2, 1, 2, true)]
    [InlineData(2, 0, 1, 0, false)]
    public void FileHeader_Equals_ShouldReturnValidValue(int majorVersion1, int minorVersion1, int majorVersion2, int minorVersion2, bool expectedResult)
    {
        // Arrange
        FileHeader fileHeader1 = new(majorVersion1, minorVersion1);
        FileHeader fileHeader2 = new(majorVersion2, minorVersion2);

        // Act
        bool actualResult = fileHeader1.Equals(fileHeader2);

        // Assert
        Assert.Equal(expectedResult, actualResult);
    }

    [Theory(DisplayName = "Equals method should return a valid value")]
    [InlineData(1, 0, 1, 0, true)]
    [InlineData(1, 1, 1, 0, false)]
    [InlineData(1, 2, 1, 2, true)]
    [InlineData(2, 0, 1, 0, false)]
    public void FileHeader_EqualsObject_ShouldReturnValidValue(int majorVersion1, int minorVersion1, int majorVersion2, int minorVersion2, bool expectedResult)
    {
        // Arrange
        FileHeader fileHeader1 = new(majorVersion1, minorVersion1);
        object fileHeader2 = new FileHeader(majorVersion2, minorVersion2);

        // Act
        bool actualResult = fileHeader1.Equals(fileHeader2);

        // Assert
        Assert.Equal(expectedResult, actualResult);
    }

    [Theory(DisplayName = "The '==' operator should return a valid value")]
    [InlineData(1, 0, 1, 0, true)]
    [InlineData(1, 1, 1, 0, false)]
    [InlineData(1, 2, 1, 2, true)]
    [InlineData(2, 0, 1, 0, false)]
    public void FileHeader_EqualOperator_ShouldReturnValidValue(int majorVersion1, int minorVersion1, int majorVersion2, int minorVersion2, bool expectedResult)
    {
        // Arrange
        FileHeader fileHeader1 = new(majorVersion1, minorVersion1);
        FileHeader fileHeader2 = new(majorVersion2, minorVersion2);

        // Act
        bool actualResult = fileHeader1 == fileHeader2;

        // Assert
        Assert.Equal(expectedResult, actualResult);
    }

    [Theory(DisplayName = "The '!=' operator should return a valid value")]
    [InlineData(1, 0, 1, 0, true)]
    [InlineData(1, 1, 1, 0, false)]
    [InlineData(1, 2, 1, 2, true)]
    [InlineData(2, 0, 1, 0, false)]
    public void FileHeader_NotEqualOperator_ShouldReturnValidValue(int majorVersion1, int minorVersion1, int majorVersion2, int minorVersion2, bool expectedResult)
    {
        // Arrange
        FileHeader fileHeader1 = new(majorVersion1, minorVersion1);
        FileHeader fileHeader2 = new(majorVersion2, minorVersion2);

        // Act
        bool actualResult = fileHeader1 != fileHeader2;

        // Assert
        Assert.Equal(!expectedResult, actualResult);
    }

    [Theory(DisplayName = "GetHashCode method should return a valid value")]
    [InlineData(1, 0)]
    [InlineData(1, 1)]
    [InlineData(1, 2)]
    [InlineData(1, 3)]
    [InlineData(1, 4)]
    [InlineData(1, 5)]
    [InlineData(1, 6)]
    [InlineData(1, 7)]
    [InlineData(2, 0)]
    public void FileHeader_GetHashCode_ShouldReturnValidValue(int majorVersion, int minorVersion)
    {
        // Arrange
        FileHeader fileHeader = new(majorVersion, minorVersion);
        int expectedHashCode = HashCode.Combine(nameof(FileHeader), fileHeader.MajorVersion, fileHeader.MinorVersion);

        // Act
        int actualHashCode = fileHeader.GetHashCode();

        // Assert
        Assert.Equal(expectedHashCode, actualHashCode);
    }
}

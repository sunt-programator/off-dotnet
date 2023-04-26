// <copyright file="XRefEntryTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Diagnostics;
using Off.Net.Pdf.Core.FileStructure;
using Xunit;

namespace Off.Net.Pdf.Core.Tests.FileStructure;

public class XRefEntryTests
{
    [Theory(DisplayName = $"Constructor with negative {nameof(XRefEntry.ByteOffset)} should throw an {nameof(ArgumentOutOfRangeException)}")]
    [InlineData(-1)]
    [InlineData(-15)]
    [InlineData(-23)]
    public void XRefEntry_NegativeByteOffset_ShouldThrowException(int byteOffset)
    {
        // Arrange

        // Act
        XRefEntry XRefEntryFunction()
        {
            return new(byteOffset, 0, XRefEntryType.InUse);
        }

        // Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(XRefEntryFunction);
        Assert.StartsWith(Resource.XRefEntry_ByteOffsetMustBePositive, exception.Message);
    }

    [Theory(DisplayName = $"Constructor with overflowed {nameof(XRefEntry.ByteOffset)} should throw an {nameof(ArgumentOutOfRangeException)}")]
    [InlineData(99999999999)]
    [InlineData(19999999999)]
    [InlineData(10000000000)]
    [InlineData(long.MaxValue)]
    public void XRefEntry_OverflowByteOffset_ShouldThrowException(long byteOffset)
    {
        // Arrange

        // Act
        XRefEntry XRefEntryFunction()
        {
            return new(byteOffset, 0, XRefEntryType.InUse);
        }

        // Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(XRefEntryFunction);
        Assert.StartsWith(Resource.XRefEntry_ByteOffsetMustNotExceedMaxAllowedValue, exception.Message);
    }

    [Theory(DisplayName = $"Constructor with negative {nameof(XRefEntry.GenerationNumber)} should throw an {nameof(ArgumentOutOfRangeException)}")]
    [InlineData(-1)]
    [InlineData(-15)]
    [InlineData(-23)]
    public void XRefEntry_NegativeGenerationNumber_ShouldThrowException(int generationNumber)
    {
        // Arrange

        // Act
        XRefEntry XRefEntryFunction()
        {
            return new(0, generationNumber, XRefEntryType.InUse);
        }

        // Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(XRefEntryFunction);
        Assert.StartsWith(Resource.PdfIndirect_GenerationNumberMustBePositive, exception.Message);
    }

    [Theory(DisplayName = $"Constructor with overflowed {nameof(XRefEntry.GenerationNumber)} should throw an {nameof(ArgumentOutOfRangeException)}")]
    [InlineData(100000)]
    [InlineData(ushort.MaxValue + 1)]
    [InlineData(int.MaxValue)]
    public void XRefEntry_OverflowGenerationNumber_ShouldThrowException(int generationNumber)
    {
        // Arrange

        // Act
        XRefEntry XRefEntryFunction()
        {
            return new(0, generationNumber, XRefEntryType.InUse);
        }

        // Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(XRefEntryFunction);
        Assert.StartsWith(Resource.PdfIndirect_GenerationNumberMustNotExceedMaxAllowedValue, exception.Message);
    }

    [Theory(DisplayName = $"{nameof(XRefEntry.Content)} property should return a valid value")]
    [InlineData(0, 0, true, "0000000000 00000 n \n")]
    [InlineData(52, 123, false, "0000000052 00123 f \n")]
    [InlineData(4567, 1234, false, "0000004567 01234 f \n")]
    [InlineData(9999999999, 65535, true, "9999999999 65535 n \n")]
    public void XRefEntry_Content_ShouldReturnValidValue(long byteOffset, int generationNumber, bool isInUse, string expectedContent)
    {
        // Arrange
        XRefEntry xRefEntry = new(byteOffset, generationNumber, isInUse ? XRefEntryType.InUse : XRefEntryType.Free);

        // Act
        string actualContent = xRefEntry.Content;

        // Assert
        Assert.Equal(expectedContent, actualContent);
    }

    [Theory(DisplayName = $"{nameof(XRefEntry.Length)} property should return always 20")]
    [InlineData(0, 0, true)]
    [InlineData(52, 123, false)]
    [InlineData(4567, 1234, false)]
    [InlineData(9999999999, 65535, true)]
    public void XRefEntry_Length_ShouldReturn20(long byteOffset, int generationNumber, bool isInUse)
    {
        // Arrange
        const int expectedLength = 20;
        XRefEntry xRefEntry = new(byteOffset, generationNumber, isInUse ? XRefEntryType.InUse : XRefEntryType.Free);

        // Act
        int actualLength = xRefEntry.Length;

        // Assert
        Assert.Equal(expectedLength, actualLength);
    }

    [Theory(DisplayName = $"{nameof(XRefEntry.ByteOffset)} property should return a valid value")]
    [InlineData(0, 0, true)]
    [InlineData(52, 123, false)]
    [InlineData(4567, 1234, false)]
    [InlineData(9999999999, 65535, true)]
    public void XRefEntry_ByteOffset_ShouldReturnValidValue(long byteOffset, int generationNumber, bool isInUse)
    {
        // Arrange
        XRefEntry xRefEntry = new(byteOffset, generationNumber, isInUse ? XRefEntryType.InUse : XRefEntryType.Free);

        // Act
        long actualByteOffset = xRefEntry.ByteOffset;

        // Assert
        Assert.Equal(byteOffset, actualByteOffset);
    }

    [Theory(DisplayName = $"{nameof(XRefEntry.GenerationNumber)} property should return a valid value")]
    [InlineData(0, 0, true)]
    [InlineData(52, 123, false)]
    [InlineData(4567, 1234, false)]
    [InlineData(9999999999, 65535, true)]
    public void XRefEntry_GenerationNumber_ShouldReturnValidValue(long byteOffset, int generationNumber, bool isInUse)
    {
        // Arrange
        XRefEntry xRefEntry = new(byteOffset, generationNumber, isInUse ? XRefEntryType.InUse : XRefEntryType.Free);

        // Act
        long actualGenerationNumber = xRefEntry.GenerationNumber;

        // Assert
        Assert.Equal(generationNumber, actualGenerationNumber);
    }

    [Theory(DisplayName = $"{nameof(XRefEntry.EntryType)} property should return a valid value")]
    [InlineData(0, 0, true)]
    [InlineData(52, 123, false)]
    [InlineData(4567, 1234, false)]
    [InlineData(9999999999, 65535, true)]
    public void XRefEntry_EntryType_ShouldReturnValidValue(long byteOffset, int generationNumber, bool isInUse)
    {
        // Arrange
        XRefEntryType xRefEntryType = isInUse ? XRefEntryType.InUse : XRefEntryType.Free;
        XRefEntry xRefEntry = new(byteOffset, generationNumber, xRefEntryType);

        // Act
        XRefEntryType actualXRefEntryType = xRefEntry.EntryType;

        // Assert
        Assert.Equal(xRefEntryType, actualXRefEntryType);
    }

    [Theory(DisplayName = $"{nameof(XRefEntry.Bytes)} property should return a valid value")]
    [InlineData(0, 0, true, new byte[] { 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x20, 0x30, 0x30, 0x30, 0x30, 0x30, 0x20, 0x6e, 0x20, 0x0a })]
    [InlineData(52, 123, false, new byte[] { 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x35, 0x32, 0x20, 0x30, 0x30, 0x31, 0x32, 0x33, 0x20, 0x66, 0x20, 0x0a })]
    [InlineData(4567, 1234, false, new byte[] { 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x34, 0x35, 0x36, 0x37, 0x20, 0x30, 0x31, 0x32, 0x33, 0x34, 0x20, 0x66, 0x20, 0x0a })]
    [InlineData(9999999999, 65535, true, new byte[] { 0x39, 0x39, 0x39, 0x39, 0x39, 0x39, 0x39, 0x39, 0x39, 0x39, 0x20, 0x36, 0x35, 0x35, 0x33, 0x35, 0x20, 0x6e, 0x20, 0x0a })]
    public void XRefEntry_Bytes_ShouldReturnValidValue(long byteOffset, int generationNumber, bool isInUse, byte[] expectedBytes)
    {
        // Arrange
        XRefEntry xRefEntry = new(byteOffset, generationNumber, isInUse ? XRefEntryType.InUse : XRefEntryType.Free);

        // Act
        byte[] actualBytes = xRefEntry.Bytes.ToArray();

        // Assert
        Assert.Equal(expectedBytes, actualBytes);
    }

    [Theory(DisplayName = "Check if GetHashCode method returns valid value")]
    [InlineData(0, 0, true)]
    [InlineData(52, 123, false)]
    [InlineData(4567, 1234, false)]
    [InlineData(9999999999, 65535, true)]
    public void XRefEntry_GetHashCode_CheckValidity(long byteOffset, int generationNumber, bool isInUse)
    {
        // Arrange
        XRefEntryType xRefEntryType = isInUse ? XRefEntryType.InUse : XRefEntryType.Free;
        XRefEntry xRefEntry = new(byteOffset, generationNumber, xRefEntryType);
        int expectedHashCode = HashCode.Combine(nameof(XRefEntry), byteOffset, generationNumber);

        // Act
        int actualHashCode = xRefEntry.GetHashCode();

        // Assert
        Assert.Equal(expectedHashCode, actualHashCode);
    }

    [Fact(DisplayName = $"{nameof(XRefEntry.Content)} property, accessed multiple times, should return the same reference")]
    public void XRefEntry_Content_MultipleAccesses_ShouldReturnSameReference()
    {
        // Arrange
        XRefEntry xRefEntry = new(0, 0, XRefEntryType.InUse);

        // Act
        string actualContent1 = xRefEntry.Content;
        string actualContent2 = xRefEntry.Content;

        // Assert
        Assert.True(ReferenceEquals(actualContent1, actualContent2));
    }

    [Theory(DisplayName = "Check if Equals returns a valid result")]
    [InlineData(0, 0, 0, 0, true, true, true)]
    [InlineData(52, 25, 123, 123, false, false, false)]
    [InlineData(4567, 4567, 1234, 4321, false, false, false)]
    [InlineData(9999999999, 9999999999, 65535, 65535, true, false, false)]
    [InlineData(9999999999, 9999999999, 65535, 65535, true, true, true)]
    public void XRefEntry_Equals_CheckValidity(long byteOffset1, long byteOffset2, int generationNumber1, int generationNumber2, bool isInUse1, bool isInUse2, bool expectedValue)
    {
        // Arrange
        XRefEntryType xRefEntryType1 = isInUse1 ? XRefEntryType.InUse : XRefEntryType.Free;
        XRefEntryType xRefEntryType2 = isInUse2 ? XRefEntryType.InUse : XRefEntryType.Free;
        XRefEntry xRefEntry1 = new(byteOffset1, generationNumber1, xRefEntryType1);
        XRefEntry xRefEntry2 = new(byteOffset2, generationNumber2, xRefEntryType2);

        // Act
        bool actualResult = xRefEntry1.Equals(xRefEntry2);

        // Assert
        Assert.Equal(expectedValue, actualResult);
    }

    [Fact(DisplayName = "Check if Equals method with null object returns always false")]
    public void XRefEntry_EqualsNullObject_CheckValidity()
    {
        // Arrange
        XRefEntry xRefEntry = new(0, 0, XRefEntryType.InUse);

        // Act
        bool actualResult1 = xRefEntry.Equals(null);

        Debug.Assert(xRefEntry != null, nameof(xRefEntry) + " != null");
        bool actualResult2 = xRefEntry.Equals((object?)null);

        // Assert
        Assert.False(actualResult1);
        Assert.False(actualResult2);
    }
}

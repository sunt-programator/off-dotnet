// <copyright file="XRefSubSectionTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Tests.FileStructure;

using System.Diagnostics;
using OffDotNet.Pdf.Core.FileStructure;
using Properties;

public class XRefSubSectionTests
{
    [Theory(DisplayName = $"Constructor with negative {nameof(XRefSubSection.ObjectNumber)} should throw an {nameof(ArgumentOutOfRangeException)}")]
    [InlineData(-1)]
    [InlineData(-15)]
    [InlineData(-23)]
    public void XRefSubSection_NegativeByteOffset_ShouldThrowException(int objectNumber)
    {
        // Arrange
        ICollection<IxRefEntry> entries = new List<IxRefEntry>(1) { new XRefEntry(0, 0, XRefEntryType.Free) };

        // Act
        IxRefSubSection XRefSubSectionFunction()
        {
            return new XRefSubSection(objectNumber, entries);
        }

        // Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(XRefSubSectionFunction);
        Assert.StartsWith(Resource.PdfIndirect_ObjectNumberMustBePositive, exception.Message);
    }

    [Fact(DisplayName = $"Constructor with an empty list of {nameof(XRefEntry)} should throw an {nameof(ArgumentOutOfRangeException)}")]
    public void XRefSubSection_NegativeGenerationNumber_ShouldThrowException()
    {
        // Arrange
        ICollection<IxRefEntry> entries = new List<IxRefEntry>(0);

        // Act
        IxRefSubSection XRefSubSectionFunction()
        {
            return new XRefSubSection(0, entries);
        }

        // Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(XRefSubSectionFunction);
        Assert.StartsWith(Resource.XRefSubSection_MustHaveNonEmptyEntriesCollection, exception.Message);
    }

    [Fact(DisplayName = $"{nameof(XRefSubSection.Content)} property should return a valid value")]
    public void XRefSubSection_Content_ShouldReturnValidValue()
    {
        // Arrange
        const string ExpectedContent = "23 2\n0000025518 00002 n \n0000025635 00000 f \n";
        const int ObjectNumber = 23;
        IxRefEntry xRefEntry1 = new XRefEntry(25518, 2, XRefEntryType.InUse);
        IxRefEntry xRefEntry2 = new XRefEntry(25635, 0, XRefEntryType.Free);
        IxRefSubSection xRefSubSection = new XRefSubSection(ObjectNumber, new List<IxRefEntry> { xRefEntry1, xRefEntry2 });

        // Act
        var actualContent = xRefSubSection.Content;

        // Assert
        Assert.Equal(ExpectedContent, actualContent);
    }

    [Fact(DisplayName = $"{nameof(XRefSubSection.ObjectNumber)} property should return a valid value")]
    public void XRefSubSection_ObjectNumber_ShouldReturnValidValue()
    {
        // Arrange
        const int ObjectNumber = 23;
        IxRefEntry xRefEntry1 = new XRefEntry(25518, 2, XRefEntryType.InUse);
        IxRefEntry xRefEntry2 = new XRefEntry(25635, 0, XRefEntryType.Free);
        IxRefSubSection xRefSubSection = new XRefSubSection(ObjectNumber, new List<IxRefEntry> { xRefEntry1, xRefEntry2 });

        // Act
        long actualObjectNumber = xRefSubSection.ObjectNumber;

        // Assert
        Assert.Equal(ObjectNumber, actualObjectNumber);
    }

    [Fact(DisplayName = $"{nameof(XRefSubSection.Bytes)} property should return a valid value")]
    public void XRefSubSection_Bytes_ShouldReturnValidValue()
    {
        // Arrange
        byte[] expectedBytes =
        [
            0x32,
            0x33,
            0x20,
            0x32,
            0x0A,
            0x30,
            0x30,
            0x30,
            0x30,
            0x30,
            0x32,
            0x35,
            0x35,
            0x31,
            0x38,
            0x20,
            0x30,
            0x30,
            0x30,
            0x30,
            0x32,
            0x20,
            0x6E,
            0x20,
            0x0A,
            0x30,
            0x30,
            0x30,
            0x30,
            0x30,
            0x32,
            0x35,
            0x36,
            0x33,
            0x35,
            0x20,
            0x30,
            0x30,
            0x30,
            0x30,
            0x30,
            0x20,
            0x6E,
            0x20,
            0x0A
        ];

        const int ObjectNumber = 23;
        IxRefEntry xRefEntry1 = new XRefEntry(25518, 2, XRefEntryType.InUse);
        IxRefEntry xRefEntry2 = new XRefEntry(25635, 0, XRefEntryType.InUse);
        IxRefSubSection xRefSubSection = new XRefSubSection(ObjectNumber, new List<IxRefEntry> { xRefEntry1, xRefEntry2 });

        // Act
        var actualBytes = xRefSubSection.Bytes.ToArray();

        // Assert
        Assert.Equal(expectedBytes, actualBytes);
    }

    [Fact(DisplayName = $"{nameof(XRefSubSection.Content)} property, accessed multiple times, should return the same reference")]
    public void XRefSubSection_Content_MultipleAccesses_ShouldReturnSameReference()
    {
        // Arrange
        const int ObjectNumber = 23;
        IxRefEntry xRefEntry1 = new XRefEntry(25518, 2, XRefEntryType.InUse);
        IxRefEntry xRefEntry2 = new XRefEntry(25635, 0, XRefEntryType.InUse);
        IxRefSubSection xRefSubSection = new XRefSubSection(ObjectNumber, new List<IxRefEntry> { xRefEntry1, xRefEntry2 });

        // Act
        var actualContent1 = xRefSubSection.Content;
        var actualContent2 = xRefSubSection.Content;

        // Assert
        Assert.True(ReferenceEquals(actualContent1, actualContent2));
    }

    [Fact(DisplayName = "Check if Equals returns true")]
    public void XRefSubSection_Equals_MustReturnTrue()
    {
        // Arrange
        const int ObjectNumber = 23;
        IxRefEntry xRefEntry1 = new XRefEntry(25518, 2, XRefEntryType.InUse);
        IxRefEntry xRefEntry2 = new XRefEntry(25635, 0, XRefEntryType.InUse);
        IxRefSubSection xRefSubSection1 = new XRefSubSection(ObjectNumber, new List<IxRefEntry> { xRefEntry1, xRefEntry2 });
        IxRefSubSection xRefSubSection2 = new XRefSubSection(ObjectNumber, new List<IxRefEntry> { xRefEntry1, xRefEntry2 });

        // Act
        var actualResult = xRefSubSection1.Equals(xRefSubSection2);

        // Assert
        Assert.True(actualResult);
    }

    [Fact(DisplayName = "Check if Equals returns false")]
    public void XRefSubSection_Equals_MustReturnFalse()
    {
        // Arrange
        const int ObjectNumber = 23;
        IxRefEntry xRefEntry1 = new XRefEntry(25518, 2, XRefEntryType.InUse);
        IxRefEntry xRefEntry2 = new XRefEntry(25635, 0, XRefEntryType.InUse);
        IxRefSubSection xRefSubSection1 = new XRefSubSection(ObjectNumber, new List<IxRefEntry> { xRefEntry1, xRefEntry2 });
        IxRefSubSection xRefSubSection2 = new XRefSubSection(ObjectNumber, new List<IxRefEntry> { xRefEntry1 });

        // Act
        var actualResult = xRefSubSection1.Equals(xRefSubSection2);

        // Assert
        Assert.False(actualResult);
    }

    [Fact(DisplayName = "Check if Equals method with null object returns always false")]
    public void XRefSubSection_EqualsNullObject_CheckValidity()
    {
        // Arrange
        const int ObjectNumber = 23;
        IxRefEntry xRefEntry1 = new XRefEntry(25518, 2, XRefEntryType.InUse);
        IxRefEntry xRefEntry2 = new XRefEntry(25635, 0, XRefEntryType.InUse);
        IxRefSubSection xRefSubSection = new XRefSubSection(ObjectNumber, new List<IxRefEntry> { xRefEntry1, xRefEntry2 });

        // Act
        var actualResult1 = xRefSubSection.Equals(null);

        Debug.Assert(xRefSubSection != null, nameof(xRefSubSection) + " != null");
        var actualResult2 = xRefSubSection.Equals(null);

        // Assert
        Assert.False(actualResult1);
        Assert.False(actualResult2);
    }
}

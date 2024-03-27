// <copyright file="XRefTableTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Tests.FileStructure;

using System.Diagnostics;
using OffDotNet.Pdf.Core.FileStructure;
using Properties;

public class XRefTableTests
{
    [Fact(DisplayName = $"Constructor with an empty {nameof(XRefSection)}s collection should throw an {nameof(ArgumentOutOfRangeException)}")]
    public void XRefTable_NegativeGenerationNumber_ShouldThrowException()
    {
        // Arrange
        ICollection<IxRefSection> sections = new List<IxRefSection>(0);

        // Act
        IxRefTable XRefTableFunction()
        {
            return new XRefTable(sections);
        }

        // Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(XRefTableFunction);
        Assert.StartsWith(Resource.XRefTable_MustHaveNonEmptyEntriesCollection, exception.Message);
    }

    [Fact(DisplayName = $"{nameof(XRefTable.Content)} property should return a valid value")]
    public void XRefTable_Content_ShouldReturnValidValue()
    {
        // Arrange
        const string ExpectedContent = "xref\n0 1\n0000000000 65535 f \n3 1\n0000025325 00000 n \n30 1\n0000025777 00000 n \n23 2\n0000025518 00002 n \n0000025635 00000 n \n";
        var xRefSubSection1 = new XRefEntry(0, 65535, XRefEntryType.Free).ToXRefSubSection(0);
        var xRefSubSection2 = new XRefEntry(25325, 0, XRefEntryType.InUse).ToXRefSubSection(3);
        var xRefSubSection3 = new XRefEntry(25777, 0, XRefEntryType.InUse).ToXRefSubSection(30);
        List<IxRefEntry> multipleEntries =
            [new XRefEntry(25518, 2, XRefEntryType.InUse), new XRefEntry(25635, 0, XRefEntryType.InUse)];
        List<IxRefSubSection> subSections =
            [xRefSubSection1, xRefSubSection2, xRefSubSection3, multipleEntries.ToXRefSubSection(23)];

        var xRefTable = subSections.ToXRefTable();

        // Act
        var actualContent = xRefTable.Content;

        // Assert
        Assert.Equal(ExpectedContent, actualContent);
    }

    [Fact(DisplayName = $"{nameof(XRefTable.Bytes)} property should return a valid value")]
    public void XRefTable_Bytes_ShouldReturnValidValue()
    {
        // Arrange
        byte[] expectedBytes =
        [
            0x78,
            0x72,
            0x65,
            0x66,
            0x0A,
            0x30,
            0x20,
            0x31,
            0x0A,
            0x30,
            0x30,
            0x30,
            0x30,
            0x30,
            0x30,
            0x30,
            0x30,
            0x30,
            0x30,
            0x20,
            0x36,
            0x35,
            0x35,
            0x33,
            0x35,
            0x20,
            0x66,
            0x20,
            0x0A,
            0x33,
            0x20,
            0x31,
            0x0A,
            0x30,
            0x30,
            0x30,
            0x30,
            0x30,
            0x32,
            0x35,
            0x33,
            0x32,
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
            0x0A,
            0x33,
            0x30,
            0x20,
            0x31,
            0x0A,
            0x30,
            0x30,
            0x30,
            0x30,
            0x30,
            0x32,
            0x35,
            0x37,
            0x37,
            0x37,
            0x20,
            0x30,
            0x30,
            0x30,
            0x30,
            0x30,
            0x20,
            0x6E,
            0x20,
            0x0A,
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
        var xRefSubSection1 = new XRefEntry(0, 65535, XRefEntryType.Free).ToXRefSubSection(0);
        var xRefSubSection2 = new XRefEntry(25325, 0, XRefEntryType.InUse).ToXRefSubSection(3);
        var xRefSubSection3 = new XRefEntry(25777, 0, XRefEntryType.InUse).ToXRefSubSection(30);
        List<IxRefEntry> multipleEntries =
            [new XRefEntry(25518, 2, XRefEntryType.InUse), new XRefEntry(25635, 0, XRefEntryType.InUse)];
        List<IxRefSubSection> subSections =
            [xRefSubSection1, xRefSubSection2, xRefSubSection3, multipleEntries.ToXRefSubSection(23)];

        var xRefTable = subSections.ToXRefTable();

        // Act
        var actualBytes = xRefTable.Bytes.ToArray();

        // Assert
        Assert.Equal(expectedBytes, actualBytes);
    }

    [Fact(DisplayName = $"{nameof(XRefTable.Content)} property, accessed multiple times, should return the same reference")]
    public void XRefTable_Content_MultipleAccesses_ShouldReturnSameReference()
    {
        // Arrange
        var xRefSubSection1 = new XRefEntry(0, 65535, XRefEntryType.Free).ToXRefSubSection(0);
        var xRefSubSection2 = new XRefEntry(25325, 0, XRefEntryType.InUse).ToXRefSubSection(3);
        var xRefSubSection3 = new XRefEntry(25777, 0, XRefEntryType.InUse).ToXRefSubSection(30);
        List<IxRefEntry> multipleEntries =
            [new XRefEntry(25518, 2, XRefEntryType.InUse), new XRefEntry(25635, 0, XRefEntryType.InUse)];
        List<IxRefSubSection> subSections =
            [xRefSubSection1, xRefSubSection2, xRefSubSection3, multipleEntries.ToXRefSubSection(23)];

        var xRefTable = subSections.ToXRefTable();

        // Act
        var actualContent1 = xRefTable.Content;
        var actualContent2 = xRefTable.Content;

        // Assert
        Assert.True(ReferenceEquals(actualContent1, actualContent2));
    }

    [Fact(DisplayName = "Check if Equals returns false")]
    public void XRefTable_Equals_MustReturnFalse()
    {
        // Arrange
        var xRefSubSection1 = new XRefEntry(0, 65535, XRefEntryType.Free).ToXRefSubSection(0);
        var xRefSubSection2 = new XRefEntry(25325, 0, XRefEntryType.InUse).ToXRefSubSection(3);
        var xRefSubSection3 = new XRefEntry(25777, 0, XRefEntryType.InUse).ToXRefSubSection(30);
        List<IxRefEntry> multipleEntries =
            [new XRefEntry(25518, 2, XRefEntryType.InUse), new XRefEntry(25635, 0, XRefEntryType.InUse)];
        List<IxRefSubSection> subSections1 =
            [xRefSubSection1, xRefSubSection2, xRefSubSection3, multipleEntries.ToXRefSubSection(23)];
        List<IxRefSubSection> subSections2 = [xRefSubSection1, xRefSubSection2, xRefSubSection3];

        var xRefTable1 = subSections1.ToXRefTable();
        var xRefTable2 = subSections2.ToXRefTable();

        // Act
        var actualResult = xRefTable1.Equals(xRefTable2);

        // Assert
        Assert.False(actualResult);
    }

    [Fact(DisplayName = "Check if Equals returns true")]
    public void XRefTable_Equals_MustReturnTrue()
    {
        // Arrange
        var xRefSubSection1 = new XRefEntry(0, 65535, XRefEntryType.Free).ToXRefSubSection(0);
        var xRefSubSection2 = new XRefEntry(25325, 0, XRefEntryType.InUse).ToXRefSubSection(3);
        var xRefSubSection3 = new XRefEntry(25777, 0, XRefEntryType.InUse).ToXRefSubSection(30);
        List<IxRefEntry> multipleEntries =
            [new XRefEntry(25518, 2, XRefEntryType.InUse), new XRefEntry(25635, 0, XRefEntryType.InUse)];
        List<IxRefSubSection> subSections1 =
            [xRefSubSection1, xRefSubSection2, xRefSubSection3, multipleEntries.ToXRefSubSection(23)];
        List<IxRefSubSection> subSections2 =
            [xRefSubSection1, xRefSubSection2, xRefSubSection3, multipleEntries.ToXRefSubSection(23)];

        var xRefTable1 = subSections1.ToXRefTable();
        var xRefTable2 = subSections2.ToXRefTable();

        // Act
        var actualResult = xRefTable1.Equals(xRefTable2);

        // Assert
        Assert.True(actualResult);
    }

    [Fact(DisplayName = "Check if Equals method with null object returns always false")]
    public void XRefTable_EqualsNullObject_CheckValidity()
    {
        // Arrange
        var xRefSubSection1 = new XRefEntry(0, 65535, XRefEntryType.Free).ToXRefSubSection(0);
        var xRefSubSection2 = new XRefEntry(25325, 0, XRefEntryType.InUse).ToXRefSubSection(3);
        var xRefSubSection3 = new XRefEntry(25777, 0, XRefEntryType.InUse).ToXRefSubSection(30);
        List<IxRefEntry> multipleEntries =
            [new XRefEntry(25518, 2, XRefEntryType.InUse), new XRefEntry(25635, 0, XRefEntryType.InUse)];
        List<IxRefSubSection> subSections =
            [xRefSubSection1, xRefSubSection2, xRefSubSection3, multipleEntries.ToXRefSubSection(23)];

        var xRefTable = subSections.ToXRefTable();

        // Act
        var actualResult1 = xRefTable.Equals(null);

        Debug.Assert(xRefTable != null, nameof(xRefTable) + " != null");
        var actualResult2 = xRefTable.Equals(null);

        // Assert
        Assert.False(actualResult1);
        Assert.False(actualResult2);
    }
}

// <copyright file="XRefTableTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Tests.FileStructure;

using System.Diagnostics;
using OffDotNet.Pdf.Core.FileStructure;
using OffDotNet.Pdf.Core.Properties;

public class XRefTableTests
{
    [Fact(DisplayName = $"Constructor with an empty {nameof(XRefSection)}s collection should throw an {nameof(ArgumentOutOfRangeException)}")]
    public void XRefTable_NegativeGenerationNumber_ShouldThrowException()
    {
        // Arrange
        ICollection<IXRefSection> sections = new List<IXRefSection>(0);

        // Act
        IXRefTable XRefTableFunction()
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
        const string expectedContent = "xref\n0 1\n0000000000 65535 f \n3 1\n0000025325 00000 n \n30 1\n0000025777 00000 n \n23 2\n0000025518 00002 n \n0000025635 00000 n \n";
        IXRefSubSection xRefSubSection1 = new XRefEntry(0, 65535, XRefEntryType.Free).ToXRefSubSection(0);
        IXRefSubSection xRefSubSection2 = new XRefEntry(25325, 0, XRefEntryType.InUse).ToXRefSubSection(3);
        IXRefSubSection xRefSubSection3 = new XRefEntry(25777, 0, XRefEntryType.InUse).ToXRefSubSection(30);
        List<IXRefEntry> multipleEntries = new() { new XRefEntry(25518, 2, XRefEntryType.InUse), new XRefEntry(25635, 0, XRefEntryType.InUse) };
        List<IXRefSubSection> subSections = new() { xRefSubSection1, xRefSubSection2, xRefSubSection3, multipleEntries.ToXRefSubSection(23) };

        IXRefTable xRefTable = subSections.ToXRefTable();

        // Act
        string actualContent = xRefTable.Content;

        // Assert
        Assert.Equal(expectedContent, actualContent);
    }

    [Fact(DisplayName = $"{nameof(XRefTable.Bytes)} property should return a valid value")]
    public void XRefTable_Bytes_ShouldReturnValidValue()
    {
        // Arrange
        byte[] expectedBytes =
        {
            0x78, 0x72, 0x65, 0x66, 0x0A, 0x30, 0x20, 0x31, 0x0A, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x20, 0x36, 0x35, 0x35, 0x33, 0x35, 0x20, 0x66, 0x20, 0x0A, 0x33, 0x20,
            0x31, 0x0A, 0x30, 0x30, 0x30, 0x30, 0x30, 0x32, 0x35, 0x33, 0x32, 0x35, 0x20, 0x30, 0x30, 0x30, 0x30, 0x30, 0x20, 0x6E, 0x20, 0x0A, 0x33, 0x30, 0x20, 0x31, 0x0A, 0x30, 0x30, 0x30, 0x30,
            0x30, 0x32, 0x35, 0x37, 0x37, 0x37, 0x20, 0x30, 0x30, 0x30, 0x30, 0x30, 0x20, 0x6E, 0x20, 0x0A, 0x32, 0x33, 0x20, 0x32, 0x0A, 0x30, 0x30, 0x30, 0x30, 0x30, 0x32, 0x35, 0x35, 0x31, 0x38,
            0x20, 0x30, 0x30, 0x30, 0x30, 0x32, 0x20, 0x6E, 0x20, 0x0A, 0x30, 0x30, 0x30, 0x30, 0x30, 0x32, 0x35, 0x36, 0x33, 0x35, 0x20, 0x30, 0x30, 0x30, 0x30, 0x30, 0x20, 0x6E, 0x20, 0x0A,
        };
        IXRefSubSection xRefSubSection1 = new XRefEntry(0, 65535, XRefEntryType.Free).ToXRefSubSection(0);
        IXRefSubSection xRefSubSection2 = new XRefEntry(25325, 0, XRefEntryType.InUse).ToXRefSubSection(3);
        IXRefSubSection xRefSubSection3 = new XRefEntry(25777, 0, XRefEntryType.InUse).ToXRefSubSection(30);
        List<IXRefEntry> multipleEntries = new() { new XRefEntry(25518, 2, XRefEntryType.InUse), new XRefEntry(25635, 0, XRefEntryType.InUse) };
        List<IXRefSubSection> subSections = new() { xRefSubSection1, xRefSubSection2, xRefSubSection3, multipleEntries.ToXRefSubSection(23) };

        IXRefTable xRefTable = subSections.ToXRefTable();

        // Act
        byte[] actualBytes = xRefTable.Bytes.ToArray();

        // Assert
        Assert.Equal(expectedBytes, actualBytes);
    }

    [Fact(DisplayName = $"{nameof(XRefTable.Content)} property, accessed multiple times, should return the same reference")]
    public void XRefTable_Content_MultipleAccesses_ShouldReturnSameReference()
    {
        // Arrange
        IXRefSubSection xRefSubSection1 = new XRefEntry(0, 65535, XRefEntryType.Free).ToXRefSubSection(0);
        IXRefSubSection xRefSubSection2 = new XRefEntry(25325, 0, XRefEntryType.InUse).ToXRefSubSection(3);
        IXRefSubSection xRefSubSection3 = new XRefEntry(25777, 0, XRefEntryType.InUse).ToXRefSubSection(30);
        List<IXRefEntry> multipleEntries = new() { new XRefEntry(25518, 2, XRefEntryType.InUse), new XRefEntry(25635, 0, XRefEntryType.InUse) };
        List<IXRefSubSection> subSections = new() { xRefSubSection1, xRefSubSection2, xRefSubSection3, multipleEntries.ToXRefSubSection(23) };

        IXRefTable xRefTable = subSections.ToXRefTable();

        // Act
        string actualContent1 = xRefTable.Content;
        string actualContent2 = xRefTable.Content;

        // Assert
        Assert.True(ReferenceEquals(actualContent1, actualContent2));
    }

    [Fact(DisplayName = "Check if Equals returns false")]
    public void XRefTable_Equals_MustReturnFalse()
    {
        // Arrange
        IXRefSubSection xRefSubSection1 = new XRefEntry(0, 65535, XRefEntryType.Free).ToXRefSubSection(0);
        IXRefSubSection xRefSubSection2 = new XRefEntry(25325, 0, XRefEntryType.InUse).ToXRefSubSection(3);
        IXRefSubSection xRefSubSection3 = new XRefEntry(25777, 0, XRefEntryType.InUse).ToXRefSubSection(30);
        List<IXRefEntry> multipleEntries = new() { new XRefEntry(25518, 2, XRefEntryType.InUse), new XRefEntry(25635, 0, XRefEntryType.InUse) };
        List<IXRefSubSection> subSections1 = new() { xRefSubSection1, xRefSubSection2, xRefSubSection3, multipleEntries.ToXRefSubSection(23) };
        List<IXRefSubSection> subSections2 = new() { xRefSubSection1, xRefSubSection2, xRefSubSection3 };

        IXRefTable xRefTable1 = subSections1.ToXRefTable();
        IXRefTable xRefTable2 = subSections2.ToXRefTable();

        // Act
        bool actualResult = xRefTable1.Equals(xRefTable2);

        // Assert
        Assert.False(actualResult);
    }

    [Fact(DisplayName = "Check if Equals returns true")]
    public void XRefTable_Equals_MustReturnTrue()
    {
        // Arrange
        IXRefSubSection xRefSubSection1 = new XRefEntry(0, 65535, XRefEntryType.Free).ToXRefSubSection(0);
        IXRefSubSection xRefSubSection2 = new XRefEntry(25325, 0, XRefEntryType.InUse).ToXRefSubSection(3);
        IXRefSubSection xRefSubSection3 = new XRefEntry(25777, 0, XRefEntryType.InUse).ToXRefSubSection(30);
        List<IXRefEntry> multipleEntries = new() { new XRefEntry(25518, 2, XRefEntryType.InUse), new XRefEntry(25635, 0, XRefEntryType.InUse) };
        List<IXRefSubSection> subSections1 = new() { xRefSubSection1, xRefSubSection2, xRefSubSection3, multipleEntries.ToXRefSubSection(23) };
        List<IXRefSubSection> subSections2 = new() { xRefSubSection1, xRefSubSection2, xRefSubSection3, multipleEntries.ToXRefSubSection(23) };

        IXRefTable xRefTable1 = subSections1.ToXRefTable();
        IXRefTable xRefTable2 = subSections2.ToXRefTable();

        // Act
        bool actualResult = xRefTable1.Equals(xRefTable2);

        // Assert
        Assert.True(actualResult);
    }

    [Fact(DisplayName = "Check if Equals method with null object returns always false")]
    public void XRefTable_EqualsNullObject_CheckValidity()
    {
        // Arrange
        IXRefSubSection xRefSubSection1 = new XRefEntry(0, 65535, XRefEntryType.Free).ToXRefSubSection(0);
        IXRefSubSection xRefSubSection2 = new XRefEntry(25325, 0, XRefEntryType.InUse).ToXRefSubSection(3);
        IXRefSubSection xRefSubSection3 = new XRefEntry(25777, 0, XRefEntryType.InUse).ToXRefSubSection(30);
        List<IXRefEntry> multipleEntries = new() { new XRefEntry(25518, 2, XRefEntryType.InUse), new XRefEntry(25635, 0, XRefEntryType.InUse) };
        List<IXRefSubSection> subSections = new() { xRefSubSection1, xRefSubSection2, xRefSubSection3, multipleEntries.ToXRefSubSection(23) };

        IXRefTable xRefTable = subSections.ToXRefTable();

        // Act
        bool actualResult1 = xRefTable.Equals(null);

        Debug.Assert(xRefTable != null, nameof(xRefTable) + " != null");
        bool actualResult2 = xRefTable.Equals(null);

        // Assert
        Assert.False(actualResult1);
        Assert.False(actualResult2);
    }
}

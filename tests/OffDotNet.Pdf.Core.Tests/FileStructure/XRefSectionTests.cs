// <copyright file="XRefSectionTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Diagnostics;
using OffDotNet.Pdf.Core.FileStructure;
using OffDotNet.Pdf.Core.Properties;
using Xunit;

namespace OffDotNet.Pdf.Core.Tests.FileStructure;

public class XRefSectionTests
{
    [Fact(DisplayName = $"Constructor with an empty subsections collection must throw an {nameof(ArgumentOutOfRangeException)}")]
    public void XRefSection_NegativeGenerationNumber_ShouldThrowException()
    {
        // Arrange
        ICollection<IXRefSubSection> subSections = new List<IXRefSubSection>(0);

        // Act
        IXRefSection XRefSectionFunction()
        {
            return new XRefSection(subSections);
        }

        // Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(XRefSectionFunction);
        Assert.StartsWith(Resource.XRefSection_MustHaveNonEmptyEntriesCollection, exception.Message);
    }

    [Fact(DisplayName = $"{nameof(XRefSection.Content)} property should return a valid value")]
    public void XRefSection_Content_ShouldReturnValidValue()
    {
        // Arrange
        const string expectedContent = "xref\n0 1\n0000000000 65535 f \n3 1\n0000025325 00000 n \n30 1\n0000025777 00000 n \n23 2\n0000025518 00002 n \n0000025635 00000 n \n";
        IXRefSubSection xRefSubSection1 = new XRefEntry(0, 65535, XRefEntryType.Free).ToXRefSubSection(0);
        IXRefSubSection xRefSubSection2 = new XRefEntry(25325, 0, XRefEntryType.InUse).ToXRefSubSection(3);
        IXRefSubSection xRefSubSection3 = new XRefEntry(25777, 0, XRefEntryType.InUse).ToXRefSubSection(30);
        List<IXRefEntry> multipleEntries = new() { new XRefEntry(25518, 2, XRefEntryType.InUse), new XRefEntry(25635, 0, XRefEntryType.InUse) };

        IXRefSection xRefSection = new XRefSection(new List<IXRefSubSection> { xRefSubSection1, xRefSubSection2, xRefSubSection3, multipleEntries.ToXRefSubSection(23) });

        // Act
        string actualContent = xRefSection.Content;

        // Assert
        Assert.Equal(expectedContent, actualContent);
    }

    [Fact(DisplayName = $"{nameof(XRefSection.Bytes)} property should return a valid value")]
    public void XRefSection_Bytes_ShouldReturnValidValue()
    {
        // Arrange
        byte[] expectedBytes =
        {
            0x78, 0x72, 0x65, 0x66, 0x0A, 0x30, 0x20, 0x31, 0x0A, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x20, 0x36, 0x35, 0x35, 0x33, 0x35, 0x20, 0x66, 0x20, 0x0A,
            0x33, 0x20, 0x31, 0x0A, 0x30, 0x30, 0x30, 0x30, 0x30, 0x32, 0x35, 0x33, 0x32, 0x35, 0x20, 0x30, 0x30, 0x30, 0x30, 0x30, 0x20, 0x6E, 0x20, 0x0A, 0x33, 0x30, 0x20, 0x31, 0x0A,
            0x30, 0x30, 0x30, 0x30, 0x30, 0x32, 0x35, 0x37, 0x37, 0x37, 0x20, 0x30, 0x30, 0x30, 0x30, 0x30, 0x20, 0x6E, 0x20, 0x0A, 0x32, 0x33, 0x20, 0x32, 0x0A, 0x30, 0x30, 0x30, 0x30,
            0x30, 0x32, 0x35, 0x35, 0x31, 0x38, 0x20, 0x30, 0x30, 0x30, 0x30, 0x32, 0x20, 0x6E, 0x20, 0x0A, 0x30, 0x30, 0x30, 0x30, 0x30, 0x32, 0x35, 0x36, 0x33, 0x35, 0x20, 0x30, 0x30,
            0x30, 0x30, 0x30, 0x20, 0x6E, 0x20, 0x0A,
        };

        IXRefSubSection xRefSubSection1 = new XRefEntry(0, 65535, XRefEntryType.Free).ToXRefSubSection(0);
        IXRefSubSection xRefSubSection2 = new XRefEntry(25325, 0, XRefEntryType.InUse).ToXRefSubSection(3);
        IXRefSubSection xRefSubSection3 = new XRefEntry(25777, 0, XRefEntryType.InUse).ToXRefSubSection(30);
        List<IXRefEntry> multipleEntries = new() { new XRefEntry(25518, 2, XRefEntryType.InUse), new XRefEntry(25635, 0, XRefEntryType.InUse) };

        IXRefSection xRefSection = new XRefSection(new List<IXRefSubSection> { xRefSubSection1, xRefSubSection2, xRefSubSection3, multipleEntries.ToXRefSubSection(23) });

        // Act
        byte[] actualBytes = xRefSection.Bytes.ToArray();

        // Assert
        Assert.Equal(expectedBytes, actualBytes);
    }

    [Fact(DisplayName = $"{nameof(XRefSection.Content)} property, accessed multiple times, should return the same reference")]
    public void XRefSection_Content_MultipleAccesses_ShouldReturnSameReference()
    {
        // Arrange
        IXRefSubSection xRefSubSection1 = new XRefEntry(0, 65535, XRefEntryType.Free).ToXRefSubSection(0);
        IXRefSubSection xRefSubSection2 = new XRefEntry(25325, 0, XRefEntryType.InUse).ToXRefSubSection(3);
        IXRefSubSection xRefSubSection3 = new XRefEntry(25777, 0, XRefEntryType.InUse).ToXRefSubSection(30);
        List<IXRefEntry> multipleEntries = new() { new XRefEntry(25518, 2, XRefEntryType.InUse), new XRefEntry(25635, 0, XRefEntryType.InUse) };

        IXRefSection xRefSection = new XRefSection(new List<IXRefSubSection> { xRefSubSection1, xRefSubSection2, xRefSubSection3, multipleEntries.ToXRefSubSection(23) });

        // Act
        string actualContent1 = xRefSection.Content;
        string actualContent2 = xRefSection.Content;

        // Assert
        Assert.True(ReferenceEquals(actualContent1, actualContent2));
    }

    [Fact(DisplayName = "Check if Equals method returns true")]
    public void XRefSection_Equals_MustReturnTrue()
    {
        // Arrange
        IXRefSubSection xRefSubSection1 = new XRefEntry(0, 65535, XRefEntryType.Free).ToXRefSubSection(0);
        IXRefSubSection xRefSubSection2 = new XRefEntry(25325, 0, XRefEntryType.InUse).ToXRefSubSection(3);
        IXRefSubSection xRefSubSection3 = new XRefEntry(25777, 0, XRefEntryType.InUse).ToXRefSubSection(30);
        List<IXRefEntry> multipleEntries = new() { new XRefEntry(25518, 2, XRefEntryType.InUse), new XRefEntry(25635, 0, XRefEntryType.InUse) };

        IXRefSection xRefSection1 = new XRefSection(new List<IXRefSubSection> { xRefSubSection1, xRefSubSection2, xRefSubSection3, multipleEntries.ToXRefSubSection(23) });
        IXRefSection xRefSection2 = new XRefSection(new List<IXRefSubSection> { xRefSubSection1, xRefSubSection2, xRefSubSection3, multipleEntries.ToXRefSubSection(23) });

        // Act
        bool actualResult = xRefSection1.Equals(xRefSection2);

        // Assert
        Assert.True(actualResult);
    }

    [Fact(DisplayName = "Check if Equals method returns false")]
    public void XRefSection_Equals_MustReturnFalse()
    {
        // Arrange
        IXRefSubSection xRefSubSection1 = new XRefEntry(0, 65535, XRefEntryType.Free).ToXRefSubSection(0);
        IXRefSubSection xRefSubSection2 = new XRefEntry(25325, 0, XRefEntryType.InUse).ToXRefSubSection(3);
        IXRefSubSection xRefSubSection3 = new XRefEntry(25777, 0, XRefEntryType.InUse).ToXRefSubSection(30);
        List<IXRefEntry> multipleEntries = new() { new XRefEntry(25518, 2, XRefEntryType.InUse), new XRefEntry(25635, 0, XRefEntryType.InUse) };

        IXRefSection xRefSection1 = new XRefSection(new List<IXRefSubSection> { xRefSubSection1, xRefSubSection2, xRefSubSection3, multipleEntries.ToXRefSubSection(23) });
        IXRefSection xRefSection2 = new XRefSection(new List<IXRefSubSection> { xRefSubSection1, xRefSubSection2, xRefSubSection3 });

        // Act
        bool actualResult = xRefSection1.Equals(xRefSection2);

        // Assert
        Assert.False(actualResult);
    }

    [Fact(DisplayName = "Check if Equals method with null object returns always false")]
    public void XRefSection_EqualsNullObject_CheckValidity()
    {
        // Arrange
        IXRefSubSection xRefSubSection1 = new XRefEntry(0, 65535, XRefEntryType.Free).ToXRefSubSection(0);
        IXRefSubSection xRefSubSection2 = new XRefEntry(25325, 0, XRefEntryType.InUse).ToXRefSubSection(3);
        IXRefSubSection xRefSubSection3 = new XRefEntry(25777, 0, XRefEntryType.InUse).ToXRefSubSection(30);
        List<IXRefEntry> multipleEntries = new() { new XRefEntry(25518, 2, XRefEntryType.InUse), new XRefEntry(25635, 0, XRefEntryType.InUse) };

        IXRefSection xRefSection = new XRefSection(new List<IXRefSubSection> { xRefSubSection1, xRefSubSection2, xRefSubSection3, multipleEntries.ToXRefSubSection(23) });

        // Act
        bool actualResult1 = xRefSection.Equals(null);

        Debug.Assert(xRefSection != null, nameof(xRefSection) + " != null");
        bool actualResult2 = xRefSection.Equals(null);

        // Assert
        Assert.False(actualResult1);
        Assert.False(actualResult2);
    }
}

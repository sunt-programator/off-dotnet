// <copyright file="XRefTableTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Diagnostics;
using Off.Net.Pdf.Core.FileStructure;
using Xunit;

namespace Off.Net.Pdf.Core.Tests.FileStructure;

public class XRefTableTests
{
    [Fact(DisplayName = $"Constructor with negative {nameof(XRefTable.NumberOfSections)} should throw an {nameof(ArgumentOutOfRangeException)}")]
    public void XRefTable_NegativeGenerationNumber_ShouldThrowException()
    {
        // Arrange
        ICollection<XRefSection> sections = new List<XRefSection>(0);

        // Act
        XRefTable XRefTableFunction()
        {
            return new(sections);
        }

        // Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(XRefTableFunction);
        Assert.StartsWith(Resource.XRefTable_MustHaveNonEmptyEntriesCollection, exception.Message);
    }

    [Theory(DisplayName = $"{nameof(XRefTable.Content)} property should return a valid value")]
    [MemberData(nameof(XRefTableTestsDataGenerator.XRefTable_Content_TestCases), MemberType = typeof(XRefTableTestsDataGenerator))]
    public void XRefTable_Content_ShouldReturnValidValue(XRefTable xRefTable, string expectedContent)
    {
        // Arrange

        // Act
        string actualContent = xRefTable.Content;

        // Assert
        Assert.Equal(expectedContent, actualContent);
    }

    [Theory(DisplayName = $"{nameof(XRefTable.Length)} property should return a valid value")]
    [MemberData(nameof(XRefTableTestsDataGenerator.XRefTable_Length_TestCases), MemberType = typeof(XRefTableTestsDataGenerator))]
    public void XRefTable_Length_ShouldReturnValidValue(XRefTable xRefTable, int expectedLength)
    {
        // Arrange

        // Act
        int actualLength = xRefTable.Length;

        // Assert
        Assert.Equal(expectedLength, actualLength);
    }

    [Theory(DisplayName = $"{nameof(XRefTable.NumberOfSections)} property should return a valid value")]
    [MemberData(nameof(XRefTableTestsDataGenerator.XRefTable_NumberOfSections_TestCases), MemberType = typeof(XRefTableTestsDataGenerator))]
    public void XRefTable_NumberOfEntries_ShouldReturnValidValue(XRefTable xRefTable, int expectedNumberOfEntries)
    {
        // Arrange

        // Act
        long actualNumberOfEntries = xRefTable.NumberOfSections;

        // Assert
        Assert.Equal(expectedNumberOfEntries, actualNumberOfEntries);
    }

    [Theory(DisplayName = $"{nameof(XRefTable.Bytes)} property should return a valid value")]
    [MemberData(nameof(XRefTableTestsDataGenerator.XRefTable_Bytes_TestCases), MemberType = typeof(XRefTableTestsDataGenerator))]
    public void XRefTable_Bytes_ShouldReturnValidValue(XRefTable xRefTable, byte[] expectedBytes)
    {
        // Arrange

        // Act
        byte[] actualBytes = xRefTable.Bytes.ToArray();

        // Assert
        Assert.Equal(expectedBytes, actualBytes);
    }

    [Theory(DisplayName = "Check if GetHashCode method returns valid value")]
    [MemberData(nameof(XRefTableTestsDataGenerator.XRefTable_NoExpectedData_TestCases), MemberType = typeof(XRefTableTestsDataGenerator))]
    public void XRefTable_GetHashCode_CheckValidity(XRefTable xRefTable)
    {
        // Arrange
        int expectedHashCode = HashCode.Combine(nameof(XRefTable), xRefTable.Value);

        // Act
        int actualHashCode = xRefTable.GetHashCode();

        // Assert
        Assert.Equal(expectedHashCode, actualHashCode);
    }

    [Theory(DisplayName = $"{nameof(XRefTable.Content)} property, accessed multiple times, should return the same reference")]
    [MemberData(nameof(XRefTableTestsDataGenerator.XRefTable_NoExpectedData_TestCases), MemberType = typeof(XRefTableTestsDataGenerator))]
    public void XRefTable_Content_MultipleAccesses_ShouldReturnSameReference(XRefTable xRefTable)
    {
        // Arrange

        // Act
        string actualContent1 = xRefTable.Content;
        string actualContent2 = xRefTable.Content;

        // Assert
        Assert.True(ReferenceEquals(actualContent1, actualContent2));
    }

    [Theory(DisplayName = "Check if Equals returns a valid result")]
    [MemberData(nameof(XRefTableTestsDataGenerator.XRefTable_Equals_TestCases), MemberType = typeof(XRefTableTestsDataGenerator))]
    public void XRefTable_Equals_CheckValidity(XRefTable xRefTable1, XRefTable xRefTable2, bool expectedValue)
    {
        // Arrange

        // Act
        bool actualResult = xRefTable1.Equals(xRefTable2);

        // Assert
        Assert.Equal(expectedValue, actualResult);
    }

    [Theory(DisplayName = "Check if Equals method with null object returns always false")]
    [MemberData(nameof(XRefTableTestsDataGenerator.XRefTable_NoExpectedData_TestCases), MemberType = typeof(XRefTableTestsDataGenerator))]
    public void XRefTable_EqualsNullObject_CheckValidity(XRefTable xRefTable)
    {
        // Arrange

        // Act
        bool actualResult1 = xRefTable.Equals(null);

        Debug.Assert(xRefTable != null, nameof(xRefTable) + " != null");
        bool actualResult2 = xRefTable.Equals((object?)null);

        // Assert
        Assert.False(actualResult1);
        Assert.False(actualResult2);
    }
}

[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "TestData generator class can be in the same file")]
internal static class XRefTableTestsDataGenerator
{
    private static readonly XRefEntry EntryObj0Gen65535F = new(0, 65535, XRefEntryType.Free);
    private static readonly XRefEntry EntryObj25325Gen0N = new(25325, 0, XRefEntryType.InUse);
    private static readonly XRefEntry EntryObj257775Gen0N = new(25777, 0, XRefEntryType.InUse);
    private static readonly List<XRefEntry> MultipleEntries = new() { new(25518, 2, XRefEntryType.InUse), new(25635, 0, XRefEntryType.InUse) };
    private static readonly List<XRefSubSection> MultipleSubSections = new()
    {
        EntryObj0Gen65535F.ToXRefSubSection(0), EntryObj25325Gen0N.ToXRefSubSection(3), EntryObj257775Gen0N.ToXRefSubSection(30), MultipleEntries.ToXRefSubSection(23),
    };

    private static readonly List<XRefSection> MultipleSections = new() { MultipleEntries.ToXRefSection(23), MultipleEntries.ToXRefSection(41) };

    public static IEnumerable<object[]> XRefTable_Content_TestCases()
    {
        yield return new object[] { EntryObj0Gen65535F.ToXRefSubSection(0).ToXRefSection().ToXRefTable(), "xref\n0 1\n0000000000 65535 f \n" };
        yield return new object[] { EntryObj25325Gen0N.ToXRefSubSection(3).ToXRefTable(), "xref\n3 1\n0000025325 00000 n \n" };
        yield return new object[] { EntryObj257775Gen0N.ToXRefTable(30), "xref\n30 1\n0000025777 00000 n \n" };
        yield return new object[] { MultipleEntries.ToXRefTable(23), "xref\n23 2\n0000025518 00002 n \n0000025635 00000 n \n" };
        yield return new object[] { MultipleSubSections.ToXRefTable(), "xref\n0 1\n0000000000 65535 f \n3 1\n0000025325 00000 n \n30 1\n0000025777 00000 n \n23 2\n0000025518 00002 n \n0000025635 00000 n \n" };
        yield return new object[] { MultipleSections.ToXRefTable(), "xref\n23 2\n0000025518 00002 n \n0000025635 00000 n \nxref\n41 2\n0000025518 00002 n \n0000025635 00000 n \n" };
    }

    public static IEnumerable<object[]> XRefTable_Length_TestCases()
    {
        yield return new object[] { EntryObj0Gen65535F.ToXRefSubSection(0).ToXRefSection().ToXRefTable(), 29 };
        yield return new object[] { EntryObj25325Gen0N.ToXRefSubSection(3).ToXRefTable(), 29 };
        yield return new object[] { EntryObj257775Gen0N.ToXRefTable(30), 30 };
        yield return new object[] { MultipleEntries.ToXRefTable(23), 50 };
        yield return new object[] { MultipleSubSections.ToXRefTable(), 123 };
        yield return new object[] { MultipleSections.ToXRefTable(), 100 };
    }

    public static IEnumerable<object[]> XRefTable_NumberOfSections_TestCases()
    {
        yield return new object[] { EntryObj0Gen65535F.ToXRefSubSection(0).ToXRefSection().ToXRefTable(), 1 };
        yield return new object[] { EntryObj25325Gen0N.ToXRefSubSection(3).ToXRefTable(), 1 };
        yield return new object[] { EntryObj257775Gen0N.ToXRefTable(30), 1 };
        yield return new object[] { MultipleEntries.ToXRefTable(23), 1 };
        yield return new object[] { MultipleSubSections.ToXRefTable(), 1 };
        yield return new object[] { MultipleSections.ToXRefTable(), 2 };
    }

    public static IEnumerable<object[]> XRefTable_NoExpectedData_TestCases()
    {
        yield return new object[] { EntryObj0Gen65535F.ToXRefSubSection(0).ToXRefSection().ToXRefTable() };
        yield return new object[] { EntryObj25325Gen0N.ToXRefSubSection(3).ToXRefTable() };
        yield return new object[] { EntryObj257775Gen0N.ToXRefTable(30) };
        yield return new object[] { MultipleEntries.ToXRefTable(23) };
        yield return new object[] { MultipleSubSections.ToXRefTable() };
        yield return new object[] { MultipleSections.ToXRefTable() };
    }

    public static IEnumerable<object[]> XRefTable_Equals_TestCases()
    {
        yield return new object[]
        {
            EntryObj0Gen65535F.ToXRefTable(0),
            EntryObj0Gen65535F.ToXRefTable(0),
            true,
        };
        yield return new object[]
        {
            EntryObj0Gen65535F.ToXRefTable(0),
            EntryObj0Gen65535F.ToXRefTable(560),
            false,
        };
        yield return new object[]
        {
            EntryObj0Gen65535F.ToXRefTable(0),
            EntryObj25325Gen0N.ToXRefTable(0),
            false,
        };
        yield return new object[]
        {
            MultipleEntries.ToXRefTable(0),
            MultipleEntries.ToXRefTable(0),
            true,
        };
        yield return new object[]
        {
            MultipleEntries.ToXRefTable(0),
            MultipleEntries.ToXRefTable(560),
            false,
        };
        yield return new object[]
        {
            MultipleSections.ToXRefTable(),
            MultipleSections.ToXRefTable(),
            true,
        };
    }

    public static IEnumerable<object[]> XRefTable_Bytes_TestCases()
    {
        yield return new object[]
        {
            EntryObj0Gen65535F.ToXRefSubSection(0).ToXRefSection().ToXRefTable(),
            new byte[]
            {
                0x78, 0x72, 0x65, 0x66, 0x0A, 0x30, 0x20, 0x31, 0x0A, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x20, 0x36, 0x35, 0x35, 0x33, 0x35, 0x20, 0x66, 0x20, 0x0A,
            },
        };
        yield return new object[]
        {
            EntryObj25325Gen0N.ToXRefSubSection(3).ToXRefTable(),
            new byte[]
            {
                0x78, 0x72, 0x65, 0x66, 0x0A, 0x33, 0x20, 0x31, 0x0A, 0x30, 0x30, 0x30, 0x30, 0x30, 0x32, 0x35, 0x33, 0x32, 0x35, 0x20, 0x30, 0x30, 0x30, 0x30, 0x30, 0x20, 0x6E, 0x20, 0x0A,
            },
        };
        yield return new object[]
        {
            EntryObj257775Gen0N.ToXRefTable(30),
            new byte[]
            {
                0x78, 0x72, 0x65, 0x66, 0x0A, 0x33, 0x30, 0x20, 0x31, 0x0A, 0x30, 0x30, 0x30, 0x30, 0x30, 0x32, 0x35, 0x37, 0x37, 0x37, 0x20, 0x30, 0x30, 0x30, 0x30, 0x30, 0x20, 0x6E, 0x20,
                0x0A,
            },
        };
        yield return new object[]
        {
            MultipleEntries.ToXRefTable(23),
            new byte[]
            {
                0x78, 0x72, 0x65, 0x66, 0x0A, 0x32, 0x33, 0x20, 0x32, 0x0A, 0x30, 0x30, 0x30, 0x30, 0x30, 0x32, 0x35, 0x35, 0x31, 0x38, 0x20, 0x30, 0x30, 0x30, 0x30, 0x32, 0x20, 0x6E, 0x20,
                0x0A, 0x30, 0x30, 0x30, 0x30, 0x30, 0x32, 0x35, 0x36, 0x33, 0x35, 0x20, 0x30, 0x30, 0x30, 0x30, 0x30, 0x20, 0x6E, 0x20, 0x0A,
            },
        };
        yield return new object[]
        {
            MultipleSections.ToXRefTable(),
            new byte[]
            {
                0x78, 0x72, 0x65, 0x66, 0x0A, 0x32, 0x33, 0x20, 0x32, 0x0A, 0x30, 0x30, 0x30, 0x30, 0x30, 0x32, 0x35, 0x35, 0x31, 0x38, 0x20, 0x30, 0x30, 0x30, 0x30, 0x32, 0x20, 0x6E, 0x20,
                0x0A, 0x30, 0x30, 0x30, 0x30, 0x30, 0x32, 0x35, 0x36, 0x33, 0x35, 0x20, 0x30, 0x30, 0x30, 0x30, 0x30, 0x20, 0x6E, 0x20, 0x0A, 0x78, 0x72, 0x65, 0x66, 0x0A, 0x34, 0x31, 0x20,
                0x32, 0x0A, 0x30, 0x30, 0x30, 0x30, 0x30, 0x32, 0x35, 0x35, 0x31, 0x38, 0x20, 0x30, 0x30, 0x30, 0x30, 0x32, 0x20, 0x6E, 0x20, 0x0A, 0x30, 0x30, 0x30, 0x30, 0x30, 0x32, 0x35,
                0x36, 0x33, 0x35, 0x20, 0x30, 0x30, 0x30, 0x30, 0x30, 0x20, 0x6E, 0x20, 0x0A,
            },
        };
    }
}

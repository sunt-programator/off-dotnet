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
    [Fact(DisplayName = $"Constructor with negative {nameof(XRefSection.NumberOfSubSections)} should throw an {nameof(ArgumentOutOfRangeException)}")]
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

    [Theory(DisplayName = $"{nameof(XRefSection.Content)} property should return a valid value")]
    [MemberData(nameof(XRefSectionTestsDataGenerator.XRefSection_Content_TestCases), MemberType = typeof(XRefSectionTestsDataGenerator))]
    public void XRefSection_Content_ShouldReturnValidValue(IXRefSection xRefSection, string expectedContent)
    {
        // Arrange

        // Act
        string actualContent = xRefSection.Content;

        // Assert
        Assert.Equal(expectedContent, actualContent);
    }

    [Theory(DisplayName = $"{nameof(XRefSection.NumberOfSubSections)} property should return a valid value")]
    [MemberData(nameof(XRefSectionTestsDataGenerator.XRefSection_NumberOfSubSections_TestCases), MemberType = typeof(XRefSectionTestsDataGenerator))]
    public void XRefSection_NumberOfEntries_ShouldReturnValidValue(IXRefSection xRefSection, int expectedNumberOfEntries)
    {
        // Arrange

        // Act
        long actualNumberOfEntries = xRefSection.NumberOfSubSections;

        // Assert
        Assert.Equal(expectedNumberOfEntries, actualNumberOfEntries);
    }

    [Theory(DisplayName = $"{nameof(XRefSection.Bytes)} property should return a valid value")]
    [MemberData(nameof(XRefSectionTestsDataGenerator.XRefSection_Bytes_TestCases), MemberType = typeof(XRefSectionTestsDataGenerator))]
    public void XRefSection_Bytes_ShouldReturnValidValue(IXRefSection xRefSection, byte[] expectedBytes)
    {
        // Arrange

        // Act
        byte[] actualBytes = xRefSection.Bytes.ToArray();

        // Assert
        Assert.Equal(expectedBytes, actualBytes);
    }

    [Theory(DisplayName = $"{nameof(XRefSection.Content)} property, accessed multiple times, should return the same reference")]
    [MemberData(nameof(XRefSectionTestsDataGenerator.XRefSection_NoExpectedData_TestCases), MemberType = typeof(XRefSectionTestsDataGenerator))]
    public void XRefSection_Content_MultipleAccesses_ShouldReturnSameReference(IXRefSection xRefSection)
    {
        // Arrange

        // Act
        string actualContent1 = xRefSection.Content;
        string actualContent2 = xRefSection.Content;

        // Assert
        Assert.True(ReferenceEquals(actualContent1, actualContent2));
    }

    [Theory(DisplayName = "Check if Equals returns a valid result")]
    [MemberData(nameof(XRefSectionTestsDataGenerator.XRefSection_Equals_TestCases), MemberType = typeof(XRefSectionTestsDataGenerator))]
    public void XRefSection_Equals_CheckValidity(IXRefSection xRefSection1, IXRefSection xRefSection2, bool expectedValue)
    {
        // Arrange

        // Act
        bool actualResult = xRefSection1.Equals(xRefSection2);

        // Assert
        Assert.Equal(expectedValue, actualResult);
    }

    [Theory(DisplayName = "Check if Equals method with null object returns always false")]
    [MemberData(nameof(XRefSectionTestsDataGenerator.XRefSection_NoExpectedData_TestCases), MemberType = typeof(XRefSectionTestsDataGenerator))]
    public void XRefSection_EqualsNullObject_CheckValidity(IXRefSection xRefSection)
    {
        // Arrange

        // Act
        bool actualResult1 = xRefSection.Equals(null);

        Debug.Assert(xRefSection != null, nameof(xRefSection) + " != null");
        bool actualResult2 = xRefSection.Equals((object?)null);

        // Assert
        Assert.False(actualResult1);
        Assert.False(actualResult2);
    }
}

[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "TestData generator class can be in the same file")]
internal static class XRefSectionTestsDataGenerator
{
    private static readonly IXRefEntry EntryObj0Gen65535F = new XRefEntry(0, 65535, XRefEntryType.Free);
    private static readonly IXRefEntry EntryObj25325Gen0N = new XRefEntry(25325, 0, XRefEntryType.InUse);
    private static readonly IXRefEntry EntryObj257775Gen0N = new XRefEntry(25777, 0, XRefEntryType.InUse);
    private static readonly List<IXRefEntry> MultipleEntries = new() { new XRefEntry(25518, 2, XRefEntryType.InUse), new XRefEntry(25635, 0, XRefEntryType.InUse) };

    private static readonly List<IXRefSubSection> MultipleSubSections = new()
    {
        EntryObj0Gen65535F.ToXRefSubSection(0), EntryObj25325Gen0N.ToXRefSubSection(3), EntryObj257775Gen0N.ToXRefSubSection(30), MultipleEntries.ToXRefSubSection(23),
    };

    public static IEnumerable<object[]> XRefSection_Content_TestCases()
    {
        yield return new object[] { EntryObj0Gen65535F.ToXRefSection(0), "xref\n0 1\n0000000000 65535 f \n" };
        yield return new object[] { EntryObj25325Gen0N.ToXRefSection(3), "xref\n3 1\n0000025325 00000 n \n" };
        yield return new object[] { EntryObj257775Gen0N.ToXRefSection(30), "xref\n30 1\n0000025777 00000 n \n" };
        yield return new object[] { MultipleEntries.ToXRefSection(23), "xref\n23 2\n0000025518 00002 n \n0000025635 00000 n \n" };
        yield return new object[]
        {
            MultipleSubSections.ToXRefSection(), "xref\n0 1\n0000000000 65535 f \n3 1\n0000025325 00000 n \n30 1\n0000025777 00000 n \n23 2\n0000025518 00002 n \n0000025635 00000 n \n",
        };
    }

    public static IEnumerable<object[]> XRefSection_NumberOfSubSections_TestCases()
    {
        yield return new object[] { EntryObj0Gen65535F.ToXRefSection(0), 1 };
        yield return new object[] { EntryObj25325Gen0N.ToXRefSection(3), 1 };
        yield return new object[] { EntryObj257775Gen0N.ToXRefSection(30), 1 };
        yield return new object[] { MultipleEntries.ToXRefSection(23), 1 };
        yield return new object[] { MultipleSubSections.ToXRefSection(), 4 };
    }

    public static IEnumerable<object[]> XRefSection_NoExpectedData_TestCases()
    {
        yield return new object[] { EntryObj0Gen65535F.ToXRefSection(0) };
        yield return new object[] { EntryObj25325Gen0N.ToXRefSection(3) };
        yield return new object[] { EntryObj257775Gen0N.ToXRefSection(30) };
        yield return new object[] { MultipleEntries.ToXRefSection(23) };
        yield return new object[] { MultipleSubSections.ToXRefSection() };
    }

    public static IEnumerable<object[]> XRefSection_Equals_TestCases()
    {
        yield return new object[] { EntryObj0Gen65535F.ToXRefSection(0), EntryObj0Gen65535F.ToXRefSection(0), true };
        yield return new object[] { EntryObj0Gen65535F.ToXRefSection(0), EntryObj0Gen65535F.ToXRefSection(326), false };
        yield return new object[] { EntryObj0Gen65535F.ToXRefSection(326), EntryObj25325Gen0N.ToXRefSection(326), false };
        yield return new object[] { EntryObj25325Gen0N.ToXRefSection(326), EntryObj25325Gen0N.ToXRefSection(326), true };
        yield return new object[] { MultipleEntries.ToXRefSection(326), MultipleEntries.ToXRefSection(326), true };
        yield return new object[] { MultipleEntries.ToXRefSection(412), MultipleEntries.ToXRefSection(326), false };
    }

    public static IEnumerable<object[]> XRefSection_Bytes_TestCases()
    {
        yield return new object[]
        {
            EntryObj0Gen65535F.ToXRefSection(0),
            new byte[]
            {
                0x78, 0x72, 0x65, 0x66, 0x0A, 0x30, 0x20, 0x31, 0x0A, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x20, 0x36, 0x35, 0x35, 0x33, 0x35, 0x20, 0x66, 0x20, 0x0A,
            },
        };
        yield return new object[]
        {
            EntryObj25325Gen0N.ToXRefSection(3),
            new byte[]
            {
                0x78, 0x72, 0x65, 0x66, 0x0A, 0x33, 0x20, 0x31, 0x0A, 0x30, 0x30, 0x30, 0x30, 0x30, 0x32, 0x35, 0x33, 0x32, 0x35, 0x20, 0x30, 0x30, 0x30, 0x30, 0x30, 0x20, 0x6E, 0x20, 0x0A,
            },
        };
        yield return new object[]
        {
            EntryObj257775Gen0N.ToXRefSection(30),
            new byte[]
            {
                0x78, 0x72, 0x65, 0x66, 0x0A, 0x33, 0x30, 0x20, 0x31, 0x0A, 0x30, 0x30, 0x30, 0x30, 0x30, 0x32, 0x35, 0x37, 0x37, 0x37, 0x20, 0x30, 0x30, 0x30, 0x30, 0x30, 0x20, 0x6E, 0x20,
                0x0A,
            },
        };
        yield return new object[]
        {
            MultipleEntries.ToXRefSection(23),
            new byte[]
            {
                0x78, 0x72, 0x65, 0x66, 0x0A, 0x32, 0x33, 0x20, 0x32, 0x0A, 0x30, 0x30, 0x30, 0x30, 0x30, 0x32, 0x35, 0x35, 0x31, 0x38, 0x20, 0x30, 0x30, 0x30, 0x30, 0x32, 0x20, 0x6E, 0x20,
                0x0A, 0x30, 0x30, 0x30, 0x30, 0x30, 0x32, 0x35, 0x36, 0x33, 0x35, 0x20, 0x30, 0x30, 0x30, 0x30, 0x30, 0x20, 0x6E, 0x20, 0x0A,
            },
        };
        yield return new object[]
        {
            MultipleSubSections.ToXRefSection(),
            new byte[]
            {
                0x78, 0x72, 0x65, 0x66, 0x0A, 0x30, 0x20, 0x31, 0x0A, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x20, 0x36, 0x35, 0x35, 0x33, 0x35, 0x20, 0x66, 0x20, 0x0A,
                0x33, 0x20, 0x31, 0x0A, 0x30, 0x30, 0x30, 0x30, 0x30, 0x32, 0x35, 0x33, 0x32, 0x35, 0x20, 0x30, 0x30, 0x30, 0x30, 0x30, 0x20, 0x6E, 0x20, 0x0A, 0x33, 0x30, 0x20, 0x31, 0x0A,
                0x30, 0x30, 0x30, 0x30, 0x30, 0x32, 0x35, 0x37, 0x37, 0x37, 0x20, 0x30, 0x30, 0x30, 0x30, 0x30, 0x20, 0x6E, 0x20, 0x0A, 0x32, 0x33, 0x20, 0x32, 0x0A, 0x30, 0x30, 0x30, 0x30,
                0x30, 0x32, 0x35, 0x35, 0x31, 0x38, 0x20, 0x30, 0x30, 0x30, 0x30, 0x32, 0x20, 0x6E, 0x20, 0x0A, 0x30, 0x30, 0x30, 0x30, 0x30, 0x32, 0x35, 0x36, 0x33, 0x35, 0x20, 0x30, 0x30,
                0x30, 0x30, 0x30, 0x20, 0x6E, 0x20, 0x0A,
            },
        };
    }
}

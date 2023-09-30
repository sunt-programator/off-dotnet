// <copyright file="XRefSubSectionTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Diagnostics;
using OffDotNet.Pdf.Core.FileStructure;
using OffDotNet.Pdf.Core.Properties;
using Xunit;

namespace OffDotNet.Pdf.Core.Tests.FileStructure;

public class XRefSubSectionTests
{
    [Theory(DisplayName = $"Constructor with negative {nameof(XRefSubSection.ObjectNumber)} should throw an {nameof(ArgumentOutOfRangeException)}")]
    [InlineData(-1)]
    [InlineData(-15)]
    [InlineData(-23)]
    public void XRefSubSection_NegativeByteOffset_ShouldThrowException(int objectNumber)
    {
        // Arrange
        ICollection<XRefEntry> entries = new List<XRefEntry>(1) { new(0, 0, XRefEntryType.Free) };

        // Act
        XRefSubSection XRefSubSectionFunction()
        {
            return new XRefSubSection(objectNumber, entries);
        }

        // Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(XRefSubSectionFunction);
        Assert.StartsWith(Resource.PdfIndirect_ObjectNumberMustBePositive, exception.Message);
    }

    [Fact(DisplayName = $"Constructor with negative {nameof(XRefSubSection.NumberOfEntries)} should throw an {nameof(ArgumentOutOfRangeException)}")]
    public void XRefSubSection_NegativeGenerationNumber_ShouldThrowException()
    {
        // Arrange
        ICollection<XRefEntry> entries = new List<XRefEntry>(0);

        // Act
        XRefSubSection XRefSubSectionFunction()
        {
            return new XRefSubSection(0, entries);
        }

        // Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(XRefSubSectionFunction);
        Assert.StartsWith(Resource.XRefSubSection_MustHaveNonEmptyEntriesCollection, exception.Message);
    }

    [Theory(DisplayName = $"{nameof(XRefSubSection.Content)} property should return a valid value")]
    [MemberData(nameof(XRefSubSectionTestsDataGenerator.XRefSubSection_Content_TestCases), MemberType = typeof(XRefSubSectionTestsDataGenerator))]
    public void XRefSubSection_Content_ShouldReturnValidValue(int objectNumber, List<XRefEntry> entries, string expectedContent)
    {
        // Arrange
        XRefSubSection xRefEntry = new(objectNumber, entries);

        // Act
        string actualContent = xRefEntry.Content;

        // Assert
        Assert.Equal(expectedContent, actualContent);
    }

    [Theory(DisplayName = $"{nameof(XRefSubSection.ObjectNumber)} property should return a valid value")]
    [MemberData(nameof(XRefSubSectionTestsDataGenerator.XRefSubSection_ObjectNumber_TestCases), MemberType = typeof(XRefSubSectionTestsDataGenerator))]
    public void XRefSubSection_ObjectNumber_ShouldReturnValidValue(int objectNumber, List<XRefEntry> entries, int expectedObjectNumber)
    {
        // Arrange
        XRefSubSection xRefEntry = new(objectNumber, entries);

        // Act
        long actualObjectNumber = xRefEntry.ObjectNumber;

        // Assert
        Assert.Equal(expectedObjectNumber, actualObjectNumber);
    }

    [Theory(DisplayName = $"{nameof(XRefSubSection.NumberOfEntries)} property should return a valid value")]
    [MemberData(nameof(XRefSubSectionTestsDataGenerator.XRefSubSection_NumberOfEntries_TestCases), MemberType = typeof(XRefSubSectionTestsDataGenerator))]
    public void XRefSubSection_NumberOfEntries_ShouldReturnValidValue(int objectNumber, List<XRefEntry> entries, int expectedNumberOfEntries)
    {
        // Arrange
        XRefSubSection xRefEntry = new(objectNumber, entries);

        // Act
        long actualNumberOfEntries = xRefEntry.NumberOfEntries;

        // Assert
        Assert.Equal(expectedNumberOfEntries, actualNumberOfEntries);
    }

    [Theory(DisplayName = $"{nameof(XRefSubSection.Bytes)} property should return a valid value")]
    [MemberData(nameof(XRefSubSectionTestsDataGenerator.XRefSubSection_Bytes_TestCases), MemberType = typeof(XRefSubSectionTestsDataGenerator))]
    public void XRefSubSection_Bytes_ShouldReturnValidValue(int objectNumber, List<XRefEntry> entries, byte[] expectedBytes)
    {
        // Arrange
        XRefSubSection xRefEntry = new(objectNumber, entries);

        // Act
        byte[] actualBytes = xRefEntry.Bytes.ToArray();

        // Assert
        Assert.Equal(expectedBytes, actualBytes);
    }

    [Theory(DisplayName = $"{nameof(XRefSubSection.Content)} property, accessed multiple times, should return the same reference")]
    [MemberData(nameof(XRefSubSectionTestsDataGenerator.XRefSubSection_NoExpectedData_TestCases), MemberType = typeof(XRefSubSectionTestsDataGenerator))]
    public void XRefSubSection_Content_MultipleAccesses_ShouldReturnSameReference(int objectNumber, List<XRefEntry> entries)
    {
        // Arrange
        XRefSubSection xRefEntry = new(objectNumber, entries);

        // Act
        string actualContent1 = xRefEntry.Content;
        string actualContent2 = xRefEntry.Content;

        // Assert
        Assert.True(ReferenceEquals(actualContent1, actualContent2));
    }

    [Theory(DisplayName = "Check if Equals returns a valid result")]
    [MemberData(nameof(XRefSubSectionTestsDataGenerator.XRefSubSection_Equals_TestCases), MemberType = typeof(XRefSubSectionTestsDataGenerator))]
    public void XRefSubSection_Equals_CheckValidity(int objectNumber1, int objectNumber2, List<XRefEntry> entries1, List<XRefEntry> entries2, bool expectedValue)
    {
        // Arrange
        XRefSubSection xRefEntry1 = new(objectNumber1, entries1);
        XRefSubSection xRefEntry2 = new(objectNumber2, entries2);

        // Act
        bool actualResult = xRefEntry1.Equals(xRefEntry2);

        // Assert
        Assert.Equal(expectedValue, actualResult);
    }

    [Theory(DisplayName = "Check if Equals method with null object returns always false")]
    [MemberData(nameof(XRefSubSectionTestsDataGenerator.XRefSubSection_NoExpectedData_TestCases), MemberType = typeof(XRefSubSectionTestsDataGenerator))]
    public void XRefSubSection_EqualsNullObject_CheckValidity(int objectNumber, List<XRefEntry> entries)
    {
        // Arrange
        XRefSubSection xRefEntry = new(objectNumber, entries);

        // Act
        bool actualResult1 = xRefEntry.Equals(null);

        Debug.Assert(xRefEntry != null, nameof(xRefEntry) + " != null");
        bool actualResult2 = xRefEntry.Equals((object?)null);

        // Assert
        Assert.False(actualResult1);
        Assert.False(actualResult2);
    }
}

[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "TestData generator class can be in the same file")]
internal static class XRefSubSectionTestsDataGenerator
{
    public static IEnumerable<object[]> XRefSubSection_Content_TestCases()
    {
        yield return new object[] { 0, new List<XRefEntry> { new(0, 65535, XRefEntryType.Free) }, "0 1\n0000000000 65535 f \n" };
        yield return new object[] { 3, new List<XRefEntry> { new(25325, 0, XRefEntryType.InUse) }, "3 1\n0000025325 00000 n \n" };
        yield return new object[] { 23, new List<XRefEntry> { new(25518, 2, XRefEntryType.InUse), new(25635, 0, XRefEntryType.InUse) }, "23 2\n0000025518 00002 n \n0000025635 00000 n \n" };
        yield return new object[] { 30, new List<XRefEntry> { new(25777, 0, XRefEntryType.InUse) }, "30 1\n0000025777 00000 n \n" };
    }

    public static IEnumerable<object[]> XRefSubSection_ObjectNumber_TestCases()
    {
        yield return new object[] { 0, new List<XRefEntry> { new(0, 65535, XRefEntryType.Free) }, 0 };
        yield return new object[] { 3, new List<XRefEntry> { new(25325, 0, XRefEntryType.InUse) }, 3 };
        yield return new object[] { 23, new List<XRefEntry> { new(25518, 2, XRefEntryType.InUse), new(25635, 0, XRefEntryType.InUse) }, 23 };
        yield return new object[] { 30, new List<XRefEntry> { new(25777, 0, XRefEntryType.InUse) }, 30 };
    }

    public static IEnumerable<object[]> XRefSubSection_NumberOfEntries_TestCases()
    {
        yield return new object[] { 0, new List<XRefEntry> { new(0, 65535, XRefEntryType.Free) }, 1 };
        yield return new object[] { 3, new List<XRefEntry> { new(25325, 0, XRefEntryType.InUse) }, 1 };
        yield return new object[] { 23, new List<XRefEntry> { new(25518, 2, XRefEntryType.InUse), new(25635, 0, XRefEntryType.InUse) }, 2 };
        yield return new object[] { 30, new List<XRefEntry> { new(25777, 0, XRefEntryType.InUse) }, 1 };
    }

    public static IEnumerable<object[]> XRefSubSection_NoExpectedData_TestCases()
    {
        yield return new object[] { 0, new List<XRefEntry> { new(0, 65535, XRefEntryType.Free) } };
        yield return new object[] { 3, new List<XRefEntry> { new(25325, 0, XRefEntryType.InUse) } };
        yield return new object[] { 23, new List<XRefEntry> { new(25518, 2, XRefEntryType.InUse), new(25635, 0, XRefEntryType.InUse) } };
        yield return new object[] { 30, new List<XRefEntry> { new(25777, 0, XRefEntryType.InUse) } };
    }

    public static IEnumerable<object[]> XRefSubSection_Equals_TestCases()
    {
        yield return new object[] { 0, 0, new List<XRefEntry> { new(0, 65535, XRefEntryType.Free) }, new List<XRefEntry> { new(0, 65535, XRefEntryType.Free) }, true };
        yield return new object[] { 3, 1, new List<XRefEntry> { new(25325, 0, XRefEntryType.InUse) }, new List<XRefEntry> { new(25325, 0, XRefEntryType.InUse) }, false };
        yield return new object[]
        {
            23, 23, new List<XRefEntry> { new(25518, 2, XRefEntryType.InUse), new(25635, 0, XRefEntryType.InUse) }, new List<XRefEntry> { new(25518, 2, XRefEntryType.InUse) }, false,
        };
        yield return new object[] { 30, 30, new List<XRefEntry> { new(25777, 0, XRefEntryType.InUse) }, new List<XRefEntry> { new(25777, 65535, XRefEntryType.InUse) }, false };
    }

    public static IEnumerable<object[]> XRefSubSection_Bytes_TestCases()
    {
        yield return new object[]
        {
            0, new List<XRefEntry> { new(0, 65535, XRefEntryType.Free) },
            new byte[] { 0x30, 0x20, 0x31, 0x0A, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x20, 0x36, 0x35, 0x35, 0x33, 0x35, 0x20, 0x66, 0x20, 0x0A },
        };
        yield return new object[]
        {
            3, new List<XRefEntry> { new(25325, 0, XRefEntryType.InUse) },
            new byte[] { 0x33, 0x20, 0x31, 0x0A, 0x30, 0x30, 0x30, 0x30, 0x30, 0x32, 0x35, 0x33, 0x32, 0x35, 0x20, 0x30, 0x30, 0x30, 0x30, 0x30, 0x20, 0x6E, 0x20, 0x0A },
        };
        yield return new object[]
        {
            23, new List<XRefEntry> { new(25518, 2, XRefEntryType.InUse), new(25635, 0, XRefEntryType.InUse) },
            new byte[]
            {
                0x32, 0x33, 0x20, 0x32, 0x0A, 0x30, 0x30, 0x30, 0x30, 0x30, 0x32, 0x35, 0x35, 0x31, 0x38, 0x20, 0x30, 0x30, 0x30, 0x30, 0x32, 0x20, 0x6E, 0x20, 0x0A, 0x30, 0x30, 0x30, 0x30,
                0x30, 0x32, 0x35, 0x36, 0x33, 0x35, 0x20, 0x30, 0x30, 0x30, 0x30, 0x30, 0x20, 0x6E, 0x20, 0x0A,
            },
        };
        yield return new object[]
        {
            30, new List<XRefEntry> { new(25777, 0, XRefEntryType.InUse) },
            new byte[] { 0x33, 0x30, 0x20, 0x31, 0x0A, 0x30, 0x30, 0x30, 0x30, 0x30, 0x32, 0x35, 0x37, 0x37, 0x37, 0x20, 0x30, 0x30, 0x30, 0x30, 0x30, 0x20, 0x6E, 0x20, 0x0A },
        };
    }
}

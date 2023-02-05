using System;
using System.Collections.Generic;
using System.Diagnostics;
using Off.Net.Pdf.Core.FileStructure;
using Xunit;

namespace Off.Net.Pdf.Core.Tests.FileStructure;

public class XRefSectionTests
{
    [Fact(DisplayName = $"Constructor with negative {nameof(XRefSection.NumberOfSubSections)} should throw an {nameof(ArgumentOutOfRangeException)}")]
    public void XRefSection_NegativeGenerationNumber_ShouldThrowException()
    {
        // Arrange
        ICollection<XRefSubSection> subSections = new List<XRefSubSection>(0);

        // Act
        XRefSection XRefSectionFunction() => new(subSections);

        // Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(XRefSectionFunction);
        Assert.StartsWith(Resource.XRefSection_MustHaveNonEmptyEntriesCollection, exception.Message);
    }

    [Theory(DisplayName = $"{nameof(XRefSection.Content)} property should return a valid value")]
    [MemberData(nameof(XRefSectionTestsDataGenerator.XRefSection_Content_TestCases), MemberType = typeof(XRefSectionTestsDataGenerator))]
    public void XRefSection_Content_ShouldReturnValidValue(List<XRefSubSection> subSections, string expectedContent)
    {
        // Arrange
        XRefSection xRefSection = new(subSections);

        // Act
        string actualContent = xRefSection.Content;

        // Assert
        Assert.Equal(expectedContent, actualContent);
    }

    [Theory(DisplayName = $"{nameof(XRefSection.Length)} property should return always 20")]
    [MemberData(nameof(XRefSectionTestsDataGenerator.XRefSection_Length_TestCases), MemberType = typeof(XRefSectionTestsDataGenerator))]
    public void XRefSection_Length_ShouldReturnValidValue(List<XRefSubSection> subSections, int expectedLength)
    {
        // Arrange
        XRefSection xRefSection = new(subSections);

        // Act
        int actualLength = xRefSection.Length;

        // Assert
        Assert.Equal(expectedLength, actualLength);
    }

    [Theory(DisplayName = $"{nameof(XRefSection.NumberOfSubSections)} property should return a valid value")]
    [MemberData(nameof(XRefSectionTestsDataGenerator.XRefSection_NumberOfEntries_TestCases), MemberType = typeof(XRefSectionTestsDataGenerator))]
    public void XRefSection_NumberOfEntries_ShouldReturnValidValue(List<XRefSubSection> subSections, int expectedNumberOfEntries)
    {
        // Arrange
        XRefSection xRefSection = new(subSections);

        // Act
        long actualNumberOfEntries = xRefSection.NumberOfSubSections;

        // Assert
        Assert.Equal(expectedNumberOfEntries, actualNumberOfEntries);
    }

    [Theory(DisplayName = $"{nameof(XRefSection.Bytes)} property should return a valid value")]
    [MemberData(nameof(XRefSectionTestsDataGenerator.XRefSection_Bytes_TestCases), MemberType = typeof(XRefSectionTestsDataGenerator))]
    public void XRefSection_Bytes_ShouldReturnValidValue(List<XRefSubSection> subSections, byte[] expectedBytes)
    {
        // Arrange
        XRefSection xRefSection = new(subSections);

        // Act
        byte[] actualBytes = xRefSection.Bytes.ToArray();

        // Assert
        Assert.Equal(expectedBytes, actualBytes);
    }


    [Theory(DisplayName = "Check if GetHashCode method returns valid value")]
    [MemberData(nameof(XRefSectionTestsDataGenerator.XRefSection_NoExpectedData_TestCases), MemberType = typeof(XRefSectionTestsDataGenerator))]
    public void XRefSection_GetHashCode_CheckValidity(List<XRefSubSection> subSections)
    {
        // Arrange
        XRefSection xRefSection = new(subSections);
        int expectedHashCode = HashCode.Combine(nameof(XRefSection).GetHashCode(), subSections.GetHashCode());

        // Act
        int actualHashCode = xRefSection.GetHashCode();

        // Assert
        Assert.Equal(expectedHashCode, actualHashCode);
    }

    [Theory(DisplayName = $"{nameof(XRefSection.Content)} property, accessed multiple times, should return the same reference")]
    [MemberData(nameof(XRefSectionTestsDataGenerator.XRefSection_NoExpectedData_TestCases), MemberType = typeof(XRefSectionTestsDataGenerator))]
    public void XRefSection_Content_MultipleAccesses_ShouldReturnSameReference(List<XRefSubSection> subSections)
    {
        // Arrange
        XRefSection xRefSection = new(subSections);

        // Act
        string actualContent1 = xRefSection.Content;
        string actualContent2 = xRefSection.Content;

        // Assert
        Assert.True(ReferenceEquals(actualContent1, actualContent2));
    }

    [Theory(DisplayName = "Check if Equals returns a valid result")]
    [MemberData(nameof(XRefSectionTestsDataGenerator.XRefSection_Equals_TestCases), MemberType = typeof(XRefSectionTestsDataGenerator))]
    public void XRefSection_Equals_CheckValidity(List<XRefSubSection> subSections1, List<XRefSubSection> subSections2, bool expectedValue)
    {
        // Arrange
        XRefSection xRefSection1 = new(subSections1);
        XRefSection xRefSection2 = new(subSections2);

        // Act
        bool actualResult = xRefSection1.Equals(xRefSection2);

        // Assert
        Assert.Equal(expectedValue, actualResult);
    }

    [Theory(DisplayName = "Check if Equals method with null object returns always false")]
    [MemberData(nameof(XRefSectionTestsDataGenerator.XRefSection_NoExpectedData_TestCases), MemberType = typeof(XRefSectionTestsDataGenerator))]
    public void XRefSection_EqualsNullObject_CheckValidity(List<XRefSubSection> subSections)
    {
        // Arrange
        XRefSection xRefSection = new(subSections);

        // Act
        bool actualResult1 = xRefSection.Equals(null);

        Debug.Assert(xRefSection != null, nameof(xRefSection) + " != null");
        bool actualResult2 = xRefSection.Equals((object?)null);

        // Assert
        Assert.False(actualResult1);
        Assert.False(actualResult2);
    }
}

internal static class XRefSectionTestsDataGenerator
{
    public static IEnumerable<object[]> XRefSection_Content_TestCases()
    {
        yield return new object[] { new List<XRefSubSection> { new(0, new List<XRefEntry> { new(0, 65535, XRefEntryType.Free) }) }, "xref\n0 1\n0000000000 65535 f \n" };
        yield return new object[] { new List<XRefSubSection> { new(3, new List<XRefEntry> { new(25325, 0, XRefEntryType.InUse) }) }, "xref\n3 1\n0000025325 00000 n \n" };
        yield return new object[] { new List<XRefSubSection> { new(30, new List<XRefEntry> { new(25777, 0, XRefEntryType.InUse) }) }, "xref\n30 1\n0000025777 00000 n \n" };
        yield return new object[]
        {
            new List<XRefSubSection> { new(23, new List<XRefEntry> { new(25518, 2, XRefEntryType.InUse), new(25635, 0, XRefEntryType.InUse) }) },
            "xref\n23 2\n0000025518 00002 n \n0000025635 00000 n \n"
        };
        yield return new object[]
        {
            new List<XRefSubSection>
            {
                new(0, new List<XRefEntry> { new(0, 65535, XRefEntryType.Free) }),
                new(3, new List<XRefEntry> { new(25325, 0, XRefEntryType.InUse) }),
                new(30, new List<XRefEntry> { new(25777, 0, XRefEntryType.InUse) }),
                new(23, new List<XRefEntry> { new(25518, 2, XRefEntryType.InUse), new(25635, 0, XRefEntryType.InUse) }),
            },
            "xref\n0 1\n0000000000 65535 f \n3 1\n0000025325 00000 n \n30 1\n0000025777 00000 n \n23 2\n0000025518 00002 n \n0000025635 00000 n \n"
        };
    }

    public static IEnumerable<object[]> XRefSection_Length_TestCases()
    {
        yield return new object[] { new List<XRefSubSection> { new(0, new List<XRefEntry> { new(0, 65535, XRefEntryType.Free) }) }, 29 };
        yield return new object[] { new List<XRefSubSection> { new(3, new List<XRefEntry> { new(25325, 0, XRefEntryType.InUse) }) }, 29 };
        yield return new object[] { new List<XRefSubSection> { new(30, new List<XRefEntry> { new(25777, 0, XRefEntryType.InUse) }) }, 30 };
        yield return new object[] { new List<XRefSubSection> { new(23, new List<XRefEntry> { new(25518, 2, XRefEntryType.InUse), new(25635, 0, XRefEntryType.InUse) }) }, 50 };
        yield return new object[]
        {
            new List<XRefSubSection>
            {
                new(0, new List<XRefEntry> { new(0, 65535, XRefEntryType.Free) }),
                new(3, new List<XRefEntry> { new(25325, 0, XRefEntryType.InUse) }),
                new(30, new List<XRefEntry> { new(25777, 0, XRefEntryType.InUse) }),
                new(23, new List<XRefEntry> { new(25518, 2, XRefEntryType.InUse), new(25635, 0, XRefEntryType.InUse) }),
            },
            123
        };
    }

    public static IEnumerable<object[]> XRefSection_NumberOfEntries_TestCases()
    {
        yield return new object[] { new List<XRefSubSection> { new(0, new List<XRefEntry> { new(0, 65535, XRefEntryType.Free) }) }, 1 };
        yield return new object[] { new List<XRefSubSection> { new(3, new List<XRefEntry> { new(25325, 0, XRefEntryType.InUse) }) }, 1 };
        yield return new object[] { new List<XRefSubSection> { new(30, new List<XRefEntry> { new(25777, 0, XRefEntryType.InUse) }) }, 1 };
        yield return new object[] { new List<XRefSubSection> { new(23, new List<XRefEntry> { new(25518, 2, XRefEntryType.InUse), new(25635, 0, XRefEntryType.InUse) }) }, 1 };
        yield return new object[]
        {
            new List<XRefSubSection>
            {
                new(0, new List<XRefEntry> { new(0, 65535, XRefEntryType.Free) }),
                new(3, new List<XRefEntry> { new(25325, 0, XRefEntryType.InUse) }),
                new(30, new List<XRefEntry> { new(25777, 0, XRefEntryType.InUse) }),
                new(23, new List<XRefEntry> { new(25518, 2, XRefEntryType.InUse), new(25635, 0, XRefEntryType.InUse) }),
            },
            4
        };
    }

    public static IEnumerable<object[]> XRefSection_NoExpectedData_TestCases()
    {
        yield return new object[] { new List<XRefSubSection> { new(0, new List<XRefEntry> { new(0, 65535, XRefEntryType.Free) }) } };
        yield return new object[] { new List<XRefSubSection> { new(3, new List<XRefEntry> { new(25325, 0, XRefEntryType.InUse) }) } };
        yield return new object[] { new List<XRefSubSection> { new(30, new List<XRefEntry> { new(25777, 0, XRefEntryType.InUse) }) } };
        yield return new object[] { new List<XRefSubSection> { new(23, new List<XRefEntry> { new(25518, 2, XRefEntryType.InUse), new(25635, 0, XRefEntryType.InUse) }) } };
        yield return new object[]
        {
            new List<XRefSubSection>
            {
                new(0, new List<XRefEntry> { new(0, 65535, XRefEntryType.Free) }),
                new(3, new List<XRefEntry> { new(25325, 0, XRefEntryType.InUse) }),
                new(30, new List<XRefEntry> { new(25777, 0, XRefEntryType.InUse) }),
                new(23, new List<XRefEntry> { new(25518, 2, XRefEntryType.InUse), new(25635, 0, XRefEntryType.InUse) }),
            }
        };
    }

    public static IEnumerable<object[]> XRefSection_Equals_TestCases()
    {
        yield return new object[]
        {
            new List<XRefSubSection> { new(0, new List<XRefEntry> { new(0, 65535, XRefEntryType.Free) }) },
            new List<XRefSubSection> { new(0, new List<XRefEntry> { new(0, 65535, XRefEntryType.Free) }) },
            true
        };
        yield return new object[]
        {
            new List<XRefSubSection> { new(3, new List<XRefEntry> { new(25325, 0, XRefEntryType.InUse) }) },
            new List<XRefSubSection> { new(3, new List<XRefEntry> { new(98765, 0, XRefEntryType.InUse) }) },
            false
        };
        yield return new object[]
        {
            new List<XRefSubSection> { new(30, new List<XRefEntry> { new(25777, 0, XRefEntryType.InUse) }) },
            new List<XRefSubSection> { new(30, new List<XRefEntry> { new(25777, 0, XRefEntryType.InUse) }) },
            true
        };
        yield return new object[]
        {
            new List<XRefSubSection> { new(23, new List<XRefEntry> { new(25518, 2, XRefEntryType.InUse), new(25635, 0, XRefEntryType.InUse) }) },
            new List<XRefSubSection> { new(1, new List<XRefEntry> { new(25518, 2, XRefEntryType.InUse), new(25635, 0, XRefEntryType.InUse) }) },
            false
        };
        yield return new object[]
        {
            new List<XRefSubSection>
            {
                new(0, new List<XRefEntry> { new(0, 65535, XRefEntryType.Free) }),
                new(3, new List<XRefEntry> { new(25325, 0, XRefEntryType.InUse) }),
                new(30, new List<XRefEntry> { new(25777, 0, XRefEntryType.InUse) }),
                new(23, new List<XRefEntry> { new(25518, 2, XRefEntryType.InUse), new(25635, 0, XRefEntryType.InUse) }),
            },
            new List<XRefSubSection>
            {
                new(0, new List<XRefEntry> { new(0, 65535, XRefEntryType.Free) }),
                new(3, new List<XRefEntry> { new(25325, 0, XRefEntryType.InUse) }),
                new(30, new List<XRefEntry> { new(25777, 0, XRefEntryType.InUse) }),
                new(23, new List<XRefEntry> { new(25518, 2, XRefEntryType.InUse), new(25635, 0, XRefEntryType.InUse) }),
            },
            true
        };
        yield return new object[]
        {
            new List<XRefSubSection>
            {
                new(0, new List<XRefEntry> { new(0, 123, XRefEntryType.Free) }),
                new(3, new List<XRefEntry> { new(1234, 0, XRefEntryType.InUse) }),
                new(30, new List<XRefEntry> { new(25777, 0, XRefEntryType.Free) }),
                new(123, new List<XRefEntry> { new(25518, 2, XRefEntryType.InUse), new(25635, 0, XRefEntryType.InUse) }),
            },
            new List<XRefSubSection>
            {
                new(0, new List<XRefEntry> { new(0, 65535, XRefEntryType.Free) }),
                new(3, new List<XRefEntry> { new(25325, 0, XRefEntryType.InUse) }),
                new(30, new List<XRefEntry> { new(25777, 0, XRefEntryType.InUse) }),
                new(23, new List<XRefEntry> { new(25518, 2, XRefEntryType.InUse), new(25635, 0, XRefEntryType.InUse) }),
            },
            false
        };
    }

    public static IEnumerable<object[]> XRefSection_Bytes_TestCases()
    {
        yield return new object[]
        {
            new List<XRefSubSection> { new(0, new List<XRefEntry> { new(0, 65535, XRefEntryType.Free) }) },
            new byte[]
            {
                0x78, 0x72, 0x65, 0x66, 0x0A, 0x30, 0x20, 0x31, 0x0A, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x20, 0x36, 0x35, 0x35, 0x33, 0x35, 0x20, 0x66, 0x20, 0x0A
            }
        };
        yield return new object[]
        {
            new List<XRefSubSection> { new(3, new List<XRefEntry> { new(25325, 0, XRefEntryType.InUse) }) },
            new byte[]
            {
                0x78, 0x72, 0x65, 0x66, 0x0A, 0x33, 0x20, 0x31, 0x0A, 0x30, 0x30, 0x30, 0x30, 0x30, 0x32, 0x35, 0x33, 0x32, 0x35, 0x20, 0x30, 0x30, 0x30, 0x30, 0x30, 0x20, 0x6E, 0x20, 0x0A
            }
        };
        yield return new object[]
        {
            new List<XRefSubSection> { new(30, new List<XRefEntry> { new(25777, 0, XRefEntryType.InUse) }) },
            new byte[]
            {
                0x78, 0x72, 0x65, 0x66, 0x0A, 0x33, 0x30, 0x20, 0x31, 0x0A, 0x30, 0x30, 0x30, 0x30, 0x30, 0x32, 0x35, 0x37, 0x37, 0x37, 0x20, 0x30, 0x30, 0x30, 0x30, 0x30, 0x20, 0x6E, 0x20,
                0x0A
            }
        };
        yield return new object[]
        {
            new List<XRefSubSection> { new(23, new List<XRefEntry> { new(25518, 2, XRefEntryType.InUse), new(25635, 0, XRefEntryType.InUse) }) },
            new byte[]
            {
                0x78, 0x72, 0x65, 0x66, 0x0A, 0x32, 0x33, 0x20, 0x32, 0x0A, 0x30, 0x30, 0x30, 0x30, 0x30, 0x32, 0x35, 0x35, 0x31, 0x38, 0x20, 0x30, 0x30, 0x30, 0x30, 0x32, 0x20, 0x6E, 0x20,
                0x0A, 0x30, 0x30, 0x30, 0x30, 0x30, 0x32, 0x35, 0x36, 0x33, 0x35, 0x20, 0x30, 0x30, 0x30, 0x30, 0x30, 0x20, 0x6E, 0x20, 0x0A
            }
        };
        yield return new object[]
        {
            new List<XRefSubSection>
            {
                new(0, new List<XRefEntry> { new(0, 65535, XRefEntryType.Free) }),
                new(3, new List<XRefEntry> { new(25325, 0, XRefEntryType.InUse) }),
                new(30, new List<XRefEntry> { new(25777, 0, XRefEntryType.InUse) }),
                new(23, new List<XRefEntry> { new(25518, 2, XRefEntryType.InUse), new(25635, 0, XRefEntryType.InUse) }),
            },
            new byte[]
            {
                0x78, 0x72, 0x65, 0x66, 0x0A, 0x30, 0x20, 0x31, 0x0A, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x20, 0x36, 0x35, 0x35, 0x33, 0x35, 0x20, 0x66, 0x20, 0x0A, 0x33,
                0x20, 0x31, 0x0A, 0x30, 0x30, 0x30, 0x30, 0x30, 0x32, 0x35, 0x33, 0x32, 0x35, 0x20, 0x30, 0x30, 0x30, 0x30, 0x30, 0x20, 0x6E, 0x20, 0x0A, 0x33, 0x30, 0x20, 0x31, 0x0A, 0x30, 0x30,
                0x30, 0x30, 0x30, 0x32, 0x35, 0x37, 0x37, 0x37, 0x20, 0x30, 0x30, 0x30, 0x30, 0x30, 0x20, 0x6E, 0x20, 0x0A, 0x32, 0x33, 0x20, 0x32, 0x0A, 0x30, 0x30, 0x30, 0x30, 0x30, 0x32, 0x35,
                0x35, 0x31, 0x38, 0x20, 0x30, 0x30, 0x30, 0x30, 0x32, 0x20, 0x6E, 0x20, 0x0A, 0x30, 0x30, 0x30, 0x30, 0x30, 0x32, 0x35, 0x36, 0x33, 0x35, 0x20, 0x30, 0x30, 0x30, 0x30, 0x30, 0x20,
                0x6E, 0x20, 0x0A
            }
        };
    }
}

// <copyright file="TextObjectTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.Core.ContentStreamAndResources;
using OffDotNet.Pdf.Core.Text;
using OffDotNet.Pdf.Core.Text.Operations.TextPosition;
using OffDotNet.Pdf.Core.Text.Operations.TextShowing;
using OffDotNet.Pdf.Core.Text.Operations.TextState;
using Xunit;

namespace OffDotNet.Pdf.Core.Tests.Text;

public class TextObjectTests
{
    [Theory(DisplayName = $"The {nameof(TextObject.Value)} property should return a valid value")]
    [MemberData(nameof(TextObjectTestsDataGenerator.TextObject_NoExpectedData_TestCases), MemberType = typeof(TextObjectTestsDataGenerator))]
    public void TextObject_Value_ShouldReturnValidValues(TextObject textObject)
    {
        // Arrange

        // Act
        IReadOnlyCollection<PdfOperation> textOperations = textObject.Value.ToArray();

        // Assert
        Assert.Equal(textOperations, textObject.Value);
    }

    [Theory(DisplayName = $"The {nameof(TextObject.Content)} property should return a valid value")]
    [MemberData(nameof(TextObjectTestsDataGenerator.TextObject_Content_TestCases), MemberType = typeof(TextObjectTestsDataGenerator))]
    public void TextObject_Content_ShouldReturnValidValue(TextObject textObject, string expectedContent)
    {
        // Arrange

        // Act
        string actualContent = textObject.Content;

        // Assert
        Assert.Equal(expectedContent, actualContent);
    }

    [Theory(DisplayName = $"The {nameof(TextObject.Bytes)} property should return a valid value")]
    [MemberData(nameof(TextObjectTestsDataGenerator.TextObject_Bytes_TestCases), MemberType = typeof(TextObjectTestsDataGenerator))]
    public void TextObject_Bytes_ShouldReturnValidValue(TextObject textObject, byte[] expectedBytes)
    {
        // Arrange

        // Act
        byte[] actualBytes = textObject.Bytes.ToArray();

        // Assert
        Assert.Equal(expectedBytes, actualBytes);
    }

    [Theory(DisplayName = "The GetHashCode method should return a valid value")]
    [MemberData(nameof(TextObjectTestsDataGenerator.TextObject_NoExpectedData_TestCases), MemberType = typeof(TextObjectTestsDataGenerator))]
    public void TextObject_GetHashCode_ShouldReturnValidValue(TextObject textObject)
    {
        // Arrange
        int expectedHashCode = HashCode.Combine(nameof(TextObject), textObject.Value);

        // Act
        int actualHashCode = textObject.GetHashCode();

        // Assert
        Assert.Equal(expectedHashCode, actualHashCode);
    }

    [Theory(DisplayName = "The Equals property should return a valid value")]
    [MemberData(nameof(TextObjectTestsDataGenerator.TextObject_Equals_TestCases), MemberType = typeof(TextObjectTestsDataGenerator))]
    public void TextObject_Equals_ShouldReturnValidValue(TextObject textObject1, TextObject textObject2, bool expectedResult)
    {
        // Arrange

        // Act
        bool actualResult = textObject1.Equals(textObject2);

        // Assert
        Assert.Equal(expectedResult, actualResult);
    }
}

[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "TestData generator class can be in the same file")]
internal static class TextObjectTestsDataGenerator
{
    private static readonly PdfOperation[] PdfOperations1 = { new FontOperation("F12", 13), new MoveTextOperation(13, 46) };

    private static readonly PdfOperation[] PdfOperations2 = { new FontOperation("F4", 13), new MoveTextOperation(2, 72), new ShowTextOperation("A text with special chars !@#$%^&*()") };

    public static IEnumerable<object[]> TextObject_NoExpectedData_TestCases()
    {
        yield return new object[] { new TextObject(PdfOperations1) };
        yield return new object[] { new TextObject(PdfOperations2) };
    }

    public static IEnumerable<object[]> TextObject_Content_TestCases()
    {
        yield return new object[] { new TextObject(PdfOperations1), "BT\n/F12 13 Tf\n13 46 Td\nET\n" };
        yield return new object[] { new TextObject(PdfOperations2), "BT\n/F4 13 Tf\n2 72 Td\n(A text with special chars !@#$%^&*()) Tj\nET\n" };
    }

    public static IEnumerable<object[]> TextObject_Length_TestCases()
    {
        yield return new object[] { new TextObject(PdfOperations1), 26 };
        yield return new object[] { new TextObject(PdfOperations2), 66 };
    }

    public static IEnumerable<object[]> TextObject_Bytes_TestCases()
    {
        yield return new object[]
        {
            new TextObject(PdfOperations1),
            new byte[] { 0x42, 0x54, 0x0A, 0x2F, 0x46, 0x31, 0x32, 0x20, 0x31, 0x33, 0x20, 0x54, 0x66, 0x0A, 0x31, 0x33, 0x20, 0x34, 0x36, 0x20, 0x54, 0x64, 0x0A, 0x45, 0x54, 0x0A },
        };
        yield return new object[]
        {
            new TextObject(PdfOperations2),
            new byte[]
            {
                0x42, 0x54, 0x0A, 0x2F, 0x46, 0x34, 0x20, 0x31, 0x33, 0x20, 0x54, 0x66, 0x0A, 0x32, 0x20, 0x37, 0x32, 0x20, 0x54, 0x64, 0x0A, 0x28, 0x41, 0x20, 0x74, 0x65, 0x78, 0x74, 0x20,
                0x77, 0x69, 0x74, 0x68, 0x20, 0x73, 0x70, 0x65, 0x63, 0x69, 0x61, 0x6C, 0x20, 0x63, 0x68, 0x61, 0x72, 0x73, 0x20, 0x21, 0x40, 0x23, 0x24, 0x25, 0x5E, 0x26, 0x2A, 0x28, 0x29,
                0x29, 0x20, 0x54, 0x6A, 0x0A, 0x45, 0x54, 0x0A,
            },
        };
    }

    public static IEnumerable<object[]> TextObject_Equals_TestCases()
    {
        yield return new object[] { new TextObject(PdfOperations1), new TextObject(PdfOperations1), true };
        yield return new object[] { new TextObject(PdfOperations1), new TextObject(PdfOperations2), false };
        yield return new object[] { new TextObject(PdfOperations2), new TextObject(PdfOperations2), true };
        yield return new object[] { new TextObject(PdfOperations2), null!, false };
    }
}

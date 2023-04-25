// <copyright file="ShowTextOperationTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using Off.Net.Pdf.Core.Primitives;
using Off.Net.Pdf.Core.Text.Operations.TextShowing;
using Xunit;

namespace Off.Net.Pdf.Core.Tests.Text.Operations.TextShowing;

public class ShowTextOperationTests
{
    [Fact(DisplayName = $"The {nameof(ShowTextOperation.PdfOperator)} property should return a valid value")]
    public void ShowTextOperation_PdfOperatorProperty_ShouldReturnValidValue()
    {
        // Arrange
        const string expectedOperator = "Tj";
        ShowTextOperation showTextOperation = new("test");

        // Act
        string actualPdfOperator = showTextOperation.PdfOperator;

        // Assert
        Assert.Equal(expectedOperator, actualPdfOperator);
    }

    [Theory(DisplayName = $"The {nameof(ShowTextOperation.Text)} property should return a valid value")]
    [InlineData("FirstText")]
    [InlineData("SecondText with special chars !@#$%^&*()")]
    public void ShowTextOperation_Text_ShouldReturnValidValues(string text)
    {
        // Arrange
        ShowTextOperation showTextOperation = new(text);

        // Act
        string actualText = showTextOperation.Text;

        // Assert
        Assert.Equal(text, actualText);
    }

    [Theory(DisplayName = $"The {nameof(ShowTextOperation.Content)} property should return a valid value")]
    [InlineData("FirstText", "(FirstText) Tj\n")]
    [InlineData("SecondText with special chars !@#$%^&*()", "(SecondText with special chars !@#$%^&*()) Tj\n")]
    public void ShowTextOperation_Content_ShouldReturnValidValue(string text, string expectedContent)
    {
        // Arrange
        ShowTextOperation showTextOperation = new(text);

        // Act
        string actualContent = showTextOperation.Content;

        // Assert
        Assert.Equal(expectedContent, actualContent);
    }

    [Theory(DisplayName = $"The {nameof(ShowTextOperation.Length)} property should return a valid value")]
    [InlineData("FirstText", 15)]
    [InlineData("SecondText with special chars !@#$%^&*()", 46)]
    public void ShowTextOperation_Length_ShouldReturnValidValue(string text, int expectedLength)
    {
        // Arrange
        ShowTextOperation showTextOperation = new(text);

        // Act
        int actualLength = showTextOperation.Length;

        // Assert
        Assert.Equal(expectedLength, actualLength);
    }

    [Theory(DisplayName = $"The {nameof(ShowTextOperation.Bytes)} property should return a valid value")]
    [MemberData(nameof(ShowTextOperationTestsDataGenerator.ShowTextOperation_Bytes_TestCases), MemberType = typeof(ShowTextOperationTestsDataGenerator))]
    public void ShowTextOperation_Bytes_ShouldReturnValidValue(string text, byte[] expectedBytes)
    {
        // Arrange
        ShowTextOperation showTextOperation = new(text);

        // Act
        byte[] actualBytes = showTextOperation.Bytes.ToArray();

        // Assert
        Assert.Equal(expectedBytes, actualBytes);
    }

    [Theory(DisplayName = "The GetHashCode method should return a valid value")]
    [InlineData("FirstText")]
    [InlineData("SecondText with special chars !@#$%^&*()")]
    public void ShowTextOperation_GetHashCode_ShouldReturnValidValue(string text)
    {
        // Arrange
        PdfString pdfString = text;
        int expectedHashCode = HashCode.Combine(nameof(ShowTextOperation), pdfString, ShowTextOperation.OperatorName);
        ShowTextOperation showTextOperation = new(pdfString);

        // Act
        int actualHashCode = showTextOperation.GetHashCode();

        // Assert
        Assert.Equal(expectedHashCode, actualHashCode);
    }

    [Theory(DisplayName = "The Equals property should return a valid value")]
    [MemberData(nameof(ShowTextOperationTestsDataGenerator.ShowTextOperation_Equals_TestCases), MemberType = typeof(ShowTextOperationTestsDataGenerator))]
    public void ShowTextOperation_Equals_ShouldReturnValidValue(ShowTextOperation showTextOperation1, ShowTextOperation showTextOperation2, bool expectedResult)
    {
        // Arrange

        // Act
        bool actualResult = showTextOperation1.Equals(showTextOperation2);

        // Assert
        Assert.Equal(expectedResult, actualResult);
    }
}

[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "TestData generator class can be in the same file")]
internal static class ShowTextOperationTestsDataGenerator
{
    public static IEnumerable<object[]> ShowTextOperation_Equals_TestCases()
    {
        yield return new object[] { new ShowTextOperation("FirstText"), new ShowTextOperation("FirstText"), true };
        yield return new object[] { new ShowTextOperation("FirstText"), new ShowTextOperation("FirstText_!"), false };
        yield return new object[] { new ShowTextOperation("SecondText with special chars !@#$%^&*()"), new ShowTextOperation("SecondText with special chars !@#$%^&*()"), true };
        yield return new object[] { new ShowTextOperation("SecondText with special chars !@#$%^&*()"), new ShowTextOperation("%% SecondText with special chars !@#$%^&*()"), false };
        yield return new object[] { new ShowTextOperation("SecondText with special chars !@#$%^&*()"), null!, false };
    }

    public static IEnumerable<object[]> ShowTextOperation_Bytes_TestCases()
    {
        yield return new object[] { "FirstText", new byte[] { 0x28, 0x46, 0x69, 0x72, 0x73, 0x74, 0x54, 0x65, 0x78, 0x74, 0x29, 0x20, 0x54, 0x6A, 0x0A } };
        yield return new object[]
        {
            "SecondText with special chars !@#$%^&*()",
            new byte[]
            {
                0x28, 0x53, 0x65, 0x63, 0x6F, 0x6E, 0x64, 0x54, 0x65, 0x78, 0x74, 0x20, 0x77, 0x69, 0x74, 0x68, 0x20, 0x73, 0x70, 0x65, 0x63, 0x69, 0x61, 0x6C, 0x20, 0x63, 0x68, 0x61, 0x72,
                0x73, 0x20, 0x21, 0x40, 0x23, 0x24, 0x25, 0x5E, 0x26, 0x2A, 0x28, 0x29, 0x29, 0x20, 0x54, 0x6A, 0x0A,
            },
        };
    }
}

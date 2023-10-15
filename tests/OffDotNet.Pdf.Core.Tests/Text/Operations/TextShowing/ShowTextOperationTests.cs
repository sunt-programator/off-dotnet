// <copyright file="ShowTextOperationTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.Core.Primitives;
using OffDotNet.Pdf.Core.Text.Operations.TextShowing;
using Xunit;

namespace OffDotNet.Pdf.Core.Tests.Text.Operations.TextShowing;

public class ShowTextOperationTests
{
    [Fact(DisplayName = $"The {nameof(ShowTextOperation.PdfOperator)} property should return a valid value")]
    public void ShowTextOperation_PdfOperatorProperty_ShouldReturnValidValue()
    {
        // Arrange
        const string expectedOperator = "Tj";
        IShowTextOperation showTextOperation = new ShowTextOperation("test");

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
        IShowTextOperation showTextOperation = new ShowTextOperation(text);

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
        IShowTextOperation showTextOperation = new ShowTextOperation(text);

        // Act
        string actualContent = showTextOperation.Content;

        // Assert
        Assert.Equal(expectedContent, actualContent);
    }

    [Fact(DisplayName = $"The {nameof(ShowTextOperation.Bytes)} property should return a valid value")]
    public void ShowTextOperation_Bytes_ShouldReturnValidValue()
    {
        // Arrange
        const string text = "SecondText with special chars !@#$%^&*()";
        byte[] expectedBytes =
        {
            0x28, 0x53, 0x65, 0x63, 0x6F, 0x6E, 0x64, 0x54, 0x65, 0x78, 0x74, 0x20, 0x77, 0x69, 0x74, 0x68, 0x20, 0x73, 0x70, 0x65, 0x63, 0x69, 0x61, 0x6C, 0x20, 0x63, 0x68, 0x61, 0x72,
            0x73, 0x20, 0x21, 0x40, 0x23, 0x24, 0x25, 0x5E, 0x26, 0x2A, 0x28, 0x29, 0x29, 0x20, 0x54, 0x6A, 0x0A,
        };

        IShowTextOperation showTextOperation = new ShowTextOperation(text);

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
        IShowTextOperation showTextOperation = new ShowTextOperation(pdfString);

        // Act
        int actualHashCode = showTextOperation.GetHashCode();

        // Assert
        Assert.Equal(expectedHashCode, actualHashCode);
    }

    [Theory(DisplayName = "The Equals property should return a valid value")]
    [InlineData("FirstText", "FirstText", true)]
    [InlineData("FirstText", "FirstText_!", false)]
    [InlineData("SecondText with special chars !@#$%^&*()", "SecondText with special chars !@#$%^&*()", true)]
    [InlineData("SecondText with special chars !@#$%^&*()", "%% SecondText with special chars !@#$%^&*()", false)]
    [InlineData("SecondText with special chars !@#$%^&*()", null!, false)]
    public void ShowTextOperation_Equals_ShouldReturnValidValue(string showTextOperationText1, string? showTextOperationText2, bool expectedResult)
    {
        // Arrange
        IShowTextOperation showTextOperation1 = new ShowTextOperation(showTextOperationText1);
        IShowTextOperation? showTextOperation2 = showTextOperationText2 is null ? null : new ShowTextOperation(showTextOperationText2);

        // Act
        bool actualResult = showTextOperation1.Equals(showTextOperation2);

        // Assert
        Assert.Equal(expectedResult, actualResult);
    }
}

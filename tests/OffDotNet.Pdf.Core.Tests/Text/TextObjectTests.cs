// <copyright file="TextObjectTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Tests.Text;

using OffDotNet.Pdf.Core.Common;
using OffDotNet.Pdf.Core.Text;
using OffDotNet.Pdf.Core.Text.Operations.TextPosition;
using OffDotNet.Pdf.Core.Text.Operations.TextShowing;
using OffDotNet.Pdf.Core.Text.Operations.TextState;

public class TextObjectTests
{
    [Fact(DisplayName = $"The {nameof(TextObject.Value)} property should return a valid value")]
    public void TextObject_Value_ShouldReturnValidValues()
    {
        // Arrange
        IPdfOperation fontOperation = new FontOperation("F4", 13);
        IPdfOperation moveTextOperation = new MoveTextOperation(2, 72);
        IPdfOperation showTextOperation = new ShowTextOperation("A text with special chars !@#$%^&*()");

        ITextObject textObject = new TextObject(new[] { fontOperation, moveTextOperation, showTextOperation });

        // Act
        IReadOnlyCollection<IPdfOperation> textOperations = textObject.Value.ToArray();

        // Assert
        Assert.Equal(textOperations, textObject.Value);
    }

    [Fact(DisplayName = $"The {nameof(TextObject.Content)} property should return a valid value")]
    public void TextObject_Content_ShouldReturnValidValue()
    {
        // Arrange
        IPdfOperation fontOperation = new FontOperation("F4", 13);
        IPdfOperation moveTextOperation = new MoveTextOperation(2, 72);
        IPdfOperation showTextOperation = new ShowTextOperation("A text with special chars !@#$%^&*()");
        const string expectedContent = "BT\n/F4 13 Tf\n2 72 Td\n(A text with special chars !@#$%^&*()) Tj\nET\n";

        ITextObject textObject = new TextObject(new[] { fontOperation, moveTextOperation, showTextOperation });

        // Act
        string actualContent = textObject.Content;

        // Assert
        Assert.Equal(expectedContent, actualContent);
    }

    [Fact(DisplayName = $"The {nameof(TextObject.Bytes)} property should return a valid value")]
    public void TextObject_Bytes_ShouldReturnValidValue()
    {
        // Arrange
        byte[] expectedBytes =
        {
            0x42, 0x54, 0x0A, 0x2F, 0x46, 0x34, 0x20, 0x31, 0x33, 0x20, 0x54, 0x66, 0x0A, 0x32, 0x20, 0x37, 0x32, 0x20, 0x54, 0x64, 0x0A, 0x28, 0x41, 0x20, 0x74, 0x65, 0x78, 0x74, 0x20, 0x77, 0x69,
            0x74, 0x68, 0x20, 0x73, 0x70, 0x65, 0x63, 0x69, 0x61, 0x6C, 0x20, 0x63, 0x68, 0x61, 0x72, 0x73, 0x20, 0x21, 0x40, 0x23, 0x24, 0x25, 0x5E, 0x26, 0x2A, 0x28, 0x29, 0x29, 0x20, 0x54, 0x6A,
            0x0A, 0x45, 0x54, 0x0A,
        };

        IPdfOperation fontOperation = new FontOperation("F4", 13);
        IPdfOperation moveTextOperation = new MoveTextOperation(2, 72);
        IPdfOperation showTextOperation = new ShowTextOperation("A text with special chars !@#$%^&*()");

        ITextObject textObject = new TextObject(new[] { fontOperation, moveTextOperation, showTextOperation });

        // Act
        byte[] actualBytes = textObject.Bytes.ToArray();

        // Assert
        Assert.Equal(expectedBytes, actualBytes);
    }

    [Fact(DisplayName = "The GetHashCode method should return a valid value")]
    public void TextObject_GetHashCode_ShouldReturnValidValue()
    {
        // Arrange
        IPdfOperation fontOperation = new FontOperation("F4", 13);
        IPdfOperation moveTextOperation = new MoveTextOperation(2, 72);
        IPdfOperation showTextOperation = new ShowTextOperation("A text with special chars !@#$%^&*()");

        ITextObject textObject = new TextObject(new[] { fontOperation, moveTextOperation, showTextOperation });

        int expectedHashCode = HashCode.Combine(nameof(TextObject), textObject.Value);

        // Act
        int actualHashCode = textObject.GetHashCode();

        // Assert
        Assert.Equal(expectedHashCode, actualHashCode);
    }

    [Fact(DisplayName = "The Equals property should return true")]
    public void TextObject_Equals_ShouldReturnTrue()
    {
        // Arrange
        IPdfOperation[] pdfOperations = { new FontOperation("F4", 13), new MoveTextOperation(2, 72), new ShowTextOperation("A text with special chars !@#$%^&*()") };
        ITextObject textObject1 = new TextObject(pdfOperations);
        ITextObject textObject2 = new TextObject(pdfOperations);

        // Act
        bool actualResult = textObject1.Equals(textObject2);

        // Assert
        Assert.True(actualResult);
    }

    [Fact(DisplayName = "The Equals property should return false")]
    public void TextObject_Equals_ShouldReturnFalse()
    {
        // Arrange
        ITextObject textObject1 = new TextObject(new IPdfOperation[] { new FontOperation("F4", 13), new MoveTextOperation(2, 72), new ShowTextOperation("A text with special chars !@#$%^&*()") });
        ITextObject textObject2 = new TextObject(new IPdfOperation[] { new FontOperation("F4", 13), new MoveTextOperation(2, 72), new ShowTextOperation("A text with special chars !@#$%^&*()") });

        // Act
        bool actualResult = textObject1.Equals(textObject2);

        // Assert
        Assert.False(actualResult);
    }
}

// <copyright file="InputReaderTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Text;
using OffDotNet.Pdf.CodeAnalysis.Parser;
using Xunit;

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Parser;

public class InputReaderTests
{
    [Fact(DisplayName = $"The {nameof(InputReader.Peek)} method should return first character")]
    public void InputReader_Peek_ShouldReturnFirstCharacter()
    {
        // Arrange
        byte[] inputBytes = Encoding.ASCII.GetBytes("Hello, OffDotNet!");
        InputReader inputReader = new(inputBytes);

        // Act
        byte? nextByte = inputReader.Peek();

        // Assert
        Assert.Equal('H', (char)nextByte!);
    }

    [Fact(DisplayName = $"The {nameof(InputReader.Peek)} method accessed multiple times should return same character")]
    public void InputReader_PeekMultipleTimes_ShouldReturnSameCharacter()
    {
        // Arrange
        byte[] inputBytes = Encoding.ASCII.GetBytes("Hello, OffDotNet!");
        InputReader inputReader = new(inputBytes);

        // Act
        _ = inputReader.Peek();
        byte? nextByte = inputReader.Peek();

        // Assert
        Assert.Equal('H', (char)nextByte!);
    }

    [Fact(DisplayName = $"The {nameof(InputReader.Peek)} method with out of range offset should return false")]
    public void InputReader_PeekWithOutOfRangeOffset_ShouldReturnFalse()
    {
        // Arrange
        byte[] inputBytes = Encoding.ASCII.GetBytes(string.Empty);
        InputReader inputReader = new(inputBytes);

        // Act
        byte? nextByte = inputReader.Peek();

        // Assert
        Assert.Null(nextByte);
    }

    [Fact(DisplayName = $"The {nameof(InputReader.Peek)} method should return first character")]
    public void InputReader_PeekWithDelta_ShouldReturnNthCharacter()
    {
        // Arrange
        byte[] inputBytes = Encoding.ASCII.GetBytes("Hello!");
        InputReader inputReader = new(inputBytes);

        // Act
        byte? nextByte1 = inputReader.Peek(0);
        byte? nextByte2 = inputReader.Peek(1);
        byte? nextByte3 = inputReader.Peek(2);
        byte? nextByte4 = inputReader.Peek(3);
        byte? nextByte5 = inputReader.Peek(4);
        byte? nextByte6 = inputReader.Peek(5);

        // Assert
        Assert.Equal('H', (char)nextByte1!);
        Assert.Equal('e', (char)nextByte2!);
        Assert.Equal('l', (char)nextByte3!);
        Assert.Equal('l', (char)nextByte4!);
        Assert.Equal('o', (char)nextByte5!);
        Assert.Equal('!', (char)nextByte6!);
    }

    [Fact(DisplayName = $"The {nameof(InputReader.Peek)} method with out of range delta should return false")]
    public void InputReader_PeekWithOutOfRangeDelta_ShouldReturnFalse()
    {
        // Arrange
        byte[] inputBytes = Encoding.ASCII.GetBytes("Hello!");
        InputReader inputReader = new(inputBytes);

        // Act
        byte? nextByte = inputReader.Peek(6);

        // Assert
        Assert.Null(nextByte);
    }

    [Fact(DisplayName = $"The {nameof(InputReader.Peek)} method with negative delta should return false")]
    public void InputReader_PeekWithNegativeDelta_ShouldReturnFalse()
    {
        // Arrange
        byte[] inputBytes = Encoding.ASCII.GetBytes("Hello!");
        InputReader inputReader = new(inputBytes);

        // Act
        byte? nextByte = inputReader.Peek(-1);

        // Assert
        Assert.Null(nextByte);
    }

    [Fact(DisplayName = $"The {nameof(InputReader.Consume)} method should return the next character and advance the offset")]
    public void InputReader_Consume_ShouldReturnNextCharacterAndAdvanceOffset()
    {
        // Arrange
        byte[] inputBytes = Encoding.ASCII.GetBytes("Hello!");
        InputReader inputReader = new(inputBytes);

        // Act
        byte? nextByte1 = inputReader.Consume();
        byte? nextByte2 = inputReader.Consume();
        byte? nextByte3 = inputReader.Consume();
        byte? nextByte4 = inputReader.Consume();
        byte? nextByte5 = inputReader.Consume();
        byte? nextByte6 = inputReader.Consume();

        // Assert
        Assert.Equal('H', (char)nextByte1!);
        Assert.Equal('e', (char)nextByte2!);
        Assert.Equal('l', (char)nextByte3!);
        Assert.Equal('l', (char)nextByte4!);
        Assert.Equal('o', (char)nextByte5!);
        Assert.Equal('!', (char)nextByte6!);
    }

    [Fact(DisplayName = $"The {nameof(InputReader.Consume)} method with out of range offset should return false")]
    public void InputReader_ConsumeWithOutOfRangeOffset_ShouldReturnNextCharacterAndAdvanceOffset()
    {
        // Arrange
        byte[] inputBytes = Encoding.ASCII.GetBytes("H");
        InputReader inputReader = new(inputBytes);

        // Act
        byte? nextByte1 = inputReader.Consume();
        byte? nextByte2 = inputReader.Consume();

        // Assert
        Assert.Equal('H', (char)nextByte1!);
        Assert.Null(nextByte2);
    }

    [Fact(DisplayName = $"The {nameof(InputReader.AdvanceByte)} method should move the offset")]
    public void InputReader_AdvanceByte_ShouldMoveOffset()
    {
        // Arrange
        byte[] inputBytes = Encoding.ASCII.GetBytes("Hello, OffDotNet!");
        InputReader inputReader = new(inputBytes);

        // Act
        inputReader.AdvanceByte(4);
        byte? nextByte = inputReader.Peek();

        // Assert
        Assert.Equal('o', (char)nextByte!);
    }
}

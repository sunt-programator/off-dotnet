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
    [Fact(DisplayName = $"The {nameof(SlidingTextWindow.PeekByte)} method should return first character")]
    public void InputReader_Peek_ShouldReturnFirstCharacter()
    {
        // Arrange
        byte[] inputBytes = Encoding.ASCII.GetBytes("Hello, OffDotNet!");
        SlidingTextWindow textWindow = new(inputBytes);

        // Act
        byte? nextByte = textWindow.PeekByte();

        // Assert
        Assert.Equal('H', (char)nextByte!);
    }

    [Fact(DisplayName = $"The {nameof(SlidingTextWindow.PeekByte)} method accessed multiple times should return same character")]
    public void InputReader_PeekMultipleTimes_ShouldReturnSameCharacter()
    {
        // Arrange
        byte[] inputBytes = Encoding.ASCII.GetBytes("Hello, OffDotNet!");
        SlidingTextWindow textWindow = new(inputBytes);

        // Act
        _ = textWindow.PeekByte();
        byte? nextByte = textWindow.PeekByte();

        // Assert
        Assert.Equal('H', (char)nextByte!);
    }

    [Fact(DisplayName = $"The {nameof(SlidingTextWindow.PeekByte)} method with out of range offset should return false")]
    public void InputReader_PeekWithOutOfRangeOffset_ShouldReturnFalse()
    {
        // Arrange
        byte[] inputBytes = Encoding.ASCII.GetBytes(string.Empty);
        SlidingTextWindow textWindow = new(inputBytes);

        // Act
        byte? nextByte = textWindow.PeekByte();

        // Assert
        Assert.Null(nextByte);
    }

    [Fact(DisplayName = $"The {nameof(SlidingTextWindow.PeekByte)} method should return first character")]
    public void InputReader_PeekWithDelta_ShouldReturnNthCharacter()
    {
        // Arrange
        byte[] inputBytes = Encoding.ASCII.GetBytes("Hello!");
        SlidingTextWindow textWindow = new(inputBytes);

        // Act
        byte? nextByte1 = textWindow.PeekByte(0);
        byte? nextByte2 = textWindow.PeekByte(1);
        byte? nextByte3 = textWindow.PeekByte(2);
        byte? nextByte4 = textWindow.PeekByte(3);
        byte? nextByte5 = textWindow.PeekByte(4);
        byte? nextByte6 = textWindow.PeekByte(5);

        // Assert
        Assert.Equal('H', (char)nextByte1!);
        Assert.Equal('e', (char)nextByte2!);
        Assert.Equal('l', (char)nextByte3!);
        Assert.Equal('l', (char)nextByte4!);
        Assert.Equal('o', (char)nextByte5!);
        Assert.Equal('!', (char)nextByte6!);
    }

    [Fact(DisplayName = $"The {nameof(SlidingTextWindow.PeekByte)} method with out of range delta should return false")]
    public void InputReader_PeekWithOutOfRangeDelta_ShouldReturnFalse()
    {
        // Arrange
        byte[] inputBytes = Encoding.ASCII.GetBytes("Hello!");
        SlidingTextWindow textWindow = new(inputBytes);

        // Act
        byte? nextByte = textWindow.PeekByte(6);

        // Assert
        Assert.Null(nextByte);
    }

    [Fact(DisplayName = $"The {nameof(SlidingTextWindow.PeekByte)} method with negative delta should return false")]
    public void InputReader_PeekWithNegativeDelta_ShouldReturnFalse()
    {
        // Arrange
        byte[] inputBytes = Encoding.ASCII.GetBytes("Hello!");
        SlidingTextWindow textWindow = new(inputBytes);

        // Act
        byte? nextByte = textWindow.PeekByte(-1);

        // Assert
        Assert.Null(nextByte);
    }

    [Fact(DisplayName = $"The {nameof(SlidingTextWindow.NextByte)} method should return the next character and advance the offset")]
    public void InputReader_Consume_ShouldReturnNextCharacterAndAdvanceOffset()
    {
        // Arrange
        byte[] inputBytes = Encoding.ASCII.GetBytes("Hello!");
        SlidingTextWindow textWindow = new(inputBytes);

        // Act
        byte? nextByte1 = textWindow.NextByte();
        byte? nextByte2 = textWindow.NextByte();
        byte? nextByte3 = textWindow.NextByte();
        byte? nextByte4 = textWindow.NextByte();
        byte? nextByte5 = textWindow.NextByte();
        byte? nextByte6 = textWindow.NextByte();

        // Assert
        Assert.Equal('H', (char)nextByte1!);
        Assert.Equal('e', (char)nextByte2!);
        Assert.Equal('l', (char)nextByte3!);
        Assert.Equal('l', (char)nextByte4!);
        Assert.Equal('o', (char)nextByte5!);
        Assert.Equal('!', (char)nextByte6!);
    }

    [Fact(DisplayName = $"The {nameof(SlidingTextWindow.NextByte)} method with out of range offset should return false")]
    public void InputReader_ConsumeWithOutOfRangeOffset_ShouldReturnNextCharacterAndAdvanceOffset()
    {
        // Arrange
        byte[] inputBytes = Encoding.ASCII.GetBytes("H");
        SlidingTextWindow textWindow = new(inputBytes);

        // Act
        byte? nextByte1 = textWindow.NextByte();
        byte? nextByte2 = textWindow.NextByte();

        // Assert
        Assert.Equal('H', (char)nextByte1!);
        Assert.Null(nextByte2);
    }

    [Fact(DisplayName = $"The {nameof(SlidingTextWindow.AdvanceByte)} method should move the offset")]
    public void InputReader_AdvanceByte_ShouldMoveOffset()
    {
        // Arrange
        byte[] inputBytes = Encoding.ASCII.GetBytes("Hello, OffDotNet!");
        SlidingTextWindow textWindow = new(inputBytes);

        // Act
        textWindow.AdvanceByte(4);
        byte? nextByte = textWindow.PeekByte();

        // Assert
        Assert.Equal('o', (char)nextByte!);
    }
}

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
    [Fact(DisplayName = $"The {nameof(InputReader.TryPeek)} method should return first character")]
    public void InputReader_TryPeek_ShouldReturnFirstCharacter()
    {
        // Arrange
        byte[] inputBytes = Encoding.ASCII.GetBytes("Hello, OffDotNet!");
        InputReader inputReader = new(inputBytes);

        // Act
        bool canPeek = inputReader.TryPeek(out byte? nextByte);

        // Assert
        Assert.True(canPeek);
        Assert.Equal('H', (char)nextByte!);
    }

    [Fact(DisplayName = $"The {nameof(InputReader.TryPeek)} method accessed multiple times should return same character")]
    public void InputReader_TryPeekMultipleTimes_ShouldReturnSameCharacter()
    {
        // Arrange
        byte[] inputBytes = Encoding.ASCII.GetBytes("Hello, OffDotNet!");
        InputReader inputReader = new(inputBytes);

        // Act
        _ = inputReader.TryPeek(out byte? _);
        bool canPeek = inputReader.TryPeek(out byte? nextByte);

        // Assert
        Assert.True(canPeek);
        Assert.Equal('H', (char)nextByte!);
    }

    [Fact(DisplayName = $"The {nameof(InputReader.TryPeek)} method with out of range offset should return false")]
    public void InputReader_TryPeekWithOutOfRangeOffset_ShouldReturnFalse()
    {
        // Arrange
        byte[] inputBytes = Encoding.ASCII.GetBytes(string.Empty);
        InputReader inputReader = new(inputBytes);

        // Act
        bool canPeek = inputReader.TryPeek(out byte? _);

        // Assert
        Assert.False(canPeek);
    }

    [Fact(DisplayName = $"The {nameof(InputReader.TryPeek)} method should return first character")]
    public void InputReader_TryPeekWithDelta_ShouldReturnNthCharacter()
    {
        // Arrange
        byte[] inputBytes = Encoding.ASCII.GetBytes("Hello!");
        InputReader inputReader = new(inputBytes);

        // Act
        bool canPeek1 = inputReader.TryPeek(0, out byte? nextByte1);
        bool canPeek2 = inputReader.TryPeek(1, out byte? nextByte2);
        bool canPeek3 = inputReader.TryPeek(2, out byte? nextByte3);
        bool canPeek4 = inputReader.TryPeek(3, out byte? nextByte4);
        bool canPeek5 = inputReader.TryPeek(4, out byte? nextByte5);
        bool canPeek6 = inputReader.TryPeek(5, out byte? nextByte6);

        // Assert
        Assert.True(canPeek1);
        Assert.True(canPeek2);
        Assert.True(canPeek3);
        Assert.True(canPeek4);
        Assert.True(canPeek5);
        Assert.True(canPeek6);
        Assert.Equal('H', (char)nextByte1!);
        Assert.Equal('e', (char)nextByte2!);
        Assert.Equal('l', (char)nextByte3!);
        Assert.Equal('l', (char)nextByte4!);
        Assert.Equal('o', (char)nextByte5!);
        Assert.Equal('!', (char)nextByte6!);
    }

    [Fact(DisplayName = $"The {nameof(InputReader.TryPeek)} method with out of range delta should return false")]
    public void InputReader_TryPeekWithOutOfRangeDelta_ShouldReturnFalse()
    {
        // Arrange
        byte[] inputBytes = Encoding.ASCII.GetBytes("Hello!");
        InputReader inputReader = new(inputBytes);

        // Act
        bool canPeek = inputReader.TryPeek(6, out byte? _);

        // Assert
        Assert.False(canPeek);
    }

    [Fact(DisplayName = $"The {nameof(InputReader.TryPeek)} method with negative delta should return false")]
    public void InputReader_PeekWithNegativeDelta_ShouldReturnFalse()
    {
        // Arrange
        byte[] inputBytes = Encoding.ASCII.GetBytes("Hello!");
        InputReader inputReader = new(inputBytes);

        // Act
        bool canPeek = inputReader.TryPeek(-1, out byte? _);

        // Assert
        Assert.False(canPeek);
    }

    [Fact(DisplayName = $"The {nameof(InputReader.TryConsume)} method should return the next character and advance the offset")]
    public void InputReader_TryConsume_ShouldReturnNextCharacterAndAdvanceOffset()
    {
        // Arrange
        byte[] inputBytes = Encoding.ASCII.GetBytes("Hello!");
        InputReader inputReader = new(inputBytes);

        // Act
        bool canConsume1 = inputReader.TryConsume(out byte? nextByte1);
        bool canConsume2 = inputReader.TryConsume(out byte? nextByte2);
        bool canConsume3 = inputReader.TryConsume(out byte? nextByte3);
        bool canConsume4 = inputReader.TryConsume(out byte? nextByte4);
        bool canConsume5 = inputReader.TryConsume(out byte? nextByte5);
        bool canConsume6 = inputReader.TryConsume(out byte? nextByte6);

        // Assert
        Assert.True(canConsume1);
        Assert.True(canConsume2);
        Assert.True(canConsume3);
        Assert.True(canConsume4);
        Assert.True(canConsume5);
        Assert.True(canConsume6);
        Assert.Equal('H', (char)nextByte1!);
        Assert.Equal('e', (char)nextByte2!);
        Assert.Equal('l', (char)nextByte3!);
        Assert.Equal('l', (char)nextByte4!);
        Assert.Equal('o', (char)nextByte5!);
        Assert.Equal('!', (char)nextByte6!);
    }

    [Fact(DisplayName = $"The {nameof(InputReader.TryConsume)} method with out of range offset should return false")]
    public void InputReader_TryConsumeWithOutOfRangeOffset_ShouldReturnNextCharacterAndAdvanceOffset()
    {
        // Arrange
        byte[] inputBytes = Encoding.ASCII.GetBytes("H");
        InputReader inputReader = new(inputBytes);

        // Act
        bool canConsume1 = inputReader.TryConsume(out byte? nextByte1);
        bool canConsume2 = inputReader.TryConsume(out byte? _);

        // Assert
        Assert.True(canConsume1);
        Assert.False(canConsume2);
        Assert.Equal('H', (char)nextByte1!);
    }

    [Fact(DisplayName = $"The {nameof(InputReader.AdvanceByte)} method should move the offset")]
    public void InputReader_AdvanceByte_ShouldMoveOffset()
    {
        // Arrange
        byte[] inputBytes = Encoding.ASCII.GetBytes("Hello, OffDotNet!");
        InputReader inputReader = new(inputBytes);

        // Act
        inputReader.AdvanceByte(4);
        bool canPeek = inputReader.TryPeek(out byte? nextByte);

        // Assert
        Assert.True(canPeek);
        Assert.Equal('o', (char)nextByte!);
    }
}

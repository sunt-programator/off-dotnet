// <copyright file="TextCursorTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Tests.Lexer;

using OffDotNet.CodeAnalysis.Lexer;
using OffDotNet.CodeAnalysis.Utils;

[WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
public class TextCursorTests
{
    private readonly ISourceText _sourceText = Substitute.For<ISourceText>();

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"Class should implement {nameof(IDisposable)} interface")]
    public void Class_ShouldImplementIDisposableInterface()
    {
        // Arrange

        // Act
        var cursor = new TextCursor(_sourceText);

        // Assert
        Assert.IsAssignableFrom<IDisposable>(cursor);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"Class should implement {nameof(ITextCursor)} interface")]
    public void Class_ShouldImplementITextCursorInterface()
    {
        // Arrange

        // Act
        var cursor = new TextCursor(_sourceText);

        // Assert
        Assert.IsAssignableFrom<ITextCursor>(cursor);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = "Dispose() method should not throw any exception")]
    public void Dispose_ShouldNotThrowAnyException()
    {
        // Arrange
        var cursor = new TextCursor(_sourceText);

        // Act
        cursor.Dispose();

        // Assert
        Assert.True(true);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = "Constructor should copy the input text")]
    public void Constructor_WithReadOnlySpanByteParameter_ShouldCopyInputText()
    {
        // Arrange
        const int TextLength = 4;
        const byte Expected = (byte)'A';

        _sourceText[0].Returns(Expected);
        _sourceText.Length.Returns(TextLength);

        var cursor = new TextCursor(_sourceText);

        // Act
        var actual = cursor.Current;

        // Assert
        Assert.Equal(Expected, actual.GetValueOrDefault<byte>(0x0));
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(TextCursor.Current)} property should return none when the input string is empty")]
    public void Current_ShouldReturnNone_WhenInputStringIsEmpty()
    {
        // Arrange
        var expected = Option<byte>.None;
        var cursor = new TextCursor(_sourceText);

        // Act
        var current = cursor.Current;

        // Assert
        Assert.Equal(expected, current);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(TextCursor.Current)} property should return the first character of the input string")]
    public void Current_ShouldReturnFirstCharacterOfInputString()
    {
        // Arrange
        const int TextLength = 4;
        const byte Expected = (byte)'C';

        _sourceText[0].Returns(Expected);
        _sourceText.Length.Returns(TextLength);

        var cursor = new TextCursor(_sourceText);

        // Act
        var actual = cursor.Current;

        // Assert
        Assert.Equal(Expected, actual.GetValueOrDefault<byte>(0x0));
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(TextCursor.Current)} property should return none when the cursor is at the end of the input string")]
    public void Current_ShouldReturnNone_WhenCursorIsAtTheEnd()
    {
        // Arrange
        const int TextLength = 4;
        _sourceText.Length.Returns(TextLength);

        var cursor = new TextCursor(_sourceText);
        cursor.Advance(TextLength);

        // Act
        var current = cursor.Current;

        // Assert
        Assert.Equal(Option<byte>.None, current);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(TextCursor.IsAtEnd)} property should return true when the input string is empty")]
    public void IsAtEnd_ShouldReturnTrue_WhenInputStringIsEmpty()
    {
        // Arrange
        const bool Expected = true;
        var cursor = new TextCursor(_sourceText);

        // Act
        var isAtEnd = cursor.IsAtEnd;

        // Assert
        Assert.Equal(Expected, isAtEnd);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(TextCursor.IsAtEnd)} property should return false when the input string is not empty and the cursor is at the beginning")]
    public void IsAtEnd_ShouldReturnFalse_WhenInputStringIsNotEmpty_AndCursorIsAtTheBeginning()
    {
        // Arrange
        const bool Expected = false;
        const int TextLength = 4;

        _sourceText.Length.Returns(TextLength);

        var cursor = new TextCursor(_sourceText);

        // Act
        var isAtEnd = cursor.IsAtEnd;

        // Assert
        Assert.Equal(Expected, isAtEnd);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(TextCursor.IsAtEnd)} property should return true when the cursor is at the end of the input string")]
    public void IsAtEnd_ShouldReturnTrue_WhenCursorIsAtTheEnd()
    {
        // Arrange
        const bool Expected = true;
        const int TextLength = 4;

        _sourceText.Length.Returns(TextLength);

        var cursor = new TextCursor(_sourceText);
        cursor.Advance(TextLength);

        // Act
        var isAtEnd = cursor.IsAtEnd;

        // Assert
        Assert.Equal(Expected, isAtEnd);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(TextCursor.Length)} property should return the length of the input string")]
    public void Length_ShouldReturnLengthOfInputString()
    {
        // Arrange
        const int Expected = 4;

        _sourceText.Length.Returns(Expected);

        var cursor = new TextCursor(_sourceText);

        // Act
        var length = cursor.Length;

        // Assert
        Assert.Equal(Expected, length);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(TextCursor.Peek)} method should return none when the input string is empty")]
    public void Peek_ShouldReturnNone_WhenInputStringIsEmpty()
    {
        // Arrange
        var expected = Option<byte>.None;
        var cursor = new TextCursor(_sourceText);

        // Act
        var peek = cursor.Peek();

        // Assert
        Assert.Equal(expected, peek);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(TextCursor.Peek)} method should return the first character of the input string")]
    public void Peek_ShouldReturnFirstCharacterOfInputString()
    {
        // Arrange
        const int TextLength = 4;
        const byte Expected = (byte)'C';

        _sourceText[0].Returns(Expected);
        _sourceText.Length.Returns(TextLength);

        var cursor = new TextCursor(_sourceText);

        // Act
        var actual = cursor.Peek();

        // Assert
        Assert.Equal(Expected, actual.GetValueOrDefault<byte>(0x0));
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(TextCursor.Peek)} method should return nth character of the input string")]
    public void Peek_ShouldReturnNthCharacterOfInputString()
    {
        // Arrange
        const int Index = 2;
        const int TextLength = 4;
        const byte Expected = (byte)'E';

        _sourceText[Index].Returns(Expected);
        _sourceText.Length.Returns(TextLength);

        var cursor = new TextCursor(_sourceText);

        // Act
        var actual = cursor.Peek(Index);

        // Assert
        Assert.Equal(Expected, actual.GetValueOrDefault<byte>(0x0));
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(TextCursor.Peek)} method should return none when the cursor is at the end of the input string")]
    public void Peek_ShouldReturnNone_WhenCursorIsAtTheEnd()
    {
        // Arrange
        const int TextLength = 4;

        _sourceText.Length.Returns(TextLength);

        var cursor = new TextCursor(_sourceText);
        cursor.Advance(TextLength);

        // Act
        var peek = cursor.Peek();

        // Assert
        Assert.Equal(Option<byte>.None, peek);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(TextCursor.Advance)} method should advance the cursor by one position")]
    public void Advance_ShouldAdvanceCursorByOnePosition()
    {
        // Arrange
        const int TextLength = 4;
        const byte Expected = (byte)'D';

        _sourceText.Length.Returns(TextLength);
        _sourceText[1].Returns(Expected);

        var cursor = new TextCursor(_sourceText);

        // Act
        cursor.Advance();
        var actual = cursor.Current;

        // Assert
        Assert.Equal(Expected, actual.GetValueOrDefault<byte>(0x0));
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(TextCursor.Advance)} method with nth delta should advance the cursor by n positions")]
    public void Advance_ShouldAdvanceCursorByNPositions()
    {
        // Arrange
        const int TextLength = 4;
        const int Index = 2;
        const byte Expected = (byte)'E';

        _sourceText.Length.Returns(TextLength);
        _sourceText[Index].Returns(Expected);

        var cursor = new TextCursor(_sourceText);

        // Act
        cursor.Advance(Index);
        var actual = cursor.Current;

        // Assert
        Assert.Equal(Expected, actual.GetValueOrDefault<byte>(0x0));
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(TextCursor.Advance)} method with predicate should advance the cursor if the predicate is true")]
    public void Advance_WithPredicate_ShouldAdvanceCursor_IfPredicateIsTrue()
    {
        // Arrange
        const byte Expected = (byte)'E';
        var text = "CDEF"u8.ToArray();

        _sourceText.Length.Returns(text.Length);
        _sourceText[0].Returns(text[0]);
        _sourceText[1].Returns(text[1]);
        _sourceText[2].Returns(text[2]);
        _sourceText[3].Returns(text[3]);

        var cursor = new TextCursor(_sourceText);

        // Act
        cursor.Advance(static x => x is (byte)'C' or (byte)'D');
        var peek = cursor.Current;

        // Assert
        Assert.Equal(Expected, peek.GetValueOrDefault<byte>(0x0));
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(TextCursor.Advance)} method with predicate should not advance the cursor if the predicate is false")]
    public void Advance_WithPredicate_ShouldNotAdvanceCursor_IfPredicateIsFalse()
    {
        // Arrange
        const byte Expected = (byte)'C';
        var text = "CDEF"u8.ToArray();

        _sourceText.Length.Returns(text.Length);
        _sourceText[0].Returns(text[0]);
        _sourceText[1].Returns(text[1]);
        _sourceText[2].Returns(text[2]);
        _sourceText[3].Returns(text[3]);

        var cursor = new TextCursor(_sourceText);

        // Act
        cursor.Advance(static x => x is (byte)'A' or (byte)'B');
        var peek = cursor.Current;

        // Assert
        Assert.Equal(Expected, peek.GetValueOrDefault<byte>(0x0));
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(TextCursor.TryAdvance)} method should return false if the character at the current position is not the expected one")]
    public void TryAdvance_ShouldReturnFalse_IfCharacterAtCurrentPositionIsNotTheExpectedOne()
    {
        // Arrange
        const byte Expected = (byte)'C';
        var text = "CDEF"u8.ToArray();

        _sourceText.Length.Returns(text.Length);
        _sourceText[0].Returns(text[0]);
        _sourceText[1].Returns(text[1]);
        _sourceText[2].Returns(text[2]);
        _sourceText[3].Returns(text[3]);

        var cursor = new TextCursor(_sourceText);

        // Act
        var result = cursor.TryAdvance((byte)'A');
        var peek = cursor.Current;

        // Assert
        Assert.False(result);
        Assert.Equal(Expected, peek.GetValueOrDefault<byte>(0x0));
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(TextCursor.TryAdvance)} method should return true if the character at the current position is the expected one")]
    public void TryAdvance_ShouldReturnTrue_IfCharacterAtCurrentPositionIsTheExpectedOne()
    {
        // Arrange
        const byte Expected = (byte)'D';
        var text = "CDEF"u8.ToArray();

        _sourceText.Length.Returns(text.Length);
        _sourceText[0].Returns(text[0]);
        _sourceText[1].Returns(text[1]);
        _sourceText[2].Returns(text[2]);
        _sourceText[3].Returns(text[3]);

        var cursor = new TextCursor(_sourceText);

        // Act
        var result = cursor.TryAdvance((byte)'C');
        var peek = cursor.Current;

        // Assert
        Assert.True(result);
        Assert.Equal(Expected, peek.GetValueOrDefault<byte>(0x0));
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(TextCursor.TryAdvance)} method with empty subtext should return false")]
    public void TryAdvance_WithEmptySubText_ShouldReturnFalse()
    {
        // Arrange
        const byte Expected = (byte)'C';
        var text = "CDEF"u8.ToArray();

        _sourceText.Length.Returns(text.Length);
        _sourceText[0].Returns(text[0]);
        _sourceText[1].Returns(text[1]);
        _sourceText[2].Returns(text[2]);
        _sourceText[3].Returns(text[3]);

        var cursor = new TextCursor(_sourceText);

        // Act
        var result = cursor.TryAdvance(ReadOnlySpan<byte>.Empty);
        var peek = cursor.Current;

        // Assert
        Assert.False(result);
        Assert.Equal(Expected, peek.GetValueOrDefault<byte>(0x0));
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Theory(DisplayName = $"{nameof(TextCursor.TryAdvance)} method with subtext should return false if the subtext is not found")]
    [InlineData("AB")]
    [InlineData("CE")]
    [InlineData("EF")]
    [InlineData("CDF")]
    public void TryAdvance_WithSubText_ShouldReturnFalse_IfSubTextIsNotFound(string subtext)
    {
        // Arrange
        const byte Expected = (byte)'C';
        var text = "CDEF"u8.ToArray();

        _sourceText.Length.Returns(text.Length);
        _sourceText[0].Returns(text[0]);
        _sourceText[1].Returns(text[1]);
        _sourceText[2].Returns(text[2]);
        _sourceText[3].Returns(text[3]);

        var cursor = new TextCursor(_sourceText);

        // Act
        var result = cursor.TryAdvance(Encoding.ASCII.GetBytes(subtext));
        var peek = cursor.Current;

        // Assert
        Assert.False(result);
        Assert.Equal(Expected, peek.GetValueOrDefault<byte>(0x0));
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Theory(DisplayName = $"{nameof(TextCursor.TryAdvance)} method with subtext should return true if the subtext is found")]
    [InlineData("C", (byte)'D')]
    [InlineData("CD", (byte)'E')]
    [InlineData("CDE", (byte)'F')]
    [InlineData("CDEF", 0x0)]
    public void TryAdvance_WithSubText_ShouldReturnTrue_IfSubTextIsFound(string subtext, byte nextChar)
    {
        // Arrange
        var text = "CDEF"u8.ToArray();

        _sourceText.Length.Returns(text.Length);
        _sourceText[0].Returns(text[0]);
        _sourceText[1].Returns(text[1]);
        _sourceText[2].Returns(text[2]);
        _sourceText[3].Returns(text[3]);

        var cursor = new TextCursor(_sourceText);

        // Act
        var result = cursor.TryAdvance(Encoding.ASCII.GetBytes(subtext));
        var peek = cursor.Current;

        // Assert
        Assert.True(result);
        Assert.Equal(nextChar, peek.GetValueOrDefault<byte>(0x0));
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(TextCursor.SourceText)} property should return the source text")]
    public void SourceText_ShouldReturnSourceText()
    {
        // Arrange
        var cursor = new TextCursor(_sourceText);

        // Act
        var actual = cursor.SourceText;

        // Assert
        Assert.Same(_sourceText, actual);
    }
}

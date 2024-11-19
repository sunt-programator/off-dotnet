// <copyright file="TextCursorTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Tests.Lexer;

using Configs;
using Microsoft.Extensions.Options;
using OffDotNet.CodeAnalysis.Lexer;
using OffDotNet.CodeAnalysis.Utils;

[WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
public class TextCursorTests
{
    private readonly IOptions<TextCursorOptions> _options = Substitute.For<IOptions<TextCursorOptions>>();

    public TextCursorTests()
    {
        _options.Value.Returns(new TextCursorOptions { WindowSize = 2048 });
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"Class should implement {nameof(IDisposable)} interface")]
    public void Class_ShouldImplementIDisposableInterface()
    {
        // Arrange
        const string Text = "CDEF";
        var sourceText = GetSourceText(Text);

        // Act
        ITextCursor cursor = new TextCursor(sourceText, _options);

        // Assert
        Assert.IsAssignableFrom<IDisposable>(cursor);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"Class should implement {nameof(ITextCursor)} interface")]
    public void Class_ShouldImplementITextCursorInterface()
    {
        // Arrange
        const string Text = "CDEF";
        var sourceText = GetSourceText(Text);

        // Act
        ITextCursor cursor = new TextCursor(sourceText, _options);

        // Assert
        Assert.IsAssignableFrom<ITextCursor>(cursor);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = "Dispose() method should not throw any exception")]
    public void Dispose_ShouldNotThrowAnyException()
    {
        // Arrange
        const string Text = "CDEF";
        var sourceText = GetSourceText(Text);
        ITextCursor cursor = new TextCursor(sourceText, _options);

        // Act
        cursor.Dispose();

        // Assert
        Assert.True(true);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = "Constructor should copy the input text")]
    public void Constructor_ShouldCopyInputText()
    {
        // Arrange
        const string Text = "CDEF";
        const byte Expected = (byte)'C';

        var sourceText = GetSourceText(Text);
        ITextCursor cursor = new TextCursor(sourceText, _options);

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
        const string Text = "";
        var expected = Option<byte>.None;

        var sourceText = GetSourceText(Text);
        ITextCursor cursor = new TextCursor(sourceText, _options);

        // Act
        var current = cursor.Current;

        // Assert
        Assert.Equal(expected, current);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(TextCursor.Current)} property should return none when the cursor is at the end of the input string")]
    public void Current_ShouldReturnNone_WhenCursorIsAtTheEnd()
    {
        // Arrange
        const string Text = "CDEF";

        var sourceText = GetSourceText(Text);
        ITextCursor cursor = new TextCursor(sourceText, _options);
        cursor.Advance(Text.Length);

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
        const string Text = "";
        const bool Expected = true;

        var sourceText = GetSourceText(Text);
        ITextCursor cursor = new TextCursor(sourceText, _options);

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
        const string Text = "CDEF";
        const bool Expected = false;

        var sourceText = GetSourceText(Text);
        ITextCursor cursor = new TextCursor(sourceText, _options);

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
        const string Text = "CDEF";
        const bool Expected = true;

        var sourceText = GetSourceText(Text);
        ITextCursor cursor = new TextCursor(sourceText, _options);

        // Act
        cursor.Advance(Text.Length);
        var isAtEnd = cursor.IsAtEnd;

        // Assert
        Assert.Equal(Expected, isAtEnd);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(TextCursor.Peek)} method should return none when the input string is empty")]
    public void Peek_ShouldReturnNone_WhenInputStringIsEmpty()
    {
        // Arrange
        const string Text = "";
        var expected = Option<byte>.None;

        var sourceText = GetSourceText(Text);
        ITextCursor cursor = new TextCursor(sourceText, _options);

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
        const string Text = "CDEF";
        const byte Expected = (byte)'C';

        var sourceText = GetSourceText(Text);
        ITextCursor cursor = new TextCursor(sourceText, _options);

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
        const string Text = "CDEF";
        const int Index = 2;
        const byte Expected = (byte)'E';

        var sourceText = GetSourceText(Text);
        ITextCursor cursor = new TextCursor(sourceText, _options);

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
        const string Text = "CDEF";

        var sourceText = GetSourceText(Text);
        ITextCursor cursor = new TextCursor(sourceText, _options);

        // Act
        cursor.Advance(Text.Length);
        var peek = cursor.Peek();

        // Assert
        Assert.Equal(Option<byte>.None, peek);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(TextCursor.Advance)} method should advance the cursor by one position")]
    public void Advance_ShouldAdvanceCursorByOnePosition()
    {
        // Arrange
        const string Text = "CDEF";
        const byte Expected = (byte)'D';

        var sourceText = GetSourceText(Text);
        ITextCursor cursor = new TextCursor(sourceText, _options);

        // Act
        cursor.Peek();
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
        const string Text = "CDEF";
        const int Index = 2;
        const byte Expected = (byte)'E';

        var sourceText = GetSourceText(Text);
        ITextCursor cursor = new TextCursor(sourceText, _options);

        // Act
        cursor.Peek();
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
        const string Text = "CDEF";
        const byte Expected = (byte)'E';

        var sourceText = GetSourceText(Text);
        ITextCursor cursor = new TextCursor(sourceText, _options);

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
        const string Text = "CDEF";
        const byte Expected = (byte)'C';

        var sourceText = GetSourceText(Text);
        ITextCursor cursor = new TextCursor(sourceText, _options);

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
        const string Text = "CDEF";
        const byte Expected = (byte)'C';

        var sourceText = GetSourceText(Text);
        ITextCursor cursor = new TextCursor(sourceText, _options);

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
        const string Text = "CDEF";
        const byte Expected = (byte)'D';

        var sourceText = GetSourceText(Text);
        ITextCursor cursor = new TextCursor(sourceText, _options);

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
        const string Text = "CDEF";
        const byte Expected = (byte)'C';

        var sourceText = GetSourceText(Text);
        ITextCursor cursor = new TextCursor(sourceText, _options);

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
        const string Text = "CDEF";
        const byte Expected = (byte)'C';

        var sourceText = GetSourceText(Text);
        ITextCursor cursor = new TextCursor(sourceText, _options);

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
        const string Text = "CDEF";

        var sourceText = GetSourceText(Text);
        ITextCursor cursor = new TextCursor(sourceText, _options);

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
        const string Text = "CDEF";

        var sourceText = GetSourceText(Text);
        ITextCursor cursor = new TextCursor(sourceText, _options);

        // Act
        var actual = cursor.SourceText;

        // Assert
        Assert.Same(sourceText, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(TextCursor.WindowStart)} property should return 0 by default")]
    public void WindowStart_ShouldReturnZero_ByDefault()
    {
        // Arrange
        const string Text = "CDEF";

        var sourceText = GetSourceText(Text);
        ITextCursor cursor = new TextCursor(sourceText, _options);

        // Act
        var actual = cursor.WindowStart;

        // Assert
        Assert.Equal(0, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(TextCursor.Offset)} property should return 0 by default")]
    public void Offset_ShouldReturnZero_ByDefault()
    {
        // Arrange
        const string Text = "CDEF";

        var sourceText = GetSourceText(Text);
        ITextCursor cursor = new TextCursor(sourceText, _options);

        // Act
        var actual = cursor.Offset;

        // Assert
        Assert.Equal(0, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(TextCursor.Position)} property should return 0 by default")]
    public void Position_ShouldReturnZero_ByDefault()
    {
        // Arrange
        const string Text = "CDEF";

        var sourceText = GetSourceText(Text);
        ITextCursor cursor = new TextCursor(sourceText, _options);

        // Act
        var actual = cursor.Position;

        // Assert
        Assert.Equal(0, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(TextCursor.IsLexemeMode)} property should return false by default")]
    public void IsLexemeMode_ShouldReturnFalse_ByDefault()
    {
        // Arrange
        const string Text = "CDEF";

        var sourceText = GetSourceText(Text);
        ITextCursor cursor = new TextCursor(sourceText, _options);

        // Act
        var actual = cursor.IsLexemeMode;

        // Assert
        Assert.False(actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(TextCursor.LexemeStart)} property should return 0 by default")]
    public void LexemeStart_ShouldReturnZero_ByDefault()
    {
        // Arrange
        const string Text = "CDEF";

        var sourceText = GetSourceText(Text);
        ITextCursor cursor = new TextCursor(sourceText, _options);

        // Act
        var actual = cursor.LexemeStart;

        // Assert
        Assert.Equal(0, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(TextCursor.LexemePosition)} property should return 0 by default")]
    public void LexemePosition_ShouldReturnZero_ByDefault()
    {
        // Arrange
        const string Text = "CDEF";

        var sourceText = GetSourceText(Text);
        ITextCursor cursor = new TextCursor(sourceText, _options);

        // Act
        var actual = cursor.LexemePosition;

        // Assert
        Assert.Equal(0, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(TextCursor.LexemeWidth)} property should return 0 by default")]
    public void LexemeWidth_ShouldReturnZero_ByDefault()
    {
        // Arrange
        const string Text = "CDEF";

        var sourceText = GetSourceText(Text);
        ITextCursor cursor = new TextCursor(sourceText, _options);

        // Act
        var actual = cursor.LexemeWidth;

        // Assert
        Assert.Equal(0, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(TextCursor.WindowSize)} property should return 0 by default")]
    public void WindowSize_ShouldReturnZero_ByDefault()
    {
        // Arrange
        const string Text = "CDEF";

        var sourceText = GetSourceText(Text);
        ITextCursor cursor = new TextCursor(sourceText, _options);

        // Act
        var actual = cursor.WindowSize;

        // Assert
        Assert.Equal(0, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Theory(DisplayName = $"{nameof(TextCursor.WindowSize)} property should update when peeking a character")]
    [InlineData(2048, 24)]
    [InlineData(16, 16)]
    public void WindowSize_ShouldUpdate_WhenPeekingACharacter(int windowSize, int expectedWindowCount)
    {
        // Arrange
        const int TextLength = 24;

        _options.Value.Returns(new TextCursorOptions { WindowSize = windowSize });
        var text = new string('A', TextLength);

        var sourceText = GetSourceText(text);
        ITextCursor cursor = new TextCursor(sourceText, _options);

        // Act
        cursor.Peek();
        var actual = cursor.WindowSize;

        // Assert
        Assert.Equal(expectedWindowCount, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(TextCursor.Offset)} property should correspond to the number of characters advanced")]
    public void Offset_ShouldCorrespondToNumberOfCharactersAdvanced()
    {
        // Arrange
        const string Text = "CDEF";
        const int Expected = 3;

        var sourceText = GetSourceText(Text);
        ITextCursor cursor = new TextCursor(sourceText, _options);

        // Act
        cursor.Advance(3);
        var actual = cursor.Offset;

        // Assert
        Assert.Equal(Expected, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(TextCursor.Position)} property should correspond to the number of characters advanced")]
    public void Position_ShouldCorrespondToNumberOfCharactersAdvanced()
    {
        // Arrange
        const string Text = "CDEF";
        const int Expected = 3;

        var sourceText = GetSourceText(Text);
        ITextCursor cursor = new TextCursor(sourceText, _options);

        // Act
        cursor.Advance(3);
        var actual = cursor.Position;

        // Assert
        Assert.Equal(Expected, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(TextCursor.StartLexemeMode)} method should set the lexeme basis to the current offset")]
    public void StartLexemeMode_ShouldSetLexemeStartToCurrentOffset()
    {
        // Arrange
        const string Text = "CDEF";
        const int Expected = 3;

        var sourceText = GetSourceText(Text);
        ITextCursor cursor = new TextCursor(sourceText, _options);

        // Act
        cursor.Advance(3);
        cursor.StartLexemeMode();
        var actual = cursor.LexemeStart;

        // Assert
        Assert.Equal(Expected, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(TextCursor.StartLexemeMode)} method should set the {nameof(TextCursor.IsLexemeMode)} property to true")]
    public void StartLexemeMode_ShouldSetIsLexemeModeToTrue()
    {
        // Arrange
        const string Text = "CDEF";

        var sourceText = GetSourceText(Text);
        ITextCursor cursor = new TextCursor(sourceText, _options);

        // Act
        cursor.Advance(3);
        cursor.StartLexemeMode();
        var actual = cursor.IsLexemeMode;

        // Assert
        Assert.True(actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(TextCursor.StopLexemeMode)} method should set the {nameof(TextCursor.IsLexemeMode)} property to false")]
    public void StopLexemeMode_ShouldSetIsLexemeModeToFalse()
    {
        // Arrange
        const string Text = "CDEF";

        var sourceText = GetSourceText(Text);
        ITextCursor cursor = new TextCursor(sourceText, _options);

        // Act
        cursor.Advance(3);
        cursor.StartLexemeMode();
        cursor.StopLexemeMode();
        var actual = cursor.IsLexemeMode;

        // Assert
        Assert.False(actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(TextCursor.StopLexemeMode)} method should set the {nameof(TextCursor.LexemeStart)} property to 0")]
    public void StopLexemeMode_ShouldSetLexemeStartToZero()
    {
        // Arrange
        const string Text = "CDEF";
        const int Expected = 0;

        var sourceText = GetSourceText(Text);
        ITextCursor cursor = new TextCursor(sourceText, _options);

        // Act
        cursor.Advance(3);
        cursor.StartLexemeMode();
        cursor.StopLexemeMode();
        var actual = cursor.LexemeStart;

        // Assert
        Assert.Equal(Expected, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Theory(DisplayName = $"{nameof(TextCursor.SlideTextWindow)} method should slide the text window and compute the window size")]
    [InlineData(0, 4, 2048, 4)]
    [InlineData(1, 4, 2048, 3)]
    [InlineData(2, 4, 2048, 2)]
    [InlineData(3, 4, 2048, 1)]
    [InlineData(0, 256, 16, 16)]
    [InlineData(17, 256, 16, 16)]
    [InlineData(-1, 256, 16, 0)]
    public void SlideTextWindow_NotInitialized_ShouldComputeWindowSize(int windowStart, int sourceTextLength, int defaultWindowSize, int expectedWindowSize)
    {
        // Arrange
        var sourceText = GetSourceText(new string('A', sourceTextLength));
        _options.Value.Returns(new TextCursorOptions { WindowSize = defaultWindowSize });
        ITextCursor cursor = new TextCursor(sourceText, _options);

        // Act
        cursor.SlideTextWindow(windowStart);

        // Assert
        Assert.Equal(expectedWindowSize, cursor.WindowSize);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Theory(DisplayName = $"{nameof(TextCursor.SlideTextWindow)} method should slide the text window and compute the window start position")]
    [InlineData(0, 0)]
    [InlineData(15, 15)]
    [InlineData(-1, 0)]
    public void SlideTextWindow_NotInitialized_ShouldComputeWindowStart(int windowStart, int expectedWindowStart)
    {
        // Arrange
        const int WindowSize = 2048;
        const int TextLength = 16;

        var sourceText = GetSourceText(new string('A', TextLength));
        _options.Value.Returns(new TextCursorOptions { WindowSize = WindowSize });
        ITextCursor cursor = new TextCursor(sourceText, _options);

        // Act
        cursor.SlideTextWindow(windowStart);

        // Assert
        Assert.Equal(expectedWindowStart, cursor.WindowStart);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(TextCursor.SlideTextWindow)} method should shrink the window size")]
    public void SlideTextWindow_NotInitialized_ShouldShrinkWindowSize()
    {
        // Arrange
        const int TextLength = 256;
        const int WindowStart = 0;
        const int DefaultWindowSize = 2048;
        const int WindowSize = 48;
        const int ExpectedWindowSize = 64;

        var sourceText = GetSourceText(GenerateIncrementingString(TextLength));
        _options.Value.Returns(new TextCursorOptions { WindowSize = DefaultWindowSize });
        ITextCursor cursor = new TextCursor(sourceText, _options);

        // Act
        cursor.SlideTextWindow(WindowStart, WindowSize);
        var character = cursor.Peek(delta: 0);

        // Assert
        Assert.Equal(ExpectedWindowSize, cursor.WindowSize);
        Assert.Equal((byte)'a', character);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(TextCursor.SlideTextWindow)} method should extend the window size")]
    public void SlideTextWindow_NotInitialized_ShouldExtendWindowSize()
    {
        // Arrange
        const int TextLength = 256;
        const int WindowStart = 0;
        const int DefaultWindowSize = 16;
        const int WindowSize = 48;
        const int ExpectedWindowSize = 64;

        var sourceText = GetSourceText(GenerateIncrementingString(TextLength));
        _options.Value.Returns(new TextCursorOptions { WindowSize = DefaultWindowSize });
        ITextCursor cursor = new TextCursor(sourceText, _options);

        // Act
        cursor.SlideTextWindow(WindowStart, WindowSize);
        var character = cursor.Peek(delta: 0);

        // Assert
        Assert.Equal(ExpectedWindowSize, cursor.WindowSize);
        Assert.Equal((byte)'a', character);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(TextCursor.SlideTextWindow)} method should copy the data from old window when extending new window")]
    public void SlideTextWindow_WhenInitialized_WhenExtending_ShouldCopyOldWindowToNewWindow()
    {
        // Arrange
        const int TextLength = 256;
        const int WindowStart = 0;
        const int DefaultWindowSize = 16;
        const int WindowSize = 48;
        const int ExpectedWindowSize = 64;

        var sourceText = GetSourceText(GenerateIncrementingString(TextLength));
        _options.Value.Returns(new TextCursorOptions { WindowSize = DefaultWindowSize });
        ITextCursor cursor = new TextCursor(sourceText, _options);

        // Act
        cursor.SlideTextWindow(WindowStart);
        cursor.SlideTextWindow(WindowStart, WindowSize);

        // Assert
        Assert.Equal(ExpectedWindowSize, cursor.WindowSize);
        Assert.Equal(sourceText.Source[WindowStart..ExpectedWindowSize], cursor.Window[..ExpectedWindowSize]);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(TextCursor.SlideTextWindow)} method should copy the data from old window when shrinking new window")]
    public void SlideTextWindow_WhenInitialized_WhenShrinking_ShouldCopyOldWindowToNewWindow()
    {
        // Arrange
        const int TextLength = 256;
        const int WindowStart = 0;
        const int DefaultWindowSize = 128;
        const int WindowSize = 48;
        const int ExpectedWindowSize = 64;

        var sourceText = GetSourceText(GenerateIncrementingString(TextLength));
        _options.Value.Returns(new TextCursorOptions { WindowSize = DefaultWindowSize });
        ITextCursor cursor = new TextCursor(sourceText, _options);

        // Act
        cursor.SlideTextWindow(WindowStart);
        cursor.SlideTextWindow(WindowStart, WindowSize);

        // Assert
        Assert.Equal(ExpectedWindowSize, cursor.WindowSize);
        Assert.Equal(sourceText.Source[WindowStart..ExpectedWindowSize], cursor.Window[..ExpectedWindowSize]);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Theory(DisplayName = $"{nameof(TextCursor.SlideTextWindow)} method should adjust {nameof(TextCursor.Offset)} when sliding the window")]
    [InlineData(5, 0)]
    [InlineData(16, 0)]
    [InlineData(17, 0)]
    public void SlideTextWindow_WhenSliding_ShouldSetOffset(int advanceDelta, int expectedOffset)
    {
        // Arrange
        const int TextLength = 256;
        const int WindowStart = 0;
        const int DefaultWindowSize = 128;
        const int WindowSize = 16;

        var sourceText = GetSourceText(GenerateIncrementingString(TextLength));
        _options.Value.Returns(new TextCursorOptions { WindowSize = DefaultWindowSize });
        ITextCursor cursor = new TextCursor(sourceText, _options);

        // Act
        cursor.Peek();
        cursor.Advance(advanceDelta);
        cursor.SlideTextWindow(WindowStart, WindowSize);

        // Assert
        Assert.Equal(expectedOffset, cursor.Offset);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Theory(DisplayName = $"{nameof(TextCursor.SlideTextWindow)} method should adjust {nameof(TextCursor.LexemeStart)} when sliding the window")]
    [InlineData(5, 5, true)]
    [InlineData(16, 0, false)]
    [InlineData(17, 0, false)]
    public void SlideTextWindow_WhenSliding_ShouldSetLexemeStartToZero(int advanceDelta, int expectedLexemeStart, bool expectedLexemeMode)
    {
        // Arrange
        const int TextLength = 256;
        const int WindowStart = 0;
        const int DefaultWindowSize = 128;
        const int WindowSize = 16;

        var sourceText = GetSourceText(GenerateIncrementingString(TextLength));
        _options.Value.Returns(new TextCursorOptions { WindowSize = DefaultWindowSize });
        ITextCursor cursor = new TextCursor(sourceText, _options);

        // Act
        cursor.Peek();
        cursor.Advance(advanceDelta);
        cursor.StartLexemeMode();
        cursor.SlideTextWindow(WindowStart, WindowSize);

        // Assert
        Assert.Equal(expectedLexemeStart, cursor.LexemeStart);
        Assert.Equal(expectedLexemeMode, cursor.IsLexemeMode);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(TextCursor.SlideTextWindow)} method should not slide the window if the start position and size are the same")]
    public void SlideTextWindow_WhenStartAndSizeAreTheSame_ShouldNotSlideWindow()
    {
        // Arrange
        const int TextLength = 256;
        const int WindowStart = 0;
        const int DefaultWindowSize = 128;
        const int WindowSize = 128;

        var sourceText = GetSourceText(GenerateIncrementingString(TextLength));
        _options.Value.Returns(new TextCursorOptions { WindowSize = DefaultWindowSize });
        ITextCursor cursor = new TextCursor(sourceText, _options);

        // Act
        cursor.SlideTextWindow(WindowStart, WindowSize);
        cursor.SlideTextWindow();

        // Assert
        Assert.Equal(DefaultWindowSize, cursor.WindowSize);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Theory(DisplayName = $"{nameof(TextCursor.Peek)} method should restore the {nameof(TextCursor.WindowStart)} if the delta is outside window")]
    [InlineData(16, (byte)'s')]
    [InlineData(17, (byte)'t')]
    [InlineData(18, (byte)'u')]
    [InlineData(19, (byte)'v')]
    public void Peek_CustomWindowStart_WhenDeltaIsOutsideWindow_ShouldRestoreWindowStart(int peekDelta, byte expectedByte)
    {
        // Arrange
        const int TextLength = 256;
        const int WindowStart = 2;
        const int DefaultWindowSize = 16;

        var sourceText = GetSourceText(GenerateIncrementingString(TextLength));
        _options.Value.Returns(new TextCursorOptions { WindowSize = DefaultWindowSize });
        ITextCursor cursor = new TextCursor(sourceText, _options);

        // Act
        cursor.SlideTextWindow(WindowStart);
        var actual = cursor.Peek(peekDelta);

        // Assert
        Assert.Equal(WindowStart, cursor.WindowStart);
        Assert.Equal(expectedByte, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(TextCursor.WindowSize)} property should be increased " +
                        $"when {nameof(TextCursor.IsLexemeMode)} is true and {nameof(TextCursor.Offset)} is outside the window")]
    public void WindowSize_WhenLexemeModeIsTrue_WhenOffsetIsOutsideWindow_ShouldIncreaseWindowSize()
    {
        // Arrange
        const int TextLength = 256;
        const int DefaultWindowSize = 16;
        const int ExpectedWindowSize = 32;
        const byte ExpectedByte = (byte)'q';

        var sourceText = GetSourceText(GenerateIncrementingString(TextLength));
        _options.Value.Returns(new TextCursorOptions { WindowSize = DefaultWindowSize });
        ITextCursor cursor = new TextCursor(sourceText, _options);

        // Act
        cursor.StartLexemeMode();
        var actual = cursor.Peek(DefaultWindowSize);

        // Assert
        Assert.True(cursor.IsLexemeMode);
        Assert.Equal(ExpectedWindowSize, cursor.WindowSize);
        Assert.Equal(ExpectedByte, actual);
    }

    private static ISourceText GetSourceText(string text) => new StringText(Encoding.ASCII.GetBytes(text));

    private static string GenerateIncrementingString(int n)
    {
        const char StartChar = 'a';
        var result = new char[n];

        for (var i = 0; i < n; i++)
        {
            result[i] = (char)(StartChar + i);
        }

        return new string(result);
    }
}

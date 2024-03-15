// <copyright file="SlidingTextWindowTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Parser;

using OffDotNet.Pdf.CodeAnalysis.Parser;
using OffDotNet.Pdf.CodeAnalysis.Text;

public sealed class SlidingTextWindowTests : IDisposable
{
    private readonly byte[] _text;
    private readonly SourceText _sourceText;
    private readonly SlidingTextWindow _sut;

    public SlidingTextWindowTests()
    {
        _text = "ABCDEFGH \rIJK\nLMNO\r\nPQRS\nTUVW\r\nXYZ\n"u8.ToArray();
        _sourceText = SourceText.From(_text);
        _sut = new SlidingTextWindow(_sourceText);
    }

    [Fact(DisplayName = $"The class must implement the {nameof(IDisposable)} interface")]
    public void Class_MustImplementIDisposable()
    {
        // Arrange

        // Act

        // Assert
        Assert.IsAssignableFrom<IDisposable>(_sut);
    }

    [Fact(DisplayName =
        $"The {nameof(SlidingTextWindow.Text)} property must return the {nameof(SourceText)} instance passed to the constructor")]
    public void TextProperty_MustReturnTheSourceTextInstancePassedToTheConstructor()
    {
        // Arrange

        // Act
        var result = _sut.Text;

        // Assert
        Assert.Same(_sourceText, result);
    }

    [Fact(DisplayName = $"The {nameof(SlidingTextWindow.Offset)} property must return 0 when the instance is created")]
    public void OffsetProperty_MustReturn0_WhenTheInstanceIsCreated()
    {
        // Arrange

        // Act
        var result = _sut.Offset;

        // Assert
        Assert.Equal(0, result);
    }

    [Fact(DisplayName =
        $"The {nameof(SlidingTextWindow.Position)} property must return 0 when the instance is created")]
    public void PositionProperty_MustReturn0_WhenTheInstanceIsCreated()
    {
        // Arrange

        // Act
        var result = _sut.Position;

        // Assert
        Assert.Equal(0, result);
    }

    [Fact(DisplayName =
        $"The {nameof(SlidingTextWindow.Basis)} property must return 0 when the instance is created")]
    public void BasisProperty_MustReturn0_WhenTheInstanceIsCreated()
    {
        // Arrange

        // Act
        var result = _sut.Basis;

        // Assert
        Assert.Equal(0, result);
    }

    [Fact(DisplayName =
        $"The {nameof(SlidingTextWindow.LexemeBasis)} property must return 0 when the instance is created")]
    public void LexemeBasisProperty_MustReturn0_WhenTheInstanceIsCreated()
    {
        // Arrange

        // Act
        var result = _sut.LexemeBasis;

        // Assert
        Assert.Equal(0, result);
    }

    [Fact(DisplayName =
        $"The {nameof(SlidingTextWindow.LexemePosition)} property must return 0 when the instance is created")]
    public void LexemePositionProperty_MustReturn0_WhenTheInstanceIsCreated()
    {
        // Arrange

        // Act
        var result = _sut.LexemePosition;

        // Assert
        Assert.Equal(0, result);
    }

    [Fact(DisplayName =
        $"The {nameof(SlidingTextWindow.LexemeWidth)} property must return 0 when the instance is created")]
    public void LexemeWidthProperty_MustReturn0_WhenTheInstanceIsCreated()
    {
        // Arrange

        // Act
        var result = _sut.LexemeWidth;

        // Assert
        Assert.Equal(0, result);
    }

    [Fact(DisplayName =
        $"The {nameof(SlidingTextWindow.CharacterWindowCount)} property must return 0 when the instance is created")]
    public void CharacterWindowCountProperty_MustReturn0_WhenTheInstanceIsCreated()
    {
        // Arrange

        // Act
        var result = _sut.CharacterWindowCount;

        // Assert
        Assert.Equal(0, result);
    }

    [Fact(DisplayName =
        $"The AdvanceByte() method must increment the {nameof(SlidingTextWindow.Offset)} property by 1")]
    public void AdvanceByte_MustIncrementOffsetBy1()
    {
        // Arrange
        const int Expected = 1;

        // Act
        _sut.AdvanceByte();

        // Assert
        Assert.Equal(Expected, _sut.Offset);
    }

    [Fact(DisplayName =
        $"The AdvanceByte(int) method must increment the {nameof(SlidingTextWindow.Offset)} property by the specified value")]
    public void AdvanceByte_MustIncrementOffsetBySpecifiedValue()
    {
        // Arrange
        const int Expected = 5;

        // Act
        _sut.AdvanceByte(5);

        // Assert
        Assert.Equal(Expected, _sut.Offset);
    }

    [Fact(DisplayName = "The PeekByte() method must return the byte at the current position in the window")]
    public void PeekByte_MustReturnTheByteAtTheCurrentPositionInTheWindow()
    {
        // Arrange
        const byte Expected = 0x41;

        // Act
        var result1 = _sut.PeekByte();
        var result2 = _sut.PeekByte();
        var result3 = _sut.PeekByte();

        // Assert
        Assert.Equal(Expected, result1);
        Assert.Equal(Expected, result2);
        Assert.Equal(Expected, result3);
    }

    [Fact(DisplayName = "The PeekByte() method must return null when the window has no more bytes")]
    public void PeekByte_MustReturnNull_WhenTheWindowHasNoMoreBytes()
    {
        // Arrange
        _sut.AdvanceByte(_text.Length);

        // Act
        var result = _sut.PeekByte();

        // Assert
        Assert.Null(result);
    }

    [Fact(DisplayName = "The PeekByte(int) method must return the byte at the specified position in the window")]
    public void PeekByte_MustReturnTheByteAtTheSpecifiedPositionInTheWindow()
    {
        // Arrange
        const byte Expected1 = 0x45;
        const byte Expected2 = 0x46;
        const byte Expected3 = 0x47;

        // Act
        var result1 = _sut.PeekByte(4);
        var result2 = _sut.PeekByte(5);
        var result3 = _sut.PeekByte(6);

        // Assert
        Assert.Equal(Expected1, result1);
        Assert.Equal(Expected2, result2);
        Assert.Equal(Expected3, result3);
    }

    [Fact(DisplayName = "The PeekByte(int) method must return null when the window has no more bytes")]
    public void PeekByte_WithDelta_MustReturnNull_WhenTheWindowHasNoMoreBytes()
    {
        // Arrange
        _sut.AdvanceByte(_text.Length);

        // Act
        var result = _sut.PeekByte(1);

        // Assert
        Assert.Null(result);
        Assert.False(_sut.IsLexemeMode);
    }

    [Theory(DisplayName =
        "The TryAdvanceIfMatches(ReadOnlySpan<byte>) method must advance the window by the length of the specified string when the window contains the specified string")]
    [InlineData("ABCD", true)]
    [InlineData("BCD", false)]
    public void TryAdvanceIfMatches_MustReturnTrue_WhenTheWindowContainsTheSpecifiedString(
        string pattern,
        bool expected)
    {
        // Arrange
        var patternBytes = Encoding.UTF8.GetBytes(pattern);

        // Act
        var result = _sut.TryAdvanceIfMatches(patternBytes);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact(DisplayName =
        "The TryAdvanceIfMatches(byte) method must advance the window by the length of the specified string when the window contains the specified string")]
    public void TryAdvanceIfMatches_MustReturnTrue_WhenTheWindowContainsTheSpecifiedByte()
    {
        // Arrange
        const byte Pattern = (byte)'A';

        // Act
        var result = _sut.TryAdvanceIfMatches(Pattern);

        // Assert
        Assert.True(result);
        Assert.Equal(1, _sut.Offset);
    }

    [Fact(DisplayName =
        $"The StartParsingLexeme() method must set the {nameof(SlidingTextWindow.LexemeBasis)} property " +
        $"to the current {nameof(SlidingTextWindow.Offset)} value")]
    public void StartParsingLexeme_MustSetLexemeBasisToOffset()
    {
        // Arrange
        const int Offset = 3;

        // Act
        _sut.AdvanceByte(Offset);
        _sut.StartParsingLexeme();

        // Assert
        Assert.Equal(Offset, _sut.LexemeBasis);
        Assert.True(_sut.IsLexemeMode);
    }

    [Fact(DisplayName =
        $"The StopParsingLexeme() method must set the {nameof(SlidingTextWindow.LexemeBasis)} property to 0 " +
        $"and the {nameof(SlidingTextWindow.IsLexemeMode)} property to true")]
    public void StopParsingLexeme_MustResetLexemeBasisAndIsLexemeMode()
    {
        // Arrange
        const int Offset = 3;

        // Act
        _sut.AdvanceByte(Offset);
        _sut.StartParsingLexeme();
        _sut.StopParsingLexeme();

        // Assert
        Assert.Equal(0, _sut.LexemeBasis);
        Assert.False(_sut.IsLexemeMode);
    }

    [Fact(DisplayName = "The Reset(int) method must set the Offset property to the specified value")]
    public void Reset_MustSetOffsetToSpecifiedValue()
    {
        // Arrange
        const int Expected = 2;

        // Act
        _ = _sut.PeekByte();
        _sut.Reset(Expected);

        // Assert
        Assert.Equal(Expected, _sut.Offset);
    }

    [Theory(DisplayName = "The GetBytes(int, int, bool) method must return the text from the window")]
    [InlineData(2, 3, true, "CDE")]
    [InlineData(2, 3, false, "CDE")]
    [InlineData(2, 0, false, "")]
    [InlineData(8, 1, false, " ")]
    [InlineData(9, 1, false, "\r")]
    [InlineData(13, 1, false, "\n")]
    [InlineData(18, 2, false, "\r\n")]
    public void GetBytes_MustReturnTheTextFromTheWindow(int offset, int length, bool shouldIntern, string expected)
    {
        // Arrange

        // Act
        _ = _sut.PeekByte(offset);
        var result = _sut.GetBytes(offset, length, shouldIntern);

        // Assert
        Assert.Equal(expected, Encoding.UTF8.GetString(result));
    }

    [Theory(DisplayName = "The GetLexemeBytes() method must return the lexeme bytes from the window")]
    [InlineData(2, 3, true, "CDE")]
    [InlineData(2, 3, false, "CDE")]
    [InlineData(2, 0, false, "")]
    [InlineData(8, 1, false, " ")]
    [InlineData(9, 1, false, "\r")]
    [InlineData(13, 1, false, "\n")]
    [InlineData(18, 2, false, "\r\n")]
    public void GetLexemeBytes_MustReturnTheLexemeTextFromTheWindow(
        int offset,
        int length,
        bool shouldIntern,
        string expected)
    {
        // Arrange
        _sut.PeekByte();
        _sut.AdvanceByte(offset);
        _sut.StartParsingLexeme();
        _sut.AdvanceByte(length);

        // Act
        var result = _sut.GetLexemeBytes(shouldIntern);

        // Assert
        Assert.Equal(expected, Encoding.UTF8.GetString(result));
        Assert.True(_sut.IsLexemeMode);
    }

    [Fact(DisplayName =
        "The PickAndAdvanceByte() method must return the next byte in the window and advance the window by 1")]
    public void PickAndAdvanceByte_MustReturnTheNextByteInTheWindowAndAdvanceTheWindowBy1()
    {
        // Arrange
        const byte Expected1 = 0x41;
        const byte Expected2 = 0x42;
        const byte Expected3 = 0x43;

        // Act
        var result1 = _sut.PeekAndAdvanceByte();
        var result2 = _sut.PeekAndAdvanceByte();
        var result3 = _sut.PeekAndAdvanceByte();

        // Assert
        Assert.Equal(Expected1, result1);
        Assert.Equal(Expected2, result2);
        Assert.Equal(Expected3, result3);
    }

    [Fact(DisplayName =
        "The PickAndAdvanceByte() method must return null and not advance when window has no more bytes")]
    public void PickAndAdvanceByte_MustReturnNull_WhenTheWindowHasNoMoreBytes()
    {
        // Arrange
        _sut.AdvanceByte(_text.Length);

        // Act
        var result = _sut.PeekAndAdvanceByte();

        // Assert
        Assert.Null(result);
        Assert.Equal(_text.Length, _sut.Offset);
    }

    [Theory(DisplayName = "The IsAtEnd() method must return true when the window has no more bytes")]
    [InlineData(0, 0, false)]
    [InlineData(0, short.MaxValue, true)]
    [InlineData(short.MaxValue, short.MaxValue, true)]
    public void IsAtEnd_MustReturnTrue_WhenTheWindowHasNoMoreBytes(int basis, int offset, bool expected)
    {
        // Arrange
        _sut.PeekByte(basis);
        _sut.AdvanceByte(offset);

        // Act
        var result = _sut.IsAtEnd();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact(DisplayName =
        "The GetBytes(int, int, bool) method must intern the text when the shouldIntern parameter is true")]
    public void GetBytes_MustInternTheText_WhenTheShouldInternParameterIsTrue()
    {
        // Arrange
        const int Offset = 2;
        const int Length = 3;
        const bool ShouldIntern = true;
        const string ExpectedText = "CDE";

        // Act
        _ = _sut.PeekByte(Offset);
        var text1 = _sut.GetBytes(Offset, Length, ShouldIntern);
        var text2 = _sut.GetBytes(Offset, Length, ShouldIntern);

        // Assert
        Assert.True(text1.Overlaps(text2));
        Assert.Equal(ExpectedText, Encoding.UTF8.GetString(text1));
        Assert.Equal(ExpectedText, Encoding.UTF8.GetString(text2));
    }

    [Fact(DisplayName =
        "The TryAdvanceIfMatches(Func<byte, bool>) method must advance the window by the length of the matching sequence")]
    public void AdvanceIfMatches_MustReturnTrue_WhenTheWindowContainsTheSpecifiedSequence()
    {
        // Arrange

        // Act
        _sut.AdvanceIfMatches(b => b >= 'A' && b <= 'H');

        // Assert
        Assert.Equal(8, _sut.Offset);
    }

    [Fact(DisplayName = $"The {nameof(SlidingTextWindow.IsLexemeMode)} property must be set to false by default")]
    public void IsLexemeMode_MustBeSetToFalseByDefault()
    {
        // Arrange

        // Act
        var result = _sut.IsLexemeMode;

        // Assert
        Assert.False(result);
    }

    [Fact(DisplayName =
        $"The {nameof(SlidingTextWindow.IsLexemeMode)} property must be set to false if the window is reset")]
    public void IsLexemeModeProperty_MustBeSetToFalse_IfTheWindowIsReset()
    {
        // Arrange
        _sut.StartParsingLexeme();

        // Act
        _ = _sut.PeekByte(_text.Length);

        // Assert
        Assert.Equal(0, _sut.LexemeWidth);
        Assert.False(_sut.IsLexemeMode);
    }

    public void Dispose()
    {
        _sut.Dispose();
    }
}

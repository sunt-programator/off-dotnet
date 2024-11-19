// <copyright file="TextCursor.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Lexer;

using Configs;
using Microsoft.Extensions.Options;
using Utils;

/// <summary>Represents a text cursor for navigating and processing text data.</summary>
/// <example>
/// <code>
/// SLIDING                        position
/// WINDOW                            |
///                ------  size ------
///               |                   |
///             start   =======>>   offset
///               |                   |
///        -------------------------------
/// TXT:  | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
///        -------------------------------
///               |          |        |
///               |        start      |
///               |          |        |
///               - position | width -
/// LEXEME
/// SUB-WINDOW
/// </code>
/// </example>
internal sealed class TextCursor : ITextCursor
{
    private IMemoryOwner<byte> _characterWindowMemoryOwner;

    /// <summary>Initializes a new instance of the <see cref="TextCursor"/> class with a read-only byte span.</summary>
    /// <param name="text">The text represented as a read-only byte span.</param>
    /// <param name="options">The text cursor options.</param>
    public TextCursor(in ISourceText text, IOptions<TextCursorOptions> options)
    {
        _characterWindowMemoryOwner = MemoryPool<byte>.Shared.Rent(options.Value.WindowSize);

        SourceText = text;
        WindowStart = 0;
        Offset = 0;
        LexemeStart = 0;
    }

    /// <summary>Gets the source text.</summary>
    public ISourceText SourceText { get; }

    /// <summary>Gets the current byte at the cursor position.</summary>
    public Option<byte> Current => Peek();

    /// <summary>Gets a value indicating whether the cursor is at the end of the text.</summary>
    public bool IsAtEnd => !HasMoreBytes();

    /// <summary>Gets the start offset inside the window (relative to the <see cref="SourceText"/> start).</summary>
    public int WindowStart { get; private set; }

    /// <summary>Gets the end offset inside the window (relative to the <see cref="WindowStart"/>).</summary>
    public int Offset { get; private set; }

    /// <summary>Gets the absolute position in the <see cref="SourceText"/>.</summary>
    public int Position => WindowStart + Offset;

    /// <summary>Gets a value indicating whether the window is in parsing lexeme mode.</summary>
    public bool IsLexemeMode { get; private set; }

    /// <summary>Gets the lexeme start offset relative to the <see cref="WindowStart">window start</see>.</summary>
    public int LexemeStart { get; private set; }

    /// <summary>Gets the absolute position of the lexeme in the <see cref="SourceText"/>.</summary>
    public int LexemePosition => WindowStart + LexemeStart;

    /// <summary>Gets the width of the lexeme.</summary>
    public int LexemeWidth => Offset - LexemeStart;

    /// <summary>Gets the text window.</summary>
    public ReadOnlyMemory<byte> Window => _characterWindowMemoryOwner.Memory;

    /// <summary>Gets the number of valid characters in the text window.</summary>
    public int WindowSize { get; private set; }

    /// <summary>Peeks at the byte at the specified delta from the current position.</summary>
    /// <returns>The byte at the specified delta if available; otherwise, <see cref="Option{T}.None"/>.</returns>
    public Option<byte> Peek()
    {
        return !HasMoreBytes() ? Option<byte>.None : _characterWindowMemoryOwner.Memory.Span[Offset];
    }

    /// <summary>Peeks at the byte at the specified delta from the current position.</summary>
    /// <param name="delta">The delta from the current position.</param>
    /// <returns>The byte at the specified delta if available; otherwise, <see cref="Option{T}.None"/>.</returns>
    public Option<byte> Peek(int delta)
    {
        Debug.Assert(delta >= 0, "Delta should be positive");
        var position = Position;

        Advance(delta);
        var b = !HasMoreBytes() ? Option<byte>.None : _characterWindowMemoryOwner.Memory.Span[Offset];

        SlideTextWindow(position);
        return b;
    }

    /// <summary>Advances the cursor by the specified delta.</summary>
    /// <param name="delta">The delta by which to advance the cursor.</param>
    public void Advance(int delta = 1)
    {
        Offset += delta;
    }

    /// <summary>Advances the cursor while the specified predicate is true.</summary>
    /// <param name="predicate">The predicate to test each byte against.</param>
    public void Advance(Predicate<byte> predicate)
    {
        while (Current.Where(predicate).IsSome)
        {
            Advance();
        }
    }

    /// <summary>Tries to advance the cursor if the current byte matches the specified byte.</summary>
    /// <param name="b">The byte to match against.</param>
    /// <returns>True if the cursor was advanced; otherwise, false.</returns>
    public bool TryAdvance(byte b)
    {
        if (!Current.Where(x => x == b).IsSome)
            return false;

        Advance();
        return true;
    }

    /// <summary>Tries to advance the cursor if the subsequent bytes match the specified subtext.</summary>
    /// <param name="subtext">The subtext to match against.</param>
    /// <returns>True if the cursor was advanced; otherwise, false.</returns>
    public bool TryAdvance(ReadOnlySpan<byte> subtext)
    {
        if (subtext.IsEmpty)
            return false;

        for (var i = 0; i < subtext.Length; i++)
        {
            if (!Peek(i).TryGetValue(out var value) || value != subtext[i])
            {
                return false;
            }
        }

        Advance(subtext.Length);
        return true;
    }

    /// <summary>Slides the text window to the specified start position and size.</summary>
    /// <param name="windowStart">The start position of the window.</param>
    /// <param name="windowSize">The size of the window.</param>
    public void SlideTextWindow(int windowStart = -1, int windowSize = -1)
    {
        ChangeWindowSize(windowSize);
        ChangeWindowStart(windowStart);
        AdjustTextWindowCursors(windowStart);
    }

    /// <summary>Starts parsing a lexeme and sets the <see cref="LexemeStart"/> to the current <see cref="Offset"/> value.</summary>
    public void StartLexemeMode()
    {
        LexemeStart = Offset;
        IsLexemeMode = true;
    }

    /// <summary>Stops parsing a lexeme.</summary>
    public void StopLexemeMode()
    {
        LexemeStart = 0;
        IsLexemeMode = false;
    }

    /// <summary>Releases the resources used by the <see cref="TextCursor"/> class.</summary>
    public void Dispose()
    {
        _characterWindowMemoryOwner.Dispose();
    }

    private bool HasMoreBytes()
    {
        if (Offset < WindowSize)
            return true;

        if (Position >= SourceText.Length)
            return false;

        if (IsLexemeMode)
        {
            // grow the window to include the entire lexeme
            var offset = Offset;
            var size = WindowSize == 0 ? _characterWindowMemoryOwner.Memory.Span.Length : WindowSize;
            var windowSize = size * 2;

            SlideTextWindow(WindowStart, windowSize);
            Offset = offset;
        }
        else
        {
            SlideTextWindow(Position);
        }

        return WindowSize > 0;
    }

    private void ChangeWindowSize(int windowSize)
    {
        if (windowSize < 0)
            return;

        if (windowSize == WindowSize)
            return;

        // the new memory owner size is not guaranteed to match the requested window size as it is rounded up to the nearest power of 2
        var newCharacterWindowMemoryOwner = MemoryPool<byte>.Shared.Rent(windowSize);
        var oldCharacterWindowMemoryOwner = _characterWindowMemoryOwner;

        var newWindowSize = Math.Min(windowSize, WindowSize);
        oldCharacterWindowMemoryOwner.Memory.Span[..newWindowSize].CopyTo(newCharacterWindowMemoryOwner.Memory.Span);
        oldCharacterWindowMemoryOwner.Dispose();

        _characterWindowMemoryOwner = newCharacterWindowMemoryOwner;
    }

    private void ChangeWindowStart(int windowStart)
    {
        if (windowStart < 0)
            return;

        if (windowStart >= SourceText.Length)
            return;

        var windowSpan = _characterWindowMemoryOwner.Memory.Span;
        var count = Math.Min(SourceText.Length - windowStart, windowSpan.Length);

        if (count > 0)
        {
            SourceText.CopyTo(windowStart, windowSpan, 0, count);
        }

        WindowStart = windowStart;
        WindowSize = count;
    }

    private void AdjustTextWindowCursors(int windowStart)
    {
        var windowStartChanged = windowStart != WindowStart;
        var relative = windowStart - WindowStart;
        var isRelativeWithinBounds = relative >= 0 && relative <= WindowSize;

        if (windowStartChanged || Offset >= WindowSize)
        {
            Offset = 0;
            StopLexemeMode();
            return;
        }

        if (isRelativeWithinBounds && relative != Offset)
        {
            Offset = relative;
        }
    }
}

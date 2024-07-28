// <copyright file="TextCursor.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Lexer;

using PooledObjects;
using Utils;

/// <summary>Represents a text cursor for navigating and processing text data.</summary>
/// <example>
/// <code>
/// SLIDING                    position
/// WINDOW                        |
///            -------- + --------
///           |                   |
///         basis   =======>>   offset
///           |                   |
///        -------------------------------
/// SRC:  | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
///        -------------------------------
///               |           |
///             basis ==>>  offset
///               |           |
///                ---- + ----
/// LEXEME                    |
/// SUB-WINDOW             position
/// </code>
/// </example>
internal sealed class TextCursor : ITextCursor
{
    private byte[] _characterWindow;

    /// <summary>Initializes a new instance of the <see cref="TextCursor"/> class with a read-only byte span.</summary>
    /// <param name="text">The text represented as a read-only byte span.</param>
    public TextCursor(in ISourceText text)
    {
        _characterWindow = SharedObjectPools.WindowPool.Get();
        SourceText = text;
        Basis = 0;
        Offset = 0;
        LexemeBasis = 0;
    }

    /// <summary>Gets the source text.</summary>
    public ISourceText SourceText { get; }

    /// <summary>Gets the current byte at the cursor position.</summary>
    public Option<byte> Current => Peek();

    /// <summary>Gets a value indicating whether the cursor is at the end of the text.</summary>
    public bool IsAtEnd => Offset >= WindowCount && !HasMoreBytes();

    /// <summary>Gets the start offset inside the window (relative to the <see cref="SourceText"/> start).</summary>
    public int Basis { get; private set; }

    /// <summary>Gets the end offset inside the window (relative to the <see cref="Basis"/>).</summary>
    public int Offset { get; private set; }

    /// <summary>Gets the absolute position in the <see cref="SourceText"/>.</summary>
    public int Position => Basis + Offset;

    /// <summary>Gets a value indicating whether the window is in parsing lexeme mode.</summary>
    public bool IsLexemeMode { get; private set; }

    /// <summary>Gets the lexeme start offset relative to the <see cref="Basis">window start</see>.</summary>
    public int LexemeBasis { get; private set; }

    /// <summary>Gets the absolute position of the lexeme in the <see cref="SourceText"/>.</summary>
    public int LexemePosition => Basis + LexemeBasis;

    /// <summary>Gets the width of the lexeme.</summary>
    public int LexemeWidth => Offset - LexemeBasis;

    /// <summary>Gets the number of characters in the window.</summary>
    public int WindowCount { get; private set; }

    /// <summary>Peeks at the byte at the specified delta from the current position.</summary>
    /// <param name="delta">The delta from the current position.</param>
    /// <returns>The byte at the specified delta if available; otherwise, <see cref="Option{T}.None"/>.</returns>
    public Option<byte> Peek(int delta = 0)
    {
        Debug.Assert(delta >= 0, "Delta should be positive");
        return !IsAtEnd
            ? Option<byte>.Some(_characterWindow[Offset + delta])
            : Option<byte>.None;
    }

    /// <summary>Advances the cursor by the specified delta.</summary>
    /// <param name="delta">The delta by which to advance the cursor.</param>
    public void Advance(int delta = 1)
    {
        Debug.Assert(delta > 0, "Delta should greater than 0");
        Offset += delta;
    }

    /// <summary>Advances the cursor while the specified predicate is true.</summary>
    /// <param name="predicate">The predicate to test each byte against.</param>
    public void Advance(Predicate<byte> predicate)
    {
        while (Current.Where(predicate).IsSome(out _))
        {
            Advance();
        }
    }

    /// <summary>Tries to advance the cursor if the current byte matches the specified byte.</summary>
    /// <param name="b">The byte to match against.</param>
    /// <returns>True if the cursor was advanced; otherwise, false.</returns>
    public bool TryAdvance(byte b)
    {
        if (!Current.Where(x => x == b).IsSome(out _))
        {
            return false;
        }

        Advance();
        return true;
    }

    /// <summary>Tries to advance the cursor if the subsequent bytes match the specified subtext.</summary>
    /// <param name="subtext">The subtext to match against.</param>
    /// <returns>True if the cursor was advanced; otherwise, false.</returns>
    public bool TryAdvance(ReadOnlySpan<byte> subtext)
    {
        if (subtext.IsEmpty)
        {
            return false;
        }

        var pool = ArrayPool<byte>.Shared;
        var buffer = pool.Rent(subtext.Length);
        subtext.CopyTo(buffer);

        for (var i = 0; i < subtext.Length; i++)
        {
            var i1 = i;
            if (!Peek(i).Where(x => x == buffer[i1]).IsSome(out _))
            {
                pool.Return(buffer);
                return false;
            }
        }

        pool.Return(buffer);
        Advance(subtext.Length);
        return true;
    }

    /// <summary>Starts parsing a lexeme and sets the <see cref="LexemeBasis"/> to the current <see cref="Offset"/> value.</summary>
    public void StartLexemeMode()
    {
        LexemeBasis = Offset;
        IsLexemeMode = true;
    }

    /// <summary>Stops parsing a lexeme.</summary>
    public void StopLexemeMode()
    {
        LexemeBasis = 0;
        IsLexemeMode = false;
    }

    /// <summary>Releases the resources used by the <see cref="TextCursor"/> class.</summary>
    public void Dispose()
    {
        SharedObjectPools.WindowPool.Return(_characterWindow);
    }

    private bool HasMoreBytes()
    {
        if (Offset < WindowCount)
        {
            return true;
        }

        if (Position >= SourceText.Length)
        {
            return false;
        }

        // if lexeme scanning is sufficiently into the char buffer,
        // then refocus the window onto the lexeme
        if (LexemeBasis > WindowCount / 4)
        {
            Array.Copy(
                sourceArray: _characterWindow,
                sourceIndex: LexemeBasis,
                destinationArray: _characterWindow,
                destinationIndex: 0,
                length: WindowCount - LexemeBasis);

            WindowCount -= LexemeBasis;
            Offset -= LexemeBasis;
            Basis += LexemeBasis;
            StopLexemeMode();
        }

        if (WindowCount >= _characterWindow.Length)
        {
            // grow char array, since we need more contiguous space
            var oldWindow = _characterWindow;
            var newWindow = new byte[_characterWindow.Length * 2];

            Array.Copy(
                sourceArray: oldWindow,
                sourceIndex: 0,
                destinationArray: newWindow,
                destinationIndex: 0,
                length: WindowCount);

            _characterWindow = newWindow;
        }

        var amountToRead = Math.Min(
            SourceText.Length - (Basis + WindowCount),
            _characterWindow.Length - WindowCount);

        SourceText.CopyTo(Basis + WindowCount, _characterWindow, WindowCount, amountToRead);
        WindowCount += amountToRead;
        return amountToRead > 0;
    }
}

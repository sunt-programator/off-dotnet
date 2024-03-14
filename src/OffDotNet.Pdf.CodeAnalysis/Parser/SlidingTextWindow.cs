// <copyright file="SlidingTextWindow.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Parser;

using System.Diagnostics.CodeAnalysis;
using Caching;
using InternalUtilities;
using PooledObjects;
using Text;

/// <summary>
/// Keeps a sliding buffer over the SourceText of a file for the lexer.
/// <p>Also provides the lexer with the ability to keep track of a current "lexeme"
/// by leaving a marker and advancing ahead the offset.</p>
/// <p>The lexer can then decide to "keep" the lexeme by erasing the marker, or abandon the current
/// lexeme by moving the offset back to the marker.</p>
/// </summary>
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
internal sealed class SlidingTextWindow : IDisposable
{
    private readonly ThreadSafeCacheFactory<int, byte[]> _stringTable = SharedObjectPools.StringTable.Get();

    private byte[] _characterWindow;

    /// <summary>Initializes a new instance of the <see cref="SlidingTextWindow"/> class.</summary>
    /// <param name="text">The <see cref="SourceText"/> instance to slide over.</param>
    public SlidingTextWindow(SourceText text)
    {
        Text = text;
        _characterWindow = SharedObjectPools.WindowPool.Get();
    }

    /// <summary>Gets the <see cref="SourceText"/> instance that the window is sliding over.</summary>
    public SourceText Text { get; }

    /// <summary>Gets the start offset inside the window (relative to the <see cref="SourceText"/> start).</summary>
    public int Basis { get; private set; }

    /// <summary>Gets the end offset inside the window (relative to the <see cref="Basis"/>).</summary>
    public int Offset { get; private set; }

    /// <summary>Gets the absolute position in the <see cref="SourceText"/>.</summary>
    public int Position => Basis + Offset;

    /// <summary>Gets the lexeme start offset relative to the <see cref="Basis">window start</see>.</summary>
    public int LexemeBasis { get; private set; }

    /// <summary>Gets a value indicating whether the window is in parsing lexeme mode.</summary>
    public bool IsLexemeMode { get; private set; }

    /// <summary>Gets the absolute position of the lexeme in the <see cref="SourceText"/>.</summary>
    public int LexemePosition => Basis + LexemeBasis;

    /// <summary>Gets the width of the lexeme.</summary>
    public int LexemeWidth => Offset - LexemeBasis;

    /// <summary>Gets the number of characters in the window.</summary>
    public int CharacterWindowCount { get; private set; }

    /// <summary>Advances the window by one byte.</summary>
    public void AdvanceByte()
    {
        Offset++;
    }

    /// <summary>Advances the window by the specified number of bytes.</summary>
    /// <param name="delta">The number of bytes to advance.</param>
    public void AdvanceByte(int delta)
    {
        Offset += delta;
    }

    /// <summary>Advances the offset if the next byte matches the specified value.</summary>
    /// <param name="expected">The value to match.</param>
    /// <returns><see langword="true"/> if the next byte matches the specified value; otherwise, <see langword="false"/>.</returns>
    public bool TryAdvanceIfMatches(byte expected)
    {
        if (PeekByte() == expected)
        {
            AdvanceByte();
            return true;
        }

        return false;
    }

    /// <summary>Advances the offset if the next bytes matches the specified string.</summary>
    /// <param name="expected">The string to match.</param>
    /// <returns><see langword="true"/> if the next bytes match the specified string; otherwise, <see langword="false"/>.</returns>
    public bool TryAdvanceIfMatches(ReadOnlySpan<byte> expected)
    {
        for (var i = 0; i < expected.Length; i++)
        {
            if (PeekByte(i) != expected[i])
            {
                return false;
            }
        }

        AdvanceByte(expected.Length);
        return true;
    }

    /// <summary>Advances the offset if the next byte matches the specified predicate.</summary>
    /// <param name="predicate">The predicate to match.</param>
    public void AdvanceIfMatches(Func<byte?, bool> predicate)
    {
        while (predicate(PeekByte()))
        {
            AdvanceByte();
        }
    }

    /// <summary>Gets the byte at the current position in the window.</summary>
    /// <returns>The byte at the current position in the window.</returns>
    public byte? PeekByte()
    {
        if (Offset >= CharacterWindowCount && !HasMoreBytes())
        {
            return null;
        }

        return _characterWindow[Offset];
    }

    /// <summary>Gets the byte at the specified position in the window.</summary>
    /// <param name="delta">The number of bytes to advance the window before peeking at the byte.</param>
    /// <returns>The byte at the specified position in the window. Otherwise, <see langword="null"/>.</returns>
    public byte? PeekByte(int delta)
    {
        var position = this.Position;
        this.AdvanceByte(delta);
        byte? b = Offset >= CharacterWindowCount && !HasMoreBytes() ? null : _characterWindow[Offset];
        this.Reset(position);
        return b;
    }

    /// <summary>Picks the byte at the current position and advances the window by one byte.</summary>
    /// <returns>The byte at the current position in the window if exists. Otherwise, <see langword="null"/>.</returns>
    public byte? PeekAndAdvanceByte()
    {
        var b = PeekByte();

        if (b == null)
        {
            return null;
        }

        AdvanceByte();
        return b.Value;
    }

    /// <summary>Starts parsing a lexeme and sets the <see cref="LexemeBasis"/> to the current <see cref="Offset"/> value.</summary>
    public void StartParsingLexeme()
    {
        IsLexemeMode = true;
        LexemeBasis = Offset;
    }

    /// <summary>Stops parsing a lexeme.</summary>
    public void StopParsingLexeme()
    {
        IsLexemeMode = false;
        LexemeBasis = 0;
    }

    /// <summary>Resets the window to the specified position.</summary>
    /// <param name="position">The position to reset the window to.</param>
    public void Reset(int position)
    {
        // if position is within already read character range then just use what we have
        var relative = position - Basis;
        if (relative >= 0 && relative < CharacterWindowCount)
        {
            Offset = relative;
            return;
        }

        // otherwise, we need to reread text buffer
        var amountToRead = Math.Min(Text.Length, position + _characterWindow.Length) - position;
        amountToRead = Math.Max(0, amountToRead);

        if (amountToRead > 0)
        {
            Text.CopyTo(position, _characterWindow, 0, amountToRead);
        }

        Basis = position;
        Offset = 0;
        CharacterWindowCount = amountToRead;
        StopParsingLexeme();
    }

    /// <summary>Gets a byte array from the window.</summary>
    /// <param name="position">The position to start reading from.</param>
    /// <param name="length">The number of bytes to read.</param>
    /// <param name="shouldIntern">Whether to intern the string.</param>
    /// <returns>The byte array from the window.</returns>
    public ReadOnlySpan<byte> GetBytes(int position, int length, bool shouldIntern)
    {
        var offset = position - Basis;

        if (TryGetBytesFast(offset, length, out var result))
        {
            return result;
        }

        if (!shouldIntern)
        {
            return _characterWindow.AsSpan(offset, length);
        }

        ReadOnlySpan<byte> bytes = _characterWindow.AsSpan(offset, length);
        var hashCode = HashCodeUtilities.ComputeHashCode(bytes);
        return _stringTable.GetOrAdd(hashCode, bytes.ToArray());
    }

    /// <summary>Gets the lexeme bytes from the window.</summary>
    /// <param name="shouldIntern">Whether to intern the lexeme as byte array.</param>
    /// <returns>The lexeme text from the window.</returns>
    public ReadOnlySpan<byte> GetLexemeBytes(bool shouldIntern)
    {
        return GetBytes(LexemeBasis, LexemeWidth, shouldIntern);
    }

    /// <summary>Checks whether the window is at the end of the <see cref="SourceText"/>.</summary>
    /// <returns>
    /// <see langword="true"/> if the window is at the end of the <see cref="SourceText"/>; otherwise, <see langword="false"/>.
    /// </returns>
    public bool IsAtEnd()
    {
        return Offset >= CharacterWindowCount && Position >= Text.Length;
    }

    /// <summary>Disposes the window and return the underlying buffer to the pool.</summary>
    public void Dispose()
    {
        SharedObjectPools.StringTable.Return(_stringTable);
        SharedObjectPools.WindowPool.Return(_characterWindow);
    }

    private bool TryGetBytesFast(int offset, int length, [NotNullWhen(true)] out byte[]? result)
    {
        // PERF: Whether interning or not, there are some frequently occurring
        // easy cases we can pick off easily.
        result = length switch
        {
            0 => [],
            1 when _characterWindow[offset] == (byte)' ' => [(byte)' '],
            1 when _characterWindow[offset] == (byte)'\n' => [(byte)'\n'],
            2 when _characterWindow[offset] == (byte)'\r' && _characterWindow[offset + 1] == (byte)'\n' =>
                [(byte)'\r', (byte)'\n'],
            _ => null,
        };

        return result != null;
    }

    private bool HasMoreBytes()
    {
        if (Offset < CharacterWindowCount)
        {
            return true;
        }

        if (this.Position >= Text.Length)
        {
            return false;
        }

        // if lexeme scanning is sufficiently into the char buffer,
        // then refocus the window onto the lexeme
        if (LexemeBasis > (CharacterWindowCount / 4))
        {
            Array.Copy(
                _characterWindow,
                LexemeBasis,
                _characterWindow,
                0,
                CharacterWindowCount - LexemeBasis);

            CharacterWindowCount -= LexemeBasis;
            Offset -= LexemeBasis;
            Basis += LexemeBasis;
            StopParsingLexeme();
        }

        if (CharacterWindowCount >= _characterWindow.Length)
        {
            // grow char array, since we need more contiguous space
            var oldWindow = _characterWindow;
            var newWindow = new byte[_characterWindow.Length * 2];
            Array.Copy(oldWindow, 0, newWindow, 0, CharacterWindowCount);
            _characterWindow = newWindow;
        }

        var amountToRead = Math.Min(
            Text.Length - (Basis + CharacterWindowCount),
            _characterWindow.Length - CharacterWindowCount);

        Text.CopyTo(Basis + CharacterWindowCount, _characterWindow, CharacterWindowCount, amountToRead);
        CharacterWindowCount += amountToRead;
        return amountToRead > 0;
    }
}

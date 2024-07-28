// <copyright file="TextCursor.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Lexer;

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
    private int _position;

    /// <summary>Initializes a new instance of the <see cref="TextCursor"/> class with a read-only byte span.</summary>
    /// <param name="text">The text represented as a read-only byte span.</param>
    public TextCursor(ISourceText text)
    {
        SourceText = text;
        Length = text.Length;
        _position = 0;
    }

    /// <summary>Gets the source text.</summary>
    public ISourceText SourceText { get; }

    /// <summary>Gets the current byte at the cursor position.</summary>
    public Option<byte> Current => Peek();

    /// <summary>Gets the length of the text.</summary>
    public int Length { get; }

    /// <summary>Gets a value indicating whether the cursor is at the end of the text.</summary>
    public bool IsAtEnd => _position >= Length;

    /// <summary>Peeks at the byte at the specified delta from the current position.</summary>
    /// <param name="delta">The delta from the current position.</param>
    /// <returns>The byte at the specified delta if available; otherwise, <see cref="Option{T}.None"/>.</returns>
    public Option<byte> Peek(int delta = 0)
    {
        Debug.Assert(delta >= 0, "Delta should be positive");
        return !IsAtEnd
            ? Option<byte>.Some(SourceText[_position + delta])
            : Option<byte>.None;
    }

    /// <summary>Advances the cursor by the specified delta.</summary>
    /// <param name="delta">The delta by which to advance the cursor.</param>
    public void Advance(int delta = 1)
    {
        Debug.Assert(delta > 0, "Delta should greater than 0");
        _position += delta;
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

    /// <summary>Releases the resources used by the <see cref="TextCursor"/> class.</summary>
    public void Dispose()
    {
    }
}

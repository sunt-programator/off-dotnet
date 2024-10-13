// <copyright file="ITextCursor.cs" company="Sunt Programator">
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
public interface ITextCursor : IDisposable
{
    /// <summary>Gets the source text.</summary>
    ISourceText SourceText { get; }

    /// <summary>
    /// Gets the current byte at the cursor position.
    /// </summary>
    Option<byte> Current { get; }

    /// <summary>
    /// Gets a value indicating whether the cursor is at the end of the text.
    /// </summary>
    bool IsAtEnd { get; }

    /// <summary>Gets the start offset inside the window (relative to the <see cref="ISourceText"/> start).</summary>
    int WindowStart { get; }

    /// <summary>Gets the end offset inside the window (relative to the <see cref="WindowStart"/>).</summary>
    int Offset { get; }

    /// <summary>Gets the absolute position in the <see cref="ISourceText"/>.</summary>
    int Position => WindowStart + Offset;

    /// <summary>Gets a value indicating whether the window is in parsing lexeme mode.</summary>
    bool IsLexemeMode { get; }

    /// <summary>Gets the lexeme start offset relative to the <see cref="WindowStart">window start</see>.</summary>
    int LexemeStart { get; }

    /// <summary>Gets the absolute position of the lexeme in the <see cref="ISourceText"/>.</summary>
    int LexemePosition => WindowStart + LexemeStart;

    /// <summary>Gets the width of the lexeme.</summary>
    int LexemeWidth => Offset - LexemeStart;

    /// <summary>Gets the text window.</summary>
    ReadOnlyMemory<byte> Window { get; }

    /// <summary>Gets the number of characters in the window.</summary>
    int WindowSize { get; }

    /// <summary>
    /// Peeks at the byte at the specified delta from the current position.
    /// </summary>
    /// <param name="delta">The delta from the current position.</param>
    /// <returns>The byte at the specified delta if available; otherwise, <see cref="Option{T}.None"/>.</returns>
    Option<byte> Peek(int delta = 0);

    /// <summary>
    /// Advances the cursor by the specified delta.
    /// </summary>
    /// <param name="delta">The delta by which to advance the cursor.</param>
    void Advance(int delta = 1);

    /// <summary>
    /// Advances the cursor while the specified predicate is true.
    /// </summary>
    /// <param name="predicate">The predicate to test each byte against.</param>
    void Advance(Predicate<byte> predicate);

    /// <summary>
    /// Tries to advance the cursor if the current byte matches the specified byte.
    /// </summary>
    /// <param name="b">The byte to match against.</param>
    /// <returns>True if the cursor was advanced; otherwise, false.</returns>
    bool TryAdvance(byte b);

    /// <summary>
    /// Tries to advance the cursor if the subsequent bytes match the specified subtext.
    /// </summary>
    /// <param name="subtext">The subtext to match against.</param>
    /// <returns>True if the cursor was advanced; otherwise, false.</returns>
    bool TryAdvance(ReadOnlySpan<byte> subtext);

    /// <summary>Slides the text window to the specified start position and size.</summary>
    /// <param name="windowStart">The start position of the window.</param>
    /// <param name="windowSize">The size of the window.</param>
    void SlideTextWindow(int windowStart = -1, int windowSize = -1);

    /// <summary>Starts parsing a lexeme and sets the <see cref="LexemeStart"/> to the current <see cref="Offset"/> value.</summary>
    public void StartLexemeMode();

    /// <summary>Stops parsing a lexeme.</summary>
    public void StopLexemeMode();
}

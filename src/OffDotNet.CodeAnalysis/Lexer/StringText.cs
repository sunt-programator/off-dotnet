// <copyright file="StringText.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Lexer;

/// <summary>
/// Represents a string-based source text for the lexer.
/// </summary>
internal sealed class StringText : ISourceText
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StringText"/> class with the specified source text.
    /// </summary>
    /// <param name="source">The source text as a read-only span of bytes.</param>
    internal StringText(in ReadOnlySpan<byte> source)
    {
        Source = source.ToArray();
    }

    /// <summary>
    /// Gets the source text as a read-only memory of bytes.
    /// </summary>
    public ReadOnlyMemory<byte> Source { get; }

    /// <summary>
    /// Gets the length of the source text.
    /// </summary>
    public int Length => Source.Length;

    /// <summary>
    /// Gets the byte at the specified position in the source text.
    /// </summary>
    /// <param name="position">The position of the byte.</param>
    /// <returns>The byte at the specified position.</returns>
    public byte this[int position] => Source.Span[position];

    /// <summary>
    /// Copies a range of bytes from the source text to a destination span.
    /// </summary>
    /// <param name="sourceIndex">The starting index in the source text.</param>
    /// <param name="destination">The destination span.</param>
    /// <param name="destinationIndex">The starting index in the destination span.</param>
    /// <param name="count">The number of bytes to copy.</param>
    public void CopyTo(int sourceIndex, Span<byte> destination, int destinationIndex, int count)
    {
        Source.Span.Slice(sourceIndex, count).CopyTo(destination[destinationIndex..]);
    }
}

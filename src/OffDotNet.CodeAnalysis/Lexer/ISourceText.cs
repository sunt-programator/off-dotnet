// <copyright file="ISourceText.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Lexer;

/// <summary>
/// Represents the source text for the lexer.
/// </summary>
public interface ISourceText
{
    /// <summary>
    /// Gets the source text as a read-only memory of bytes.
    /// </summary>
    ReadOnlyMemory<byte> Source { get; }

    /// <summary>
    /// Gets the length of the source text.
    /// </summary>
    int Length { get; }

    /// <summary>
    /// Gets the byte at the specified position in the source text.
    /// </summary>
    /// <param name="position">The position of the byte.</param>
    /// <returns>The byte at the specified position.</returns>
    byte this[int position] { get; }

    /// <summary>
    /// Copies a range of bytes from the source text to a destination span.
    /// </summary>
    /// <param name="sourceIndex">The starting index in the source text.</param>
    /// <param name="destination">The destination span.</param>
    /// <param name="destinationIndex">The starting index in the destination span.</param>
    /// <param name="count">The number of bytes to copy.</param>
    void CopyTo(int sourceIndex, Span<byte> destination, int destinationIndex, int count);
}

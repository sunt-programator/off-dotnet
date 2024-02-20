// <copyright file="SourceText.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Text;

/// <summary>Represents an abstraction of source text.</summary>
public abstract class SourceText
{
    /// <summary>Gets the length of the text represented by the <see cref="SourceText"/>.</summary>
    public abstract int Length { get; }

    /// <summary>Gets the byte at the specified position.</summary>
    /// <param name="position">The position to get the byte from.</param>
    public abstract byte this[int position] { get; }

    /// <summary>Creates a new <see cref="SourceText"/> instance from the specified UTF-8 string literal.</summary>
    /// <param name="source">The source UTF-8 string literal.</param>
    /// <returns>A new <see cref="SourceText"/> instance.</returns>
    public static SourceText From(ReadOnlySpan<byte> source)
    {
        return new StringText(source);
    }

    /// <summary>
    /// Copies a specified number of characters from a specified position in the <see cref="SourceText"/>
    /// to a specified position in a destination <see cref="Span{T}"/>.
    /// </summary>
    /// <param name="sourceIndex">The position in the <see cref="SourceText"/> at which to start copying characters.</param>
    /// <param name="destination">The destination <see cref="Span{T}"/> to which the characters are copied.</param>
    /// <param name="destinationIndex">The position in the destination <see cref="Span{T}"/> at which to start copying characters.</param>
    /// <param name="count">The number of characters to copy.</param>
    public abstract void CopyTo(int sourceIndex, byte[] destination, int destinationIndex, int count);
}

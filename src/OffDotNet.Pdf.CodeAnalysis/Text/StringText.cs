// <copyright file="StringText.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Text;

/// <summary>
/// Represents an implementation of the <see cref="SourceText"/> based on an UTF-8 string literal.
/// </summary>
internal sealed class StringText : SourceText
{
    /// <summary>Initializes a new instance of the <see cref="StringText"/> class.</summary>
    /// <param name="source">The source string.</param>
    internal StringText(ReadOnlySpan<byte> source)
    {
        Source = source.ToArray();
    }

    /// <summary>Gets the source string.</summary>
    public ReadOnlyMemory<byte> Source { get; }

    /// <inheritdoc />
    public override int Length => Source.Length;

    /// <inheritdoc />
    public override byte this[int position] => Source.Span[position];

    /// <inheritdoc />
    public override void CopyTo(int sourceIndex, byte[] destination, int destinationIndex, int count)
    {
        Source.Span.Slice(sourceIndex, count).CopyTo(destination.AsSpan(destinationIndex));
    }
}

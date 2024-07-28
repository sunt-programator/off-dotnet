// <copyright file="StringText.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Lexer;

internal sealed class StringText : ISourceText
{
    internal StringText(in ReadOnlySpan<byte> source)
    {
        Source = source.ToArray();
    }

    public ReadOnlyMemory<byte> Source { get; }

    public int Length => Source.Length;

    public byte this[int position] => Source.Span[position];

    public void CopyTo(int sourceIndex, byte[] destination, int destinationIndex, int count)
    {
        Source.Span.Slice(sourceIndex, count).CopyTo(destination.AsSpan(destinationIndex));
    }
}

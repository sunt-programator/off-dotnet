// <copyright file="InputReader.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.InputReaders;

internal sealed class InputReader
{
    public InputReader(ReadOnlyMemory<byte> source)
    {
        this.Source = source;
    }

    public ReadOnlyMemory<byte> Source { get; }

    public int Offset { get; private set; }

    public bool IsEndOfStream => this.Offset >= this.Source.Length;

    public byte? PeekByte()
    {
        return !this.IsEndOfStream ? this.Source.Span[this.Offset] : null;
    }

    public void AdvanceByte()
    {
        this.Offset++;
    }
}

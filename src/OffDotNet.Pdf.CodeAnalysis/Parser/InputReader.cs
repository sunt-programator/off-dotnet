// <copyright file="InputReader.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Parser;

internal sealed class InputReader
{
    private readonly byte[] source;
    private int offset;

    public InputReader(byte[] source)
    {
        this.source = source;
    }

    public byte? Peek()
    {
        if (this.offset >= this.source.Length)
        {
            return null;
        }

        return this.source[this.offset];
    }

    public byte? Peek(int delta)
    {
        if (delta < 0 || delta + this.offset >= this.source.Length)
        {
            return null;
        }

        return this.source[this.offset + delta];
    }

    public byte? Consume()
    {
        byte? b = this.Peek();
        if (!b.HasValue)
        {
            return null;
        }

        this.AdvanceByte();
        return b;
    }

    public void AdvanceByte()
    {
        this.offset++;
    }

    public void AdvanceByte(int delta)
    {
        this.offset += delta;
    }
}

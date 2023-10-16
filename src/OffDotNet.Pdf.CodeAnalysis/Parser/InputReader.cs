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

    public bool TryPeek(out byte? nextByte)
    {
        if (this.offset >= this.source.Length)
        {
            nextByte = null;
            return false;
        }

        nextByte = this.source[this.offset];
        return true;
    }

    public bool TryPeek(int delta, out byte? nextByte)
    {
        if (delta < 0 || delta + this.offset >= this.source.Length)
        {
            nextByte = null;
            return false;
        }

        nextByte = this.source[this.offset + delta];
        return true;
    }

    public bool TryConsume(out byte? nextByte)
    {
        bool canPeek = this.TryPeek(out byte? b);
        if (!canPeek)
        {
            nextByte = null;
            return false;
        }

        this.offset++;

        nextByte = b;
        return true;
    }

    public void AdvanceByte(int n)
    {
        this.offset += n;
    }
}

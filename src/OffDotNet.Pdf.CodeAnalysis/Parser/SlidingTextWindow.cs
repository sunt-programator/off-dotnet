// <copyright file="SlidingTextWindow.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Text;

namespace OffDotNet.Pdf.CodeAnalysis.Parser;

internal sealed class SlidingTextWindow : IDisposable
{
    private readonly int basis;
    private int lexemeStart;

    public SlidingTextWindow(byte[] source)
    {
        this.Source = source;
    }

    public byte[] Source { get; }

    public int Position => this.basis + this.Offset;

    public int Offset { get; private set; }

    public int LexemeStartPosition => this.basis + this.lexemeStart;

    public int Width => this.Offset - this.lexemeStart;

    public byte? PeekByte()
    {
        if (this.Offset >= this.Source.Length)
        {
            return null;
        }

        return this.Source[this.Offset];
    }

    public byte? PeekByte(int delta)
    {
        if (delta < 0 || delta + this.Offset >= this.Source.Length)
        {
            return null;
        }

        return this.Source[this.Offset + delta];
    }

    /// <summary>
    /// Grab the next byte from the input stream and advance the offset.
    /// </summary>
    /// <returns>The next byte from the input stream, otherwise <see langword="null"/> if the end of the stream was reached.
    /// </returns>
    public byte? NextByte()
    {
        byte? b = this.PeekByte();
        this.AdvanceByte();
        return b;
    }

    public byte? NextByte(int delta)
    {
        byte? b = this.PeekByte(delta);
        this.AdvanceByte(delta);
        return b;
    }

    public bool TryAdvanceByte(byte b)
    {
        if (this.PeekByte() != b)
        {
            return false;
        }

        this.AdvanceByte();
        return true;
    }

    public bool TryAdvanceByte(int delta, byte b)
    {
        if (this.PeekByte(delta) != b)
        {
            return false;
        }

        this.AdvanceByte(delta);
        return true;
    }

    public void AdvanceByte()
    {
        this.Offset++;
    }

    public void AdvanceByte(int delta)
    {
        this.Offset += delta;
    }

    public void Dispose()
    {
        // TODO release managed resources here
    }

    public void Start()
    {
        this.lexemeStart = this.Offset;
    }

    public string GetText()
    {
        return this.GetText(this.LexemeStartPosition, this.Width);
    }

    private string GetText(int position, int length)
    {
        int offset = position - this.basis;
        return Encoding.Latin1.GetString(this.Source, offset, length);
    }
}

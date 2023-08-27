// <copyright file="PdfArray.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Text;
using OffDotNet.Pdf.Core.Interfaces;

namespace OffDotNet.Pdf.Core.Primitives;

public class PdfArray<TValue> : IPdfObject<IReadOnlyCollection<TValue>>
    where TValue : IPdfObject
{
    private readonly int hashCode;
    private string literalValue = string.Empty;
    private byte[]? bytes;

    public PdfArray(IReadOnlyCollection<TValue> value)
    {
        this.Value = value;
        this.hashCode = HashCode.Combine(nameof(PdfArray<TValue>), value);
        this.bytes = null;
    }

    public int Length => this.Bytes.Length;

    public IReadOnlyCollection<TValue> Value { get; }

    public ReadOnlyMemory<byte> Bytes => this.bytes ??= Encoding.ASCII.GetBytes(this.Content);

    public string Content => this.GenerateContent();

    public static PdfArray<TValue> Create(TValue item)
    {
        return new PdfArray<TValue>(new List<TValue>(1) { item });
    }

    public static PdfArray<TValue> CreateRange(IEnumerable<TValue> items)
    {
        return new PdfArray<TValue>(items.ToList());
    }

    public override int GetHashCode()
    {
        return this.hashCode;
    }

    public override bool Equals(object? obj)
    {
        return obj is PdfArray<TValue> other && EqualityComparer<IReadOnlyCollection<TValue>>.Default.Equals(this.Value, other.Value);
    }

    private string GenerateContent()
    {
        if (this.literalValue.Length != 0)
        {
            return this.literalValue;
        }

        StringBuilder stringBuilder = new();

        foreach (var item in this.Value)
        {
            stringBuilder
                .Append(item.Content)
                .Append(' ');
        }

        if (stringBuilder.Length > 0 && stringBuilder[^1] == ' ')
        {
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
        }

        this.literalValue = stringBuilder
            .Insert(0, '[')
            .Append(']')
            .ToString();

        return this.literalValue;
    }
}

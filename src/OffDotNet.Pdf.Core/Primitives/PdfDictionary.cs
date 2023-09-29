// <copyright file="PdfDictionary.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.ObjectModel;
using System.Text;
using OffDotNet.Pdf.Core.Interfaces;

namespace OffDotNet.Pdf.Core.Primitives;

public class PdfDictionary<TValue> : IPdfObject<IReadOnlyDictionary<PdfName, TValue>>
    where TValue : IPdfObject
{
    private readonly int hashCode;
    private string literalValue = string.Empty;
    private byte[]? bytes;

    public PdfDictionary(IReadOnlyDictionary<PdfName, TValue> value)
    {
        this.Value = value;
        this.hashCode = HashCode.Combine(nameof(PdfDictionary<TValue>), value);
        this.bytes = null;
    }

    public IReadOnlyDictionary<PdfName, TValue> Value { get; }

    public ReadOnlyMemory<byte> Bytes => this.bytes ??= Encoding.ASCII.GetBytes(this.Content);

    public string Content => this.GenerateContent();

    public static PdfDictionary<TValue> Create(KeyValuePair<PdfName, TValue> item)
    {
        IDictionary<PdfName, TValue> dictionary = new Dictionary<PdfName, TValue>(1) { { item.Key, item.Value } };
        return new PdfDictionary<TValue>(new ReadOnlyDictionary<PdfName, TValue>(dictionary));
    }

    public static PdfDictionary<TValue> CreateRange(IDictionary<PdfName, TValue> items)
    {
        return new PdfDictionary<TValue>(new ReadOnlyDictionary<PdfName, TValue>(items));
    }

    public override int GetHashCode()
    {
        return this.hashCode;
    }

    public override bool Equals(object? obj)
    {
        return obj is PdfDictionary<TValue> other && EqualityComparer<IReadOnlyDictionary<PdfName, TValue>>.Default.Equals(this.Value, other.Value);
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
                .Append(item.Key.Content)
                .Append(' ')
                .Append(item.Value.Content)
                .Append(' ');
        }

        if (stringBuilder.Length > 0 && stringBuilder[^1] == ' ')
        {
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
        }

        this.literalValue = stringBuilder
            .Insert(0, "<<")
            .Append(">>")
            .ToString();

        return this.literalValue;
    }
}

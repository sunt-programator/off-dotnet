// <copyright file="PdfDictionary.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Primitives;

using System.Text;
using Common;

public class PdfDictionary<TValue> : PdfObject, IPdfDictionary<TValue>
    where TValue : IPdfObject
{
    private string literalValue = string.Empty;
    private byte[]? bytes;

    public PdfDictionary(IReadOnlyDictionary<PdfName, TValue> value)
    {
        this.Value = value;
        this.bytes = null;
    }

    /// <inheritdoc/>
    public IReadOnlyDictionary<PdfName, TValue> Value { get; }

    /// <inheritdoc/>
    public override ReadOnlyMemory<byte> Bytes => this.bytes ??= Encoding.ASCII.GetBytes(this.Content);

    /// <inheritdoc/>
    public override string Content => this.GenerateContent();

    /// <inheritdoc/>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.Value;
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

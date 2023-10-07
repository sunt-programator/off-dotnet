// <copyright file="PdfArray.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Text;
using OffDotNet.Pdf.Core.Common;

namespace OffDotNet.Pdf.Core.Primitives;

public class PdfArray<TValue> : PdfObject, IPdfArray<TValue>
    where TValue : IPdfObject
{
    private string literalValue = string.Empty;
    private byte[]? bytes;

    public PdfArray(IReadOnlyCollection<TValue> value)
    {
        this.Value = value;
        this.bytes = null;
    }

    public IReadOnlyCollection<TValue> Value { get; }

    public override ReadOnlyMemory<byte> Bytes => this.bytes ??= Encoding.ASCII.GetBytes(this.Content);

    public override string Content => this.GenerateContent();

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

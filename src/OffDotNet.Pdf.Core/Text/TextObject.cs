// <copyright file="TextObject.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Text;
using OffDotNet.Pdf.Core.ContentStreamAndResources;
using OffDotNet.Pdf.Core.Interfaces;

namespace OffDotNet.Pdf.Core.Text;

public sealed class TextObject : IPdfObject<IReadOnlyCollection<PdfOperation>>
{
    private readonly Lazy<int> hashCode;
    private readonly Lazy<string> literalValue;
    private readonly Lazy<byte[]> bytes;

    public TextObject(IReadOnlyCollection<PdfOperation> operations)
    {
        this.Value = operations;

        this.hashCode = new Lazy<int>(() => HashCode.Combine(nameof(TextObject), operations));
        this.literalValue = new Lazy<string>(this.GenerateContent);
        this.bytes = new Lazy<byte[]>(() => Encoding.ASCII.GetBytes(this.Content));
    }

    public ReadOnlyMemory<byte> Bytes => this.bytes.Value;

    public IReadOnlyCollection<PdfOperation> Value { get; }

    public string Content => this.literalValue.Value;

    public override int GetHashCode()
    {
        return this.hashCode.Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is TextObject other && EqualityComparer<IReadOnlyCollection<PdfOperation>>.Default.Equals(this.Value, other.Value);
    }

    private string GenerateContent()
    {
        StringBuilder stringBuilder = new();

        foreach (PdfOperation pdfOperation in this.Value)
        {
            stringBuilder.Append(pdfOperation.Content);
        }

        return stringBuilder
            .Insert(0, "BT\n")
            .Append("ET\n")
            .ToString();
    }
}

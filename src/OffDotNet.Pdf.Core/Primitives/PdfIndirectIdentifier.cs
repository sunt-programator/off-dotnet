// <copyright file="PdfIndirectIdentifier.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Text;
using OffDotNet.Pdf.Core.Interfaces;

namespace OffDotNet.Pdf.Core.Primitives;

public sealed class PdfIndirectIdentifier<T> : IPdfObject, IEquatable<PdfIndirectIdentifier<T>>
    where T : IPdfObject
{
    private readonly Lazy<int> hashCode;
    private readonly Lazy<string> literalValue;
    private readonly Lazy<byte[]> bytes;

    public PdfIndirectIdentifier(PdfIndirect<T> pdfObject)
    {
        this.hashCode = new Lazy<int>(() => HashCode.Combine(nameof(PdfIndirectIdentifier<T>), pdfObject.ObjectNumber, pdfObject.GenerationNumber));
        this.literalValue = new Lazy<string>(this.GenerateContent);
        this.bytes = new Lazy<byte[]>(() => Encoding.ASCII.GetBytes(this.Content));

        this.ObjectNumber = pdfObject.ObjectNumber;
        this.GenerationNumber = pdfObject.GenerationNumber;
        this.PdfIndirect = pdfObject;
    }

    public int GenerationNumber { get; }

    public int ObjectNumber { get; }

    public int Length => this.Bytes.Length;

    public PdfIndirect<T> PdfIndirect { get; }

    public ReadOnlyMemory<byte> Bytes => this.bytes.Value;

    public string Content => this.literalValue.Value;

    public override int GetHashCode()
    {
        return this.hashCode.Value;
    }

    public bool Equals(PdfIndirectIdentifier<T>? other)
    {
        return other != null
               && this.ObjectNumber == other.ObjectNumber
               && this.GenerationNumber == other.GenerationNumber;
    }

    public override bool Equals(object? obj)
    {
        return obj is PdfIndirectIdentifier<T> pdfIndirect && this.Equals(pdfIndirect);
    }

    private string GenerateContent()
    {
        return $"{this.ObjectNumber} {this.GenerationNumber} R";
    }
}

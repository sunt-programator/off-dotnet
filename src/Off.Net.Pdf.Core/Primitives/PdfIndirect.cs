// <copyright file="PdfIndirect.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Text;
using Off.Net.Pdf.Core.Extensions;
using Off.Net.Pdf.Core.Interfaces;

namespace Off.Net.Pdf.Core.Primitives;

public sealed class PdfIndirect<T> : IMutablePdfObject<T>, IEquatable<PdfIndirect<T>>
    where T : IPdfObject
{
    public PdfIndirect(int objectNumber, int generationNumber = 0)
        : this(default, objectNumber, generationNumber)
    {
    }

    public PdfIndirect(T? pdfObject, int objectNumber, int generationNumber = 0)
    {
        this.ObjectNumber = objectNumber
            .CheckConstraints(num => num >= 0, Resource.PdfIndirect_ObjectNumberMustBePositive);

        this.GenerationNumber = generationNumber
            .CheckConstraints(num => num >= 0, Resource.PdfIndirect_ObjectNumberMustBePositive)
            .CheckConstraints(num => num <= 65535, Resource.PdfIndirect_GenerationNumberMustNotExceedMaxAllowedValue);

        this.Value = pdfObject;
    }

    public int GenerationNumber { get; }

    public int ObjectNumber { get; }

    public int Length => this.Bytes.Length;

    public ReadOnlyMemory<byte> Bytes => Encoding.ASCII.GetBytes(this.Content);

    public string Content => this.GenerateContent();

    public T? Value { get; set; }

    public override int GetHashCode()
    {
        return HashCode.Combine(nameof(PdfIndirect<T>), this.ObjectNumber, this.GenerationNumber);
    }

    public bool Equals(PdfIndirect<T>? other)
    {
        return other != null
               && this.ObjectNumber == other.ObjectNumber
               && this.GenerationNumber == other.GenerationNumber;
    }

    public override bool Equals(object? obj)
    {
        return obj is PdfIndirect<T> pdfIndirect && this.Equals(pdfIndirect);
    }

    private string GenerateContent()
    {
        string objectIdentifier = $"{this.ObjectNumber} {this.GenerationNumber} obj\n";

        StringBuilder stringBuilder = new StringBuilder()
            .Append(objectIdentifier);

        if (this.Value != null)
        {
            stringBuilder.Append(this.Value.Content);
        }

        return stringBuilder
            .Append("\nendobj\n")
            .ToString();
    }
}

// <copyright file="PdfIndirect.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Primitives;

using System.Text;
using Common;
using Extensions;
using Properties;

public sealed class PdfIndirect<T> : PdfObject, IPdfIndirect<T>
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

    /// <inheritdoc/>
    public int GenerationNumber { get; }

    /// <inheritdoc/>
    public int ObjectNumber { get; }

    /// <inheritdoc/>
    public override ReadOnlyMemory<byte> Bytes => Encoding.ASCII.GetBytes(this.Content);

    /// <inheritdoc/>
    public override string Content => this.GenerateContent();

    /// <inheritdoc/>
    public T? Value { get; set; }

    /// <inheritdoc/>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.ObjectNumber;
        yield return this.GenerationNumber;
    }

    private string GenerateContent()
    {
        var objectIdentifier = $"{this.ObjectNumber} {this.GenerationNumber} obj\n";

        var stringBuilder = new StringBuilder()
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

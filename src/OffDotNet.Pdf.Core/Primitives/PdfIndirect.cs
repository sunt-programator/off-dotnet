// <copyright file="PdfIndirect.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Text;
using OffDotNet.Pdf.Core.Common;
using OffDotNet.Pdf.Core.Extensions;
using OffDotNet.Pdf.Core.Properties;

namespace OffDotNet.Pdf.Core.Primitives;

public sealed class PdfIndirect<T> : BasePdfObject, IPdfIndirect
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

    public override ReadOnlyMemory<byte> Bytes => Encoding.ASCII.GetBytes(this.Content);

    public override string Content => this.GenerateContent();

    public T? Value { get; set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.ObjectNumber;
        yield return this.GenerationNumber;
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

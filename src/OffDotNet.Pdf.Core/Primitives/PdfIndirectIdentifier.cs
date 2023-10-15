// <copyright file="PdfIndirectIdentifier.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Text;
using OffDotNet.Pdf.Core.Common;

namespace OffDotNet.Pdf.Core.Primitives;

public sealed class PdfIndirectIdentifier<T> : PdfObject, IPdfIndirectIdentifier<T>
    where T : IPdfObject
{
    private readonly Lazy<string> literalValue;
    private readonly Lazy<byte[]> bytes;

    public PdfIndirectIdentifier(IPdfIndirect<T> pdfObject)
    {
        this.literalValue = new Lazy<string>(this.GenerateContent);
        this.bytes = new Lazy<byte[]>(() => Encoding.ASCII.GetBytes(this.Content));

        this.ObjectNumber = pdfObject.ObjectNumber;
        this.GenerationNumber = pdfObject.GenerationNumber;
        this.PdfIndirect = pdfObject;
    }

    public int GenerationNumber { get; }

    public int ObjectNumber { get; }

    public IPdfIndirect<T> PdfIndirect { get; }

    public override ReadOnlyMemory<byte> Bytes => this.bytes.Value;

    public override string Content => this.literalValue.Value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.ObjectNumber;
        yield return this.GenerationNumber;
    }

    private string GenerateContent()
    {
        return $"{this.ObjectNumber} {this.GenerationNumber} R";
    }
}

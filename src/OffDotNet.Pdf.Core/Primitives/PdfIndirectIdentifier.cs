// <copyright file="PdfIndirectIdentifier.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Primitives;

using System.Text;
using OffDotNet.Pdf.Core.Common;

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

    /// <inheritdoc/>
    public int GenerationNumber { get; }

    /// <inheritdoc/>
    public int ObjectNumber { get; }

    /// <inheritdoc/>
    public IPdfIndirect<T> PdfIndirect { get; }

    /// <inheritdoc/>
    public override ReadOnlyMemory<byte> Bytes => this.bytes.Value;

    /// <inheritdoc/>
    public override string Content => this.literalValue.Value;

    /// <inheritdoc/>
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

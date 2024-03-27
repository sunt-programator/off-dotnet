﻿// <copyright file="PdfIndirectIdentifier.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Primitives;

using System.Text;
using Common;

public sealed class PdfIndirectIdentifier<T> : PdfObject, IPdfIndirectIdentifier<T>
    where T : IPdfObject
{
    private readonly Lazy<string> _literalValue;
    private readonly Lazy<byte[]> _bytes;

    public PdfIndirectIdentifier(IPdfIndirect<T> pdfObject)
    {
        _literalValue = new Lazy<string>(this.GenerateContent);
        _bytes = new Lazy<byte[]>(() => Encoding.ASCII.GetBytes(this.Content));

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
    public override ReadOnlyMemory<byte> Bytes => _bytes.Value;

    /// <inheritdoc/>
    public override string Content => _literalValue.Value;

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

// <copyright file="PdfOperation.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Text;
using OffDotNet.Pdf.Core.Interfaces;

namespace OffDotNet.Pdf.Core.ContentStreamAndResources;

public abstract class PdfOperation : IPdfObject
{
    private readonly Lazy<string> literalValue;
    private readonly Lazy<byte[]> bytes;

    protected PdfOperation(string @operator)
    {
        this.PdfOperator = @operator;
        this.literalValue = new Lazy<string>(this.GenerateContent);
        this.bytes = new Lazy<byte[]>(() => Encoding.ASCII.GetBytes(this.Content));
    }

    public string PdfOperator { get; }

    public ReadOnlyMemory<byte> Bytes => this.bytes.Value;

    public string Content => this.literalValue.Value;

    public abstract override int GetHashCode();

    public abstract override bool Equals(object? obj);

    protected abstract string GenerateContent();
}

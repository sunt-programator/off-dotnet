// <copyright file="PdfOperation.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Text;

namespace OffDotNet.Pdf.Core.Common;

public abstract class PdfOperation : BasePdfObject
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

    public override ReadOnlyMemory<byte> Bytes => this.bytes.Value;

    public override string Content => this.literalValue.Value;

    protected abstract string GenerateContent();
}

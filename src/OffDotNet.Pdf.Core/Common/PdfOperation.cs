// <copyright file="PdfOperation.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Common;

using System.Text;

public abstract class PdfOperation : PdfObject, IPdfOperation
{
    private readonly Lazy<string> _literalValue;
    private readonly Lazy<byte[]> _bytes;

    protected PdfOperation(string @operator)
    {
        this.PdfOperator = @operator;
        _literalValue = new Lazy<string>(this.GenerateContent);
        _bytes = new Lazy<byte[]>(() => Encoding.ASCII.GetBytes(this.Content));
    }

    /// <inheritdoc/>
    public string PdfOperator { get; }

    /// <inheritdoc/>
    public override ReadOnlyMemory<byte> Bytes => _bytes.Value;

    /// <inheritdoc/>
    public override string Content => _literalValue.Value;

    protected abstract string GenerateContent();
}

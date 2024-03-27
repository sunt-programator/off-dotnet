﻿// <copyright file="TextObject.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Text;

using System.Text;
using Common;

public sealed class TextObject : PdfObject, ITextObject
{
    private readonly Lazy<string> _literalValue;
    private readonly Lazy<byte[]> _bytes;

    public TextObject(IReadOnlyCollection<IPdfOperation> operations)
    {
        this.Value = operations;

        _literalValue = new Lazy<string>(this.GenerateContent);
        _bytes = new Lazy<byte[]>(() => Encoding.ASCII.GetBytes(this.Content));
    }

    /// <inheritdoc/>
    public override ReadOnlyMemory<byte> Bytes => _bytes.Value;

    /// <inheritdoc/>
    public IReadOnlyCollection<IPdfOperation> Value { get; }

    /// <inheritdoc/>
    public override string Content => _literalValue.Value;

    /// <inheritdoc/>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.Value;
    }

    private string GenerateContent()
    {
        StringBuilder stringBuilder = new();

        foreach (var pdfOperation in this.Value)
        {
            stringBuilder.Append(pdfOperation.Content);
        }

        return stringBuilder
            .Insert(0, "BT\n")
            .Append("ET\n")
            .ToString();
    }
}

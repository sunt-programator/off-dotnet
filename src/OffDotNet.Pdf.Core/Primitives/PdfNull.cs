// <copyright file="PdfNull.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Text;
using OffDotNet.Pdf.Core.Interfaces;

namespace OffDotNet.Pdf.Core.Primitives;

public readonly struct PdfNull : IPdfObject
{
    private const string LiteralValue = "null";
    private static readonly int HashCode = System.HashCode.Combine(nameof(PdfNull), LiteralValue);
    private static readonly ReadOnlyMemory<byte> BytesArray = Encoding.ASCII.GetBytes(LiteralValue);

    public ReadOnlyMemory<byte> Bytes => BytesArray;

    public string Content => LiteralValue;

    public override int GetHashCode()
    {
        return HashCode;
    }

    public override bool Equals(object? obj)
    {
        return obj is PdfNull;
    }
}

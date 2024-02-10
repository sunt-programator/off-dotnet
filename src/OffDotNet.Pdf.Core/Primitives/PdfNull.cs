// <copyright file="PdfNull.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Primitives;

using System.Diagnostics.CodeAnalysis;
using System.Text;
using Common;

[SuppressMessage("Usage", "CA2231:Implement the equality operators and make their behavior identical to that of the Equals method.", Justification = "Not needed.")]
public readonly struct PdfNull : IPdfObject
{
    private const string LiteralValue = "null";
    private static readonly int HashCode = System.HashCode.Combine(nameof(PdfNull), LiteralValue);
    private static readonly ReadOnlyMemory<byte> BytesArray = Encoding.ASCII.GetBytes(LiteralValue);

    /// <inheritdoc/>
    public ReadOnlyMemory<byte> Bytes => BytesArray;

    /// <inheritdoc/>
    public string Content => LiteralValue;

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return HashCode;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is PdfNull;
    }
}

// <copyright file="XRefEntry.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Text;
using OffDotNet.Pdf.Core.Extensions;
using OffDotNet.Pdf.Core.Interfaces;

namespace OffDotNet.Pdf.Core.FileStructure;

public enum XRefEntryType
{
    InUse,
    Free,
}

public sealed class XRefEntry : IPdfObject, IEquatable<XRefEntry>
{
    private readonly Lazy<int> hashCode;
    private readonly Lazy<string> literalValue;
    private readonly Lazy<byte[]> bytes;

    public XRefEntry(long byteOffset, int generationNumber, XRefEntryType entryType)
    {
        this.ByteOffset = byteOffset
            .CheckConstraints(num => num >= 0, Resource.XRefEntry_ByteOffsetMustBePositive)
            .CheckConstraints(num => num <= 9999999999, Resource.XRefEntry_ByteOffsetMustNotExceedMaxAllowedValue);

        this.GenerationNumber = generationNumber
            .CheckConstraints(num => num >= 0, Resource.PdfIndirect_GenerationNumberMustBePositive)
            .CheckConstraints(num => num <= 65535, Resource.PdfIndirect_GenerationNumberMustNotExceedMaxAllowedValue);

        this.EntryType = entryType;

        this.hashCode = new Lazy<int>(() => HashCode.Combine(nameof(XRefEntry), byteOffset, generationNumber));
        this.literalValue = new Lazy<string>(this.GenerateContent);
        this.bytes = new Lazy<byte[]>(() => Encoding.ASCII.GetBytes(this.Content));
    }

    public int Length => this.Bytes.Length;

    public ReadOnlyMemory<byte> Bytes => this.bytes.Value;

    public long ByteOffset { get; }

    public int GenerationNumber { get; }

    public XRefEntryType EntryType { get; }

    public string Content => this.literalValue.Value;

    public override int GetHashCode()
    {
        return this.hashCode.Value;
    }

    public override bool Equals(object? obj)
    {
        return this.Equals(obj as XRefEntry);
    }

    public bool Equals(XRefEntry? other)
    {
        return other is not null &&
               this.ByteOffset == other.ByteOffset &&
               this.GenerationNumber == other.GenerationNumber &&
               this.EntryType == other.EntryType;
    }

    private string GenerateContent()
    {
        char literalEntryType = this.EntryType == XRefEntryType.Free ? 'f' : 'n';
        return $"{this.ByteOffset:D10} {this.GenerationNumber:D5} {literalEntryType} \n";
    }
}

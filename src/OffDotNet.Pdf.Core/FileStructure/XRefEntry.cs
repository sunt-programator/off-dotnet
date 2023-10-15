// <copyright file="XRefEntry.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Text;
using OffDotNet.Pdf.Core.Common;
using OffDotNet.Pdf.Core.Extensions;
using OffDotNet.Pdf.Core.Properties;

namespace OffDotNet.Pdf.Core.FileStructure;

public sealed class XRefEntry : PdfObject, IXRefEntry
{
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

        this.literalValue = new Lazy<string>(this.GenerateContent);
        this.bytes = new Lazy<byte[]>(() => Encoding.ASCII.GetBytes(this.Content));
    }

    public override ReadOnlyMemory<byte> Bytes => this.bytes.Value;

    public long ByteOffset { get; }

    public int GenerationNumber { get; }

    public XRefEntryType EntryType { get; }

    public override string Content => this.literalValue.Value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.ByteOffset;
        yield return this.GenerationNumber;
        yield return this.EntryType;
    }

    private string GenerateContent()
    {
        char literalEntryType = this.EntryType == XRefEntryType.Free ? 'f' : 'n';
        return $"{this.ByteOffset:D10} {this.GenerationNumber:D5} {literalEntryType} \n";
    }
}

// <copyright file="XRefSubSection.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Text;
using OffDotNet.Pdf.Core.Extensions;
using OffDotNet.Pdf.Core.Interfaces;

namespace OffDotNet.Pdf.Core.FileStructure;

public sealed class XRefSubSection : IPdfObject<ICollection<XRefEntry>>, IEquatable<XRefSubSection>
{
    private readonly Lazy<int> hashCode;
    private readonly Lazy<string> literalValue;
    private readonly Lazy<byte[]> bytes;

    public XRefSubSection(int objectNumber, ICollection<XRefEntry> xRefEntries)
    {
        this.ObjectNumber = objectNumber.CheckConstraints(num => num >= 0, Resource.PdfIndirect_ObjectNumberMustBePositive);
        this.Value = xRefEntries.CheckConstraints(entry => entry.Count > 0, Resource.XRefSubSection_MustHaveNonEmptyEntriesCollection);
        this.hashCode = new Lazy<int>(() => HashCode.Combine(nameof(XRefSubSection), objectNumber, xRefEntries));
        this.literalValue = new Lazy<string>(this.GenerateContent);
        this.bytes = new Lazy<byte[]>(() => Encoding.ASCII.GetBytes(this.Content));
    }

    public int ObjectNumber { get; }

    public int NumberOfEntries => this.Value.Count;

    public ReadOnlyMemory<byte> Bytes => this.bytes.Value;

    public ICollection<XRefEntry> Value { get; }

    public string Content => this.literalValue.Value;

    public override int GetHashCode()
    {
        return this.hashCode.Value;
    }

    public override bool Equals(object? obj)
    {
        return this.Equals(obj as XRefSubSection);
    }

    public bool Equals(XRefSubSection? other)
    {
        return other is not null &&
               this.ObjectNumber == other.ObjectNumber &&
               this.Value.SequenceEqual(other.Value);
    }

    private string GenerateContent()
    {
        StringBuilder stringBuilder = new StringBuilder()
            .Append(this.ObjectNumber)
            .Append(' ')
            .Append(this.NumberOfEntries)
            .Append('\n');

        foreach (XRefEntry entry in this.Value)
        {
            stringBuilder.Append(entry.Content);
        }

        return stringBuilder.ToString();
    }
}

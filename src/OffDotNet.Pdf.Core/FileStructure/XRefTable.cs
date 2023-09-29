// <copyright file="XRefTable.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Text;
using OffDotNet.Pdf.Core.Extensions;
using OffDotNet.Pdf.Core.Interfaces;

namespace OffDotNet.Pdf.Core.FileStructure;

public sealed class XRefTable : IPdfObject<ICollection<XRefSection>>, IEquatable<XRefTable>
{
    private readonly Lazy<int> hashCode;
    private readonly Lazy<string> literalValue;
    private readonly Lazy<byte[]> bytes;

    public XRefTable(ICollection<XRefSection> xRefSections)
    {
        this.Value = xRefSections.CheckConstraints(sections => sections.Count > 0, Resource.XRefTable_MustHaveNonEmptyEntriesCollection);
        this.hashCode = new Lazy<int>(() => HashCode.Combine(nameof(XRefTable), xRefSections));
        this.literalValue = new Lazy<string>(this.GenerateContent);
        this.bytes = new Lazy<byte[]>(() => Encoding.ASCII.GetBytes(this.Content));
    }

    public int NumberOfSections => this.Value.Count;

    public ReadOnlyMemory<byte> Bytes => this.bytes.Value;

    public ICollection<XRefSection> Value { get; }

    public string Content => this.literalValue.Value;

    public override int GetHashCode()
    {
        return this.hashCode.Value;
    }

    public override bool Equals(object? obj)
    {
        return this.Equals(obj as XRefTable);
    }

    public bool Equals(XRefTable? other)
    {
        return other is not null &&
               this.Value.SequenceEqual(other.Value);
    }

    private string GenerateContent()
    {
        StringBuilder stringBuilder = new();

        foreach (XRefSection section in this.Value)
        {
            stringBuilder.Append(section.Content);
        }

        return stringBuilder.ToString();
    }
}

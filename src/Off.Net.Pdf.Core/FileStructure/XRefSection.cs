// <copyright file="XRefSection.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Text;
using Off.Net.Pdf.Core.Extensions;
using Off.Net.Pdf.Core.Interfaces;

namespace Off.Net.Pdf.Core.FileStructure;

public sealed class XRefSection : IPdfObject<ICollection<XRefSubSection>>, IEquatable<XRefSection>
{
    private readonly Lazy<int> hashCode;
    private readonly Lazy<string> literalValue;
    private readonly Lazy<byte[]> bytes;

    public XRefSection(ICollection<XRefSubSection> xRefSubSections)
    {
        this.Value = xRefSubSections.CheckConstraints(subSections => subSections.Count > 0, Resource.XRefSection_MustHaveNonEmptyEntriesCollection);
        this.hashCode = new Lazy<int>(() => HashCode.Combine(nameof(XRefSection), xRefSubSections));
        this.literalValue = new Lazy<string>(this.GenerateContent);
        this.bytes = new Lazy<byte[]>(() => Encoding.ASCII.GetBytes(this.Content));
    }

    public int Length => this.Bytes.Length;

    public int NumberOfSubSections => this.Value.Count;

    public ReadOnlyMemory<byte> Bytes => this.bytes.Value;

    public ICollection<XRefSubSection> Value { get; }

    public string Content => this.literalValue.Value;

    public override int GetHashCode()
    {
        return this.hashCode.Value;
    }

    public override bool Equals(object? obj)
    {
        return this.Equals(obj as XRefSection);
    }

    public bool Equals(XRefSection? other)
    {
        return other is not null &&
               this.Value.SequenceEqual(other.Value);
    }

    private string GenerateContent()
    {
        StringBuilder stringBuilder = new StringBuilder()
            .Append("xref")
            .Append('\n');

        foreach (XRefSubSection subSection in this.Value)
        {
            stringBuilder.Append(subSection.Content);
        }

        return stringBuilder.ToString();
    }
}

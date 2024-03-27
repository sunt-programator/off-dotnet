// <copyright file="XRefSection.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.FileStructure;

using System.Text;
using Common;
using Extensions;
using Properties;

public sealed class XRefSection : PdfObject, IxRefSection
{
    private readonly Lazy<string> _literalValue;
    private readonly Lazy<byte[]> _bytes;

    public XRefSection(ICollection<IxRefSubSection> xRefSubSections)
    {
        this.Value = xRefSubSections.CheckConstraints(subSections => subSections.Count > 0, Resource.XRefSection_MustHaveNonEmptyEntriesCollection);
        _literalValue = new Lazy<string>(this.GenerateContent);
        _bytes = new Lazy<byte[]>(() => Encoding.ASCII.GetBytes(this.Content));
    }

    /// <inheritdoc/>
    public override ReadOnlyMemory<byte> Bytes => _bytes.Value;

    /// <inheritdoc/>
    public ICollection<IxRefSubSection> Value { get; }

    /// <inheritdoc/>
    public override string Content => _literalValue.Value;

    /// <inheritdoc/>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        return this.Value;
    }

    private string GenerateContent()
    {
        var stringBuilder = new StringBuilder()
            .Append("xref")
            .Append('\n');

        foreach (var subSection in this.Value)
        {
            stringBuilder.Append(subSection.Content);
        }

        return stringBuilder.ToString();
    }
}

// <copyright file="XRefTable.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.FileStructure;

using System.Text;
using Common;
using Extensions;
using Properties;

public sealed class XRefTable : PdfObject, IxRefTable
{
    private readonly Lazy<string> _literalValue;
    private readonly Lazy<byte[]> _bytes;

    public XRefTable(ICollection<IxRefSection> xRefSections)
    {
        this.Value = xRefSections.CheckConstraints(sections => sections.Count > 0, Resource.XRefTable_MustHaveNonEmptyEntriesCollection);
        _literalValue = new Lazy<string>(this.GenerateContent);
        _bytes = new Lazy<byte[]>(() => Encoding.ASCII.GetBytes(this.Content));
    }

    /// <inheritdoc/>
    public override ReadOnlyMemory<byte> Bytes => _bytes.Value;

    /// <inheritdoc/>
    public ICollection<IxRefSection> Value { get; }

    /// <inheritdoc/>
    public override string Content => _literalValue.Value;

    /// <inheritdoc/>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        return this.Value;
    }

    private string GenerateContent()
    {
        StringBuilder stringBuilder = new();

        foreach (var section in this.Value)
        {
            stringBuilder.Append(section.Content);
        }

        return stringBuilder.ToString();
    }
}

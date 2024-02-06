// <copyright file="XRefTable.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.FileStructure;

using System.Text;
using OffDotNet.Pdf.Core.Common;
using OffDotNet.Pdf.Core.Extensions;
using OffDotNet.Pdf.Core.Properties;

public sealed class XRefTable : PdfObject, IXRefTable
{
    private readonly Lazy<string> literalValue;
    private readonly Lazy<byte[]> bytes;

    public XRefTable(ICollection<IXRefSection> xRefSections)
    {
        this.Value = xRefSections.CheckConstraints(sections => sections.Count > 0, Resource.XRefTable_MustHaveNonEmptyEntriesCollection);
        this.literalValue = new Lazy<string>(this.GenerateContent);
        this.bytes = new Lazy<byte[]>(() => Encoding.ASCII.GetBytes(this.Content));
    }

    /// <inheritdoc/>
    public override ReadOnlyMemory<byte> Bytes => this.bytes.Value;

    /// <inheritdoc/>
    public ICollection<IXRefSection> Value { get; }

    /// <inheritdoc/>
    public override string Content => this.literalValue.Value;

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

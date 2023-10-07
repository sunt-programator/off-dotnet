// <copyright file="XRefTable.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Text;
using OffDotNet.Pdf.Core.Common;
using OffDotNet.Pdf.Core.Extensions;
using OffDotNet.Pdf.Core.Properties;

namespace OffDotNet.Pdf.Core.FileStructure;

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

    public int NumberOfSections => this.Value.Count;

    public override ReadOnlyMemory<byte> Bytes => this.bytes.Value;

    public ICollection<IXRefSection> Value { get; }

    public override string Content => this.literalValue.Value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        return this.Value;
    }

    private string GenerateContent()
    {
        StringBuilder stringBuilder = new();

        foreach (IXRefSection section in this.Value)
        {
            stringBuilder.Append(section.Content);
        }

        return stringBuilder.ToString();
    }
}

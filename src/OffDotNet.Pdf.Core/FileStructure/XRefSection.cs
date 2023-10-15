// <copyright file="XRefSection.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Text;
using OffDotNet.Pdf.Core.Common;
using OffDotNet.Pdf.Core.Extensions;
using OffDotNet.Pdf.Core.Properties;

namespace OffDotNet.Pdf.Core.FileStructure;

public sealed class XRefSection : PdfObject, IXRefSection
{
    private readonly Lazy<string> literalValue;
    private readonly Lazy<byte[]> bytes;

    public XRefSection(ICollection<IXRefSubSection> xRefSubSections)
    {
        this.Value = xRefSubSections.CheckConstraints(subSections => subSections.Count > 0, Resource.XRefSection_MustHaveNonEmptyEntriesCollection);
        this.literalValue = new Lazy<string>(this.GenerateContent);
        this.bytes = new Lazy<byte[]>(() => Encoding.ASCII.GetBytes(this.Content));
    }

    public override ReadOnlyMemory<byte> Bytes => this.bytes.Value;

    public ICollection<IXRefSubSection> Value { get; }

    public override string Content => this.literalValue.Value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        return this.Value;
    }

    private string GenerateContent()
    {
        StringBuilder stringBuilder = new StringBuilder()
            .Append("xref")
            .Append('\n');

        foreach (IXRefSubSection subSection in this.Value)
        {
            stringBuilder.Append(subSection.Content);
        }

        return stringBuilder.ToString();
    }
}

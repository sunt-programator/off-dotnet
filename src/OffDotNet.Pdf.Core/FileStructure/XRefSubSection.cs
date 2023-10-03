// <copyright file="XRefSubSection.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Text;
using OffDotNet.Pdf.Core.Common;
using OffDotNet.Pdf.Core.Extensions;
using OffDotNet.Pdf.Core.Properties;

namespace OffDotNet.Pdf.Core.FileStructure;

public sealed class XRefSubSection : BasePdfObject, IXRefSubSection
{
    private readonly Lazy<string> literalValue;
    private readonly Lazy<byte[]> bytes;

    public XRefSubSection(int objectNumber, ICollection<IXRefEntry> xRefEntries)
    {
        this.ObjectNumber = objectNumber.CheckConstraints(num => num >= 0, Resource.PdfIndirect_ObjectNumberMustBePositive);
        this.Value = xRefEntries.CheckConstraints(entry => entry.Count > 0, Resource.XRefSubSection_MustHaveNonEmptyEntriesCollection);
        this.literalValue = new Lazy<string>(this.GenerateContent);
        this.bytes = new Lazy<byte[]>(() => Encoding.ASCII.GetBytes(this.Content));
    }

    public int ObjectNumber { get; }

    public int NumberOfEntries => this.Value.Count;

    public override ReadOnlyMemory<byte> Bytes => this.bytes.Value;

    public ICollection<IXRefEntry> Value { get; }

    public override string Content => this.literalValue.Value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.ObjectNumber;

        foreach (var xRefEntry in this.Value)
        {
            yield return xRefEntry;
        }
    }

    private string GenerateContent()
    {
        StringBuilder stringBuilder = new StringBuilder()
            .Append(this.ObjectNumber)
            .Append(' ')
            .Append(this.NumberOfEntries)
            .Append('\n');

        foreach (IXRefEntry entry in this.Value)
        {
            stringBuilder.Append(entry.Content);
        }

        return stringBuilder.ToString();
    }
}

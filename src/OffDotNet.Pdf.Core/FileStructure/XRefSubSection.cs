// <copyright file="XRefSubSection.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.FileStructure;

using System.Text;
using Common;
using Extensions;
using Properties;

public sealed class XRefSubSection : PdfObject, IxRefSubSection
{
    private readonly Lazy<string> _literalValue;
    private readonly Lazy<byte[]> _bytes;

    public XRefSubSection(int objectNumber, ICollection<IxRefEntry> xRefEntries)
    {
        this.ObjectNumber = objectNumber.CheckConstraints(num => num >= 0, Resource.PdfIndirect_ObjectNumberMustBePositive);
        this.Value = xRefEntries.CheckConstraints(entry => entry.Count > 0, Resource.XRefSubSection_MustHaveNonEmptyEntriesCollection);
        _literalValue = new Lazy<string>(this.GenerateContent);
        _bytes = new Lazy<byte[]>(() => Encoding.ASCII.GetBytes(this.Content));
    }

    /// <inheritdoc/>
    public int ObjectNumber { get; }

    /// <inheritdoc/>
    public override ReadOnlyMemory<byte> Bytes => _bytes.Value;

    /// <inheritdoc/>
    public ICollection<IxRefEntry> Value { get; }

    /// <inheritdoc/>
    public override string Content => _literalValue.Value;

    /// <inheritdoc/>
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
        var stringBuilder = new StringBuilder()
            .Append(this.ObjectNumber)
            .Append(' ')
            .Append(this.Value.Count)
            .Append('\n');

        foreach (var entry in this.Value)
        {
            stringBuilder.Append(entry.Content);
        }

        return stringBuilder.ToString();
    }
}

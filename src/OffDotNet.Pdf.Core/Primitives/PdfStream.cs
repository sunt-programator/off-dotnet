// <copyright file="PdfStream.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Text;
using OffDotNet.Pdf.Core.Common;

namespace OffDotNet.Pdf.Core.Primitives;

public sealed class PdfStream : PdfObject, IPdfStream
{
    private static readonly PdfName LengthKey = "Length";
    private static readonly PdfName FilterKey = "Filter";
    private static readonly PdfName DecodeParametersKey = "DecodeParms";
    private static readonly PdfName FileFilterKey = "FFilter";
    private static readonly PdfName FileDecodeParametersKey = "FDecodeParms";
    private static readonly PdfName FileSpecificationKey = "F";

    private readonly PdfStreamExtentOptions pdfStreamExtentOptions = new();
    private string literalValue = string.Empty;
    private byte[]? bytes;
    private IPdfDictionary<IPdfObject>? streamExtentDictionary;

    public PdfStream(ReadOnlyMemory<char> value, Action<PdfStreamExtentOptions>? options = null)
    {
        this.bytes = null;
        this.Value = value;
        options?.Invoke(this.pdfStreamExtentOptions);
    }

    public ReadOnlyMemory<char> Value { get; }

    public override ReadOnlyMemory<byte> Bytes => this.bytes ??= Encoding.ASCII.GetBytes(this.Content);

    public override string Content => this.GenerateContent();

    public IPdfDictionary<IPdfObject> StreamExtent => this.GenerateStreamExtendDictionary();

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.Value;
    }

    private string GenerateContent()
    {
        if (this.literalValue.Length != 0)
        {
            return this.literalValue;
        }

        StringBuilder stringBuilder = new StringBuilder()
            .Insert(0, "\nstream\n")
            .Append(this.Value);

        if (stringBuilder[^1] != '\n')
        {
            stringBuilder.Append('\n');
        }

        stringBuilder.Append("endstream");

        this.literalValue = stringBuilder
            .Insert(0, this.StreamExtent.Content)
            .ToString();

        return this.literalValue;
    }

    private IPdfDictionary<IPdfObject> GenerateStreamExtendDictionary()
    {
        if (this.streamExtentDictionary != null)
        {
            return this.streamExtentDictionary;
        }

        int length = this.Value.Length;

        this.streamExtentDictionary = new Dictionary<PdfName, IPdfObject>(6)
            .WithKeyValue(FilterKey, this.pdfStreamExtentOptions.Filter?.PdfObject)
            .WithKeyValue(DecodeParametersKey, this.pdfStreamExtentOptions.DecodeParameters?.PdfObject)
            .WithKeyValue(FileSpecificationKey, this.pdfStreamExtentOptions.FileSpecification)
            .WithKeyValue(FileFilterKey, this.pdfStreamExtentOptions.FileFilter?.PdfObject)
            .WithKeyValue(FileDecodeParametersKey, this.pdfStreamExtentOptions.FileDecodeParameters?.PdfObject)
            .WithKeyValue(LengthKey, (PdfInteger)length)
            .ToPdfDictionary();

        return this.streamExtentDictionary;
    }
}

// <copyright file="PdfStream.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Primitives;

using System.Text;
using Common;

public sealed class PdfStream : PdfObject, IPdfStream
{
    private static readonly PdfName s_lengthKey = "Length";
    private static readonly PdfName s_filterKey = "Filter";
    private static readonly PdfName s_decodeParametersKey = "DecodeParms";
    private static readonly PdfName s_fileFilterKey = "FFilter";
    private static readonly PdfName s_fileDecodeParametersKey = "FDecodeParms";
    private static readonly PdfName s_fileSpecificationKey = "F";

    private readonly PdfStreamExtentOptions _pdfStreamExtentOptions = new();
    private string _literalValue = string.Empty;
    private byte[]? _bytes;
    private IPdfDictionary<IPdfObject>? _streamExtentDictionary;

    public PdfStream(ReadOnlyMemory<char> value, Action<PdfStreamExtentOptions>? options = null)
    {
        _bytes = null;
        this.Value = value;
        options?.Invoke(_pdfStreamExtentOptions);
    }

    /// <inheritdoc/>
    public ReadOnlyMemory<char> Value { get; }

    /// <inheritdoc/>
    public override ReadOnlyMemory<byte> Bytes => _bytes ??= Encoding.ASCII.GetBytes(this.Content);

    /// <inheritdoc/>
    public override string Content => this.GenerateContent();

    /// <inheritdoc/>
    public IPdfDictionary<IPdfObject> StreamExtent => this.GenerateStreamExtendDictionary();

    /// <inheritdoc/>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.Value;
    }

    private string GenerateContent()
    {
        if (_literalValue.Length != 0)
        {
            return _literalValue;
        }

        var stringBuilder = new StringBuilder()
            .Insert(0, "\nstream\n")
            .Append(this.Value);

        if (stringBuilder[^1] != '\n')
        {
            stringBuilder.Append('\n');
        }

        stringBuilder.Append("endstream");

        _literalValue = stringBuilder
            .Insert(0, this.StreamExtent.Content)
            .ToString();

        return _literalValue;
    }

    private IPdfDictionary<IPdfObject> GenerateStreamExtendDictionary()
    {
        if (_streamExtentDictionary != null)
        {
            return _streamExtentDictionary;
        }

        var length = this.Value.Length;

        _streamExtentDictionary = new Dictionary<PdfName, IPdfObject>(6)
            .WithKeyValue(s_filterKey, _pdfStreamExtentOptions.Filter?.PdfObject)
            .WithKeyValue(s_decodeParametersKey, _pdfStreamExtentOptions.DecodeParameters?.PdfObject)
            .WithKeyValue(s_fileSpecificationKey, _pdfStreamExtentOptions.FileSpecification)
            .WithKeyValue(s_fileFilterKey, _pdfStreamExtentOptions.FileFilter?.PdfObject)
            .WithKeyValue(s_fileDecodeParametersKey, _pdfStreamExtentOptions.FileDecodeParameters?.PdfObject)
            .WithKeyValue(s_lengthKey, (PdfInteger)length)
            .ToPdfDictionary();

        return _streamExtentDictionary;
    }
}

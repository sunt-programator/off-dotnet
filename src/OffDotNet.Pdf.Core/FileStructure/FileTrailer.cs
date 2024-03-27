// <copyright file="FileTrailer.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.FileStructure;

using System.Text;
using Common;
using Extensions;
using Primitives;
using Properties;

public sealed class FileTrailer : PdfObject, IFileTrailer
{
    private static readonly PdfName s_size = "Size";
    private static readonly PdfName s_prev = "Prev";
    private static readonly PdfName s_root = "Root";
    private static readonly PdfName s_encrypt = "Encrypt";
    private static readonly PdfName s_info = "Info";
    private static readonly PdfName s_id = "ID";

    private readonly FileTrailerOptions _fileTrailerOptions;
    private readonly Lazy<string> _literalValue;
    private readonly Lazy<byte[]> _bytes;
    private readonly Lazy<IPdfDictionary<IPdfObject>> _fileTrailerDictionary;

    public FileTrailer(long byteOffset, Action<FileTrailerOptions> fileTrailerOptionsFunc)
        : this(byteOffset, GetFileTrailerOptions(fileTrailerOptionsFunc))
    {
    }

    public FileTrailer(long byteOffset, FileTrailerOptions fileTrailerOptions)
    {
        _fileTrailerOptions = fileTrailerOptions;

        this.ByteOffset = byteOffset.CheckConstraints(num => num >= 0, Resource.FileTrailer_ByteOffsetMustBePositive);
        _fileTrailerOptions
            .CheckConstraints(option => option.Size > 0, Resource.FileTrailer_SizeMustBeGreaterThanZero)
            .CheckConstraints(option => option.Prev == null || option.Prev >= 0, Resource.FileTrailer_PrevMustBePositive)
            .CheckConstraints(option => option.Encrypt == null || option.Encrypt.Value.Count > 0, Resource.FileTrailer_EncryptMustHaveANonEmptyCollection)
            .CheckConstraints(option => option.Encrypt == null || option.Id?.Value.Count == 2, Resource.FileTrailer_IdMustBeAnArrayOfTwoByteStrings)
            .NotNull(x => x.Root);

        _literalValue = new Lazy<string>(this.GenerateContent);
        _bytes = new Lazy<byte[]>(() => Encoding.ASCII.GetBytes(this.Content));
        _fileTrailerDictionary = new Lazy<IPdfDictionary<IPdfObject>>(this.GenerateFileTrailerDictionary);
    }

    /// <inheritdoc/>
    public long ByteOffset { get; }

    /// <inheritdoc/>
    public IPdfDictionary<IPdfObject> FileTrailerDictionary => _fileTrailerDictionary.Value;

    /// <inheritdoc/>
    public override ReadOnlyMemory<byte> Bytes => _bytes.Value;

    /// <inheritdoc/>
    public override string Content => _literalValue.Value;

    /// <inheritdoc/>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.Content;
    }

    private static FileTrailerOptions GetFileTrailerOptions(Action<FileTrailerOptions> optionsFunc)
    {
        FileTrailerOptions options = new();
        optionsFunc.Invoke(options);
        return options;
    }

    private string GenerateContent()
    {
        return new StringBuilder()
            .Insert(0, "trailer")
            .Append('\n')
            .Append(_fileTrailerDictionary.Value.Content)
            .Append('\n')
            .Append("startxref")
            .Append('\n')
            .Append(this.ByteOffset)
            .Append('\n')
            .Append("%%EOF")
            .ToString();
    }

    private IPdfDictionary<IPdfObject> GenerateFileTrailerDictionary()
    {
        return new Dictionary<PdfName, IPdfObject>(6)
            .WithKeyValue(s_size, _fileTrailerOptions.Size)
            .WithKeyValue(s_prev, _fileTrailerOptions.Prev)
            .WithKeyValue(s_root, _fileTrailerOptions.Root)
            .WithKeyValue(s_encrypt, _fileTrailerOptions.Encrypt)
            .WithKeyValue(s_info, _fileTrailerOptions.Info)
            .WithKeyValue(s_id, _fileTrailerOptions.Id)
            .ToPdfDictionary();
    }
}

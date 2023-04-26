// <copyright file="FileTrailer.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Text;
using Off.Net.Pdf.Core.Extensions;
using Off.Net.Pdf.Core.Interfaces;
using Off.Net.Pdf.Core.Primitives;

namespace Off.Net.Pdf.Core.FileStructure;

public sealed class FileTrailer : IPdfObject, IEquatable<FileTrailer?>
{
    private static readonly PdfName Size = "Size";
    private static readonly PdfName Prev = "Prev";
    private static readonly PdfName Root = "Root";
    private static readonly PdfName Encrypt = "Encrypt";
    private static readonly PdfName Info = "Info";
    private static readonly PdfName Id = "ID";

    private readonly FileTrailerOptions fileTrailerOptions;
    private readonly Lazy<int> hashCode;
    private readonly Lazy<string> literalValue;
    private readonly Lazy<byte[]> bytes;
    private readonly Lazy<PdfDictionary<IPdfObject>> fileTrailerDictionary;

    public FileTrailer(long byteOffset, Action<FileTrailerOptions> fileTrailerOptionsFunc)
        : this(byteOffset, GetFileTrailerOptions(fileTrailerOptionsFunc))
    {
    }

    public FileTrailer(long byteOffset, FileTrailerOptions fileTrailerOptions)
    {
        this.fileTrailerOptions = fileTrailerOptions;

        this.ByteOffset = byteOffset.CheckConstraints(num => num >= 0, Resource.FileTrailer_ByteOffsetMustBePositive);
        this.fileTrailerOptions
            .CheckConstraints(option => option.Size > 0, Resource.FileTrailer_SizeMustBeGreaterThanZero)
            .CheckConstraints(option => option.Prev == null || option.Prev >= 0, Resource.FileTrailer_PrevMustBePositive)
            .CheckConstraints(option => option.Encrypt == null || option.Encrypt.Value.Count > 0, Resource.FileTrailer_EncryptMustHaveANonEmptyCollection)
            .CheckConstraints(option => option.Encrypt == null || option.Id?.Value.Count == 2, Resource.FileTrailer_IdMustBeAnArrayOfTwoByteStrings)
            .NotNull(x => x.Root);

        this.hashCode = new Lazy<int>(() => HashCode.Combine(nameof(FileTrailer), this.Content));
        this.literalValue = new Lazy<string>(this.GenerateContent);
        this.bytes = new Lazy<byte[]>(() => Encoding.ASCII.GetBytes(this.Content));
        this.fileTrailerDictionary = new Lazy<PdfDictionary<IPdfObject>>(this.GenerateFileTrailerDictionary);
    }

    public int Length => this.Bytes.Length;

    public long ByteOffset { get; }

    public PdfDictionary<IPdfObject> FileTrailerDictionary => this.fileTrailerDictionary.Value;

    public ReadOnlyMemory<byte> Bytes => this.bytes.Value;

    public string Content => this.literalValue.Value;

    public override int GetHashCode()
    {
        return this.hashCode.Value;
    }

    public override bool Equals(object? obj)
    {
        return this.Equals(obj as FileTrailer);
    }

    public bool Equals(FileTrailer? other)
    {
        return other is not null && this.GetHashCode() == other.GetHashCode();
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
            .Append(this.fileTrailerDictionary.Value.Content)
            .Append('\n')
            .Append("startxref")
            .Append('\n')
            .Append(this.ByteOffset)
            .Append('\n')
            .Append("%%EOF")
            .ToString();
    }

    private PdfDictionary<IPdfObject> GenerateFileTrailerDictionary()
    {
        return new Dictionary<PdfName, IPdfObject>(6)
            .WithKeyValue(Size, this.fileTrailerOptions.Size)
            .WithKeyValue(Prev, this.fileTrailerOptions.Prev)
            .WithKeyValue(Root, this.fileTrailerOptions.Root)
            .WithKeyValue(Encrypt, this.fileTrailerOptions.Encrypt)
            .WithKeyValue(Info, this.fileTrailerOptions.Info)
            .WithKeyValue(Id, this.fileTrailerOptions.Id)
            .ToPdfDictionary();
    }
}

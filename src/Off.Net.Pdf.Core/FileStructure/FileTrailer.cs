using System.Text;
using Off.Net.Pdf.Core.DocumentStructure;
using Off.Net.Pdf.Core.Extensions;
using Off.Net.Pdf.Core.Interfaces;
using Off.Net.Pdf.Core.Primitives;

namespace Off.Net.Pdf.Core.FileStructure;

public sealed class FileTrailer : IPdfObject, IEquatable<FileTrailer?>
{
    #region Fields

    private static readonly PdfName Size = "Size";
    private static readonly PdfName Prev = "Prev";
    private static readonly PdfName Root = "Root";
    private static readonly PdfName Encrypt = "Encrypt";
    private static readonly PdfName Info = "Info";
    private static readonly PdfName Id = "ID";

    private readonly FileTrailerOptions _fileTrailerOptions;
    private readonly Lazy<int> _hashCode;
    private readonly Lazy<string> _literalValue;
    private readonly Lazy<byte[]> _bytes;
    private readonly Lazy<PdfDictionary<IPdfObject>> _fileTrailerDictionary;

    #endregion

    #region Constructors

    public FileTrailer(long byteOffset, Action<FileTrailerOptions> fileTrailerOptionsFunc) : this(byteOffset, GetFileTrailerOptions(fileTrailerOptionsFunc))
    {
    }

    public FileTrailer(long byteOffset, FileTrailerOptions fileTrailerOptions)
    {
        _fileTrailerOptions = fileTrailerOptions;

        ByteOffset = byteOffset.CheckConstraints(num => num >= 0, Resource.FileTrailer_ByteOffsetMustBePositive);
        _fileTrailerOptions
            .CheckConstraints(option => option.Size > 0, Resource.FileTrailer_SizeMustBeGreaterThanZero)
            .CheckConstraints(option => option.Prev == null || option.Prev >= 0, Resource.FileTrailer_PrevMustBePositive)
            .CheckConstraints(option => option.Encrypt == null || option.Encrypt.Value.Count > 0, Resource.FileTrailer_EncryptMustHaveANonEmptyCollection)
            .CheckConstraints(option => option.Encrypt == null || option.Id?.Value.Count == 2, Resource.FileTrailer_IdMustBeAnArrayOfTwoByteStrings)
            .NotNull(x => x.Root);

        _hashCode = new Lazy<int>(() => HashCode.Combine(nameof(FileTrailer), Content));
        _literalValue = new Lazy<string>(GenerateContent);
        _bytes = new Lazy<byte[]>(() => Encoding.ASCII.GetBytes(Content));
        _fileTrailerDictionary = new Lazy<PdfDictionary<IPdfObject>>(GenerateFileTrailerDictionary);
    }

    #endregion

    #region Properties

    public int Length => this.Bytes.Length;

    public long ByteOffset { get; }

    public PdfDictionary<IPdfObject> FileTrailerDictionary => _fileTrailerDictionary.Value;

    public ReadOnlyMemory<byte> Bytes => _bytes.Value;

    public string Content => _literalValue.Value;

    #endregion

    #region Public Methods

    public override int GetHashCode()
    {
        return _hashCode.Value;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as FileTrailer);
    }

    public bool Equals(FileTrailer? other)
    {
        return other is not null && GetHashCode() == other.GetHashCode();
    }

    #endregion

    #region Private Methods

    private string GenerateContent()
    {
        return new StringBuilder()
            .Insert(0, "trailer")
            .Append('\n')
            .Append(_fileTrailerDictionary.Value.Content)
            .Append('\n')
            .Append("startxref")
            .Append('\n')
            .Append(ByteOffset)
            .Append('\n')
            .Append("%%EOF")
            .ToString();
    }

    private PdfDictionary<IPdfObject> GenerateFileTrailerDictionary()
    {
        return new Dictionary<PdfName, IPdfObject>(6)
            .WithKeyValue(Size, _fileTrailerOptions.Size)
            .WithKeyValue(Prev, _fileTrailerOptions.Prev)
            .WithKeyValue(Root, _fileTrailerOptions.Root)
            .WithKeyValue(Encrypt, _fileTrailerOptions.Encrypt)
            .WithKeyValue(Info, _fileTrailerOptions.Info)
            .WithKeyValue(Id, _fileTrailerOptions.Id)
            .ToPdfDictionary();
    }

    private static FileTrailerOptions GetFileTrailerOptions(Action<FileTrailerOptions> optionsFunc)
    {
        FileTrailerOptions fileTrailerOptions = new();
        optionsFunc.Invoke(fileTrailerOptions);
        return fileTrailerOptions;
    }

    #endregion
}

public sealed class FileTrailerOptions
{
    public PdfInteger Size { get; set; }

    public PdfInteger? Prev { get; set; }

    public PdfIndirectIdentifier<DocumentCatalog> Root { get; set; } = default!;

    public PdfDictionary<IPdfObject>? Encrypt { get; set; }

    public PdfIndirectIdentifier<PdfDictionary<IPdfObject>>? Info { get; set; }

    public PdfArray<PdfString>? Id { get; set; }
}

public static class FileTrailerExtensions
{
    public static FileTrailer ToFileTrailer(this FileTrailerOptions fileTrailerOptions, long byteOffset)
    {
        return new FileTrailer(byteOffset, fileTrailerOptions);
    }
}

using System.Text;
using Off.Net.Pdf.Core.Extensions;
using Off.Net.Pdf.Core.Interfaces;

namespace Off.Net.Pdf.Core.FileStructure;

public enum XRefEntryType
{
    InUse,
    Free,
}

public sealed class XRefEntry : IPdfObject, IEquatable<XRefEntry>
{
    #region Fields

    private readonly Lazy<int> _hashCode;
    private readonly Lazy<string> _literalValue;
    private readonly Lazy<byte[]> _bytes;

    #endregion

    #region Constructors

    public XRefEntry(long byteOffset, int generationNumber, XRefEntryType entryType)
    {
        ByteOffset = byteOffset
            .CheckConstraints(num => num >= 0, Resource.XRefEntry_ByteOffsetMustBePositive)
            .CheckConstraints(num => num <= 9999999999, Resource.XRefEntry_ByteOffsetMustNotExceedMaxAllowedValue);

        GenerationNumber = generationNumber
            .CheckConstraints(num => num >= 0, Resource.PdfIndirect_GenerationNumberMustBePositive)
            .CheckConstraints(num => num <= 65535, Resource.PdfIndirect_GenerationNumberMustNotExceedMaxAllowedValue);

        EntryType = entryType;

        _hashCode = new Lazy<int>(() => HashCode.Combine(nameof(XRefEntry).GetHashCode(), byteOffset.GetHashCode(), generationNumber.GetHashCode()));
        _literalValue = new Lazy<string>(GenerateContent);
        _bytes = new Lazy<byte[]>(() => Encoding.ASCII.GetBytes(Content));
    }

    #endregion

    #region Properties

    public int Length => Content.Length;

    public ReadOnlyMemory<byte> Bytes => _bytes.Value;

    public long ByteOffset { get; }

    public int GenerationNumber { get; }

    public XRefEntryType EntryType { get; }

    public string Content => _literalValue.Value;

    #endregion

    #region Public Methods

    public override int GetHashCode()
    {
        return _hashCode.Value;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as XRefEntry);
    }

    public bool Equals(XRefEntry? other)
    {
        return other is not null &&
               ByteOffset == other.ByteOffset &&
               GenerationNumber == other.GenerationNumber &&
               EntryType == other.EntryType;
    }

    #endregion

    #region Private Methods

    private string GenerateContent()
    {
        char literalEntryType = EntryType == XRefEntryType.Free ? 'f' : 'n';
        return $"{ByteOffset:D10} {GenerationNumber:D5} {literalEntryType} \n";
    }

    #endregion
}

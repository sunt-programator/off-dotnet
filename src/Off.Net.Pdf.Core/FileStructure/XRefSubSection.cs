using System.Text;
using Off.Net.Pdf.Core.Extensions;
using Off.Net.Pdf.Core.Interfaces;

namespace Off.Net.Pdf.Core.FileStructure;

public sealed class XRefSubSection : IPdfObject<ICollection<XRefEntry>>, IEquatable<XRefSubSection>
{
    #region Fields

    private readonly Lazy<int> _hashCode;
    private readonly Lazy<string> _literalValue;
    private readonly Lazy<byte[]> _bytes;

    #endregion

    #region Constructors

    public XRefSubSection(int objectNumber, ICollection<XRefEntry> xRefEntries)
    {
        ObjectNumber = objectNumber.CheckConstraints(num => num >= 0, Resource.PdfIndirect_ObjectNumberMustBePositive);
        Value = xRefEntries.CheckConstraints(entry => entry.Count > 0, Resource.XRefSubSection_MustHaveNonEmptyEntriesCollection);
        _hashCode = new Lazy<int>(() => HashCode.Combine(nameof(XRefSubSection).GetHashCode(), objectNumber.GetHashCode(), xRefEntries.GetHashCode()));
        _literalValue = new Lazy<string>(GenerateContent);
        _bytes = new Lazy<byte[]>(() => Encoding.ASCII.GetBytes(Content));
    }

    #endregion

    #region Properties

    public int Length => Content.Length;

    public int ObjectNumber { get; }

    public int NumberOfEntries => Value.Count;

    public ReadOnlyMemory<byte> Bytes => _bytes.Value;

    public ICollection<XRefEntry> Value { get; }

    public string Content => _literalValue.Value;

    #endregion

    #region Public Methods

    public override int GetHashCode()
    {
        return _hashCode.Value;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as XRefSubSection);
    }

    public bool Equals(XRefSubSection? other)
    {
        return other is not null &&
               ObjectNumber == other.ObjectNumber &&
               Value.SequenceEqual(other.Value);
    }

    #endregion

    #region Private Methods

    private string GenerateContent()
    {
        StringBuilder stringBuilder = new StringBuilder()
            .Append(this.ObjectNumber)
            .Append(' ')
            .Append(this.NumberOfEntries)
            .Append('\n');

        foreach (XRefEntry entry in Value)
        {
            stringBuilder.Append(entry.Content);
        }

        return stringBuilder.ToString();
    }

    #endregion
}

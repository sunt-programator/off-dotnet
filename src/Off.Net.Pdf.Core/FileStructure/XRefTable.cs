using System.Text;
using Off.Net.Pdf.Core.Extensions;
using Off.Net.Pdf.Core.Interfaces;

namespace Off.Net.Pdf.Core.FileStructure;

public sealed class XRefTable : IPdfObject<ICollection<XRefSection>>, IEquatable<XRefTable>
{
    #region Fields

    private readonly Lazy<int> _hashCode;
    private readonly Lazy<string> _literalValue;
    private readonly Lazy<byte[]> _bytes;

    #endregion

    #region Constructors

    public XRefTable(ICollection<XRefSection> xRefSections)
    {
        Value = xRefSections.CheckConstraints(sections => sections.Count > 0, Resource.XRefTable_MustHaveNonEmptyEntriesCollection);
        _hashCode = new Lazy<int>(() => HashCode.Combine(nameof(XRefTable), xRefSections));
        _literalValue = new Lazy<string>(GenerateContent);
        _bytes = new Lazy<byte[]>(() => Encoding.ASCII.GetBytes(Content));
    }

    #endregion

    #region Properties

    public int Length => Content.Length;

    public int NumberOfSections => Value.Count;

    public ReadOnlyMemory<byte> Bytes => _bytes.Value;

    public ICollection<XRefSection> Value { get; }

    public string Content => _literalValue.Value;

    #endregion

    #region Public Methods

    public override int GetHashCode()
    {
        return _hashCode.Value;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as XRefTable);
    }

    public bool Equals(XRefTable? other)
    {
        return other is not null &&
               Value.SequenceEqual(other.Value);
    }

    #endregion

    #region Private Methods

    private string GenerateContent()
    {
        StringBuilder stringBuilder = new();

        foreach (XRefSection section in Value)
        {
            stringBuilder.Append(section.Content);
        }

        return stringBuilder.ToString();
    }

    #endregion
}

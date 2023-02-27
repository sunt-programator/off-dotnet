using System.Text;
using Off.Net.Pdf.Core.Extensions;
using Off.Net.Pdf.Core.Interfaces;

namespace Off.Net.Pdf.Core.FileStructure;

public sealed class XRefSection : IPdfObject<ICollection<XRefSubSection>>, IEquatable<XRefSection>
{
    #region Fields

    private readonly Lazy<int> _hashCode;
    private readonly Lazy<string> _literalValue;
    private readonly Lazy<byte[]> _bytes;

    #endregion

    #region Constructors

    public XRefSection(ICollection<XRefSubSection> xRefSubSections)
    {
        Value = xRefSubSections.CheckConstraints(subSections => subSections.Count > 0, Resource.XRefSection_MustHaveNonEmptyEntriesCollection);
        _hashCode = new Lazy<int>(() => HashCode.Combine(nameof(XRefSection), xRefSubSections));
        _literalValue = new Lazy<string>(GenerateContent);
        _bytes = new Lazy<byte[]>(() => Encoding.ASCII.GetBytes(Content));
    }

    #endregion

    #region Properties

    public int Length => Content.Length;

    public int NumberOfSubSections => Value.Count;

    public ReadOnlyMemory<byte> Bytes => _bytes.Value;

    public ICollection<XRefSubSection> Value { get; }

    public string Content => _literalValue.Value;

    #endregion

    #region Public Methods

    public override int GetHashCode()
    {
        return _hashCode.Value;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as XRefSection);
    }

    public bool Equals(XRefSection? other)
    {
        return other is not null &&
               Value.SequenceEqual(other.Value);
    }

    #endregion

    #region Private Methods

    private string GenerateContent()
    {
        StringBuilder stringBuilder = new StringBuilder()
            .Append("xref")
            .Append('\n');

        foreach (XRefSubSection subSection in Value)
        {
            stringBuilder.Append(subSection.Content);
        }

        return stringBuilder.ToString();
    }

    #endregion
}

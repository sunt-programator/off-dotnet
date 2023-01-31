using System.Collections.ObjectModel;
using System.Text;
using Off.Net.Pdf.Core.Interfaces;

namespace Off.Net.Pdf.Core.Primitives;

public sealed class PdfDictionary : IPdfObject<IReadOnlyDictionary<PdfName, IPdfObject>>, IEquatable<PdfDictionary?>
{
    #region Fields

    private readonly int _hashCode;
    private string _literalValue = string.Empty;
    private byte[]? _bytes;

    #endregion

    #region Constructors

    public PdfDictionary(IReadOnlyDictionary<PdfName, IPdfObject> value)
    {
        Value = value;
        _hashCode = HashCode.Combine(nameof(PdfDictionary).GetHashCode(), value.GetHashCode());
        _bytes = null;
    }

    #endregion

    #region Properties

    public int Length => Content.Length;

    public IReadOnlyDictionary<PdfName, IPdfObject> Value { get; }

    public ReadOnlyMemory<byte> Bytes => _bytes ??= Encoding.ASCII.GetBytes(Content);

    public string Content => GenerateContent();

    #endregion

    #region Public Methods

    public override int GetHashCode()
    {
        return _hashCode;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as PdfDictionary);
    }

    public bool Equals(PdfDictionary? other)
    {
        return other is not null &&
               EqualityComparer<IReadOnlyDictionary<PdfName, IPdfObject>>.Default.Equals(Value, other.Value);
    }

    public static PdfDictionary CreateRange(IDictionary<PdfName, IPdfObject> items)
    {
        return new PdfDictionary(new ReadOnlyDictionary<PdfName, IPdfObject>(items));
    }

    public static PdfDictionary Create(KeyValuePair<PdfName, IPdfObject> item)
    {
        IDictionary<PdfName, IPdfObject> dictionary = new Dictionary<PdfName, IPdfObject>(1) { { item.Key, item.Value } };
        return new PdfDictionary(new ReadOnlyDictionary<PdfName, IPdfObject>(dictionary));
    }

    #endregion

    #region Private Methods

    private string GenerateContent()
    {
        if (_literalValue.Length != 0)
        {
            return _literalValue;
        }

        StringBuilder stringBuilder = new StringBuilder();

        foreach (var item in Value)
        {
            stringBuilder
                .Append(item.Key.Content)
                .Append(' ')
                .Append(item.Value.Content)
                .Append(' ');
        }

        if (stringBuilder.Length > 0 && stringBuilder[^1] == ' ')
        {
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
        }

        _literalValue = stringBuilder
            .Insert(0, "<<")
            .Append(">>")
            .ToString();

        return _literalValue;
    }

    #endregion
}

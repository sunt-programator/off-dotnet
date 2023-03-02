using System.Collections.ObjectModel;
using System.Text;
using Off.Net.Pdf.Core.Interfaces;

namespace Off.Net.Pdf.Core.Primitives;

public class PdfDictionary<TValue> : IPdfObject<IReadOnlyDictionary<PdfName, TValue>> where TValue: IPdfObject
{
    #region Fields

    private readonly int _hashCode;
    private string _literalValue = string.Empty;
    private byte[]? _bytes;

    #endregion

    #region Constructors

    public PdfDictionary(IReadOnlyDictionary<PdfName, TValue> value)
    {
        Value = value;
        _hashCode = HashCode.Combine(nameof(PdfDictionary<TValue>), value);
        _bytes = null;
    }

    #endregion

    #region Properties

    public int Length => Content.Length;

    public IReadOnlyDictionary<PdfName, TValue> Value { get; }

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
        return obj is PdfDictionary<TValue> other && EqualityComparer<IReadOnlyDictionary<PdfName, TValue>>.Default.Equals(Value, other.Value);
    }

    public static PdfDictionary<TValue> CreateRange(IDictionary<PdfName, TValue> items)
    {
        return new PdfDictionary<TValue>(new ReadOnlyDictionary<PdfName, TValue>(items));
    }

    public static PdfDictionary<TValue> Create(KeyValuePair<PdfName, TValue> item)
    {
        IDictionary<PdfName, TValue> dictionary = new Dictionary<PdfName, TValue>(1) { { item.Key, item.Value } };
        return new PdfDictionary<TValue>(new ReadOnlyDictionary<PdfName, TValue>(dictionary));
    }

    #endregion

    #region Private Methods

    private string GenerateContent()
    {
        if (_literalValue.Length != 0)
        {
            return _literalValue;
        }

        StringBuilder stringBuilder = new();

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

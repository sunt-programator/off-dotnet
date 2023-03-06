using System.Text;
using Off.Net.Pdf.Core.Interfaces;

namespace Off.Net.Pdf.Core.Primitives;

public class PdfArray<TValue> : IPdfObject<IReadOnlyCollection<TValue>> where TValue: IPdfObject
{
    #region Fields
    private readonly int _hashCode;
    private string _literalValue = string.Empty;
    private byte[]? _bytes;
    #endregion

    #region Constructors
    public PdfArray(IReadOnlyCollection<TValue> value)
    {
        Value = value;
        _hashCode = HashCode.Combine(nameof(PdfArray<TValue>), value);
        _bytes = null;
    }
    #endregion

    #region Properties
    public int Length => Content.Length;

    public IReadOnlyCollection<TValue> Value { get; }

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
        return obj is PdfArray<TValue> other && EqualityComparer<IReadOnlyCollection<TValue>>.Default.Equals(Value, other.Value);
    }

    public static PdfArray<TValue> CreateRange(IEnumerable<TValue> items)
    {
        return new PdfArray<TValue>(items.ToList());
    }

    public static PdfArray<TValue> Create(TValue item)
    {
        return new PdfArray<TValue>(new List<TValue>(1) { item });
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
                .Append(item.Content)
                .Append(' ');
        }

        if (stringBuilder.Length > 0 && stringBuilder[^1] == ' ')
        {
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
        }

        _literalValue = stringBuilder
            .Insert(0, '[')
            .Append(']')
            .ToString();

        return _literalValue;
    }
    #endregion
}

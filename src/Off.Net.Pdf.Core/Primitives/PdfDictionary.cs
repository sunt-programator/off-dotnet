using System.Collections.ObjectModel;
using System.Text;
using Off.Net.Pdf.Core.Interfaces;

namespace Off.Net.Pdf.Core.Primitives;

public sealed class PdfDictionary : IPdfObject<IReadOnlyDictionary<PdfName, IPdfObject>>, IEquatable<PdfDictionary?>
{
    #region Fields

    private readonly int hashCode;
    private string literalValue = string.Empty;
    private byte[]? bytes;

    #endregion

    #region Constructors

    public PdfDictionary(IReadOnlyDictionary<PdfName, IPdfObject> value)
    {
        Value = value;
        hashCode = HashCode.Combine(nameof(PdfDictionary).GetHashCode(), value.GetHashCode());
        bytes = null;
    }

    #endregion

    #region Properties

    public int Length => Content.Length;

    public IReadOnlyDictionary<PdfName, IPdfObject> Value { get; }

    public byte[] Bytes => bytes ??= Encoding.ASCII.GetBytes(Content);

    public string Content => GenerateContent();

    #endregion

    #region Public Methods

    public override int GetHashCode()
    {
        return hashCode;
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
        if (literalValue.Length != 0)
        {
            return literalValue;
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

        literalValue = stringBuilder
            .Insert(0, "<<")
            .Append(">>")
            .ToString();

        return literalValue;
    }

    #endregion
}

public static class PdfDictionaryExtensions
{
    public static PdfDictionary ToPdfDictionary(this IDictionary<PdfName, IPdfObject> items)
    {
        return PdfDictionary.CreateRange(items);
    }

    public static PdfDictionary ToPdfDictionary(this KeyValuePair<PdfName, IPdfObject> item)
    {
        return PdfDictionary.Create(item);
    }
}

using System.Text;
using Off.Net.Pdf.Core.Interfaces;

namespace Off.Net.Pdf.Core.Primitives;

public sealed class PdfArray : IPdfObject<IReadOnlyCollection<IPdfObject>>, IEquatable<PdfArray?>
{
    #region Fields
    private readonly int hashCode;
    private string literalValue = string.Empty;
    private byte[]? bytes;
    #endregion

    #region Constructors
    public PdfArray(IReadOnlyCollection<IPdfObject> value)
    {
        Value = value;
        hashCode = HashCode.Combine(nameof(PdfArray).GetHashCode(), value.GetHashCode());
        bytes = null;
    }
    #endregion

    #region Properties
    public int Length => Content.Length;

    public IReadOnlyCollection<IPdfObject> Value { get; }

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
        return Equals(obj as PdfArray);
    }

    public bool Equals(PdfArray? other)
    {
        return other is not null &&
               EqualityComparer<IReadOnlyCollection<IPdfObject>>.Default.Equals(Value, other.Value);
    }

    public static PdfArray CreateRange(IEnumerable<IPdfObject> items)
    {
        return new PdfArray(items.ToList());
    }

    public static PdfArray Create(IPdfObject item)
    {
        return new PdfArray(new List<IPdfObject>(1) { item });
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
                .Append(item.Content)
                .Append(' ');
        }

        if (stringBuilder.Length > 0 && stringBuilder[^1] == ' ')
        {
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
        }

        literalValue = stringBuilder
            .Insert(0, '[')
            .Append(']')
            .ToString();

        return literalValue;
    }
    #endregion
}

public static class PdfArrayExtensions
{
    public static PdfArray ToPdfArray(this IEnumerable<IPdfObject> items)
    {
        return PdfArray.CreateRange(items.ToList());
    }

    public static PdfArray ToPdfArray(this IPdfObject item)
    {
        return PdfArray.Create(item);
    }
}

using System.Globalization;
using System.Text;
using Off.Net.Pdf.Core.Interfaces;

namespace Off.Net.Pdf.Core.Primitives;

public struct PdfInteger : IPdfObject<int>, IEquatable<PdfInteger>, IComparable, IComparable<PdfInteger>
{
    #region Fields
    private readonly int _hashCode;
    private string _literalValue = string.Empty;
    private byte[]? _bytes;
    #endregion

    #region Constructors
    public PdfInteger() : this(0)
    {
    }

    public PdfInteger(int value)
    {
        Value = value;
        _hashCode = HashCode.Combine(nameof(PdfInteger).GetHashCode(), value.GetHashCode());
        _bytes = null;
    }
    #endregion

    #region Properties
    public int Length => Content.Length;

    public int Value { get; }

    public ReadOnlyMemory<byte> Bytes => _bytes ??= Encoding.ASCII.GetBytes(Content);

    public string Content => GenerateContent();
    #endregion

    #region Public Methods
    public override int GetHashCode()
    {
        return _hashCode;
    }

    public bool Equals(PdfInteger other)
    {
        return Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return (obj is PdfInteger integerObject) && Equals(integerObject);
    }

    public int CompareTo(object? obj)
    {
        if (obj is not PdfInteger pdfInteger)
        {
            throw new ArgumentException(Resource.PdfInteger_MustBePdfInteger);
        }

        return CompareTo(pdfInteger);
    }

    public int CompareTo(PdfInteger other)
    {
        if (Value == other.Value)
        {
            return 0;
        }

        return Value > other.Value ? 1 : -1;
    }
    #endregion

    #region Operators
    public static bool operator ==(PdfInteger leftOperator, PdfInteger rightOperator)
    {
        return leftOperator.CompareTo(rightOperator) == 0;
    }

    public static bool operator !=(PdfInteger leftOperator, PdfInteger rightOperator)
    {
        return leftOperator.CompareTo(rightOperator) != 0;
    }

    public static bool operator <(PdfInteger leftOperator, PdfInteger rightOperator)
    {
        return leftOperator.CompareTo(rightOperator) < 0;
    }

    public static bool operator <=(PdfInteger leftOperator, PdfInteger rightOperator)
    {
        return leftOperator.CompareTo(rightOperator) <= 0;
    }

    public static bool operator >(PdfInteger leftOperator, PdfInteger rightOperator)
    {
        return leftOperator.CompareTo(rightOperator) > 0;
    }

    public static bool operator >=(PdfInteger leftOperator, PdfInteger rightOperator)
    {
        return leftOperator.CompareTo(rightOperator) >= 0;
    }

    public static implicit operator int(PdfInteger pdfInteger)
    {
        return pdfInteger.Value;
    }

    public static implicit operator PdfInteger(int value)
    {
        return new(value);
    }
    #endregion

    #region Private Methods
    private string GenerateContent()
    {
        if (_literalValue.Length == 0)
        {
            _literalValue = Value.ToString(CultureInfo.InvariantCulture);
        }

        return _literalValue;
    }
    #endregion
}

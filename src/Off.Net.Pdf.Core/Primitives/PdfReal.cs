using System.Globalization;
using System.Text;
using Off.Net.Pdf.Core.Interfaces;

namespace Off.Net.Pdf.Core.Primitives;

public struct PdfReal : IPdfObject<float>, IEquatable<PdfReal>, IComparable, IComparable<PdfReal>
{
    private const float ApproximationValue = 1.175e-38f;

    #region Fields
    private readonly int _hashCode;
    private string _literalValue = string.Empty;
    private byte[]? _bytes;
    #endregion

    #region Constructors
    public PdfReal() : this(0f)
    {
    }

    public PdfReal(float value)
    {
        if (value >= -ApproximationValue && value <= ApproximationValue)
        {
            value = 0;
        }

        Value = value;
        _hashCode = HashCode.Combine(nameof(PdfReal), value);
        _bytes = null;
    }
    #endregion

    #region Properties
    public int Length => this.Bytes.Length;

    public float Value { get; }

    public ReadOnlyMemory<byte> Bytes => _bytes ??= Encoding.ASCII.GetBytes(Content);

    public string Content => GenerateContent();
    #endregion

    #region Public Methods
    public override int GetHashCode()
    {
        return _hashCode;
    }

    public bool Equals(PdfReal other)
    {
        return Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return (obj is PdfReal pdfReal) && Equals(pdfReal);
    }

    public int CompareTo(object? obj)
    {
        if (obj is not PdfReal pdfReal)
        {
            throw new ArgumentException(Resource.PdfReal_MustBePdfReal);
        }

        return CompareTo(pdfReal);
    }

    public int CompareTo(PdfReal other)
    {
        if (Value == other.Value)
        {
            return 0;
        }

        return Value > other.Value ? 1 : -1;
    }
    #endregion

    #region Operators
    public static bool operator ==(PdfReal leftOperator, PdfReal rightOperator)
    {
        return leftOperator.CompareTo(rightOperator) == 0;
    }

    public static bool operator !=(PdfReal leftOperator, PdfReal rightOperator)
    {
        return leftOperator.CompareTo(rightOperator) != 0;
    }

    public static bool operator <(PdfReal leftOperator, PdfReal rightOperator)
    {
        return leftOperator.CompareTo(rightOperator) < 0;
    }

    public static bool operator <=(PdfReal leftOperator, PdfReal rightOperator)
    {
        return leftOperator.CompareTo(rightOperator) <= 0;
    }

    public static bool operator >(PdfReal leftOperator, PdfReal rightOperator)
    {
        return leftOperator.CompareTo(rightOperator) > 0;
    }

    public static bool operator >=(PdfReal leftOperator, PdfReal rightOperator)
    {
        return leftOperator.CompareTo(rightOperator) >= 0;
    }

    public static implicit operator float(PdfReal pdfReal)
    {
        return pdfReal.Value;
    }

    public static implicit operator PdfReal(float value)
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

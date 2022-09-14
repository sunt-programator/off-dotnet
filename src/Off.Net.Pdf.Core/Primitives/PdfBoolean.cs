namespace Off.Net.Pdf.Core.Primitives;

using System.Text;
using Off.Net.Pdf.Core.Interfaces;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S1210:\"Equals\" and the comparison operators should be overridden when implementing \"IComparable\"", Justification = "Other operators, except == and !=, are not required.")]
public struct PdfBoolean : IPdfObject<bool>, IEquatable<PdfBoolean>, IComparable, IComparable<PdfBoolean>
{
    #region Fields
    private readonly string stringValue;
    #endregion

    #region Constructors
    public PdfBoolean() : this(false)
    {
    }

    public PdfBoolean(bool value)
    {
        Value = value;
        stringValue = value ? "true" : "false";
        Bytes = Encoding.ASCII.GetBytes(stringValue);
    }
    #endregion

    #region Properties
    public int Length => stringValue.Length;

    public bool Value { get; }

    public byte[] Bytes { get; }
    #endregion

    #region Public Methods
    public override int GetHashCode()
    {
        return HashCode.Combine(nameof(PdfBoolean).GetHashCode(), Value.GetHashCode());
    }

    public override string ToString()
    {
        return stringValue;
    }

    public bool Equals(PdfBoolean other)
    {
        return Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return (obj is PdfBoolean booleanObject) && Equals(booleanObject);
    }

    public int CompareTo(PdfBoolean other)
    {
        if (Value == other.Value)
        {
            return 0;
        }

        return Value ? 1 : -1;
    }

    public int CompareTo(object? obj)
    {
        if (obj is not PdfBoolean pdfBoolean)
        {
            throw new ArgumentException(Resource.Arg_MustBePdfBoolean);
        }

        return CompareTo(pdfBoolean);
    }
    #endregion

    #region Operators
    public static bool operator ==(PdfBoolean leftOperator, PdfBoolean rightOperator)
    {
        return leftOperator.CompareTo(rightOperator) == 0;
    }

    public static bool operator !=(PdfBoolean leftOperator, PdfBoolean rightOperator)
    {
        return leftOperator.CompareTo(rightOperator) != 0;
    }

    public static implicit operator bool(PdfBoolean pdfBoolean)
    {
        return pdfBoolean.Value;
    }

    public static implicit operator PdfBoolean(bool value)
    {
        return new PdfBoolean(value);
    }
    #endregion
}

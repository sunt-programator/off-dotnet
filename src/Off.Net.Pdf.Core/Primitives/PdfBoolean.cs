namespace Off.Net.Pdf.Core.Primitives;

using System.Text;
using Off.Net.Pdf.Core.Interfaces;

public struct PdfBoolean : IPdfObject<bool>, IEquatable<PdfBoolean>
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
    #endregion

    #region Operators
    public static bool operator ==(PdfBoolean leftOperator, PdfBoolean rightOperator)
    {
        return leftOperator.Equals(rightOperator);
    }

    public static bool operator !=(PdfBoolean leftOperator, PdfBoolean rightOperator)
    {
        return !leftOperator.Equals(rightOperator);
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

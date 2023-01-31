using System.Text;
using Off.Net.Pdf.Core.Interfaces;

namespace Off.Net.Pdf.Core.Primitives;

public struct PdfBoolean : IPdfObject<bool>, IEquatable<PdfBoolean>
{
    #region Constructors
    public PdfBoolean() : this(false)
    {
    }

    public PdfBoolean(bool value)
    {
        Value = value;
        Content = value ? "true" : "false";
        Bytes = Encoding.ASCII.GetBytes(Content);
    }
    #endregion

    #region Properties
    public int Length => Content.Length;

    public bool Value { get; }

    public ReadOnlyMemory<byte> Bytes { get; }

    public string Content { get; }
    #endregion

    #region Public Methods
    public override int GetHashCode()
    {
        return HashCode.Combine(nameof(PdfBoolean).GetHashCode(), Value.GetHashCode());
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

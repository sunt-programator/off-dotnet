using System.Globalization;
using System.Text;
using Off.Net.Pdf.Core.Interfaces;

namespace Off.Net.Pdf.Core.Primitives;

public sealed class PdfName : IPdfObject<string>, IEquatable<PdfName>
{
    #region Fields
    private const string NumberChar = "#23"; // '#'
    private const string SolidusChar = "#2F"; // '/'
    private const string PercentChar = "#25"; // '/'
    private const string LeftParanthesisChar = "#28"; // '('
    private const string RightParanthesisChar = "#29"; // ')'
    private const string GreaterThanChar = "#3E"; // '>'
    private const string LessThanChar = "#3C"; // '<'
    private const string LeftSquareBracketChar = "#5B"; // '['
    private const string RightSquareBracketChar = "#5D"; // ']'
    private const string LeftCurlyBracketChar = "#7B"; // '{'
    private const string RightCurlyBracketChar = "#7D"; // '}'
    private readonly int _hashCode;
    private string _literalValue = string.Empty;
    private byte[]? _bytes;
    #endregion

    #region Constructors
    public PdfName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentNullException(nameof(value), Resource.PdfName_CannotBeNullOrWhitespace);
        }

        Value = value;
        _hashCode = HashCode.Combine(nameof(PdfName).GetHashCode(), value.GetHashCode());
        _bytes = null;
    }
    #endregion

    #region Properties
    public int Length => Content.Length;

    public string Value { get; }

    public ReadOnlyMemory<byte> Bytes => _bytes ??= Encoding.ASCII.GetBytes(Content);

    public string Content => GenerateContent();
    #endregion

    #region Public Methods
    public override int GetHashCode()
    {
        return _hashCode;
    }

    public bool Equals(PdfName? other)
    {
        if (other is not PdfName pdfName)
        {
            return false;
        }

        return Value == pdfName.Value;
    }

    public override bool Equals(object? obj)
    {
        return (obj is PdfName pdfName) && Equals(pdfName);
    }

    #endregion

    #region Operators
    public static bool operator ==(PdfName leftOperator, PdfName rightOperator)
    {
        return leftOperator.Equals(rightOperator);
    }

    public static bool operator !=(PdfName leftOperator, PdfName rightOperator)
    {
        return !leftOperator.Equals(rightOperator);
    }

    public static implicit operator string(PdfName pdfName)
    {
        return pdfName.Value;
    }

    public static implicit operator PdfName(string value)
    {
        return new(value);
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

        foreach (char ch in Value)
        {
            stringBuilder.Append(ConvertCharToString(ch));
        }

        _literalValue = stringBuilder
            .Insert(0, '/')
            .ToString();

        return _literalValue;
    }

    private static string ConvertCharToString(char ch)
    {
        return ch switch
        {
            '#' => NumberChar,
            '/' => SolidusChar,
            '%' => PercentChar,
            '(' => LeftParanthesisChar,
            ')' => RightParanthesisChar,
            '>' => GreaterThanChar,
            '<' => LessThanChar,
            '[' => LeftSquareBracketChar,
            ']' => RightSquareBracketChar,
            '{' => LeftCurlyBracketChar,
            '}' => RightCurlyBracketChar,
            char regularChar when regularChar <= 0x20 || regularChar >= 0x7F => $"#{Convert.ToByte(ch).ToString("X2", CultureInfo.InvariantCulture)}",
            _ => ch.ToString(),
        };
    }
    #endregion
}

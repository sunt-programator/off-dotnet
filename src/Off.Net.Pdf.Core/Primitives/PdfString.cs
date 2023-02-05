using System.Text;
using Off.Net.Pdf.Core.Interfaces;

namespace Off.Net.Pdf.Core.Primitives;

public sealed class PdfString : IPdfObject<string>, IEquatable<PdfString>
{
    #region Fields
    private readonly int _hashCode;
    private readonly bool _isHexString;
    private string _literalValue = string.Empty;
    private byte[]? _bytes;
    #endregion

    #region Constructors
    public PdfString(string value) : this(value, false)
    {
    }

    public PdfString(string value, bool isHexString)
    {
        ThrowExceptionIfValueIsNotValid(value, isHexString);
        Value = value;
        _hashCode = HashCode.Combine(nameof(PdfString).GetHashCode(), value.GetHashCode());
        _bytes = null;
        this._isHexString = isHexString;
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

    public bool Equals(PdfString? other)
    {
        if (other is not PdfString pdfName)
        {
            return false;
        }

        return Value == pdfName.Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is PdfString pdfName && Equals(pdfName);
    }

    #endregion

    #region Operators
    public static bool operator ==(PdfString leftOperator, PdfString rightOperator)
    {
        return leftOperator.Equals(rightOperator);
    }

    public static bool operator !=(PdfString leftOperator, PdfString rightOperator)
    {
        return !leftOperator.Equals(rightOperator);
    }

    public static implicit operator string(PdfString pdfName)
    {
        return pdfName.Value;
    }

    public static implicit operator PdfString(string value)
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

        for (int i = 0; i < Value.Length; i++)
        {
            stringBuilder.Append(Value[i]);
        }

        _literalValue = stringBuilder
            .Insert(0, _isHexString ? '<' : '(')
            .Append(_isHexString ? '>' : ')')
            .ToString();

        return _literalValue;
    }

    private static void ThrowExceptionIfValueIsNotValid(string value, bool isHexString)
    {
        if (!isHexString)
        {
            ValidateStringValue(value);
            return;
        }

        ValidateHexValue(value);
    }

    private static void ValidateStringValue(string value)
    {
        if (!ContainsUnbalancedParentheses(value))
        {
            throw new ArgumentException(Resource.PdfString_MustHaveBalancedParentheses, nameof(value));
        }

        if (!ContainsValidSolidusChars(value))
        {
            throw new ArgumentException(Resource.PdfString_MustHaveValidSolidusChars, nameof(value));
        }
    }

    private static void ValidateHexValue(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentNullException(nameof(value), Resource.PdfString_MustNotBeEmpty);
        }

        if (!IsValidHexValue(value))
        {
            throw new ArgumentException(Resource.PdfString_MustHaveValidHexValue, nameof(value));
        }
    }

    private static bool ContainsUnbalancedParentheses(string value)
    {
        int parenthesesBalance = 0;

        foreach (char ch in value)
        {
            if (parenthesesBalance < 0)
            {
                return false;
            }

            if (ch == '(')
            {
                parenthesesBalance++;
                continue;
            }

            if (ch == ')')
            {
                parenthesesBalance--;
            }
        }

        return parenthesesBalance == 0;
    }

    private static bool ContainsValidSolidusChars(string value)
    {
        for (int i = 0; i < value.Length; i++)
        {
            if (value[i] != '\\')
            {
                continue;
            }

            int nextIndex = i + 1;
            if (nextIndex == value.Length)
            {
                return false;
            }

            if (!IsValidNextCharAfterSolidus(value[nextIndex]))
            {
                return false;
            }
        }

        return true;
    }

    private static bool IsValidNextCharAfterSolidus(char ch)
    {
        return ch switch
        {
            '\\' or '(' or ')' or '\n' or '\r' or '\t' or '\b' or '\f' => true,
            char numberChar when numberChar >= '0' && numberChar <= '9' => true,
            _ => false,
        };
    }

    private static bool IsValidHexValue(string value)
    {
        foreach (char ch in value)
        {
            if (!IsValidHexChar(ch))
            {
                return false;
            }
        }

        return true;
    }

    private static bool IsValidHexChar(char ch)
    {
        return ch switch
        {
            char letterChar when letterChar >= 'a' && letterChar <= 'f' => true,
            char capitalLetterChar when capitalLetterChar >= 'A' && capitalLetterChar <= 'F' => true,
            char numberChar when numberChar >= '0' && numberChar <= '9' => true,
            ' ' or '\t' or '\r' or '\n' or '\f' => true,
            _ => false
        };
    }
    #endregion
}

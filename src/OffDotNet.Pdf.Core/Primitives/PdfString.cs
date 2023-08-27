// <copyright file="PdfString.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Text;
using OffDotNet.Pdf.Core.Interfaces;

namespace OffDotNet.Pdf.Core.Primitives;

public sealed class PdfString : IPdfObject<string>, IEquatable<PdfString>
{
    private readonly int hashCode;
    private readonly bool isHexString;
    private string literalValue = string.Empty;
    private byte[]? bytes;

    public PdfString(string value)
        : this(value, false)
    {
    }

    public PdfString(string value, bool isHexString)
    {
        ThrowExceptionIfValueIsNotValid(value, isHexString);
        this.Value = value;
        this.hashCode = HashCode.Combine(nameof(PdfString), value);
        this.bytes = null;
        this.isHexString = isHexString;
    }

    public int Length => this.Bytes.Length;

    public string Value { get; }

    public ReadOnlyMemory<byte> Bytes => this.bytes ??= Encoding.ASCII.GetBytes(this.Content);

    public string Content => this.GenerateContent();

    public static implicit operator string(PdfString pdfName)
    {
        return pdfName.Value;
    }

    public static implicit operator PdfString(string value)
    {
        return new(value);
    }

    public static bool operator ==(PdfString leftOperator, PdfString rightOperator)
    {
        return leftOperator.Equals(rightOperator);
    }

    public static bool operator !=(PdfString leftOperator, PdfString rightOperator)
    {
        return !leftOperator.Equals(rightOperator);
    }

    public override int GetHashCode()
    {
        return this.hashCode;
    }

    public bool Equals(PdfString? other)
    {
        if (other is not PdfString pdfName)
        {
            return false;
        }

        return this.Value == pdfName.Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is PdfString pdfName && this.Equals(pdfName);
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
            _ => false,
        };
    }

    private string GenerateContent()
    {
        if (this.literalValue.Length != 0)
        {
            return this.literalValue;
        }

        StringBuilder stringBuilder = new();

        for (int i = 0; i < this.Value.Length; i++)
        {
            stringBuilder.Append(this.Value[i]);
        }

        this.literalValue = stringBuilder
            .Insert(0, this.isHexString ? '<' : '(')
            .Append(this.isHexString ? '>' : ')')
            .ToString();

        return this.literalValue;
    }
}

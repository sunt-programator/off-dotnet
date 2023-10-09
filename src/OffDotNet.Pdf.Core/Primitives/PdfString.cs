// <copyright file="PdfString.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Text;
using OffDotNet.Pdf.Core.Common;
using OffDotNet.Pdf.Core.Properties;

namespace OffDotNet.Pdf.Core.Primitives;

public sealed class PdfString : PdfObject
{
    private readonly bool isHexString;
    private string literalValue = string.Empty;
    private byte[]? bytes;

    public PdfString(string value, bool isHexString = false)
    {
        ThrowExceptionIfValueIsNotValid(value, isHexString);
        this.Value = value;
        this.bytes = null;
        this.isHexString = isHexString;
    }

    public string Value { get; }

    public override ReadOnlyMemory<byte> Bytes => this.bytes ??= Encoding.ASCII.GetBytes(this.Content);

    public override string Content => this.GenerateContent();

    public static implicit operator string(PdfString pdfName)
    {
        return pdfName.Value;
    }

    public static implicit operator PdfString(string value)
    {
        return new PdfString(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.Value;
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
            >= '0' and <= '9' => true,
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
            >= 'a' and <= 'f' => true,
            >= 'A' and <= 'F' => true,
            >= '0' and <= '9' => true,
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

        foreach (char ch in this.Value)
        {
            stringBuilder.Append(ch);
        }

        this.literalValue = stringBuilder
            .Insert(0, this.isHexString ? '<' : '(')
            .Append(this.isHexString ? '>' : ')')
            .ToString();

        return this.literalValue;
    }
}

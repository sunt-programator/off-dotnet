// <copyright file="PdfName.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Globalization;
using System.Text;
using Off.Net.Pdf.Core.Interfaces;

namespace Off.Net.Pdf.Core.Primitives;

public sealed class PdfName : IPdfObject<string>, IEquatable<PdfName>
{
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
    private readonly int hashCode;
    private string literalValue = string.Empty;
    private byte[]? bytes;

    public PdfName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentNullException(nameof(value), Resource.PdfName_CannotBeNullOrWhitespace);
        }

        this.Value = value;
        this.hashCode = HashCode.Combine(nameof(PdfName), value);
        this.bytes = null;
    }

    public int Length => this.Bytes.Length;

    public string Value { get; }

    public ReadOnlyMemory<byte> Bytes => this.bytes ??= Encoding.ASCII.GetBytes(this.Content);

    public string Content => this.GenerateContent();

    public static implicit operator string(PdfName pdfName)
    {
        return pdfName.Value;
    }

    public static implicit operator PdfName(string value)
    {
        return new(value);
    }

    public static bool operator ==(PdfName leftOperator, PdfName rightOperator)
    {
        return leftOperator.Equals(rightOperator);
    }

    public static bool operator !=(PdfName leftOperator, PdfName rightOperator)
    {
        return !leftOperator.Equals(rightOperator);
    }

    public override int GetHashCode()
    {
        return this.hashCode;
    }

    public bool Equals(PdfName? other)
    {
        if (other is not PdfName pdfName)
        {
            return false;
        }

        return this.Value == pdfName.Value;
    }

    public override bool Equals(object? obj)
    {
        return (obj is PdfName pdfName) && this.Equals(pdfName);
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

    private string GenerateContent()
    {
        if (this.literalValue.Length != 0)
        {
            return this.literalValue;
        }

        StringBuilder stringBuilder = new();

        foreach (char ch in this.Value)
        {
            stringBuilder.Append(ConvertCharToString(ch));
        }

        this.literalValue = stringBuilder
            .Insert(0, '/')
            .ToString();

        return this.literalValue;
    }
}

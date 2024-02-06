// <copyright file="PdfName.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Primitives;

using System.Globalization;
using System.Text;
using OffDotNet.Pdf.Core.Common;
using OffDotNet.Pdf.Core.Properties;

public sealed class PdfName : PdfObject
{
    private const string NumberChar = "#23"; // '#'
    private const string SolidusChar = "#2F"; // '/'
    private const string PercentChar = "#25"; // '/'
    private const string LeftParenthesisChar = "#28"; // '('
    private const string RightParenthesisChar = "#29"; // ')'
    private const string GreaterThanChar = "#3E"; // '>'
    private const string LessThanChar = "#3C"; // '<'
    private const string LeftSquareBracketChar = "#5B"; // '['
    private const string RightSquareBracketChar = "#5D"; // ']'
    private const string LeftCurlyBracketChar = "#7B"; // '{'
    private const string RightCurlyBracketChar = "#7D"; // '}'
    private string literalValue = string.Empty;
    private byte[]? bytes;

    public PdfName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentNullException(nameof(value), Resource.PdfName_CannotBeNullOrWhitespace);
        }

        this.Value = value;
        this.bytes = null;
    }

    public string Value { get; }

    /// <inheritdoc/>
    public override ReadOnlyMemory<byte> Bytes => this.bytes ??= Encoding.ASCII.GetBytes(this.Content);

    /// <inheritdoc/>
    public override string Content => this.GenerateContent();

    public static implicit operator string(PdfName pdfName)
    {
        return pdfName.Value;
    }

    public static implicit operator PdfName(string value)
    {
        return new PdfName(value);
    }

    /// <inheritdoc/>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.Value;
    }

    private static string ConvertCharToString(char ch)
    {
        return ch switch
        {
            '#' => NumberChar,
            '/' => SolidusChar,
            '%' => PercentChar,
            '(' => LeftParenthesisChar,
            ')' => RightParenthesisChar,
            '>' => GreaterThanChar,
            '<' => LessThanChar,
            '[' => LeftSquareBracketChar,
            ']' => RightSquareBracketChar,
            '{' => LeftCurlyBracketChar,
            '}' => RightCurlyBracketChar,
            _ when ch <= 0x20 || ch >= 0x7F => $"#{Convert.ToByte(ch).ToString("X2", CultureInfo.InvariantCulture)}",
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

        foreach (var ch in this.Value)
        {
            stringBuilder.Append(ConvertCharToString(ch));
        }

        this.literalValue = stringBuilder
            .Insert(0, '/')
            .ToString();

        return this.literalValue;
    }
}

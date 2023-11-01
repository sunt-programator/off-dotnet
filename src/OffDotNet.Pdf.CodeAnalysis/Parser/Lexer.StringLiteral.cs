// <copyright file="Lexer.StringLiteral.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Buffers;
using System.Diagnostics;
using System.Text;
using OffDotNet.Pdf.CodeAnalysis.Errors;
using OffDotNet.Pdf.CodeAnalysis.Parser;

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

internal partial class Lexer
{
    private void ScanStringLiteral(ref TokenInfo info)
    {
        byte? leftParenthesis = this.textWindow.NextByte();
        Debug.Assert(leftParenthesis is 0x28, "String literal must start with left parenthesis.");

        info.Text = string.Empty;
        info.ValueKind = TokenInfoSpecialKind.None;
        this.stringBuilder.Clear();

        int balancedParenthesisCount = 1;

        while (true)
        {
            byte? peekedByte = this.textWindow.PeekByte();

            if (!peekedByte.HasValue)
            {
                break;
            }

            switch (peekedByte.Value)
            {
                case 0x5c: // '\'
                    peekedByte = this.ScanEscapeSequence();
                    break;
                case 0x28: // '('
                    balancedParenthesisCount++;
                    break;
                case 0x29: // ')'
                    balancedParenthesisCount--;
                    break;
                case 0x0d: // '\r'
                    this.ScanToEndOfLine();
                    peekedByte = this.textWindow.TryAdvanceByte(1, 0x0a) ? 0x0a : peekedByte; // for '\r\n' case
                    break;
            }

            if (balancedParenthesisCount == 0)
            {
                this.textWindow.AdvanceByte();
                break;
            }

            if (!peekedByte.HasValue)
            {
                continue;
            }

            this.stringBuilder.Append((char)peekedByte.Value);
            this.textWindow.AdvanceByte();
        }

        if (balancedParenthesisCount != 0)
        {
            this.AddError(MakeError(ErrorCode.ErrorStringUnbalancedParenthesis));
        }

        info.Kind = SyntaxKind.StringLiteralToken;
        info.Text = this.textWindow.GetText();
        info.StringValue = this.stringBuilder.ToString();
    }

    private byte? ScanEscapeSequence()
    {
        byte? peekedByte = this.textWindow.NextByte();
        Debug.Assert(peekedByte is 0x5c, "Escape sequence must start with reverse solidus.");

        peekedByte = this.textWindow.PeekByte();

        if (!peekedByte.HasValue)
        {
            this.AddError(MakeError(ErrorCode.WarnStringEscapeSequenceIncomplete));
            return null;
        }

        switch (peekedByte.Value)
        {
            case 0x6e: // 'n'
                return 0x0a;
            case 0x72: // 'r'
                return 0x0d;
            case 0x74: // 't'
                return 0x09;
            case 0x62: // 'b'
                return 0x08;
            case 0x66: // 'f'
                return 0x0c;
            case 0x5c: // '\'
                return 0x5c;
            case 0x28: // '('
                return 0x28;
            case 0x29: // ')'
                return 0x29;
            case 0x0a: // '\n'
            case 0x0d: // '\r'
                this.ScanToEndOfLine();
                this.textWindow.AdvanceByte();
                this.textWindow.TryAdvanceByte(0x0a); // for '\r\n' case
                return null;
            case >= 0x30 and <= 0x37: // '0-7', for octal (\ddd) escape sequence
                return this.ScanOctalEscapeSequence();
            default:
                this.AddError(MakeError(ErrorCode.WarnStringEscapeSequenceInvalid));
                return null;
        }
    }

    private byte? ScanOctalEscapeSequence()
    {
        byte? peekedByte = this.textWindow.PeekByte();
        Debug.Assert(peekedByte is >= 0x30 and <= 0x37, "Octal escape sequence must start with digit.");

        const int maxOctDigits = 3;
        int i;

        ArrayPool<byte> arrayPool = ArrayPool<byte>.Shared;
        byte[] bytes = arrayPool.Rent(maxOctDigits);

        for (i = 0; i < maxOctDigits; i++)
        {
            peekedByte = this.textWindow.PeekByte(i);

            if (!peekedByte.HasValue || !peekedByte.IsOctDigit())
            {
                break;
            }

            bytes[i] = peekedByte.Value;
        }

        string octStringValue = Encoding.ASCII.GetString(bytes.AsSpan(..i));
        short decValue = Convert.ToInt16(octStringValue, 8);
        arrayPool.Return(bytes);

        if (decValue > 255)
        {
            this.AddError(MakeError(ErrorCode.WarningStringOverflowOctValue));
            this.textWindow.AdvanceByte(i);
            return null;
        }

        this.textWindow.AdvanceByte(i - 1);
        return (byte)decValue;
    }
}

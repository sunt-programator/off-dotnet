// <copyright file="Lexer.NameLiteral.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Buffers;
using System.Diagnostics;
using System.Text;
using OffDotNet.Pdf.CodeAnalysis.Old.Errors;
using OffDotNet.Pdf.CodeAnalysis.Old.Syntax;

namespace OffDotNet.Pdf.CodeAnalysis.Old.Parser;

internal partial class Lexer
{
    private void ScanNameLiteral(ref TokenInfo info)
    {
        byte? leftParenthesis = this.textWindow.NextByte();
        Debug.Assert(leftParenthesis is 0x2f, "String name literal must start with solidus character.");

        info.Text = string.Empty;
        info.ValueKind = TokenInfoSpecialKind.None;
        this.stringBuilder.Clear();

        while (true)
        {
            byte? peekedByte = this.textWindow.PeekByte();

            if (!peekedByte.HasValue)
            {
                break;
            }

            switch (peekedByte.Value)
            {
                case 0x23: // '#'
                    peekedByte = this.ScanHexadecimalEscapeSequence();
                    break;
                case 0x0:
                    peekedByte = this.textWindow.NextByte(1);
                    this.AddError(MakeError(ErrorCode.ErrorNameNullCharacterNotAllowed));
                    break;
                case < 0x21 or > 0x7e: // Characters outside of the range 0x21 (!) to 0x7e (~) should be escaped using #xx notation.
                    break;
            }

            if (!peekedByte.HasValue)
            {
                continue;
            }

            this.stringBuilder.Append((char)peekedByte.Value);
            this.textWindow.AdvanceByte();
        }

        info.Kind = SyntaxKind.NameLiteralToken;
        info.Text = this.textWindow.GetText();
        info.StringValue = this.stringBuilder.ToString();
    }

    private byte? ScanHexadecimalEscapeSequence()
    {
        byte? peekedByte = this.textWindow.NextByte();
        Debug.Assert(peekedByte is 0x23, "Escape hexadecimal sequence must start with '#' character.");

        peekedByte = this.textWindow.PeekByte();

        if (!peekedByte.HasValue)
        {
            this.AddError(MakeError(ErrorCode.ErrorNameHexSequenceIncomplete));
            return null;
        }

        const int maxHexDigits = 2;
        int i;

        ArrayPool<byte> arrayPool = ArrayPool<byte>.Shared;
        byte[] bytes = arrayPool.Rent(maxHexDigits);

        for (i = 0; i < maxHexDigits; i++)
        {
            peekedByte = this.textWindow.PeekByte(i);

            if (!peekedByte.HasValue || !peekedByte.IsHexDigit())
            {
                break;
            }

            bytes[i] = peekedByte.Value;
        }

        string octStringValue = Encoding.ASCII.GetString(bytes.AsSpan(..i));
        short decValue = Convert.ToInt16(octStringValue, 16);
        arrayPool.Return(bytes);

        if (decValue == 0)
        {
            this.AddError(MakeError(ErrorCode.ErrorNameNullCharacterNotAllowed));
            this.textWindow.AdvanceByte(i);
            return null;
        }

        this.textWindow.AdvanceByte(i - 1);
        return (byte)decValue;
    }
}

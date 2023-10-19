// <copyright file="LexerNumericLiteralHelpers.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Globalization;
using OffDotNet.Pdf.CodeAnalysis.Errors;
using OffDotNet.Pdf.CodeAnalysis.Parser;
using OffDotNet.Pdf.CodeAnalysis.Syntax;

namespace OffDotNet.Pdf.CodeAnalysis.Lexer;

internal partial class Lexer
{
    private bool TryScanNumericLiteral(ref TokenInfo info)
    {
        info.Text = string.Empty;
        info.ValueKind = TokenInfoSpecialKind.None;
        this.stringBuilder.Clear();

        this.ScanNumericLiteralInteger();
        this.ScanNumericLiteralIntegerWithDecimalPoint(ref info);

        info.Kind = SyntaxKind.NumericLiteralToken;
        info.Text = this.stringBuilder.ToString();

        switch (info.ValueKind)
        {
            case TokenInfoSpecialKind.Single:
                info.FloatValue = float.Parse(info.Text, CultureInfo.InvariantCulture);
                break;
            default:
                info.ValueKind = TokenInfoSpecialKind.Int32;
                info.IntValue = this.GetValueInt32(info.Text);
                break;
        }

        return true;
    }

    private void ScanNumericLiteralIntegerWithDecimalPoint(ref TokenInfo info)
    {
        byte? peekedByte = this.reader.Peek();
        if (peekedByte != 0x2e)
        {
            return;
        }

        info.ValueKind = TokenInfoSpecialKind.Single;
        this.stringBuilder.Append((char)peekedByte.Value);
        this.reader.AdvanceByte();

        byte? peekedByte2 = this.reader.Peek();
        if (peekedByte2.HasValue && peekedByte2.Value.IsDecDigit())
        {
            this.ScanNumericLiteralInteger();
        }
    }

    private void ScanNumericLiteralInteger()
    {
        while (true)
        {
            byte? peekedByte = this.reader.Peek();

            if (!peekedByte.HasValue || !peekedByte.IsDecDigit())
            {
                break;
            }

            this.stringBuilder.Append((char)peekedByte.Value);
            this.reader.AdvanceByte();
        }
    }

    private int GetValueInt32(string text)
    {
        if (!int.TryParse(text, NumberStyles.None, CultureInfo.InvariantCulture, out int result))
        {
            this.AddError(MakeError(ErrorCode.ErrorIntOverflow));
        }

        return result;
    }
}

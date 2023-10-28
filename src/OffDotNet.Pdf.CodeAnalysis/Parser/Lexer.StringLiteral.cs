// <copyright file="Lexer.StringLiteral.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Diagnostics;
using OffDotNet.Pdf.CodeAnalysis.Errors;

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

internal partial class Lexer
{
    private void ScanStringLiteral(ref TokenInfo info)
    {
        byte? leftParenthesis = this.reader.PeekByte();
        Debug.Assert(leftParenthesis is 0x28, "String literal must start with left parenthesis.");
        this.reader.AdvanceByte();

        info.Text = string.Empty;
        info.ValueKind = TokenInfoSpecialKind.None;
        this.stringBuilder.Clear();

        int balancedParenthesisCount = 1;

        while (true)
        {
            byte? peekedByte = this.reader.PeekByte();

            if (!peekedByte.HasValue)
            {
                break;
            }

            switch (peekedByte.Value)
            {
                case 0x28: // '('
                    balancedParenthesisCount++;
                    break;
                case 0x29: // ')'
                    balancedParenthesisCount--;
                    break;
            }

            if (balancedParenthesisCount == 0)
            {
                this.reader.AdvanceByte();
                break;
            }

            this.stringBuilder.Append((char)peekedByte.Value);
            this.reader.AdvanceByte();
        }

        if (balancedParenthesisCount != 0)
        {
            this.AddError(MakeError(ErrorCode.ErrorStringUnbalancedParenthesis));
        }

        info.Kind = SyntaxKind.StringLiteralToken;
        info.Text = this.stringBuilder.ToString();
        info.StringValue = $"({info.Text})";
    }
}

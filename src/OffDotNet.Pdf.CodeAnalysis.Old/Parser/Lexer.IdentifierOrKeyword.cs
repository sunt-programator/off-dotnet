// <copyright file="Lexer.IdentifierOrKeyword.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Old.Parser;

namespace OffDotNet.Pdf.CodeAnalysis.Old.Syntax.InternalSyntax;

internal partial class Lexer
{
    private bool TryScanIdentifierOrKeyword(ref TokenInfo info)
    {
        info.Text = string.Empty;
        info.ValueKind = TokenInfoSpecialKind.None;
        this.stringBuilder.Clear();

        this.ScanIdentifier();

        info.Text = this.stringBuilder.ToString();
        info.Kind = SyntaxFacts.GetKeywordKind(info.Text);

        return true;
    }

    private void ScanIdentifier()
    {
        while (true)
        {
            byte? peekedByte = this.textWindow.PeekByte();

            if (!peekedByte.HasValue || !peekedByte.IsLetter())
            {
                break;
            }

            this.stringBuilder.Append((char)peekedByte.Value);
            this.textWindow.AdvanceByte();
        }
    }
}

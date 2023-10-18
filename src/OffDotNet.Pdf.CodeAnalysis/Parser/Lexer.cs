// <copyright file="Lexer.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Text;
using OffDotNet.Pdf.CodeAnalysis.LexerHelpers;
using OffDotNet.Pdf.CodeAnalysis.Syntax;

namespace OffDotNet.Pdf.CodeAnalysis.Parser;

internal class Lexer
{
    private readonly InputReader reader;
    private readonly StringBuilder stringBuilder = new();

    public Lexer(byte[] source)
    {
        this.reader = new InputReader(source);
    }

    public SyntaxToken Lex()
    {
        TokenInfo tokenInfo = default;
        this.SyntaxToken(ref tokenInfo);
        return Create(in tokenInfo);
    }

    private static SyntaxToken Create(in TokenInfo info)
    {
        switch (info.Kind)
        {
            case SyntaxKind.NumericLiteralToken:
                return info.ValueKind switch
                {
                    TokenInfoSpecialKind.Single => new SyntaxToken(SyntaxKind.NumericLiteralToken, info.Text, info.FloatValue),
                    TokenInfoSpecialKind.Int32 => new SyntaxToken(SyntaxKind.NumericLiteralToken, info.Text, info.IntValue),
                    TokenInfoSpecialKind.None => throw new InvalidOperationException(),
                    _ => throw new InvalidOperationException(),
                };

            case SyntaxKind.None:
            case SyntaxKind.EndOfFileToken:
            default:
                throw new InvalidOperationException();
        }
    }

    private void SyntaxToken(ref TokenInfo info)
    {
        info.Kind = SyntaxKind.None;
        info.Text = string.Empty;

        byte? peekedByte = this.reader.Peek();

        if (!peekedByte.HasValue)
        {
            return;
        }

        switch (peekedByte.Value)
        {
            case 0x2e: // .
                NumericLiteralHelpers.TryScanNumericLiteral(this.reader, this.stringBuilder, ref info);
                break;
            case >= 0x30 and <= 0x39: // 0-9
                NumericLiteralHelpers.TryScanNumericLiteral(this.reader, this.stringBuilder, ref info);
                break;
        }
    }
}

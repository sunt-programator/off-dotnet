// <copyright file="Lexer.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Text;
using OffDotNet.Pdf.CodeAnalysis.Errors;
using OffDotNet.Pdf.CodeAnalysis.Parser;
using OffDotNet.Pdf.CodeAnalysis.Syntax;

namespace OffDotNet.Pdf.CodeAnalysis.Lexer;

internal partial class Lexer
{
    private readonly InputReader reader;
    private readonly StringBuilder stringBuilder = new();
    private List<DiagnosticInfo>? errors;

    public Lexer(byte[] source)
    {
        this.reader = new InputReader(source);
    }

    public SyntaxToken Lex()
    {
        TokenInfo tokenInfo = default;
        this.ScanSyntaxToken(ref tokenInfo);
        return this.Create(tokenInfo);
    }

    private SyntaxToken Create(TokenInfo info)
    {
        SyntaxToken token;
        switch (info.Kind)
        {
            case SyntaxKind.NumericLiteralToken:
                token = info.ValueKind switch
                {
                    TokenInfoSpecialKind.Single => new SyntaxToken(SyntaxKind.NumericLiteralToken, info.Text, info.FloatValue),
                    TokenInfoSpecialKind.Int32 => new SyntaxToken(SyntaxKind.NumericLiteralToken, info.Text, info.IntValue),
                    TokenInfoSpecialKind.None => throw new InvalidOperationException(),
                    _ => throw new InvalidOperationException(),
                };
                break;

            default:
                token = new SyntaxToken(info.Kind, info.Text, null);
                break;
        }

        if (this.errors != null)
        {
            token = token.SetDiagnostics(this.errors.ToArray());
        }

        return token;
    }

    private void ScanSyntaxToken(ref TokenInfo info)
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
                this.TryScanNumericLiteral(ref info);
                break;
            case >= 0x30 and <= 0x39: // 0-9
                this.TryScanNumericLiteral(ref info);
                break;
        }
    }
}

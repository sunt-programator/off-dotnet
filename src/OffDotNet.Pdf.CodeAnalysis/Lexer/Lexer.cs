// <copyright file="Lexer.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Text;
using OffDotNet.Pdf.CodeAnalysis.Errors;
using OffDotNet.Pdf.CodeAnalysis.Parser;
using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

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

    public InternalSyntaxToken Lex()
    {
        TokenInfo tokenInfo = default;
        this.ScanSyntaxToken(ref tokenInfo);
        return this.Create(tokenInfo);
    }

    private InternalSyntaxToken Create(TokenInfo info)
    {
        InternalSyntaxToken token;
        switch (info.Kind)
        {
            case SyntaxKind.NumericLiteralToken:
                token = info.ValueKind switch
                {
                    TokenInfoSpecialKind.Single => InternalSyntaxFactory.Literal(info.Text, info.FloatValue),
                    TokenInfoSpecialKind.Int32 => InternalSyntaxFactory.Literal(info.Text, info.IntValue),
                    TokenInfoSpecialKind.None => throw new InvalidOperationException(),
                    _ => throw new InvalidOperationException(),
                };
                break;

            default:
                token = InternalSyntaxFactory.Create(info.Kind);
                break;
        }

        if (this.errors != null)
        {
            token = token.WithDiagnostics(this.errors.ToArray());
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
            case 0x2e: // '.'
                this.TryScanNumericLiteral(ref info);
                break;
            case 0x28: // '('
                this.reader.AdvanceByte();
                info.Kind = SyntaxKind.LeftParenthesisToken;
                break;
            case 0x29: // ')'
                this.reader.AdvanceByte();
                info.Kind = SyntaxKind.RightParenthesisToken;
                break;
            case 0x3c: // '<'
                this.reader.AdvanceByte();
                info.Kind = this.reader.TryAdvanceByte(0x3c) ? SyntaxKind.LessThanLessThanToken : SyntaxKind.LessThanToken; // '<<' or '<'
                break;
            case 0x3e: // '>'
                this.reader.AdvanceByte();
                info.Kind = this.reader.TryAdvanceByte(0x3e) ? SyntaxKind.GreaterThanGreaterThanToken : SyntaxKind.GreaterThanToken; // '>>' or '>'
                break;
            case 0x5b: // '['
                this.reader.AdvanceByte();
                info.Kind = SyntaxKind.LeftSquareBracketToken;
                break;
            case 0x5d: // ']'
                this.reader.AdvanceByte();
                info.Kind = SyntaxKind.RightSquareBracketToken;
                break;
            case 0x7b: // '{'
                this.reader.AdvanceByte();
                info.Kind = SyntaxKind.LeftCurlyBracketToken;
                break;
            case 0x7d: // '}'
                this.reader.AdvanceByte();
                info.Kind = SyntaxKind.RightCurlyBracketToken;
                break;
            case 0x2f: // '/'
                this.reader.AdvanceByte();
                info.Kind = SyntaxKind.SolidusToken;
                break;
            case 0x25: // '%'
                this.reader.AdvanceByte();
                info.Kind = SyntaxKind.PercentSignToken;
                break;
            case >= 0x30 and <= 0x39: // '0-9'
                this.TryScanNumericLiteral(ref info);
                break;
            case (>= 0x41 and <= 0x5a) or (>= 0x61 and <= 0x7a): // 'A-Z' or 'a-z'
                this.TryScanIdentifierOrKeyword(ref info);
                break;
        }
    }
}

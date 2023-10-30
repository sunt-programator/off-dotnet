// <copyright file="Lexer.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Diagnostics.CodeAnalysis;
using System.Text;
using OffDotNet.Pdf.CodeAnalysis.Errors;
using OffDotNet.Pdf.CodeAnalysis.Parser;

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

internal partial class Lexer : IDisposable
{
    private readonly SlidingTextWindow textWindow;
    private readonly StringBuilder stringBuilder = new();
    private readonly SyntaxListBuilder trailingTriviaCache = new(10);
    private List<SyntaxDiagnosticInfo>? errors;
    private SyntaxListBuilder leadingTriviaCache = new(10);

    public Lexer(byte[] source)
    {
        this.textWindow = new SlidingTextWindow(source);
    }

    public void Dispose()
    {
        this.textWindow.Dispose();
    }

    public SyntaxToken Lex()
    {
        var tokenInfo = default(TokenInfo);

        this.LexSyntaxTrivia(triviaList: ref this.leadingTriviaCache);
        this.ScanSyntaxToken(ref tokenInfo);

        var diagnosticErrors = this.GetErrors(GetFullWidth(this.leadingTriviaCache));

        return Create(in tokenInfo, this.leadingTriviaCache, this.trailingTriviaCache, diagnosticErrors);
    }

    [SuppressMessage("Roslynator", "RCS1242:Do not pass non-read-only struct by read-only reference.", Justification = "Performance")]
    private static SyntaxToken Create(in TokenInfo info, SyntaxListBuilder? leading, SyntaxListBuilder? trailing, SyntaxDiagnosticInfo[]? diagnosticErrors)
    {
        var leadingNode = leading?.ToListNode();
        var trailingNode = trailing?.ToListNode();

        SyntaxToken token;
        switch (info.Kind)
        {
            case SyntaxKind.NumericLiteralToken:
                token = info.ValueKind switch
                {
                    TokenInfoSpecialKind.Single => SyntaxFactory.Literal(leadingNode, info.Text, info.FloatValue, trailingNode),
                    TokenInfoSpecialKind.Int32 => SyntaxFactory.Literal(leadingNode, info.Text, info.IntValue, trailingNode),
                    _ => throw new InvalidOperationException(),
                };
                break;

            case SyntaxKind.StringLiteralToken:
                token = SyntaxFactory.Literal(info.Kind, leadingNode, info.Text, info.StringValue, trailingNode);
                break;
            case SyntaxKind.EndOfFileToken:
                token = SyntaxFactory.Token(leadingNode, info.Kind, trailingNode);
                break;
            case SyntaxKind.None:
                token = SyntaxFactory.BadToken(leadingNode, info.Text, trailingNode);
                break;
            default:
                token = SyntaxFactory.Token(leadingNode, info.Kind, trailingNode);
                break;
        }

        if (diagnosticErrors != null)
        {
            token = token.WithDiagnosticsGreen(diagnosticErrors);
        }

        return token;
    }

    private static int GetFullWidth(SyntaxListBuilder? builder)
    {
        int width = 0;

        if (builder == null)
        {
            return width;
        }

        for (int i = 0; i < builder.Count; i++)
        {
            width += builder[i]!.FullWidth;
        }

        return width;
    }

    private void ScanSyntaxToken(ref TokenInfo info)
    {
        info.Kind = SyntaxKind.None;
        info.Text = string.Empty;

        byte? peekedByte = this.textWindow.PeekByte();

        if (!peekedByte.HasValue)
        {
            info.Kind = SyntaxKind.EndOfFileToken;
            return;
        }

        switch (peekedByte.Value)
        {
            case 0x2e: // '.'
                this.TryScanNumericLiteral(ref info);
                break;
            case 0x28: // '('
                this.ScanStringLiteral(ref info);
                break;
            case 0x29: // ')'
                this.textWindow.AdvanceByte();
                info.Kind = SyntaxKind.RightParenthesisToken;
                break;
            case 0x3c: // '<'
                this.textWindow.AdvanceByte();
                info.Kind = this.textWindow.TryAdvanceByte(0x3c) ? SyntaxKind.LessThanLessThanToken : SyntaxKind.LessThanToken; // '<<' or '<'
                break;
            case 0x3e: // '>'
                this.textWindow.AdvanceByte();
                info.Kind = this.textWindow.TryAdvanceByte(0x3e) ? SyntaxKind.GreaterThanGreaterThanToken : SyntaxKind.GreaterThanToken; // '>>' or '>'
                break;
            case 0x5b: // '['
                this.textWindow.AdvanceByte();
                info.Kind = SyntaxKind.LeftSquareBracketToken;
                break;
            case 0x5d: // ']'
                this.textWindow.AdvanceByte();
                info.Kind = SyntaxKind.RightSquareBracketToken;
                break;
            case 0x7b: // '{'
                this.textWindow.AdvanceByte();
                info.Kind = SyntaxKind.LeftCurlyBracketToken;
                break;
            case 0x7d: // '}'
                this.textWindow.AdvanceByte();
                info.Kind = SyntaxKind.RightCurlyBracketToken;
                break;
            case 0x2f: // '/'
                this.textWindow.AdvanceByte();
                info.Kind = SyntaxKind.SolidusToken;
                break;
            case 0x25: // '%'
                this.textWindow.AdvanceByte();
                info.Kind = SyntaxKind.PercentSignToken;
                break;
            case 0x2b: // '+'
                this.textWindow.AdvanceByte();
                info.Kind = SyntaxKind.PlusToken;
                break;
            case 0x2d: // '-'
                this.textWindow.AdvanceByte();
                info.Kind = SyntaxKind.MinusToken;
                break;
            case >= 0x30 and <= 0x39: // '0-9'
                this.TryScanNumericLiteral(ref info);
                break;
            case (>= 0x41 and <= 0x5a) or (>= 0x61 and <= 0x7a): // 'A-Z' or 'a-z'
                this.TryScanIdentifierOrKeyword(ref info);
                break;
            default:
                this.AddError(MakeError(ErrorCode.ErrorUnexpectedCharacter));
                info.Text = $"{(char)peekedByte.Value}";
                break;
        }
    }

    private void ScanToEndOfLine()
    {
        while (true)
        {
            byte? peekedByte = this.textWindow.PeekByte();

            if (!peekedByte.HasValue || peekedByte.IsNewLine())
            {
                break;
            }

            this.stringBuilder.Append((char)peekedByte.Value);
            this.textWindow.AdvanceByte();
        }
    }
}

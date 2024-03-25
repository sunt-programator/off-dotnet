// <copyright file="StringLiteralState.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Lexer;

using System.Diagnostics;
using System.Text;
using Diagnostic;
using Syntax;

/// <summary>The state of the lexer that handles string literals.</summary>
internal sealed class StringLiteralState : LexerState
{
    /// <summary>The singleton instance of the <see cref="StringLiteralState"/> class.</summary>
    internal static readonly StringLiteralState Instance = new();

    /// <summary>Handles the string literal state.</summary>
    /// <param name="context">The lexer context.</param>
    public override void Handle(Lexer context)
    {
        Debug.Assert(
            context.TextWindow.PeekByte() == (byte)'(',
            "The string literal state should be called only when the next byte is an opening parenthesis.");

        context.StringBuilderCache.Clear();
        context.TextWindow.StartParsingLexeme();

        ScanStringLiteral(context);

        ref var tokenInfo = ref context.GetTokenInfo();
        tokenInfo.Kind = SyntaxKind.StringLiteralToken;
        tokenInfo.Text = Encoding.UTF8.GetString(context.TextWindow.GetLexemeBytes(shouldIntern: false));
        tokenInfo.StringValue = context.StringBuilderCache.ToString();

        context.TextWindow.StopParsingLexeme();
        context.StringBuilderCache.Clear();
    }

    private static void ScanStringLiteral(Lexer context)
    {
        // Skip the opening parenthesis.
        var parentheses = 1;
        context.TextWindow.AdvanceByte();

        while (context.TextWindow.PeekAndAdvanceByte() is { } b)
        {
            if (b is not ((byte)'(' or (byte)')'))
            {
                ScanEscapeSequencesAndNewLineMarkers(context, ref b);
                continue;
            }

            // Consumes the balanced parentheses.
            // Balanced parentheses should return 0.
            parentheses += b == (byte)'(' ? 1 : -1;

            if (parentheses == 0)
            {
                break;
            }

            context.StringBuilderCache.Append((char)b);
        }

        if (parentheses != 0)
        {
            var error = new DiagnosticInfo(MessageProvider.Instance, DiagnosticCode.ERR_InvalidStringLiteral);
            context.Errors.Add(error);
        }
    }

    private static void ScanEscapeSequencesAndNewLineMarkers(Lexer context, ref byte b)
    {
        switch (b)
        {
            case (byte)'\\' when context.TextWindow.PeekByte(1) is (byte)'\r' or (byte)'\n':
                context.TextWindow.AdvanceIfMatches(CharacterExtensions.IsEndOfLine);
                break;
            case (byte)'\r' or (byte)'\n':
                context.TextWindow.AdvanceIfMatches(CharacterExtensions.IsEndOfLine);
                context.StringBuilderCache.Append('\n');
                break;
            case (byte)'\\':
                ScanEscapeSequence(context);
                break;
            default:
                context.StringBuilderCache.Append((char)b);
                break;
        }
    }

    private static void ScanEscapeSequence(Lexer context)
    {
        var b = context.TextWindow.PeekByte();

        if (b == (byte)'n')
        {
            context.StringBuilderCache.Append('\n');
        }
        else if (b == (byte)'r')
        {
            context.StringBuilderCache.Append('\r');
        }
        else if (b == (byte)'t')
        {
            context.StringBuilderCache.Append('\t');
        }
        else if (b == (byte)'b')
        {
            context.StringBuilderCache.Append('\b');
        }
        else if (b == (byte)'f')
        {
            context.StringBuilderCache.Append('\f');
        }
        else if (b == (byte)'(')
        {
            context.StringBuilderCache.Append('(');
        }
        else if (b == (byte)')')
        {
            context.StringBuilderCache.Append(')');
        }
        else if (b.IsOctDigit() && ScanOctalEscapeSequence(context) is { } escapeSequence)
        {
            context.StringBuilderCache.Append((char)escapeSequence);
        }

        context.TextWindow.AdvanceByte();
    }

    private static byte? ScanOctalEscapeSequence(Lexer context, int maxDigits = 3)
    {
        var b = context.TextWindow.PeekByte();
        Debug.Assert(b.IsOctDigit(), "The escape sequence should start with a digit.");

        Span<byte> bytes = stackalloc byte[maxDigits];

        int i;
        for (i = 0; i < maxDigits; i++)
        {
            b = context.TextWindow.PeekByte(i);

            if (b is null || !b.IsOctDigit())
            {
                break;
            }

            bytes[i] = b.Value;
        }

        var octStringValue = Encoding.ASCII.GetString(bytes[..i]);
        var decValue = Convert.ToInt16(octStringValue, 8);

        if (decValue > 255)
        {
            // 400 oct = 256 dec, so we need to reduce the number of digits to parse
            // meaning that only 40 oct (20 hex) will be parsed
            maxDigits--;
            return ScanOctalEscapeSequence(context, maxDigits);
        }

        context.TextWindow.AdvanceByte(i - 1);
        return (byte)decValue;
    }
}

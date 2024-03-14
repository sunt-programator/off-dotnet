// <copyright file="StringLiteralState.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Lexer;

using System.Buffers;
using System.Diagnostics;
using System.Text;
using Diagnostic;
using Syntax;

/// <summary>The state of the lexer that handles string literals.</summary>
internal sealed class StringLiteralState : LexerState
{
    /// <summary>The singleton instance of the <see cref="StringLiteralState"/> class.</summary>
    internal static readonly StringLiteralState Instance = new();

    /// <summary>Handles the keyword token.</summary>
    /// <param name="context">The lexer context.</param>
    public override void Handle(LexerContext context)
    {
        Debug.Assert(
            context.TextWindow.PeekByte() == (byte)'(',
            "The string literal state should be called only when the next byte is an opening parenthesis.");

        context.StringBuilderCache.Clear();
        ScanStringLiteral(context);

        ref var tokenInfo = ref context.GetTokenInfo();
        tokenInfo._kind = SyntaxKind.StringLiteralToken;
        tokenInfo._text = Encoding.UTF8.GetString(context.TextWindow.GetLexemeBytes(shouldIntern: false));
        tokenInfo._stringValue = context.StringBuilderCache.ToString();

        context.StringBuilderCache.Clear();
    }

    private static void ScanStringLiteral(LexerContext context)
    {
        // Skip the opening parenthesis.
        var parentheses = 1;
        context.TextWindow.AdvanceByte();
        context.TextWindow.StartParsingLexeme();

        while (context.TextWindow.PeekAndAdvanceByte() is { } b)
        {
            if (b is not ((byte)'(' or (byte)')'))
            {
                ScanEscapeSequencesAndNewLineMarkers(ref b, context);
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
            var error = new DiagnosticInfo(MessageProvider.Instance, DiagnosticCode.ERR_UnbalancedStringLiteral);
            context.Errors.Add(error);
        }

        context.TextWindow.StopParsingLexeme();
    }

    private static void ScanEscapeSequencesAndNewLineMarkers(ref byte b, LexerContext context)
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

    private static void ScanEscapeSequence(LexerContext context)
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

    private static byte? ScanOctalEscapeSequence(LexerContext context, int maxDigits = 3)
    {
        var b = context.TextWindow.PeekByte();
        Debug.Assert(b.IsOctDigit(), "The escape sequence should start with a digit.");

        var arrayPool = ArrayPool<byte>.Shared;
        var bytes = arrayPool.Rent(maxDigits);

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

        var octStringValue = Encoding.ASCII.GetString(bytes.AsSpan(..i));
        var decValue = Convert.ToInt16(octStringValue, 8);
        arrayPool.Return(bytes);

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

// <copyright file="HexStringLiteralState.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Lexer;

using System.Diagnostics;
using System.Globalization;
using System.Text;
using Diagnostic;
using Syntax;

/// <summary>The state of the lexer that handles string literals.</summary>
internal sealed class HexStringLiteralState : LexerState
{
    /// <summary>The singleton instance of the <see cref="HexStringLiteralState"/> class.</summary>
    internal static readonly HexStringLiteralState Instance = new();

    /// <summary>Handles the hex string literal state.</summary>
    /// <param name="context">The lexer context.</param>
    public override void Handle(LexerContext context)
    {
        Debug.Assert(
            context.TextWindow.PeekByte() == (byte)'<',
            "The string literal state should be called only when the next byte is an opening parenthesis.");

        context.StringBuilderCache.Clear();
        ScanHexStringLiteral(context);

        ref var tokenInfo = ref context.GetTokenInfo();
        tokenInfo._kind = SyntaxKind.HexStringLiteralToken;
        tokenInfo._text = Encoding.UTF8.GetString(context.TextWindow.GetLexemeBytes(shouldIntern: false));
        tokenInfo._stringValue = context.StringBuilderCache.ToString();

        context.StringBuilderCache.Clear();
    }

    private static void ScanHexStringLiteral(LexerContext context)
    {
        Span<char> bytes = stackalloc char[2];
        var i = 0;

        while (context.TextWindow.PeekAndAdvanceByte() is { } b)
        {
            if (b == (byte)'<')
            {
                context.TextWindow.StartParsingLexeme();
                continue;
            }

            if (b == (byte)'>')
            {
                context.TextWindow.StopParsingLexeme();
                continue;
            }

            if (CharacterExtensions.IsWhiteSpace(b))
            {
                continue;
            }

            if (!CharacterExtensions.IsHexDigit(b))
            {
                var code = new DiagnosticInfo(MessageProvider.Instance, DiagnosticCode.ERR_InvalidHexStringLiteral);
                context.Errors.Add(code);
                continue;
            }

            bytes[i % 2] = (char)b;
            BuildChar(context, addTrailingZero: false, ref i, ref bytes);

            i++;
        }

        BuildChar(context, addTrailingZero: true, ref i, ref bytes);

        if (context.TextWindow.IsLexemeMode)
        {
            var code = new DiagnosticInfo(MessageProvider.Instance, DiagnosticCode.ERR_InvalidHexStringLiteral);
            context.Errors.Add(code);
        }

        context.TextWindow.StopParsingLexeme();
    }

    private static void BuildChar(
        LexerContext context,
        bool addTrailingZero,
        ref int i,
        ref Span<char> bytes)
    {
        // Only build a char if we have a pair of bytes.
        if (i % 2 != 1)
        {
            return;
        }

        // If the number of bytes is odd, we need to add a trailing 0 to the last byte.
        if (addTrailingZero)
        {
            bytes[1] = '0';
        }

        int.TryParse(bytes, NumberStyles.HexNumber, null, out var byteValue);
        context.StringBuilderCache.Append((char)byteValue);
    }
}

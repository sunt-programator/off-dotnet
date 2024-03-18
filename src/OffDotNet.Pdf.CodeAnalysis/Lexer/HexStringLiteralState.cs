// <copyright file="HexStringLiteralState.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Lexer;

using System.Diagnostics;
using System.Text;
using Diagnostic;
using Syntax;

/// <summary>The state of the lexer that handles string literals.</summary>
internal sealed class HexStringLiteralState : LexerState
{
    /// <summary>The singleton instance of the <see cref="HexStringLiteralState"/> class.</summary>
    internal static readonly HexStringLiteralState Instance = new();

    private static readonly DiagnosticInfo s_code = new(
        MessageProvider.Instance,
        DiagnosticCode.ERR_InvalidHexStringLiteral);

    /// <summary>Handles the hex string literal state.</summary>
    /// <param name="context">The lexer context.</param>
    public override void Handle(LexerContext context)
    {
        Debug.Assert(
            context.TextWindow.PeekByte() == (byte)'<',
            "The hex string literal state should be called only when the next byte is a less than sign.");

        context.StringBuilderCache.Clear();
        ScanHexStringLiteral(context);

        ref var tokenInfo = ref context.GetTokenInfo();
        tokenInfo.Kind = SyntaxKind.HexStringLiteralToken;
        tokenInfo.Text = Encoding.UTF8.GetString(context.TextWindow.GetLexemeBytes(shouldIntern: false));
        tokenInfo.StringValue = context.StringBuilderCache.ToString();

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
                context.Errors.Add(s_code);
                continue;
            }

            bytes[i % 2] = (char)b;
            context.BuildCharFromStringHex(addTrailingZero: false, ref i, ref bytes);
            i++;
        }

        context.BuildCharFromStringHex(addTrailingZero: true, ref i, ref bytes);

        if (context.TextWindow.IsLexemeMode)
        {
            context.Errors.Add(s_code);
        }

        context.TextWindow.StopParsingLexeme();
    }
}

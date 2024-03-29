﻿// <copyright file="NameLiteralState.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Lexer;

using System.Diagnostics;
using System.Text;
using Diagnostic;
using Syntax;

/// <summary>The state of the lexer that handles name literals.</summary>
internal sealed class NameLiteralState : LexerState
{
    /// <summary>The singleton instance of the <see cref="NameLiteralState"/> class.</summary>
    internal static readonly NameLiteralState Instance = new();

    private static readonly DiagnosticInfo
        s_code = new(MessageProvider.Instance, DiagnosticCode.ERR_InvalidNameLiteral);

    /// <summary>Handles the name literal state.</summary>
    /// <param name="context">The lexer context.</param>
    public override void Handle(Lexer context)
    {
        Debug.Assert(
            context.TextWindow.PeekByte() == (byte)'/',
            "The name literal state should be called only when the next byte is a forward slash.");

        context.StringBuilderCache.Clear();
        context.TextWindow.StartParsingLexeme();
        ScanNameLiteral(context);

        ref var tokenInfo = ref context.GetTokenInfo();
        tokenInfo.Kind = SyntaxKind.NameLiteralToken;
        tokenInfo.Text = Encoding.UTF8.GetString(context.TextWindow.GetLexemeBytes(shouldIntern: false));
        tokenInfo.StringValue = context.StringBuilderCache.ToString();

        context.TextWindow.StopParsingLexeme();
        context.StringBuilderCache.Clear();
    }

    private static void ScanNameLiteral(Lexer context)
    {
        Span<char> bytes = stackalloc char[2];
        context.TextWindow.AdvanceByte();

        while (context.TextWindow.PeekAndAdvanceByte() is { } b)
        {
            if (b == (byte)'/' || CharacterExtensions.IsWhiteSpaceExceptNull(b))
            {
                // The name literals are delimited by a forward slash or any whitespace character.
                context.TextWindow.AdvanceByte(-1);
                break;
            }

            if (b == (byte)'#')
            {
                ScanHexCode(context, ref bytes);
                continue;
            }

            if (b == (byte)'\0')
            {
                context.Errors.Add(s_code);
                continue;
            }

            context.StringBuilderCache.Append((char)b);
        }

        AddErrorIfNameIsTooLong(context);
    }

    private static void AddErrorIfNameIsTooLong(Lexer context)
    {
        if (context.StringBuilderCache.Length > 127)
        {
            context.Errors.Add(s_code);
        }
    }

    private static void ScanHexCode(Lexer context, ref Span<char> bytes)
    {
        var b1 = context.TextWindow.PeekByte();

        if (!b1.IsHexDigit())
        {
            context.Errors.Add(s_code);
            return;
        }

        context.TextWindow.AdvanceByte();

        var b2 = context.TextWindow.PeekByte();

        if (!b2.IsHexDigit())
        {
            context.StringBuilderCache.Append((char)b1);
            context.Errors.Add(s_code);
            return;
        }

        context.TextWindow.AdvanceByte();

        bytes[0] = (char)b1;
        bytes[1] = (char)b2;
        var i = 1;
        context.BuildCharFromStringHex(addTrailingZero: false, ref i, ref bytes);
    }
}

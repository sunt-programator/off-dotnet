﻿// <copyright file="SimpleTokenState.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Lexer;

using System.Diagnostics;
using System.Text;
using Diagnostic;
using Syntax;

/// <summary>The state of the lexer for the simple token.</summary>
internal sealed class SimpleTokenState : LexerState
{
    /// <summary>The singleton instance of the <see cref="SimpleTokenState"/> class.</summary>
    internal static readonly SimpleTokenState Instance = new();

    /// <summary>Handles the simple token.</summary>
    /// <param name="context">The lexer context.</param>
    public override void Handle(LexerContext context)
    {
        ref var tokenInfo = ref context.GetTokenInfo();
        tokenInfo._kind = ScanSimpleToken(context);
        tokenInfo._text = tokenInfo._kind.GetText();
    }

    private static SyntaxKind ScanSimpleToken(LexerContext context)
    {
        switch (context.TextWindow.PeekByte())
        {
            case (byte)'<' when context.TextWindow.PeekByte(1) == (byte)'<':
                context.TextWindow.TryAdvanceIfMatches("<<"u8);
                return SyntaxKind.LessThanLessThanToken;
            case (byte)'>' when context.TextWindow.PeekByte(1) == (byte)'>':
                context.TextWindow.TryAdvanceIfMatches(">>"u8);
                return SyntaxKind.GreaterThanGreaterThanToken;
            case (byte)'[':
                context.TextWindow.AdvanceByte();
                return SyntaxKind.LeftSquareBracketToken;
            case (byte)']':
                context.TextWindow.AdvanceByte();
                return SyntaxKind.RightSquareBracketToken;
            case (byte)'{':
                context.TextWindow.AdvanceByte();
                return SyntaxKind.LeftCurlyBracketToken;
            case (byte)'}':
                context.TextWindow.AdvanceByte();
                return SyntaxKind.RightCurlyBracketToken;
            case (byte)'+':
                context.TextWindow.AdvanceByte();
                return SyntaxKind.PlusToken;
            case (byte)'-':
                context.TextWindow.AdvanceByte();
                return SyntaxKind.MinusToken;
        }

        return SyntaxKind.None;
    }
}

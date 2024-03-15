// <copyright file="NumericLiteralState.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Lexer;

using System.Diagnostics;
using System.Globalization;
using System.Text;
using Diagnostic;
using Syntax;

/// <summary>The state of the lexer that handles numeric literals.</summary>
internal sealed class NumericLiteralState : LexerState
{
    /// <summary>The singleton instance of the <see cref="NumericLiteralState"/> class.</summary>
    internal static readonly NumericLiteralState Instance = new();

    /// <summary>Handles the numeric literal token.</summary>
    /// <param name="context">The lexer context.</param>
    public override void Handle(LexerContext context)
    {
        Debug.Assert(
            context.TextWindow.PeekByte().IsDecDigit() || context.TextWindow.PeekByte() == (byte)'.',
            "The numeric literal state should be called only when the next byte is a digit or a dot.");

        context.TextWindow.StartParsingLexeme();
        context.TextWindow.AdvanceIfMatches(CharacterExtensions.IsDecDigit); // handle integer numbers
        var isRealNumber = context.TextWindow.TryAdvanceIfMatches((byte)'.'); // handle real numbers
        context.TextWindow.AdvanceIfMatches(CharacterExtensions.IsDecDigit); // handle integer numbers after the dot
        context.TextWindow.StopParsingLexeme();

        ref var tokenInfo = ref context.GetTokenInfo();
        tokenInfo._kind = SyntaxKind.NumericLiteralToken;
        tokenInfo._text = Encoding.UTF8.GetString(context.TextWindow.GetLexemeBytes(shouldIntern: true));

        if (!isRealNumber)
        {
            ParseIntNumber(tokenInfo._text, context.Errors, ref tokenInfo);
            return;
        }

        ParseRealNumber(tokenInfo._text, context.Errors, ref tokenInfo);
    }

    private static void ParseIntNumber(
        ReadOnlySpan<char> text,
        ICollection<DiagnosticInfo> errors,
        ref LexerContext.TokenInfo tokenInfo)
    {
        try
        {
            tokenInfo._intValue = int.Parse(text, NumberStyles.Integer, CultureInfo.InvariantCulture);
        }
        catch (OverflowException)
        {
            // ISO 32000-2:2020 - 7.3.3 Numeric Objects
            // Real numbers can be represented as integers, so we can parse large integers as real numbers.
            ParseRealNumber(text, errors, ref tokenInfo);
        }
    }

    private static void ParseRealNumber(
        ReadOnlySpan<char> text,
        ICollection<DiagnosticInfo> errors,
        ref LexerContext.TokenInfo tokenInfo)
    {
        var realValue = double.Parse(text, NumberStyles.Float, CultureInfo.InvariantCulture);

        if (double.IsInfinity(realValue))
        {
            errors.Add(new DiagnosticInfo(MessageProvider.Instance, DiagnosticCode.ERR_RealOverflow));
            return;
        }

        tokenInfo._realValue = realValue;
    }
}

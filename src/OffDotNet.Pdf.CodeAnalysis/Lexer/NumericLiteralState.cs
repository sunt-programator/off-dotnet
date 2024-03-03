// <copyright file="NumericLiteralState.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Lexer;

using System.Diagnostics;
using System.Globalization;
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
            context.TextWindow.IsLexemeMode,
            "NumericLiteralState should be called only when the lexer is in lexeme scanning mode.");

        context.TextWindow.AdvanceIfMatches(CharacterExtensions.IsDecDigit); // handle integer numbers
        var isRealNumber = context.TextWindow.TryAdvanceIfMatches((byte)'.'); // handle real numbers
        context.TextWindow.AdvanceIfMatches(CharacterExtensions.IsDecDigit); // handle integer numbers after the dot

        context.TextWindow.StopParsingLexeme();

        ref var tokenInfo = ref context.GetTokenInfo();
        tokenInfo._kind = SyntaxKind.NumericLiteralToken;
        tokenInfo._text = context.TextWindow.GetLexemeBytes(shouldIntern: true).ToArray();

        Span<char> text = stackalloc char[tokenInfo._text.Length];
        for (var i = 0; i < tokenInfo._text.Length; i++)
        {
            text[i] = (char)tokenInfo._text[i];
        }

        if (!isRealNumber)
        {
            ParseIntNumber(text, ref tokenInfo, context.Errors);
            return;
        }

        ParseRealNumber(text, ref tokenInfo, context.Errors);
    }

    private static void ParseIntNumber(
        Span<char> text,
        ref LexerContext.TokenInfo tokenInfo,
        ICollection<DiagnosticInfo> errors)
    {
        try
        {
            tokenInfo._intValue = int.Parse(text, NumberStyles.Integer, CultureInfo.InvariantCulture);
        }
        catch (OverflowException)
        {
            // ISO 32000-2:2020 - 7.3.3 Numeric Objects
            // Real numbers can be represented as integers, so we can parse large integers as real numbers.
            ParseRealNumber(text, ref tokenInfo, errors);
        }
    }

    private static void ParseRealNumber(
        Span<char> text,
        ref LexerContext.TokenInfo tokenInfo,
        ICollection<DiagnosticInfo> errors)
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

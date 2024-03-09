// <copyright file="KeywordState.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Lexer;

using System.Diagnostics;
using System.Text;
using Diagnostic;
using Syntax;

/// <summary>The state of the lexer that handles keywords.</summary>
internal sealed class KeywordState : LexerState
{
    /// <summary>The singleton instance of the <see cref="KeywordState"/> class.</summary>
    internal static readonly KeywordState Instance = new();

    private const int MaxKeywordLength = 10;

    /// <summary>Handles the keyword token.</summary>
    /// <param name="context">The lexer context.</param>
    public override void Handle(LexerContext context)
    {
        Debug.Assert(
            context.TextWindow.IsLexemeMode,
            "NumericLiteralState should be called only when the lexer is in lexeme scanning mode.");

        Debug.Assert(
            context.TextWindow.PeekByte().IsAlpha(),
            "The keyword state should be called only when the next byte is an alpha character.");

        context.TextWindow.AdvanceIfMatches(CharacterExtensions.IsAlpha); // handle the keyword token

        context.TextWindow.StopParsingLexeme();

        ref var tokenInfo = ref context.GetTokenInfo();
        tokenInfo._text = Encoding.UTF8.GetString(context.TextWindow.GetLexemeBytes(shouldIntern: true));

        tokenInfo._kind = tokenInfo._text.Length > MaxKeywordLength
            ? SyntaxKind.None
            : context.KeywordKindCache.GetOrAdd(
                tokenInfo._text,
                static text => SyntaxKindFacts.SyntaxKindDictionary.GetValueOrDefault(text, SyntaxKind.None));

        if (tokenInfo._kind == SyntaxKind.None)
        {
            tokenInfo._kind = SyntaxKind.UnknownKeyword;
            context.Errors.Add(new DiagnosticInfo(MessageProvider.Instance, DiagnosticCode.ERR_InvalidKeyword));
        }
    }
}

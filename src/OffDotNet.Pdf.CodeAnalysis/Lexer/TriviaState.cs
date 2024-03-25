// <copyright file="TriviaState.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Lexer;

using System.Text;
using Syntax;
using Syntax.InternalSyntax;
using SyntaxTrivia = Syntax.InternalSyntax.SyntaxTrivia;

/// <summary>The state of the lexer for the simple token.</summary>
internal sealed class TriviaState : LexerState
{
    /// <summary>Maximum size of tokens/trivia that we cache and use in quick scanner.</summary>
    internal const int MaxCachedTokenSize = 42;

    /// <summary>The singleton instance of the <see cref="TriviaState"/> class.</summary>
    internal static readonly TriviaState Instance = new();

    /// <summary>Handles the simple token.</summary>
    /// <param name="context">The lexer context.</param>
    public override void Handle(Lexer context)
    {
        while (true)
        {
            var b = context.TextWindow.PeekByte();
            if (b.IsWhiteSpaceExceptEndOfLine())
            {
                ScanWhiteSpace(context);
                continue;
            }

            if (b.IsEndOfLine())
            {
                ScanEndOfLine(context);
                continue;
            }

            if (b == (byte)'%')
            {
                ScanComment(context);
                continue;
            }

            break;
        }
    }

    private static void ScanComment(Lexer context)
    {
        context.TextWindow.StartParsingLexeme();
        context.TextWindow.AdvanceIfMatches(static b1 => b1 != null && !b1.IsEndOfLine());
        var text = Encoding.UTF8.GetString(context.TextWindow.GetLexemeBytes(shouldIntern: false));
        var trivia = SyntaxFactory.Trivia(SyntaxKind.SingleLineCommentTrivia, text);
        AddTrivia(context, trivia);
        context.TextWindow.StopParsingLexeme();
    }

    private static void ScanWhiteSpace(Lexer context)
    {
        context.TextWindow.StartParsingLexeme();
        var onlySpaces = true;
        while (context.TextWindow.PeekAndAdvanceByte() is { } b)
        {
            if (b is (byte)'\t' or (byte)'\f' or (byte)'\0')
            {
                onlySpaces = false;
                continue;
            }

            if (b is (byte)' ')
            {
                continue;
            }

            if (CharacterExtensions.IsEndOfLine(b))
            {
                break;
            }

            context.TextWindow.AdvanceByte(-1);
            break;
        }

        AddTrivia(context, CreateSyntaxTrivia(context, onlySpaces));
        context.TextWindow.StopParsingLexeme();
    }

    private static void ScanEndOfLine(Lexer context)
    {
        if (context.TextWindow.PeekByte() == (byte)'\r')
        {
            context.TextWindow.AdvanceByte();
            var trivia = context.TextWindow.TryAdvanceIfMatches((byte)'\n')
                ? SyntaxFactory.CarriageReturnLineFeed
                : SyntaxFactory.CarriageReturn;

            AddTrivia(context, trivia);
            return;
        }

        if (context.TextWindow.PeekByte() == (byte)'\n')
        {
            context.TextWindow.AdvanceByte();
            AddTrivia(context, SyntaxFactory.LineFeed);
        }
    }

    private static void AddTrivia(Lexer context, SyntaxTrivia trivia)
    {
        if (context.IsLeadingTriviaMode)
        {
            context.LeadingTriviaBuilder.Add(trivia);
        }
        else
        {
            context.TrailingTriviaBuilder.Add(trivia);
        }
    }

    private static SyntaxTrivia CreateSyntaxTrivia(Lexer context, bool onlySpaces)
    {
        var text = Encoding.UTF8.GetString(context.TextWindow.GetLexemeBytes(shouldIntern: true));
        if (text.Length == 1 && onlySpaces)
        {
            return SyntaxFactory.Space;
        }

        return SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, text); // TODO: Caching
    }
}

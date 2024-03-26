// <copyright file="Lexer.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Lexer;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Caching;
using Diagnostic;
using Parser;
using PooledObjects;
using Syntax;
using Syntax.InternalSyntax;
using SyntaxToken = Syntax.InternalSyntax.SyntaxToken;
using SyntaxTrivia = Syntax.InternalSyntax.SyntaxTrivia;

/// <summary>The context of the lexer.</summary>
internal sealed class Lexer : IDisposable
{
    private readonly Lazy<ICollection<DiagnosticInfo>> _errors;

    [SuppressMessage(
        "Minor Code Smell",
        "S1450:Private fields only used as local variables in methods should become local variables",
        Justification = "False positive.")]
    private LexerState _state;

    [SuppressMessage("ReSharper", "RedundantDefaultMemberInitializer", Justification = "False positive.`")]
    private TokenInfo _info = default;

    /// <summary>Initializes a new instance of the <see cref="Lexer"/> class.</summary>
    /// <param name="textWindow">The text window.</param>
    internal Lexer(SlidingTextWindow textWindow)
    {
        TextWindow = textWindow;
        KeywordKindCache = SharedObjectPools.KeywordKindCache.Get();
        StringBuilderCache = SharedObjectPools.StringBuilderPool.Get();
        SyntaxTriviaCache = SharedObjectPools.SyntaxTriviaCache.Get();
        LeadingTriviaBuilder = new SyntaxListBuilder(10);
        TrailingTriviaBuilder = new SyntaxListBuilder(10);
        _state = DefaultState.Instance;
        _errors = new Lazy<ICollection<DiagnosticInfo>>(() => new List<DiagnosticInfo>(8));
    }

    /// <summary>Gets the text window.</summary>
    internal SlidingTextWindow TextWindow { get; }

    /// <summary>Gets the keyword kind cache.</summary>
    internal ThreadSafeCacheFactory<string, SyntaxKind> KeywordKindCache { get; }

    /// <summary>Gets the syntax token cache.</summary>
    internal ThreadSafeCacheFactory<string, SyntaxTrivia> SyntaxTriviaCache { get; }

    /// <summary>Gets the string builder cache.</summary>
    internal StringBuilder StringBuilderCache { get; }

    /// <summary>Gets the errors.</summary>
    internal ICollection<DiagnosticInfo> Errors => _errors.Value;

    /// <summary>Gets the leading trivia builder.</summary>
    internal SyntaxListBuilder LeadingTriviaBuilder { get; }

    /// <summary>Gets the trailing trivia builder.</summary>
    internal SyntaxListBuilder TrailingTriviaBuilder { get; }

    /// <summary>
    /// Gets or sets a value indicating whether the leading trivia mode is active. Otherwise, the trailing trivia mode is active.
    /// </summary>
    internal bool IsLeadingTriviaMode { get; set; } = true;

    /// <summary>Gets the token info.</summary>
    /// <returns>The token info.</returns>
    public ref TokenInfo GetTokenInfo()
    {
        return ref _info;
    }

    /// <summary>Creates a new <see cref="SyntaxToken"/> instance.</summary>
    /// <returns>The new <see cref="SyntaxToken"/> instance.</returns>
    public SyntaxToken LexSyntaxToken()
    {
        ref var tokenInfo = ref GetTokenInfo();
        tokenInfo = default;

        this.TransitionTo(DefaultState.Instance);
        var token = CreateSyntaxToken();

        if (Errors.Count > 0)
        {
            // TODO: error offset from leading trivia
            token = (SyntaxToken)token.SetDiagnostics(Errors.ToArray());
        }

        return token;
    }

    /// <summary>Disposes the resources used by the <see cref="Lexer"/> class.</summary>
    public void Dispose()
    {
        TextWindow.Dispose();
        SharedObjectPools.KeywordKindCache.Return(KeywordKindCache);
        SharedObjectPools.StringBuilderPool.Return(StringBuilderCache);
        SharedObjectPools.SyntaxTriviaCache.Return(SyntaxTriviaCache);
    }

    /// <summary>Transitions to the specified state.</summary>
    /// <param name="state">The state to transition to.</param>
    internal void TransitionTo(LexerState state)
    {
        _state = state;
        _state.Handle(this);
    }

    private SyntaxToken CreateSyntaxToken()
    {
        ref var tokenInfo = ref GetTokenInfo();

        var leadingNode = LeadingTriviaBuilder.ToListNode();
        var trailingNode = TrailingTriviaBuilder.ToListNode();

        switch (tokenInfo.Kind)
        {
            case SyntaxKind.HexStringLiteralToken:
            case SyntaxKind.StringLiteralToken:
            case SyntaxKind.NameLiteralToken:
                return SyntaxFactory.Token(
                    tokenInfo.Kind,
                    tokenInfo.Text,
                    tokenInfo.StringValue,
                    leadingNode,
                    trailingNode);
            case SyntaxKind.NumericLiteralToken when tokenInfo.ValueKind == TokenInfoSpecialType.SystemInt32:
                return SyntaxFactory.Token(
                    tokenInfo.Kind,
                    tokenInfo.Text,
                    tokenInfo.IntValue,
                    leadingNode,
                    trailingNode);
            case SyntaxKind.NumericLiteralToken when tokenInfo.ValueKind == TokenInfoSpecialType.SystemDouble:
                return SyntaxFactory.Token(
                    tokenInfo.Kind,
                    tokenInfo.Text,
                    tokenInfo.RealValue,
                    leadingNode,
                    trailingNode);
            case SyntaxKind.None:
                return SyntaxFactory.Token(SyntaxKind.BadToken, tokenInfo.Text, leadingNode, trailingNode);
            default:
                Debug.Assert(
                    tokenInfo.Kind is >= SyntaxKind.LeftSquareBracketToken and < SyntaxKind.BadToken,
                    "The default case must handle only punctuations and keywords");

                return SyntaxFactory.Token(tokenInfo.Kind, tokenInfo.Text, leadingNode, trailingNode);
        }
    }
}

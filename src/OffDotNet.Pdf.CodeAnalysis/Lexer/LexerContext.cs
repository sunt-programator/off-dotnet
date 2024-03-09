// <copyright file="LexerContext.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Lexer;

using System.Diagnostics.CodeAnalysis;
using Caching;
using Diagnostic;
using Parser;
using PooledObjects;
using Syntax;

/// <summary>The context of the lexer.</summary>
internal sealed class LexerContext : IDisposable
{
    private readonly Lazy<ICollection<DiagnosticInfo>> _errors;

    [SuppressMessage(
        "Minor Code Smell",
        "S1450:Private fields only used as local variables in methods should become local variables",
        Justification = "False positive.")]
    private LexerState _state;

    [SuppressMessage("ReSharper", "RedundantDefaultMemberInitializer", Justification = "False positive.`")]
    private TokenInfo _info = default;

    /// <summary>Initializes a new instance of the <see cref="LexerContext"/> class.</summary>
    /// <param name="textWindow">The text window.</param>
    public LexerContext(SlidingTextWindow textWindow)
    {
        TextWindow = textWindow;
        KeywordKindCache = SharedObjectPools.KeywordKindCache.Get();
        _state = DefaultState.Instance;
        _errors = new Lazy<ICollection<DiagnosticInfo>>(() => new List<DiagnosticInfo>(8));
    }

    /// <summary>Gets the text window.</summary>
    public SlidingTextWindow TextWindow { get; }

    /// <summary>Gets the keyword kind cache.</summary>
    public ThreadSafeCacheFactory<string, SyntaxKind> KeywordKindCache { get; }

    /// <summary>Gets the errors.</summary>
    public ICollection<DiagnosticInfo> Errors => _errors.Value;

    /// <summary>Gets the token info.</summary>
    /// <returns>The token info.</returns>
    public ref TokenInfo GetTokenInfo()
    {
        return ref _info;
    }

    /// <summary>Transitions to the specified state.</summary>
    /// <param name="state">The state to transition to.</param>
    public void TransitionTo(LexerState state)
    {
        _state = state;
        _state.Handle(this);
    }

    /// <summary>Disposes the resources used by the <see cref="LexerContext"/> class.</summary>
    public void Dispose()
    {
        TextWindow.Dispose();
        SharedObjectPools.KeywordKindCache.Return(KeywordKindCache);
    }

    /// <summary>Represents the token info.</summary>
    internal struct TokenInfo
    {
        /// <summary>The kind of the token.</summary>
        internal SyntaxKind _kind;

        /// <summary>The text of the token.</summary>
        internal string _text;

        /// <summary>The integer value of the token.</summary>
        internal int _intValue;

        /// <summary>The real value of the token.</summary>
        internal double _realValue;
    }
}

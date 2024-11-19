// <copyright file="ITextCursor.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Lexer;

using Utils;

/// <inheritdoc cref="TextCursor"/>
public interface ITextCursor : IDisposable
{
    /// <inheritdoc cref="TextCursor.SourceText"/>
    ISourceText SourceText { get; }

    /// <inheritdoc cref="TextCursor.Current"/>
    Option<byte> Current { get; }

    /// <inheritdoc cref="TextCursor.IsAtEnd"/>
    bool IsAtEnd { get; }

    /// <inheritdoc cref="TextCursor.WindowStart"/>
    int WindowStart { get; }

    /// <inheritdoc cref="TextCursor.Offset"/>
    int Offset { get; }

    /// <inheritdoc cref="TextCursor.Position"/>
    int Position => WindowStart + Offset;

    /// <inheritdoc cref="TextCursor.LexemeStart"/>
    bool IsLexemeMode { get; }

    /// <inheritdoc cref="TextCursor.LexemeStart"/>
    int LexemeStart { get; }

    /// <inheritdoc cref="TextCursor.LexemePosition"/>
    int LexemePosition => WindowStart + LexemeStart;

    /// <inheritdoc cref="TextCursor.LexemeWidth"/>
    int LexemeWidth => Offset - LexemeStart;

    /// <inheritdoc cref="TextCursor.Window"/>
    ReadOnlyMemory<byte> Window { get; }

    /// <inheritdoc cref="TextCursor.WindowSize"/>
    int WindowSize { get; }

    /// <inheritdoc cref="TextCursor.Peek()"/>
    Option<byte> Peek();

    /// <inheritdoc cref="TextCursor.Peek(int)"/>
    Option<byte> Peek(int delta);

    /// <inheritdoc cref="TextCursor.Advance(int)"/>
    void Advance(int delta = 1);

    /// <inheritdoc cref="TextCursor.Advance(Predicate{byte})"/>
    void Advance(Predicate<byte> predicate);

    /// <inheritdoc cref="TextCursor.TryAdvance(byte)"/>
    bool TryAdvance(byte b);

    /// <inheritdoc cref="TextCursor.TryAdvance(ReadOnlySpan{byte})"/>
    bool TryAdvance(ReadOnlySpan<byte> subtext);

    /// <inheritdoc cref="TextCursor.SlideTextWindow"/>
    void SlideTextWindow(int windowStart = -1, int windowSize = -1);

    /// <inheritdoc cref="TextCursor.StartLexemeMode"/>
    public void StartLexemeMode();

    /// <inheritdoc cref="TextCursor.StopLexemeMode"/>
    public void StopLexemeMode();
}

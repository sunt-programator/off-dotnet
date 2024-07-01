// <copyright file="CursorFactory.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Lexer;

/// <summary>
/// Factory class for creating instances of <see cref="ITextCursor"/>.
/// </summary>
internal sealed class CursorFactory : ICursorFactory
{
    /// <summary>
    /// Creates an instance of <see cref="ITextCursor"/> from a read-only byte span.
    /// </summary>
    /// <param name="text">The text represented as a read-only byte span.</param>
    /// <returns>An instance of <see cref="ITextCursor"/> initialized with the provided text.</returns>
    public ITextCursor Create(ReadOnlySpan<byte> text)
    {
        return new TextCursor(text);
    }

    /// <summary>
    /// Creates an instance of <see cref="ITextCursor"/> from a read-only character span.
    /// </summary>
    /// <param name="text">The text represented as a read-only character span.</param>
    /// <returns>An instance of <see cref="ITextCursor"/> initialized with the provided text.</returns>
    public ITextCursor Create(ReadOnlySpan<char> text)
    {
        return new TextCursor(text);
    }

    /// <summary>
    /// Creates an instance of <see cref="ITextCursor"/> from a string.
    /// </summary>
    /// <param name="text">The text represented as a string.</param>
    /// <returns>An instance of <see cref="ITextCursor"/> initialized with the provided text.</returns>
    public ITextCursor Create(string text)
    {
        return new TextCursor(text);
    }
}

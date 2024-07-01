// <copyright file="ICursorFactory.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Lexer;

/// <summary>
/// Factory interface for creating instances of <see cref="ITextCursor"/>.
/// </summary>
public interface ICursorFactory
{
    /// <summary>
    /// Creates an instance of <see cref="ITextCursor"/> from a read-only byte span.
    /// </summary>
    /// <param name="text">The text represented as a read-only byte span.</param>
    /// <returns>An instance of <see cref="ITextCursor"/> initialized with the provided text.</returns>
    ITextCursor Create(ReadOnlySpan<byte> text);

    /// <summary>
    /// Creates an instance of <see cref="ITextCursor"/> from a read-only character span.
    /// </summary>
    /// <param name="text">The text represented as a read-only character span.</param>
    /// <returns>An instance of <see cref="ITextCursor"/> initialized with the provided text.</returns>
    ITextCursor Create(ReadOnlySpan<char> text);

    /// <summary>
    /// Creates an instance of <see cref="ITextCursor"/> from a string.
    /// </summary>
    /// <param name="text">The text represented as a string.</param>
    /// <returns>An instance of <see cref="ITextCursor"/> initialized with the provided text.</returns>
    ITextCursor Create(string text);
}

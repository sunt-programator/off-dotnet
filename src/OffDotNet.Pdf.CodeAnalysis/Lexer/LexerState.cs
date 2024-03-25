// <copyright file="LexerState.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Lexer;

/// <summary>Represents an abstract class that defines the lexer state.</summary>
internal abstract class LexerState
{
    /// <summary>Handles the lexer state.</summary>
    /// <param name="context">The lexer context.</param>
    public abstract void Handle(Lexer context);
}

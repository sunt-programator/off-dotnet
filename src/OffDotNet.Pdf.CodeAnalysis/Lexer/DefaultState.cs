﻿// <copyright file="DefaultState.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Lexer;

using System.Diagnostics;

/// <summary>The initial state of the lexer.</summary>
internal sealed class DefaultState : LexerState
{
    /// <summary>The singleton instance of the <see cref="DefaultState"/> class.</summary>
    internal static readonly DefaultState Instance = new();

    /// <summary>Handles the initial state of the lexer.</summary>
    /// <param name="context">The lexer context.</param>
    public override void Handle(LexerContext context)
    {
        var b = context.TextWindow.PeekByte();
        Debug.Assert(b != null, "The byte cannot be null.");

        if (b.IsDecDigit() || (b == (byte)'.' && context.TextWindow.PeekByte(1).IsDecDigit()))
        {
            context.TransitionTo(NumericLiteralState.Instance);
            return;
        }

        if (b.IsAlpha())
        {
            context.TransitionTo(KeywordState.Instance);
        }
    }
}

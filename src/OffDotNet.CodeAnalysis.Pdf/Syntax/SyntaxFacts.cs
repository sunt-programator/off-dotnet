// <copyright file="SyntaxFacts.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Pdf.Syntax;

/// <summary>
/// Provides utility methods for syntax kinds.
/// </summary>
internal static class SyntaxFacts
{
    /// <summary>
    /// Gets the text representation of the specified <see cref="SyntaxKind"/>.
    /// </summary>
    /// <param name="kind">The syntax kind.</param>
    /// <returns>The text representation of the syntax kind.</returns>
    public static string GetText(this SyntaxKind kind)
    {
        return kind switch
        {
            // Tokens
            SyntaxKind.MinusToken => "-",

            // Keywords
            SyntaxKind.TrueKeyword => "true",
            SyntaxKind.FalseKeyword => "false",
            SyntaxKind.NullKeyword => "null",
            _ => string.Empty,
        };
    }
}

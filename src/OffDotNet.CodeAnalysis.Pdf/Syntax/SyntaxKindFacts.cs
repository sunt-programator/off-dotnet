// <copyright file="SyntaxKindFacts.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Pdf.Syntax;

/// <summary>Provides utility methods for <see cref="SyntaxKind"/>.</summary>
internal static class SyntaxKindFacts
{
    /// <summary>Determines whether the specified <see cref="SyntaxKind"/> is trivia.</summary>
    /// <param name="kind">The syntax kind to check.</param>
    /// <returns><c>true</c> if the specified syntax kind is trivia; otherwise, <c>false</c>.</returns>
    public static bool IsTrivia(this SyntaxKind kind)
    {
        switch (kind)
        {
            case SyntaxKind.EndOfLineTrivia:
            case SyntaxKind.WhitespaceTrivia:
            case SyntaxKind.SingleLineCommentTrivia:
                return true;
            default:
                return false;
        }
    }
}

// <copyright file="SyntaxFactory.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax;

public static class SyntaxFactory
{
    public static SyntaxTrivia SyntaxTrivia(SyntaxKind kind, string text)
    {
        if (kind is SyntaxKind.WhitespaceTrivia or SyntaxKind.EndOfLineTrivia or SyntaxKind.SingleLineCommentTrivia)
        {
            var greenNode = InternalSyntax.SyntaxFactory.Trivia(kind, text);
            return new SyntaxTrivia(default, greenNode, position: 0, index: 0);
        }

        throw new ArgumentException($"Unexpected {nameof(SyntaxKind)}: {kind}.", nameof(kind));
    }
}

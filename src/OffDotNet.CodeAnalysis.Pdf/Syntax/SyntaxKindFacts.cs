// <copyright file="SyntaxKindFacts.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Pdf.Syntax;

public static class SyntaxKindFacts
{
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

// <copyright file="SyntaxKind.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Pdf.Syntax;

using OffDotNet.CodeAnalysis.Syntax;

public enum SyntaxKind : ushort
{
    None = AbstractNode.NoneKind,
    List = AbstractNode.ListKind,

    // Trivia
    EndOfLineTrivia,
    WhitespaceTrivia,
    SingleLineCommentTrivia,
}

// <copyright file="SyntaxKind.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Pdf.Syntax;

using OffDotNet.CodeAnalysis.Syntax;

/// <summary>Represents the different kinds of syntax elements.</summary>
public enum SyntaxKind : ushort
{
    /// <summary>Represents no syntax kind.</summary>
    None = AbstractNode.NoneKind,

    /// <summary>Represents a list of syntax elements.</summary>
    List = AbstractNode.ListKind,

    // Tokens

    /// <summary>Represents a minus token ('-').</summary>
    MinusToken,

    // Keywords

    /// <summary>Represents the 'true' keyword.</summary>
    TrueKeyword,

    /// <summary>Represents the 'false' keyword.</summary>
    FalseKeyword,

    /// <summary>Represents the 'null' keyword.</summary>
    NullKeyword,

    /// <summary>Represents a bad token.</summary>
    BadToken,

    // Trivia

    /// <summary>Represents end-of-line trivia.</summary>
    EndOfLineTrivia,

    /// <summary>Represents whitespace trivia.</summary>
    WhitespaceTrivia,

    /// <summary>Represents single-line comment trivia.</summary>
    SingleLineCommentTrivia,
}

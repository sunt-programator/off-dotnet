// <copyright file="SyntaxKind.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Old.Syntax;

public enum SyntaxKind : ushort
{
    /// <summary> Represents unknown token.</summary>
    None = 0,

    // Delimiters
    LeftParenthesisToken,

    RightParenthesisToken,

    LessThanToken,

    GreaterThanToken,

    LeftSquareBracketToken,

    RightSquareBracketToken,

    LeftCurlyBracketToken,

    RightCurlyBracketToken,

    SolidusToken,

    PercentSignToken,

    LessThanLessThanToken,

    GreaterThanGreaterThanToken,

    PlusToken,

    MinusToken,

    // Trivia
    SingleLineCommentTrivia,

    // Keywords
    TrueKeyword,

    FalseKeyword,

    NullKeyword,

    StartObjectKeyword,

    EndObjectKeyword,

    IndirectReferenceKeyword,

    StartStreamKeyword,

    EndStreamKeyword,

    XRefKeyword,

    TrailerKeyword,

    StartXRefKeyword,

    // Other
    EndOfFileToken,

    // Primitives
    BadToken,

    NumericLiteralToken,

    StringLiteralToken,

    NameLiteralToken,
}

// <copyright file="SyntaxKind.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax;

public enum SyntaxKind : ushort
{
    /// <summary>Represents an unknown token.</summary>
    None = 0,

    // Punctuations

    /// <summary>Represents <c>(</c> token.</summary>
    LeftParenthesisToken = 8100,

    /// <summary>Represents <c>)</c> token.</summary>
    RightParenthesisToken = 8101,

    /// <summary>Represents <c>&lt;</c> token.</summary>
    LessThanToken = 8102,

    /// <summary>Represents <c>&gt;</c> token.</summary>
    GreaterThanToken = 8103,

    /// <summary>Represents <c>[</c> token.</summary>
    LeftSquareBracketToken = 8104,

    /// <summary>Represents <c>]</c> token.</summary>
    RightSquareBracketToken = 8105,

    /// <summary>Represents <c>{</c> token.</summary>
    LeftCurlyBracketToken = 8106,

    /// <summary>Represents <c>}</c> token.</summary>
    RightCurlyBracketToken = 8107,

    /// <summary>Represents <c>&lt;&lt;</c> token.</summary>
    LessThanLessThanToken = 8108,

    /// <summary>Represents <c>&gt;&gt;</c> token.</summary>
    GreaterThanGreaterThanToken = 8109,

    /// <summary>Represents <c>/</c> token.</summary>
    SolidusToken = 8110,

    /// <summary>Represents <c>%</c> token.</summary>
    PercentSignToken = 8111,

    /// <summary>Represents <c>+</c> token.</summary>
    PlusToken = 8112,

    /// <summary>Represents <c>-</c> token.</summary>
    MinusToken = 8113,

    // Keywords

    /// <summary>Represents <c>true</c> keyword.</summary>
    TrueKeyword = 8300,

    /// <summary>Represents <c>false</c> keyword.</summary>
    FalseKeyword = 8301,

    /// <summary>Represents <c>null</c> keyword.</summary>
    NullKeyword = 8302,

    /// <summary>Represents <c>obj</c> keyword.</summary>
    StartObjectKeyword = 8303,

    /// <summary>Represents <c>endobj</c> keyword.</summary>
    EndObjectKeyword = 8304,

    /// <summary>Represents <c>R</c> keyword.</summary>
    IndirectReferenceKeyword = 8305,

    /// <summary>Represents <c>stream</c> keyword.</summary>
    StartStreamKeyword = 8306,

    /// <summary>Represents <c>endstream</c> keyword.</summary>
    EndStreamKeyword = 8307,

    /// <summary>Represents <c>xref</c> keyword.</summary>
    XrefKeyword = 8308,

    /// <summary>Represents <c>startxref</c> keyword.</summary>
    StartXrefKeyword = 8309,

    /// <summary>Represents <c>trailer</c> keyword.</summary>
    TrailerKeyword = 8310,

    // Tokens with text

    /// <summary>Represents a numeric literal token.</summary>
    NumericLiteralToken = 8509,
}

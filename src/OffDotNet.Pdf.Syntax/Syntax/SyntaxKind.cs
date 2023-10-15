// <copyright file="SyntaxKind.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Syntax.Syntax;

public enum SyntaxKind : ushort
{
    /// <summary> Represents unknown token.</summary>
    None = 0,

    // White-space characters

    /// <summary> Represents <c>0x0</c> (NUL) token.</summary>
    NullToken = 1,

    /// <summary> Represents <c>\t</c> (Horizontal Tab) token.</summary>
    HorizontalTabToken = 2,

    /// <summary> Represents <c>\n</c> (Line Feed) token.</summary>
    LineFeedToken = 3,

    /// <summary> Represents <c>^L</c> (Form Feed) token.</summary>
    FormFeedToken = 4,

    /// <summary> Represents <c>\r</c> (Carriage Return) token.</summary>
    CarriageReturnToken = 5,

    /// <summary> Represents space token.</summary>
    SpaceToken = 6,

    // Delimiters

    /// <summary> Represents <c>(</c> token.</summary>
    LeftParenthesisToken = 7,

    /// <summary> Represents <c>)</c> token.</summary>
    RightParenthesisToken = 8,

    /// <summary> Represents <c>&lt;</c> token.</summary>
    LessThanToken = 9,

    /// <summary> Represents <c>&gt;</c> token.</summary>
    GreaterThanToken = 10,

    /// <summary> Represents <c>[</c> token.</summary>
    LeftSquareBracketToken = 11,

    /// <summary> Represents <c>]</c> token.</summary>
    RightSquareBracketToken = 12,

    /// <summary> Represents <c>{</c> token.</summary>
    LeftCurlyBracketToken = 13,

    /// <summary> Represents <c>}</c> token.</summary>
    RightCurlyBracketToken = 14,

    /// <summary> Represents <c>/</c> (Solidus or Slash) token.</summary>
    SolidusToken = 15,

    /// <summary> Represents <c>%</c> token.</summary>
    PercentToken = 16,

    // Tokens with text

    /// <summary> Represents <c>%</c> token.</summary>
    NumericLiteralToken,
}

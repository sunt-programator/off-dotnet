// <copyright file="TokenInfo.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Lexer;

using System.Diagnostics.CodeAnalysis;
using Syntax;

/// <summary>Represents the token info.</summary>
[SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Fine for token info.")]
internal struct TokenInfo
{
    /// <summary>The kind of the token.</summary>
    public SyntaxKind Kind;

    /// <summary>The special value kind of the token.</summary>
    public TokenInfoSpecialType ValueKind;

    /// <summary>The text of the token.</summary>
    public string Text;

    /// <summary>The integer value of the token.</summary>
    public int IntValue;

    /// <summary>The real value of the token.</summary>
    public double RealValue;

    /// <summary>The string value of the token.</summary>
    public string StringValue;
}

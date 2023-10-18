// <copyright file="SyntaxKind.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax;

public enum SyntaxKind : ushort
{
    /// <summary> Represents unknown token.</summary>
    None = 0,

    /// <summary> Represents <c>%</c> token.</summary>
    NumericLiteralToken,

    /// <summary> Represents <c>EOF</c> (End of file) token.</summary>
    EndOfFileToken,
}

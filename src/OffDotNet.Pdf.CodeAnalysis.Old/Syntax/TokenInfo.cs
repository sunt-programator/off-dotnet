// <copyright file="TokenInfo.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Old.Syntax;

internal struct TokenInfo
{
    internal SyntaxKind Kind;
    internal string Text;
    internal TokenInfoSpecialKind ValueKind;
    internal int IntValue;
    internal float FloatValue;
    internal string? StringValue;
}
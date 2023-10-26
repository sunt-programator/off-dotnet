// <copyright file="SyntaxCharacterInfoFacts.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Parser;

public static class SyntaxCharacterInfoFacts
{
    internal static bool IsDecDigit(this byte? b)
    {
        return b.HasValue && b.Value.IsDecDigit();
    }

    internal static bool IsDecDigit(this byte b)
    {
        return b is >= 0x30 and <= 0x39;
    }

    internal static bool IsLetter(this byte? b)
    {
        return b.HasValue && b.Value.IsLetter();
    }

    internal static bool IsLetter(this byte b)
    {
        return b is >= 0x41 and <= 0x5a or >= 0x61 and <= 0x7a;
    }
}

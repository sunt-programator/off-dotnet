// <copyright file="SyntaxCharacterInfoFacts.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Old.Parser;

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

    internal static bool IsOctDigit(this byte? b)
    {
        return b.HasValue && b.Value.IsOctDigit();
    }

    internal static bool IsOctDigit(this byte b)
    {
        return b is >= 0x30 and <= 0x37;
    }

    internal static bool IsHexDigit(this byte? b)
    {
        return b.HasValue && b.Value.IsHexDigit();
    }

    internal static bool IsHexDigit(this byte b)
    {
        return b.IsDecDigit() || b is >= 0x41 and <= 0x46 or >= 0x61 and <= 0x66;
    }

    internal static bool IsLetter(this byte? b)
    {
        return b.HasValue && b.Value.IsLetter();
    }

    internal static bool IsLetter(this byte b)
    {
        return b is >= 0x41 and <= 0x5a or >= 0x61 and <= 0x7a;
    }

    internal static bool IsNewLine(this byte? b)
    {
        return b.HasValue && b.Value.IsNewLine();
    }

    internal static bool IsNewLine(this byte b)
    {
        return b is 0x0A or 0x0D;
    }
}

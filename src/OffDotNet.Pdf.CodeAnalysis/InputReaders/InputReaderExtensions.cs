// <copyright file="InputReaderExtensions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.InputReaders;

internal static class InputReaderExtensions
{
    internal static bool IsWhiteSpaceCharacter(this byte? b)
    {
        return b.HasValue && b.Value.IsWhiteSpaceCharacter();
    }

    internal static bool IsWhiteSpaceCharacter(this byte b)
    {
        return b is (byte)CharacterKind.Null
            or (byte)CharacterKind.HorizontalTab
            or (byte)CharacterKind.LineFeed
            or (byte)CharacterKind.FormFeed
            or (byte)CharacterKind.CarriageReturn
            or (byte)CharacterKind.Space;
    }

    internal static bool IsDecDigit(this byte? b)
    {
        return b.HasValue && b.Value.IsDecDigit();
    }

    internal static bool IsDecDigit(this byte b)
    {
        return b is >= (byte)CharacterKind.Digit0 and <= (byte)CharacterKind.Digit9;
    }

    internal static bool IsOctDigit(this byte? b)
    {
        return b.HasValue && b.Value.IsOctDigit();
    }

    internal static bool IsOctDigit(this byte b)
    {
        return b is >= (byte)CharacterKind.Digit0 and <= (byte)CharacterKind.Digit7;
    }

    internal static bool IsHexDigit(this byte? b)
    {
        return b.HasValue && b.Value.IsHexDigit();
    }

    internal static bool IsHexDigit(this byte b)
    {
        return b.IsDecDigit() || b is >= (byte)CharacterKind.CapitalLetterA and <= (byte)CharacterKind.CapitalLetterF or >= (byte)CharacterKind.SmallLetterA and <= (byte)CharacterKind.SmallLetterF;
    }

    internal static bool IsLetter(this byte? b)
    {
        return b.HasValue && b.Value.IsLetter();
    }

    internal static bool IsLetter(this byte b)
    {
        return b is >= (byte)CharacterKind.CapitalLetterA and <= (byte)CharacterKind.CapitalLetterZ or >= (byte)CharacterKind.SmallLetterA and <= (byte)CharacterKind.SmallLetterZ;
    }

    internal static bool IsNewLine(this byte? b)
    {
        return b.HasValue && b.Value.IsNewLine();
    }

    internal static bool IsNewLine(this byte b)
    {
        return b is (byte)CharacterKind.CarriageReturn or (byte)CharacterKind.LineFeed;
    }
}

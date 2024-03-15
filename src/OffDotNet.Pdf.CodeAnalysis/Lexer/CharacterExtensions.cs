// <copyright file="CharacterExtensions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Lexer;

using System.Diagnostics.CodeAnalysis;
using System.Globalization;

/// <summary>Defines extension methods for the <see cref="byte"/> type.</summary>
internal static class CharacterExtensions
{
    /// <summary>Determines whether the specified byte is a decimal digit.</summary>
    /// <param name="b">The byte to check.</param>
    /// <returns><see langword="true" /> if the specified byte is a decimal digit; otherwise, <see langword="false" />.</returns>
    public static bool IsDecDigit([NotNullWhen(true)] this byte? b) => b is >= (byte)'0' and <= (byte)'9';

    /// <summary>Determines whether the specified byte is an alpha character.</summary>
    /// <param name="b">The byte to check.</param>
    /// <returns><see langword="true" /> if the specified byte is an alpha character; otherwise, <see langword="false" />.</returns>
    public static bool IsAlpha([NotNullWhen(true)] this byte? b) =>
        b is >= (byte)'a' and <= (byte)'z' or >= (byte)'A' and <= (byte)'Z';

    /// <summary>Determines whether the specified byte is an octal digit.</summary>
    /// <param name="b">The byte to check.</param>
    /// <returns><see langword="true" /> if the specified byte is an octal digit; otherwise, <see langword="false" />.</returns>
    public static bool IsOctDigit([NotNullWhen(true)] this byte? b) => b is >= (byte)'0' and <= (byte)'7';

    /// <summary>Determines whether the specified byte is a hexadecimal digit.</summary>
    /// <param name="b">The byte to check.</param>
    /// <returns><see langword="true" /> if the specified byte is a hexadecimal digit; otherwise, <see langword="false" />.</returns>
    public static bool IsHexDigit([NotNullWhen(true)] this byte? b) => b is >= (byte)'0' and <= (byte)'9'
        or >= (byte)'A' and <= (byte)'F'
        or >= (byte)'a' and <= (byte)'f';

    /// <summary>Determines whether the specified byte is a whitespace character.</summary>
    /// <param name="b">The byte to check.</param>
    /// <returns><see langword="true" /> if the specified byte is a whitespace character; otherwise, <see langword="false" />.</returns>
    public static bool IsEndOfLine([NotNullWhen(true)] this byte? b) => b is (byte)'\r' or (byte)'\n';

    /// <summary>Determines whether the specified byte is a whitespace character.</summary>
    /// <param name="b">The byte to check.</param>
    /// <param name="ignoreNullByte">Whether to ignore the null byte.</param>
    /// <returns><see langword="true" /> if the specified byte is a whitespace character; otherwise, <see langword="false" />.</returns>
    public static bool IsWhiteSpace([NotNullWhen(true)] this byte? b, bool ignoreNullByte = false) =>
        (!ignoreNullByte && b is (byte)'\0') ||
        b is (byte)'\t' or (byte)'\f' or (byte)' ' ||
        b.IsEndOfLine();

    /// <summary>Builds a character from a pair of bytes.</summary>
    /// <example>
    /// '20' -> ' '.
    /// '37' -> '%'.
    /// </example>
    /// <param name="context">The lexer context.</param>
    /// <param name="addTrailingZero">Whether to add a trailing zero to the last byte.</param>
    /// <param name="i">The index of the byte in the pair.</param>
    /// <param name="bytes">The pair of bytes.</param>
    public static void BuildCharFromStringHex(
        this LexerContext context,
        bool addTrailingZero,
        ref int i,
        ref Span<char> bytes)
    {
        // Only build a char if we have a pair of bytes.
        if (i % 2 != 1)
        {
            return;
        }

        // If the number of bytes is odd, we need to add a trailing 0 to the last byte.
        if (addTrailingZero)
        {
            bytes[1] = '0';
        }

        int.TryParse(bytes, NumberStyles.HexNumber, null, out var byteValue);
        context.StringBuilderCache.Append((char)byteValue);
    }
}

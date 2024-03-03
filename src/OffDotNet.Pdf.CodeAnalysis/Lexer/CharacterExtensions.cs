// <copyright file="CharacterExtensions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Lexer;

/// <summary>Defines extension methods for the <see cref="byte"/> type.</summary>
internal static class CharacterExtensions
{
    /// <summary>Determines whether the specified byte is a decimal digit.</summary>
    /// <param name="b">The byte to check.</param>
    /// <returns><see langword="true" /> if the specified byte is a decimal digit; otherwise, <see langword="false" />.</returns>
    public static bool IsDecDigit(this byte? b) => b is >= (byte)'0' and <= (byte)'9';
}

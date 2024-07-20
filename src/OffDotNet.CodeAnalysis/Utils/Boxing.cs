// <copyright file="Boxing.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Utils;

/// <summary>
/// Provides boxed versions of common value types.
/// </summary>
internal static class Boxing
{
    /// <summary>
    /// A boxed representation of the boolean value <c>true</c>.
    /// </summary>
    public static readonly object True = true;

    /// <summary>
    /// A boxed representation of the boolean value <c>false</c>.
    /// </summary>
    public static readonly object False = false;
}

// <copyright file="ExceptionUtilities.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Utils;

using System.Runtime.CompilerServices;

/// <summary>
/// Provides utility methods for handling exceptions.
/// </summary>
internal static class ExceptionUtilities
{
    /// <summary>
    /// Throws an <see cref="InvalidOperationException"/> indicating that the program location is thought to be unreachable.
    /// </summary>
    /// <param name="path">The file path of the caller. This is automatically provided by the compiler.</param>
    /// <param name="line">The line number of the caller. This is automatically provided by the compiler.</param>
    /// <returns>An <see cref="InvalidOperationException"/> indicating an unreachable program location.</returns>
    internal static Exception Unreachable([CallerFilePath] string? path = null, [CallerLineNumber] int line = 0)
        => new InvalidOperationException($"This program location is thought to be unreachable. File='{path}' Line={line}");
}

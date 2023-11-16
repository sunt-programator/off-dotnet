// <copyright file="ExceptionUtilities.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Runtime.CompilerServices;

namespace OffDotNet.Pdf.CodeAnalysis.InternalUtilities;

internal static class ExceptionUtilities
{
    internal static Exception Unreachable([CallerFilePath] string? path = null, [CallerLineNumber] int line = 0)
    {
        return new InvalidOperationException($"This program location is thought to be unreachable. File='{path}' Line={line}");
    }
}

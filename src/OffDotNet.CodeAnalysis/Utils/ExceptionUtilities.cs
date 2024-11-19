﻿// <copyright file="ExceptionUtilities.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Utils;

using System.Runtime.CompilerServices;

internal static class ExceptionUtilities
{
    internal static Exception Unreachable([CallerFilePath] string? path = null, [CallerLineNumber] int line = 0)
        => new InvalidOperationException($"This program location is thought to be unreachable. File='{path}' Line={line}");
}

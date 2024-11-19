// <copyright file="TextCursorOptions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Configs;

/// <summary>
/// Represents the text cursor options for the OffDotNet analysis.
/// </summary>
public sealed record TextCursorOptions
{
    /// <summary>
    /// Gets the window size for the text cursor.
    /// </summary>
    public int WindowSize { get; init; } = 2048;
}

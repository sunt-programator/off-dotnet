// <copyright file="TextCursorOptions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Configs;

public sealed record TextCursorOptions
{
    public int WindowSize { get; init; } = 2048;
}

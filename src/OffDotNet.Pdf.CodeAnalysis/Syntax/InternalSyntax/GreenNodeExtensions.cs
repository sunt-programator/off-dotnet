// <copyright file="GreenNodeExtensions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

internal static class GreenNodeExtensions
{
    /// <summary>Gets the first terminal node of a <see cref="GreenNode"/>.</summary>
    /// <param name="node">The <see cref="GreenNode"/> to get the first terminal node from.</param>
    /// <returns>The first terminal node of a <see cref="GreenNode"/>.</returns>
    internal static GreenNode? GetFirstTerminal(this GreenNode? node)
    {
        return null;
    }

    /// <summary>Gets the last terminal node of a <see cref="GreenNode"/>.</summary>
    /// <param name="node">The <see cref="GreenNode"/> to get the last terminal node from.</param>
    /// <returns>The last terminal node of a <see cref="GreenNode"/>.</returns>
    internal static GreenNode? GetLastTerminal(this GreenNode? node)
    {
        return null;
    }
}

// <copyright file="GreenNodeExtensions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

internal static class GreenNodeExtensions
{
    /// <summary>Gets the first terminal node of a <see cref="GreenNode"/>.</summary>
    /// <param name="source">The <see cref="GreenNode"/> to get the first terminal node from.</param>
    /// <returns>The first terminal node of a <see cref="GreenNode"/>.</returns>
    internal static GreenNode? GetFirstTerminal(this GreenNode? source)
    {
        var node = source;

        if (node == null)
        {
            return null;
        }

        do
        {
            GreenNode? firstChild = null;
            for (var i = 0; i < node.SlotCount; i++)
            {
                var child = node.GetSlot(i);
                if (child != null)
                {
                    firstChild = child;
                    break;
                }
            }

            node = firstChild;
        }
        while (node?.SlotCount > 0);

        return node;
    }

    /// <summary>Gets the last terminal node of a <see cref="GreenNode"/>.</summary>
    /// <param name="source">The <see cref="GreenNode"/> to get the last terminal node from.</param>
    /// <returns>The last terminal node of a <see cref="GreenNode"/>.</returns>
    internal static GreenNode? GetLastTerminal(this GreenNode? source)
    {
        var node = source;

        if (node == null)
        {
            return null;
        }

        do
        {
            GreenNode? lastChild = null;
            for (var i = node.SlotCount - 1; i >= 0; i--)
            {
                var child = node.GetSlot(i);
                if (child != null)
                {
                    lastChild = child;
                    break;
                }
            }

            node = lastChild;
        }
        while (node?.SlotCount > 0);

        return node;
    }

    /// <summary>Gets the first non null child index.</summary>
    /// <param name="node">The <see cref="GreenNode"/> to get the first non null child index from.</param>
    /// <returns>The first non null child index.</returns>
    internal static int GetFirstNonNullChildIndex(this GreenNode node)
    {
        var firstIndex = 0;
        for (; firstIndex < node.SlotCount; firstIndex++)
        {
            var child = node.GetSlot(firstIndex);
            if (child != null)
            {
                break;
            }
        }

        return firstIndex;
    }

    /// <summary>Gets the last non null child index.</summary>
    /// <param name="node">The <see cref="GreenNode"/> to get the last non null child index from.</param>
    /// <returns>The last non null child index.</returns>
    internal static int GetLastNonNullChildIndex(this GreenNode node)
    {
        var lastIndex = node.SlotCount - 1;
        for (; lastIndex >= 0; lastIndex--)
        {
            var child = node.GetSlot(lastIndex);
            if (child != null)
            {
                break;
            }
        }

        return lastIndex;
    }
}

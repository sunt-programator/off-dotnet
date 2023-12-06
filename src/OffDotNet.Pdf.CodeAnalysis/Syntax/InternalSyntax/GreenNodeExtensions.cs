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
        GreenNode? node = source;

        if (node == null)
        {
            return null;
        }

        do
        {
            GreenNode? firstChild = null;
            for (int i = 0; i < node.SlotCount; i++)
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
        GreenNode? node = source;

        if (node == null)
        {
            return null;
        }

        do
        {
            GreenNode? lastChild = null;
            for (int i = node.SlotCount - 1; i >= 0; i--)
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
}

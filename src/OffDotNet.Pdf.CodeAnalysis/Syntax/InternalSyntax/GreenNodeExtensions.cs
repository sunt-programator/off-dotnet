// <copyright file="GreenNodeExtensions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

using Collections;

/// <summary>Contains extension methods for the <see cref="GreenNode"/> class.</summary>
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
            for (var i = 0; i < node.Count; i++)
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
        while (node?.Count > 0);

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
            for (var i = node.Count - 1; i >= 0; i--)
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
        while (node?.Count > 0);

        return node;
    }

    /// <summary>Gets the first non null child index.</summary>
    /// <param name="node">The <see cref="GreenNode"/> to get the first non null child index from.</param>
    /// <returns>The first non-null child index.</returns>
    internal static int GetFirstNonNullChildIndex(this GreenNode node)
    {
        var firstIndex = 0;
        for (; firstIndex < node.Count; firstIndex++)
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
    /// <returns>The last non-null child index.</returns>
    internal static int GetLastNonNullChildIndex(this GreenNode node)
    {
        var lastIndex = node.Count - 1;
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

    /// <summary>Creates a list of <see cref="GreenNode"/> from a list of <typeparamref name="TFrom"/>.</summary>
    /// <param name="list">The list of <typeparamref name="TFrom"/> to create a list of <see cref="GreenNode"/> from.</param>
    /// <param name="select">The function to select the <see cref="GreenNode"/> from the <typeparamref name="TFrom"/>.</param>
    /// <typeparam name="TFrom">The type of the source list.</typeparam>
    /// <returns>A list of <see cref="GreenNode"/> from a list of <typeparamref name="TFrom"/>.</returns>
    internal static GreenNode? CreateList<TFrom>(IReadOnlyList<TFrom> list, Func<TFrom, GreenNode> select)
    {
        switch (list.Count)
        {
            case 0:
                return null;
            case 1:
                return select(list[0]);
            case 2:
                return SyntaxFactory.List(select(list[0]), select(list[1]));
            case 3:
                return SyntaxFactory.List(select(list[0]), select(list[1]), select(list[2]));
            default:
                {
                    var array = new ArrayElement<GreenNode>[list.Count];
                    for (var i = 0; i < array.Length; i++)
                    {
                        array[i]._value = select(list[i]);
                    }

                    return SyntaxFactory.List(array);
                }
        }
    }
}

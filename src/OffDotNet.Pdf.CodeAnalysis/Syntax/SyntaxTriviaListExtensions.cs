// <copyright file="SyntaxTriviaListExtensions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax;

using InternalSyntax;

/// <summary>Contains extension methods for the <see cref="SyntaxTriviaList"/> class.</summary>
public static class SyntaxTriviaListExtensions
{
    /// <summary>Returns the index of the specified trivia in the list.</summary>
    /// <param name="list">The list of trivia.</param>
    /// <param name="trivia">The trivia to search for.</param>
    /// <returns>The index of the trivia in the list, or -1 if the trivia is not found.</returns>
    public static int IndexOf(this SyntaxTriviaList list, SyntaxTrivia trivia)
    {
        for (var i = 0; i < list.Count; i++)
        {
            if (list[i] == trivia)
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>Returns the index of the first trivia with the specified kind in the list.</summary>
    /// <param name="list">The list of trivia.</param>
    /// <param name="kind">The kind of trivia to search for.</param>
    /// <returns>The index of the trivia in the list, or -1 if the trivia is not found.</returns>
    public static int IndexOf(this SyntaxTriviaList list, SyntaxKind kind)
    {
        for (var i = 0; i < list.Count; i++)
        {
            if (list[i].Kind == kind)
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>Removes the trivia at the specified index from the list.</summary>
    /// <param name="list">The list of trivia.</param>
    /// <param name="index">The index of the trivia to remove.</param>
    /// <returns>A new list of trivia with the specified trivia removed.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the index is negative or greater than or equal to the number of trivia in the list.</exception>
    public static SyntaxTriviaList RemoveAt(this SyntaxTriviaList list, int index)
    {
        if (index < 0 || index >= list.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        var list1 = list.ToList();
        list1.RemoveAt(index);
        return new SyntaxTriviaList(list.Token, GreenNodeExtensions.CreateList(list1, static x => x.UnderlyingNode), list.Position, list.Index);
    }

    /// <summary>Removes the specified trivia from the list if it is found.</summary>
    /// <param name="list">The list of trivia.</param>
    /// <param name="trivia">The trivia to remove.</param>
    /// <returns>A new list of trivia with the specified trivia removed.</returns>
    public static SyntaxTriviaList RemoveAt(this SyntaxTriviaList list, SyntaxTrivia trivia)
    {
        var index = list.IndexOf(trivia);
        return index > -1 ? list.RemoveAt(index) : list;
    }

    public static SyntaxTriviaList InsertRange(this SyntaxTriviaList list, int index, IEnumerable<SyntaxTrivia> trivia)
    {
        var count = list.Count;
        if (index < 0 || index > count)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        if (trivia.TryGetNonEnumeratedCount(out var enumerableCount) && enumerableCount == 0)
        {
            return list;
        }

        return default;
    }
}

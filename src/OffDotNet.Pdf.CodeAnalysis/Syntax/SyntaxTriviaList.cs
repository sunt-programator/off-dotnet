// <copyright file="SyntaxTriviaList.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax;

using System.Collections;
using System.Runtime.InteropServices;
using Collections;
using InternalSyntax;
using Text;

/// <summary>Represents a read-only list of <see cref="SyntaxTrivia"/> instances.</summary>
[StructLayout(LayoutKind.Auto)]
public readonly partial struct SyntaxTriviaList : IEquatable<SyntaxTriviaList>, IReadOnlyList<SyntaxTrivia>
{
    /// <summary>Initializes a new instance of the <see cref="SyntaxTriviaList"/> struct.</summary>
    /// <param name="trivia">The syntax trivia.</param>
    public SyntaxTriviaList(SyntaxTrivia trivia)
    {
        Token = default;
        UnderlyingUnderlyingNode = trivia.UnderlyingNode;
        Position = 0;
        Index = 0;
    }

    /// <summary>Initializes a new instance of the <see cref="SyntaxTriviaList"/> struct.</summary>
    /// <param name="token">The syntax token.</param>
    /// <param name="underlyingNode">The green node that represents the trivia list.</param>
    /// <param name="position">The absolute position of the trivia list in the source text.</param>
    /// <param name="index">The index of the trivia list in the parent list.</param>
    internal SyntaxTriviaList(SyntaxToken token, GreenNode? underlyingNode, int? position = null, int index = 0)
    {
        Token = token;
        UnderlyingUnderlyingNode = underlyingNode;
        Position = position ?? token.Position;
        Index = index;
    }

    /// <summary>Gets the empty syntax trivia list.</summary>
    public static SyntaxTriviaList Empty => [];

    /// <summary>Gets the absolute span of the trivia list, not including the leading and trailing trivia of the first and last elements.</summary>
    public TextSpan Span => UnderlyingUnderlyingNode is null
        ? default
        : new TextSpan(
            start: Position + UnderlyingUnderlyingNode.LeadingTriviaWidth,
            length: UnderlyingUnderlyingNode.Width);

    /// <summary>Gets the absolute span of the trivia list, including the leading and trailing trivia of the first and last elements.</summary>
    public TextSpan FullSpan => UnderlyingUnderlyingNode is null
        ? default
        : new TextSpan(
            start: Position,
            length: UnderlyingUnderlyingNode.FullWidth);

    /// <summary>Gets the total number of trivia in the list.</summary>
    public int Count
    {
        get
        {
            if (UnderlyingUnderlyingNode is null)
            {
                return 0;
            }

            return UnderlyingUnderlyingNode.Kind == SyntaxKind.List ? UnderlyingUnderlyingNode.Count : 1;
        }
    }

    /// <summary>
    /// Gets the syntax token.
    /// </summary>
    internal SyntaxToken Token { get; }

    /// <summary>Gets the green node that represents the trivia list.</summary>
    internal GreenNode? UnderlyingUnderlyingNode { get; }

    /// <summary>Gets the absolute position of the trivia list in the source text.</summary>
    internal int Position { get; }

    /// <summary>Gets the index of the trivia list in the parent list.</summary>
    internal int Index { get; }

    /// <summary>Gets the trivia at the specified index.</summary>
    /// <param name="index">The index of the trivia to get.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the index is less than 0 or greater than the underlying node slot count.</exception>
    public SyntaxTrivia this[int index]
    {
        get
        {
            if (index < 0 || UnderlyingUnderlyingNode is null)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            if (UnderlyingUnderlyingNode.Kind == SyntaxKind.List)
            {
                if (!unchecked((uint)index < (uint)UnderlyingUnderlyingNode.Count))
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }

                var position = Position + UnderlyingUnderlyingNode.GetSlotOffset(index);
                return new SyntaxTrivia(Token, UnderlyingUnderlyingNode.GetSlot(index)!, position, Index + index);
            }

            if (index == 0)
            {
                return new SyntaxTrivia(Token, UnderlyingUnderlyingNode, Position, Index);
            }

            throw new ArgumentOutOfRangeException(nameof(index));
        }
    }

    public static bool operator ==(SyntaxTriviaList left, SyntaxTriviaList right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(SyntaxTriviaList left, SyntaxTriviaList right)
    {
        return !left.Equals(right);
    }

    /// <summary>Returns a string representation of the underlying node, not including the leading and trailing trivia.</summary>
    /// <returns>A string representation of the underlying node, not including the leading and trailing trivia.</returns>
    public override string ToString()
    {
        return UnderlyingUnderlyingNode?.ToString() ?? string.Empty;
    }

    /// <summary>Returns an enumerator that iterates through the trivia list.</summary>
    /// <returns>An enumerator that iterates through the trivia list.</returns>
    public Enumerator GetEnumerator()
    {
        return new Enumerator(in this);
    }

    /// <summary>Returns an enumerator that iterates through the trivia list.</summary>
    /// <returns>An enumerator that iterates through the trivia list.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return UnderlyingUnderlyingNode is null ? EmptyEnumerator<SyntaxTrivia>.Instance : new EnumeratorImpl(in this);
    }

    /// <summary>Returns an enumerator that iterates through the trivia list.</summary>
    /// <returns>An enumerator that iterates through the trivia list.</returns>
    IEnumerator<SyntaxTrivia> IEnumerable<SyntaxTrivia>.GetEnumerator()
    {
        return UnderlyingUnderlyingNode is null ? EmptyEnumerator<SyntaxTrivia>.Instance : new EnumeratorImpl(in this);
    }

    /// <summary>Returns a string representation of the underlying node, including the leading and trailing trivia.</summary>
    /// <returns>A string representation of the underlying node, including the leading and trailing trivia.</returns>
    public string ToFullString()
    {
        return UnderlyingUnderlyingNode?.ToFullString() ?? string.Empty;
    }

    /// <summary>Checks if the current trivia list is equal to another trivia list.</summary>
    /// <param name="other">The other trivia list to compare to.</param>
    /// <returns><see langword="true"/> if the current trivia list is equal to the other trivia list; otherwise, <see langword="false"/>.</returns>
    public bool Equals(SyntaxTriviaList other)
    {
        return Equals(UnderlyingUnderlyingNode, other.UnderlyingUnderlyingNode) && Index == other.Index && Token.Equals(other.Token);
    }

    /// <summary>Checks if the current trivia list is equal to another trivia list.</summary>
    /// <param name="obj">The other trivia list to compare to.</param>
    /// <returns><see langword="true"/> if the current trivia list is equal to the other trivia list; otherwise, <see langword="false"/>.</returns>
    public override bool Equals(object? obj)
    {
        return obj is SyntaxTriviaList other && Equals(other);
    }

    /// <summary>Gets the hash code for the current trivia list.</summary>
    /// <returns>The hash code for the current trivia list.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(Token, UnderlyingUnderlyingNode, Index);
    }
}

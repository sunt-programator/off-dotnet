// <copyright file="SyntaxToken.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax;

using System.Diagnostics;
using System.Runtime.InteropServices;
using InternalSyntax;
using Text;

/// <summary>Represents a token in the syntax tree.</summary>
[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
[StructLayout(LayoutKind.Auto)]
public readonly struct SyntaxToken : IEquatable<SyntaxToken>
{
    /// <summary>Initializes a new instance of the <see cref="SyntaxToken"/> struct.</summary>
    /// <param name="parent">The parent node that contains this token.</param>
    /// <param name="token">The green node that represents the token.</param>
    /// <param name="position">The absolute position of the token in the source text.</param>
    /// <param name="index">The index of the token in the parent token list.</param>
    internal SyntaxToken(AbstractSyntaxNode? parent, GreenNode token, int position, int index)
    {
        Debug.Assert(token.IsToken, "Invalid token node.");
        Debug.Assert(parent == null || parent.UnderlyingNode.Kind != SyntaxKind.List, "List cannot be a parent.");
        this.UnderlyingNode = token;
        this.Position = position;
        this.Index = index;
        this.Parent = parent;
    }

    /// <summary>Gets the kind of the token.</summary>
    public SyntaxKind Kind => this.UnderlyingNode.Kind;

    /// <summary>Gets the text of the token.</summary>
    public string Text => ToString();

    /// <summary>Gets the value of the token.</summary>
    /// <remarks>If the token represents an integer literal, then this property would return <see cref="int"/> type.</remarks>
    public object? Value => this.UnderlyingNode.Value;

    /// <summary>Gets the absolute span of this token, not including its leading or trailing trivia.</summary>
    public TextSpan Span => new(Position + UnderlyingNode.LeadingTriviaWidth, UnderlyingNode.Width);

    /// <summary>Gets the absolute span of this token, including its leading or trailing trivia.</summary>
    public TextSpan FullSpan => new(this.Position, this.FullWidth);

    /// <summary>Gets the parent node that contains this token.</summary>
    public AbstractSyntaxNode? Parent { get; }

    /// <summary>Gets the syntax tree that this token belongs to.</summary>
    public AbstractSyntaxTree? SyntaxTree => Parent?.SyntaxTree;

    /// <summary>Gets the green node that this token wraps.</summary>
    internal GreenNode UnderlyingNode { get; }

    /// <summary>Gets the absolute position of the token in the source text.</summary>
    internal int Position { get; }

    /// <summary>Gets the index of the token in the parent token list.</summary>
    internal int Index { get; }

    /// <summary>Gets the width of the token.</summary>
    internal int Width => this.UnderlyingNode.Width;

    /// <summary>Gets the full width of the token.</summary>
    internal int FullWidth => this.UnderlyingNode.FullWidth;

    /// <summary>Gets the leading trivia of the token.</summary>
    internal int LeadingTriviaWidth => this.UnderlyingNode.LeadingTriviaWidth;

    /// <summary>Gets the trailing trivia of the token.</summary>
    internal int TrailingTriviaWidth => this.UnderlyingNode.TrailingTriviaWidth;

    public static bool operator ==(SyntaxToken left, SyntaxToken right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(SyntaxToken left, SyntaxToken right)
    {
        return !left.Equals(right);
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return this.UnderlyingNode.ToString();
    }

    /// <summary>Gets the full text of the token, including its leading and trailing trivia.</summary>
    /// <returns>The full text of the token, including its leading and trailing trivia.</returns>
    /// <remarks>The length of the returned string is equal to <see cref="FullSpan"/>.<see cref="TextSpan.Length"/>.</remarks>
    public string ToFullString()
    {
        return this.UnderlyingNode.ToFullString();
    }

    /// <inheritdoc/>
    public bool Equals(SyntaxToken other)
    {
        return this.Parent == other.Parent
               && this.UnderlyingNode == other.UnderlyingNode
               && this.Position == other.Position
               && this.Index == other.Index;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is SyntaxToken other && this.Equals(other);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return HashCode.Combine(this.Parent, this.UnderlyingNode, this.Position, this.Index);
    }

    private string GetDebuggerDisplay()
    {
        var kindText = this.UnderlyingNode != null ? this.UnderlyingNode.Kind.ToString() : "None";
        return $"{this.GetType().Name} {kindText} {this.ToString()}";
    }
}

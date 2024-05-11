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
    internal SyntaxToken(SyntaxNode? parent, GreenNode token, int position, int index)
    {
        Debug.Assert(token.IsToken, "Invalid token node.");
        Debug.Assert(parent == null || parent.UnderlyingNode.Kind != SyntaxKind.List, "List cannot be a parent.");
        this.UnderlyingNode = token;
        this.Position = position;
        this.Index = index;
        this.Parent = parent;
    }

    public SyntaxKind Kind => this.UnderlyingNode.Kind;

    public TextSpan FullSpan => new(this.Position, this.FullWidth);

    public SyntaxNode? Parent { get; }

    internal GreenNode UnderlyingNode { get; }

    internal int Position { get; }

    internal int Index { get; }

    internal int Width => this.UnderlyingNode.Width;

    internal int FullWidth => this.UnderlyingNode.FullWidth;

    public SyntaxTree? SyntaxTree => Parent?.SyntaxTree;

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

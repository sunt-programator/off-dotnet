// <copyright file="SyntaxTrivia.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax;

using System.Diagnostics;
using System.Runtime.InteropServices;
using InternalSyntax;
using Text;

/// <summary>Represents a trivia in the syntax tree.</summary>
[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
[StructLayout(LayoutKind.Auto)]
public readonly struct SyntaxTrivia : IEquatable<SyntaxTrivia>
{
    /// <summary>Initializes a new instance of the <see cref="SyntaxTrivia"/> struct.</summary>
    /// <param name="token">The parent token that contains this trivia.</param>
    /// <param name="triviaNode">The green node that represents the trivia.</param>
    /// <param name="position">The absolute position of the trivia in the source text.</param>
    /// <param name="index">The index of the trivia in the parent trivia list.</param>
    internal SyntaxTrivia(in SyntaxToken token, GreenNode? triviaNode, int position, int index) // skipcq: CS-R1138
    {
        Debug.Assert(triviaNode?.IsTrivia != false, "Invalid trivia node.");
        this.UnderlyingNode = triviaNode;
        this.Position = position;
        this.Index = index;
        this.SyntaxToken = token;
    }

    /// <summary>Gets the kind of the trivia.</summary>
    public SyntaxKind Kind => this.UnderlyingNode?.Kind ?? SyntaxKind.None;

    /// <summary>
    /// Gets the parent token that contains this trivia
    /// in <see cref="SyntaxToken.LeadingTrivia"/> or <see cref="SyntaxToken.TrailingTrivia"/>.
    /// </summary>
    public SyntaxToken SyntaxToken { get; }

    /// <summary>Gets the full span of the trivia.</summary>
    public TextSpan FullSpan => this.UnderlyingNode != null
        ? new TextSpan(this.Position, this.UnderlyingNode.FullWidth)
        : default;

    /// <summary>Gets the green node that this trivia wraps.</summary>
    internal GreenNode? UnderlyingNode { get; }

    /// <summary>Gets the absolute position of the trivia in the source text.</summary>
    internal int Position { get; }

    /// <summary>Gets the index of the trivia in the parent trivia list.</summary>
    internal int Index { get; }

    /// <summary>Gets the width of the trivia.</summary>
    internal int Width => this.UnderlyingNode?.Width ?? 0;

    /// <summary>Gets the full width of the trivia.</summary>
    internal int FullWidth => this.UnderlyingNode?.FullWidth ?? 0;

    public static bool operator ==(SyntaxTrivia left, SyntaxTrivia right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(SyntaxTrivia left, SyntaxTrivia right)
    {
        return !left.Equals(right);
    }

    /// <inheritdoc/>
    public bool Equals(SyntaxTrivia other)
    {
        return this.SyntaxToken == other.SyntaxToken
               && this.UnderlyingNode == other.UnderlyingNode
               && this.Position == other.Position
               && this.Index == other.Index;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is SyntaxTrivia other && this.Equals(other);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return HashCode.Combine(this.SyntaxToken, this.UnderlyingNode, this.Position, this.Index);
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return this.UnderlyingNode?.ToString() ?? string.Empty;
    }

    private string GetDebuggerDisplay()
    {
        var kindText = this.UnderlyingNode != null ? this.UnderlyingNode.Kind.ToString() : "None";
        return $"{this.GetType().Name} {kindText} {this.ToString()}";
    }
}

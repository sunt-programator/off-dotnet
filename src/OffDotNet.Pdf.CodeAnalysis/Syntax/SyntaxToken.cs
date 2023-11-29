// <copyright file="SyntaxToken.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Diagnostics;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using OffDotNet.Pdf.CodeAnalysis.Text;

namespace OffDotNet.Pdf.CodeAnalysis.Syntax;

[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
public readonly struct SyntaxToken : IEquatable<SyntaxToken>
{
    internal SyntaxToken(SyntaxNode? parent, GreenNode? token, int position, int index)
    {
        Debug.Assert(token?.IsToken != false, "Invalid token node.");
        this.Node = token;
        this.Position = position;
        this.Index = index;
        this.Parent = parent;
    }

    public SyntaxKind Kind => this.Node?.Kind ?? SyntaxKind.None;

    public TextSpan FullSpan => new(this.Position, this.FullWidth);

    public SyntaxNode? Parent { get; }

    internal GreenNode? Node { get; }

    internal int Position { get; }

    internal int Index { get; }

    internal int Width => this.Node?.Width ?? 0;

    internal int FullWidth => this.Node?.FullWidth ?? 0;

    public static bool operator ==(SyntaxToken left, SyntaxToken right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(SyntaxToken left, SyntaxToken right)
    {
        return !left.Equals(right);
    }

    public override string ToString()
    {
        return this.Node?.ToString() ?? string.Empty;
    }

    public bool Equals(SyntaxToken other)
    {
        return this.Parent == other.Parent
               && this.Node == other.Node
               && this.Position == other.Position
               && this.Index == other.Index;
    }

    public override bool Equals(object? obj)
    {
        return obj is SyntaxToken other && this.Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.Parent, this.Node, this.Position, this.Index);
    }

    private string GetDebuggerDisplay()
    {
        string kindText = this.Node != null ? this.Node.Kind.ToString() : "None";
        return $"{this.GetType().Name} {kindText} {this.ToString()}";
    }
}

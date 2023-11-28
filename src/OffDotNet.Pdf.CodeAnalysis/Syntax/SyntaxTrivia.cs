// <copyright file="SyntaxTrivia.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Diagnostics;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using OffDotNet.Pdf.CodeAnalysis.Text;

namespace OffDotNet.Pdf.CodeAnalysis.Syntax;

[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
public readonly struct SyntaxTrivia : IEquatable<SyntaxTrivia>
{
    internal SyntaxTrivia(in SyntaxToken token, GreenNode? triviaNode, int position, int index)
    {
        this.Node = triviaNode;
        this.Position = position;
        this.Index = index;
        this.SyntaxToken = token;
    }

    public SyntaxKind Kind => this.Node?.Kind ?? SyntaxKind.None;

    public SyntaxToken SyntaxToken { get; }

    public TextSpan FullSpan => this.Node != null ? new TextSpan(this.Position, this.FullWidth) : default;

    internal GreenNode? Node { get; }

    internal int Position { get; }

    internal int Index { get; }

    internal int Width => this.Node?.Width ?? 0;

    internal int FullWidth => this.Node?.FullWidth ?? 0;

    public static bool operator ==(SyntaxTrivia left, SyntaxTrivia right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(SyntaxTrivia left, SyntaxTrivia right)
    {
        return !left.Equals(right);
    }

    public bool Equals(SyntaxTrivia other)
    {
        return this.SyntaxToken == other.SyntaxToken
               && this.Node == other.Node
               && this.Position == other.Position
               && this.Index == other.Index;
    }

    public override bool Equals(object? obj)
    {
        return obj is SyntaxTrivia other && this.Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.SyntaxToken, this.Node, this.Position, this.Index);
    }

    public override string ToString()
    {
        return this.Node?.ToString() ?? string.Empty;
    }

    private string GetDebuggerDisplay()
    {
        string kindText = this.Node != null ? this.Node.Kind.ToString() : "None";
        return $"{this.GetType().Name} {kindText} {this.ToString()}";
    }
}

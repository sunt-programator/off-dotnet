// <copyright file="SyntaxToken.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax;

public readonly struct SyntaxToken
{
    internal SyntaxToken(SyntaxNode? parent, GreenNode? token, int position, int index)
    {
        this.Parent = parent;
        this.Node = token;
        this.Position = position;
        this.Index = index;
    }

    public SyntaxKind Kind => this.Node?.Kind ?? SyntaxKind.None;

    public string Text => this.ToString();

    public object? Value => this.Node?.Value;

    public SyntaxTrivia LeadingTrivia => this.Node != null
        ? new SyntaxTrivia(this, this.Node.LeadingTrivia, this.Position, 0)
        : default;

    public SyntaxNode? Parent { get; }

    internal GreenNode? Node { get; }

    internal int Index { get; }

    internal int Position { get; }

    public override string ToString()
    {
        return this.Node?.ToString() ?? string.Empty;
    }

    public string ToFullString()
    {
        return this.Node?.ToFullString() ?? string.Empty;
    }
}

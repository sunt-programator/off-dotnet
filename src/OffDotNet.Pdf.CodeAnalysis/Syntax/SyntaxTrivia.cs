// <copyright file="SyntaxTrivia.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax;

public readonly struct SyntaxTrivia
{
    internal SyntaxTrivia(in SyntaxToken token, GreenNode? triviaNode, int position, int index)
    {
        this.Token = token;
        this.UnderlyingNode = triviaNode;
        this.Position = position;
        this.Index = index;
    }

    public SyntaxKind Kind => this.UnderlyingNode?.Kind ?? SyntaxKind.None;

    public SyntaxToken Token { get; }

    internal GreenNode? UnderlyingNode { get; }

    internal int Position { get; }

    internal int Index { get; }

    public IEnumerable<char> ToFullString()
    {
        return this.UnderlyingNode?.ToFullString() ?? string.Empty;
    }

    public override string ToString()
    {
        return this.UnderlyingNode?.ToString() ?? string.Empty;
    }
}

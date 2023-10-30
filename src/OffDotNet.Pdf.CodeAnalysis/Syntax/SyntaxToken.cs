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

    /// <summary>
    /// Gets the string representation of the token without leading and trailing trivia.
    /// </summary>
    public string Text => this.ToString();

    /// <summary>
    /// Gets the string representation of the token including its leading and trailing trivia.
    /// </summary>
    public string FullText => this.Node?.ToFullString() ?? string.Empty;

    /// <summary>
    /// Gets the value of the token.
    /// </summary>
    /// <example>If the token is a PDF numeric literal, this property will return the numeric value.</example>
    public object? Value => this.Node?.Value;

    public SyntaxTrivia LeadingTrivia => this.Node != null
        ? new SyntaxTrivia(this, this.Node.LeadingTrivia, this.Position, 0)
        : default;

    public SyntaxNode? Parent { get; }

    internal GreenNode? Node { get; }

    internal int Index { get; }

    internal int Position { get; }

    /// <summary>
    /// Returns the string representation of the token without leading and trailing trivia.
    /// </summary>
    /// <returns>The string representation of the token without leading and trailing trivia.</returns>
    public override string ToString()
    {
        return this.Node?.ToString() ?? string.Empty;
    }
}

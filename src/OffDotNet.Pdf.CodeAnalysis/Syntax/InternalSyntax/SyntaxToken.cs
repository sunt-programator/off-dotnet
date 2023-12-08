// <copyright file="SyntaxToken.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.InternalUtilities;

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

/// <summary>Represents the terminal node in the PDF Syntax tree.</summary>
/// <remarks>Token is the smallest individual element of a PDF Syntax that has meaning and are not subject to further analysis.</remarks>
/// <example>PDF identifiers, keywords, literals.</example>
internal sealed class SyntaxToken : GreenNode
{
    internal SyntaxToken(SyntaxKind kind, string text, object? value, GreenNode? leading, GreenNode? trailing, int fullWidth)
        : base(kind)
    {
        this.Text = text;
        this.Value = value;
        this.FullWidth = fullWidth;
        this.LeadingTrivia = leading;
        this.TrailingTrivia = trailing;
    }

    /// <summary>Gets the lexeme of the token, not including its <see cref="GreenNode.LeadingTrivia"/> and <see cref="GreenNode.TrailingTrivia"/>.</summary>
    /// <remarks>A lexeme is an actual character sequence forming a specific instance of a token.</remarks>
    /// <example>obj, true, 45.678, (some string).</example>
    public string Text { get; }

    /// <summary>Gets the value of the token.</summary>
    /// <example>If the token is a <see cref="SyntaxKind.NumericLiteralToken"/>, the value is a <see cref="int"/> or a <see cref="double"/>.</example>
    public object? Value { get; }

    /// <summary>Gets the leading trivia of the token that is preceding the token.</summary>
    /// <remarks>Trivia or minutiae are parts of the source text that are largely insignificant for normal understanding of the PDF Syntax, such as whitespace, comments, etc.</remarks>
    public override GreenNode? LeadingTrivia { get; }

    /// <summary>Gets the trailing trivia of the token that is succeeding the token.</summary>
    /// <remarks>Trivia or minutiae are parts of the source text that are largely insignificant for normal understanding of the PDF Syntax, such as whitespace, comments, etc.</remarks>
    public override GreenNode? TrailingTrivia { get; }

    /// <summary>Gets a value indicating whether the node represents a token.</summary>
    public override bool IsToken => true;

    /// <summary>Gets the <see cref="GreenNode.TrailingTrivia"/> width.</summary>
    public override int LeadingTriviaWidth => this.LeadingTrivia?.FullWidth ?? 0;

    /// <summary>Gets the <see cref="GreenNode.TrailingTrivia"/> width.</summary>
    public override int TrailingTriviaWidth => this.TrailingTrivia?.FullWidth ?? 0;

    /// <summary>Returns the <see cref="Text"/> value.</summary>
    /// <returns>The <see cref="Text"/> value.</returns>
    public override string ToString()
    {
        return this.Text;
    }

    /// <inheritdoc cref="GreenNode.GetSlot"/>
    internal override GreenNode GetSlot(int index)
    {
        throw ExceptionUtilities.Unreachable();
    }
}

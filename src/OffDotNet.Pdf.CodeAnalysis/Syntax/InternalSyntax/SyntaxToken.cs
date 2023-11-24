// <copyright file="SyntaxToken.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.InternalUtilities;

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

internal sealed class SyntaxToken : GreenNode
{
    /// <summary>Initializes a new instance of the <see cref="SyntaxToken"/> class.</summary>
    /// <param name="kind">The <see cref="SyntaxKind"/> of the token.</param>
    /// <param name="text">The lexeme of the token, including its <see cref="GreenNode.LeadingTrivia"/> and <see cref="GreenNode.TrailingTrivia"/>.</param>
    /// <param name="fullWidth">The width of the token, including its <see cref="GreenNode.LeadingTrivia"/> and <see cref="GreenNode.TrailingTrivia"/>.</param>
    private SyntaxToken(SyntaxKind kind, string text, int fullWidth)
        : base(kind, fullWidth)
    {
        this.Text = text;
    }

    /// <summary>Gets the lexeme of the token, not including its <see cref="GreenNode.LeadingTrivia"/> and <see cref="GreenNode.TrailingTrivia"/>.</summary>
    /// <remarks>A lexeme is an actual character sequence forming a specific instance of a token.</remarks>
    /// <example>obj, true, 45.678, (some string).</example>
    public string Text { get; }

    /// <summary>Returns the <see cref="Text"/> value.</summary>
    /// <returns>The <see cref="Text"/> value.</returns>
    public override string ToString()
    {
        return this.Text;
    }

    internal static SyntaxToken CreateWithKind(SyntaxKind kind)
    {
        string text = SyntaxKindFacts.GetText(kind);
        int width = text.Length;
        return new SyntaxToken(kind, text, width);
    }

    internal static SyntaxToken CreateWithKindAndFullWidth(SyntaxKind kind, int fullWidth)
    {
        string text = SyntaxKindFacts.GetText(kind);
        return new SyntaxToken(kind, text, fullWidth);
    }

    /// <inheritdoc cref="GreenNode.GetSlot"/>
    internal override GreenNode GetSlot(int index)
    {
        throw ExceptionUtilities.Unreachable();
    }
}

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
    /// <param name="value">The value of the token.</param>
    /// <param name="fullWidth">The width of the token, including its <see cref="GreenNode.LeadingTrivia"/> and <see cref="GreenNode.TrailingTrivia"/>.</param>
    private SyntaxToken(SyntaxKind kind, string text, object? value, int fullWidth)
        : base(kind, fullWidth)
    {
        this.Text = text;
        this.Value = value;
    }

    /// <summary>Gets the lexeme of the token, not including its <see cref="GreenNode.LeadingTrivia"/> and <see cref="GreenNode.TrailingTrivia"/>.</summary>
    /// <remarks>A lexeme is an actual character sequence forming a specific instance of a token.</remarks>
    /// <example>obj, true, 45.678, (some string).</example>
    public string Text { get; }

    /// <summary>Gets the value of the token.</summary>
    /// <example>If the token is a <see cref="SyntaxKind.NumericLiteralToken"/>, the value is a <see cref="int"/> or a <see cref="double"/>.</example>
    public object? Value { get; }

    /// <summary>Returns the <see cref="Text"/> value.</summary>
    /// <returns>The <see cref="Text"/> value.</returns>
    public override string ToString()
    {
        return this.Text;
    }

    internal static SyntaxToken CreateWithKind(SyntaxKind kind)
    {
        string text = kind.GetText();
        int fullWidth = text.Length;
        object? value = kind.GetValue();
        return new SyntaxToken(kind, text, value, fullWidth);
    }

    internal static SyntaxToken CreateWithKindAndFullWidth(SyntaxKind kind, int fullWidth)
    {
        string text = kind.GetText();
        object? value = kind.GetValue();
        return new SyntaxToken(kind, text, value, fullWidth);
    }

    /// <inheritdoc cref="GreenNode.GetSlot"/>
    internal override GreenNode GetSlot(int index)
    {
        throw ExceptionUtilities.Unreachable();
    }
}

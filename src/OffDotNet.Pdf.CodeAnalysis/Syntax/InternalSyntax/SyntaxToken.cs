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
    private SyntaxToken(SyntaxKind kind, string text, object? value, GreenNode? leading, GreenNode? trailing, int fullWidth)
        : base(kind, fullWidth)
    {
        this.Text = text;
        this.Value = value;
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

    internal static SyntaxToken Create(SyntaxKind kind)
    {
        string text = kind.GetText();
        int fullWidth = text.Length;
        object? value = kind.GetValue();
        return new SyntaxToken(kind, text, value, null, null, fullWidth);
    }

    internal static SyntaxToken Create(SyntaxKind kind, int fullWidth)
    {
        string text = kind.GetText();
        object? value = kind.GetValue();
        return new SyntaxToken(kind, text, value, null, null, fullWidth);
    }

    internal static GreenNode Create(SyntaxKind kind, GreenNode? leading, GreenNode? trailing)
    {
        string text = kind.GetText();
        object? value = kind.GetValue();

        int leadingFullWidth = leading?.FullWidth ?? 0;
        int trailingFullWidth = trailing?.FullWidth ?? 0;
        int nodeFullWidth = text.Length + leadingFullWidth + trailingFullWidth;

        return new SyntaxToken(kind, text, value, leading, trailing, nodeFullWidth);
    }

    /// <inheritdoc cref="GreenNode.GetSlot"/>
    internal override GreenNode GetSlot(int index)
    {
        throw ExceptionUtilities.Unreachable();
    }
}

// <copyright file="RawSyntaxTrivia.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Pdf.Syntax;

using OffDotNet.CodeAnalysis.Syntax;
using Utils;

/// <summary>
/// Represents a raw syntax trivia node in the syntax tree.
/// </summary>
internal sealed class RawSyntaxTrivia : RawSyntaxNode
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RawSyntaxTrivia"/> class.
    /// </summary>
    /// <param name="kind">The kind of trivia.</param>
    /// <param name="text">The text of the trivia.</param>
    private RawSyntaxTrivia(SyntaxKind kind, ReadOnlySpan<byte> text)
        : base(kind, text.Length)
    {
        Debug.Assert(kind.IsTrivia(), "Invalid trivia kind");
        Text = Encoding.ASCII.GetString(text);
    }

    /// <summary>
    /// Gets the text of the trivia.
    /// </summary>
    public string Text { get; }

    /// <summary>
    /// Gets a value indicating whether this instance represents trivia.
    /// </summary>
    public override bool IsTrivia => true;

    /// <summary>
    /// Gets the width of the trailing trivia.
    /// </summary>
    public override int TrailingTriviaWidth => 0;

    /// <summary>
    /// Gets the width of the leading trivia.
    /// </summary>
    public override int LeadingTriviaWidth => 0;

    /// <summary>
    /// Gets the width of the trivia.
    /// </summary>
    public override int Width => FullWidth;

    /// <summary>
    /// Converts the trivia to its full string representation.
    /// </summary>
    /// <returns>The full string representation of the trivia.</returns>
    public override string ToFullString() => this.Text;

    /// <summary>
    /// Converts the trivia to its string representation.
    /// </summary>
    /// <returns>The string representation of the trivia.</returns>
    public override string ToString() => this.Text;

    /// <summary>Creates a new instance of the <see cref="RawSyntaxTrivia"/> class.</summary>
    /// <param name="kind">The kind of trivia.</param>
    /// <param name="text">The text of the trivia.</param>
    /// <returns>A new instance of the <see cref="RawSyntaxTrivia"/> class.</returns>
    internal static RawSyntaxTrivia Create(SyntaxKind kind, ReadOnlySpan<byte> text) => new(kind, text);

    /// <summary>
    /// Gets the slot at the specified index.
    /// </summary>
    /// <param name="index">The index of the slot.</param>
    /// <returns>An option containing the slot if it exists, otherwise none.</returns>
    internal override Option<AbstractNode> GetSlot(int index) => Option<AbstractNode>.None;

    /// <summary>Writes the trivia of the node to the specified writer.</summary>
    /// <param name="writer">The writer to which the trivia will be written.</param>
    protected override void WriteTriviaTo(TextWriter writer)
    {
        writer.Write(this.Text);
    }
}

// <copyright file="RawSyntaxToken.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Pdf.Syntax;

using OffDotNet.CodeAnalysis.Syntax;
using Utils;

/// <summary>Represents a raw syntax token in the syntax tree.</summary>
internal sealed class RawSyntaxToken : RawSyntaxNode
{
    /// <summary>Initializes a new instance of the <see cref="RawSyntaxToken"/> class with the specified kind.</summary>
    /// <param name="kind">The kind of the token.</param>
    private RawSyntaxToken(SyntaxKind kind)
        : base(kind)
    {
        Debug.Assert(!kind.IsTrivia(), "Invalid token kind");
        Text = kind.GetText();
        Value = GetValue();
        ValueText = Text;
        FullWidth = Text.Length;
    }

    /// <summary>Initializes a new instance of the <see cref="RawSyntaxToken"/> class with the specified kind and trivia.</summary>
    /// <param name="kind">The kind of the token.</param>
    /// <param name="leadingTrivia">The leading trivia of the token.</param>
    /// <param name="trailingTrivia">The trailing trivia of the token.</param>
    private RawSyntaxToken(SyntaxKind kind, Option<AbstractNode> leadingTrivia, Option<AbstractNode> trailingTrivia)
        : this(kind)
    {
        LeadingTrivia = leadingTrivia;
        TrailingTrivia = trailingTrivia;
        FullWidth = Text.Length
                    + LeadingTrivia.Match(static t => t.FullWidth, static () => 0)
                    + TrailingTrivia.Match(static t => t.FullWidth, static () => 0);
    }

    /// <summary>Initializes a new instance of the <see cref="RawSyntaxToken"/> class with the specified kind and text.</summary>
    /// <param name="kind">The kind of the token.</param>
    /// <param name="text">The text of the token.</param>
    private RawSyntaxToken(SyntaxKind kind, object text)
        : base(kind)
    {
        Text = text.ToString() ?? string.Empty;
        Value = GetValue();
        ValueText = Text;
        FullWidth = Text.Length;
    }

    /// <summary>Initializes a new instance of the <see cref="RawSyntaxToken"/> class with the specified kind, text, and trivia.</summary>
    /// <param name="kind">The kind of the token.</param>
    /// <param name="text">The text of the token.</param>
    /// <param name="leadingTrivia">The leading trivia of the token.</param>
    /// <param name="trailingTrivia">The trailing trivia of the token.</param>
    private RawSyntaxToken(SyntaxKind kind, object text, Option<AbstractNode> leadingTrivia, Option<AbstractNode> trailingTrivia)
        : this(kind, text)
    {
        LeadingTrivia = leadingTrivia;
        TrailingTrivia = trailingTrivia;
        FullWidth = Text.Length
                    + LeadingTrivia.Match(static t => t.FullWidth, static () => 0)
                    + TrailingTrivia.Match(static t => t.FullWidth, static () => 0);
    }

    /// <summary>Gets a value indicating whether this instance represents a token.</summary>
    public override bool IsToken => true;

    /// <summary>Gets the text of the token.</summary>
    public string Text { get; }

    /// <summary>Gets the value of the token.</summary>
    public override Option<object> Value { get; }

    /// <summary>Gets the text representation of the value of the token.</summary>
    public override string ValueText { get; }

    /// <summary>Gets the width of the token.</summary>
    public override int Width => Text.Length;

    /// <summary>Gets the leading trivia of the token.</summary>
    public override Option<AbstractNode> LeadingTrivia { get; }

    /// <summary>Gets the width of the leading trivia.</summary>
    public override int LeadingTriviaWidth => LeadingTrivia.Match(static t => t.FullWidth, static () => 0);

    /// <summary>Gets the trailing trivia of the token.</summary>
    public override Option<AbstractNode> TrailingTrivia { get; }

    /// <summary>Gets the width of the trailing trivia.</summary>
    public override int TrailingTriviaWidth => TrailingTrivia.Match(static t => t.FullWidth, static () => 0);

    /// <summary>Creates a new instance of the <see cref="RawSyntaxToken"/> class with the specified kind.</summary>
    /// <param name="kind">The kind of the token.</param>
    /// <returns>A new instance of the <see cref="RawSyntaxToken"/> class.</returns>
    internal static RawSyntaxToken Create(SyntaxKind kind) => new(kind);

    /// <summary>Creates a new instance of the <see cref="RawSyntaxToken"/> class with the specified kind and text.</summary>
    /// <typeparam name="T">The type of the text.</typeparam>
    /// <param name="kind">The kind of the token.</param>
    /// <param name="text">The text of the token.</param>
    /// <returns>A new instance of the <see cref="RawSyntaxToken"/> class.</returns>
    internal static RawSyntaxToken Create<T>(SyntaxKind kind, T text)
        where T : notnull
    {
        return new RawSyntaxToken(kind, text);
    }

    /// <summary>Creates a new instance of the <see cref="RawSyntaxToken"/> class with the specified kind, text, and trivia.</summary>
    /// <typeparam name="T">The type of the text.</typeparam>
    /// <param name="kind">The kind of the token.</param>
    /// <param name="text">The text of the token.</param>
    /// <param name="leadingTrivia">The leading trivia of the token.</param>
    /// <param name="trailingTrivia">The trailing trivia of the token.</param>
    /// <returns>A new instance of the <see cref="RawSyntaxToken"/> class.</returns>
    internal static RawSyntaxToken Create<T>(SyntaxKind kind, T text, Option<AbstractNode> leadingTrivia, Option<AbstractNode> trailingTrivia)
        where T : notnull
    {
        return new RawSyntaxToken(kind, text, leadingTrivia, trailingTrivia);
    }

    /// <summary>Creates a new instance of the <see cref="RawSyntaxToken"/> class with the specified kind and trivia.</summary>
    /// <param name="kind">The kind of the token.</param>
    /// <param name="leadingTrivia">The leading trivia of the token.</param>
    /// <param name="trailingTrivia">The trailing trivia of the token.</param>
    /// <returns>A new instance of the <see cref="RawSyntaxToken"/> class.</returns>
    internal static RawSyntaxToken Create(SyntaxKind kind, Option<AbstractNode> leadingTrivia, Option<AbstractNode> trailingTrivia) =>
        new(kind, leadingTrivia, trailingTrivia);

    /// <summary>Gets the slot at the specified index.</summary>
    /// <param name="index">The index of the slot.</param>
    /// <returns>An option containing the slot if it exists, otherwise none.</returns>
    internal override Option<AbstractNode> GetSlot(int index) => Option<AbstractNode>.None;

    /// <summary>Writes the token to the specified <see cref="TextWriter"/>.</summary>
    /// <param name="writer">The writer to write to.</param>
    /// <param name="leading">Whether to include leading trivia.</param>
    /// <param name="trailing">Whether to include trailing trivia.</param>
    protected override void WriteTokenTo(TextWriter writer, bool leading, bool trailing)
    {
        if (leading && LeadingTrivia.IsSome(out var leadingTrivia))
        {
            leadingTrivia.WriteTo(writer, leading: true, trailing: false);
        }

        writer.Write(this.Text);

        if (trailing && TrailingTrivia.IsSome(out var trailingTrivia))
        {
            trailingTrivia.WriteTo(writer, leading: false, trailing: true);
        }
    }

    /// <summary>Gets the value of the token based on its kind.</summary>
    /// <returns>An option containing the value of the token.</returns>
    private Option<object> GetValue() => this.Kind switch
    {
        SyntaxKind.TrueKeyword => Boxing.True,
        SyntaxKind.FalseKeyword => Boxing.False,
        SyntaxKind.NullKeyword => Option<object>.None,
        _ => Text,
    };
}

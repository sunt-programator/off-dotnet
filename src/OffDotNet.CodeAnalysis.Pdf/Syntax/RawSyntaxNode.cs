// <copyright file="RawSyntaxNode.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Pdf.Syntax;

using OffDotNet.CodeAnalysis.Syntax;

/// <summary>
/// Represents a raw syntax node in the syntax tree.
/// </summary>
internal abstract class RawSyntaxNode : AbstractNode
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RawSyntaxNode"/> class with the specified kind.
    /// </summary>
    /// <param name="kind">The kind of the syntax node.</param>
    protected RawSyntaxNode(SyntaxKind kind)
        : base((ushort)kind)
    {
        Kind = kind;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RawSyntaxNode"/> class with the specified kind and full width.
    /// </summary>
    /// <param name="kind">The kind of the syntax node.</param>
    /// <param name="fullWidth">The full width of the syntax node.</param>
    protected RawSyntaxNode(SyntaxKind kind, int fullWidth)
        : base((ushort)kind, fullWidth)
    {
        Kind = kind;
    }

    /// <summary>
    /// Gets the language of the syntax node.
    /// </summary>
    public static string Language => "PDF";

    /// <summary>
    /// Gets the kind of the syntax node.
    /// </summary>
    public SyntaxKind Kind { get; }

    /// <summary>
    /// Gets the text representation of the kind of the syntax node.
    /// </summary>
    public override string KindText => this.Kind.ToString();
}

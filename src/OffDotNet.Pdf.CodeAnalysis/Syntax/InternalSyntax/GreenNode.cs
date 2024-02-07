// <copyright file="GreenNode.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

using OffDotNet.Pdf.CodeAnalysis.Diagnostic;

/// <summary>Represents an syntax node in the PDF Syntax tree.</summary>
/// <remarks>The <see href="https://ericlippert.com/2012/06/08/red-green-trees/">green node</see> has no parent reference, it tracks only its relative width, but not its absolute position.</remarks>
internal abstract partial class GreenNode
{
    /// <summary>Initializes a new instance of the <see cref="GreenNode"/> class.</summary>
    /// <param name="kind">The <see cref="SyntaxKind"/> of the token.</param>
    /// <param name="diagnostics">The diagnostics associated with this node.</param>
    protected GreenNode(SyntaxKind kind, DiagnosticInfo[]? diagnostics = null)
    {
        this.Kind = kind;

        if (diagnostics?.Length > 0)
        {
            this.SetFlags(NodeFlags.ContainsDiagnostics);
            DiagnosticsTable.Add(this, diagnostics);
        }
    }

    /// <summary>Gets the <see cref="SyntaxKind"/> of the token.</summary>
    public SyntaxKind Kind { get; }

    /// <summary>Gets the leading trivia of the token that is preceding the token.</summary>
    /// <remarks>Trivia or minutiae are parts of the source text that are largely insignificant for normal understanding of the PDF Syntax, such as whitespace, comments, etc.</remarks>
    public virtual GreenNode? LeadingTrivia => null;

    /// <summary>Gets the trailing trivia of the token that is succeeding the token.</summary>
    /// <remarks>Trivia or minutiae are parts of the source text that are largely insignificant for normal understanding of the PDF Syntax, such as whitespace, comments, etc.</remarks>
    public virtual GreenNode? TrailingTrivia => null;

    /// <summary>Gets a value indicating whether the node represents a trivia.</summary>
    /// <remarks>Trivia or minutiae are parts of the source text that are largely insignificant for normal understanding of the PDF Syntax, such as whitespace, comments, etc.</remarks>
    public virtual bool IsTrivia => false;

    /// <summary>Gets a value indicating whether the node represents a token.</summary>
    public virtual bool IsToken => false;
}

// <copyright file="FileTrailerSyntax.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

using Diagnostic;

internal sealed class FileTrailerSyntax : GreenNode
{
    internal FileTrailerSyntax(
        SyntaxKind kind,
        SyntaxToken trailerKeyword,
        DictionaryExpressionSyntax trailerDictionary,
        SyntaxToken startXRefKeyword,
        LiteralExpressionSyntax byteOffset,
        DiagnosticInfo[]? diagnostics = null)
        : base(kind, diagnostics)
    {
        this.TrailerKeyword = trailerKeyword;
        this.TrailerDictionary = trailerDictionary;
        this.StartXRefKeyword = startXRefKeyword;
        this.ByteOffset = byteOffset;
        this.SlotCount = 4;
        this.FullWidth = this.TrailerKeyword.FullWidth + this.TrailerDictionary.FullWidth + this.StartXRefKeyword.FullWidth + this.ByteOffset.FullWidth;
    }

    public SyntaxToken TrailerKeyword { get; }

    public DictionaryExpressionSyntax TrailerDictionary { get; }

    public SyntaxToken StartXRefKeyword { get; }

    public LiteralExpressionSyntax ByteOffset { get; }

    /// <inheritdoc/>
    internal override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => this.TrailerKeyword,
            1 => this.TrailerDictionary,
            2 => this.StartXRefKeyword,
            3 => this.ByteOffset,
            _ => null,
        };
    }

    /// <inheritdoc cref="GreenNode.SetDiagnostics"/>
    internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new FileTrailerSyntax(this.Kind, this.TrailerKeyword, this.TrailerDictionary, this.StartXRefKeyword, this.ByteOffset, diagnostics);
    }
}

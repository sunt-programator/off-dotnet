// <copyright file="SyntaxToken.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.LexicalAnalysis;

namespace OffDotNet.Pdf.CodeAnalysis.Syntax;

public readonly struct SyntaxToken
{
    internal SyntaxToken(SyntaxKind kind, string text, object? value = null)
    {
        this.Kind = kind;
        this.Text = text;
        this.Value = value;
    }

    public SyntaxKind Kind { get; }

    public string Text { get; }

    public object? Value { get; }

    public IReadOnlyList<SyntaxTrivia> LeadingTrivia { get; } = new List<SyntaxTrivia>();

    public IReadOnlyList<SyntaxTrivia> TrailingTrivia { get; } = new List<SyntaxTrivia>();
}

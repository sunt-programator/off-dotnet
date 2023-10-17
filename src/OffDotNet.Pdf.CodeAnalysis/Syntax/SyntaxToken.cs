// <copyright file="SyntaxToken.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax;

public readonly struct SyntaxToken
{
    public SyntaxToken()
    {
        this.Kind = SyntaxKind.None;
        this.Text = string.Empty;
        this.Value = null;
    }

    internal SyntaxToken(SyntaxKind kind, string text, object? value)
    {
        this.Kind = kind;
        this.Text = text;
        this.Value = value;
    }

    public SyntaxKind Kind { get; }

    public string Text { get; }

    public object? Value { get; }
}

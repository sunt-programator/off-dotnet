// <copyright file="SyntaxToken.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax;

public readonly struct SyntaxToken
{
    internal SyntaxToken(AbstractSyntaxToken token)
    {
        this.Node = token;
    }

    public SyntaxKind Kind => this.Node?.Kind ?? SyntaxKind.None;

    public string Text => this.ToString();

    public object? Value => this.Node?.Value;

    public string ValueText => this.Node?.ValueText ?? string.Empty;

    internal AbstractSyntaxToken? Node { get; }

    public override string ToString()
    {
        return this.Node?.ToString() ?? string.Empty;
    }
}

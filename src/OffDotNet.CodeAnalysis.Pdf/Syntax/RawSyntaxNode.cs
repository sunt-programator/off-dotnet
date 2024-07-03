// <copyright file="RawSyntaxNode.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Pdf.Syntax;

using OffDotNet.CodeAnalysis.Syntax;

internal abstract class RawSyntaxNode : AbstractNode
{
    protected RawSyntaxNode(SyntaxKind kind)
        : base((ushort)kind)
    {
        Kind = kind;
    }

    protected RawSyntaxNode(SyntaxKind kind, int fullWidth)
        : base((ushort)kind, fullWidth)
    {
        Kind = kind;
    }

    public SyntaxKind Kind { get; }

    public override string KindText => this.Kind.ToString();

    public string Language => "PDF";
}

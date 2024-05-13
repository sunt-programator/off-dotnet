// <copyright file="SyntaxNode.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax;

using InternalSyntax;

public class SyntaxNode : AbstractSyntaxNode
{
    internal SyntaxNode(GreenNode underlyingNode, AbstractSyntaxNode? parent, int position)
    {
        UnderlyingNode = underlyingNode;
    }

    /// <inheritdoc/>
    public override AbstractSyntaxTree? SyntaxTree { get; }

    /// <inheritdoc/>
    internal override GreenNode UnderlyingNode { get; }
}

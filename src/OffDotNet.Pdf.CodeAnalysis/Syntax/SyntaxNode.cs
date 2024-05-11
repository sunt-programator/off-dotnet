﻿// <copyright file="SyntaxNode.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax;

using InternalSyntax;

public class SyntaxNode
{
    internal SyntaxNode(GreenNode underlyingNode, SyntaxNode? parent, int position)
    {
        UnderlyingNode = underlyingNode;
    }

    public SyntaxTree? SyntaxTree { get; }

    internal GreenNode UnderlyingNode { get; }
}

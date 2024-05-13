// <copyright file="AbstractSyntaxNode.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax;

using InternalSyntax;

public abstract class AbstractSyntaxNode
{
    public virtual AbstractSyntaxTree? SyntaxTree => null;

    internal abstract GreenNode UnderlyingNode { get; }
}

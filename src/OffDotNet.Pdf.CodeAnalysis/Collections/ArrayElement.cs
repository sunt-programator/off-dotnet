// <copyright file="ArrayElement.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Diagnostics;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

namespace OffDotNet.Pdf.CodeAnalysis.Collections;

[DebuggerDisplay("{Value,nq}")]
internal struct ArrayElement<TNode>
    where TNode : GreenNode?
{
    internal TNode Value;

    public static implicit operator TNode(ArrayElement<TNode> arrayElement)
    {
        return arrayElement.Value;
    }
}
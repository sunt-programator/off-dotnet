// <copyright file="ArrayElement.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Collections;

using System.Diagnostics;

[DebuggerDisplay("{_value,nq}")]
internal struct ArrayElement<TNode>
{
    internal TNode _value;

    public static implicit operator TNode(ArrayElement<TNode> arrayElement)
    {
        return arrayElement._value;
    }
}

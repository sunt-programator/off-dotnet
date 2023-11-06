// <copyright file="ArrayElement.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Diagnostics;

namespace OffDotNet.Pdf.CodeAnalysis.Old.Collections;

[DebuggerDisplay("{Value,nq}")]
internal struct ArrayElement<T>
{
    internal T Value;

    public static implicit operator T(ArrayElement<T> element)
    {
        return element.Value;
    }
}

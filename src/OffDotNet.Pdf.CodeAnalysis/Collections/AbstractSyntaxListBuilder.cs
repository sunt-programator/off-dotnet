// <copyright file="AbstractSyntaxListBuilder.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Collections;

using System.Diagnostics;

[DebuggerDisplay("Count = {Count}")]
internal abstract class AbstractSyntaxListBuilder<T>
{
    private const int DefaultCapacity = 4;
    private ArrayElement<T?>[] _nodes;
    
    
}

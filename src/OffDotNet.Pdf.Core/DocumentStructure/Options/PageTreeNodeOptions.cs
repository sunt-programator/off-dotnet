// <copyright file="PageTreeNodeOptions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.Core.Primitives;

namespace OffDotNet.Pdf.Core.DocumentStructure;

public sealed class PageTreeNodeOptions
{
    public IPdfIndirectIdentifier<IPageTreeNode>? Parent { get; set; }

    public IPdfArray<IPdfIndirectIdentifier<IPageObject>> Kids { get; set; } = default!;
}

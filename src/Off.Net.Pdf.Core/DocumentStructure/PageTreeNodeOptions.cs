// <copyright file="PageTreeNodeOptions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using Off.Net.Pdf.Core.Primitives;

namespace Off.Net.Pdf.Core.DocumentStructure;

public sealed class PageTreeNodeOptions
{
    public PdfIndirectIdentifier<PageTreeNode>? Parent { get; set; }

    public PdfArray<PdfIndirectIdentifier<PageObject>> Kids { get; set; } = default!;
}

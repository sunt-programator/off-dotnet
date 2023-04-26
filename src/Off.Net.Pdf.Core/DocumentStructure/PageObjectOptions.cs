// <copyright file="PageObjectOptions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using Off.Net.Pdf.Core.CommonDataStructures;
using Off.Net.Pdf.Core.ContentStreamAndResources;
using Off.Net.Pdf.Core.Extensions;
using Off.Net.Pdf.Core.Primitives;

namespace Off.Net.Pdf.Core.DocumentStructure;

public sealed class PageObjectOptions
{
    public PdfIndirectIdentifier<PageTreeNode> Parent { get; set; } = default!;

    public ResourceDictionary Resources { get; set; } = default!;

    public Rectangle MediaBox { get; set; } = default!;

    public AnyOf<PdfIndirectIdentifier<PdfStream>, PdfArray<PdfIndirectIdentifier<PdfStream>>>? Contents { get; set; }
}

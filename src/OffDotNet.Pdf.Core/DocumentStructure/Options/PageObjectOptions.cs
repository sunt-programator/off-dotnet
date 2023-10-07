// <copyright file="PageObjectOptions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.Core.CommonDataStructures;
using OffDotNet.Pdf.Core.ContentStreamAndResources;
using OffDotNet.Pdf.Core.Extensions;
using OffDotNet.Pdf.Core.Primitives;

namespace OffDotNet.Pdf.Core.DocumentStructure;

public sealed class PageObjectOptions
{
    public IPdfIndirectIdentifier<IPageTreeNode> Parent { get; set; } = default!;

    public ResourceDictionary Resources { get; set; } = default!;

    public Rectangle MediaBox { get; set; } = default!;

    public AnyOf<IPdfIndirectIdentifier<IPdfStream>, IPdfArray<IPdfIndirectIdentifier<IPdfStream>>>? Contents { get; set; }
}

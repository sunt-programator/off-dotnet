// <copyright file="PageObjectOptions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.DocumentStructure;

using OffDotNet.Pdf.Core.CommonDataStructures;
using OffDotNet.Pdf.Core.ContentStreamAndResources;
using OffDotNet.Pdf.Core.Extensions;
using OffDotNet.Pdf.Core.Primitives;

public sealed class PageObjectOptions
{
    public IPdfIndirectIdentifier<IPageTreeNode> Parent { get; set; } = default!;

    public IResourceDictionary Resources { get; set; } = default!;

    public IRectangle MediaBox { get; set; } = default!;

    public AnyOf<IPdfIndirectIdentifier<IPdfStream>, IPdfArray<IPdfIndirectIdentifier<IPdfStream>>>? Contents { get; set; }
}

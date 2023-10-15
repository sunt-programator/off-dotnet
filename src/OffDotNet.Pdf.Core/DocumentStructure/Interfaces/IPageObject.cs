// <copyright file="IPageObject.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.Core.Common;
using OffDotNet.Pdf.Core.CommonDataStructures;
using OffDotNet.Pdf.Core.ContentStreamAndResources;
using OffDotNet.Pdf.Core.Extensions;
using OffDotNet.Pdf.Core.Primitives;

namespace OffDotNet.Pdf.Core.DocumentStructure;

public interface IPageObject : IPdfDictionary<IPdfObject>
{
    IPdfIndirectIdentifier<IPageTreeNode> Parent { get; }

    IResourceDictionary Resources { get; }

    IRectangle MediaBox { get; }

    AnyOf<IPdfIndirectIdentifier<IPdfStream>, IPdfArray<IPdfIndirectIdentifier<IPdfStream>>>? Contents { get; }
}

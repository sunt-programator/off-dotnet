// <copyright file="IPageObject.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.DocumentStructure;

using Common;
using CommonDataStructures;
using ContentStreamAndResources;
using Extensions;
using Primitives;

public interface IPageObject : IPdfDictionary<IPdfObject>
{
    IPdfIndirectIdentifier<IPageTreeNode> Parent { get; }

    IResourceDictionary Resources { get; }

    IRectangle MediaBox { get; }

    AnyOf<IPdfIndirectIdentifier<IPdfStream>, IPdfArray<IPdfIndirectIdentifier<IPdfStream>>>? Contents { get; }
}

// <copyright file="IDocumentCatalog.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.Core.Common;
using OffDotNet.Pdf.Core.Primitives;

namespace OffDotNet.Pdf.Core.DocumentStructure;

public interface IDocumentCatalog : IPdfDictionary<IPdfObject>
{
    IPdfIndirectIdentifier<IPageTreeNode> Pages { get; }
}

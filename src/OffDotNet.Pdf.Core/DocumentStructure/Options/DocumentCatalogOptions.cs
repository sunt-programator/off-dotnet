// <copyright file="DocumentCatalogOptions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.DocumentStructure;

using Primitives;

public sealed class DocumentCatalogOptions
{
    public IPdfIndirectIdentifier<IPageTreeNode> Pages { get; set; } = default!;
}

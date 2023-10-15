// <copyright file="IPageTreeNode.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.Core.Common;
using OffDotNet.Pdf.Core.Primitives;

namespace OffDotNet.Pdf.Core.DocumentStructure;

public interface IPageTreeNode : IPdfDictionary<IPdfObject>
{
    IPdfIndirectIdentifier<IPageTreeNode>? Parent { get; }

    IPdfArray<IPdfIndirectIdentifier<IPageObject>> Kids { get; }
}

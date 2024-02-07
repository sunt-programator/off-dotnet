// <copyright file="IPageTreeNode.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.DocumentStructure;

using OffDotNet.Pdf.Core.Common;
using OffDotNet.Pdf.Core.Primitives;

public interface IPageTreeNode : IPdfDictionary<IPdfObject>
{
    IPdfIndirectIdentifier<IPageTreeNode>? Parent { get; }

    IPdfArray<IPdfIndirectIdentifier<IPageObject>> Kids { get; }
}

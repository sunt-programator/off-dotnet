// <copyright file="PageTreeNode.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.ObjectModel;
using OffDotNet.Pdf.Core.Common;
using OffDotNet.Pdf.Core.Extensions;
using OffDotNet.Pdf.Core.Primitives;

namespace OffDotNet.Pdf.Core.DocumentStructure;

public sealed class PageTreeNode : PdfDictionary<IPdfObject>, IPageTreeNode
{
    private static readonly PdfName TypeName = "Type";
    private static readonly PdfName PagesName = "Pages";
    private static readonly PdfName ParentName = "Parent";
    private static readonly PdfName KidsName = "Kids";
    private static readonly PdfName CountName = "Count";

    public PageTreeNode(Action<PageTreeNodeOptions> optionsFunc)
        : this(GetPageTreeNodeOptions(optionsFunc))
    {
    }

    public PageTreeNode(PageTreeNodeOptions options)
        : base(GenerateDictionary(options))
    {
        this.Parent = options.Parent;
        this.Kids = options.Kids;
    }

    public IPdfIndirectIdentifier<IPageTreeNode>? Parent { get; }

    public IPdfArray<IPdfIndirectIdentifier<PageObject>> Kids { get; }

    private static PageTreeNodeOptions GetPageTreeNodeOptions(Action<PageTreeNodeOptions> optionsFunc)
    {
        PageTreeNodeOptions options = new();
        optionsFunc.Invoke(options);
        return options;
    }

    private static IReadOnlyDictionary<PdfName, IPdfObject> GenerateDictionary(PageTreeNodeOptions options)
    {
        options.NotNull(x => x.Kids);

        IDictionary<PdfName, IPdfObject> documentCatalogDictionary = new Dictionary<PdfName, IPdfObject>(4)
            .WithKeyValue(TypeName, PagesName)
            .WithKeyValue(ParentName, options.Parent)
            .WithKeyValue(KidsName, options.Kids)
            .WithKeyValue(CountName, (PdfInteger)options.Kids.Value.Count);

        return new ReadOnlyDictionary<PdfName, IPdfObject>(documentCatalogDictionary);
    }
}

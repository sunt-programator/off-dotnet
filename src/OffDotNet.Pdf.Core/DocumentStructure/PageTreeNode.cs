// <copyright file="PageTreeNode.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.ObjectModel;
using OffDotNet.Pdf.Core.Extensions;
using OffDotNet.Pdf.Core.Interfaces;
using OffDotNet.Pdf.Core.Primitives;

namespace OffDotNet.Pdf.Core.DocumentStructure;

public sealed class PageTreeNode : PdfDictionary<IPdfObject>
{
    private static readonly PdfName TypeName = "Type";
    private static readonly PdfName TypeValue = "Pages";
    private static readonly PdfName Parent = "Parent";
    private static readonly PdfName Kids = "Kids";
    private static readonly PdfName Count = "Count";

    public PageTreeNode(Action<PageTreeNodeOptions> optionsFunc)
        : this(GetPageTreeNodeOptions(optionsFunc))
    {
    }

    public PageTreeNode(PageTreeNodeOptions options)
        : base(GenerateDictionary(options))
    {
    }

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
            .WithKeyValue(TypeName, TypeValue)
            .WithKeyValue(Parent, options.Parent)
            .WithKeyValue(Kids, options.Kids)
            .WithKeyValue(Count, (PdfInteger)options.Kids.Value.Count);

        return new ReadOnlyDictionary<PdfName, IPdfObject>(documentCatalogDictionary);
    }
}

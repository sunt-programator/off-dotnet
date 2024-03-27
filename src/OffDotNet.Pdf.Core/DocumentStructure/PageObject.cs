// <copyright file="PageObject.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.DocumentStructure;

using System.Collections.ObjectModel;
using Common;
using CommonDataStructures;
using ContentStreamAndResources;
using Extensions;
using Primitives;

public sealed class PageObject : PdfDictionary<IPdfObject>, IPageObject
{
    private static readonly PdfName s_typeName = "Type";
    private static readonly PdfName s_pageName = "Page";
    private static readonly PdfName s_parentName = "Parent";
    private static readonly PdfName s_resourcesName = "Resources";
    private static readonly PdfName s_mediaBoxName = "MediaBox";
    private static readonly PdfName s_contentsName = "Contents";

    public PageObject(Action<PageObjectOptions> optionsFunc)
        : this(GetPageObjectOptions(optionsFunc))
    {
    }

    public PageObject(PageObjectOptions options)
        : base(GenerateDictionary(options))
    {
        this.Parent = options.Parent;
        this.Resources = options.Resources;
        this.MediaBox = options.MediaBox;
        this.Contents = options.Contents;
    }

    /// <inheritdoc/>
    public IPdfIndirectIdentifier<IPageTreeNode> Parent { get; }

    /// <inheritdoc/>
    public IResourceDictionary Resources { get; }

    /// <inheritdoc/>
    public IRectangle MediaBox { get; }

    /// <inheritdoc/>
    public AnyOf<IPdfIndirectIdentifier<IPdfStream>, IPdfArray<IPdfIndirectIdentifier<IPdfStream>>>? Contents { get; }

    private static PageObjectOptions GetPageObjectOptions(Action<PageObjectOptions> optionsFunc)
    {
        PageObjectOptions options = new();
        optionsFunc.Invoke(options);
        return options;
    }

    private static IReadOnlyDictionary<PdfName, IPdfObject> GenerateDictionary(PageObjectOptions options)
    {
        options.NotNull(x => x.Parent);
        options.NotNull(x => x.Resources);
        options.NotNull(x => x.MediaBox);

        var documentCatalogDictionary = new Dictionary<PdfName, IPdfObject>(5)
            .WithKeyValue(s_typeName, s_pageName)
            .WithKeyValue(s_parentName, options.Parent)
            .WithKeyValue(s_resourcesName, options.Resources)
            .WithKeyValue(s_mediaBoxName, options.MediaBox)
            .WithKeyValue(s_contentsName, options.Contents?.PdfObject);

        return new ReadOnlyDictionary<PdfName, IPdfObject>(documentCatalogDictionary);
    }
}

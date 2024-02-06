// <copyright file="PageObject.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.DocumentStructure;

using System.Collections.ObjectModel;
using OffDotNet.Pdf.Core.Common;
using OffDotNet.Pdf.Core.CommonDataStructures;
using OffDotNet.Pdf.Core.ContentStreamAndResources;
using OffDotNet.Pdf.Core.Extensions;
using OffDotNet.Pdf.Core.Primitives;

public sealed class PageObject : PdfDictionary<IPdfObject>, IPageObject
{
    private static readonly PdfName TypeName = "Type";
    private static readonly PdfName PageName = "Page";
    private static readonly PdfName ParentName = "Parent";
    private static readonly PdfName ResourcesName = "Resources";
    private static readonly PdfName MediaBoxName = "MediaBox";
    private static readonly PdfName ContentsName = "Contents";

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
            .WithKeyValue(TypeName, PageName)
            .WithKeyValue(ParentName, options.Parent)
            .WithKeyValue(ResourcesName, options.Resources)
            .WithKeyValue(MediaBoxName, options.MediaBox)
            .WithKeyValue(ContentsName, options.Contents?.PdfObject);

        return new ReadOnlyDictionary<PdfName, IPdfObject>(documentCatalogDictionary);
    }
}

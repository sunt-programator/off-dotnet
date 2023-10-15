// <copyright file="DocumentCatalog.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.ObjectModel;
using OffDotNet.Pdf.Core.Common;
using OffDotNet.Pdf.Core.Extensions;
using OffDotNet.Pdf.Core.Primitives;

namespace OffDotNet.Pdf.Core.DocumentStructure;

public sealed class DocumentCatalog : PdfDictionary<IPdfObject>, IDocumentCatalog
{
    private static readonly PdfName TypeName = "Type";
    private static readonly PdfName CatalogName = "Catalog";
    private static readonly PdfName PagesName = "Pages";

    public DocumentCatalog(Action<DocumentCatalogOptions> optionsFunc)
        : this(GetDocumentCatalogOptions(optionsFunc))
    {
    }

    public DocumentCatalog(DocumentCatalogOptions options)
        : base(GenerateDictionary(options))
    {
        options.NotNull(x => x.Pages);
        this.Pages = options.Pages;
    }

    public IPdfIndirectIdentifier<IPageTreeNode> Pages { get; }

    private static DocumentCatalogOptions GetDocumentCatalogOptions(Action<DocumentCatalogOptions> optionsFunc)
    {
        DocumentCatalogOptions options = new();
        optionsFunc.Invoke(options);
        return options;
    }

    private static IReadOnlyDictionary<PdfName, IPdfObject> GenerateDictionary(DocumentCatalogOptions options)
    {
        IDictionary<PdfName, IPdfObject> documentCatalogDictionary = new Dictionary<PdfName, IPdfObject>(2)
            .WithKeyValue(TypeName, CatalogName)
            .WithKeyValue(PagesName, options.Pages);

        return new ReadOnlyDictionary<PdfName, IPdfObject>(documentCatalogDictionary);
    }
}

// <copyright file="DocumentCatalog.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.ObjectModel;
using Off.Net.Pdf.Core.Extensions;
using Off.Net.Pdf.Core.Interfaces;
using Off.Net.Pdf.Core.Primitives;

namespace Off.Net.Pdf.Core.DocumentStructure;

public sealed class DocumentCatalog : PdfDictionary<IPdfObject>
{
    private static readonly PdfName Type = "Type";
    private static readonly PdfName Catalog = "Catalog";
    private static readonly PdfName Pages = "Pages";

    public DocumentCatalog(Action<DocumentCatalogOptions> optionsFunc)
        : this(GetDocumentCatalogOptions(optionsFunc))
    {
    }

    public DocumentCatalog(DocumentCatalogOptions options)
        : base(GenerateDictionary(options))
    {
        options.NotNull(x => x.Pages);
    }

    private static DocumentCatalogOptions GetDocumentCatalogOptions(Action<DocumentCatalogOptions> optionsFunc)
    {
        DocumentCatalogOptions options = new();
        optionsFunc.Invoke(options);
        return options;
    }

    private static IReadOnlyDictionary<PdfName, IPdfObject> GenerateDictionary(DocumentCatalogOptions options)
    {
        IDictionary<PdfName, IPdfObject> documentCatalogDictionary = new Dictionary<PdfName, IPdfObject>(2)
            .WithKeyValue(Type, Catalog)
            .WithKeyValue(Pages, options.Pages);

        return new ReadOnlyDictionary<PdfName, IPdfObject>(documentCatalogDictionary);
    }
}

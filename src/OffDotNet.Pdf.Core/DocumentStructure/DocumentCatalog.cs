﻿// <copyright file="DocumentCatalog.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.DocumentStructure;

using System.Collections.ObjectModel;
using Common;
using Extensions;
using Primitives;

public sealed class DocumentCatalog : PdfDictionary<IPdfObject>, IDocumentCatalog
{
    private static readonly PdfName s_typeName = "Type";
    private static readonly PdfName s_catalogName = "Catalog";
    private static readonly PdfName s_pagesName = "Pages";

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

    /// <inheritdoc/>
    public IPdfIndirectIdentifier<IPageTreeNode> Pages { get; }

    private static DocumentCatalogOptions GetDocumentCatalogOptions(Action<DocumentCatalogOptions> optionsFunc)
    {
        DocumentCatalogOptions options = new();
        optionsFunc.Invoke(options);
        return options;
    }

    private static IReadOnlyDictionary<PdfName, IPdfObject> GenerateDictionary(DocumentCatalogOptions options)
    {
        var documentCatalogDictionary = new Dictionary<PdfName, IPdfObject>(2)
            .WithKeyValue(s_typeName, s_catalogName)
            .WithKeyValue(s_pagesName, options.Pages);

        return new ReadOnlyDictionary<PdfName, IPdfObject>(documentCatalogDictionary);
    }
}

// <copyright file="PageObject.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.ObjectModel;
using Off.Net.Pdf.Core.Extensions;
using Off.Net.Pdf.Core.Interfaces;
using Off.Net.Pdf.Core.Primitives;

namespace Off.Net.Pdf.Core.DocumentStructure;

public sealed class PageObject : PdfDictionary<IPdfObject>
{
    private static readonly PdfName TypeName = "Type";
    private static readonly PdfName TypeValue = "Page";
    private static readonly PdfName Parent = "Parent";
    private static readonly PdfName Resources = "Resources";
    private static readonly PdfName MediaBox = "MediaBox";
    private static readonly PdfName Contents = "Contents";

    public PageObject(Action<PageObjectOptions> optionsFunc)
        : this(GetPageObjectOptions(optionsFunc))
    {
    }

    public PageObject(PageObjectOptions options)
        : base(GenerateDictionary(options))
    {
    }

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

        IDictionary<PdfName, IPdfObject> documentCatalogDictionary = new Dictionary<PdfName, IPdfObject>(5)
            .WithKeyValue(TypeName, TypeValue)
            .WithKeyValue(Parent, options.Parent)
            .WithKeyValue(Resources, options.Resources)
            .WithKeyValue(MediaBox, options.MediaBox)
            .WithKeyValue(Contents, options.Contents?.PdfObject);

        return new ReadOnlyDictionary<PdfName, IPdfObject>(documentCatalogDictionary);
    }
}

// <copyright file="ResourceDictionary.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.ObjectModel;
using OffDotNet.Pdf.Core.Interfaces;
using OffDotNet.Pdf.Core.Primitives;

namespace OffDotNet.Pdf.Core.ContentStreamAndResources;

public sealed class ResourceDictionary : PdfDictionary<IPdfObject>
{
    private static readonly PdfName Font = "Font";
    private static readonly PdfName ProcSet = "ProcSet";

    public ResourceDictionary(Action<ResourceDictionaryOptions> optionsFunc)
        : this(GetResourceDictionaryOptions(optionsFunc))
    {
    }

    public ResourceDictionary(ResourceDictionaryOptions options)
        : base(GenerateDictionary(options))
    {
    }

    private static ResourceDictionaryOptions GetResourceDictionaryOptions(Action<ResourceDictionaryOptions> optionsFunc)
    {
        ResourceDictionaryOptions options = new();
        optionsFunc.Invoke(options);
        return options;
    }

    private static IReadOnlyDictionary<PdfName, IPdfObject> GenerateDictionary(ResourceDictionaryOptions options)
    {
        IDictionary<PdfName, IPdfObject> documentCatalogDictionary = new Dictionary<PdfName, IPdfObject>(2)
            .WithKeyValue(Font, options.Font)
            .WithKeyValue(ProcSet, options.ProcSet);

        return new ReadOnlyDictionary<PdfName, IPdfObject>(documentCatalogDictionary);
    }
}

// <copyright file="ResourceDictionary.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.ObjectModel;
using OffDotNet.Pdf.Core.Common;
using OffDotNet.Pdf.Core.Primitives;
using OffDotNet.Pdf.Core.Text.Fonts;

namespace OffDotNet.Pdf.Core.ContentStreamAndResources;

public sealed class ResourceDictionary : PdfDictionary<IPdfObject>, IResourceDictionary
{
    private static readonly PdfName FontName = "Font";
    private static readonly PdfName ProcSetName = "ProcSet";

    public ResourceDictionary(Action<ResourceDictionaryOptions> optionsFunc)
        : this(GetResourceDictionaryOptions(optionsFunc))
    {
    }

    public ResourceDictionary(ResourceDictionaryOptions options)
        : base(GenerateDictionary(options))
    {
        this.Font = options.Font;
        this.ProcSet = options.ProcSet;
    }

    public IPdfDictionary<IPdfIndirectIdentifier<Type1Font>>? Font { get; }

    public IPdfArray<PdfName>? ProcSet { get; }

    private static ResourceDictionaryOptions GetResourceDictionaryOptions(Action<ResourceDictionaryOptions> optionsFunc)
    {
        ResourceDictionaryOptions options = new();
        optionsFunc.Invoke(options);
        return options;
    }

    private static IReadOnlyDictionary<PdfName, IPdfObject> GenerateDictionary(ResourceDictionaryOptions options)
    {
        IDictionary<PdfName, IPdfObject> documentCatalogDictionary = new Dictionary<PdfName, IPdfObject>(2)
            .WithKeyValue(FontName, options.Font)
            .WithKeyValue(ProcSetName, options.ProcSet);

        return new ReadOnlyDictionary<PdfName, IPdfObject>(documentCatalogDictionary);
    }
}

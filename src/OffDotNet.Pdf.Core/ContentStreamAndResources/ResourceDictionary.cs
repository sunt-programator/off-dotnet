// <copyright file="ResourceDictionary.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.ContentStreamAndResources;

using System.Collections.ObjectModel;
using OffDotNet.Pdf.Core.Common;
using OffDotNet.Pdf.Core.Primitives;
using OffDotNet.Pdf.Core.Text.Fonts;

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

    /// <inheritdoc/>
    public IPdfDictionary<IPdfIndirectIdentifier<IType1Font>>? Font { get; }

    /// <inheritdoc/>
    public IPdfArray<PdfName>? ProcSet { get; }

    private static ResourceDictionaryOptions GetResourceDictionaryOptions(Action<ResourceDictionaryOptions> optionsFunc)
    {
        ResourceDictionaryOptions options = new();
        optionsFunc.Invoke(options);
        return options;
    }

    private static IReadOnlyDictionary<PdfName, IPdfObject> GenerateDictionary(ResourceDictionaryOptions options)
    {
        var documentCatalogDictionary = new Dictionary<PdfName, IPdfObject>(2)
            .WithKeyValue(FontName, options.Font)
            .WithKeyValue(ProcSetName, options.ProcSet);

        return new ReadOnlyDictionary<PdfName, IPdfObject>(documentCatalogDictionary);
    }
}

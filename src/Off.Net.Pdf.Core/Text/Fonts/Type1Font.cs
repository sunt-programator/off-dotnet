// <copyright file="Type1Font.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.ObjectModel;
using Off.Net.Pdf.Core.Extensions;
using Off.Net.Pdf.Core.Interfaces;
using Off.Net.Pdf.Core.Primitives;

namespace Off.Net.Pdf.Core.Text.Fonts;

public sealed class Type1Font : PdfDictionary<IPdfObject>
{
    private static readonly PdfName Type = "Type";
    private static readonly PdfName TypeValue = "Font";
    private static readonly PdfName Subtype = "Subtype";
    private static readonly PdfName SubtypeValue = "Type1";
    private static readonly PdfName FontName = "Name";
    private static readonly PdfName BaseFont = "BaseFont";

    public Type1Font(Action<Type1FontOptions> optionsFunc)
        : this(GetType1FontOptions(optionsFunc))
    {
    }

    public Type1Font(Type1FontOptions options)
        : base(GenerateDictionary(options))
    {
        options.NotNull(x => x.BaseFont);
    }

    private static Type1FontOptions GetType1FontOptions(Action<Type1FontOptions> optionsFunc)
    {
        Type1FontOptions options = new();
        optionsFunc.Invoke(options);
        return options;
    }

    private static IReadOnlyDictionary<PdfName, IPdfObject> GenerateDictionary(Type1FontOptions options)
    {
        IDictionary<PdfName, IPdfObject> documentCatalogDictionary = new Dictionary<PdfName, IPdfObject>(4)
            .WithKeyValue(Type, TypeValue)
            .WithKeyValue(Subtype, SubtypeValue)
            .WithKeyValue(FontName, options.FontName)
            .WithKeyValue(BaseFont, options.BaseFont);

        return new ReadOnlyDictionary<PdfName, IPdfObject>(documentCatalogDictionary);
    }
}

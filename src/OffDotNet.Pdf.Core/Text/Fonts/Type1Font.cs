﻿// <copyright file="Type1Font.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Text.Fonts;

using System.Collections.ObjectModel;
using Common;
using Extensions;
using Primitives;

public sealed class Type1Font : PdfDictionary<IPdfObject>, IType1Font
{
    private static readonly PdfName s_typeName = "Type";
    private static readonly PdfName s_fontValueName = "Font";
    private static readonly PdfName s_subtypeName = "Subtype";
    private static readonly PdfName s_type1Name = "Type1";
    private static readonly PdfName s_fontPdfName = "Name";
    private static readonly PdfName s_baseFontName = "BaseFont";

    public Type1Font(Action<Type1FontOptions> optionsFunc)
        : this(GetType1FontOptions(optionsFunc))
    {
    }

    public Type1Font(Type1FontOptions options)
        : base(GenerateDictionary(options))
    {
        options.NotNull(x => x.BaseFont);
        this.BaseFont = options.BaseFont;
        this.FontName = options.FontName;
    }

    /// <inheritdoc/>
    public PdfName? FontName { get; }

    /// <inheritdoc/>
    public PdfName BaseFont { get; }

    private static Type1FontOptions GetType1FontOptions(Action<Type1FontOptions> optionsFunc)
    {
        Type1FontOptions options = new();
        optionsFunc.Invoke(options);
        return options;
    }

    private static IReadOnlyDictionary<PdfName, IPdfObject> GenerateDictionary(Type1FontOptions options)
    {
        var documentCatalogDictionary = new Dictionary<PdfName, IPdfObject>(4)
            .WithKeyValue(s_typeName, s_fontValueName)
            .WithKeyValue(s_subtypeName, s_type1Name)
            .WithKeyValue(s_fontPdfName, options.FontName)
            .WithKeyValue(s_baseFontName, options.BaseFont);

        return new ReadOnlyDictionary<PdfName, IPdfObject>(documentCatalogDictionary);
    }
}

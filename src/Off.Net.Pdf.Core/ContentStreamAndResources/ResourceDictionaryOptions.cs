// <copyright file="ResourceDictionaryOptions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using Off.Net.Pdf.Core.Primitives;
using Off.Net.Pdf.Core.Text.Fonts;

namespace Off.Net.Pdf.Core.ContentStreamAndResources;

public sealed class ResourceDictionaryOptions
{
    public static readonly PdfName ProcSetPdf = "PDF";
    public static readonly PdfName ProcSetText = "Text";
    public static readonly PdfName ProcSetImageB = "ImageB";
    public static readonly PdfName ProcSetImageC = "ImageC";
    public static readonly PdfName ProcSetImageI = "ImageI";
    private static readonly PdfArray<PdfName> DefaultProcedureSets = new[] { ProcSetPdf, ProcSetText, ProcSetImageB, ProcSetImageC, ProcSetImageI }.ToPdfArray();

    public PdfDictionary<PdfIndirectIdentifier<Type1Font>>? Font { get; set; }

    public PdfArray<PdfName>? ProcSet { get; set; } = DefaultProcedureSets;
}
﻿// <copyright file="ResourceDictionaryOptions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.ContentStreamAndResources;

using Primitives;
using Text.Fonts;

public sealed class ResourceDictionaryOptions
{
    public static readonly PdfName ProcSetPdf = "PDF";
    public static readonly PdfName ProcSetText = "Text";
    public static readonly PdfName ProcSetImageB = "ImageB";
    public static readonly PdfName ProcSetImageC = "ImageC";
    public static readonly PdfName ProcSetImageI = "ImageI";
    private static readonly IPdfArray<PdfName> s_defaultProcedureSets = new[] { ProcSetPdf, ProcSetText, ProcSetImageB, ProcSetImageC, ProcSetImageI }.ToPdfArray();

    public IPdfDictionary<IPdfIndirectIdentifier<IType1Font>>? Font { get; set; }

    public IPdfArray<PdfName>? ProcSet { get; set; } = s_defaultProcedureSets;
}

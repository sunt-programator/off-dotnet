// <copyright file="PdfDocumentOptions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Immutable;
using Off.Net.Pdf.Core.DocumentStructure;
using Off.Net.Pdf.Core.Primitives;

namespace Off.Net.Pdf.Document;

public sealed class PdfDocumentOptions : IPdfDocumentOptions
{
    public IImmutableList<PdfIndirectIdentifier<PageObject>> Pages { get; set; } = ImmutableList<PdfIndirectIdentifier<PageObject>>.Empty;
}

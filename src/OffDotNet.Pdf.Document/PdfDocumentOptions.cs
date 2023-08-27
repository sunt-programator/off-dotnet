// <copyright file="PdfDocumentOptions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Immutable;
using OffDotNet.Pdf.Core.DocumentStructure;
using OffDotNet.Pdf.Core.Primitives;

namespace OffDotNet.Pdf.Document;

public sealed class PdfDocumentOptions : IPdfDocumentOptions
{
    public IImmutableList<PdfIndirectIdentifier<PageObject>> Pages { get; set; } = ImmutableList<PdfIndirectIdentifier<PageObject>>.Empty;
}

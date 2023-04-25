// <copyright file="FileTrailerOptions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using Off.Net.Pdf.Core.DocumentStructure;
using Off.Net.Pdf.Core.Interfaces;
using Off.Net.Pdf.Core.Primitives;

namespace Off.Net.Pdf.Core.FileStructure;

public sealed class FileTrailerOptions
{
    public PdfInteger Size { get; set; }

    public PdfInteger? Prev { get; set; }

    public PdfIndirectIdentifier<DocumentCatalog> Root { get; set; } = default!;

    public PdfDictionary<IPdfObject>? Encrypt { get; set; }

    public PdfIndirectIdentifier<PdfDictionary<IPdfObject>>? Info { get; set; }

    public PdfArray<PdfString>? Id { get; set; }
}

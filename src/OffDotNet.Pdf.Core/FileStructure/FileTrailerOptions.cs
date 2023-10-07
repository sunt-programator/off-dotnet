// <copyright file="FileTrailerOptions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.Core.Common;
using OffDotNet.Pdf.Core.DocumentStructure;
using OffDotNet.Pdf.Core.Primitives;

namespace OffDotNet.Pdf.Core.FileStructure;

public sealed class FileTrailerOptions
{
    public PdfInteger Size { get; set; }

    public PdfInteger? Prev { get; set; }

    public IPdfIndirectIdentifier<IDocumentCatalog> Root { get; set; } = default!;

    public IPdfDictionary<IPdfObject>? Encrypt { get; set; }

    public IPdfIndirectIdentifier<IPdfDictionary<IPdfObject>>? Info { get; set; }

    public IPdfArray<PdfString>? Id { get; set; }
}

// <copyright file="PdfIndirectIdentifierExtensions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.Core.Common;

namespace OffDotNet.Pdf.Core.Primitives;

public static class PdfIndirectIdentifierExtensions
{
    public static PdfIndirectIdentifier<T> ToPdfIndirectIdentifier<T>(this PdfIndirect<T> pdfIndirect)
        where T : IPdfObject
    {
        return new PdfIndirectIdentifier<T>(pdfIndirect);
    }
}

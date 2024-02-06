// <copyright file="PdfIndirectIdentifierExtensions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Primitives;

using OffDotNet.Pdf.Core.Common;

public static class PdfIndirectIdentifierExtensions
{
    public static IPdfIndirectIdentifier<T> ToPdfIndirectIdentifier<T>(this IPdfIndirect<T> pdfIndirect)
        where T : IPdfObject
    {
        return new PdfIndirectIdentifier<T>(pdfIndirect);
    }
}

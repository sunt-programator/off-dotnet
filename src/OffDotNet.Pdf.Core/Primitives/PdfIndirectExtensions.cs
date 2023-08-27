// <copyright file="PdfIndirectExtensions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.Core.Interfaces;

namespace OffDotNet.Pdf.Core.Primitives;

public static class PdfIndirectExtensions
{
    public static PdfIndirect<T> ToPdfIndirect<T>(this T pdfObject, int objectNumber, int generationNumber = 0)
        where T : IPdfObject
    {
        return new PdfIndirect<T>(pdfObject, objectNumber, generationNumber);
    }
}

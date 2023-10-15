// <copyright file="PdfIndirectExtensions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.Core.Common;

namespace OffDotNet.Pdf.Core.Primitives;

public static class PdfIndirectExtensions
{
    public static IPdfIndirect<T> ToPdfIndirect<T>(this T pdfObject, int objectNumber, int generationNumber = 0)
        where T : IPdfObject
    {
        return new PdfIndirect<T>(pdfObject, objectNumber, generationNumber);
    }
}

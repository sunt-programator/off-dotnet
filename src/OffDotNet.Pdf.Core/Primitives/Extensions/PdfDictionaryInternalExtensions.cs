// <copyright file="PdfDictionaryInternalExtensions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.Core.Common;

namespace OffDotNet.Pdf.Core.Primitives;

internal static class PdfDictionaryInternalExtensions
{
    public static IDictionary<PdfName, IPdfObject> WithKeyValue(this IDictionary<PdfName, IPdfObject> dictionary, PdfName key, IPdfObject? pdfObject)
    {
        if (pdfObject != null)
        {
            dictionary.Add(key, pdfObject);
        }

        return dictionary;
    }
}

// <copyright file="PdfDictionaryInternalExtensions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Primitives;

using OffDotNet.Pdf.Core.Common;

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

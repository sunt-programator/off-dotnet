// <copyright file="PdfArrayExtensions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using Off.Net.Pdf.Core.Interfaces;

namespace Off.Net.Pdf.Core.Primitives;

public static class PdfArrayExtensions
{
    public static PdfArray<TValue> ToPdfArray<TValue>(this IEnumerable<TValue> items)
        where TValue : IPdfObject
    {
        return PdfArray<TValue>.CreateRange(items.ToList());
    }

    public static PdfArray<TValue> ToPdfArray<TValue>(this TValue item)
        where TValue : IPdfObject
    {
        return PdfArray<TValue>.Create(item);
    }
}

// <copyright file="PdfArrayExtensions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Primitives;

using OffDotNet.Pdf.Core.Common;

public static class PdfArrayExtensions
{
    public static IPdfArray<TValue> ToPdfArray<TValue>(this IEnumerable<TValue> items)
        where TValue : IPdfObject
    {
        return new PdfArray<TValue>(items.ToList());
    }

    public static IPdfArray<TValue> ToPdfArray<TValue>(this TValue item)
        where TValue : IPdfObject
    {
        return new PdfArray<TValue>(new List<TValue>(1) { item });
    }
}

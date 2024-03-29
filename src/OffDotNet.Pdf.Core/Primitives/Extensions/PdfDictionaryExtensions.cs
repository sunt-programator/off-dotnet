﻿// <copyright file="PdfDictionaryExtensions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Primitives;

using System.Collections.ObjectModel;
using Common;

public static class PdfDictionaryExtensions
{
    public static IPdfDictionary<TValue> ToPdfDictionary<TValue>(this IDictionary<PdfName, TValue> items)
        where TValue : IPdfObject
    {
        return new PdfDictionary<TValue>(new ReadOnlyDictionary<PdfName, TValue>(items));
    }

    public static IPdfDictionary<TValue> ToPdfDictionary<TValue>(this KeyValuePair<PdfName, TValue> item)
        where TValue : IPdfObject
    {
        IDictionary<PdfName, TValue> dictionary = new Dictionary<PdfName, TValue>(1) { { item.Key, item.Value } };
        return new PdfDictionary<TValue>(new ReadOnlyDictionary<PdfName, TValue>(dictionary));
    }
}

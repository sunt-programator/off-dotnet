// <copyright file="IPdfDictionary.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.Core.Common;

namespace OffDotNet.Pdf.Core.Primitives;

public interface IPdfDictionary<TValue> : IPdfObject
    where TValue : IPdfObject
{
    IReadOnlyDictionary<PdfName, TValue> Value { get; }
}

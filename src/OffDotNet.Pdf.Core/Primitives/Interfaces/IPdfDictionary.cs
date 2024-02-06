// <copyright file="IPdfDictionary.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Primitives;

using OffDotNet.Pdf.Core.Common;

public interface IPdfDictionary<TValue> : IPdfObject
    where TValue : IPdfObject
{
    IReadOnlyDictionary<PdfName, TValue> Value { get; }
}

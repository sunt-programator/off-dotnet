// <copyright file="IPdfArray.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.Core.Common;

namespace OffDotNet.Pdf.Core.Primitives;

public interface IPdfArray<out TValue> : IPdfObject
    where TValue : IPdfObject
{
    IReadOnlyCollection<TValue> Value { get; }
}

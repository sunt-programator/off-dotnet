// <copyright file="IPdfArray.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Primitives;

using OffDotNet.Pdf.Core.Common;

public interface IPdfArray<out TValue> : IPdfObject
    where TValue : IPdfObject
{
    IReadOnlyCollection<TValue> Value { get; }
}

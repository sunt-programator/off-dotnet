// <copyright file="IPdfStream.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Primitives;

using Common;

public interface IPdfStream : IPdfObject
{
    ReadOnlyMemory<char> Value { get; }

    IPdfDictionary<IPdfObject> StreamExtent { get; }
}

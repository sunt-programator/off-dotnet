// <copyright file="IPdfIndirectIdentifier.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Primitives;

using OffDotNet.Pdf.Core.Common;

public interface IPdfIndirectIdentifier<T> : IPdfObject
    where T : IPdfObject
{
    int GenerationNumber { get; }

    int ObjectNumber { get; }

    IPdfIndirect<T> PdfIndirect { get; }
}

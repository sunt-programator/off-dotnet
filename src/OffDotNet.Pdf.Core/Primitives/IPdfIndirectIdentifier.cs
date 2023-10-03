// <copyright file="IPdfIndirectIdentifier.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.Core.Common;

namespace OffDotNet.Pdf.Core.Primitives;

public interface IPdfIndirectIdentifier<T> : IPdfObject
    where T : IPdfObject
{
    int GenerationNumber { get; }

    int ObjectNumber { get; }

    PdfIndirect<T> PdfIndirect { get; }
}

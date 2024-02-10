// <copyright file="PdfDocumentOptions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Document;

using System.Collections.Immutable;
using Core.DocumentStructure;
using Core.Primitives;

public sealed class PdfDocumentOptions : IPdfDocumentOptions
{
    public IImmutableList<IPdfIndirectIdentifier<IPageObject>> Pages { get; set; } = ImmutableList<IPdfIndirectIdentifier<IPageObject>>.Empty;
}

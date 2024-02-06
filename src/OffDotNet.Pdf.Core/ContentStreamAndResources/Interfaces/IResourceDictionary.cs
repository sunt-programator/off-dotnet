// <copyright file="IResourceDictionary.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.ContentStreamAndResources;

using OffDotNet.Pdf.Core.Common;
using OffDotNet.Pdf.Core.Primitives;
using OffDotNet.Pdf.Core.Text.Fonts;

public interface IResourceDictionary : IPdfDictionary<IPdfObject>
{
    IPdfDictionary<IPdfIndirectIdentifier<IType1Font>>? Font { get; }

    IPdfArray<PdfName>? ProcSet { get; }
}

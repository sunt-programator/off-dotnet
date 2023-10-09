// <copyright file="IType1Font.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.Core.Common;
using OffDotNet.Pdf.Core.Primitives;

namespace OffDotNet.Pdf.Core.Text.Fonts;

public interface IType1Font : IPdfDictionary<IPdfObject>
{
    PdfName? FontName { get; }

    PdfName BaseFont { get; }
}

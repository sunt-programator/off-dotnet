// <copyright file="IType1Font.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Text.Fonts;

using Common;
using Primitives;

public interface IType1Font : IPdfDictionary<IPdfObject>
{
    PdfName? FontName { get; }

    PdfName BaseFont { get; }
}

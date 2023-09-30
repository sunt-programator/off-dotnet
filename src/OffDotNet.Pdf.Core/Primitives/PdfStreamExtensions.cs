// <copyright file="PdfStreamExtensions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.Core.Common;

namespace OffDotNet.Pdf.Core.Primitives;

public static class PdfStreamExtensions
{
    public static PdfStream ToPdfStream(this IPdfObject pdfObject, Action<PdfStreamExtentOptions>? options = null)
    {
        return new PdfStream(pdfObject.Content.AsMemory(), options);
    }
}

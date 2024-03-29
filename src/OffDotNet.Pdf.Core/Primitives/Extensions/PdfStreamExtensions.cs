﻿// <copyright file="PdfStreamExtensions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Primitives;

using Common;

public static class PdfStreamExtensions
{
    public static IPdfStream ToPdfStream(this IPdfObject pdfObject, Action<PdfStreamExtentOptions>? options = null)
    {
        return new PdfStream(pdfObject.Content.AsMemory(), options);
    }
}

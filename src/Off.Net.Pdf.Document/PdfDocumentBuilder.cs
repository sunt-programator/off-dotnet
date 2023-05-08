// <copyright file="PdfDocumentBuilder.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Off.Net.Pdf.Document;

public sealed class PdfDocumentBuilder : IPdfDocumentBuilder
{
    public PdfDocument BuildPdfDocument(Stream stream)
    {
        return new PdfDocument(stream, _ => { });
    }
}

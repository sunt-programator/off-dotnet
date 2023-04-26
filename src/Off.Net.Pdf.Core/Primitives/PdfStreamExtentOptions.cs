// <copyright file="PdfStreamExtentOptions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using Off.Net.Pdf.Core.Extensions;

namespace Off.Net.Pdf.Core.Primitives;

public sealed class PdfStreamExtentOptions
{
    public AnyOf<PdfName, PdfArray<PdfName>>? Filter { get; set; } // Name or Array

    public AnyOf<PdfDictionary<PdfName>, PdfArray<PdfName>>? DecodeParameters { get; set; } // Dictionary or Array

    public PdfString? FileSpecification { get; set; }

    public AnyOf<PdfName, PdfArray<PdfName>>? FileFilter { get; set; } // Name or Array

    public AnyOf<PdfDictionary<PdfName>, PdfArray<PdfName>>? FileDecodeParameters { get; set; } // Dictionary or Array
}

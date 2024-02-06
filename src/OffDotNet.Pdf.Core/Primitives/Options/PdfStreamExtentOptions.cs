// <copyright file="PdfStreamExtentOptions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Primitives;

using OffDotNet.Pdf.Core.Extensions;

public sealed class PdfStreamExtentOptions
{
    public AnyOf<PdfName, IPdfArray<PdfName>>? Filter { get; set; } // Name or Array

    public AnyOf<IPdfDictionary<PdfName>, IPdfArray<PdfName>>? DecodeParameters { get; set; } // Dictionary or Array

    public PdfString? FileSpecification { get; set; }

    public AnyOf<PdfName, IPdfArray<PdfName>>? FileFilter { get; set; } // Name or Array

    public AnyOf<IPdfDictionary<PdfName>, IPdfArray<PdfName>>? FileDecodeParameters { get; set; } // Dictionary or Array
}

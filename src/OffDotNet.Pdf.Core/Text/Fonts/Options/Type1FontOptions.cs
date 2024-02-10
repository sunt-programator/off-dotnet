// <copyright file="Type1FontOptions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Text.Fonts;

using Primitives;

public sealed class Type1FontOptions
{
    public PdfName? FontName { get; set; }

    public PdfName BaseFont { get; set; } = default!;
}

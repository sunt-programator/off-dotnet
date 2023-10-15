// <copyright file="IRectangle.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.Core.Primitives;

namespace OffDotNet.Pdf.Core.CommonDataStructures;

public interface IRectangle : IPdfArray<PdfReal>
{
    float LowerLeftX { get; }

    float LowerLeftY { get; }

    float UpperRightX { get; }

    float UpperRightY { get; }
}

// <copyright file="IRectangle.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.CommonDataStructures;

using OffDotNet.Pdf.Core.Primitives;

public interface IRectangle : IPdfArray<PdfReal>
{
    float LowerLeftX { get; }

    float LowerLeftY { get; }

    float UpperRightX { get; }

    float UpperRightY { get; }
}

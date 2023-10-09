// <copyright file="Rectangle.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.Core.Extensions;
using OffDotNet.Pdf.Core.Primitives;
using OffDotNet.Pdf.Core.Properties;

namespace OffDotNet.Pdf.Core.CommonDataStructures;

public sealed class Rectangle : PdfArray<PdfReal>, IRectangle
{
    public Rectangle(float lowerLeftX, float lowerLeftY, float upperRightX, float upperRightY)
        : base(new List<PdfReal> { lowerLeftX, lowerLeftY, upperRightX, upperRightY })
    {
        this.LowerLeftX = lowerLeftX.CheckConstraints(x => x >= 0, Resource.Rectangle_PointMustBePositive);
        this.LowerLeftY = lowerLeftY.CheckConstraints(x => x >= 0, Resource.Rectangle_PointMustBePositive);
        this.UpperRightX = upperRightX.CheckConstraints(x => x >= 0, Resource.Rectangle_PointMustBePositive);
        this.UpperRightY = upperRightY.CheckConstraints(x => x >= 0, Resource.Rectangle_PointMustBePositive);
    }

    public float LowerLeftX { get; }

    public float LowerLeftY { get; }

    public float UpperRightX { get; }

    public float UpperRightY { get; }
}

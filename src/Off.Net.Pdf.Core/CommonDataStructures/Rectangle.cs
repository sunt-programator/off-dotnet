// <copyright file="Rectangle.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using Off.Net.Pdf.Core.Extensions;
using Off.Net.Pdf.Core.Primitives;

namespace Off.Net.Pdf.Core.CommonDataStructures;

public sealed class Rectangle : PdfArray<PdfInteger>
{
    public Rectangle(int lowerLeftX, int lowerLeftY, int upperRightX, int upperRightY)
        : base(new List<PdfInteger> { lowerLeftX, lowerLeftY, upperRightX, upperRightY })
    {
        this.LowerLeftX = lowerLeftX.CheckConstraints(x => x >= 0, Resource.Rectangle_PointMustBePositive);
        this.LowerLeftY = lowerLeftY.CheckConstraints(x => x >= 0, Resource.Rectangle_PointMustBePositive);
        this.UpperRightX = upperRightX.CheckConstraints(x => x >= 0, Resource.Rectangle_PointMustBePositive);
        this.UpperRightY = upperRightY.CheckConstraints(x => x >= 0, Resource.Rectangle_PointMustBePositive);
    }

    public int LowerLeftX { get; }

    public int LowerLeftY { get; }

    public int UpperRightX { get; }

    public int UpperRightY { get; }
}

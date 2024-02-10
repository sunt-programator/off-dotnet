// <copyright file="Rectangle.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.CommonDataStructures;

using Extensions;
using Primitives;
using Properties;

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

    /// <inheritdoc/>
    public float LowerLeftX { get; }

    /// <inheritdoc/>
    public float LowerLeftY { get; }

    /// <inheritdoc/>
    public float UpperRightX { get; }

    /// <inheritdoc/>
    public float UpperRightY { get; }
}

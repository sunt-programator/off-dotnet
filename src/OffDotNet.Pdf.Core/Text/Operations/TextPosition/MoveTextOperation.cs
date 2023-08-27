// <copyright file="MoveTextOperation.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.Core.ContentStreamAndResources;
using OffDotNet.Pdf.Core.Primitives;

namespace OffDotNet.Pdf.Core.Text.Operations.TextPosition;

public sealed class MoveTextOperation : PdfOperation
{
    public const string OperatorName = "Td";
    private readonly Lazy<int> hashCode;

    public MoveTextOperation(float x, float y)
        : base(OperatorName)
    {
        this.X = x;
        this.Y = y;

        this.hashCode = new Lazy<int>(() => HashCode.Combine(nameof(MoveTextOperation), x, y, OperatorName));
    }

    public PdfReal X { get; }

    public PdfReal Y { get; }

    public override int GetHashCode()
    {
        return this.hashCode.Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is MoveTextOperation other && this.X == other.X && this.Y == other.Y;
    }

    protected override string GenerateContent()
    {
        return $"{this.X.Content} {this.Y.Content} {this.PdfOperator}\n";
    }
}

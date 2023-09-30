// <copyright file="MoveTextOperation.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.Core.ContentStreamAndResources;
using OffDotNet.Pdf.Core.Primitives;

namespace OffDotNet.Pdf.Core.Text.Operations.TextPosition;

public sealed class MoveTextOperation : PdfOperation
{
    private const string OperatorName = "Td";

    public MoveTextOperation(float x, float y)
        : base(OperatorName)
    {
        this.X = x;
        this.Y = y;
    }

    public PdfReal X { get; }

    public PdfReal Y { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.X;
        yield return this.Y;
        yield return this.PdfOperator;
    }

    protected override string GenerateContent()
    {
        return $"{this.X.Content} {this.Y.Content} {this.PdfOperator}\n";
    }
}

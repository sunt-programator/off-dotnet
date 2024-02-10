// <copyright file="MoveTextOperation.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Text.Operations.TextPosition;

using Common;
using Primitives;

public sealed class MoveTextOperation : PdfOperation, IMoveTextOperation
{
    private const string OperatorName = "Td";

    public MoveTextOperation(float x, float y)
        : base(OperatorName)
    {
        this.X = x;
        this.Y = y;
    }

    /// <inheritdoc/>
    public PdfReal X { get; }

    /// <inheritdoc/>
    public PdfReal Y { get; }

    /// <inheritdoc/>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.X;
        yield return this.Y;
        yield return this.PdfOperator;
    }

    /// <inheritdoc/>
    protected override string GenerateContent()
    {
        return $"{this.X.Content} {this.Y.Content} {this.PdfOperator}\n";
    }
}

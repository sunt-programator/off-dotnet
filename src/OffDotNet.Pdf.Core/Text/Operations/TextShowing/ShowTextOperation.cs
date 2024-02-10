// <copyright file="ShowTextOperation.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Text.Operations.TextShowing;

using Common;
using Extensions;
using Primitives;

public sealed class ShowTextOperation : PdfOperation, IShowTextOperation
{
    public const string OperatorName = "Tj";

    public ShowTextOperation(PdfString text)
        : base(OperatorName)
    {
        this.Text = text.NotNull(x => x);
    }

    /// <inheritdoc/>
    public PdfString Text { get; }

    /// <inheritdoc/>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.Text;
        yield return this.PdfOperator;
    }

    /// <inheritdoc/>
    protected override string GenerateContent()
    {
        return $"{this.Text.Content} {this.PdfOperator}\n";
    }
}

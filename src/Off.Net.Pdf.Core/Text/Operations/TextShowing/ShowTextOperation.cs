// <copyright file="ShowTextOperation.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using Off.Net.Pdf.Core.ContentStreamAndResources;
using Off.Net.Pdf.Core.Extensions;
using Off.Net.Pdf.Core.Primitives;

namespace Off.Net.Pdf.Core.Text.Operations.TextShowing;

public sealed class ShowTextOperation : PdfOperation
{
    public const string OperatorName = "Tj";
    private readonly Lazy<int> hashCode;

    public ShowTextOperation(PdfString text)
        : base(OperatorName)
    {
        this.Text = text.NotNull(x => x);

        this.hashCode = new Lazy<int>(() => HashCode.Combine(nameof(ShowTextOperation), text, OperatorName));
    }

    public PdfString Text { get; }

    public override bool Equals(object? obj)
    {
        return obj is ShowTextOperation other && this.Text == other.Text;
    }

    public override int GetHashCode()
    {
        return this.hashCode.Value;
    }

    protected override string GenerateContent()
    {
        return $"{this.Text.Content} {this.PdfOperator}\n";
    }
}

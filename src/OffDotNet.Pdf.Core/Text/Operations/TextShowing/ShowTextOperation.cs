// <copyright file="ShowTextOperation.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.Core.Common;
using OffDotNet.Pdf.Core.ContentStreamAndResources;
using OffDotNet.Pdf.Core.Extensions;
using OffDotNet.Pdf.Core.Primitives;

namespace OffDotNet.Pdf.Core.Text.Operations.TextShowing;

public sealed class ShowTextOperation : PdfOperation
{
    public const string OperatorName = "Tj";

    public ShowTextOperation(PdfString text)
        : base(OperatorName)
    {
        this.Text = text.NotNull(x => x);
    }

    public PdfString Text { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.Text;
        yield return this.PdfOperator;
    }

    protected override string GenerateContent()
    {
        return $"{this.Text.Content} {this.PdfOperator}\n";
    }
}

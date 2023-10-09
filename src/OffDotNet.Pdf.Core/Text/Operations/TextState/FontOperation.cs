// <copyright file="FontOperation.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.Core.Common;
using OffDotNet.Pdf.Core.Extensions;
using OffDotNet.Pdf.Core.Primitives;
using OffDotNet.Pdf.Core.Properties;

namespace OffDotNet.Pdf.Core.Text.Operations.TextState;

public sealed class FontOperation : PdfOperation, IFontOperation
{
    public const string OperatorName = "Tf";

    public FontOperation(PdfName fontName, PdfInteger fontSize)
        : base(OperatorName)
    {
        this.FontName = fontName;
        this.FontSize = fontSize.CheckConstraints(x => x >= 0, Resource.FontOperation_FontSizeMustBePositive);
    }

    public PdfName FontName { get; }

    public PdfInteger FontSize { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.FontName;
        yield return this.FontSize;
        yield return this.PdfOperator;
    }

    protected override string GenerateContent()
    {
        return $"{this.FontName.Content} {this.FontSize.Content} {this.PdfOperator}\n";
    }
}

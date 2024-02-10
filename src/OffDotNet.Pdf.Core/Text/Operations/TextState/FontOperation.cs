// <copyright file="FontOperation.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Text.Operations.TextState;

using Common;
using Extensions;
using Primitives;
using Properties;

public sealed class FontOperation : PdfOperation, IFontOperation
{
    public const string OperatorName = "Tf";

    public FontOperation(PdfName fontName, PdfInteger fontSize)
        : base(OperatorName)
    {
        this.FontName = fontName;
        this.FontSize = fontSize.CheckConstraints(x => x >= 0, Resource.FontOperation_FontSizeMustBePositive);
    }

    /// <inheritdoc/>
    public PdfName FontName { get; }

    /// <inheritdoc/>
    public PdfInteger FontSize { get; }

    /// <inheritdoc/>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.FontName;
        yield return this.FontSize;
        yield return this.PdfOperator;
    }

    /// <inheritdoc/>
    protected override string GenerateContent()
    {
        return $"{this.FontName.Content} {this.FontSize.Content} {this.PdfOperator}\n";
    }
}

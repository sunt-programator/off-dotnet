// <copyright file="FontOperation.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using Off.Net.Pdf.Core.ContentStreamAndResources;
using Off.Net.Pdf.Core.Extensions;
using Off.Net.Pdf.Core.Primitives;

namespace Off.Net.Pdf.Core.Text.Operations.TextState;

public sealed class FontOperation : PdfOperation
{
    public const string OperatorName = "Tf";
    private readonly Lazy<int> hashCode;

    public FontOperation(PdfName fontName, PdfInteger fontSize)
        : base(OperatorName)
    {
        this.FontName = fontName;
        this.FontSize = fontSize.CheckConstraints(x => x >= 0, Resource.FontOperation_FontSizeMustBePositive);

        this.hashCode = new Lazy<int>(() => HashCode.Combine(nameof(FontOperation), fontName, fontSize, OperatorName));
    }

    public PdfName FontName { get; }

    public PdfInteger FontSize { get; }

    public override int GetHashCode()
    {
        return this.hashCode.Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is FontOperation other && this.FontName == other.FontName && this.FontSize == other.FontSize;
    }

    protected override string GenerateContent()
    {
        return $"{this.FontName.Content} {this.FontSize.Content} {this.PdfOperator}\n";
    }
}

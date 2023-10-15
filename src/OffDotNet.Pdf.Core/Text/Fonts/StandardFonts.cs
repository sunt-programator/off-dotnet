// <copyright file="StandardFonts.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Text.Fonts;

public static class StandardFonts
{
    public static readonly IType1Font TimesRoman = new Type1Font(options =>
    {
        options.FontName = "Times-Roman";
        options.BaseFont = "Times-Roman";
    });

    public static readonly IType1Font TimesBold = new Type1Font(options =>
    {
        options.FontName = "Times-Bold";
        options.BaseFont = "Times-Bold";
    });

    public static readonly IType1Font TimesItalic = new Type1Font(options =>
    {
        options.FontName = "Times-Italic";
        options.BaseFont = "Times-Italic";
    });

    public static readonly IType1Font TimesBoldItalic = new Type1Font(options =>
    {
        options.FontName = "Times-BoldItalic";
        options.BaseFont = "Times-BoldItalic";
    });

    public static readonly IType1Font Helvetica = new Type1Font(options =>
    {
        options.FontName = "Helvetica";
        options.BaseFont = "Helvetica";
    });

    public static readonly IType1Font HelveticaBold = new Type1Font(options =>
    {
        options.FontName = "Helvetica-Bold";
        options.BaseFont = "Helvetica-Bold";
    });

    public static readonly IType1Font HelveticaOblique = new Type1Font(options =>
    {
        options.FontName = "Helvetica-Oblique";
        options.BaseFont = "Helvetica-Oblique";
    });

    public static readonly IType1Font HelveticaBoldOblique = new Type1Font(options =>
    {
        options.FontName = "Helvetica-BoldOblique";
        options.BaseFont = "Helvetica-BoldOblique";
    });

    public static readonly IType1Font Courier = new Type1Font(options =>
    {
        options.FontName = "Courier";
        options.BaseFont = "Courier";
    });

    public static readonly IType1Font CourierBold = new Type1Font(options =>
    {
        options.FontName = "Courier-Bold";
        options.BaseFont = "Courier-Bold";
    });

    public static readonly IType1Font CourierOblique = new Type1Font(options =>
    {
        options.FontName = "Courier-Oblique";
        options.BaseFont = "Courier-Oblique";
    });

    public static readonly IType1Font CourierBoldOblique = new Type1Font(options =>
    {
        options.FontName = "Courier-BoldOblique";
        options.BaseFont = "Courier-BoldOblique";
    });

    public static readonly IType1Font Symbol = new Type1Font(options =>
    {
        options.FontName = "Symbol";
        options.BaseFont = "Symbol";
    });

    public static readonly IType1Font ZapfDingbats = new Type1Font(options =>
    {
        options.FontName = "ZapfDingbats";
        options.BaseFont = "ZapfDingbats";
    });
}

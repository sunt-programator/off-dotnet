namespace Off.Net.Pdf.Core.Text.Fonts;

public static class StandardFonts
{
    public static readonly Type1Font TimesRoman = new(options =>
    {
        options.FontName = "Times-Roman";
        options.BaseFont = "Times-Roman";
    });

    public static readonly Type1Font TimesBold = new(options =>
    {
        options.FontName = "Times-Bold";
        options.BaseFont = "Times-Bold";
    });

    public static readonly Type1Font TimesItalic = new(options =>
    {
        options.FontName = "Times-Italic";
        options.BaseFont = "Times-Italic";
    });

    public static readonly Type1Font TimesBoldItalic = new(options =>
    {
        options.FontName = "Times-BoldItalic";
        options.BaseFont = "Times-BoldItalic";
    });

    public static readonly Type1Font Helvetica = new(options =>
    {
        options.FontName = "Helvetica";
        options.BaseFont = "Helvetica";
    });

    public static readonly Type1Font HelveticaBold = new(options =>
    {
        options.FontName = "Helvetica-Bold";
        options.BaseFont = "Helvetica-Bold";
    });

    public static readonly Type1Font HelveticaOblique = new(options =>
    {
        options.FontName = "Helvetica-Oblique";
        options.BaseFont = "Helvetica-Oblique";
    });

    public static readonly Type1Font HelveticaBoldOblique = new(options =>
    {
        options.FontName = "Helvetica-BoldOblique";
        options.BaseFont = "Helvetica-BoldOblique";
    });

    public static readonly Type1Font Courier = new(options =>
    {
        options.FontName = "Courier";
        options.BaseFont = "Courier";
    });

    public static readonly Type1Font CourierBold = new(options =>
    {
        options.FontName = "Courier-Bold";
        options.BaseFont = "Courier-Bold";
    });

    public static readonly Type1Font CourierOblique = new(options =>
    {
        options.FontName = "Courier-Oblique";
        options.BaseFont = "Courier-Oblique";
    });

    public static readonly Type1Font CourierBoldOblique = new(options =>
    {
        options.FontName = "Courier-BoldOblique";
        options.BaseFont = "Courier-BoldOblique";
    });

    public static readonly Type1Font Symbol = new(options =>
    {
        options.FontName = "Symbol";
        options.BaseFont = "Symbol";
    });

    public static readonly Type1Font ZapfDingbats = new(options =>
    {
        options.FontName = "ZapfDingbats";
        options.BaseFont = "ZapfDingbats";
    });
}

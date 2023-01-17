using Off.Net.Pdf.Core.Interfaces;

namespace Off.Net.Pdf.Core.Primitives;

public sealed class PdfStreamExtentOptions
{
    public EitherType<PdfName, PdfArray>? Filter { get; set; } // Name or Array
    public EitherType<PdfDictionary, PdfArray>? DecodeParameters { get; set; } // Dictionary or Array
    public PdfString? FileSpecification { get; set; }
    public EitherType<PdfName, PdfArray>? FileFilter { get; set; } // Name or Array
    public EitherType<PdfDictionary, PdfArray>? FileDecodeParameters { get; set; } // Dictionary or Array
}

internal static class PdfStreamInternalExtensions
{
    public static IDictionary<PdfName, IPdfObject> WithKeyValue(this IDictionary<PdfName, IPdfObject> dictionary, PdfName key, IPdfObject? pdfObject)
    {
        if (pdfObject != null)
        {
            dictionary.Add(key, pdfObject);
        }

        return dictionary;
    }
}

public static class PdfStreamExtensions
{
    public static PdfStream ToPdfStream(this IPdfObject pdfObject, Action<PdfStreamExtentOptions>? options = null)
    {
        return new PdfStream(pdfObject.Content.AsMemory(), options);
    }
}

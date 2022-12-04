using Off.Net.Pdf.Core.Interfaces;

namespace Off.Net.Pdf.Core.Primitives;

public static class PdfDictionaryExtensions
{
    public static PdfDictionary ToPdfDictionary(this IDictionary<PdfName, IPdfObject> items)
    {
        return PdfDictionary.CreateRange(items);
    }

    public static PdfDictionary ToPdfDictionary(this KeyValuePair<PdfName, IPdfObject> item)
    {
        return PdfDictionary.Create(item);
    }
}

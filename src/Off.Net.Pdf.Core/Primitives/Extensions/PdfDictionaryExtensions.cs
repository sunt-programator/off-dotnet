using Off.Net.Pdf.Core.Interfaces;

namespace Off.Net.Pdf.Core.Primitives;

public static class PdfDictionaryExtensions
{
    public static PdfDictionary<TValue> ToPdfDictionary<TValue>(this IDictionary<PdfName, TValue> items) where TValue: IPdfObject
    {
        return PdfDictionary<TValue>.CreateRange(items);
    }

    public static PdfDictionary<TValue> ToPdfDictionary<TValue>(this KeyValuePair<PdfName, TValue> item) where TValue: IPdfObject
    {
        return PdfDictionary<TValue>.Create(item);
    }
}

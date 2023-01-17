using Off.Net.Pdf.Core.Interfaces;

namespace Off.Net.Pdf.Core.Primitives;

public static class PdfArrayExtensions
{
    public static PdfArray ToPdfArray(this IEnumerable<IPdfObject> items)
    {
        return PdfArray.CreateRange(items.ToList());
    }

    public static PdfArray ToPdfArray(this IPdfObject item)
    {
        return PdfArray.Create(item);
    }
}

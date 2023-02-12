using Off.Net.Pdf.Core.Interfaces;

namespace Off.Net.Pdf.Core.Primitives;

public static class PdfIndirectExtensions
{
    public static PdfIndirect<T> ToPdfIndirect<T>(this T pdfObject, int objectNumber, int generationNumber = 0) where T : IPdfObject
    {
        return new PdfIndirect<T>(pdfObject, objectNumber, generationNumber);
    }

    public static PdfIndirectIdentifier<T> ToPdfIndirectIdentifier<T>(this PdfIndirect<T> pdfIndirect) where T : IPdfObject
    {
        return new PdfIndirectIdentifier<T>(pdfIndirect);
    }
}

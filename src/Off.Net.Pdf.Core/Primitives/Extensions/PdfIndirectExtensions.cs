using Off.Net.Pdf.Core.Interfaces;

namespace Off.Net.Pdf.Core.Primitives;

public static class PdfIndirectExtensions
{
    public static PdfIndirect ToPdfIndirect(this IPdfObject pdfObject, int objectNumber, int generationNumber = 0)
    {
        return new PdfIndirect(pdfObject, objectNumber, generationNumber);
    }
}

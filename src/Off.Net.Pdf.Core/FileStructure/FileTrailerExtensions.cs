using Off.Net.Pdf.Core.Interfaces;
using Off.Net.Pdf.Core.Primitives;

namespace Off.Net.Pdf.Core.FileStructure;

public sealed class FileTrailerOptions
{
    public PdfInteger Size { get; set; }

    public PdfInteger? Prev { get; set; }

    public PdfIndirectIdentifier<PdfDictionary<IPdfObject>> Root { get; set; } = new Dictionary<PdfName, IPdfObject>(0)
        .ToPdfDictionary()
        .ToPdfIndirect(0)
        .ToPdfIndirectIdentifier();

    public PdfDictionary<IPdfObject>? Encrypt { get; set; }

    public PdfIndirectIdentifier<PdfDictionary<IPdfObject>>? Info { get; set; }

    public PdfArray<PdfString>? Id { get; set; }
}

public static class FileTrailerExtensions
{
    public static FileTrailer ToFileTrailer(this FileTrailerOptions fileTrailerOptions, long byteOffset)
    {
        return new FileTrailer(byteOffset, fileTrailerOptions);
    }
}

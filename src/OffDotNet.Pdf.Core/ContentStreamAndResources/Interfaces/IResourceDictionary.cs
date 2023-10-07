using OffDotNet.Pdf.Core.Common;
using OffDotNet.Pdf.Core.Primitives;
using OffDotNet.Pdf.Core.Text.Fonts;

namespace OffDotNet.Pdf.Core.ContentStreamAndResources;

public interface IResourceDictionary : IPdfDictionary<IPdfObject>
{
    IPdfDictionary<IPdfIndirectIdentifier<Type1Font>>? Font { get; }

    IPdfArray<PdfName>? ProcSet { get; }
}

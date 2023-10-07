using OffDotNet.Pdf.Core.Common;
using OffDotNet.Pdf.Core.Primitives;

namespace OffDotNet.Pdf.Core.Text.Operations.TextPosition;

public interface IMoveTextOperation : IPdfOperation
{
    PdfReal X { get; }

    PdfReal Y { get; }
}

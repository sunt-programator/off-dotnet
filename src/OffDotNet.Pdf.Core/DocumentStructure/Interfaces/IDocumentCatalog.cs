using OffDotNet.Pdf.Core.Common;
using OffDotNet.Pdf.Core.Primitives;

namespace OffDotNet.Pdf.Core.DocumentStructure;

public interface IDocumentCatalog : IPdfObject
{
    IPdfIndirectIdentifier<IPageTreeNode> Pages { get; }
}

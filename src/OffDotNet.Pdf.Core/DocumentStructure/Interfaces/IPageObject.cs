using OffDotNet.Pdf.Core.Common;
using OffDotNet.Pdf.Core.CommonDataStructures;
using OffDotNet.Pdf.Core.ContentStreamAndResources;
using OffDotNet.Pdf.Core.Extensions;
using OffDotNet.Pdf.Core.Primitives;

namespace OffDotNet.Pdf.Core.DocumentStructure;

public interface IPageObject : IPdfObject
{
    IPdfIndirectIdentifier<IPageTreeNode> Parent { get; }

    ResourceDictionary Resources { get; }

    Rectangle MediaBox { get; }

    AnyOf<IPdfIndirectIdentifier<IPdfStream>, IPdfArray<IPdfIndirectIdentifier<IPdfStream>>>? Contents { get; }
}

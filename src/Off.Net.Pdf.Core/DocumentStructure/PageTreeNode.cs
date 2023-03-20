using System.Collections.ObjectModel;
using Off.Net.Pdf.Core.CommonDataStructures;
using Off.Net.Pdf.Core.ContentStreamAndResources;
using Off.Net.Pdf.Core.Extensions;
using Off.Net.Pdf.Core.Interfaces;
using Off.Net.Pdf.Core.Primitives;

namespace Off.Net.Pdf.Core.DocumentStructure;

public sealed class PageTreeNode : PdfDictionary<IPdfObject>
{
    #region Fields

    private static readonly PdfName TypeName = "Type";
    private static readonly PdfName TypeValue = "Pages";
    private static readonly PdfName Parent = "Parent";
    private static readonly PdfName Kids = "Kids";
    private static readonly PdfName Count = "Count";

    #endregion

    #region Constructors

    public PageTreeNode(Action<PageTreeNodeOptions> optionsFunc) : this(GetPageTreeNodeOptions(optionsFunc))
    {
    }

    public PageTreeNode(PageTreeNodeOptions options) : base(GenerateDictionary(options))
    {
    }

    #endregion

    #region Private Methods

    private static PageTreeNodeOptions GetPageTreeNodeOptions(Action<PageTreeNodeOptions> optionsFunc)
    {
        PageTreeNodeOptions options = new();
        optionsFunc.Invoke(options);
        return options;
    }

    private static IReadOnlyDictionary<PdfName, IPdfObject> GenerateDictionary(PageTreeNodeOptions options)
    {
        options.NotNull(x => x.Kids);

        IDictionary<PdfName, IPdfObject> documentCatalogDictionary = new Dictionary<PdfName, IPdfObject>(4)
            .WithKeyValue(TypeName, TypeValue)
            .WithKeyValue(Parent, options.Parent)
            .WithKeyValue(Kids, options.Kids)
            .WithKeyValue(Count, (PdfInteger)options.Kids.Value.Count);

        return new ReadOnlyDictionary<PdfName, IPdfObject>(documentCatalogDictionary);
    }

    #endregion
}

public sealed class PageTreeNodeOptions
{
    public PdfIndirectIdentifier<PageTreeNode>? Parent { get; set; }

    public PdfArray<PdfIndirectIdentifier<PageObject>> Kids { get; set; } = default!;
}

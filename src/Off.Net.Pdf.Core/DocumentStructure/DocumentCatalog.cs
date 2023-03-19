using System.Collections.ObjectModel;
using Off.Net.Pdf.Core.Extensions;
using Off.Net.Pdf.Core.Interfaces;
using Off.Net.Pdf.Core.Primitives;

namespace Off.Net.Pdf.Core.DocumentStructure;

public sealed class DocumentCatalog : PdfDictionary<IPdfObject>
{
    #region Fields

    private static readonly PdfName Type = "Type";
    private static readonly PdfName Catalog = "Catalog";
    private static readonly PdfName Pages = "Pages";

    #endregion

    #region Constructors

    public DocumentCatalog(Action<DocumentCatalogOptions> optionsFunc) : this(GetDocumentCatalogOptions(optionsFunc))
    {
    }

    public DocumentCatalog(DocumentCatalogOptions options) : base(GenerateDictionary(options))
    {
        options
            .NotNull(x => x.Pages)
            .CheckConstraints(x => x.Pages.Value.Count > 0, Resource.DocumentCatalog_Pages_MustNotBeEmpty);
    }

    #endregion

    #region Private Methods

    private static DocumentCatalogOptions GetDocumentCatalogOptions(Action<DocumentCatalogOptions> optionsFunc)
    {
        DocumentCatalogOptions fileTrailerOptions = new();
        optionsFunc.Invoke(fileTrailerOptions);
        return fileTrailerOptions;
    }

    private static IReadOnlyDictionary<PdfName, IPdfObject> GenerateDictionary(DocumentCatalogOptions options)
    {
        IDictionary<PdfName, IPdfObject> documentCatalogDictionary = new Dictionary<PdfName, IPdfObject>(2)
            .WithKeyValue(Type, Catalog)
            .WithKeyValue(Pages, options.Pages);

        return new ReadOnlyDictionary<PdfName, IPdfObject>(documentCatalogDictionary);
    }

    #endregion
}

public sealed class DocumentCatalogOptions
{
    public PdfDictionary<IPdfObject> Pages { get; set; } = default!;
}

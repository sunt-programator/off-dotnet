using System.Collections.ObjectModel;
using Off.Net.Pdf.Core.CommonDataStructures;
using Off.Net.Pdf.Core.ContentStreamAndResources;
using Off.Net.Pdf.Core.Extensions;
using Off.Net.Pdf.Core.Interfaces;
using Off.Net.Pdf.Core.Primitives;

namespace Off.Net.Pdf.Core.DocumentStructure;

public sealed class PageObject : PdfDictionary<IPdfObject>
{
    #region Fields

    private static readonly PdfName TypeName = new("Type");
    private static readonly PdfName TypeValue = new("Page");
    private static readonly PdfName Parent = new("Parent");
    private static readonly PdfName Resources = new("Resources");
    private static readonly PdfName MediaBox = new("MediaBox");

    #endregion

    #region Constructors

    public PageObject(Action<PageObjectOptions> optionsFunc) : this(GetPageObjectOptions(optionsFunc))
    {
    }

    public PageObject(PageObjectOptions options) : base(GenerateDictionary(options))
    {
        options.NotNull(x => x.Parent);
        options.NotNull(x => x.Resources);
        options.NotNull(x => x.MediaBox);
    }

    #endregion

    #region Private Methods

    private static PageObjectOptions GetPageObjectOptions(Action<PageObjectOptions> optionsFunc)
    {
        PageObjectOptions fileTrailerOptions = new();
        optionsFunc.Invoke(fileTrailerOptions);
        return fileTrailerOptions;
    }

    private static IReadOnlyDictionary<PdfName, IPdfObject> GenerateDictionary(PageObjectOptions options)
    {
        IDictionary<PdfName, IPdfObject> documentCatalogDictionary = new Dictionary<PdfName, IPdfObject>(4)
            .WithKeyValue(TypeName, TypeValue)
            .WithKeyValue(Parent, options.Parent)
            .WithKeyValue(Resources, options.Resources)
            .WithKeyValue(MediaBox, options.MediaBox);

        return new ReadOnlyDictionary<PdfName, IPdfObject>(documentCatalogDictionary);
    }

    #endregion
}

public sealed class PageObjectOptions
{
    public PdfIndirectIdentifier<PdfNull> Parent { get; set; } = default!; // TODO: convert to page tree node once it's implemented

    public ResourceDictionary Resources { get; set; } = default!;

    public Rectangle MediaBox { get; set; } = default!;
}

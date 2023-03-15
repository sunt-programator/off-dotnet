using System.Collections.ObjectModel;
using Off.Net.Pdf.Core.Interfaces;
using Off.Net.Pdf.Core.Primitives;
using Off.Net.Pdf.Core.Text.Fonts;

namespace Off.Net.Pdf.Core.ContentStreamAndResources;

public sealed class ResourceDictionary : PdfDictionary<IPdfObject>
{
    #region Fields

    private static readonly PdfName Font = new("Font");

    #endregion

    #region Constructors

    public ResourceDictionary(Action<ResourceDictionaryOptions> optionsFunc) : this(GetResourceDictionaryOptions(optionsFunc))
    {
    }

    public ResourceDictionary(ResourceDictionaryOptions options) : base(GenerateDictionary(options))
    {
    }

    #endregion

    #region Private Methods

    private static ResourceDictionaryOptions GetResourceDictionaryOptions(Action<ResourceDictionaryOptions> optionsFunc)
    {
        ResourceDictionaryOptions fileTrailerOptions = new();
        optionsFunc.Invoke(fileTrailerOptions);
        return fileTrailerOptions;
    }

    private static IReadOnlyDictionary<PdfName, IPdfObject> GenerateDictionary(ResourceDictionaryOptions options)
    {
        IDictionary<PdfName, IPdfObject> documentCatalogDictionary = new Dictionary<PdfName, IPdfObject>(1)
            .WithKeyValue(Font, options.Font);

        return new ReadOnlyDictionary<PdfName, IPdfObject>(documentCatalogDictionary);
    }

    #endregion
}

public sealed class ResourceDictionaryOptions
{
    public PdfDictionary<PdfIndirectIdentifier<Type1Font>>? Font { get; set; }
}

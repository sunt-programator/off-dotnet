using System.Collections.ObjectModel;
using Off.Net.Pdf.Core.Interfaces;
using Off.Net.Pdf.Core.Primitives;
using Off.Net.Pdf.Core.Text.Fonts;

namespace Off.Net.Pdf.Core.ContentStreamAndResources;

public sealed class ResourceDictionary : PdfDictionary<IPdfObject>
{
    #region Fields

    private static readonly PdfName Font = "Font";
    private static readonly PdfName ProcSet = "ProcSet";

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
        ResourceDictionaryOptions options = new();
        optionsFunc.Invoke(options);
        return options;
    }

    private static IReadOnlyDictionary<PdfName, IPdfObject> GenerateDictionary(ResourceDictionaryOptions options)
    {
        IDictionary<PdfName, IPdfObject> documentCatalogDictionary = new Dictionary<PdfName, IPdfObject>(2)
            .WithKeyValue(Font, options.Font)
            .WithKeyValue(ProcSet, options.ProcSet);

        return new ReadOnlyDictionary<PdfName, IPdfObject>(documentCatalogDictionary);
    }

    #endregion
}

public sealed class ResourceDictionaryOptions
{
    public static readonly PdfName ProcSetPdf = "PDF";
    public static readonly PdfName ProcSetText = "Text";
    public static readonly PdfName ProcSetImageB = "ImageB";
    public static readonly PdfName ProcSetImageC = "ImageC";
    public static readonly PdfName ProcSetImageI = "ImageI";
    private static readonly PdfArray<PdfName> DefaultProcedureSets = new[] { ProcSetPdf, ProcSetText, ProcSetImageB, ProcSetImageC, ProcSetImageI }.ToPdfArray();

    public PdfDictionary<PdfIndirectIdentifier<Type1Font>>? Font { get; set; }

    public PdfArray<PdfName>? ProcSet { get; set; } = DefaultProcedureSets;
}

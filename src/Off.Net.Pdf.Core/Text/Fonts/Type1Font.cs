using System.Collections.ObjectModel;
using Off.Net.Pdf.Core.Extensions;
using Off.Net.Pdf.Core.Interfaces;
using Off.Net.Pdf.Core.Primitives;

namespace Off.Net.Pdf.Core.Text.Fonts;

public sealed class Type1Font : PdfDictionary<IPdfObject>
{
    #region Fields

    private static readonly PdfName Type = new("Type");
    private static readonly PdfName TypeValue = new("Font");
    private static readonly PdfName Subtype = new("Subtype");
    private static readonly PdfName SubtypeValue = new("Type1");
    private static readonly PdfName FontName = new("Name");
    private static readonly PdfName BaseFont = new("BaseFont");

    #endregion

    #region Constructors

    public Type1Font(Action<Type1FontOptions> optionsFunc) : this(GetType1FontOptions(optionsFunc))
    {
    }

    public Type1Font(Type1FontOptions options) : base(GenerateDictionary(options))
    {
        options.NotNull(x => x.BaseFont);
    }

    #endregion

    #region Private Methods

    private static Type1FontOptions GetType1FontOptions(Action<Type1FontOptions> optionsFunc)
    {
        Type1FontOptions fileTrailerOptions = new();
        optionsFunc.Invoke(fileTrailerOptions);
        return fileTrailerOptions;
    }

    private static IReadOnlyDictionary<PdfName, IPdfObject> GenerateDictionary(Type1FontOptions options)
    {
        IDictionary<PdfName, IPdfObject> documentCatalogDictionary = new Dictionary<PdfName, IPdfObject>(4)
            .WithKeyValue(Type, TypeValue)
            .WithKeyValue(Subtype, SubtypeValue)
            .WithKeyValue(FontName, options.FontName)
            .WithKeyValue(BaseFont, options.BaseFont);

        return new ReadOnlyDictionary<PdfName, IPdfObject>(documentCatalogDictionary);
    }

    #endregion
}

public sealed class Type1FontOptions
{
    public PdfName? FontName { get; set; }
    public PdfName BaseFont { get; set; } = default!;
}

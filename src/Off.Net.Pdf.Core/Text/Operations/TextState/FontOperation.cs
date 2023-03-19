using Off.Net.Pdf.Core.ContentStreamAndResources;
using Off.Net.Pdf.Core.Extensions;
using Off.Net.Pdf.Core.Primitives;

namespace Off.Net.Pdf.Core.Text.Operations.TextState;

public sealed class FontOperation : PdfOperation
{
    #region Fields, Constants

    public const string OperatorName = "Tf";
    private readonly Lazy<int> _hashCode;

    #endregion

    #region Constructors

    public FontOperation(PdfName fontName, PdfInteger fontSize) : base(OperatorName)
    {
        FontName = fontName;
        FontSize = fontSize.CheckConstraints(x => x >= 0, Resource.FontOperation_FontSizeMustBePositive);

        _hashCode = new Lazy<int>(() => HashCode.Combine(nameof(FontOperation), fontName, fontSize, OperatorName));
    }

    #endregion

    #region Properties

    public PdfName FontName { get; }

    public PdfInteger FontSize { get; }

    #endregion

    #region Public Methods

    public override int GetHashCode()
    {
        return _hashCode.Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is FontOperation other && FontName == other.FontName && FontSize == other.FontSize;
    }

    #endregion

    #region Protected Methods

    protected override string GenerateContent()
    {
        return $"{FontName.Content} {FontSize.Content} {PdfOperator}\n";
    }

    #endregion
}

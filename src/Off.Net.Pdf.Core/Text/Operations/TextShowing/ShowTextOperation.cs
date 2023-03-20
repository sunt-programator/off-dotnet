using Off.Net.Pdf.Core.ContentStreamAndResources;
using Off.Net.Pdf.Core.Extensions;
using Off.Net.Pdf.Core.Primitives;

namespace Off.Net.Pdf.Core.Text.Operations.TextShowing;

public sealed class ShowTextOperation : PdfOperation
{
    #region Fields, Constants

    public const string OperatorName = "Tj";
    private readonly Lazy<int> _hashCode;

    #endregion

    #region Constructors

    public ShowTextOperation(PdfString text) : base(OperatorName)
    {
        Text = text.NotNull(x => x);

        _hashCode = new Lazy<int>(() => HashCode.Combine(nameof(ShowTextOperation), text, OperatorName));
    }

    #endregion

    #region Properties

    public PdfString Text { get; }

    #endregion

    #region Public Methods

    public override bool Equals(object? obj)
    {
        return obj is ShowTextOperation other && Text == other.Text;
    }

    #endregion

    #region Protected Methods

    public override int GetHashCode()
    {
        return _hashCode.Value;
    }

    protected override string GenerateContent()
    {
        return $"{Text.Content} {PdfOperator}\n";
    }

    #endregion
}

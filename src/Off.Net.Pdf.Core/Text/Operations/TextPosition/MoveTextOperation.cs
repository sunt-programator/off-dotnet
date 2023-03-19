using Off.Net.Pdf.Core.ContentStreamAndResources;
using Off.Net.Pdf.Core.Primitives;

namespace Off.Net.Pdf.Core.Text.Operations.TextPosition;

public sealed class MoveTextOperation : PdfOperation
{
    #region Fields, Constants

    public const string OperatorName = "Td";
    private readonly Lazy<int> _hashCode;

    #endregion

    #region Constructors

    public MoveTextOperation(float x, float y) : base(OperatorName)
    {
        X = x;
        Y = y;

        _hashCode = new Lazy<int>(() => HashCode.Combine(nameof(MoveTextOperation), x, y, OperatorName));
    }

    #endregion

    #region Properties

    public PdfReal X { get; }

    public PdfReal Y { get; }

    #endregion

    #region Public Methods

    public override int GetHashCode()
    {
        return _hashCode.Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is MoveTextOperation other && X == other.X && Y == other.Y;
    }

    #endregion

    #region Protected Methods

    protected override string GenerateContent()
    {
        return $"{X.Content} {Y.Content} {PdfOperator}\n";
    }

    #endregion
}

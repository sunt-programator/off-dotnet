using Off.Net.Pdf.Core.Extensions;
using Off.Net.Pdf.Core.Primitives;

namespace Off.Net.Pdf.Core.CommonDataStructures;

public sealed class Rectangle : PdfArray<PdfInteger>
{
    #region Constructors

    public Rectangle(int lowerLeftX, int lowerLeftY, int upperRightX, int upperRightY) : base(new List<PdfInteger> { lowerLeftX, lowerLeftY, upperRightX, upperRightY })
    {
        LowerLeftX = lowerLeftX.CheckConstraints(x => x >= 0, Resource.Rectangle_PointMustBePositive);
        LowerLeftY = lowerLeftY.CheckConstraints(x => x >= 0, Resource.Rectangle_PointMustBePositive);
        UpperRightX = upperRightX.CheckConstraints(x => x >= 0, Resource.Rectangle_PointMustBePositive);
        UpperRightY = upperRightY.CheckConstraints(x => x >= 0, Resource.Rectangle_PointMustBePositive);
    }

    #endregion

    #region Properties

    public int LowerLeftX { get; }

    public int LowerLeftY { get; }

    public int UpperRightX { get; }

    public int UpperRightY { get; }

    #endregion
}

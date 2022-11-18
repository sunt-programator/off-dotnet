using System.Text;
using Off.Net.Pdf.Core.Interfaces;

namespace Off.Net.Pdf.Core.Primitives;

public struct PdfNull : IPdfObject
{
    #region Fields
    private const string literalValue = "null";
    private static readonly int hashCode = HashCode.Combine(nameof(PdfNull).GetHashCode(), literalValue.GetHashCode());
    private static readonly byte[] bytes = Encoding.ASCII.GetBytes(literalValue);
    #endregion

    #region Properties
    public int Length => 4;

    public byte[] Bytes => bytes;

    public string Content => literalValue;
    #endregion

    #region Public Methods
    public override int GetHashCode()
    {
        return hashCode;
    }

    public override bool Equals(object? obj)
    {
        return obj is PdfNull;
    }
    #endregion
}

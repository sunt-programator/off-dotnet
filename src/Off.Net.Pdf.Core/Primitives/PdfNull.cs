using System.Text;
using Off.Net.Pdf.Core.Interfaces;

namespace Off.Net.Pdf.Core.Primitives;

public struct PdfNull : IPdfObject
{
    #region Fields
    private const string LiteralValue = "null";
    private static readonly int HashCode = System.HashCode.Combine(nameof(PdfNull).GetHashCode(), LiteralValue.GetHashCode());
    private static readonly byte[] BytesArray = Encoding.ASCII.GetBytes(LiteralValue);
    #endregion

    #region Properties
    public int Length => 4;

    public byte[] Bytes => BytesArray;

    public string Content => LiteralValue;
    #endregion

    #region Public Methods
    public override int GetHashCode()
    {
        return HashCode;
    }

    public override bool Equals(object? obj)
    {
        return obj is PdfNull;
    }
    #endregion
}

using System.Text;
using Off.Net.Pdf.Core.Interfaces;

namespace Off.Net.Pdf.Core.Primitives;

public sealed class PdfIndirectIdentifier<T> : IPdfObject, IEquatable<PdfIndirectIdentifier<T>> where T: IPdfObject
{
    #region Fields

    private readonly Lazy<int> _hashCode;
    private readonly Lazy<string> _literalValue;
    private readonly Lazy<byte[]> _bytes;

    #endregion

    #region Constructors

    public PdfIndirectIdentifier(PdfIndirect<T> pdfObject)
    {
        _hashCode = new Lazy<int>(() => HashCode.Combine(nameof(PdfIndirectIdentifier<T>), pdfObject.ObjectNumber, pdfObject.GenerationNumber));
        _literalValue = new Lazy<string>(GenerateContent);
        _bytes = new Lazy<byte[]>(() => Encoding.ASCII.GetBytes(Content));

        ObjectNumber = pdfObject.ObjectNumber;
        GenerationNumber = pdfObject.GenerationNumber;
        PdfIndirect = pdfObject;
    }

    #endregion

    #region Properties

    public int GenerationNumber { get; }

    public int ObjectNumber { get; }

    public int Length => Content.Length;

    public PdfIndirect<T> PdfIndirect { get; }

    public ReadOnlyMemory<byte> Bytes => _bytes.Value;

    public string Content => _literalValue.Value;

    #endregion

    #region Public Methods

    public override int GetHashCode()
    {
        return _hashCode.Value;
    }

    public bool Equals(PdfIndirectIdentifier<T>? other)
    {
        return other != null
               && ObjectNumber == other.ObjectNumber
               && GenerationNumber == other.GenerationNumber;
    }

    public override bool Equals(object? obj)
    {
        return obj is PdfIndirectIdentifier<T> pdfIndirect && Equals(pdfIndirect);
    }

    #endregion

    #region Private Methods

    private string GenerateContent()
    {
        return $"{ObjectNumber} {GenerationNumber} R";
    }

    #endregion
}

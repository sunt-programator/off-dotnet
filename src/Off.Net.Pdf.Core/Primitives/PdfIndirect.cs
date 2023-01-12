using System.Text;
using Off.Net.Pdf.Core.Interfaces;

namespace Off.Net.Pdf.Core.Primitives;

public sealed class PdfIndirect : IPdfObject<IPdfObject>, IEquatable<PdfIndirect>
{
    #region Fields

    private readonly int hashCode;
    private string literalValue = string.Empty;
    private byte[]? bytes;

    #endregion

    #region Constructors

    public PdfIndirect(IPdfObject pdfObject, int objectNumber, int generationNumber = 0)
    {
        if (objectNumber < 0)
        {
            throw new ArgumentException(Resource.PdfIndirect_ObjectNumberMustBePositive, nameof(objectNumber));
        }

        if (generationNumber < 0)
        {
            throw new ArgumentException(Resource.PdfIndirect_GenerationNumberMustBePositive, nameof(objectNumber));
        }

        ObjectNumber = objectNumber;
        GenerationNumber = generationNumber;
        Value = pdfObject;
        hashCode = HashCode.Combine(nameof(PdfIndirect).GetHashCode(), objectNumber.GetHashCode(), generationNumber.GetHashCode());
        bytes = null;
        ReferenceIdentifier = $"{ObjectNumber} {GenerationNumber} R";
    }

    #endregion

    #region Properties

    public int GenerationNumber { get; }

    public int ObjectNumber { get; }

    public string ReferenceIdentifier { get; }

    public int Length => Content.Length;

    public byte[] Bytes => bytes ??= Encoding.ASCII.GetBytes(Content);

    public string Content => GenerateContent();

    public IPdfObject Value { get; }

    #endregion

    #region Public Methods

    public override int GetHashCode()
    {
        return hashCode;
    }

    public bool Equals(PdfIndirect? other)
    {
        return other != null
               && ObjectNumber == other.ObjectNumber
               && GenerationNumber == other.GenerationNumber;
    }

    public override bool Equals(object? obj)
    {
        return obj is PdfIndirect pdfName && Equals(pdfName);
    }

    #endregion

    #region Private Methods

    private string GenerateContent()
    {
        if (literalValue.Length != 0)
        {
            return literalValue;
        }

        string objectIdentifier = $"{ObjectNumber} {GenerationNumber} obj";

        literalValue = new StringBuilder()
            .Append(objectIdentifier)
            .Append('\n')
            .Append(Value.Content)
            .Append('\n')
            .Append("endobj")
            .ToString();

        return literalValue;
    }

    #endregion
}

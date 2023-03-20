using System.Text;
using Off.Net.Pdf.Core.Extensions;
using Off.Net.Pdf.Core.Interfaces;

namespace Off.Net.Pdf.Core.Primitives;

public sealed class PdfIndirect<T> : IPdfObject<T>, IEquatable<PdfIndirect<T>> where T: IPdfObject
{
    #region Fields

    private readonly int _hashCode;
    private string _literalValue = string.Empty;
    private byte[]? _bytes;

    #endregion

    #region Constructors

    public PdfIndirect(T pdfObject, int objectNumber, int generationNumber = 0)
    {
        ObjectNumber = objectNumber
            .CheckConstraints(num => num >= 0, Resource.PdfIndirect_ObjectNumberMustBePositive);

        GenerationNumber = generationNumber
            .CheckConstraints(num => num >= 0, Resource.PdfIndirect_ObjectNumberMustBePositive)
            .CheckConstraints(num => num <= 65535, Resource.PdfIndirect_GenerationNumberMustNotExceedMaxAllowedValue);

        Value = pdfObject;
        _hashCode = HashCode.Combine(nameof(PdfIndirect<T>), objectNumber, generationNumber);
        _bytes = null;
    }

    #endregion

    #region Properties

    public int GenerationNumber { get; }

    public int ObjectNumber { get; }

    public int Length => Content.Length;

    public ReadOnlyMemory<byte> Bytes => _bytes ??= Encoding.ASCII.GetBytes(Content);

    public string Content => GenerateContent();

    public T Value { get; }

    #endregion

    #region Public Methods

    public override int GetHashCode()
    {
        return _hashCode;
    }

    public bool Equals(PdfIndirect<T>? other)
    {
        return other != null
               && ObjectNumber == other.ObjectNumber
               && GenerationNumber == other.GenerationNumber;
    }

    public override bool Equals(object? obj)
    {
        return obj is PdfIndirect<T> pdfIndirect && Equals(pdfIndirect);
    }

    #endregion

    #region Private Methods

    private string GenerateContent()
    {
        if (_literalValue.Length != 0)
        {
            return _literalValue;
        }

        string objectIdentifier = $"{ObjectNumber} {GenerationNumber} obj";

        _literalValue = new StringBuilder()
            .Append(objectIdentifier)
            .Append('\n')
            .Append(Value.Content)
            .Append('\n')
            .Append("endobj")
            .ToString();

        return _literalValue;
    }

    #endregion
}

public static class PdfIndirectExtensions
{
    public static PdfIndirect<T> ToPdfIndirect<T>(this T pdfObject, int objectNumber, int generationNumber = 0) where T : IPdfObject
    {
        return new PdfIndirect<T>(pdfObject, objectNumber, generationNumber);
    }
}

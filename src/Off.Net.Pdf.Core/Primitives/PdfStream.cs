using System.Text;
using Off.Net.Pdf.Core.Extensions;
using Off.Net.Pdf.Core.Interfaces;

namespace Off.Net.Pdf.Core.Primitives;

public sealed class PdfStream : IPdfObject<ReadOnlyMemory<char>>, IEquatable<PdfStream?>
{
    #region Fields

    private static readonly PdfName LengthKey = "Length";
    private static readonly PdfName FilterKey = "Filter";
    private static readonly PdfName DecodeParametersKey = "DecodeParms";
    private static readonly PdfName FileFilterKey = "FFilter";
    private static readonly PdfName FileDecodeParametersKey = "FDecodeParms";
    private static readonly PdfName FileSpecificationKey = "F";

    private readonly PdfStreamExtentOptions _pdfStreamExtentOptions = new();
    private readonly int _hashCode;
    private string _literalValue = string.Empty;
    private byte[]? _bytes;
    private PdfDictionary<IPdfObject>? _streamExtentDictionary;

    #endregion

    #region Constructors

    public PdfStream(ReadOnlyMemory<char> value, Action<PdfStreamExtentOptions>? options = null)
    {
        _hashCode = HashCode.Combine(nameof(PdfStream), value);
        _bytes = null;
        Value = value;
        options?.Invoke(_pdfStreamExtentOptions);
    }

    #endregion

    #region Properties

    public int Length => this.Bytes.Length;

    public ReadOnlyMemory<char> Value { get; }

    public ReadOnlyMemory<byte> Bytes => _bytes ??= Encoding.ASCII.GetBytes(Content);

    public string Content => GenerateContent();

    public PdfDictionary<IPdfObject> StreamExtent => GenerateStreamExtendDictionary();

    #endregion

    #region Public Methods

    public override int GetHashCode()
    {
        return _hashCode;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as PdfStream);
    }

    public bool Equals(PdfStream? other)
    {
        return other is not null && Value.Equals(other.Value);
    }

    #endregion

    #region Private Methods

    private string GenerateContent()
    {
        if (_literalValue.Length != 0)
        {
            return _literalValue;
        }

        StringBuilder stringBuilder = new StringBuilder()
            .Insert(0, "\nstream\n")
            .Append(this.Value);

        if (stringBuilder[^1] != '\n')
        {
            stringBuilder.Append('\n');
        }

        stringBuilder.Append("endstream");

        _literalValue = stringBuilder
            .Insert(0, StreamExtent.Content)
            .ToString();

        return _literalValue;
    }

    private PdfDictionary<IPdfObject> GenerateStreamExtendDictionary()
    {
        if (_streamExtentDictionary != null)
        {
            return _streamExtentDictionary;
        }

        int length = this.Value.Length;

        _streamExtentDictionary = new Dictionary<PdfName, IPdfObject>(6)
            .WithKeyValue(FilterKey, _pdfStreamExtentOptions.Filter?.PdfObject)
            .WithKeyValue(DecodeParametersKey, _pdfStreamExtentOptions.DecodeParameters?.PdfObject)
            .WithKeyValue(FileSpecificationKey, _pdfStreamExtentOptions.FileSpecification)
            .WithKeyValue(FileFilterKey, _pdfStreamExtentOptions.FileFilter?.PdfObject)
            .WithKeyValue(FileDecodeParametersKey, _pdfStreamExtentOptions.FileDecodeParameters?.PdfObject)
            .WithKeyValue(LengthKey, (PdfInteger)length)
            .ToPdfDictionary();

        return _streamExtentDictionary;
    }

    #endregion
}

public sealed class PdfStreamExtentOptions
{
    public AnyOf<PdfName, PdfArray<PdfName>>? Filter { get; set; } // Name or Array
    public AnyOf<PdfDictionary<PdfName>, PdfArray<PdfName>>? DecodeParameters { get; set; } // Dictionary or Array
    public PdfString? FileSpecification { get; set; }
    public AnyOf<PdfName, PdfArray<PdfName>>? FileFilter { get; set; } // Name or Array
    public AnyOf<PdfDictionary<PdfName>, PdfArray<PdfName>>? FileDecodeParameters { get; set; } // Dictionary or Array
}

public static class PdfStreamExtensions
{
    public static PdfStream ToPdfStream(this IPdfObject pdfObject, Action<PdfStreamExtentOptions>? options = null)
    {
        return new PdfStream(pdfObject.Content.AsMemory(), options);
    }
}

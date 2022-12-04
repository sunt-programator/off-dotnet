using System.Text;
using Off.Net.Pdf.Core.Interfaces;

namespace Off.Net.Pdf.Core.Primitives;

public sealed class PdfStream : IPdfObject<ReadOnlyMemory<char>>, IEquatable<PdfStream?>
{
    #region Fields

    private static readonly PdfName LengthKey = new("Length");
    private static readonly PdfName FilterKey = new("Filter");
    private static readonly PdfName DecodeParametersKey = new("DecodeParms");
    private static readonly PdfName FileFilterKey = new("FFilter");
    private static readonly PdfName FileDecodeParametersKey = new("FDecodeParms");
    private static readonly PdfName FileSpecificationKey = new("F");

    private readonly PdfStreamExtentOptions _pdfStreamExtentOptions = new();
    private readonly int _hashCode;
    private string _literalValue = string.Empty;
    private byte[]? _bytes;
    private PdfDictionary? _streamExtentDictionary;

    #endregion

    #region Constructors

    public PdfStream(ReadOnlyMemory<char> value, Action<PdfStreamExtentOptions>? options = null)
    {
        _hashCode = HashCode.Combine(nameof(PdfStream).GetHashCode(), value.GetHashCode());
        _bytes = null;
        Value = value;
        options?.Invoke(_pdfStreamExtentOptions);
    }

    #endregion

    #region Properties

    public int Length => Content.Length;

    public ReadOnlyMemory<char> Value { get; }

    public byte[] Bytes => _bytes ??= Encoding.ASCII.GetBytes(Content);

    public string Content => GenerateContent();

    public PdfDictionary StreamExtent => GenerateStreamExtendDictionary();

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
            .Insert(0, "stream")
            .Append('\n')
            .Append(Value)
            .Append('\n')
            .Append("endstream");

        _literalValue = stringBuilder
            .Insert(0, StreamExtent.Content)
            .Insert(StreamExtent.Content.Length, '\n')
            .ToString();

        return _literalValue;
    }

    private PdfDictionary GenerateStreamExtendDictionary()
    {
        if (_streamExtentDictionary != null)
        {
            return _streamExtentDictionary;
        }

        const int streamContentWrapperLength = 17; // 'stream' + '\n' + '<content_byte_array>' + '\n' + 'endstream'
        int length = Value.Length + streamContentWrapperLength;

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

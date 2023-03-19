using System.Text;
using Off.Net.Pdf.Core.ContentStreamAndResources;
using Off.Net.Pdf.Core.Interfaces;

namespace Off.Net.Pdf.Core.Text;

public sealed class TextObject : IPdfObject<IReadOnlyCollection<PdfOperation>>
{
    #region Fields

    private readonly Lazy<int> _hashCode;
    private readonly Lazy<string> _literalValue;
    private readonly Lazy<byte[]> _bytes;

    #endregion

    #region Constructors

    public TextObject(IReadOnlyCollection<PdfOperation> operations)
    {
        Value = operations;

        _hashCode = new Lazy<int>(() => HashCode.Combine(nameof(TextObject), operations));
        _literalValue = new Lazy<string>(GenerateContent);
        _bytes = new Lazy<byte[]>(() => Encoding.ASCII.GetBytes(Content));
    }

    #endregion

    #region Properties

    public int Length => Content.Length;

    public ReadOnlyMemory<byte> Bytes => _bytes.Value;

    public IReadOnlyCollection<PdfOperation> Value { get; }

    public string Content => _literalValue.Value;

    #endregion

    #region Public Methods

    public override int GetHashCode()
    {
        return _hashCode.Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is TextObject other && EqualityComparer<IReadOnlyCollection<PdfOperation>>.Default.Equals(Value, other.Value);
    }

    #endregion

    #region Private Methods

    private string GenerateContent()
    {
        StringBuilder stringBuilder = new();

        foreach (PdfOperation pdfOperation in Value)
        {
            stringBuilder.Append(pdfOperation.Content);
        }

        return stringBuilder
            .Insert(0, "BT\n")
            .Append("ET\n")
            .ToString();
    }

    #endregion
}

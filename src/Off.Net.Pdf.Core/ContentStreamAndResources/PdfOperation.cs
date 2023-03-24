using System.Text;
using Off.Net.Pdf.Core.Interfaces;

namespace Off.Net.Pdf.Core.ContentStreamAndResources;

public abstract class PdfOperation : IPdfObject
{
    #region Fields

    private readonly Lazy<string> _literalValue;
    private readonly Lazy<byte[]> _bytes;

    #endregion

    #region Constructors

    protected PdfOperation(string @operator)
    {
        PdfOperator = @operator;
        _literalValue = new Lazy<string>(GenerateContent);
        _bytes = new Lazy<byte[]>(() => Encoding.ASCII.GetBytes(Content));
    }

    #endregion

    #region Properties

    public int Length => this.Bytes.Length;

    public string PdfOperator { get; }

    public ReadOnlyMemory<byte> Bytes => _bytes.Value;

    public string Content => _literalValue.Value;

    #endregion

    #region Public Methods

    public abstract override int GetHashCode();

    public abstract override bool Equals(object? obj);

    #endregion

    #region Protected Methods

    protected abstract string GenerateContent();

    #endregion
}

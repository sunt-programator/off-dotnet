using System.Text;
using Off.Net.Pdf.Core.Extensions;
using Off.Net.Pdf.Core.Interfaces;

namespace Off.Net.Pdf.Core.FileStructure;

public readonly struct FileHeader : IPdfObject, IEquatable<FileHeader>
{
    public static readonly FileHeader PdfVersion10 = new(1, 0);
    public static readonly FileHeader PdfVersion11 = new(1, 1);
    public static readonly FileHeader PdfVersion12 = new(1, 2);
    public static readonly FileHeader PdfVersion13 = new(1, 3);
    public static readonly FileHeader PdfVersion14 = new(1, 4);
    public static readonly FileHeader PdfVersion15 = new(1, 5);
    public static readonly FileHeader PdfVersion16 = new(1, 6);
    public static readonly FileHeader PdfVersion17 = new(1, 7);
    public static readonly FileHeader PdfVersion20 = new(2, 0);

    #region Constructors

    public FileHeader(int majorVersion, int minorVersion)
    {
        MajorVersion = majorVersion.CheckConstraints(num => num is 1 or 2, Resource.FileHeader_MajorVersionIsNotValid);
        MinorVersion = minorVersion.CheckConstraints(num => num is >= 0 and <= 9, Resource.FileHeader_MinorVersionIsNotValid);
    }

    #endregion

    #region Properties

    public int Length => Content.Length;

    public ReadOnlyMemory<byte> Bytes => Encoding.ASCII.GetBytes(Content);

    public int MajorVersion { get; }

    public int MinorVersion { get; }

    public string Content => $"%PDF-{MajorVersion}.{MinorVersion}\n";

    #endregion

    #region Public Methods

    public override int GetHashCode()
    {
        return HashCode.Combine(nameof(FileHeader).GetHashCode(), MajorVersion, MinorVersion);
    }

    public override bool Equals(object? obj)
    {
        return obj is FileHeader other && Equals(other);
    }

    public bool Equals(FileHeader other)
    {
        return MajorVersion == other.MajorVersion &&
               MinorVersion == other.MinorVersion;
    }

    #endregion

    #region Operators

    public static bool operator ==(FileHeader leftOperator, FileHeader rightOperator)
    {
        return leftOperator.Equals(rightOperator);
    }

    public static bool operator !=(FileHeader leftOperator, FileHeader rightOperator)
    {
        return !leftOperator.Equals(rightOperator);
    }

    #endregion
}

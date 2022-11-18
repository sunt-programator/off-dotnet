namespace Off.Net.Pdf.Core.Interfaces;

public interface IPdfObject
{
    /// <summary>
    /// Gets the length of the object.
    /// </summary>
    int Length { get; }

    /// <summary>
    /// Gets the bytes array representation of the Pdf object.
    /// </summary>
    byte[] Bytes { get; }

    string Content { get; }
}

public interface IPdfObject<out T> : IPdfObject
{
    /// <summary>
    /// Gets or sets the value of the object.
    /// </summary>
    T Value { get; }
}

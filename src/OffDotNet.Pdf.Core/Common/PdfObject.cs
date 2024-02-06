// <copyright file="PdfObject.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Common;

public abstract class PdfObject : IPdfObject, IEquatable<PdfObject>, IEqualityComparer<PdfObject>
{
    /// <inheritdoc/>
    public abstract ReadOnlyMemory<byte> Bytes { get; }

    /// <inheritdoc/>
    public abstract string Content { get; }

    public static bool operator ==(PdfObject? leftOperator, PdfObject? rightOperator)
    {
        return Equals(leftOperator?.GetEqualityComponents(), rightOperator?.GetEqualityComponents());
    }

    public static bool operator !=(PdfObject? leftOperator, PdfObject? rightOperator)
    {
        return !(leftOperator == rightOperator);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is PdfObject pdfObject && this.Equals(pdfObject);
    }

    /// <inheritdoc/>
    public bool Equals(PdfObject? other)
    {
        return this.Equals(this, other);
    }

    /// <inheritdoc/>
    public bool Equals(PdfObject? x, PdfObject? y)
    {
        return Equals(x?.GetEqualityComponents(), y?.GetEqualityComponents());
    }

    /// <inheritdoc/>
    public int GetHashCode(PdfObject obj)
    {
        return obj.GetHashCode();
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return GetHashCode(this.GetType().Name, this.GetEqualityComponents());
    }

    protected abstract IEnumerable<object> GetEqualityComponents();

    private static int GetHashCode(string typeName, IEnumerable<object> equalityComponents)
    {
        var hashCode = default(HashCode);

        var index = typeName.IndexOf('`');
        var parsedObjectName = typeName.AsSpan()[..(index > 0 ? index : typeName.Length)].ToString();

        hashCode.Add(parsedObjectName);

        foreach (var component in equalityComponents)
        {
            hashCode.Add(component);
        }

        return hashCode.ToHashCode();
    }

    private static bool Equals(IEnumerable<object>? x, IEnumerable<object>? y)
    {
        if (ReferenceEquals(x, null) && ReferenceEquals(y, null))
        {
            return true;
        }

        if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
        {
            return false;
        }

        return x.SequenceEqual(y);
    }
}

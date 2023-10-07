// <copyright file="PdfObject.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Diagnostics.CodeAnalysis;

namespace OffDotNet.Pdf.Core.Common;

[SuppressMessage("Usage", "S4035:Seal class 'PdfObject' or implement 'IEqualityComparer' instead.", Justification = "GetEqualityComponents will contain property collection.")]
public abstract class PdfObject : IPdfObject, IEquatable<PdfObject>, IEqualityComparer<PdfObject>
{
    public abstract ReadOnlyMemory<byte> Bytes { get; }

    public abstract string Content { get; }

    public static bool operator ==(PdfObject? leftOperator, PdfObject? rightOperator)
    {
        return Equals(leftOperator?.GetEqualityComponents(), rightOperator?.GetEqualityComponents());
    }

    public static bool operator !=(PdfObject? leftOperator, PdfObject? rightOperator)
    {
        return !(leftOperator == rightOperator);
    }

    public override bool Equals(object? obj)
    {
        return obj is PdfObject pdfObject && this.Equals(pdfObject);
    }

    public bool Equals(PdfObject? other)
    {
        return this.Equals(this, other);
    }

    public bool Equals(PdfObject? x, PdfObject? y)
    {
        return Equals(x?.GetEqualityComponents(), y?.GetEqualityComponents());
    }

    public int GetHashCode(PdfObject obj)
    {
        return obj.GetHashCode();
    }

    public override int GetHashCode()
    {
        return GetHashCode(this.GetType().Name, this.GetEqualityComponents());
    }

    protected abstract IEnumerable<object> GetEqualityComponents();

    private static int GetHashCode(string typeName, IEnumerable<object> equalityComponents)
    {
        var hashCode = default(HashCode);

        int index = typeName.IndexOf('`');
        string parsedObjectName = typeName.AsSpan()[..(index > 0 ? index : typeName.Length)].ToString();

        hashCode.Add(parsedObjectName);

        foreach (object component in equalityComponents)
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

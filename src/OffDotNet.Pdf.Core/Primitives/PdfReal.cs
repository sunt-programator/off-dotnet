// <copyright file="PdfReal.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Primitives;

using System.Globalization;
using System.Text;
using Common;
using Properties;

public struct PdfReal : IPdfObject, IEquatable<PdfReal>, IComparable, IComparable<PdfReal>
{
    private const float Tolerance = 0.00001f;
    private const float ApproximationValue = 1.175e-38f;
    private readonly int hashCode;
    private string literalValue = string.Empty;
    private byte[]? bytes;

    public PdfReal()
        : this(0f)
    {
    }

    public PdfReal(float value)
    {
        if (value is >= -ApproximationValue and <= ApproximationValue)
        {
            value = 0;
        }

        this.Value = value;
        this.hashCode = HashCode.Combine(nameof(PdfReal), value);
        this.bytes = null;
    }

    public float Value { get; }

    /// <inheritdoc/>
    public ReadOnlyMemory<byte> Bytes => this.bytes ??= Encoding.ASCII.GetBytes(this.Content);

    /// <inheritdoc/>
    public string Content => this.GenerateContent();

    public static implicit operator float(PdfReal pdfReal)
    {
        return pdfReal.Value;
    }

    public static implicit operator PdfReal(float value)
    {
        return new PdfReal(value);
    }

    public static implicit operator PdfReal(int value)
    {
        return new PdfReal(value);
    }

    public static bool operator ==(PdfReal leftOperator, PdfReal rightOperator)
    {
        return leftOperator.CompareTo(rightOperator) == 0;
    }

    public static bool operator !=(PdfReal leftOperator, PdfReal rightOperator)
    {
        return leftOperator.CompareTo(rightOperator) != 0;
    }

    public static bool operator <(PdfReal leftOperator, PdfReal rightOperator)
    {
        return leftOperator.CompareTo(rightOperator) < 0;
    }

    public static bool operator <=(PdfReal leftOperator, PdfReal rightOperator)
    {
        return leftOperator.CompareTo(rightOperator) <= 0;
    }

    public static bool operator >(PdfReal leftOperator, PdfReal rightOperator)
    {
        return leftOperator.CompareTo(rightOperator) > 0;
    }

    public static bool operator >=(PdfReal leftOperator, PdfReal rightOperator)
    {
        return leftOperator.CompareTo(rightOperator) >= 0;
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return this.hashCode;
    }

    /// <inheritdoc/>
    public bool Equals(PdfReal other)
    {
        return Math.Abs(this.Value - other.Value) < Tolerance;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return (obj is PdfReal pdfReal) && this.Equals(pdfReal);
    }

    /// <inheritdoc/>
    public int CompareTo(object? obj)
    {
        if (obj is not PdfReal pdfReal)
        {
            throw new ArgumentException(Resource.PdfReal_MustBePdfReal);
        }

        return this.CompareTo(pdfReal);
    }

    /// <inheritdoc/>
    public int CompareTo(PdfReal other)
    {
        if (Math.Abs(this.Value - other.Value) < Tolerance)
        {
            return 0;
        }

        return this.Value > other.Value ? 1 : -1;
    }

    private string GenerateContent()
    {
        if (this.literalValue.Length == 0)
        {
            this.literalValue = this.Value.ToString(CultureInfo.InvariantCulture);
        }

        return this.literalValue;
    }
}

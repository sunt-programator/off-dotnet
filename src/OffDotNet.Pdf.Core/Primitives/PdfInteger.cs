﻿// <copyright file="PdfInteger.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Primitives;

using System.Globalization;
using System.Text;
using Common;
using Properties;

public struct PdfInteger : IPdfObject, IEquatable<PdfInteger>, IComparable, IComparable<PdfInteger>
{
    private readonly int _hashCode;
    private string _literalValue = string.Empty;
    private byte[]? _bytes;

    public PdfInteger()
        : this(0)
    {
    }

    public PdfInteger(int value)
    {
        this.Value = value;
        _hashCode = HashCode.Combine(nameof(PdfInteger), value);
        _bytes = null;
    }

    public int Value { get; }

    /// <inheritdoc/>
    public ReadOnlyMemory<byte> Bytes => _bytes ??= Encoding.ASCII.GetBytes(this.Content);

    /// <inheritdoc/>
    public string Content => this.GenerateContent();

    public static implicit operator int(PdfInteger pdfInteger)
    {
        return pdfInteger.Value;
    }

    public static implicit operator PdfInteger(int value)
    {
        return new PdfInteger(value);
    }

    public static bool operator ==(PdfInteger leftOperator, PdfInteger rightOperator)
    {
        return leftOperator.CompareTo(rightOperator) == 0;
    }

    public static bool operator !=(PdfInteger leftOperator, PdfInteger rightOperator)
    {
        return leftOperator.CompareTo(rightOperator) != 0;
    }

    public static bool operator <(PdfInteger leftOperator, PdfInteger rightOperator)
    {
        return leftOperator.CompareTo(rightOperator) < 0;
    }

    public static bool operator <=(PdfInteger leftOperator, PdfInteger rightOperator)
    {
        return leftOperator.CompareTo(rightOperator) <= 0;
    }

    public static bool operator >(PdfInteger leftOperator, PdfInteger rightOperator)
    {
        return leftOperator.CompareTo(rightOperator) > 0;
    }

    public static bool operator >=(PdfInteger leftOperator, PdfInteger rightOperator)
    {
        return leftOperator.CompareTo(rightOperator) >= 0;
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return _hashCode;
    }

    /// <inheritdoc/>
    public bool Equals(PdfInteger other)
    {
        return this.Value == other.Value;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return (obj is PdfInteger integerObject) && this.Equals(integerObject);
    }

    /// <inheritdoc/>
    public int CompareTo(object? obj)
    {
        if (obj is not PdfInteger pdfInteger)
        {
            throw new ArgumentException(Resource.PdfInteger_MustBePdfInteger);
        }

        return this.CompareTo(pdfInteger);
    }

    /// <inheritdoc/>
    public int CompareTo(PdfInteger other)
    {
        if (this.Value == other.Value)
        {
            return 0;
        }

        return this.Value > other.Value ? 1 : -1;
    }

    private string GenerateContent()
    {
        if (_literalValue.Length == 0)
        {
            _literalValue = this.Value.ToString(CultureInfo.InvariantCulture);
        }

        return _literalValue;
    }
}

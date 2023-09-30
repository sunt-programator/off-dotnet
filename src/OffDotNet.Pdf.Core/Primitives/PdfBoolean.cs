// <copyright file="PdfBoolean.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Text;
using OffDotNet.Pdf.Core.Common;

namespace OffDotNet.Pdf.Core.Primitives;

public readonly struct PdfBoolean : IPdfObject, IEquatable<PdfBoolean>
{
    public PdfBoolean()
        : this(false)
    {
    }

    public PdfBoolean(bool value)
    {
        this.Value = value;
        this.Content = value ? "true" : "false";
        this.Bytes = Encoding.ASCII.GetBytes(this.Content);
    }

    public bool Value { get; }

    public ReadOnlyMemory<byte> Bytes { get; }

    public string Content { get; }

    public static implicit operator bool(PdfBoolean pdfBoolean)
    {
        return pdfBoolean.Value;
    }

    public static implicit operator PdfBoolean(bool value)
    {
        return new PdfBoolean(value);
    }

    public static bool operator ==(PdfBoolean leftOperator, PdfBoolean rightOperator)
    {
        return leftOperator.Equals(rightOperator);
    }

    public static bool operator !=(PdfBoolean leftOperator, PdfBoolean rightOperator)
    {
        return !leftOperator.Equals(rightOperator);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(nameof(PdfBoolean), this.Value);
    }

    public bool Equals(PdfBoolean other)
    {
        return this.Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return (obj is PdfBoolean booleanObject) && this.Equals(booleanObject);
    }
}

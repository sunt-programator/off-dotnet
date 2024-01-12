// <copyright file="LinePositionSpan.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Globalization;

namespace OffDotNet.Pdf.CodeAnalysis.Text;

public readonly struct LinePositionSpan : IEquatable<LinePositionSpan>
{
    public LinePositionSpan(LinePosition start, LinePosition end)
    {
        if (end < start)
        {
            string message = string.Format(CultureInfo.CurrentCulture, PDFResources.LinePositionSpan_EndMustNotBeLessThanStart, start, end);
            throw new ArgumentException(message, nameof(end));
        }

        this.Start = start;
        this.End = end;
    }

    public LinePosition Start { get; }

    public LinePosition End { get; }

    public static bool operator ==(LinePositionSpan left, LinePositionSpan right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(LinePositionSpan left, LinePositionSpan right)
    {
        return !left.Equals(right);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.Start, this.End);
    }

    public override string ToString()
    {
        return $"({this.Start})-({this.End})";
    }

    public bool Equals(LinePositionSpan other)
    {
        return this.Start.Equals(other.Start) && this.End.Equals(other.End);
    }

    public override bool Equals(object? obj)
    {
        return obj is LinePositionSpan other && this.Equals(other);
    }
}

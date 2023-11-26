// <copyright file="TextSpan.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace OffDotNet.Pdf.CodeAnalysis.Text;

/// <summary>Immutable struct representing a span of text, i.e. <c>[150, 153)</c>.</summary>
[SuppressMessage("Minor Code Smell", "S1210:\"Equals\" and the comparison operators should be overridden when implementing \"IComparable\"", Justification = "Not needed.")]
[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
public readonly struct TextSpan : IEquatable<TextSpan>, IComparable<TextSpan>
{
    /// <summary>Initializes a new instance of the <see cref="TextSpan"/> struct with the specified <paramref name="start"/> and <paramref name="length"/> parameters.</summary>
    /// <param name="start">The start point of the span.</param>
    /// <param name="length">The length of the span.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="start"/> or <paramref name="length"/> is negative.</exception>
    public TextSpan(int start, int length)
    {
        if (start < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(start));
        }

        if (length < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(length));
        }

        this.Start = start;
        this.Length = length;
    }

    /// <summary>Gets the start point of the span.</summary>
    public int Start { get; }

    /// <summary>Gets the end point of the span.</summary>
    public int End => this.Start + this.Length;

    /// <summary>Gets the length of the span.</summary>
    public int Length { get; }

    /// <summary>Gets a value indicating whether the current <see cref="TextSpan"/> is empty.</summary>
    public bool IsEmpty => this.Length == 0;

    /// <summary>Indicates whether the current <see cref="TextSpan"/> is equal to another <see cref="TextSpan"/>.</summary>
    /// <param name="left">A left <see cref="TextSpan"/> to compare with this <see cref="TextSpan"/>.</param>
    /// <param name="right">A right <see cref="TextSpan"/> to compare with this <see cref="TextSpan"/>.</param>
    /// <returns><see langword="true"/> if the current <see cref="TextSpan"/> is equal to the <paramref name="right"/> parameter; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(TextSpan left, TextSpan right)
    {
        return left.Equals(right);
    }

    /// <summary>Indicates whether the current <see cref="TextSpan"/> is equal to another <see cref="TextSpan"/>.</summary>
    /// <param name="left">A left <see cref="TextSpan"/> to compare with this <see cref="TextSpan"/>.</param>
    /// <param name="right">A right <see cref="TextSpan"/> to compare with this <see cref="TextSpan"/>.</param>
    /// <returns><see langword="true"/> if the current <see cref="TextSpan"/> is not equal to the <paramref name="right"/> parameter; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(TextSpan left, TextSpan right)
    {
        return !left.Equals(right);
    }

    /// <summary>Indicates whether the current <see cref="TextSpan"/> is equal to another <see cref="TextSpan"/>.</summary>
    /// <param name="other">A <see cref="TextSpan"/> to compare with this <see cref="TextSpan"/>.</param>
    /// <returns><see langword="true"/> if the current <see cref="TextSpan"/> is equal to the <paramref name="other"/> parameter; otherwise, <see langword="false"/>.</returns>
    public bool Equals(TextSpan other)
    {
        return this.Start == other.Start && this.Length == other.Length;
    }

    /// <summary>Indicates whether the current <see cref="TextSpan"/> is equal to another <see cref="TextSpan"/>.</summary>
    /// <param name="obj">A <see cref="TextSpan"/> to compare with this <see cref="TextSpan"/>.</param>
    /// <returns><see langword="true"/> if the current <see cref="TextSpan"/> is equal to the <paramref name="obj"/> parameter; otherwise, <see langword="false"/>.</returns>
    public override bool Equals(object? obj)
    {
        return obj is TextSpan other && this.Equals(other);
    }

    /// <summary>Returns the hash code for this <see cref="TextSpan"/>.</summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(this.Start, this.Length);
    }

    /// <summary>Provides a string representation of the <see cref="TextSpan"/>.</summary>
    /// <remarks>This representation uses "half-open interval" notation, indicating the endpoint character is not included.</remarks>
    /// <example><c>[10..20)</c>, indicating the text starts at position 10 and ends at position 20 not included.</example>
    /// <returns>The "half-open interval" notation of the text span.</returns>
    public override string ToString()
    {
        return $"[{this.Start}..{this.End})";
    }

    /// <summary>Compares two <see cref="TextSpan"/>s by their <see cref="Start"/> and <see cref="Length"/> properties.</summary>
    /// <param name="other">A <see cref="TextSpan"/> to compare with this <see cref="TextSpan"/>.</param>
    /// <returns>A value that indicates the relative order of the objects being compared.</returns>
    public int CompareTo(TextSpan other)
    {
        int diff = this.Start - other.Start;
        return diff != 0 ? diff : this.Length - other.Length;
    }

    private string GetDebuggerDisplay()
    {
        return this.ToString();
    }
}

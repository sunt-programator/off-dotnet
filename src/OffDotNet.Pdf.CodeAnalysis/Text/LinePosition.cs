// <copyright file="LinePosition.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Text;

public readonly struct LinePosition : IEquatable<LinePosition>, IComparable<LinePosition>
{
    public LinePosition(int line, int character)
    {
        if (line < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(line), line, PDFResources.LinePosition_NegativeLineException);
        }

        if (character < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(character), character, PDFResources.LinePosition_NegativeCharacterException);
        }

        this.Line = line;
        this.Character = character;
    }

    public int Line { get; }

    public int Character { get; }

    public static bool operator ==(LinePosition left, LinePosition right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(LinePosition left, LinePosition right)
    {
        return !left.Equals(right);
    }

    public static bool operator <(LinePosition left, LinePosition right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator >(LinePosition left, LinePosition right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator <=(LinePosition left, LinePosition right)
    {
        return left.CompareTo(right) <= 0;
    }

    public static bool operator >=(LinePosition left, LinePosition right)
    {
        return left.CompareTo(right) >= 0;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.Line, this.Character);
    }

    public bool Equals(LinePosition other)
    {
        return this.Line == other.Line && this.Character == other.Character;
    }

    public override bool Equals(object? obj)
    {
        return obj is LinePosition other && this.Equals(other);
    }

    public int CompareTo(LinePosition other)
    {
        int lineComparison = this.Line.CompareTo(other.Line);
        return lineComparison != 0 ? lineComparison : this.Character.CompareTo(other.Character);
    }

    public override string ToString()
    {
        return this.Line + "," + this.Character;
    }
}

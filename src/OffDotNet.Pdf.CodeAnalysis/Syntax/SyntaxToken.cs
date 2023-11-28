// <copyright file="SyntaxToken.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax;

public readonly struct SyntaxToken : IEquatable<SyntaxToken>
{
    public bool Equals(SyntaxToken other)
    {
        return true;
    }

    public override bool Equals(object? obj)
    {
        return obj is SyntaxToken other && this.Equals(other);
    }

    public override int GetHashCode()
    {
        return 0;
    }

    public static bool operator ==(SyntaxToken left, SyntaxToken right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(SyntaxToken left, SyntaxToken right)
    {
        return !left.Equals(right);
    }
}

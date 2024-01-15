// <copyright file="Location.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Diagnostics;
using System.Text;
using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Text;

namespace OffDotNet.Pdf.CodeAnalysis.Diagnostic;

[DebuggerDisplay("{ToString(), nq}")]
public abstract partial class Location : IEquatable<Location>
{
    public static Location None => NoLocation.Instance;

    public abstract LocationKind Kind { get; }

    public virtual TextSpan SourceSpan => default;

    public virtual FileLinePositionSpan LineSpan => default;

    public virtual SyntaxTree? SyntaxTree => null;

    public static bool operator ==(Location? left, Location? right)
    {
        if (ReferenceEquals(left, null))
        {
            return ReferenceEquals(right, null);
        }

        return left.Equals(right);
    }

    public static bool operator !=(Location? left, Location? right)
    {
        return !(left == right);
    }

    public bool Equals(Location? other)
    {
        return this.Equals((object?)other);
    }

    public abstract override bool Equals(object? obj);

    public abstract override int GetHashCode();

    public override string ToString()
    {
        StringBuilder result = new(this.Kind.ToString());

        if (this.SyntaxTree is not null)
        {
            return result
                 .Append(' ')
                 .Append('(')
                 .Append(this.SyntaxTree.FilePath)
                 .Append(this.SourceSpan)
                 .Append(')')
                 .ToString();
        }

        if (!string.IsNullOrEmpty(this.LineSpan.Path))
        {
            return result
                .Append(' ')
                .Append('(')
                .Append(this.LineSpan.Path)
                .Append('@')
                .Append(this.LineSpan.StartLinePosition.Line + 1) // user-visible line and column counts are 1-based, but internally are 0-based.
                .Append(':')
                .Append(this.LineSpan.StartLinePosition.Character + 1) // user-visible line and column counts are 1-based, but internally are 0-based.
                .Append(')')
                .ToString();
        }

        return result.ToString();
    }
}

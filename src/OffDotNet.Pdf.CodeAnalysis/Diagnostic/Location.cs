// <copyright file="Location.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Diagnostic;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Syntax;
using Text;

[DebuggerDisplay("{ToString(), nq}")]
[SuppressMessage("Major Code Smell", "S4035:Classes implementing \"IEquatable<T>\" should be sealed", Justification = "To be reviewed.")]
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

    public static Location Create(string filePath, TextSpan textSpan, LinePositionSpan lineSpan)
    {
        if (filePath == null)
        {
            throw new ArgumentNullException(nameof(filePath));
        }

        return new ExternalFileLocation(filePath, textSpan, lineSpan);
    }

    public static Location Create(SyntaxTree syntaxTree, TextSpan textSpan)
    {
        if (syntaxTree == null)
        {
            throw new ArgumentNullException(nameof(syntaxTree));
        }

        return new SourceLocation(syntaxTree, textSpan);
    }

    /// <inheritdoc/>
    public bool Equals(Location? other)
    {
        return this.Equals((object?)other);
    }

    /// <inheritdoc/>
    public abstract override bool Equals(object? obj);

    /// <inheritdoc/>
    public abstract override int GetHashCode();

    /// <inheritdoc/>
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

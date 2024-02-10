// <copyright file="SourceLocation.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Diagnostic;

using System.Diagnostics;
using Syntax;
using Text;

[DebuggerDisplay("{ToString(), nq}")]
internal sealed class SourceLocation : Location, IEquatable<SourceLocation>
{
    internal SourceLocation(SyntaxTree syntaxTree, TextSpan span)
    {
        this.SourceSpan = span;
        this.SyntaxTree = syntaxTree;
    }

    /// <inheritdoc/>
    public override LocationKind Kind => LocationKind.SourceFile;

    /// <inheritdoc/>
    public override TextSpan SourceSpan { get; }

    /// <inheritdoc/>
    public override SyntaxTree? SyntaxTree { get; }

    /// <inheritdoc/>
    public bool Equals(SourceLocation? other)
    {
        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return other != null && this.SyntaxTree == other.SyntaxTree && this.SourceSpan == other.SourceSpan;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return this.Equals(obj as SourceLocation);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return HashCode.Combine(this.SyntaxTree, this.LineSpan);
    }
}

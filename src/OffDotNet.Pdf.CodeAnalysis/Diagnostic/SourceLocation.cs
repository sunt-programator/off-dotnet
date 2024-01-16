// <copyright file="SourceLocation.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Diagnostics;
using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Text;

namespace OffDotNet.Pdf.CodeAnalysis.Diagnostic;

[DebuggerDisplay("{ToString(), nq}")]
internal sealed class SourceLocation : Location, IEquatable<SourceLocation>
{
    internal SourceLocation(SyntaxTree syntaxTree, TextSpan span)
    {
        this.SourceSpan = span;
        this.SyntaxTree = syntaxTree;
    }

    public override LocationKind Kind => LocationKind.SourceFile;

    public override TextSpan SourceSpan { get; }

    public override SyntaxTree? SyntaxTree { get; }

    public bool Equals(SourceLocation? other)
    {
        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return other != null && this.SyntaxTree == other.SyntaxTree && this.SourceSpan == other.SourceSpan;
    }

    public override bool Equals(object? obj)
    {
        return this.Equals(obj as SourceLocation);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.SyntaxTree, this.LineSpan);
    }
}

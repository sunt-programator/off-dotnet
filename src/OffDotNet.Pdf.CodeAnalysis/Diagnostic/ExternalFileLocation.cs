// <copyright file="ExternalFileLocation.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Diagnostic;

using System.Diagnostics;
using Text;

[DebuggerDisplay("{ToString(), nq}")]
internal sealed class ExternalFileLocation : Location, IEquatable<ExternalFileLocation>
{
    internal ExternalFileLocation(string filePath, TextSpan sourceSpan, LinePositionSpan lineSpan)
    {
        this.SourceSpan = sourceSpan;
        this.LineSpan = new FileLinePositionSpan(filePath, lineSpan);
    }

    /// <inheritdoc/>
    public override LocationKind Kind => LocationKind.ExternalFile;

    /// <inheritdoc/>
    public override TextSpan SourceSpan { get; }

    /// <inheritdoc/>
    public override FileLinePositionSpan LineSpan { get; }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return this.Equals(obj as ExternalFileLocation);
    }

    /// <inheritdoc/>
    public bool Equals(ExternalFileLocation? obj)
    {
        if (ReferenceEquals(obj, this))
        {
            return true;
        }

        return obj != null
               && this.SourceSpan == obj.SourceSpan
               && this.LineSpan.Equals(obj.LineSpan);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return HashCode.Combine(this.SourceSpan, this.LineSpan);
    }
}

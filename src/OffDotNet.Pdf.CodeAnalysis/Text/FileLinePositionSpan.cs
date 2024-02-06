// <copyright file="FileLinePositionSpan.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Text;

public readonly struct FileLinePositionSpan : IEquatable<FileLinePositionSpan>
{
    public FileLinePositionSpan(string path, LinePositionSpan span)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentNullException(nameof(path));
        }

        this.Path = path;
        this.Span = span;
    }

    public FileLinePositionSpan(string path, LinePosition start, LinePosition end)
        : this(path, new LinePositionSpan(start, end))
    {
    }

    public string Path { get; }

    public LinePositionSpan Span { get; }

    public LinePosition StartLinePosition => this.Span.Start;

    public LinePosition EndLinePosition => this.Span.End;

    public static bool operator ==(FileLinePositionSpan left, FileLinePositionSpan right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(FileLinePositionSpan left, FileLinePositionSpan right)
    {
        return !left.Equals(right);
    }

    /// <inheritdoc/>
    public bool Equals(FileLinePositionSpan other)
    {
        return this.Path == other.Path && this.Span.Equals(other.Span);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is FileLinePositionSpan other && this.Equals(other);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return HashCode.Combine(this.Path, this.Span);
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return this.Path + ": " + this.Span;
    }
}

// <copyright file="FileHeader.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Text;
using OffDotNet.Pdf.Core.Common;
using OffDotNet.Pdf.Core.Extensions;
using OffDotNet.Pdf.Core.Properties;

namespace OffDotNet.Pdf.Core.FileStructure;

public readonly struct FileHeader : IPdfObject, IEquatable<FileHeader>
{
    public static readonly FileHeader PdfVersion10 = new(1, 0);
    public static readonly FileHeader PdfVersion11 = new(1, 1);
    public static readonly FileHeader PdfVersion12 = new(1, 2);
    public static readonly FileHeader PdfVersion13 = new(1, 3);
    public static readonly FileHeader PdfVersion14 = new(1, 4);
    public static readonly FileHeader PdfVersion15 = new(1, 5);
    public static readonly FileHeader PdfVersion16 = new(1, 6);
    public static readonly FileHeader PdfVersion17 = new(1, 7);
    public static readonly FileHeader PdfVersion20 = new(2, 0);

    public FileHeader(int majorVersion, int minorVersion)
    {
        this.MajorVersion = majorVersion.CheckConstraints(num => num is 1 or 2, Resource.FileHeader_MajorVersionIsNotValid);
        this.MinorVersion = minorVersion.CheckConstraints(num => num is >= 0 and <= 9, Resource.FileHeader_MinorVersionIsNotValid);
    }

    public ReadOnlyMemory<byte> Bytes => Encoding.ASCII.GetBytes(this.Content);

    public int MajorVersion { get; }

    public int MinorVersion { get; }

    public string Content => $"%PDF-{this.MajorVersion}.{this.MinorVersion}\n";

    public static bool operator ==(FileHeader leftOperator, FileHeader rightOperator)
    {
        return leftOperator.Equals(rightOperator);
    }

    public static bool operator !=(FileHeader leftOperator, FileHeader rightOperator)
    {
        return !leftOperator.Equals(rightOperator);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(nameof(FileHeader), this.MajorVersion, this.MinorVersion);
    }

    public override bool Equals(object? obj)
    {
        return obj is FileHeader other && this.Equals(other);
    }

    public bool Equals(FileHeader other)
    {
        return this.MajorVersion == other.MajorVersion &&
               this.MinorVersion == other.MinorVersion;
    }
}

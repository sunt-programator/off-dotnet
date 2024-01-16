// <copyright file="LocationKind.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Diagnostic;

/// <summary>Specifies the kind of location.</summary>
public enum LocationKind : byte
{
    /// <summary>Unspecified location.</summary>
    None = 0,

    /// <summary>The location represents a position in a source file.</summary>
    SourceFile = 1,

    /// <summary>The location in some external file.</summary>
    ExternalFile = 2,
}

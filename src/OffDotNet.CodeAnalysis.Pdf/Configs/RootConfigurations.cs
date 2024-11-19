// <copyright file="RootConfigurations.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Pdf.Configs;

using OffDotNet.CodeAnalysis.Configs;

/// <summary>Represents the root configurations for the OffDotNet analysis.</summary>
public sealed record RootConfigurations
{
    /// <summary>The section name for the OffDotNet configurations.</summary>
    public const string SectionName = "OffDotNet";

    /// <summary>Gets the diagnostic options for the PDF analysis.</summary>
    public required DiagnosticOptions Diagnostic { get; init; }

    /// <summary>Gets the text cursor options for the PDF analysis.</summary>
    public required TextCursorOptions TextCursor { get; init; }
}

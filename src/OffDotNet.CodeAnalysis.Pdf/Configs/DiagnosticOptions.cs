// <copyright file="DiagnosticOptions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Pdf.Configs;

/// <summary>Represents the diagnostic options for the PDF analysis.</summary>
public sealed record DiagnosticOptions
{
    /// <summary>Gets the help link associated with the diagnostic.</summary>
    public required string HelpLink { get; init; }
}

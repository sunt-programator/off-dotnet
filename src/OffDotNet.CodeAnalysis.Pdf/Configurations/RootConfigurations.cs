// <copyright file="RootConfigurations.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Pdf.Configurations;

public sealed record RootConfigurations
{
    public const string SectionName = "OffDotNet";

    public required DiagnosticOptions Diagnostic { get; init; }
}

// <copyright file="WellKnownDiagnosticTags.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Diagnostic;

public static class WellKnownDiagnosticTags
{
    /// <summary>Indicates that the diagnostic is reported by the compiler.</summary>
    public const string Compiler = nameof(Compiler);

    /// <summary>Indicates that the diagnostic can be used for telemetry.</summary>
    public const string Telemetry = nameof(Telemetry);

    /// <summary>Indicates that the diagnostic is not configurable, i.e. it cannot be suppressed or filtered or have its severity changed.</summary>
    public const string NotConfigurable = nameof(NotConfigurable);
}

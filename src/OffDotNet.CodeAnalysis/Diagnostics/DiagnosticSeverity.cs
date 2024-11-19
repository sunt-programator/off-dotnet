// <copyright file="DiagnosticSeverity.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Diagnostics;

/// <summary>
/// Specifies the severity of a diagnostic.
/// </summary>
public enum DiagnosticSeverity
{
    /// <summary>
    /// Hidden severity, used for diagnostics that should not be shown to the user.
    /// </summary>
    Hidden = 0,

    /// <summary>
    /// Informational severity, used for diagnostics that provide information to the user.
    /// </summary>
    Info = 1,

    /// <summary>
    /// Warning severity, used for diagnostics that indicate a potential issue.
    /// </summary>
    Warning = 2,

    /// <summary>
    /// Error severity, used for diagnostics that indicate a definite issue.
    /// </summary>
    Error = 3,
}

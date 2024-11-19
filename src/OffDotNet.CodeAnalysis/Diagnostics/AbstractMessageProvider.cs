// <copyright file="AbstractMessageProvider.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Diagnostics;

using System.Collections.Concurrent;
using Microsoft.Extensions.Localization;
using DiagnosticCacheTuple = (string LanguagePrefix, ushort Code);

/// <summary>
/// Provides an abstract base class for message providers in diagnostics.
/// </summary>
internal abstract class AbstractMessageProvider : IMessageProvider
{
    /// <summary>
    /// A cache for storing diagnostic IDs.
    /// </summary>
    private static readonly ConcurrentDictionary<DiagnosticCacheTuple, string> s_cache = new();

    /// <summary>
    /// Gets the language prefix for the diagnostics.
    /// </summary>
    public abstract string LanguagePrefix { get; }

    /// <summary>
    /// Gets the localized title for the specified diagnostic code.
    /// </summary>
    /// <param name="code">The diagnostic code.</param>
    /// <returns>The localized title.</returns>
    public abstract LocalizedString GetTitle(ushort code);

    /// <summary>
    /// Gets the localized description for the specified diagnostic code.
    /// </summary>
    /// <param name="code">The diagnostic code.</param>
    /// <returns>The localized description.</returns>
    public abstract LocalizedString GetDescription(ushort code);

    /// <summary>
    /// Gets the help link for the specified diagnostic code.
    /// </summary>
    /// <param name="code">The diagnostic code.</param>
    /// <returns>The help link.</returns>
    public abstract string GetHelpLink(ushort code);

    /// <summary>
    /// Gets the severity for the specified diagnostic code.
    /// </summary>
    /// <param name="code">The diagnostic code.</param>
    /// <returns>The severity as a byte value.</returns>
    public abstract byte GetSeverity(ushort code);

    /// <summary>
    /// Gets the ID for the specified diagnostic code.
    /// </summary>
    /// <param name="diagnosticCode">The diagnostic code.</param>
    /// <returns>The ID for the diagnostic code.</returns>
    public string GetIdForDiagnosticCode(ushort diagnosticCode)
    {
        return s_cache.GetOrAdd((LanguagePrefix, diagnosticCode), key => $"{key.LanguagePrefix}{key.Code:0000}");
    }
}

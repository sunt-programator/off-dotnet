// <copyright file="DiagnosticDescriptor.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Diagnostics;

using System.Collections.Immutable;
using Microsoft.Extensions.Localization;

/// <summary>
/// Represents a diagnostic descriptor which contains information about a diagnostic.
/// </summary>
public sealed record DiagnosticDescriptor
{
    private static ImmutableDictionary<ushort, DiagnosticDescriptor> s_errorCodeToDescriptorMap = ImmutableDictionary<ushort, DiagnosticDescriptor>.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="DiagnosticDescriptor"/> class.
    /// </summary>
    internal DiagnosticDescriptor()
    {
    }

    /// <summary>
    /// Gets the ID of the diagnostic.
    /// </summary>
    public required string Id { get; init; }

    /// <summary>
    /// Gets the title of the diagnostic.
    /// </summary>
    public required LocalizedString Title { get; init; }

    /// <summary>
    /// Gets the description of the diagnostic.
    /// </summary>
    public required LocalizedString Description { get; init; }

    /// <summary>
    /// Gets the help link for the diagnostic.
    /// </summary>
    public required string HelpLink { get; init; }

    /// <summary>
    /// Gets the default severity of the diagnostic.
    /// </summary>
    public DiagnosticSeverity DefaultSeverity { get; init; }

    /// <summary>
    /// Creates a diagnostic descriptor for the specified diagnostic code using the provided message provider.
    /// </summary>
    /// <param name="diagnosticCode">The diagnostic code.</param>
    /// <param name="messageProvider">The message provider.</param>
    /// <returns>The created diagnostic descriptor.</returns>
    internal static DiagnosticDescriptor CreateDescriptor(ushort diagnosticCode, AbstractMessageProvider messageProvider)
    {
        return ImmutableInterlocked.GetOrAdd(
            location: ref s_errorCodeToDescriptorMap,
            key: diagnosticCode,
            valueFactory: static (code, messageProvider) => new DiagnosticDescriptor
            {
                Id = messageProvider.GetIdForDiagnosticCode(code),
                Title = messageProvider.GetTitle(code),
                Description = messageProvider.GetDescription(code),
                HelpLink = messageProvider.GetHelpLink(code),
                DefaultSeverity = (DiagnosticSeverity)messageProvider.GetSeverity(code),
            },
            factoryArgument: messageProvider);
    }
}

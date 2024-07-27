// <copyright file="DiagnosticDescriptor.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Diagnostics;

using System.Collections.Immutable;
using Microsoft.Extensions.Localization;

public sealed record DiagnosticDescriptor
{
    internal DiagnosticDescriptor()
    {
    }

    private static ImmutableDictionary<ushort, DiagnosticDescriptor> s_errorCodeToDescriptorMap = ImmutableDictionary<ushort, DiagnosticDescriptor>.Empty;

    public required string Id { get; init; }

    public required LocalizedString Title { get; init; }

    public required LocalizedString Description { get; init; }

    public required string HelpLink { get; init; }

    public DiagnosticSeverity DefaultSeverity { get; init; }

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

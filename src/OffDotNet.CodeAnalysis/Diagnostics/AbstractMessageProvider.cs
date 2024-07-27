// <copyright file="AbstractMessageProvider.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Diagnostics;

using System.Collections.Concurrent;
using Microsoft.Extensions.Localization;
using DiagnosticCacheTuple = (string LanguagePrefix, ushort Code);

internal abstract class AbstractMessageProvider : IMessageProvider
{
    private static readonly ConcurrentDictionary<DiagnosticCacheTuple, string> s_cache = new();

    public abstract string LanguagePrefix { get; }

    public abstract LocalizedString GetTitle(ushort code);

    public abstract LocalizedString GetDescription(ushort code);

    public abstract string GetHelpLink(ushort code);

    public abstract byte GetSeverity(ushort code);

    public string GetIdForDiagnosticCode(ushort diagnosticCode)
    {
        return s_cache.GetOrAdd((LanguagePrefix, diagnosticCode), key => $"{key.LanguagePrefix}{key.Code:0000}");
    }
}

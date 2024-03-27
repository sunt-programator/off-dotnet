﻿// <copyright file="MessageProvider.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Diagnostic;

using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Resources;

internal sealed class MessageProvider : IMessageProvider
{
    public static readonly MessageProvider Instance = new();
    private const string TitleSuffix = "_Title";
    private const string DescriptionSuffix = "_Description";
    private static readonly Lazy<ResourceManager> s_resourceManager = new(() => new ResourceManager(typeof(PDFResources).FullName!, typeof(MessageProvider).GetTypeInfo().Assembly));
    private static readonly Lazy<ImmutableDictionary<DiagnosticCode, string>> s_categoriesMap = new(CreateCategoriesMap);
    private static readonly ConcurrentDictionary<(string Prefix, DiagnosticCode Code), string> s_errorIdCache = new();

    private MessageProvider()
    {
    }

    /// <inheritdoc/>
    public string CodePrefix => "PDF";

    /// <inheritdoc/>
    public LocalizableString GetTitle(DiagnosticCode code)
    {
        return new LocalizableResourceString($"{code}{TitleSuffix}", s_resourceManager.Value, typeof(MessageProvider));
    }

    /// <inheritdoc/>
    public LocalizableString GetDescription(DiagnosticCode code)
    {
        return new LocalizableResourceString($"{code}{DescriptionSuffix}", s_resourceManager.Value, typeof(MessageProvider));
    }

    /// <inheritdoc/>
    public DiagnosticSeverity GetSeverity(DiagnosticCode code)
    {
        return DiagnosticSeverity.Error;
    }

    /// <inheritdoc/>
    public LocalizableString GetMessage(DiagnosticCode code)
    {
        return new LocalizableResourceString($"{code}", s_resourceManager.Value, typeof(MessageProvider));
    }

    /// <inheritdoc/>
    public string GetHelpLink(DiagnosticCode code)
    {
        return $"https://github.com/search?q=repo%3Asunt-programator%2Foff-dotnet%20{this.GetIdForErrorCode(code)}&type=code";
    }

    /// <inheritdoc/>
    public string GetIdForErrorCode(DiagnosticCode code)
    {
        return s_errorIdCache.GetOrAdd((this.CodePrefix, code), key => $"{key.Prefix}{(int)key.Code:0000}");
    }

    /// <inheritdoc/>
    public string GetCategory(DiagnosticCode code)
    {
        return CollectionExtensions.GetValueOrDefault(s_categoriesMap.Value, code, "Syntax");
    }

    /// <inheritdoc/>
    public bool GetIsEnabledByDefault(DiagnosticCode code)
    {
        return true;
    }

    /// <inheritdoc/>
    public string LoadMessage(DiagnosticCode code, CultureInfo culture)
    {
        var message = s_resourceManager.Value.GetString(code.ToString(), culture);
        Debug.Assert(!string.IsNullOrEmpty(message), code.ToString());
        return message;
    }

    /// <summary>Given a message identifier (e.g., PDF0001), severity, warning as error and a culture, get the entire prefix (e.g., "error PDF0001:") used on error messages.</summary>
    /// <param name="id">The message identifier.</param>
    /// <param name="severity">The severity of the diagnostic.</param>
    /// <param name="isWarningAsError">True if the diagnostic is treated as an error.</param>
    /// <param name="culture">The culture used to get the prefix.</param>
    /// <returns>The entire prefix (e.g., "error PDF0001:") used on error messages.</returns>
    public string GetMessagePrefix(string id, DiagnosticSeverity severity, bool isWarningAsError, CultureInfo culture)
    {
        return string.Format(culture, "{0} {1}", severity == DiagnosticSeverity.Error || isWarningAsError ? "error" : "warning", id);
    }

    [SuppressMessage("ReSharper", "CollectionNeverUpdated.Local", Justification = "Reviewed.")]
    private static ImmutableDictionary<DiagnosticCode, string> CreateCategoriesMap()
    {
        Dictionary<DiagnosticCode, string> map = new()
        {
            // { ERROR_CODE,    CATEGORY }
        };

        return map.ToImmutableDictionary();
    }
}

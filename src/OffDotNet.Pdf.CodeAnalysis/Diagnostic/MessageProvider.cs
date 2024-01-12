// <copyright file="MessageProvider.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace OffDotNet.Pdf.CodeAnalysis.Diagnostic;

internal sealed class MessageProvider : IMessageProvider
{
    public static readonly MessageProvider Instance = new();
    private const string TitleSuffix = "_Title";
    private const string DescriptionSuffix = "_Description";
    private static readonly Lazy<ResourceManager> ResourceManager = new(() => new ResourceManager(typeof(PDFResources).FullName!, typeof(MessageProvider).GetTypeInfo().Assembly));
    private static readonly Lazy<ImmutableDictionary<DiagnosticCode, string>> CategoriesMap = new(CreateCategoriesMap);
    private static readonly ConcurrentDictionary<(string Prefix, DiagnosticCode Code), string> ErrorIdCache = new();

    private MessageProvider()
    {
    }

    public string CodePrefix => "PDF";

    public LocalizableString GetTitle(DiagnosticCode code)
    {
        return new LocalizableResourceString($"{code}{TitleSuffix}", ResourceManager.Value, typeof(MessageProvider));
    }

    public LocalizableString GetDescription(DiagnosticCode code)
    {
        return new LocalizableResourceString($"{code}{DescriptionSuffix}", ResourceManager.Value, typeof(MessageProvider));
    }

    public DiagnosticSeverity GetSeverity(DiagnosticCode code)
    {
        return DiagnosticSeverity.Error;
    }

    public LocalizableString GetMessage(DiagnosticCode code)
    {
        return new LocalizableResourceString($"{code}", ResourceManager.Value, typeof(MessageProvider));
    }

    public string GetHelpLink(DiagnosticCode code)
    {
        return $"https://github.com/search?q=repo%3Asunt-programator%2Foff-dotnet%20{this.GetIdForErrorCode(code)}&type=code";
    }

    public string GetIdForErrorCode(DiagnosticCode code)
    {
        return ErrorIdCache.GetOrAdd((this.CodePrefix, code), key => $"{key.Prefix}{(int)key.Code:0000}");
    }

    public string GetCategory(DiagnosticCode code)
    {
        return CollectionExtensions.GetValueOrDefault(CategoriesMap.Value, code, "Syntax");
    }

    public bool GetIsEnabledByDefault(DiagnosticCode code)
    {
        return true;
    }

    public string LoadMessage(DiagnosticCode code, CultureInfo culture)
    {
        string? message = ResourceManager.Value.GetString(code.ToString(), culture);
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

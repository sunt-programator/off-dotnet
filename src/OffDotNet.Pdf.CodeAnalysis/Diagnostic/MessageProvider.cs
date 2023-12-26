// <copyright file="MessageProvider.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
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
        string id = ErrorIdCache.GetOrAdd((this.CodePrefix, code), key => $"{key.Prefix}{(int)key.Code:0000}");
        return $"https://github.com/search?q=repo%3Asunt-programator%2Foff-dotnet%20{id}&type=code";
    }

    public string GetCategory(DiagnosticCode code)
    {
        return CollectionExtensions.GetValueOrDefault(CategoriesMap.Value, code, "PDF");
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

// <copyright file="LocalizableResourceString.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Diagnostic;

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Resources;

public sealed class LocalizableResourceString : LocalizableString
{
    private readonly string _nameOfLocalizableResource;
    private readonly ResourceManager _resourceManager;
    private readonly Type _resourceSource;
    private readonly string[] _formatArguments;

    public LocalizableResourceString(string nameOfLocalizableResource, ResourceManager resourceManager, Type resourceSource, params string[] formatArguments)
    {
        _nameOfLocalizableResource = nameOfLocalizableResource;
        _resourceManager = resourceManager;
        _resourceSource = resourceSource;
        _formatArguments = formatArguments;
    }

    /// <inheritdoc/>
    [SuppressMessage("ReSharper", "CoVariantArrayConversion", Justification = "Reviewed.")]
    protected override string GetText(IFormatProvider? formatProvider)
    {
        var cultureInfo = formatProvider as CultureInfo ?? CultureInfo.CurrentUICulture;
        var resourceString = _resourceManager.GetString(_nameOfLocalizableResource, cultureInfo);

        if (resourceString == null)
        {
            return string.Empty;
        }

        return _formatArguments.Length > 0 ? string.Format(cultureInfo, resourceString, _formatArguments) : resourceString;
    }

    /// <inheritdoc/>
    protected override int GetHash()
    {
        return HashCode.Combine(_nameOfLocalizableResource, _resourceManager, _resourceSource, _formatArguments);
    }

    /// <inheritdoc/>
    protected override bool AreEqual(object? other)
    {
        return other is LocalizableResourceString otherResourceString &&
               _nameOfLocalizableResource == otherResourceString._nameOfLocalizableResource &&
               _resourceManager == otherResourceString._resourceManager &&
               _resourceSource == otherResourceString._resourceSource &&
               _formatArguments.SequenceEqual(otherResourceString._formatArguments);
    }
}

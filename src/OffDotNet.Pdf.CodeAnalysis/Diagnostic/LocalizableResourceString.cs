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
    private readonly string nameOfLocalizableResource;
    private readonly ResourceManager resourceManager;
    private readonly Type resourceSource;
    private readonly string[] formatArguments;

    public LocalizableResourceString(string nameOfLocalizableResource, ResourceManager resourceManager, Type resourceSource, params string[] formatArguments)
    {
        this.nameOfLocalizableResource = nameOfLocalizableResource;
        this.resourceManager = resourceManager;
        this.resourceSource = resourceSource;
        this.formatArguments = formatArguments;
    }

    /// <inheritdoc/>
    [SuppressMessage("ReSharper", "CoVariantArrayConversion", Justification = "Reviewed.")]
    protected override string GetText(IFormatProvider? formatProvider)
    {
        var cultureInfo = formatProvider as CultureInfo ?? CultureInfo.CurrentUICulture;
        var resourceString = this.resourceManager.GetString(this.nameOfLocalizableResource, cultureInfo);

        if (resourceString == null)
        {
            return string.Empty;
        }

        return this.formatArguments.Length > 0 ? string.Format(cultureInfo, resourceString, this.formatArguments) : resourceString;
    }

    /// <inheritdoc/>
    protected override int GetHash()
    {
        return HashCode.Combine(this.nameOfLocalizableResource, this.resourceManager, this.resourceSource, this.formatArguments);
    }

    /// <inheritdoc/>
    protected override bool AreEqual(object? other)
    {
        return other is LocalizableResourceString otherResourceString &&
               this.nameOfLocalizableResource == otherResourceString.nameOfLocalizableResource &&
               this.resourceManager == otherResourceString.resourceManager &&
               this.resourceSource == otherResourceString.resourceSource &&
               this.formatArguments.SequenceEqual(otherResourceString.formatArguments);
    }
}

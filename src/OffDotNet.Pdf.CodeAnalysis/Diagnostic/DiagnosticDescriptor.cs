﻿// <copyright file="DiagnosticDescriptor.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Diagnostic;

using System.Collections.Immutable;
using System.Diagnostics;

public sealed class DiagnosticDescriptor : IEquatable<DiagnosticDescriptor>
{
    public DiagnosticDescriptor(
        string id,
        LocalizableString title,
        LocalizableString messageFormat,
        string category,
        DiagnosticSeverity defaultSeverity,
        bool isEnabledByDefault,
        LocalizableString? description = null,
        string? helpLinkUri = null,
        params string[] customTags)
        : this(id, title, messageFormat, category, defaultSeverity, isEnabledByDefault, description, helpLinkUri, ImmutableArray.Create(customTags))
    {
    }

    internal DiagnosticDescriptor(
        string id,
        LocalizableString title,
        LocalizableString messageFormat,
        string category,
        DiagnosticSeverity defaultSeverity,
        bool isEnabledByDefault,
        LocalizableString? description,
        string? helpLinkUri,
        ImmutableArray<string> customTags)
    {
        Debug.Assert(!customTags.IsDefault, "Expected non-default customTags");

        this.Id = id;
        this.Title = title;
        this.MessageFormat = messageFormat;
        this.Category = category;
        this.DefaultSeverity = defaultSeverity;
        this.IsEnabledByDefault = isEnabledByDefault;
        this.Description = description ?? string.Empty;
        this.HelpLinkUri = helpLinkUri ?? string.Empty;
        this.CustomTags = customTags;
    }

    /// <summary> Gets the unique identifier of the diagnostic.</summary>
    public string Id { get; }

    /// <summary>Gets the short localizable title describing the diagnostic.</summary>
    public LocalizableString Title { get; }

    /// <summary>
    /// Gets the localizable message format string which can be passed as the first argument
    /// to <see cref="string.Format(string, object[])"/> when creating the diagnostic message with this descriptor.
    /// </summary>
    public LocalizableString MessageFormat { get; }

    /// <summary>Gets the category of the diagnostic.</summary>
    public string Category { get; }

    /// <summary>Gets the default severity of the diagnostic.</summary>
    public DiagnosticSeverity DefaultSeverity { get; }

    /// <summary>Gets a value indicating whether the diagnostic is enabled by default.</summary>
    public bool IsEnabledByDefault { get; }

    /// <summary>Gets an optional longer localizable description of the diagnostic.</summary>
    public LocalizableString Description { get; }

    /// <summary>Gets an optional hyperlink that provides more detailed information regarding the diagnostic.</summary>
    public string HelpLinkUri { get; }

    /// <summary>Gets the custom tags for the diagnostic.</summary>
    public ImmutableArray<string> CustomTags { get; }

    public static bool operator ==(DiagnosticDescriptor? left, DiagnosticDescriptor? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(DiagnosticDescriptor? left, DiagnosticDescriptor? right)
    {
        return !Equals(left, right);
    }

    /// <inheritdoc/>
    public bool Equals(DiagnosticDescriptor? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return this.Id == other.Id &&
               this.Title.Equals(other.Title) &&
               this.MessageFormat.Equals(other.MessageFormat) &&
               this.Category == other.Category &&
               this.DefaultSeverity == other.DefaultSeverity &&
               this.IsEnabledByDefault == other.IsEnabledByDefault &&
               this.Description.Equals(other.Description) &&
               this.HelpLinkUri == other.HelpLinkUri;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return this.Equals(obj as DiagnosticDescriptor);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        var hashCode = default(HashCode);
        hashCode.Add(this.Id);
        hashCode.Add(this.Title);
        hashCode.Add(this.MessageFormat);
        hashCode.Add(this.Category);
        hashCode.Add((int)this.DefaultSeverity);
        hashCode.Add(this.IsEnabledByDefault);
        hashCode.Add(this.Description);
        hashCode.Add(this.HelpLinkUri);
        return hashCode.ToHashCode();
    }
}

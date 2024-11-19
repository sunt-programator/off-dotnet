// <copyright file="MessageProvider.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Pdf.Diagnostics;

using Configs;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using OffDotNet.CodeAnalysis.Diagnostics;

/// <summary>
/// Provides localized messages for diagnostics in PDF analysis.
/// </summary>
internal sealed class MessageProvider : AbstractMessageProvider
{
    private const string TitleSuffix = "_Title";
    private const string DescriptionSuffix = "_Description";

    private readonly IStringLocalizer<MessageProvider> _localizer;
    private readonly DiagnosticOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="MessageProvider"/> class.
    /// </summary>
    /// <param name="localizer">The localizer for retrieving localized strings.</param>
    /// <param name="options">The diagnostic options.</param>
    public MessageProvider(IStringLocalizer<MessageProvider> localizer, IOptions<DiagnosticOptions> options)
    {
        _localizer = localizer;
        _options = options.Value ?? throw new ArgumentNullException(nameof(options));
    }

    /// <summary>
    /// Gets the language prefix for the diagnostics.
    /// </summary>
    public override string LanguagePrefix => "PDF";

    /// <summary>
    /// Gets the localized title for the specified diagnostic code.
    /// </summary>
    /// <param name="code">The diagnostic code.</param>
    /// <returns>The localized title.</returns>
    public override LocalizedString GetTitle(ushort code) => _localizer[$"{(DiagnosticCode)code}{TitleSuffix}"];

    /// <summary>
    /// Gets the localized description for the specified diagnostic code.
    /// </summary>
    /// <param name="code">The diagnostic code.</param>
    /// <returns>The localized description.</returns>
    public override LocalizedString GetDescription(ushort code) => _localizer[$"{(DiagnosticCode)code}{DescriptionSuffix}"];

    /// <summary>
    /// Gets the help link for the specified diagnostic code.
    /// </summary>
    /// <param name="code">The diagnostic code.</param>
    /// <returns>The help link.</returns>
    public override string GetHelpLink(ushort code) => string.Format(_options.HelpLink, GetIdForDiagnosticCode(code));

    /// <summary>
    /// Gets the severity for the specified diagnostic code.
    /// </summary>
    /// <param name="code">The diagnostic code.</param>
    /// <returns>The severity as a byte value.</returns>
    public override byte GetSeverity(ushort code)
    {
        switch (code)
        {
            default:
                return (byte)DiagnosticSeverity.Hidden;
        }
    }
}

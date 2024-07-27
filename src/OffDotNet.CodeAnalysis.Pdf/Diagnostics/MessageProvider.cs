// <copyright file="MessageProvider.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Pdf.Diagnostics;

using Configurations;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using OffDotNet.CodeAnalysis.Diagnostics;

internal sealed class MessageProvider : AbstractMessageProvider
{
    private const string TitleSuffix = "_Title";
    private const string DescriptionSuffix = "_Description";

    private readonly IStringLocalizer<MessageProvider> _localizer;
    private readonly DiagnosticOptions _options;

    public MessageProvider(IStringLocalizer<MessageProvider> localizer, IOptions<DiagnosticOptions> options)
    {
        _localizer = localizer;
        _options = options.Value ?? throw new ArgumentNullException(nameof(options));
    }

    public override string LanguagePrefix => "PDF";

    public override LocalizedString GetTitle(ushort code) => _localizer[$"{(DiagnosticCode)code}{TitleSuffix}"];

    public override LocalizedString GetDescription(ushort code) => _localizer[$"{(DiagnosticCode)code}{DescriptionSuffix}"];

    public override string GetHelpLink(ushort code) => string.Format(_options.HelpLink, GetIdForDiagnosticCode(code));

    public override byte GetSeverity(ushort code)
    {
        switch (code)
        {
            default:
                return (byte)DiagnosticSeverity.Hidden;
        }
    }
}

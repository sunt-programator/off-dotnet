// <copyright file="IMessageProvider.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Diagnostic;

using System.Globalization;

internal interface IMessageProvider
{
    string CodePrefix { get; }

    LocalizableString GetTitle(DiagnosticCode code);

    LocalizableString GetDescription(DiagnosticCode code);

    DiagnosticSeverity GetSeverity(DiagnosticCode code);

    LocalizableString GetMessage(DiagnosticCode code);

    string GetHelpLink(DiagnosticCode code);

    string GetCategory(DiagnosticCode code);

    string GetIdForErrorCode(DiagnosticCode code);

    bool GetIsEnabledByDefault(DiagnosticCode code);

    string GetMessagePrefix(string id, DiagnosticSeverity severity, bool isWarningAsError, CultureInfo culture);

    string LoadMessage(DiagnosticCode code, CultureInfo culture);
}

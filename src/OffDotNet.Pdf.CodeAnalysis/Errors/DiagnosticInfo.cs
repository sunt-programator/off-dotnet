// <copyright file="DiagnosticInfo.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Globalization;

namespace OffDotNet.Pdf.CodeAnalysis.Errors;

internal class DiagnosticInfo : IFormattable
{
    internal DiagnosticInfo(ErrorCode code)
    {
        this.Code = code;
        this.Severity = code.GetSeverity();
    }

    public ErrorCode Code { get; }

    public string MessageIdentifier => $"PDF{(int)this.Code:D4}";

    public DiagnosticSeverity Severity { get; }

    public string ToString(IFormatProvider? formatProvider)
    {
        return ((IFormattable)this).ToString(null, formatProvider);
    }

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        CultureInfo culture = formatProvider as CultureInfo ?? CultureInfo.CurrentCulture;
        string messagePrefix = string.Format(culture, "{0} {1}", this.Severity == DiagnosticSeverity.Error ? "error" : "warning", this.MessageIdentifier);
        string message = ErrorFacts.GetMessage(this.Code, culture);
        return string.Format(formatProvider, "{0}: {1}", messagePrefix, message);
    }
}

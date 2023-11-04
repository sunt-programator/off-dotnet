// <copyright file="ErrorFacts.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Globalization;

namespace OffDotNet.Pdf.CodeAnalysis.Old.Errors;

internal static class ErrorFacts
{
    internal static DiagnosticSeverity GetSeverity(this ErrorCode code)
    {
        return code switch
        {
            _ => DiagnosticSeverity.Error,
        };
    }

    internal static string GetMessage(ErrorCode code, CultureInfo culture)
    {
        return Resources.ResourceManager.GetString(code.ToString(), culture) ?? string.Empty;
    }
}

// <copyright file="PdfSyntaxExtensions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Immutable;

namespace OffDotNet.Pdf.Syntax.Tests.Parsing;

public static class PdfSyntaxExtensions
{
    internal static ImmutableArray<DiagnosticInfo> Errors(this SyntaxToken token)
    {
        return Enumerable.Empty<DiagnosticInfo>().ToImmutableArray();
    }
}

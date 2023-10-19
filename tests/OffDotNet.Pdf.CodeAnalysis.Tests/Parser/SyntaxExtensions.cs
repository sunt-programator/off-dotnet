// <copyright file="SyntaxExtensions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Immutable;
using OffDotNet.Pdf.CodeAnalysis.Errors;
using OffDotNet.Pdf.CodeAnalysis.Syntax;

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Parser;

public static class SyntaxExtensions
{
    internal static ImmutableArray<DiagnosticInfo> Errors(this SyntaxToken token)
    {
        return token.ErrorsOrWarnings(errorsOnly: true);
    }

    private static ImmutableArray<DiagnosticInfo> ErrorsOrWarnings(this SyntaxToken node, bool errorsOnly)
    {
        return node.GetDiagnostics()
            .Where(x => x.Severity == (errorsOnly ? DiagnosticSeverity.Error : DiagnosticSeverity.Warning))
            .ToImmutableArray();
    }
}

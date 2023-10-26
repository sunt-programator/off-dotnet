// <copyright file="Lexer.ErrorHelpers.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Errors;

namespace OffDotNet.Pdf.CodeAnalysis.Lexer;

internal partial class Lexer
{
    private static DiagnosticInfo MakeError(ErrorCode code)
    {
        return new DiagnosticInfo(code);
    }

    private void AddError(DiagnosticInfo? error)
    {
        if (error == null)
        {
            return;
        }

        this.errors ??= new List<DiagnosticInfo>(8);
        this.errors.Add(error);
    }
}

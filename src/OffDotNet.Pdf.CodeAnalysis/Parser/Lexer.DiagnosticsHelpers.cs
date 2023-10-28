// <copyright file="Lexer.DiagnosticsHelpers.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Errors;

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

internal partial class Lexer
{
    private static SyntaxDiagnosticInfo MakeError(ErrorCode code)
    {
        return new SyntaxDiagnosticInfo(code);
    }

    private void AddError(SyntaxDiagnosticInfo? error)
    {
        if (error == null)
        {
            return;
        }

        this.errors ??= new List<SyntaxDiagnosticInfo>(8);
        this.errors.Add(error);
    }

    private SyntaxDiagnosticInfo[]? GetErrors(int leadingTriviaWidth)
    {
        if (this.errors == null)
        {
            return null;
        }

        if (leadingTriviaWidth <= 0)
        {
            return this.errors.ToArray();
        }

        var array = new SyntaxDiagnosticInfo[this.errors.Count];
        for (int i = 0; i < this.errors.Count; i++)
        {
            array[i] = this.errors[i].WithOffset(this.errors[i].Offset + leadingTriviaWidth);
        }

        return array;
    }
}

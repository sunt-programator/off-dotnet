// <copyright file="SyntaxList.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Diagnostic;

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

internal abstract partial class SyntaxList : GreenNode
{
    private SyntaxList(DiagnosticInfo[]? diagnostics = null)
        : base(SyntaxKind.List, diagnostics)
    {
    }
}

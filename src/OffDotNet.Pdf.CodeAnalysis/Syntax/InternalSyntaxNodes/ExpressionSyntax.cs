﻿// <copyright file="ExpressionSyntax.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

using Diagnostic;

internal abstract class ExpressionSyntax : GreenNode
{
    protected ExpressionSyntax(SyntaxKind kind, DiagnosticInfo[]? diagnostics = null)
        : base(kind, diagnostics)
    {
    }
}

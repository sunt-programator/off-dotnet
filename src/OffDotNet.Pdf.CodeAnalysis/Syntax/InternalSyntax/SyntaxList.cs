// <copyright file="SyntaxList.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

using Diagnostic;

internal abstract partial class SyntaxList : GreenNode
{
    private SyntaxList(DiagnosticInfo[]? diagnostics = null)
        : base(SyntaxKind.List, diagnostics)
    {
    }

    /// <summary>Represents the enumerators for the green node.</summary>
    /// <returns>The enumerator for the green node.</returns>
    public override Enumerator GetEnumerator()
    {
        return new Enumerator(this);
    }
}

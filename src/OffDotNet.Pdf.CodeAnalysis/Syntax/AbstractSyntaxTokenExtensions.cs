// <copyright file="AbstractSyntaxTokenExtensions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Errors;

namespace OffDotNet.Pdf.CodeAnalysis.Syntax;

internal static class AbstractSyntaxTokenExtensions
{
    public static TNode WithDiagnostics<TNode>(this TNode node, DiagnosticInfo[]? diagnostics)
        where TNode : AbstractSyntaxToken
    {
        return (TNode)node.SetDiagnostics(diagnostics);
    }
}

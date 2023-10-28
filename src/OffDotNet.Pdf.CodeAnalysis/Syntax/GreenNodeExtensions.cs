// <copyright file="GreenNodeExtensions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Errors;

namespace OffDotNet.Pdf.CodeAnalysis.Syntax;

internal static class GreenNodeExtensions
{
    public static TNode WithDiagnosticsGreen<TNode>(this TNode node, DiagnosticInfo[]? diagnostics)
        where TNode : GreenNode
    {
        return (TNode)node.SetDiagnostics(diagnostics);
    }
}

// <copyright file="GreenNodeExtensions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Old.Errors;

namespace OffDotNet.Pdf.CodeAnalysis.Old.Syntax;

internal static class GreenNodeExtensions
{
    public static TNode WithDiagnosticsGreen<TNode>(this TNode node, DiagnosticInfo[]? diagnostics)
        where TNode : GreenNode
    {
        return (TNode)node.SetDiagnostics(diagnostics);
    }
}

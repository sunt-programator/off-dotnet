// <copyright file="GreenNode.Diagnostics.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

using System.Runtime.CompilerServices;
using Diagnostic;

/// <summary>
/// The additional class containing diagnostics-related methods and properties for <see cref="GreenNode"/>.
/// </summary>
internal abstract partial class GreenNode
{
    private static readonly ConditionalWeakTable<GreenNode, DiagnosticInfo[]> DiagnosticsTable = new();
    private static readonly DiagnosticInfo[] NoDiagnostics = Array.Empty<DiagnosticInfo>();

    /// <summary>Gets the diagnostics associated with this node.</summary>
    /// <returns>The diagnostics associated with this node.</returns>
    public DiagnosticInfo[] GetDiagnostics()
    {
        if (!this.ContainsFlags(NodeFlags.ContainsDiagnostics))
        {
            return NoDiagnostics;
        }

        return DiagnosticsTable.TryGetValue(this, out var diagnostics) ? diagnostics : NoDiagnostics;
    }

    /// <summary>Sets the diagnostics associated with this node.</summary>
    /// <param name="diagnostics">The diagnostics to be set.</param>
    /// <returns>The new node with the specified diagnostics.</returns>
    internal abstract GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics);
}

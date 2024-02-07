// <copyright file="GreenNode.Flags.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Additional class containing flag-related methods and properties for <see cref="GreenNode"/>.
/// </summary>
internal abstract partial class GreenNode
{
    [Flags]
    [SuppressMessage("Minor Code Smell", "S2344:Enumeration type names should not have \"Flags\" or \"Enum\" suffixes", Justification = "This is a flags enum.")]
    internal enum NodeFlags : byte
    {
        /// <summary>None.</summary>
        None = 0,

        /// <summary>Contains diagnostics.</summary>
        ContainsDiagnostics = 1 << 0,
    }

    /// <summary>Gets the node flags.</summary>
    public NodeFlags Flags { get; private set; }

    /// <summary>Sets the <see cref="Flags"/> property.</summary>
    /// <param name="flags">The flags to be set.</param>
    public void SetFlags(NodeFlags flags)
    {
        this.Flags |= flags;
    }

    /// <summary>Clears the <see cref="Flags"/> property.</summary>
    /// <param name="flags">The flags to be set.</param>
    public void ClearFlags(NodeFlags flags)
    {
        this.Flags &= ~flags;
    }

    /// <summary>Checks if the <see cref="Flags"/> property contains the specified flags.</summary>
    /// <param name="flags">The flags to be checked.</param>
    /// <returns>True if the <see cref="Flags"/> property contains the specified flags, false otherwise.</returns>
    public bool ContainsFlags(NodeFlags flags)
    {
        return (this.Flags & flags) != 0;
    }
}

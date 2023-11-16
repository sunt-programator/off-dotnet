// <copyright file="GreenNode.Slots.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

/// <summary>
/// Additional class containing slot-related methods and properties for <see cref="GreenNode"/>.
/// </summary>
internal abstract partial class GreenNode
{
    /// <summary>Gets or sets the number of slots in a <see cref="GreenNode"/>.</summary>
    public int SlotCount { get; protected set; }

    /// <summary>Gets the slot at the specified index within a <see cref="GreenNode"/>.</summary>
    /// <param name="index">The index of the slot.</param>
    /// <returns>The slot at the specified index within a <see cref="GreenNode"/>.</returns>
    internal abstract GreenNode? GetSlot(int index);
}

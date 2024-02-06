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
    private readonly byte slotCount;

    /// <summary>Gets the number of slots in a <see cref="GreenNode"/>.</summary>
    /// <remarks>
    /// For performance reasons, if the <see cref="SlotCount"/> is less than 255, the <see cref="byte"/> field is used, otherwise it falls back to <see cref="GetSlotCount"/> method.
    /// </remarks>
    public int SlotCount
    {
        get => this.slotCount == byte.MaxValue
            ? this.GetSlotCount()
            : this.slotCount;

        protected init => this.slotCount = (byte)value;
    }

    /// <summary>Gets the offset of the slot at the specified index within a <see cref="GreenNode"/>.</summary>
    /// <param name="index">The index of the slot.</param>
    /// <returns>The offset of the slot at the specified index within a <see cref="GreenNode"/>.</returns>
    public virtual int GetSlotOffset(int index)
    {
        var offset = 0;

        for (var i = 0; i < index; i++)
        {
            var child = this.GetSlot(i);
            if (child is not null)
            {
                offset += child.FullWidth;
            }
        }

        return offset;
    }

    /// <summary>Gets the slot at the specified index within a <see cref="GreenNode"/>.</summary>
    /// <param name="index">The index of the slot.</param>
    /// <returns>The slot at the specified index within a <see cref="GreenNode"/>.</returns>
    internal abstract GreenNode? GetSlot(int index);

    /// <summary>Gets the number of slots in a <see cref="GreenNode"/>.</summary>
    /// <returns>The number of slots in a <see cref="GreenNode"/>.</returns>
    protected virtual int GetSlotCount()
    {
        return this.slotCount;
    }
}

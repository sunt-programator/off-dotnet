// <copyright file="GreenNode.Slots.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

using System.Collections;

/// <summary>
/// Additional class containing slot-related methods and properties for <see cref="GreenNode"/>.
/// </summary>
internal abstract partial class GreenNode : IReadOnlyList<GreenNode>
{
    private readonly byte _slotCount;

    /// <summary>Gets the number of slots in a <see cref="GreenNode"/>.</summary>
    /// <remarks>
    /// For performance reasons, if the <see cref="Count"/> is less than 255, the <see cref="byte"/> field is used, otherwise it falls back to <see cref="GetSlotCount"/> method.
    /// </remarks>
    public int Count
    {
        get => _slotCount == byte.MaxValue
            ? this.GetSlotCount()
            : _slotCount;

        protected init => _slotCount = (byte)value;
    }

    /// <summary>Gets the slot at the specified index within a <see cref="GreenNode"/>.</summary>
    /// <param name="index">The index of the slot.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the index is out of range.</exception>
    public GreenNode this[int index] => Kind != SyntaxKind.List || index < 0 || index >= Count
        ? throw new ArgumentOutOfRangeException(nameof(index))
        : GetSlot(index)!;

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

    /// <summary>Represents the enumerators for the green node.</summary>
    /// <returns>The enumerator for the green node.</returns>
    public Enumerator GetEnumerator()
    {
        return new Enumerator(this);
    }

    /// <summary>Represents the enumerators for the green node.</summary>
    /// <returns>The enumerator for the green node.</returns>
    IEnumerator<GreenNode> IEnumerable<GreenNode>.GetEnumerator()
    {
        return new EnumeratorImpl(this);
    }

    /// <summary>Represents the enumerators for the green node.</summary>
    /// <returns>The enumerator for the green node.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return new EnumeratorImpl(this);
    }

    /// <summary>Gets the slot at the specified index within a <see cref="GreenNode"/>.</summary>
    /// <param name="index">The index of the slot.</param>
    /// <returns>The slot at the specified index within a <see cref="GreenNode"/>.</returns>
    internal abstract GreenNode? GetSlot(int index);

    /// <summary>Gets the number of slots in a <see cref="GreenNode"/>.</summary>
    /// <returns>The number of slots in a <see cref="GreenNode"/>.</returns>
    protected virtual int GetSlotCount()
    {
        return _slotCount;
    }
}

// <copyright file="AbstractNode.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Syntax;

using Utils;

[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
internal abstract class AbstractNode
{
    internal const int NoneKind = 0;
    internal const int ListKind = 1;

    protected const int SlotCountTooLarge = 0b0000000000001111;

    private NodeFlagsAndSlotCount _nodeFlagsAndSlotCount;

    protected AbstractNode(ushort rawKind)
    {
        RawKind = rawKind;
    }

    protected AbstractNode(ushort rawKind, int fullWidth)
    {
        RawKind = rawKind;
        FullWidth = fullWidth;
    }

    public ushort RawKind { get; }

    public abstract string KindText { get; }

    public virtual bool IsToken { get; }

    public virtual bool IsTrivia => false;

    public bool IsList => RawKind == ListKind;

    public bool HasLeadingTrivia => LeadingTriviaWidth != 0;

    public bool HasTrailingTrivia => LeadingTriviaWidth != 0;

    public int FullWidth { get; }

    public virtual int Width => FullWidth - LeadingTriviaWidth - TrailingTriviaWidth;

    public virtual Option<object> Value => default;

    public virtual string ValueText => string.Empty;

    public virtual Option<AbstractNode> LeadingTrivia => default;

    public virtual Option<AbstractNode> TrailingTrivia => default;

    public virtual int LeadingTriviaWidth => this.FullWidth != 0 && this.GetFirstTerminal().IsSome(out var firstTerminal)
        ? firstTerminal.LeadingTriviaWidth
        : 0;

    public virtual int TrailingTriviaWidth => this.FullWidth != 0 && this.GetLastTerminal().IsSome(out var lastTerminal)
        ? lastTerminal.TrailingTriviaWidth
        : 0;

    public int SlotCount
    {
        get
        {
            var count = _nodeFlagsAndSlotCount.SmallSlotCount;
            return count == SlotCountTooLarge ? GetSlotCount() : count;
        }

        protected init
        {
            Debug.Assert(value <= byte.MaxValue, "Slot count is too large.");
            _nodeFlagsAndSlotCount.SmallSlotCount = (byte)value;
        }
    }

    internal abstract Option<AbstractNode> GetSlot(int index);

    protected virtual int GetSlotCount() => 0;

    public virtual int GetSlotOffset(int index)
    {
        var offset = 0;
        for (var i = 0; i < index; i++)
        {
            if (this.GetSlot(i).IsSome(out var slot))
            {
                offset += slot.FullWidth;
            }
        }

        return offset;
    }

    private Option<AbstractNode> GetFirstTerminal()
    {
        Option<AbstractNode> node = this;

        do
        {
            Option<AbstractNode> firstChild = default;
            _ = node.IsSome(out var nodeValue);
            Debug.Assert(nodeValue != null, "Node is null.");

            for (int i = 0, n = nodeValue.SlotCount; i < n; i++)
            {
                if (nodeValue.GetSlot(i).IsSome(out var child))
                {
                    firstChild = child;
                    break;
                }
            }

            node = firstChild;
        } while (node.IsSome(out var nd) && nd._nodeFlagsAndSlotCount.SmallSlotCount > 0);

        return node;
    }

    private Option<AbstractNode> GetLastTerminal()
    {
        Option<AbstractNode> node = this;

        do
        {
            Option<AbstractNode> lastChild = default;
            _ = node.IsSome(out var nodeValue);
            Debug.Assert(nodeValue != null, "Node is null.");

            for (var i = nodeValue.SlotCount - 1; i >= 0; i--)
            {
                if (nodeValue.GetSlot(i).IsSome(out var child))
                {
                    lastChild = child;
                    break;
                }
            }

            node = lastChild;
        } while (node.IsSome(out var nd) && nd._nodeFlagsAndSlotCount.SmallSlotCount > 0);

        return node;
    }

    private string GetDebuggerDisplay()
    {
        return this.GetType().Name + " " + this.KindText + " " + this;
    }

    [StructLayout(LayoutKind.Auto)]
    private struct NodeFlagsAndSlotCount
    {
        private const ushort SlotCountMask = 0b1111000000000000;
        private const ushort NodeFlagsMask = 0b0000111111111111;
        private const int SlotCountShift = 12;

        private ushort _data;

        public byte SmallSlotCount
        {
            readonly get
            {
                var shifted = _data >> SlotCountShift;
                Debug.Assert(shifted <= SlotCountTooLarge, "Slot count is too large.");
                return (byte)shifted;
            }

            set
            {
                if (value > SlotCountTooLarge)
                {
                    value = SlotCountTooLarge;
                }

                _data = (ushort)((_data & NodeFlagsMask) | (value << SlotCountShift));
            }
        }
    }
}

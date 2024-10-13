// <copyright file="AbstractNode.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Syntax;

using PooledObjects;
using Utils;
using AbstractNodesCacheTuple = (AbstractNode Node, bool Leading, bool Trailing);

/// <summary>Represents the abstract base class for all terminal and non-terminal nodes in the syntax tree.</summary>
[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
internal abstract class AbstractNode
{
    /// <summary>Represents a kind of node that does not exist.</summary>
    internal const int NoneKind = 0;

    /// <summary>Represents a kind of node that is a list.</summary>
    internal const int ListKind = 1;

    /// <summary>Indicates that the slot count is too large.</summary>
    private const int SlotCountTooLarge = 0b0000000000001111;

    private readonly NodeFlagsAndSlotCount _nodeFlagsAndSlotCount;

    /// <summary>Initializes a new instance of the <see cref="AbstractNode"/> class with the specified raw kind.</summary>
    /// <param name="rawKind">The raw kind of the node.</param>
    protected AbstractNode(ushort rawKind)
    {
        RawKind = rawKind;
    }

    /// <summary>Initializes a new instance of the <see cref="AbstractNode"/> class with the specified raw kind and full width.</summary>
    /// <param name="rawKind">The raw kind of the node.</param>
    /// <param name="fullWidth">The full width of the node.</param>
    protected AbstractNode(ushort rawKind, int fullWidth)
    {
        RawKind = rawKind;
        FullWidth = fullWidth;
    }

    /// <summary>Gets the raw kind of the node.</summary>
    public ushort RawKind { get; }

    /// <summary>Gets the kind text of the node.</summary>
    public abstract string KindText { get; }

    /// <summary>Gets a value indicating whether the node is a token.</summary>
    public virtual bool IsToken => false;

    /// <summary>Gets a value indicating whether the node is trivia.</summary>
    public virtual bool IsTrivia => false;

    /// <summary>Gets a value indicating whether the node is a list.</summary>
    public bool IsList => RawKind == ListKind;

    /// <summary>Gets a value indicating whether the node has leading trivia.</summary>
    public bool HasLeadingTrivia => LeadingTriviaWidth != 0;

    /// <summary>Gets a value indicating whether the node has trailing trivia.</summary>
    public bool HasTrailingTrivia => TrailingTriviaWidth != 0;

    /// <summary>Gets the full width of the node.</summary>
    public int FullWidth { get; protected init; }

    /// <summary>Gets the width of the node, excluding leading and trailing trivia.</summary>
    public virtual int Width => FullWidth - LeadingTriviaWidth - TrailingTriviaWidth;

    /// <summary>Gets the value of the node.</summary>
    public virtual Option<object> Value => default;

    /// <summary>Gets the text value of the node.</summary>
    public virtual string ValueText => string.Empty;

    /// <summary>Gets the leading trivia of the node.</summary>
    public virtual Option<AbstractNode> LeadingTrivia => default;

    /// <summary>Gets the trailing trivia of the node.</summary>
    public virtual Option<AbstractNode> TrailingTrivia => default;

    /// <summary>Gets the width of the leading trivia.</summary>
    public virtual int LeadingTriviaWidth => this.FullWidth != 0 && this.GetFirstTerminal().TryGetValue(out var firstTerminal)
        ? firstTerminal.LeadingTriviaWidth
        : 0;

    /// <summary>Gets the width of the trailing trivia.</summary>
    public virtual int TrailingTriviaWidth => this.FullWidth != 0 && this.GetLastTerminal().TryGetValue(out var lastTerminal)
        ? lastTerminal.TrailingTriviaWidth
        : 0;

    /// <summary>Gets the number of slots in the node.</summary>
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

    /// <summary>Gets the offset of the specified slot.</summary>
    /// <param name="index">The index of the slot.</param>
    /// <returns>The offset of the slot.</returns>
    public virtual int GetSlotOffset(int index)
    {
        var offset = 0;
        for (var i = 0; i < index; i++)
        {
            if (this.GetSlot(i).TryGetValue(out var slot))
            {
                offset += slot.FullWidth;
            }
        }

        return offset;
    }

    /// <summary>Returns a string that represents the current object.</summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString()
    {
        var stringBuilder = SharedObjectPools.StringBuilderPool.Get();
        using var writer = new StringWriter(stringBuilder, CultureInfo.InvariantCulture);

        WriteTo(writer, leading: false, trailing: false);

        var result = stringBuilder.ToString();
        SharedObjectPools.StringBuilderPool.Return(stringBuilder);
        return result;
    }

    /// <summary>Returns a full string representation of the node, including leading and trailing trivia.</summary>
    /// <returns>A full string representation of the node.</returns>
    public virtual string ToFullString()
    {
        var stringBuilder = SharedObjectPools.StringBuilderPool.Get();
        using var writer = new StringWriter(stringBuilder, CultureInfo.InvariantCulture);

        WriteTo(writer, leading: true, trailing: true);

        var result = stringBuilder.ToString();
        SharedObjectPools.StringBuilderPool.Return(stringBuilder);
        return result;
    }

    /// <summary>Gets the slot at the specified index.</summary>
    /// <param name="index">The index of the slot.</param>
    /// <returns>The slot at the specified index.</returns>
    internal abstract Option<AbstractNode> GetSlot(int index);

    /// <summary>Writes the node to the specified writer.</summary>
    /// <param name="writer">The writer to which the node will be written.</param>
    /// <param name="leading">Indicates whether to include leading trivia.</param>
    /// <param name="trailing">Indicates whether to include trailing trivia.</param>
    [SuppressMessage("Minor Code Smell", "S3626:Jump statements should not be redundant", Justification = "False positive.")]
    protected internal void WriteTo(TextWriter writer, bool leading, bool trailing)
    {
        var stack = SharedObjectPools.AbstractNodesCache.Get();
        stack.Push((this, leading, trailing));

        ProcessStack(writer, stack);
        SharedObjectPools.AbstractNodesCache.Return(stack);
        return;

        static void ProcessStack(TextWriter writer, Stack<AbstractNodesCacheTuple> stack)
        {
            while (stack.Count > 0)
            {
                var currentTuple = stack.Pop();

                if (currentTuple.Node.IsTrivia)
                {
                    currentTuple.Node.WriteTriviaTo(writer);
                    continue;
                }

                if (currentTuple.Node.IsToken)
                {
                    currentTuple.Node.WriteTokenTo(writer, currentTuple.Leading, currentTuple.Trailing);
                }

                // TODO: handle nested nodes
            }
        }
    }

    /// <summary>Gets the number of slots in the node.</summary>
    /// <returns>The number of slots.</returns>
    protected virtual int GetSlotCount() => 0;

    /// <summary>Writes the trivia of the node to the specified writer.</summary>
    /// <param name="writer">The writer to which the trivia will be written.</param>
    protected virtual void WriteTriviaTo(TextWriter writer)
    {
        Debug.Fail("Should not get here.");
    }

    /// <summary>Writes the token of the node to the specified writer.</summary>
    /// <param name="writer">The writer to which the token will be written.</param>
    /// <param name="leading">Indicates whether to include leading trivia.</param>
    /// <param name="trailing">Indicates whether to include trailing trivia.</param>
    protected virtual void WriteTokenTo(TextWriter writer, bool leading, bool trailing)
    {
        Debug.Fail("Should not get here.");
    }

    /// <summary>Gets the first terminal node in the tree.</summary>
    /// <returns>The first terminal node.</returns>
    private Option<AbstractNode> GetFirstTerminal()
    {
        Option<AbstractNode> node = this;

        do
        {
            Option<AbstractNode> firstChild = default;
            _ = node.TryGetValue(out var nodeValue);
            Debug.Assert(nodeValue != null, "Node is null.");

            for (int i = 0, n = nodeValue.SlotCount; i < n; i++)
            {
                if (nodeValue.GetSlot(i).TryGetValue(out var child))
                {
                    firstChild = child;
                    break;
                }
            }

            node = firstChild;
        } while (node.TryGetValue(out var nd) && nd._nodeFlagsAndSlotCount.SmallSlotCount > 0);

        return node;
    }

    /// <summary>Gets the last terminal node in the tree.</summary>
    /// <returns>The last terminal node.</returns>
    private Option<AbstractNode> GetLastTerminal()
    {
        Option<AbstractNode> node = this;

        do
        {
            Option<AbstractNode> lastChild = default;
            _ = node.TryGetValue(out var nodeValue);
            Debug.Assert(nodeValue != null, "Node is null.");

            for (var i = nodeValue.SlotCount - 1; i >= 0; i--)
            {
                if (nodeValue.GetSlot(i).TryGetValue(out var child))
                {
                    lastChild = child;
                    break;
                }
            }

            node = lastChild;
        } while (node.TryGetValue(out var nd) && nd._nodeFlagsAndSlotCount.SmallSlotCount > 0);

        return node;
    }

    /// <summary>Gets the debugger display string for the node.</summary>
    /// <returns>The debugger display string.</returns>
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

        /// <summary>
        /// Gets or sets the small slot count.
        /// </summary>
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

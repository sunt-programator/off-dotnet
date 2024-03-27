// <copyright file="SyntaxListBuilder.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

using System.Diagnostics;
using Collections;

internal sealed class SyntaxListBuilder
{
    private const int DefaultCapacity = 8;
    private ArrayElement<GreenNode?>[] _nodes;

    public SyntaxListBuilder(int size)
    {
        _nodes = new ArrayElement<GreenNode?>[size];
    }

    public int Count { get; private set; }

    public GreenNode? this[int index]
    {
        get => _nodes[index];
    }

    public static SyntaxListBuilder Create()
    {
        return new SyntaxListBuilder(DefaultCapacity);
    }

    public SyntaxListBuilder Add(GreenNode? item)
    {
        if (item == null)
        {
            return this;
        }

        if (item.Kind != SyntaxKind.List)
        {
            this.EnsureAdditionalCapacity(1);
            _nodes[this.Count++]._value = item;
            return this;
        }

        this.EnsureAdditionalCapacity(item.SlotCount); // Necessary, but not sufficient (e.g. for nested lists).
        for (var i = 0; i < item.SlotCount; i++)
        {
            this.Add(item.GetSlot(i));
        }

        return this;
    }

    public SyntaxListBuilder AddRange(GreenNode[] items)
    {
        return this.AddRange(items, 0, items.Length);
    }

    public SyntaxListBuilder AddRange(GreenNode[] items, int offset, int length)
    {
        if (offset < 0 || offset >= items.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(offset), "The offset must be greater than or equal to zero and less than the length of the items array.");
        }

        var offsetAndLength = offset + length;
        if (length < 0 || offsetAndLength > items.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(length), "The length must be greater than or equal to zero and less than or equal to the length of the items array.");
        }

        this.EnsureAdditionalCapacity(length - offset);
        var oldCount = this.Count;

        for (var i = offset; i < offsetAndLength; i++)
        {
            this.Add(items[i]);
        }

        this.Validate(oldCount, this.Count);
        return this;
    }

    public SyntaxListBuilder AddRange(SyntaxList<GreenNode> items)
    {
        this.AddRange(items, 0, items.Count);
        return this;
    }

    public SyntaxListBuilder AddRange(SyntaxList<GreenNode> items, int offset, int length)
    {
        if (offset < 0 || offset >= items.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(offset), "The offset must be greater than or equal to zero and less than the length of the items array.");
        }

        var offsetAndLength = offset + length;
        if (length < 0 || offsetAndLength > items.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(length), "The length must be greater than or equal to zero and less than or equal to the length of the items array.");
        }

        this.EnsureAdditionalCapacity(length - offset);
        var oldCount = this.Count;

        for (var i = offset; i < offsetAndLength; i++)
        {
            this.Add(items[i]);
        }

        this.Validate(oldCount, this.Count);
        return this;
    }

    public SyntaxListBuilder AddRange<TNode>(SyntaxList<TNode> items)
        where TNode : GreenNode
    {
        this.AddRange(items, 0, items.Count);
        return this;
    }

    public SyntaxListBuilder AddRange<TNode>(SyntaxList<TNode> items, int offset, int length)
        where TNode : GreenNode
    {
        this.AddRange(new SyntaxList<GreenNode>(items.Node), offset, length);
        return this;
    }

    public SyntaxListBuilder Clear()
    {
        this.Count = 0;
        return this;
    }

    public void RemoveLast()
    {
        this.Count--;
        _nodes[this.Count]._value = null;
    }

    public bool Any(SyntaxKind kind)
    {
        for (var i = 0; i < this.Count; i++)
        {
            if (_nodes[i]._value!.Kind == kind)
            {
                return true;
            }
        }

        return false;
    }

    public GreenNode[] ToArray()
    {
        var array = new GreenNode[this.Count];
        for (var i = 0; i < this.Count; i++)
        {
            array[i] = _nodes[i]._value!;
        }

        return array;
    }

    public SyntaxList<GreenNode> ToList()
    {
        return new SyntaxList<GreenNode>(this.ToListNode());
    }

    public SyntaxList<TNode> ToList<TNode>()
        where TNode : GreenNode
    {
        return new SyntaxList<TNode>(this.ToListNode());
    }

    public GreenNode? ToListNode()
    {
        switch (this.Count)
        {
            case 0:
                return null;
            case 1:
                return _nodes[0]._value;
            case 2:
                return SyntaxFactory.List(_nodes[0]._value!, _nodes[1]._value!);
            case 3:
                return SyntaxFactory.List(_nodes[0]._value!, _nodes[1]._value!, _nodes[2]._value!);
            default:
                var tmp = new ArrayElement<GreenNode>[this.Count];
                Array.Copy(_nodes, tmp, this.Count);
                return SyntaxFactory.List(tmp);
        }
    }

    private static int GetNewSize(int requiredSize, int currentSize)
    {
        if (requiredSize < DefaultCapacity)
        {
            return DefaultCapacity;
        }

        return requiredSize >= Array.MaxLength / 2
            ? Array.MaxLength
            : Math.Max(requiredSize, currentSize * 2);
    }

    private void EnsureAdditionalCapacity(int additionalCount)
    {
        var currentSize = _nodes.Length;
        var requiredSize = this.Count + additionalCount;

        if (requiredSize <= currentSize)
        {
            return;
        }

        var newSize = GetNewSize(requiredSize, currentSize);
        Debug.Assert(newSize >= requiredSize, "The new size must be greater than or equal to the required size.");
        Array.Resize(ref _nodes, newSize);
    }

    [Conditional("DEBUG")]
    private void Validate(int start, int end)
    {
        for (var i = start; i < end; i++)
        {
            Debug.Assert(_nodes[i]._value != null, "The node must not be null.");
        }
    }
}

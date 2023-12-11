// <copyright file="SyntaxListBuilder.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Diagnostics;
using OffDotNet.Pdf.CodeAnalysis.Collections;

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

internal sealed class SyntaxListBuilder
{
    private const int DefaultCapacity = 8;
    private ArrayElement<GreenNode?>[] nodes;

    public SyntaxListBuilder(int size)
    {
        this.nodes = new ArrayElement<GreenNode?>[size];
    }

    public int Count { get; private set; }

    public GreenNode? this[int index]
    {
        get => this.nodes[index];
    }

    public static SyntaxListBuilder Create()
    {
        return new SyntaxListBuilder(DefaultCapacity);
    }

    public void Add(GreenNode? item)
    {
        this.EnsureAdditionalCapacity(1);
        this.nodes[this.Count++].Value = item;
    }

    public void AddRange(GreenNode[] items)
    {
        this.AddRange(items, 0, items.Length);
    }

    public void AddRange(GreenNode[] items, int offset, int length)
    {
        if (offset < 0 || offset >= items.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(offset), "The offset must be greater than or equal to zero and less than the length of the items array.");
        }

        int offsetAndLength = offset + length;
        if (length < 0 || offsetAndLength > items.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(length), "The length must be greater than or equal to zero and less than or equal to the length of the items array.");
        }

        this.EnsureAdditionalCapacity(length - offset);
        int oldCount = this.Count;

        for (int i = offset; i < offsetAndLength; i++)
        {
            this.Add(items[i]);
        }

        this.Validate(oldCount, this.Count);
    }

    public void Clear()
    {
        this.Count = 0;
    }

    public void RemoveLast()
    {
        this.Count--;
        this.nodes[this.Count].Value = null;
    }

    public bool Any(SyntaxKind kind)
    {
        for (int i = 0; i < this.Count; i++)
        {
            if (this.nodes[i].Value!.Kind == kind)
            {
                return true;
            }
        }

        return false;
    }

    public GreenNode[] ToArray()
    {
        GreenNode[] array = new GreenNode[this.Count];
        for (int i = 0; i < this.Count; i++)
        {
            array[i] = this.nodes[i].Value!;
        }

        return array;
    }

    public GreenNode? ToListNode()
    {
        switch (this.Count)
        {
            case 0:
                return null;
            case 1:
                return this.nodes[0].Value;
            case 2:
                return SyntaxFactory.List(this.nodes[0].Value!, this.nodes[1].Value!);
            case 3:
                return SyntaxFactory.List(this.nodes[0].Value!, this.nodes[1].Value!, this.nodes[2].Value!);
            default:
                ArrayElement<GreenNode>[] tmp = new ArrayElement<GreenNode>[this.Count];
                Array.Copy(this.nodes, tmp, this.Count);
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
        int currentSize = this.nodes.Length;
        int requiredSize = this.Count + additionalCount;

        if (requiredSize <= currentSize)
        {
            return;
        }

        int newSize = GetNewSize(requiredSize, currentSize);
        Debug.Assert(newSize >= requiredSize, "The new size must be greater than or equal to the required size.");
        Array.Resize(ref this.nodes, newSize);
    }

    [Conditional("DEBUG")]
    private void Validate(int start, int end)
    {
        for (int i = start; i < end; i++)
        {
            Debug.Assert(this.nodes[i].Value != null, "The node must not be null.");
        }
    }
}

// <copyright file="SyntaxListBuilder.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Diagnostics;

namespace OffDotNet.Pdf.CodeAnalysis.Old.Syntax.InternalSyntax;

internal class SyntaxListBuilder
{
    private ArrayElement<GreenNode?>[] nodes;

    public SyntaxListBuilder(int size)
    {
        this.nodes = new ArrayElement<GreenNode?>[size];
    }

    public int Count { get; private set; }

    public GreenNode? this[int index]
    {
        get => this.nodes[index];
        set => this.nodes[index].Value = value;
    }

    public void Add(GreenNode? item)
    {
        if (item == null)
        {
            return;
        }

        this.EnsureAdditionalCapacity(1);
        this.nodes[this.Count++].Value = item;
    }

    public void Clear()
    {
        this.Count = 0;
    }

    internal GreenNode? ToListNode()
    {
        return this.Count switch
        {
            0 => null,
            1 => this.nodes[0],
            _ => null,
        };
    }

    private void EnsureAdditionalCapacity(int additionalCount)
    {
        int currentSize = this.nodes.Length;
        int requiredSize = this.Count + additionalCount;

        if (requiredSize <= currentSize)
        {
            return;
        }

        int newSize = requiredSize switch
        {
            < 8 => 8,
            >= int.MaxValue / 2 => int.MaxValue,
            _ => Math.Max(requiredSize, currentSize * 2),
        };

        Debug.Assert(newSize >= requiredSize, "newSize >= requiredSize");
        Array.Resize(ref this.nodes, newSize);
    }
}

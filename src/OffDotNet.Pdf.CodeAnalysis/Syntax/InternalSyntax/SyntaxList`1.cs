// <copyright file="SyntaxList`1.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections;
using System.Diagnostics;
using OffDotNet.Pdf.CodeAnalysis.InternalUtilities;

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

internal readonly partial struct SyntaxList<TNode> : IEnumerable<TNode>, IEquatable<SyntaxList<TNode>>
    where TNode : GreenNode
{
    public SyntaxList(GreenNode? node)
    {
        this.Node = node;
    }

    public GreenNode? Node { get; }

    public int Count
    {
        get
        {
            if (this.Node == null)
            {
                return 0;
            }

            return this.Node.Kind == SyntaxKind.List ? this.Node.SlotCount : 1;
        }
    }

    public TNode[] Nodes
    {
        get
        {
            TNode[] array = new TNode[this.Count];

            for (int i = 0; i < this.Count; i++)
            {
                array[i] = this.GetRequiredItem(i);
            }

            return array;
        }
    }

    public TNode? this[int index]
    {
        get
        {
            if (this.Node == null)
            {
                return null;
            }

            if (this.Node.Kind == SyntaxKind.List)
            {
                return (TNode?)this.Node.GetSlot(index);
            }

            if (index == 0)
            {
                return (TNode?)this.Node;
            }

            throw ExceptionUtilities.Unreachable();
        }
    }

    public static bool operator ==(SyntaxList<TNode> left, SyntaxList<TNode> right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(SyntaxList<TNode> left, SyntaxList<TNode> right)
    {
        return !left.Equals(right);
    }

    public bool Any()
    {
        return this.Node != null;
    }

    public bool Any(SyntaxKind kind)
    {
        foreach (var element in this)
        {
            if (element.Kind == kind)
            {
                return true;
            }
        }

        return false;
    }

    public IEnumerator<TNode> GetEnumerator()
    {
        return new Enumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    public bool Equals(SyntaxList<TNode> other)
    {
        return Equals(this.Node, other.Node);
    }

    public override bool Equals(object? obj)
    {
        return obj is SyntaxList<TNode> other && this.Equals(other);
    }

    public override int GetHashCode()
    {
        return this.Node != null ? this.Node.GetHashCode() : 0;
    }

    private TNode GetRequiredItem(int index)
    {
        var node = this[index];
        Debug.Assert(node != null, "Caller should have checked for null already.");
        return node;
    }
}

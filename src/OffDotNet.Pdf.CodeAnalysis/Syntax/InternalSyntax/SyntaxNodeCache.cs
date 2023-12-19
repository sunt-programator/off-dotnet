// <copyright file="SyntaxNodeCache.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

internal static class SyntaxNodeCache
{
    private const int MaxCachedChildNum = 3;
    private const int CacheSizeBits = 16;
    private const int CacheSize = 1 << CacheSizeBits;
    private const int CacheMask = CacheSize - 1;

    private static readonly Entry[] Cache = new Entry[CacheSize];

    private readonly struct Entry
    {
        public readonly int Hash;
        public readonly GreenNode? Node;

        internal Entry(int hash, GreenNode node)
        {
            this.Hash = hash;
            this.Node = node;
        }
    }

    [SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1201:Elements should appear in the correct order", Justification = "Reviewed.")]
    public static void AddNode(GreenNode node, int hash)
    {
        if (!AllChildrenInCache(node))
        {
            return;
        }

        Debug.Assert(GetCacheHash(node) == hash, "Hash must match");
        int index = hash & CacheMask;
        Cache[index] = new Entry(hash, node);
    }

    internal static GreenNode? TryGetNode(SyntaxKind kind, GreenNode? child1, out int hash)
    {
        if (!CanBeCached(child1))
        {
            hash = -1;
            return null;
        }

        hash = GetCacheHash(kind, child1);
        int index = hash & CacheMask;
        var cacheEntry = Cache[index];

        if (cacheEntry.Hash == hash && cacheEntry.Node != null && IsCacheEquivalent(kind, cacheEntry.Node, child1))
        {
            return cacheEntry.Node;
        }

        return null;
    }

    internal static GreenNode? TryGetNode(SyntaxKind kind, GreenNode? child1, GreenNode? child2, out int hash)
    {
        if (!CanBeCached(child1) || !CanBeCached(child2))
        {
            hash = -1;
            return null;
        }

        hash = GetCacheHash(kind, child1, child2);
        int index = hash & CacheMask;
        var cacheEntry = Cache[index];

        if (cacheEntry.Hash == hash && cacheEntry.Node != null && IsCacheEquivalent(kind, cacheEntry.Node, child1, child2))
        {
            return cacheEntry.Node;
        }

        return null;
    }

    internal static GreenNode? TryGetNode(SyntaxKind kind, GreenNode? child1, GreenNode? child2, GreenNode? child3, out int hash)
    {
        if (!CanBeCached(child1) || !CanBeCached(child2) || !CanBeCached(child3))
        {
            hash = -1;
            return null;
        }

        hash = GetCacheHash(kind, child1, child2, child3);
        int index = hash & CacheMask;
        var cacheEntry = Cache[index];

        if (cacheEntry.Hash == hash && cacheEntry.Node != null && IsCacheEquivalent(kind, cacheEntry.Node, child1, child2, child3))
        {
            return cacheEntry.Node;
        }

        return null;
    }

    private static bool CanBeCached(GreenNode? node)
    {
        return node == null || node.SlotCount <= MaxCachedChildNum;
    }

    private static int GetCacheHash(GreenNode node)
    {
        Debug.Assert(node.SlotCount <= MaxCachedChildNum, "Caller should have checked this already");

        int code = (ushort)node.Kind;
        for (int i = 0; i < node.SlotCount; i++)
        {
            var child = node.GetSlot(i);
            if (child != null)
            {
                code = HashCode.Combine(RuntimeHelpers.GetHashCode(child), code);
            }
        }

        return code & int.MaxValue;
    }

    private static int GetCacheHash(SyntaxKind kind, GreenNode? child1 = null, GreenNode? child2 = null, GreenNode? child3 = null)
    {
        int code = (ushort)kind;

        if (child1 != null)
        {
            code = HashCode.Combine(RuntimeHelpers.GetHashCode(child1), code);
        }

        if (child2 != null)
        {
            code = HashCode.Combine(RuntimeHelpers.GetHashCode(child2), code);
        }

        if (child3 != null)
        {
            code = HashCode.Combine(RuntimeHelpers.GetHashCode(child3), code);
        }

        return code & int.MaxValue; // ensure nonnegative hash
    }

    private static bool IsCacheEquivalent(SyntaxKind kind, GreenNode originalNode, GreenNode? child1 = null, GreenNode? child2 = null, GreenNode? child3 = null)
    {
        Debug.Assert(originalNode.SlotCount <= MaxCachedChildNum, "Caller should have checked this already");
        bool result = originalNode.Kind == kind;

        if (child1 != null)
        {
            result = result && originalNode.GetSlot(0) == child1;
        }

        if (child2 != null)
        {
            result = result && originalNode.GetSlot(1) == child2;
        }

        if (child3 != null)
        {
            result = result && originalNode.GetSlot(2) == child3;
        }

        return result;
    }

    private static bool AllChildrenInCache(GreenNode node)
    {
        for (int i = 0; i < node.SlotCount; i++)
        {
            if (!ChildInCache(node.GetSlot(i)))
            {
                return false;
            }
        }

        return true;
    }

    private static bool ChildInCache(GreenNode? child)
    {
        if (child == null || child.SlotCount == 0)
        {
            return true; // for the purpose of this function consider that null nodes, tokens and trivia are cached somewhere else.
        }

        int hash = GetCacheHash(child);
        int index = hash & CacheMask;
        return Cache[index].Node == child;
    }
}
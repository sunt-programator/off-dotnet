// <copyright file="SharedObjectPools.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

#pragma warning disable CS0618 // Type or member is obsolete
namespace OffDotNet.CodeAnalysis.PooledObjects;

using Microsoft.Extensions.ObjectPool;
using AbstractNodesCacheTuple = (Syntax.AbstractNode Node, bool Leading, bool Trailing);

internal static class SharedObjectPools
{
    /// <summary>The abstract nodes cache.</summary>
    internal static readonly ObjectPool<Stack<AbstractNodesCacheTuple>> AbstractNodesCache;

    /// <summary>The string builder pool.</summary>
    internal static readonly ObjectPool<StringBuilder> StringBuilderPool;

    static SharedObjectPools()
    {
        var defaultObjectPoolProvider = new DefaultObjectPoolProvider();
        var defaultStringBuilderPoolProvider = new DefaultObjectPoolProvider { MaximumRetained = 32, };

#if DEBUG
        var objectPoolProvider = new LeakTrackingObjectPoolProvider(defaultObjectPoolProvider);
        var stringBuilderPoolProvider = new LeakTrackingObjectPoolProvider(defaultStringBuilderPoolProvider);
#else
        var objectPoolProvider = defaultObjectPoolProvider;
        var stringBuilderPoolProvider = defaultStringBuilderPoolProvider;
#endif

        var stackPooledObjectPolicy = new StackPooledObjectPolicy<AbstractNodesCacheTuple>(initialCapacity: 2, maximumRetainedCapacity: 16);

        AbstractNodesCache = objectPoolProvider.Create(stackPooledObjectPolicy);
        StringBuilderPool = stringBuilderPoolProvider.CreateStringBuilderPool();
    }
}

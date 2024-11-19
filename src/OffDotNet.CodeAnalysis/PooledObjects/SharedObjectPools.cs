// <copyright file="SharedObjectPools.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

#pragma warning disable CS0618 // Type or member is obsolete
namespace OffDotNet.CodeAnalysis.PooledObjects;

using Microsoft.Extensions.ObjectPool;
using AbstractNodesCacheTuple = (Syntax.AbstractNode Node, bool Leading, bool Trailing);

/// <summary>
/// Provides shared object pools for various types used in the OffDotNet analysis.
/// </summary>
internal static class SharedObjectPools
{
    /// <summary>
    /// Gets the object pool for caching abstract nodes.
    /// </summary>
    internal static readonly ObjectPool<Stack<AbstractNodesCacheTuple>> AbstractNodesCache;

    /// <summary>
    /// Gets the object pool for <see cref="StringBuilder"/> instances.
    /// </summary>
    internal static readonly ObjectPool<StringBuilder> StringBuilderPool;

    /// <summary>
    /// Initializes static members of the <see cref="SharedObjectPools"/> class.
    /// </summary>
    [SuppressMessage("Minor Code Smell", "S3963:\"static\" fields should be initialized inline", Justification = "Reviewed")]
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

        StringBuilderPool = stringBuilderPoolProvider.CreateStringBuilderPool();
        AbstractNodesCache = objectPoolProvider.Create(stackPooledObjectPolicy);
    }
}

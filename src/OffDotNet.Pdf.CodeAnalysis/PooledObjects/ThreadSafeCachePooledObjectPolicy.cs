// <copyright file="ThreadSafeCachePooledObjectPolicy.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.PooledObjects;

using Caching;
using Microsoft.Extensions.ObjectPool;

/// <summary>The thread-safe cache pooled object policy.</summary>
/// <typeparam name="TKey">The type of the cache key.</typeparam>
/// <typeparam name="TValue">The type of the cache value.</typeparam>
internal sealed class
    ThreadSafeCachePooledObjectPolicy<TKey, TValue> : PooledObjectPolicy<ThreadSafeCacheFactory<TKey, TValue>>
    where TKey : notnull
    where TValue : notnull
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ThreadSafeCachePooledObjectPolicy{TKey, TValue}"/> class.
    /// </summary>
    /// <param name="localCacheSize">The size of the local cache.</param>
    /// <param name="sharedCacheSize">The size of the shared cache.</param>
    /// <param name="bucketSize">The size of the bucket. Used for solving hash collisions.</param>
    public ThreadSafeCachePooledObjectPolicy(
        int localCacheSize = 2048,
        int sharedCacheSize = 65536,
        int bucketSize = 16)
    {
        this.LocalCacheSize = localCacheSize;
        this.SharedCacheSize = sharedCacheSize;
        this.BucketSize = bucketSize;
    }

    /// <summary>Gets the local cache size.</summary>
    /// <value>Defaults to <c>2048</c>.</value>
    public int LocalCacheSize { get; }

    /// <summary>Gets the shared cache size.</summary>
    /// <value>Defaults to <c>65536</c>.</value>
    public int SharedCacheSize { get; }

    /// <summary>Gets the shared cache bucket size.</summary>
    /// <value>Defaults to <c>16</c>.</value>
    public int BucketSize { get; }

    /// <inheritdoc/>
    public override ThreadSafeCacheFactory<TKey, TValue> Create()
    {
        return new ThreadSafeCacheFactory<TKey, TValue>(
            this.LocalCacheSize,
            this.SharedCacheSize,
            this.BucketSize);
    }

    /// <inheritdoc/>
    public override bool Return(ThreadSafeCacheFactory<TKey, TValue> obj)
    {
        return true;
    }
}

// <copyright file="ThreadSafeCacheFactory.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Caching;

using System.Diagnostics.CodeAnalysis;

/// <summary>The thread-safe cache factory.</summary>
/// <typeparam name="TKey">The type of the cache key.</typeparam>
/// <typeparam name="TValue">The type of the cache value.</typeparam>
[SuppressMessage(
    "StyleCop.CSharp.MaintainabilityRules",
    "SA1402:File may only contain a single type",
    Justification = "Reviewed.")]
internal sealed class ThreadSafeCacheFactory<TKey, TValue> : CacheFactory<TKey, TValue>
    where TKey : notnull
    where TValue : notnull
{
    private readonly int _sharedEntriesMask;
    private readonly int _sharedBucketMask;
    private readonly CacheEntry<TKey, TValue>[] _sharedEntries;

    /// <summary>
    /// Initializes a new instance of the <see cref="ThreadSafeCacheFactory{TKey, TValue}"/> class.
    /// </summary>
    /// <param name="localCacheSize">The size of the local cache.</param>
    /// <param name="sharedCacheSize">The size of the shared cache.</param>
    /// <param name="bucketSize">The size of the bucket. Used for solving hash collisions.</param>
    /// <param name="keyHash">The key hash function.</param>
    /// <param name="keyValueEquality">The key value equality function.</param>
    public ThreadSafeCacheFactory(
        int localCacheSize,
        int sharedCacheSize,
        int bucketSize,
        Func<TKey, int>? keyHash = null,
        Func<TKey, TValue, bool>? keyValueEquality = null)
        : base(localCacheSize, keyHash, keyValueEquality)
    {
        var sharedCacheAdjustedSize = AdjustSize(sharedCacheSize);
        var bucketAdjustedSize = AdjustSize(bucketSize);
        _sharedEntriesMask = sharedCacheAdjustedSize - 1;
        _sharedBucketMask = bucketAdjustedSize - 1;
        _sharedEntries = new CacheEntry<TKey, TValue>[sharedCacheAdjustedSize];
    }

    /// <inheritdoc/>
    public override bool TryGetValue(TKey key, [NotNullWhen(true)] out TValue? value)
    {
        return base.TryGetValue(key, out value) || TryGetSharedValue(key, out value);
    }

    /// <inheritdoc/>
    public override void Add(TKey key, TValue value)
    {
        base.Add(key, value);
        AddShared(key, value);
    }

    /// <inheritdoc/>
    public override TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
    {
        if (TryGetValue(key, out var value))
        {
            return value;
        }

        value = valueFactory(key);
        Add(key, value);
        return value;
    }

    private bool TryGetSharedValue(TKey key, [NotNullWhen(true)] out TValue? value)
    {
        var index = GetSharedIndex(key);
        var entry = _sharedEntries[index];

        for (var i = 1; i < _sharedBucketMask; i++)
        {
            if (EqualityComparer<TKey>.Default.Equals(entry.Key, key) &&
                KeyValueEquality(key, entry.Value))
            {
                value = entry.Value;
                return true;
            }

            index = (index + i) & _sharedEntriesMask;
            entry = _sharedEntries[index];
        }

        value = default;
        return false;
    }

    private void AddShared(TKey key, TValue value)
    {
        var index = GetSharedIndex(key);
        var currentIndex = index;
        for (var i = 1; i < _sharedBucketMask; i++)
        {
            var entry = _sharedEntries[currentIndex];

            if (EqualityComparer<TKey>.Default.Equals(entry.Key, key) &&
                KeyValueEquality(key, entry.Value))
            {
                _sharedEntries[currentIndex] = new CacheEntry<TKey, TValue>(key, value);
                return;
            }

            currentIndex = (currentIndex + i) & _sharedEntriesMask;
        }

        var i1 = ThreadSafeCacheFactory.SharedNextRandom() & _sharedBucketMask;
        index = (index + (((i1 * i1) + i1) / 2)) & _sharedEntriesMask;
        _sharedEntries[index] = new CacheEntry<TKey, TValue>(key, value);
    }

    private int GetSharedIndex(TKey key)
    {
        var hash = KeyHash(key);
        return (hash ^ (hash >> GetNumberOfBits(Size))) & _sharedEntriesMask;
    }
}

/// <summary>The thread-safe cache factory helper class.</summary>
[SuppressMessage(
    "StyleCop.CSharp.OrderingRules",
    "SA1204:Static elements should appear before instance elements",
    Justification = "Reviewed.")]
internal static class ThreadSafeCacheFactory
{
    private static int s_sharedRandom = Environment.TickCount;

    /// <summary>Returns the next random number.</summary>
    /// <returns>The next random number.</returns>
    internal static int SharedNextRandom()
    {
        return Interlocked.Increment(ref s_sharedRandom);
    }
}

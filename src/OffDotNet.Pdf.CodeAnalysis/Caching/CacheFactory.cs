// <copyright file="CacheFactory.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Caching;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

/// <summary>The cache factory class.</summary>
/// <typeparam name="TKey">The type of the cache entry key.</typeparam>
/// <typeparam name="TValue">The type of the cache entry value.</typeparam>
[SuppressMessage(
    "StyleCop.CSharp.MaintainabilityRules",
    "SA1401:Fields should be private",
    Justification = "No need to use property in this case.")]
internal class CacheFactory<TKey, TValue> : AbstractCache<CacheEntry<TKey, TValue>>
    where TKey : notnull
    where TValue : notnull
{
    /// <summary>The size of the cache.</summary>
    protected readonly int Size;

    /// <summary>The key hash function.</summary>
    protected readonly Func<TKey, int> KeyHash;

    /// <summary>The key value equality function.</summary>
    protected readonly Func<TKey, TValue, bool> KeyValueEquality;

    private static readonly Func<TKey, int> s_defaultKeyHash = key => key.GetHashCode();
    private static readonly Func<TKey, TValue, bool> s_defaultKeyValueEquality = (_, _) => true;

    /// <summary>
    /// Initializes a new instance of the <see cref="CacheFactory{TKey, TValue}"/> class.
    /// </summary>
    /// <param name="size">The size of the cache.</param>
    /// <param name="keyHash">The key hash function.</param>
    /// <param name="keyValueEquality">The key value equality function.</param>
    public CacheFactory(
        int size,
        Func<TKey, int>? keyHash = null,
        Func<TKey, TValue, bool>? keyValueEquality = null)
        : base(size)
    {
        Size = size;
        KeyHash = keyHash ?? s_defaultKeyHash;
        KeyValueEquality = keyValueEquality ?? s_defaultKeyValueEquality;
    }

    /// <summary>Tries to get the value associated with the specified key from the cache.</summary>
    /// <param name="key">The key of the cache entry.</param>
    /// <param name="value">The value associated with the specified key, if the key is found; otherwise, the default value for the type of the value parameter.</param>
    /// <returns>true if the cache contains an element with the specified key; otherwise, false.</returns>
    public virtual bool TryGetValue(TKey key, [NotNullWhen(true)] out TValue? value)
    {
        var index = GetIndex(key);

        if (!EqualityComparer<TKey>.Default.Equals(Entries[index].Key, key) ||
            !KeyValueEquality(key, Entries[index].Value))
        {
            value = default;
            return false;
        }

        value = Entries[index].Value;
        return true;
    }

    /// <summary>Adds the specified key and value to the cache.</summary>
    /// <param name="key">The key of the cache entry.</param>
    /// <param name="value">The value of the cache entry.</param>
    public virtual void Add(TKey key, TValue value)
    {
        var index = GetIndex(key);
        Entries[index] = new CacheEntry<TKey, TValue>(key, value);
    }

    /// <summary>
    /// Gets the value associated with the specified key from the cache.
    /// If the key is not found, adds the key and value to the cache.
    /// </summary>
    /// <param name="key">The key of the cache entry.</param>
    /// <param name="valueFactory">The function used to generate a value based on the key.</param>
    /// <returns>The value associated with the specified key.</returns>
    public virtual TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
    {
        if (TryGetValue(key, out var value))
        {
            return value;
        }

        var newValue = valueFactory(key);
        Add(key, newValue);
        return newValue;
    }

    /// <summary>
    /// Gets the value associated with the specified key from the cache.
    /// If the key is not found, adds the key and value to the cache.
    /// </summary>
    /// <param name="key">The key of the cache entry.</param>
    /// <param name="value">The function used to generate a value based on the key.</param>
    /// <returns>The value associated with the specified key.</returns>
    public TValue GetOrAdd(TKey key, TValue value)
    {
        if (TryGetValue(key, out var existingValue))
        {
            return existingValue;
        }

        Add(key, value);
        return value;
    }

    /// <summary>Calculates the index of the cache entry based on the key.</summary>
    /// <param name="key">The key of the cache entry.</param>
    /// <returns>The index of the cache entry.</returns>
    private int GetIndex(TKey key)
    {
        return GetKeyHash(key) & Mask;
    }

    /// <summary>
    /// Computes the hash and ensures it is non-zero.
    /// The empty entry is treated as valid.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns>The computed hash.</returns>
    private int GetKeyHash(TKey key)
    {
        var result = KeyHash(key) | Size;
        Debug.Assert(result != 0, "The result must be non-zero to avoid treating an empty entry as valid.");
        return result;
    }
}

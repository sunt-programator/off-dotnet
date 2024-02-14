// <copyright file="CacheEntry.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Caching;

/// <summary>Represents a cache entry.</summary>
/// <typeparam name="TKey">The type of the cache entry key.</typeparam>
/// <typeparam name="TValue">The type of the cache entry value.</typeparam>
internal readonly struct CacheEntry<TKey, TValue>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CacheEntry{TKey, TValue}"/> struct with the specified <see cref="Key"/> and <see cref="Value"/>.
    /// </summary>
    /// <param name="key">The key of the cache entry.</param>
    /// <param name="value">The value of the cache entry.</param>
    internal CacheEntry(TKey key, TValue value)
    {
        Key = key;
        Value = value;
    }

    /// <summary>Gets the key of the cache entry.</summary>
    public TKey Key { get; }

    /// <summary>Gets the value of the cache entry.</summary>
    public TValue Value { get; }
}

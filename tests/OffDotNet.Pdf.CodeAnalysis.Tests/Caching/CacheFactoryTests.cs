// <copyright file="CacheFactoryTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Caching;

using OffDotNet.Pdf.CodeAnalysis.Caching;

public class CacheFactoryTests
{
    private readonly CacheFactory<string, string> _cache = new(4);

    [Fact(DisplayName = "The TryGetValue(TKey, TValue?) method must " +
                        "return false when the key is not found in the cache.")]
    public void TryGetValueMethod_MustReturnFalse_WhenKeyIsNotFound()
    {
        // Arrange

        // Act
        var result = _cache.TryGetValue("key", out var value);

        // Assert
        Assert.False(result);
        Assert.Null(value);
    }

    [Fact(DisplayName = "The Add(TKey, TValue) method must add the key-value pair to the cache.")]
    public void AddMethod_MustAddKeyValuePairToCache()
    {
        // Arrange
        const string Key = "key";
        const string Value = "value";

        // Act
        _cache.Add(Key, Value);
        var result = _cache.TryGetValue(Key, out var actualValue);

        // Assert
        Assert.True(result);
        Assert.Equal(Value, actualValue);
    }

    [Fact(DisplayName = "The multiple Add(TKey, TValue) and TryGetValue(TKey key, out TValue? value) " +
                        "method calls must return the same reference.")]
    public void AddMethod_MustReturnTheSameReference()
    {
        // Arrange
        const string Key = "key";
        const string Value = "value";

        // Act
        _cache.Add(Key, Value);
        var result1 = _cache.TryGetValue(Key, out var actualValue1);
        _cache.Add(Key, Value);
        var result2 = _cache.TryGetValue(Key, out var actualValue2);

        // Assert
        Assert.True(result1);
        Assert.True(result2);
        Assert.Same(Value, actualValue1);
        Assert.Same(Value, actualValue2);
    }

    [Fact(DisplayName = "The GetOrAdd(TKey, Func<TKey, TValue>) method must add the key-value pair " +
                        "to the cache if the key is not found.")]
    public void GetOrAddMethod_MustAddKeyValuePairToCache_WhenKeyIsNotFound()
    {
        // Arrange
        const string Key = "key";
        const string Value = "value";

        // Act
        var actualValue = _cache.GetOrAdd(Key, _ => Value);
        var containsKey = _cache.TryGetValue(Key, out var value);

        // Assert
        Assert.Equal(Value, actualValue);
        Assert.True(containsKey);
        Assert.Equal(Value, value);
        Assert.Same(actualValue, value);
    }

    [Fact(DisplayName = "The GetOrAdd(TKey, Func<TKey, TValue>) method must return the value " +
                        "associated with the specified key from the cache if the key exists.")]
    public void GetOrAddMethod_MustReturnTheValueAssociatedWithTheKey_WhenKeyExists()
    {
        // Arrange
        const string Key = "key";
        const string Value = "value";

        // Act
        _cache.Add(Key, Value);
        var actualValue = _cache.GetOrAdd(Key, _ => Value);

        // Assert
        Assert.Equal(Value, actualValue);
    }
}

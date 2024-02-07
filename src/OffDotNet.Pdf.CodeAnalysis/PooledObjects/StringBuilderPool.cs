// <copyright file="StringBuilderPool.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.PooledObjects;

using System.Collections.Immutable;
using System.Text;
using Microsoft.Extensions.ObjectPool;

internal static class StringBuilderPool
{
    internal static readonly ObjectPool<StringBuilder> Instance = new DefaultObjectPoolProvider
    {
        MaximumRetained = 32,
    }.CreateStringBuilderPool(100, 1024);

    /// <summary>
    /// Creates an <see cref="ObjectPool{T}"/> that pools <see cref="StringBuilder"/> instances.
    /// </summary>
    /// <param name="provider">The <see cref="ObjectPoolProvider"/>.</param>
    /// <param name="initialCapacity">The initial capacity to initialize <see cref="StringBuilder"/> instances with.</param>
    /// <param name="maximumRetainedCapacity">The maximum value for <see cref="StringBuilder.Capacity"/> that is allowed to be
    /// retained, when an instance is returned.</param>
    /// <typeparam name="T"> is a class.</typeparam>
    /// <returns>The <see cref="ObjectPool{T}"/>.</returns>
    public static ObjectPool<ImmutableArray<T>.Builder> CreateArrayBuilderPool<T>(
        this ObjectPoolProvider provider,
        int initialCapacity,
        int maximumRetainedCapacity)
    {
        var policy = new ArrayBuilderPooledObjectPolicy<T>
        {
            InitialCapacity = initialCapacity,
            MaximumRetainedCapacity = maximumRetainedCapacity,
        };

        return provider.Create(policy);
    }
}

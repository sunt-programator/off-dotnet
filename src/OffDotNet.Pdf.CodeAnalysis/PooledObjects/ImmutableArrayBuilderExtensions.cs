// <copyright file="ImmutableArrayBuilderExtensions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.PooledObjects;

using System.Collections.Immutable;
using System.Text;
using Microsoft.Extensions.ObjectPool;

internal static class ImmutableArrayBuilderExtensions
{
    public static T Pop<T>(this ImmutableArray<T>.Builder builder)
    {
        var e = builder.Peek();
        builder.RemoveAt(builder.Count - 1);
        return e;
    }

    public static T Peek<T>(this ImmutableArray<T>.Builder builder)
    {
        return builder[^1];
    }

    public static void Push<T>(this ImmutableArray<T>.Builder builder, T item)
    {
        builder.Add(item);
    }

    /// <summary>
    /// Creates an <see cref="ObjectPool"/> that pools <see cref="StringBuilder"/> instances.
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
        var policy = new ImmutableArrayBuilderPooledObjectPolicy<T>
        {
            InitialCapacity = initialCapacity,
            MaximumRetainedCapacity = maximumRetainedCapacity,
        };

        return provider.Create(policy);
    }
}

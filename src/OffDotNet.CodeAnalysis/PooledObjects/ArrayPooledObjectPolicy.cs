// <copyright file="ArrayPooledObjectPolicy.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.PooledObjects;

using Microsoft.Extensions.ObjectPool;

/// <summary>
/// Provides a policy for pooling arrays with specified initial and maximum retained capacities.
/// </summary>
/// <typeparam name="T">The type of elements in the array.</typeparam>
internal sealed class ArrayPooledObjectPolicy<T> : PooledObjectPolicy<T[]>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ArrayPooledObjectPolicy{T}"/> class.
    /// </summary>
    public ArrayPooledObjectPolicy()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ArrayPooledObjectPolicy{T}"/> class with the specified capacities.
    /// </summary>
    /// <param name="initialCapacity">The initial capacity of pooled byte array instances.</param>
    /// <param name="maximumRetainedCapacity">The maximum capacity of a single byte array instance that is allowed to be retained.</param>
    public ArrayPooledObjectPolicy(int initialCapacity, int maximumRetainedCapacity)
    {
        InitialCapacity = initialCapacity;
        MaximumRetainedCapacity = maximumRetainedCapacity;
    }

    /// <summary>
    /// Gets the initial capacity of pooled byte array instances.
    /// </summary>
    /// <value>Defaults to <c>8</c>.</value>
    public int InitialCapacity { get; init; } = 8;

    /// <summary>
    /// Gets the maximum capacity of a single byte array instance that is allowed to be retained, when <see cref="Return(T[])"/> is invoked.
    /// </summary>
    /// <value>Defaults to <c>128</c>.</value>
    public int MaximumRetainedCapacity { get; init; } = 128;

    /// <inheritdoc/>
    public override T[] Create()
    {
        return new T[InitialCapacity];
    }

    /// <inheritdoc/>
    public override bool Return(T[] obj)
    {
        if (obj.Length > this.MaximumRetainedCapacity)
        {
            // Too big. Discard this one.
            return false;
        }

        Array.Clear(obj);
        return true;
    }
}

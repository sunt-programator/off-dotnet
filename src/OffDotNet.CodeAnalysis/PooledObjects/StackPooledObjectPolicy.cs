// <copyright file="StackPooledObjectPolicy.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.PooledObjects;

using Microsoft.Extensions.ObjectPool;

/// <summary>
/// Provides a policy for pooling stacks with specified initial and maximum retained capacities.
/// </summary>
/// <typeparam name="T">The type of elements in the stack.</typeparam>
internal sealed class StackPooledObjectPolicy<T> : PooledObjectPolicy<Stack<T>>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StackPooledObjectPolicy{T}"/> class.
    /// </summary>
    public StackPooledObjectPolicy()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StackPooledObjectPolicy{T}"/> class with the specified capacities.
    /// </summary>
    /// <param name="initialCapacity">The initial capacity of pooled stack instances.</param>
    /// <param name="maximumRetainedCapacity">The maximum capacity of a single stack instance that is allowed to be retained.</param>
    public StackPooledObjectPolicy(int initialCapacity, int maximumRetainedCapacity)
    {
        InitialCapacity = initialCapacity;
        MaximumRetainedCapacity = maximumRetainedCapacity;
    }

    /// <summary>
    /// Gets the initial capacity of pooled stack instances.
    /// </summary>
    /// <value>Defaults to <c>8</c>.</value>
    public int InitialCapacity { get; init; } = 8;

    /// <summary>
    /// Gets the maximum capacity of a single stack instance that is allowed to be retained, when <see cref="Return(Stack{T})"/> is invoked.
    /// </summary>
    /// <value>Defaults to <c>128</c>.</value>
    public int MaximumRetainedCapacity { get; init; } = 128;

    /// <inheritdoc/>
    public override Stack<T> Create()
    {
        return new Stack<T>(capacity: InitialCapacity);
    }

    /// <inheritdoc/>
    public override bool Return(Stack<T> obj)
    {
        if (obj.Count > this.MaximumRetainedCapacity)
        {
            // Too big. Discard this one.
            return false;
        }

        obj.Clear();
        return true;
    }
}

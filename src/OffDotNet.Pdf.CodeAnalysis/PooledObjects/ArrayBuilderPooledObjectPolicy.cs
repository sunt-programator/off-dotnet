// <copyright file="ArrayBuilderPooledObjectPolicy.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.PooledObjects;

using System.Collections.Immutable;
using Microsoft.Extensions.ObjectPool;

internal sealed class ArrayBuilderPooledObjectPolicy<T> : PooledObjectPolicy<ImmutableArray<T>.Builder>
{
    /// <summary>
    /// Gets the initial capacity of pooled <see cref="ImmutableArray{T}.Builder"/> instances.
    /// </summary>
    /// <value>Defaults to <c>8</c>.</value>
    public int InitialCapacity { get; init; } = 8;

    /// <summary>
    /// Gets the maximum value for <see cref="ImmutableArray{T}.Builder.Capacity"/> that is allowed to be
    /// retained, when <see cref="Return(ImmutableArray{T}.Builder)"/> is invoked.
    /// </summary>
    /// <value>Defaults to <c>128</c>.</value>
    public int MaximumRetainedCapacity { get; init; } = 128;

    /// <inheritdoc/>
    public override ImmutableArray<T>.Builder Create()
    {
        return ImmutableArray.CreateBuilder<T>(this.InitialCapacity);
    }

    /// <inheritdoc/>
    public override bool Return(ImmutableArray<T>.Builder obj)
    {
        if (obj.Capacity > this.MaximumRetainedCapacity)
        {
            // Too big. Discard this one.
            return false;
        }

        obj.Clear();
        return true;
    }
}

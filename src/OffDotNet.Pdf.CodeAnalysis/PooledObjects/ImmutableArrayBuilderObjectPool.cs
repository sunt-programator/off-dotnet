// <copyright file="ImmutableArrayBuilderObjectPool.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.PooledObjects;

using System.Collections.Immutable;
using Microsoft.Extensions.ObjectPool;

internal static class ImmutableArrayBuilderObjectPool<T>
{
    internal static readonly ObjectPool<ImmutableArray<T>.Builder> Instance = new DefaultObjectPoolProvider
    {
        MaximumRetained = 8,
    }.CreateArrayBuilderPool<T>(8, 128);
}

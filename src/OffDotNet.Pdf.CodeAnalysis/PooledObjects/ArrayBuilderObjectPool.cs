// <copyright file="ArrayBuilderObjectPool.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Immutable;
using Microsoft.Extensions.ObjectPool;

namespace OffDotNet.Pdf.CodeAnalysis.PooledObjects;

internal static class ArrayBuilderObjectPool<T>
{
    internal static readonly ObjectPool<ImmutableArray<T>.Builder> Instance = new DefaultObjectPoolProvider
    {
        MaximumRetained = 8,
    }.CreateArrayBuilderPool<T>(8, 128);
}

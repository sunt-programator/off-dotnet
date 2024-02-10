// <copyright file="StringBuilderPool.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.PooledObjects;

using System.Text;
using Microsoft.Extensions.ObjectPool;

internal static class StringBuilderPool
{
    internal static readonly ObjectPool<StringBuilder> Instance = new DefaultObjectPoolProvider
    {
        MaximumRetained = 32,
    }.CreateStringBuilderPool(100, 1024);
}

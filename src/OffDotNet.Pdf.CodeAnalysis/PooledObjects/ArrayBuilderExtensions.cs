// <copyright file="ArrayBuilderExtensions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.PooledObjects;

using System.Collections.Immutable;

internal static class ArrayBuilderExtensions
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
}

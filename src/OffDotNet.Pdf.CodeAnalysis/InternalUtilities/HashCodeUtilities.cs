// <copyright file="HashCodeUtilities.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.InternalUtilities;

internal static class HashCodeUtilities
{
    public static int ComputeHashCode<T>(ReadOnlySpan<T> span, IEqualityComparer<T>? comparer = null)
    {
        var hashCode = default(HashCode);

        for (var i = 0; i < span.Length; i++)
        {
            hashCode.Add(span[i], comparer);
        }

        return hashCode.ToHashCode();
    }
}

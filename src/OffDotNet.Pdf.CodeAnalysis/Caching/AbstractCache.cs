// <copyright file="AbstractCache.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Caching;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

/// <summary>The abstract cache class.</summary>
/// <typeparam name="TEntry">The type of the entry.</typeparam>
[SuppressMessage(
    "StyleCop.CSharp.MaintainabilityRules",
    "SA1401:Fields should be private",
    Justification = "Reviewed.")]
internal abstract class AbstractCache<TEntry>
{
    /// <summary>The mask.</summary>
    protected readonly int Mask;

    /// <summary>
    /// The cache entries. The size of the array is always a power of 2.
    /// New item displaces anything that previously used the same slot.
    /// </summary>
    protected readonly TEntry[] Entries;

    /// <summary>
    /// Initializes a new instance of the <see cref="AbstractCache{TEntry}"/> class.
    /// </summary>
    /// <param name="size">The size of the cache.</param>
    protected AbstractCache(int size)
    {
        var adjustedSize = AdjustSize(size);
        Mask = adjustedSize - 1;
        Entries = new TEntry[adjustedSize];
    }

    /// <summary>Adjusts the size of the cache to be a power of 2.</summary>
    /// <param name="size">The size of the cache.</param>
    /// <returns>The adjusted size of the cache.</returns>
    protected static int AdjustSize(int size)
    {
        Debug.Assert(size > 0, "Size must be greater than 0");

        size--;
        size |= size >> 1;
        size |= size >> 2;
        size |= size >> 4;
        size |= size >> 8;
        size |= size >> 16;
        return size + 1;
    }

    /// <summary>Gets the number of bits required to represent the specified size.</summary>
    /// <param name="size">The size.</param>
    /// <returns>The number of bits required to represent the specified size.</returns>
    protected static int GetNumberOfBits(int size)
    {
        var bits = 0;
        while (size > 0)
        {
            size >>= 1;
            bits++;
        }

        return bits;
    }
}

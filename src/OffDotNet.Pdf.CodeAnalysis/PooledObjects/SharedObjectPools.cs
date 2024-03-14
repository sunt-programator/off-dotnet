// <copyright file="SharedObjectPools.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

#pragma warning disable CS0618 // Type or member is obsolete

namespace OffDotNet.Pdf.CodeAnalysis.PooledObjects;

using System.Diagnostics.CodeAnalysis;
using System.Text;
using Caching;
using Microsoft.Extensions.ObjectPool;
using Syntax.InternalSyntax;
using SyntaxKind = Syntax.SyntaxKind;

/// <summary>The shared object pools.</summary>
[SuppressMessage(
    "Minor Code Smell",
    "S3963:\"static\" fields should be initialized inline",
    Justification = "Reviewed.")]
internal static class SharedObjectPools
{
    /// <summary>The sliding window pool.</summary>
    internal static readonly ObjectPool<byte[]> WindowPool;

    /// <summary>The string intern cache.</summary>
    internal static readonly ObjectPool<ThreadSafeCacheFactory<int, byte[]>> StringTable;

    /// <summary>The syntax token cache.</summary>
    internal static readonly ObjectPool<ThreadSafeCacheFactory<string, SyntaxToken>> SyntaxTokenCache;

    /// <summary>The syntax trivia cache.</summary>
    internal static readonly ObjectPool<ThreadSafeCacheFactory<string, SyntaxTrivia>> SyntaxTriviaCache;

    /// <summary>The keyword kind cache.</summary>
    internal static readonly ObjectPool<ThreadSafeCacheFactory<string, SyntaxKind>> KeywordKindCache;

    /// <summary>The string builder pool.</summary>
    internal static readonly ObjectPool<StringBuilder> StringBuilderPool;

    /// <summary>The default window length.</summary>
    private const int DefaultWindowLength = 2048;

    static SharedObjectPools()
    {
        var defaultObjectPoolProvider = new DefaultObjectPoolProvider();
        var defaultStringBuilderPoolProvider = new DefaultObjectPoolProvider
        {
            MaximumRetained = 32,
        };

#if DEBUG
        var objectPoolProvider = new LeakTrackingObjectPoolProvider(defaultObjectPoolProvider);
        var stringBuilderPoolProvider = new LeakTrackingObjectPoolProvider(defaultStringBuilderPoolProvider);
#else
        var objectPoolProvider = defaultObjectPoolProvider;
        var stringBuilderPoolProvider = defaultStringBuilderPoolProvider;
#endif
        var arrayPooledObject = new ArrayPooledObjectPolicy<byte>(DefaultWindowLength, DefaultWindowLength);
        var stringTablePolicy = new ThreadSafeCachePooledObjectPolicy<int, byte[]>();
        var syntaxTokenCache = new ThreadSafeCachePooledObjectPolicy<string, SyntaxToken>();
        var syntaxTriviaCache = new ThreadSafeCachePooledObjectPolicy<string, SyntaxTrivia>();
        var keywordKindCache = new ThreadSafeCachePooledObjectPolicy<string, SyntaxKind>();

        WindowPool = objectPoolProvider.Create(arrayPooledObject);
        StringTable = objectPoolProvider.Create(stringTablePolicy);
        SyntaxTokenCache = objectPoolProvider.Create(syntaxTokenCache);
        SyntaxTriviaCache = objectPoolProvider.Create(syntaxTriviaCache);
        KeywordKindCache = objectPoolProvider.Create(keywordKindCache);
        StringBuilderPool = stringBuilderPoolProvider.CreateStringBuilderPool();
    }
}

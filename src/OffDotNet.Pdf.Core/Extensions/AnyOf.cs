// <copyright file="AnyOf.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Extensions;

using System.Diagnostics.CodeAnalysis;
using Common;

public readonly struct AnyOf<T1, T2>
    where T1 : IPdfObject
    where T2 : IPdfObject
{
    private readonly T1? firstType;
    private readonly T2? secondType;

    public AnyOf(T1 type)
    {
        this.firstType = type;
    }

    public AnyOf(T2 type)
    {
        this.secondType = type;
    }

    [SuppressMessage(
        "ReSharper",
        "ReplaceConditionalExpressionWithNullCoalescing",
        Justification = "Thi fixer leads to compiler error")]
    public IPdfObject PdfObject =>
        this.firstType is { } t1
            ? t1
            : this.secondType!;

    public static implicit operator AnyOf<T1, T2>(T1 type1)
    {
        return new AnyOf<T1, T2>(type1);
    }

    public static implicit operator AnyOf<T1, T2>(T2 type2)
    {
        return new AnyOf<T1, T2>(type2);
    }
}

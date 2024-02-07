// <copyright file="AnyOf.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Extensions;

using OffDotNet.Pdf.Core.Common;
using OffDotNet.Pdf.Core.Primitives;

public class AnyOf<T1, T2>
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

    public IPdfObject PdfObject
    {
        get
        {
            if (this.firstType != null)
            {
                return this.firstType;
            }

            if (this.secondType != null)
            {
                return this.secondType;
            }

            return default(PdfNull);
        }
    }

    public static implicit operator AnyOf<T1, T2>(T1 type1)
    {
        return new AnyOf<T1, T2>(type1);
    }

    public static implicit operator AnyOf<T1, T2>(T2 type2)
    {
        return new AnyOf<T1, T2>(type2);
    }
}

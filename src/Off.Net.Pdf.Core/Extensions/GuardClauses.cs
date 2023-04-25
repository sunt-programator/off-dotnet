// <copyright file="GuardClauses.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Runtime.CompilerServices;

namespace Off.Net.Pdf.Core.Extensions;

internal static class GuardClauses
{
    public static T NotNull<T, TProperty>(this T value, Func<T, TProperty?> checkFunc, [CallerArgumentExpression("checkFunc")] string? paramName = null)
    {
        if (checkFunc(value) is null)
        {
            throw new ArgumentNullException(paramName);
        }

        return value;
    }

    public static T CheckConstraints<T>(this T value, Func<T, bool> checkFunc, string exceptionMessage, [CallerArgumentExpression("checkFunc")] string? paramName = null)
    {
        if (!checkFunc(value))
        {
            throw new ArgumentOutOfRangeException(paramName, exceptionMessage);
        }

        return value;
    }
}

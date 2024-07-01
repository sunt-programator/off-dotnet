// <copyright file="Dependencies.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis;

using Lexer;
using Microsoft.Extensions.DependencyInjection;

public static class Dependencies
{
    public static IServiceCollection AddCodeAnalysis(this IServiceCollection services)
    {
        services.AddSingleton<ICursorFactory, CursorFactory>();
        return services;
    }
}

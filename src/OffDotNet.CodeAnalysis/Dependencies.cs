// <copyright file="Dependencies.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis;

using Lexer;
using Microsoft.Extensions.DependencyInjection;
using OffDotNet.CodeAnalysis.Diagnostics;

/// <summary>
/// Provides extension methods for registering code analysis services.
/// </summary>
public static class Dependencies
{
    /// <summary>
    /// Adds the code analysis services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The service collection to which the services will be added.</param>
    /// <returns>The same service collection so that multiple calls can be chained.</returns>
    public static IServiceCollection AddCoreCodeAnalysis(this IServiceCollection services)
    {
        services.AddLocalization(options => options.ResourcesPath = "Resources");
        services.AddSingleton<ICursorFactory, CursorFactory>();
        services.AddSingleton<IMessageProvider, AbstractMessageProvider>();
        return services;
    }
}

// <copyright file="Dependencies.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.CodeAnalysis.Pdf.Configs;

namespace OffDotNet.CodeAnalysis.Pdf;

using Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using OffDotNet.CodeAnalysis.Diagnostics;
using OffDotNet.CodeAnalysis.Lexer;

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
    public static IServiceCollection AddPdfCodeAnalysis(this IServiceCollection services)
    {
        services.AddOptions<RootConfigurations>(RootConfigurations.SectionName).ValidateOnStart();

        services.AddCoreCodeAnalysis();
        services.AddSingleton<ILexer, Lexer.Lexer>();
        services.AddSingleton<IMessageProvider, MessageProvider>();
        return services;
    }
}

// <copyright file="WorkItemAttribute.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Tests.Attributes;

using System.Text.RegularExpressions;

/// <summary>
/// Attribute to associate a work item with a test class or method.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public partial class WorkItemAttribute : Attribute
{
    private const string UrlPrefix = "https://github.com/sunt-programator/off-dotnet/issues";

    /// <summary>
    /// Initializes a new instance of the <see cref="WorkItemAttribute"/> class with the specified ID and URL.
    /// </summary>
    /// <param name="id">The ID of the work item.</param>
    /// <param name="url">The URL of the work item.</param>
    public WorkItemAttribute(int id, string url)
    {
        Id = id;
        Url = url;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="WorkItemAttribute"/> class with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the work item.</param>
    public WorkItemAttribute(int id)
    {
        Id = id;
        Url = $"{UrlPrefix}/{id}";
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="WorkItemAttribute"/> class with the specified URL.
    /// </summary>
    /// <param name="url">The URL of the work item.</param>
    /// <exception cref="ArgumentException">Thrown when the URL is not in the correct format.</exception>
    public WorkItemAttribute(string url)
    {
        var match = MyRegex().Match(url);
        if (!match.Success || !int.TryParse(match.Groups["id"].Value, out var id))
        {
            throw new ArgumentException("The URL is not in the correct format.", nameof(url));
        }

        Id = id;
        Url = url;
    }

    /// <summary>
    /// Gets the ID of the work item.
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Gets the URL of the work item.
    /// </summary>
    public string Url { get; }

    /// <summary>
    /// Matches the URL to extract the work item ID.
    /// </summary>
    /// <returns>A <see cref="Regex"/> object for matching the work item URL.</returns>
    [GeneratedRegex(@"https://github\.com/sunt-programator/off-dotnet/issues/(?<id>\d+)")]
    private static partial Regex MyRegex();
}

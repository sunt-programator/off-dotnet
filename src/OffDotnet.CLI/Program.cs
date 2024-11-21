// <copyright file="Program.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotnet.Cli;

using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.Rendering;
using OffDotnet.Cli.Widgets;

public static class Program
{
    /// <summary>The PDF compiler service.</summary>
    /// <param name="invocationContext">The invocation context.</param>
    /// <returns>The exit code.</returns>
    internal static async Task<int> Main(InvocationContext invocationContext, string[] args)
    {
        var consoleRenderer = new ConsoleRenderer(
            invocationContext.Console,
            mode: invocationContext.BindingContext.OutputMode(),
            resetAfterRender: true);

        var region = new Region(0, 0);

        var inputArgument = new Argument<string>("input", "The PDF input text to parse.");
        var treeOption = new Option<bool>(["--tree", "-t"], "Show the syntax tree.");

        var rootCommand = new RootCommand("The PDF compiler service.") { inputArgument, treeOption, };

        rootCommand.SetHandler(
            (inputArgumentValue, treeOptionValue) =>
            {
                var tree = new TreeWidget($"SyntaxTree")
                {
                    RootNode =
                    {
                        Children =
                        [
                            new TreeNode
                            {
                                Name = $"{ForegroundColorSpan.Blue()}Expression: DictionaryEntryExpression",
                                Children =
                                [
                                    new TreeNode
                                    {
                                        Name = $"{ForegroundColorSpan.Blue()}Left: NameLiteralExpression",
                                        Children = [new TreeNode { Name = $"{ForegroundColorSpan.Green()}Token: /Type {StyleSpan.StandoutOn()} NameLiteralToken" }],
                                    },
                                    new TreeNode
                                    {
                                        Name = $"{ForegroundColorSpan.Blue()}Right: TrueLiteralExpression",
                                        Children = [new TreeNode { Name = $"{ForegroundColorSpan.Green()}Token: true {StyleSpan.StandoutOn()} TrueKeywordToken" }],
                                    },
                                ],
                            },
                        ],
                    },
                };

                consoleRenderer.RenderToRegion(tree.ToFormattableString(), region);
            },
            inputArgument,
            treeOption);

        return await rootCommand.InvokeAsync(args);
    }
}

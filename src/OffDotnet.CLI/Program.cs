using System.CommandLine;
using OffDotnet.Cli.Widgets;

var inputArgument = new Argument<string>("input", "The PDF input text to parse.");
var treeOption = new Option<bool>(["--tree", "-t"], "Show the syntax tree.");

var rootCommand = new RootCommand("The PDF compiler service.") { inputArgument, treeOption, };

rootCommand.SetHandler(
    (inputArgumentValue, treeOptionValue) =>
    {
        Console.WriteLine($"Input: {inputArgumentValue}\tTree: {treeOptionValue}");

        var tree = new TreeWidget("SyntaxTree")
        {
            RootNode =
            {
                Children =
                [
                    new TreeNode
                    {
                        Name = "Child 1",
                        Children =
                        [
                            new TreeNode { Name = "Child 1.1" },
                            new TreeNode { Name = "Child 1.2" },
                        ],
                    },
                    new TreeNode
                    {
                        Name = "Child 2",
                        Children =
                        [
                            new TreeNode { Name = "Child 2.1" },
                            new TreeNode { Name = "Child 2.2" },
                        ],
                    },
                ],
            },
        };
        Console.WriteLine(tree);
    },
    inputArgument,
    treeOption);

return await rootCommand.InvokeAsync(args);

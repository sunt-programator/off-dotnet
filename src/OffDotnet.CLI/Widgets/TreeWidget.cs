// <copyright file="TreeWidget.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotnet.Cli.Widgets;

using System.CommandLine.Rendering;
using System.Runtime.CompilerServices;
using TreeNodeTuple = (TreeNode Node, List<bool> IsLastList);

internal sealed class TreeWidget
{
    private const string Indent = "    ";
    private const string VerticalBar = "|   ";
    private const string MidBranch = "├── ";
    private const string EndBranch = "└── ";

    public TreeWidget(FormattableString rootName)
    {
        RootNode = new TreeNode { Name = rootName };
    }

    public TreeNode RootNode { get; }

    public FormattableString ToFormattableString()
    {
        var stack = new Stack<TreeNodeTuple>();
        var strings = new List<FormattableString>();
        stack.Push((RootNode, []));

        while (stack.Count > 0)
        {
            var (node, isLastList) = stack.Pop();

            // Write the current node
            strings.Add($"{ForegroundColorSpan.LightBlue()}{BuildPrefix(isLastList)}{ForegroundColorSpan.Reset()}");
            strings.Add(node.Name);
            strings.Add($"{ForegroundColorSpan.Reset()}{StyleSpan.AttributesOff()}\n");

            var children = node.Children;
            if (children.Count == 0)
            {
                continue;
            }

            for (var i = children.Count - 1; i >= 0; i--)
            {
                var child = children[i];
                var isChildLast = i == 0;

                isLastList.Add(!isChildLast);
                stack.Push((child, [..isLastList]));
                isLastList.RemoveAt(isLastList.Count - 1);
            }
        }

        return ConcatFormattableStrings(strings);
    }

    public override string ToString()
    {
        return ToFormattableString().ToString();
    }

    private static string BuildPrefix(List<bool> isLastList)
    {
        var prefixBuilder = new StringBuilder();
        for (var i = 0; i < isLastList.Count; i++)
        {
            if (i == isLastList.Count - 1)
            {
                prefixBuilder.Append(isLastList[i] ? EndBranch : MidBranch);
                continue;
            }

            prefixBuilder.Append(isLastList[i] ? Indent : VerticalBar);
        }

        return prefixBuilder.ToString();
    }

    private static FormattableString ConcatFormattableStrings(IEnumerable<FormattableString> strings)
    {
        var formattableStrings = strings.ToArray();
        var combinedFormat = string.Concat(formattableStrings.Select(s => s.Format));
        var combinedArgs = formattableStrings.SelectMany(s => s.GetArguments()).ToArray();
        return FormattableStringFactory.Create(combinedFormat, combinedArgs);
    }
}

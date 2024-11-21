// <copyright file="TreeWidget.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotnet.Cli.Widgets;

using TreeNodeTuple = (TreeNode Node, List<bool> IsLastList);

internal sealed class TreeWidget
{
    private const string Indent = "    ";
    private const string VerticalBar = "|   ";
    private const string MidBranch = "├── ";
    private const string EndBranch = "└── ";

    public TreeWidget(string rootName)
    {
        RootNode = new TreeNode { Name = rootName };
    }

    public TreeNode RootNode { get; }

    public override string ToString()
    {
        var sb = new StringBuilder();
        var stack = new Stack<TreeNodeTuple>();
        stack.Push((RootNode, []));

        while (stack.Count > 0)
        {
            var (node, isLastList) = stack.Pop();

            // Write the current node
            sb.Append(BuildPrefix(isLastList)).AppendLine(node.Name);

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

        return sb.ToString();
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
}

// <copyright file="TreeNode.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotnet.Cli.Widgets;

public sealed record TreeNode
{
    public required FormattableString Name { get; set; }

    public List<TreeNode> Children { get; set; } = [];
}

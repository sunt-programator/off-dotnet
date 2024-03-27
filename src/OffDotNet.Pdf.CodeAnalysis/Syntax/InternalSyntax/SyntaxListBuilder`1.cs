// <copyright file="SyntaxListBuilder`1.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

internal readonly struct SyntaxListBuilder<TNode>
    where TNode : GreenNode
{
    private const int DefaultCapacity = 8;
    private readonly SyntaxListBuilder _builder;

    internal SyntaxListBuilder(SyntaxListBuilder builder)
    {
        _builder = builder;
    }

    internal SyntaxListBuilder(int size)
        : this(new SyntaxListBuilder(size))
    {
    }

    public bool IsNull => _builder == null;

    public int Count => _builder.Count;

    public TNode this[int index]
    {
        get
        {
            var result = _builder[index];
            Debug.Assert(result != null, "We only allow assigning non-null nodes into us, and .Add filters null out. So we should never get null here.");
            return (TNode)result;
        }
    }

    public static implicit operator SyntaxListBuilder(SyntaxListBuilder<TNode> builder)
    {
        return builder._builder;
    }

    [SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract", Justification = "Can be null when using default operator")]
    public static implicit operator SyntaxList<TNode>(SyntaxListBuilder<TNode> builder)
    {
        return builder._builder != null ? builder.ToList() : default;
    }

    public static SyntaxListBuilder<TNode> Create()
    {
        return new SyntaxListBuilder<TNode>(DefaultCapacity);
    }

    public void Add(TNode? item)
    {
        _builder.Add(item);
    }

    [SuppressMessage("ReSharper", "CoVariantArrayConversion", Justification = "The array is covariant.")]
    public void AddRange(TNode[] items, int offset, int length)
    {
        _builder.AddRange(items, offset, length);
    }

    public void AddRange(SyntaxList<TNode> nodes)
    {
        _builder.AddRange(nodes);
    }

    public void Clear()
    {
        _builder.Clear();
    }

    public bool Any(SyntaxKind kind)
    {
        return _builder.Any(kind);
    }

    public GreenNode? ToListNode()
    {
        return _builder.ToListNode();
    }

    public SyntaxList<TNode> ToList()
    {
        return _builder.ToList<TNode>();
    }
}

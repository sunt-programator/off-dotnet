// <copyright file="GreenNode.Text.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using InternalUtilities;
using PooledObjects;

/// <summary>
/// Additional class containing text-related methods and properties for <see cref="GreenNode"/>.
/// </summary>
internal abstract partial class GreenNode
{
    public virtual string ToFullString()
    {
        return this.ToString(leading: true, trailing: true);
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return this.ToString(leading: false, trailing: false);
    }

    protected internal void WriteTo(TextWriter writer, bool leading, bool trailing)
    {
        var stack = ImmutableArrayBuilderObjectPool<(GreenNode Node, bool Leading, bool Trailing)>.Instance.Get();
        stack.Push((this, leading, trailing));
        ProcessStack(writer, stack);
        ImmutableArrayBuilderObjectPool<(GreenNode Node, bool Leading, bool Trailing)>.Instance.Return(stack);
    }

    protected virtual void WriteTriviaTo(TextWriter writer)
    {
        throw ExceptionUtilities.Unreachable();
    }

    protected virtual void WriteTokenTo(TextWriter writer, bool leading, bool trailing)
    {
        throw ExceptionUtilities.Unreachable();
    }

    [SuppressMessage("Roslynator", "RCS1233:Use short-circuiting operator.", Justification = "Reviewed.")]
    [SuppressMessage("Blocker Code Smell", "S2178:Short-circuit logic should be used in boolean contexts", Justification = "Reviewed.")]
    private static void ProcessStack(TextWriter writer, ImmutableArray<(GreenNode Node, bool Leading, bool Trailing)>.Builder stack)
    {
        while (stack.Count > 0)
        {
            (var currentNode, var currentLeading, var currentTrailing) = stack.Pop();

            if (currentNode.IsToken)
            {
                currentNode.WriteTokenTo(writer, currentLeading, currentTrailing);
                continue;
            }

            if (currentNode.IsTrivia)
            {
                currentNode.WriteTriviaTo(writer);
                continue;
            }

            var firstIndex = currentNode.GetFirstNonNullChildIndex();
            var lastIndex = currentNode.GetLastNonNullChildIndex();

            for (var i = lastIndex; i >= firstIndex; i--)
            {
                var child = currentNode.GetSlot(i);
                if (child == null)
                {
                    continue;
                }

                var first = i == firstIndex;
                var last = i == lastIndex;
                stack.Push((child, currentLeading | !first, currentTrailing | !last));
            }
        }
    }

    private string ToString(bool leading, bool trailing)
    {
        var stringBuilder = SharedObjectPools.StringBuilderPool.Get();
        TextWriter writer = new StringWriter(stringBuilder, System.Globalization.CultureInfo.InvariantCulture);
        this.WriteTo(writer, leading, trailing);
        var result = stringBuilder.ToString();
        SharedObjectPools.StringBuilderPool.Return(stringBuilder);
        return result;
    }
}

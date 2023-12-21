// <copyright file="GreenNode.Text.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using OffDotNet.Pdf.CodeAnalysis.InternalUtilities;
using OffDotNet.Pdf.CodeAnalysis.PooledObjects;

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

/// <summary>
/// Additional class containing text-related methods and properties for <see cref="GreenNode"/>.
/// </summary>
internal abstract partial class GreenNode
{
    public virtual string ToFullString()
    {
        return this.ToString(leading: true, trailing: true);
    }

    public override string ToString()
    {
        return this.ToString(leading: false, trailing: false);
    }

    protected internal void WriteTo(TextWriter writer, bool leading, bool trailing)
    {
        var stack = ArrayBuilderObjectPool<(GreenNode Node, bool Leading, bool Trailing)>.Instance.Get();
        stack.Push((this, leading, trailing));
        ProcessStack(writer, stack);
        ArrayBuilderObjectPool<(GreenNode Node, bool Leading, bool Trailing)>.Instance.Return(stack);
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
            (GreenNode? currentNode, bool currentLeading, bool currentTrailing) = stack.Pop();

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

            int firstIndex = currentNode.GetFirstNonNullChildIndex();
            int lastIndex = currentNode.GetLastNonNullChildIndex();

            for (int i = lastIndex; i >= firstIndex; i--)
            {
                var child = currentNode.GetSlot(i);
                if (child == null)
                {
                    continue;
                }

                bool first = i == firstIndex;
                bool last = i == lastIndex;
                stack.Push((child, currentLeading | !first, currentTrailing | !last));
            }
        }
    }

    private string ToString(bool leading, bool trailing)
    {
        StringBuilder stringBuilder = StringBuilderPool.Instance.Get();
        TextWriter writer = new StringWriter(stringBuilder, System.Globalization.CultureInfo.InvariantCulture);
        this.WriteTo(writer, leading, trailing);
        string result = stringBuilder.ToString();
        StringBuilderPool.Instance.Return(stringBuilder);
        return result;
    }
}

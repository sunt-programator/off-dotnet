// <copyright file="SyntaxTriviaListExtensions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax;

using System.Collections.Immutable;
using Collections;
using InternalSyntax;

/// <summary>Contains extension methods for the <see cref="SyntaxTriviaList"/> class.</summary>
public static class SyntaxTriviaListExtensions
{
    /// <summary>Converts the specified <see cref="ImmutableArray{SyntaxTrivia}.Builder"/> to a <see cref="SyntaxTriviaList"/>.</summary>
    /// <param name="builder">The builder to convert.</param>
    /// <returns>The converted <see cref="SyntaxTriviaList"/>.</returns>
    public static SyntaxTriviaList ToSyntaxTriviaList(this ImmutableArray<SyntaxTrivia>.Builder builder)
    {
        switch (builder.Count)
        {
            case 0: return [];
            case 1: return new SyntaxTriviaList(default, builder[0].UnderlyingNode, position: 0, index: 0);
            case 2:
                return new SyntaxTriviaList(
                    default,
                    InternalSyntax.SyntaxFactory.List(builder[0].UnderlyingNode, builder[1].UnderlyingNode),
                    position: 0,
                    index: 0);
            case 3:
                return new SyntaxTriviaList(
                    default,
                    InternalSyntax.SyntaxFactory.List(
                        builder[0].UnderlyingNode,
                        builder[1].UnderlyingNode,
                        builder[2].UnderlyingNode),
                    position: 0,
                    index: 0);
            default:
                {
                    var tmp = new ArrayElement<GreenNode>[builder.Count];
                    for (var i = 0; i < builder.Count; i++)
                    {
                        tmp[i]._value = builder[i].UnderlyingNode;
                    }

                    return new SyntaxTriviaList(
                        default,
                        InternalSyntax.SyntaxFactory.List(tmp),
                        position: 0,
                        index: 0);
                }
        }
    }
}

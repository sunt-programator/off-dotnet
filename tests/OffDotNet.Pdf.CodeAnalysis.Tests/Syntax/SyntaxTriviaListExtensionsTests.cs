// <copyright file="SyntaxTriviaListExtensionsTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax;

using System.Collections.Immutable;
using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using SyntaxFactory = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxFactory;
using SyntaxToken = OffDotNet.Pdf.CodeAnalysis.Syntax.SyntaxToken;
using SyntaxTrivia = OffDotNet.Pdf.CodeAnalysis.Syntax.SyntaxTrivia;

public class SyntaxTriviaListExtensionsTests
{
    private readonly SyntaxToken _syntaxToken;
    private readonly GreenNode _triviaList;

    public SyntaxTriviaListExtensionsTests()
    {
        var syntaxTokenUnderlyingNode = SyntaxFactory.Token(SyntaxKind.NumericLiteralToken, "123", 123, null, null);
        _syntaxToken = new SyntaxToken(null, syntaxTokenUnderlyingNode, 10, 6);

        var trivia1 = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " ");
        var trivia2 = SyntaxFactory.Trivia(SyntaxKind.SingleLineCommentTrivia, "% comment");
        var trivia3 = SyntaxFactory.Trivia(SyntaxKind.EndOfLineTrivia, "\n");

        _triviaList = SyntaxListBuilder.Create()
            .Add(SyntaxFactory.Token(SyntaxKind.TrueKeyword, trivia1, trivia2))
            .Add(SyntaxFactory.Token(SyntaxKind.FalseKeyword, trivia1, trivia3))
            .Add(SyntaxFactory.Token(SyntaxKind.NullKeyword, trivia2, trivia3))
            .ToListNode()!;
    }

    [Fact(DisplayName = $"The ToSyntaxTriviaList() method must return a default {nameof(SyntaxTriviaList)} when the list is empty.")]
    public void ToListMethod_WithEmptyList_MustReturnDefaultSyntaxTriviaList()
    {
        // Arrange
        var builder = ImmutableArray.CreateBuilder<SyntaxTrivia>();

        // Act
        var result = builder.ToSyntaxTriviaList();

        // Assert
        Assert.Equal(default, result);
    }

    [Fact(DisplayName = $"The ToSyntaxTriviaList() method must return a {nameof(SyntaxTriviaList)} with one element when the list has one element.")]
    public void ToListMethod_WithOneElement_MustReturnSyntaxTriviaListWithOneElement()
    {
        // Arrange
        var builder = ImmutableArray.CreateBuilder<SyntaxTrivia>();
        builder.Add(new SyntaxTrivia(_syntaxToken, _triviaList.GetSlot(0)!, 56, 10));

        // Act
        var result = builder.ToSyntaxTriviaList();

        // Assert
        Assert.Single(result);
        Assert.Equal(SyntaxKind.TrueKeyword, result[0].Kind);
        Assert.Equal(0, result.Position);
        Assert.Equal(0, result.Index);
    }

    [Fact(DisplayName = $"The ToSyntaxTriviaList() method must return a {nameof(SyntaxTriviaList)} with 2 elements when the list has 2 elements.")]
    public void ToListMethod_WithTwoElements_MustReturnSyntaxTriviaListWithTwoElements()
    {
        // Arrange
        var builder = ImmutableArray.CreateBuilder<SyntaxTrivia>();
        builder.Add(new SyntaxTrivia(_syntaxToken, _triviaList.GetSlot(0)!, 56, 10));
        builder.Add(new SyntaxTrivia(_syntaxToken, _triviaList.GetSlot(1)!, 70, 11));

        // Act
        var result = builder.ToSyntaxTriviaList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal(SyntaxKind.TrueKeyword, result[0].Kind);
        Assert.Equal(SyntaxKind.FalseKeyword, result[1].Kind);
        Assert.Equal(0, result.Position);
        Assert.Equal(0, result.Index);
    }

    [Fact(DisplayName = $"The ToSyntaxTriviaList() method must return a {nameof(SyntaxTriviaList)} with 3 elements when the list has 3 elements.")]
    public void ToListMethod_WithThreeElements_MustReturnSyntaxTriviaListWithThreeElements()
    {
        // Arrange
        var builder = ImmutableArray.CreateBuilder<SyntaxTrivia>();
        builder.Add(new SyntaxTrivia(_syntaxToken, _triviaList.GetSlot(0)!, 56, 10));
        builder.Add(new SyntaxTrivia(_syntaxToken, _triviaList.GetSlot(1)!, 70, 11));
        builder.Add(new SyntaxTrivia(_syntaxToken, _triviaList.GetSlot(2)!, 80, 12));

        // Act
        var result = builder.ToSyntaxTriviaList();

        // Assert
        Assert.Equal(3, result.Count);
        Assert.Equal(SyntaxKind.TrueKeyword, result[0].Kind);
        Assert.Equal(SyntaxKind.FalseKeyword, result[1].Kind);
        Assert.Equal(SyntaxKind.NullKeyword, result[2].Kind);
        Assert.Equal(0, result.Position);
        Assert.Equal(0, result.Index);
    }

    [Fact(DisplayName =
        $"The ToSyntaxTriviaList() method must return a {nameof(SyntaxTriviaList)} with more than 3 elements when the list has more than 3 elements.")]
    public void ToListMethod_WithMoreThanThreeElements_MustReturnSyntaxTriviaListWithMoreThanThreeElements()
    {
        // Arrange
        var builder = ImmutableArray.CreateBuilder<SyntaxTrivia>();
        builder.Add(new SyntaxTrivia(_syntaxToken, _triviaList.GetSlot(0)!, 56, 10));
        builder.Add(new SyntaxTrivia(_syntaxToken, _triviaList.GetSlot(1)!, 70, 11));
        builder.Add(new SyntaxTrivia(_syntaxToken, _triviaList.GetSlot(2)!, 80, 12));
        builder.Add(new SyntaxTrivia(_syntaxToken, _triviaList.GetSlot(0)!, 90, 13));

        // Act
        var result = builder.ToSyntaxTriviaList();

        // Assert
        Assert.Equal(4, result.Count);
        Assert.Equal(SyntaxKind.TrueKeyword, result[0].Kind);
        Assert.Equal(SyntaxKind.FalseKeyword, result[1].Kind);
        Assert.Equal(SyntaxKind.NullKeyword, result[2].Kind);
        Assert.Equal(SyntaxKind.TrueKeyword, result[3].Kind);
        Assert.Equal(0, result.Position);
        Assert.Equal(0, result.Index);
    }
}

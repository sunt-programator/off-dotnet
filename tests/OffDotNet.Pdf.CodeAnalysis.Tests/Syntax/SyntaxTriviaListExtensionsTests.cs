// <copyright file="SyntaxTriviaListExtensionsTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax;

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

    [Fact(DisplayName = "The IndexOf(SyntaxTrivia) must return -1 when the list is empty.")]
    public void IndexOfMethod_WithEmptyList_MustReturnMinusOne()
    {
        // Arrange
        var list = SyntaxTriviaList.Empty;

        // Act
        var index = list.IndexOf(default(SyntaxTrivia));

        // Assert
        Assert.Equal(-1, index);
    }

    [Fact(DisplayName = "The IndexOf(SyntaxTrivia) must return -1 when the trivia is not found.")]
    public void IndexOfMethod_WithNotFoundTrivia_MustReturnMinusOne()
    {
        // Arrange
        var list = new SyntaxTriviaList(_syntaxToken, _triviaList, 56, 10);
        var triviaToFind = new SyntaxTrivia(_syntaxToken, _triviaList.GetSlot(0)!, position: 999, index: 999);

        // Act
        var index = list.IndexOf(triviaToFind);

        // Assert
        Assert.Equal(-1, index);
    }

    [Fact(DisplayName = "The IndexOf(SyntaxTrivia) must return the index of the trivia if it is found.")]
    public void IndexOfMethod_WithFoundTrivia_MustReturnIndex()
    {
        // Arrange
        var list = new SyntaxTriviaList(_syntaxToken, _triviaList, 56, 10);
        var triviaToFind = new SyntaxTrivia(_syntaxToken, _triviaList.GetSlot(0)!, position: 56, index: 10);

        // Act
        var index = list.IndexOf(triviaToFind);

        // Assert
        Assert.Equal(0, index);
    }

    [Fact(DisplayName = "The IndexOf(SyntaxKind) method must return -1 when the list is empty.")]
    public void IndexOfMethod_WithSyntaxKindArgument_WithEmptyList_MustReturnMinusOne()
    {
        // Arrange
        var list = SyntaxTriviaList.Empty;

        // Act
        var index = list.IndexOf(SyntaxKind.WhitespaceTrivia);

        // Assert
        Assert.Equal(-1, index);
    }

    [Fact(DisplayName = "The IndexOf(SyntaxKind) method must return -1 when the trivia with the specified kind is not found.")]
    public void IndexOfMethod_WithSyntaxKindArgument_WithNotFoundTrivia_MustReturnMinusOne()
    {
        // Arrange
        var list = new SyntaxTriviaList(_syntaxToken, _triviaList, 56, 10);

        // Act
        var index = list.IndexOf(SyntaxKind.BadToken);

        // Assert
        Assert.Equal(-1, index);
    }

    [Fact(DisplayName = "The IndexOf(SyntaxKind) method must return the index of the first trivia with the specified kind.")]
    public void IndexOfMethod_WithSyntaxKindArgument_WithFoundTrivia_MustReturnIndex()
    {
        // Arrange
        var list = new SyntaxTriviaList(_syntaxToken, _triviaList, 56, 10);

        // Act
        var index = list.IndexOf(SyntaxKind.TrueKeyword);

        // Assert
        Assert.Equal(0, index);
    }

    [Theory(DisplayName = $"The RemoveAt(int) method must throw an {nameof(ArgumentOutOfRangeException)} when the index is out of range.")]
    [InlineData(-1)]
    [InlineData(-10)]
    [InlineData(3)]
    [InlineData(10)]
    public void RemoveAtMethod_OutOfRangeIndex_MustThrowArgumentOutOfRangeException(int index)
    {
        // Arrange
        var list = new SyntaxTriviaList(_syntaxToken, _triviaList, 56, 10);

        // Act
        void Act() => list.RemoveAt(index);

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(Act);
    }

    [Fact(DisplayName = "The RemoveAt(int) method must remove the trivia at the specified index.")]
    public void RemoveAtMethod_WithValidIndex_MustRemoveTrivia()
    {
        // Arrange
        var list = new SyntaxTriviaList(_syntaxToken, _triviaList, 56, 10);

        // Act
        var result = list.RemoveAt(1);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal(SyntaxKind.TrueKeyword, result[0].Kind);
        Assert.Equal(SyntaxKind.NullKeyword, result[1].Kind);
        Assert.Equal(56, result.Position);
        Assert.Equal(10, result.Index);
    }

    [Fact(DisplayName = "The RemoveAt(SyntaxTrivia) method must remove the trivia from the list if it is found.")]
    public void RemoveAtMethod_WithFoundTrivia_MustRemoveTrivia()
    {
        // Arrange
        var list = new SyntaxTriviaList(_syntaxToken, _triviaList, 56, 10);
        var triviaToRemove = new SyntaxTrivia(_syntaxToken, _triviaList.GetSlot(1)!, position: 70, index: 11);

        // Act
        var result = list.RemoveAt(triviaToRemove);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal(SyntaxKind.TrueKeyword, result[0].Kind);
        Assert.Equal(SyntaxKind.NullKeyword, result[1].Kind);
        Assert.Equal(56, result.Position);
        Assert.Equal(10, result.Index);
    }

    [Fact(DisplayName = "The RemoveAt(SyntaxTrivia) method must return the original list if the trivia is not found.")]
    public void RemoveAtMethod_WithNotFoundTrivia_MustReturnOriginalList()
    {
        // Arrange
        var list = new SyntaxTriviaList(_syntaxToken, _triviaList, 56, 10);

        // Act
        var result = list.RemoveAt(default(SyntaxTrivia));

        // Assert
        Assert.Equal(3, result.Count);
        Assert.Equal(SyntaxKind.TrueKeyword, result[0].Kind);
        Assert.Equal(SyntaxKind.FalseKeyword, result[1].Kind);
        Assert.Equal(SyntaxKind.NullKeyword, result[2].Kind);
        Assert.Equal(56, result.Position);
        Assert.Equal(10, result.Index);
    }

    [Theory(DisplayName =
        $"The InsertRange(int, IEnumerable<SyntaxTrivia>) method must throw an {nameof(ArgumentOutOfRangeException)} when the index is out of range.")]
    [InlineData(-1)]
    [InlineData(-10)]
    [InlineData(4)]
    [InlineData(10)]
    public void InsertRangeMethod_OutOfRangeIndex_MustThrowArgumentOutOfRangeException(int index)
    {
        // Arrange
        var list = new SyntaxTriviaList(_syntaxToken, _triviaList, 56, 10);

        // Act
        void Act() => list.InsertRange(index, []);

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(Act);
    }

    [Fact(DisplayName = "The InsertRange(int, IEnumerable<SyntaxTrivia>) must return the same list when the trivia collection is empty.")]
    public void InsertRangeMethod_WithEmptyCollection_MustReturnSameList()
    {
        // Arrange
        var list = new SyntaxTriviaList(_syntaxToken, _triviaList, 56, 10);

        // Act
        var result = list.InsertRange(1, []);

        // Assert
        Assert.Equal(list, result);
    }
}

// <copyright file="GreenNodeTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax;

using System.Collections;
using OffDotNet.Pdf.CodeAnalysis.Collections;
using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using OffDotNet.Pdf.CodeAnalysis.Text;
using SyntaxFactory = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxFactory;
using SyntaxToken = OffDotNet.Pdf.CodeAnalysis.Syntax.SyntaxToken;
using SyntaxTrivia = OffDotNet.Pdf.CodeAnalysis.Syntax.SyntaxTrivia;

public partial class GreenNodeTests
{
    private readonly SyntaxToken _syntaxToken;
    private readonly GreenNode _triviaList;

    public GreenNodeTests()
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

    [Fact(DisplayName = $"The {nameof(SyntaxTriviaList.Empty)} field must return default value")]
    public void EmptyField_MustReturnDefaultValue()
    {
        // Arrange

        // Act
        var actual = SyntaxTriviaList.Empty;

        // Assert
        Assert.Equal(default, actual);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxTriviaList.Token)} property must return the syntax token")]
    public void TokenProperty_MustReturnSyntaxToken()
    {
        // Arrange
        SyntaxTriviaList syntaxTriviaList = new(_syntaxToken, _triviaList, 0);

        // Act
        var actual = syntaxTriviaList.Token;

        // Assert
        Assert.Equal(_syntaxToken, actual);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxTriviaList.UnderlyingUnderlyingNode)} property must return the green node with trivia list")]
    public void UnderlyingNodeProperty_MustReturnGreenNodeWithTriviaList()
    {
        // Arrange
        SyntaxTriviaList syntaxTriviaList = new(_syntaxToken, _triviaList, 0);

        // Act
        var actual = syntaxTriviaList.UnderlyingUnderlyingNode;

        // Assert
        Assert.Same(_triviaList, actual);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxTriviaList.Position)} property must return the absolute position of the trivia list in the source text")]
    public void PositionProperty_MustReturnAbsolutePositionOfTriviaListInSourceText()
    {
        // Arrange
        const int Position = 5;
        SyntaxTriviaList syntaxTriviaList = new(_syntaxToken, _triviaList, Position);

        // Act
        var actual = syntaxTriviaList.Position;

        // Assert
        Assert.Equal(Position, actual);
    }

    [Fact(DisplayName =
        $"The {nameof(SyntaxTriviaList.Position)} property must return the absolute position of the syntax token if not provided in constructor")]
    public void PositionProperty_MustReturnAbsolutePositionOfSyntaxTokenIfNotProvided()
    {
        // Arrange
        SyntaxTriviaList syntaxTriviaList = new(_syntaxToken, _triviaList, 10);

        // Act
        var actual = syntaxTriviaList.Position;

        // Assert
        Assert.Equal(_syntaxToken.Position, actual);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxTriviaList.Index)} property must return the index of the trivia list in the parent list")]
    public void IndexProperty_MustReturnIndexInParentList()
    {
        // Arrange
        const int Index = 3;
        SyntaxTriviaList syntaxTriviaList = new(_syntaxToken, _triviaList, 0, Index);

        // Act
        var actual = syntaxTriviaList.Index;

        // Assert
        Assert.Equal(Index, actual);
    }

    [Fact(DisplayName = $"The constructor with {nameof(SyntaxTrivia)} parameter must initialize the properties")]
    public void ConstructorWithSyntaxTriviaParameter_MustInitializeProperties()
    {
        // Arrange
        var triviaNode = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " ");
        var trivia = new SyntaxTrivia(default, triviaNode, 7, 4);

        // Act
        var actual = new SyntaxTriviaList(trivia);

        // Assert
        Assert.Equal(default, actual.Token);
        Assert.Same(triviaNode, actual.UnderlyingUnderlyingNode);
        Assert.Equal(0, actual.Position);
        Assert.Equal(0, actual.Index);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxTriviaList.Count)} property must return 0 if the underlying node is null")]
    public void CountProperty_MustReturnZeroIfUnderlyingNodeIsNull()
    {
        // Arrange
        SyntaxTriviaList syntaxTriviaList = new(_syntaxToken, null, 0);

        // Act
        var actual = syntaxTriviaList.Count;

        // Assert
        Assert.Equal(0, actual);
    }

    [Fact(DisplayName =
        $"The {nameof(SyntaxTriviaList.Count)} property must return the slot count of the underlying node if kind is {nameof(SyntaxKind.List)}")]
    public void CountProperty_MustReturnSlotCountIfKindIsList()
    {
        // Arrange
        SyntaxTriviaList syntaxTriviaList = new(_syntaxToken, _triviaList, 0);

        // Act
        var actual = syntaxTriviaList.Count;

        // Assert
        Assert.Equal(3, actual);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxTriviaList.Count)} property must return 1 if the underlying node is not a list")]
    public void CountProperty_MustReturnOneIfUnderlyingNodeIsNotList()
    {
        // Arrange
        var triviaNode = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " ");
        SyntaxTriviaList syntaxTriviaList = new(_syntaxToken, triviaNode, 0);

        // Act
        var actual = syntaxTriviaList.Count;

        // Assert
        Assert.Equal(1, actual);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxTriviaList.Span)} property must return the default value if the underlying node is null")]
    public void SpanProperty_MustReturnDefaultValueIfUnderlyingNodeIsNull()
    {
        // Arrange
        SyntaxTriviaList syntaxTriviaList = new(_syntaxToken, null, 0);

        // Act
        var actual = syntaxTriviaList.Span;

        // Assert
        Assert.Equal(default, actual);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxTriviaList.FullSpan)} property must return the default value if the underlying node is null")]
    public void FullSpanProperty_MustReturnDefaultValueIfUnderlyingNodeIsNull()
    {
        // Arrange
        SyntaxTriviaList syntaxTriviaList = new(_syntaxToken, null, 0);

        // Act
        var actual = syntaxTriviaList.FullSpan;

        // Assert
        Assert.Equal(default, actual);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxTriviaList.Span)} property must return the span of the trivia list, " +
                        "not including the leading and trailing trivia of the first and last elements")]
    public void SpanProperty_MustReturnSpanOfTriviaList_NotIncludingLeadingAndTrailingTrivia()
    {
        // Arrange
        SyntaxTriviaList syntaxTriviaList = new(_syntaxToken, _triviaList, 24);
        var expected = new TextSpan(25, 33);

        // Act
        var actual = syntaxTriviaList.Span;

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxTriviaList.FullSpan)} property must return the span of the trivia list, " +
                        "including the leading and trailing trivia of the first and last elements")]
    public void FullSpanProperty_MustReturnSpanOfTriviaList_IncludingLeadingAndTrailingTrivia()
    {
        // Arrange
        SyntaxTriviaList syntaxTriviaList = new(_syntaxToken, _triviaList, 24);
        var expected = new TextSpan(24, 35);

        // Act
        var actual = syntaxTriviaList.FullSpan;

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "The ToString() method must return empty string if the underlying node is null")]
    public void ToStringMethod_MustReturnEmptyStringIfUnderlyingNodeIsNull()
    {
        // Arrange
        SyntaxTriviaList syntaxTriviaList = new(_syntaxToken, null, 0);

        // Act
        var actual = syntaxTriviaList.ToString();

        // Assert
        Assert.Equal(string.Empty, actual);
    }

    [Fact(DisplayName = "The ToString() method must return the text of the trivia list")]
    public void ToStringMethod_MustReturnTextOfTriviaList()
    {
        // Arrange
        SyntaxTriviaList syntaxTriviaList = new(_syntaxToken, _triviaList, 0);
        const string Expected = "true% comment false\n% commentnull";

        // Act
        var actual = syntaxTriviaList.ToString();

        // Assert
        Assert.Equal(Expected, actual);
    }

    [Fact(DisplayName = "The ToFullString() method must return empty string if the underlying node is null")]
    public void ToFullStringMethod_MustReturnEmptyStringIfUnderlyingNodeIsNull()
    {
        // Arrange
        SyntaxTriviaList syntaxTriviaList = new(_syntaxToken, null, 0);

        // Act
        var actual = syntaxTriviaList.ToFullString();

        // Assert
        Assert.Equal(string.Empty, actual);
    }

    [Fact(DisplayName = "The ToFullString() method must return the text of the trivia list")]
    public void ToFullStringMethod_MustReturnTextOfTriviaList()
    {
        // Arrange
        SyntaxTriviaList syntaxTriviaList = new(_syntaxToken, _triviaList, 0);
        const string Expected = " true% comment false\n% commentnull\n";

        // Act
        var actual = syntaxTriviaList.ToFullString();

        // Assert
        Assert.Equal(Expected, actual);
    }

    [Fact(DisplayName = $"The structure must implement the {nameof(IEquatable<SyntaxTriviaList>)} interface")]
    public void Structure_MustImplementIEquatable()
    {
        // Arrange

        // Act
        SyntaxTriviaList syntaxTriviaList = new(_syntaxToken, _triviaList, 0);

        // Assert
        Assert.IsAssignableFrom<IEquatable<SyntaxTriviaList>>(syntaxTriviaList);
    }

    [Fact(DisplayName = "The Equals() method must return true if the objects are equal")]
    public void EqualsMethod_MustReturnTrueIfObjectsAreEqual()
    {
        // Arrange
        SyntaxTriviaList syntaxTriviaList1 = new(_syntaxToken, _triviaList, 10, 5);
        SyntaxTriviaList syntaxTriviaList2 = new(_syntaxToken, _triviaList, 10, 5);

        // Act
        var actual1 = syntaxTriviaList1.Equals(syntaxTriviaList2);
        var actual2 = syntaxTriviaList1.Equals((object)syntaxTriviaList2);
        var actual3 = syntaxTriviaList1 == syntaxTriviaList2;
        var actual4 = syntaxTriviaList1 != syntaxTriviaList2;

        // Assert
        Assert.True(actual1);
        Assert.True(actual2);
        Assert.True(actual3);
        Assert.False(actual4);
    }

    [Fact(DisplayName = "The Equals() method must return false if the underlying nodes are different")]
    public void EqualsMethod_MustReturnFalseIfUnderlyingNodesAreDifferent()
    {
        // Arrange
        SyntaxTriviaList syntaxTriviaList1 = new(_syntaxToken, _triviaList, 10, 5);
        SyntaxTriviaList syntaxTriviaList2 = new(_syntaxToken, null, 10, 5);

        // Act
        var actual1 = syntaxTriviaList1.Equals(syntaxTriviaList2);
        var actual2 = syntaxTriviaList1.Equals((object)syntaxTriviaList2);
        var actual3 = syntaxTriviaList1 == syntaxTriviaList2;
        var actual4 = syntaxTriviaList1 != syntaxTriviaList2;

        // Assert
        Assert.False(actual1);
        Assert.False(actual2);
        Assert.False(actual3);
        Assert.True(actual4);
    }

    [Fact(DisplayName = "The Equals() method must return false if the indexes are different")]
    public void EqualsMethod_MustReturnFalseIfIndexesAreDifferent()
    {
        // Arrange
        SyntaxTriviaList syntaxTriviaList1 = new(_syntaxToken, _triviaList, 10, 5);
        SyntaxTriviaList syntaxTriviaList2 = new(_syntaxToken, _triviaList, 10, 6);

        // Act
        var actual1 = syntaxTriviaList1.Equals(syntaxTriviaList2);
        var actual2 = syntaxTriviaList1.Equals((object)syntaxTriviaList2);
        var actual3 = syntaxTriviaList1 == syntaxTriviaList2;
        var actual4 = syntaxTriviaList1 != syntaxTriviaList2;

        // Assert
        Assert.False(actual1);
        Assert.False(actual2);
        Assert.False(actual3);
        Assert.True(actual4);
    }

    [Fact(DisplayName = "The Equals() method must return false if the syntax tokens are different")]
    public void EqualsMethod_MustReturnFalseIfSyntaxTokensAreDifferent()
    {
        // Arrange
        var syntaxTokenUnderlyingNode = SyntaxFactory.Token(SyntaxKind.NumericLiteralToken, "123", 123, null, null);
        var parentToken1 = new SyntaxToken(null, syntaxTokenUnderlyingNode, 10, 6);
        var parentToken2 = new SyntaxToken(null, syntaxTokenUnderlyingNode, 10, 7);
        SyntaxTriviaList syntaxTriviaList1 = new(parentToken1, _triviaList, 10, 5);
        SyntaxTriviaList syntaxTriviaList2 = new(parentToken2, _triviaList, 10, 5);

        // Act
        var actual1 = syntaxTriviaList1.Equals(syntaxTriviaList2);
        var actual2 = syntaxTriviaList1.Equals((object)syntaxTriviaList2);
        var actual3 = syntaxTriviaList1 == syntaxTriviaList2;
        var actual4 = syntaxTriviaList1 != syntaxTriviaList2;

        // Assert
        Assert.False(actual1);
        Assert.False(actual2);
        Assert.False(actual3);
        Assert.True(actual4);
    }

    [Fact(DisplayName = "The GetHashCode() method must return the hash code of the syntax token, underlying node, and index")]
    public void GetHashCodeMethod_MustReturnHashCodeOfSyntaxTokenUnderlyingNodeAndIndex()
    {
        // Arrange
        SyntaxTriviaList syntaxTriviaList = new(_syntaxToken, _triviaList, 10, 5);
        var expected = HashCode.Combine(_syntaxToken, _triviaList, 5);

        // Act
        var actual = syntaxTriviaList.GetHashCode();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = $"The structure must implement the {nameof(IReadOnlyList<SyntaxTrivia>)} interface")]
    public void Structure_MustImplementIReadOnlyList()
    {
        // Arrange

        // Act
        SyntaxTriviaList syntaxTriviaList = new(_syntaxToken, _triviaList, 0);

        // Assert
        Assert.IsAssignableFrom<IReadOnlyList<SyntaxTrivia>>(syntaxTriviaList);
    }

    [Fact(DisplayName = "The Indexer must throw ArgumentOutOfRangeException if the index is less than 0")]
    public void Indexer_MustThrowIndexOutOfRangeExceptionIfIndexIsLessThanZero()
    {
        // Arrange
        SyntaxTriviaList syntaxTriviaList = new(_syntaxToken, _triviaList, 0);

        // Act
        void Action() => _ = syntaxTriviaList[-1];

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(Action);
    }

    [Fact(DisplayName = "The Indexer must throw ArgumentOutOfRangeException if the underlying node is null")]
    public void Indexer_MustThrowIndexOutOfRangeExceptionIfUnderlyingNodeIsNull()
    {
        // Arrange
        SyntaxTriviaList syntaxTriviaList = new(_syntaxToken, null, 0);

        // Act
        void Action() => _ = syntaxTriviaList[0];

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(Action);
    }

    [Fact(DisplayName = "The Indexer must throw ArgumentOutOfRangeException if the underlying node is not a list and the index is not 0")]
    public void Indexer_MustThrowIndexOutOfRangeExceptionIfUnderlyingNodeIsNotListAndIndexIsNotZero()
    {
        // Arrange
        var triviaNode = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " ");
        SyntaxTriviaList syntaxTriviaList = new(_syntaxToken, triviaNode, 0);

        // Act
        void Action() => _ = syntaxTriviaList[1];

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(Action);
    }

    [Fact(DisplayName = "The Indexer must throw ArgumentOutOfRangeException if the index is greater than the slot count")]
    public void Indexer_MustThrowIndexOutOfRangeExceptionIfIndexIsGreaterThanSlotCount()
    {
        // Arrange
        SyntaxTriviaList syntaxTriviaList = new(_syntaxToken, _triviaList, 0);

        // Act
        void Action() => _ = syntaxTriviaList[3];

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(Action);
    }

    [Fact(DisplayName = "The Indexer must return the syntax trivia if the underlying node is not a list")]
    public void Indexer_MustReturnSyntaxTriviaIfUnderlyingNodeIsNotList()
    {
        // Arrange
        var triviaNode = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " ");
        SyntaxTriviaList syntaxTriviaList = new(_syntaxToken, triviaNode, 56);
        var expected = new SyntaxTrivia(_syntaxToken, triviaNode, 56, 0);

        // Act
        var actual = syntaxTriviaList[0];

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "The Indexer must return the syntax trivia at the specified index")]
    public void Indexer_MustReturnSyntaxTriviaAtSpecifiedIndex()
    {
        // Arrange
        SyntaxTriviaList syntaxTriviaList = new(_syntaxToken, _triviaList, 56);
        var expected = new SyntaxTrivia(_syntaxToken, _triviaList.GetSlot(1)!, 70, 1);

        // Act
        var actual = syntaxTriviaList[1];

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "The GetEnumerator() method must return an empty enumerator if the underlying node is null")]
    public void GetEnumeratorMethod_MustReturnEmptyEnumeratorIfUnderlyingNodeIsNull()
    {
        // Arrange
        SyntaxTriviaList syntaxTriviaList = new(_syntaxToken, null, 0);

        // Act
        var actual1 = (syntaxTriviaList as IEnumerable).GetEnumerator();
        var actual2 = (syntaxTriviaList as IEnumerable<SyntaxTrivia>).GetEnumerator();

        // Assert
        Assert.Equal(EmptyEnumerator<SyntaxTrivia>.Instance, actual1);
        Assert.Equal(EmptyEnumerator<SyntaxTrivia>.Instance, actual2);
    }

    [Fact(DisplayName = "The GetEnumerator() method must return an enumerator for the trivia list")]
    public void GetEnumeratorMethod_MustReturnEnumeratorForTriviaList()
    {
        // Arrange
        SyntaxTriviaList syntaxTriviaList = new(_syntaxToken, _triviaList, 0);

        // Act
        var actual1 = (syntaxTriviaList as IEnumerable).GetEnumerator();
        var actual2 = (syntaxTriviaList as IEnumerable<SyntaxTrivia>).GetEnumerator();
        var actual3 = syntaxTriviaList.GetEnumerator();

        // Assert
        Assert.IsType<SyntaxTriviaList.EnumeratorImpl>(actual1);
        Assert.IsType<SyntaxTriviaList.EnumeratorImpl>(actual2);
        Assert.IsType<SyntaxTriviaList.Enumerator>(actual3);
    }
}

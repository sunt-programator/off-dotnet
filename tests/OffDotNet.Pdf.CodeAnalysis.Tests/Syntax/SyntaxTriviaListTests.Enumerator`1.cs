// <copyright file="SyntaxTriviaListTests.Enumerator`1.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax;

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using OffDotNet.Pdf.CodeAnalysis.Syntax;
using SyntaxFactory = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxFactory;
using SyntaxTrivia = OffDotNet.Pdf.CodeAnalysis.Syntax.SyntaxTrivia;

[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1601:Partial elements should be documented", Justification = "Test file")]
[SuppressMessage("ReSharper", "GenericEnumeratorNotDisposed", Justification = "Test file")]
public partial class GreenNodeTests
{
    [Fact(DisplayName = $"The struct must implement the {nameof(IEnumerator<SyntaxTrivia>)} interface")]
    public void EnumeratorImpl_MustImplementIEnumeratorOfSyntaxTrivia()
    {
        // Arrange
        SyntaxTriviaList.EnumeratorImpl enumerator = new(default);

        // Act

        // Assert
        Assert.IsAssignableFrom<IEnumerator<SyntaxTrivia>>(enumerator);
    }

    [Fact(DisplayName = "The Dispose() method should not do anything")]
    public void EnumeratorImpl_DisposeMethod_ShouldNotDoAnything()
    {
        // Arrange
        SyntaxTriviaList.EnumeratorImpl enumerator = new(default);

        // Act
        enumerator.Dispose();

        // Assert
        Assert.True(true);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxTriviaList.EnumeratorImpl.Current)} property must throw a {nameof(InvalidOperationException)}")]
    public void EnumeratorImpl_CurrentProperty_MustThrowInvalidOperationException()
    {
        // Arrange
        SyntaxTriviaList.EnumeratorImpl enumerator = new(default);

        // Act
        void Action1() => _ = enumerator.Current;
        void Action2() => _ = (SyntaxTrivia)(enumerator as IEnumerator).Current;

        // Assert
        Assert.Throws<InvalidOperationException>(Action1);
        Assert.Throws<InvalidOperationException>(Action2);
    }

    [Fact(DisplayName = "The MoveNext() method should return false if there is no trivia in the list")]
    public void EnumeratorImpl_MoveNextMethod_ShouldReturnFalse_IfThereIsNoTriviaInTheList()
    {
        // Arrange
        SyntaxTriviaList.EnumeratorImpl enumerator = new(default);

        // Act
        var result = enumerator.MoveNext();

        // Assert
        Assert.False(result);
    }

    [Fact(DisplayName = "The MoveNext() method should return false if the enumerator is at the end of the list")]
    public void EnumeratorImpl_MoveNextMethod_ShouldReturnFalse_IfEnumeratorIsAtTheEndOfTheList()
    {
        // Arrange
        var syntaxTriviaList = new SyntaxTriviaList(_syntaxToken, _triviaList, 56) as IEnumerable<SyntaxTrivia>;
        var enumerator = syntaxTriviaList.GetEnumerator();

        enumerator.MoveNext();
        enumerator.MoveNext();
        enumerator.MoveNext();

        // Act
        var result = enumerator.MoveNext();

        // Assert
        Assert.False(result);
    }

    [Fact(DisplayName = "The MoveNext() method should return true if there is trivia in the list")]
    public void EnumeratorImpl_MoveNextMethod_ShouldReturnTrue_IfThereIsTriviaInTheList()
    {
        // Arrange
        var syntaxTriviaList = new SyntaxTriviaList(_syntaxToken, _triviaList, 56) as IEnumerable<SyntaxTrivia>;
        var enumerator = syntaxTriviaList.GetEnumerator();

        // Act
        var actual1 = enumerator.MoveNext();
        var actual2 = enumerator.MoveNext();
        var actual3 = enumerator.MoveNext();

        // Assert
        Assert.True(actual1);
        Assert.True(actual2);
        Assert.True(actual3);
    }

    [Fact(DisplayName = "The MoveNext() method should return true if the underlying node is a single node")]
    public void EnumeratorImpl_MoveNextMethod_ShouldReturnTrue_IfUnderlyingNodeIsSingleNode()
    {
        // Arrange
        var trivia1 = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " ");
        var trivia2 = SyntaxFactory.Trivia(SyntaxKind.SingleLineCommentTrivia, "% comment");
        var triviaList = SyntaxFactory.Token(SyntaxKind.TrueKeyword, trivia1, trivia2);
        var syntaxTriviaList = new SyntaxTriviaList(_syntaxToken, triviaList, 56) as IEnumerable<SyntaxTrivia>;
        var enumerator = syntaxTriviaList.GetEnumerator();

        // Act
        var actual = enumerator.MoveNext();

        // Assert
        Assert.True(actual);
    }

    [Fact(DisplayName =
        $"The {nameof(SyntaxTriviaList.EnumeratorImpl.Current)} property must return the current syntax trivia when the underlying node is a list")]
    public void EnumeratorImpl_CurrentProperty_MustReturnCurrentSyntaxTrivia_WhenUnderlyingNodeIsList()
    {
        // Arrange
        var syntaxTriviaList = new SyntaxTriviaList(_syntaxToken, _triviaList, 56, 10) as IEnumerable<SyntaxTrivia>;
        var enumerator = syntaxTriviaList.GetEnumerator();

        // Act
        enumerator.MoveNext();
        var actual1 = enumerator.Current;

        enumerator.MoveNext();
        var actual2 = enumerator.Current;

        enumerator.MoveNext();
        var actual3 = enumerator.Current;

        // Assert
        Assert.Equal(56, actual1.Position);
        Assert.Equal(70, actual2.Position);
        Assert.Equal(77, actual3.Position);
        Assert.Equal(10, actual1.Index);
        Assert.Equal(11, actual2.Index);
        Assert.Equal(12, actual3.Index);
        Assert.Same(_triviaList.GetSlot(0), actual1.UnderlyingNode);
        Assert.Same(_triviaList.GetSlot(1), actual2.UnderlyingNode);
        Assert.Same(_triviaList.GetSlot(2), actual3.UnderlyingNode);
    }

    [Fact(DisplayName =
        $"The {nameof(SyntaxTriviaList.EnumeratorImpl.Current)} property must return the current syntax trivia when the underlying node is a single node")]
    public void EnumeratorImpl_CurrentProperty_MustReturnCurrentSyntaxTrivia_WhenUnderlyingNodeIsSingleNode()
    {
        // Arrange
        const int Position = 56;
        const int Index = 10;
        var trivia1 = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " ");
        var trivia2 = SyntaxFactory.Trivia(SyntaxKind.SingleLineCommentTrivia, "% comment");
        var triviaList = SyntaxFactory.Token(SyntaxKind.TrueKeyword, trivia1, trivia2);
        var syntaxTriviaList = new SyntaxTriviaList(_syntaxToken, triviaList, Position, Index) as IEnumerable<SyntaxTrivia>;
        var enumerator = syntaxTriviaList.GetEnumerator();

        // Act
        enumerator.MoveNext();
        var actual1 = enumerator.Current;
        var actual2 = (SyntaxTrivia)(enumerator as IEnumerator).Current;

        // Assert
        Assert.Equal(Position, actual1.Position);
        Assert.Equal(Index, actual1.Index);
        Assert.Same(triviaList, actual1.UnderlyingNode);
        Assert.Equal(Position, actual2.Position);
        Assert.Equal(Index, actual2.Index);
        Assert.Same(triviaList, actual2.UnderlyingNode);
    }

    [Fact(DisplayName = "The Reset() method should reset the enumerator")]
    public void EnumeratorImpl_ResetMethod_ShouldResetEnumerator()
    {
        // Arrange
        const int Position = 56;
        const int Index = 10;
        var syntaxTriviaList = new SyntaxTriviaList(_syntaxToken, _triviaList, Position, Index) as IEnumerable<SyntaxTrivia>;
        var enumerator = syntaxTriviaList.GetEnumerator();

        // Act
        enumerator.MoveNext();
        enumerator.Reset();
        enumerator.MoveNext();
        var actual = enumerator.Current;

        // Assert
        Assert.Equal(Position, actual.Position);
        Assert.Equal(Index, actual.Index);
        Assert.Same(_triviaList.GetSlot(0), actual.UnderlyingNode);
    }
}

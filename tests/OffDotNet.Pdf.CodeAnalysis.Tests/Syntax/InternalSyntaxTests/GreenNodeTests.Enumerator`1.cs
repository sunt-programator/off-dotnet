// <copyright file="GreenNodeTests.Enumerator`1.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxTests;

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using SyntaxFactory = CodeAnalysis.Syntax.InternalSyntax.SyntaxFactory;
using SyntaxTrivia = CodeAnalysis.Syntax.SyntaxTrivia;

[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1601:Partial elements should be documented", Justification = "Test file")]
[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:File name should match first type name", Justification = "Test file")]
[SuppressMessage("ReSharper", "GenericEnumeratorNotDisposed", Justification = "Test file")]
[SuppressMessage("ReSharper", "AssignNullToNotNullAttribute", Justification = "Test file")]
public class GreenNodeTestsEnumeratorImpl
{
    private readonly GreenNode _greenNodeList = SyntaxFactory.List(
        SyntaxFactory.Token(SyntaxKind.TrueKeyword),
        SyntaxFactory.Token(SyntaxKind.FalseKeyword),
        SyntaxFactory.Token(SyntaxKind.NullKeyword));

    [Fact(DisplayName = $"The struct must implement the {nameof(IEnumerator<GreenNode>)} interface")]
    public void Enumerator_MustImplementIEnumeratorOfSyntaxTrivia()
    {
        // Arrange
        var enumerator = new GreenNode.EnumeratorImpl(GreenNode.None);

        // Act

        // Assert
        Assert.IsAssignableFrom<IEnumerator<GreenNode>>(enumerator);
    }

    [Fact(DisplayName = "The Dispose() method should not do anything")]
    public void Enumerator_DisposeMethod_ShouldNotDoAnything()
    {
        // Arrange
        var enumerator = new GreenNode.EnumeratorImpl(GreenNode.None);

        // Act
        enumerator.Dispose();

        // Assert
        Assert.True(true);
    }

    [Fact(DisplayName = $"The {nameof(GreenNode.Enumerator.Current)} property must throw a {nameof(InvalidOperationException)}")]
    public void Enumerator_CurrentProperty_MustThrowInvalidOperationException()
    {
        // Arrange
        var enumerator = new GreenNode.EnumeratorImpl(GreenNode.None);

        // Act
        void Action1() => _ = enumerator.Current;
        void Action2() => _ = (SyntaxTrivia)(enumerator as IEnumerator).Current;

        // Assert
        Assert.Throws<InvalidOperationException>(Action1);
        Assert.Throws<InvalidOperationException>(Action2);
    }

    [Fact(DisplayName = "The MoveNext() method should return false if there is no trivia in the list")]
    public void Enumerator_MoveNextMethod_ShouldReturnFalse_IfThereIsNoTriviaInTheList()
    {
        // Arrange
        var enumerator = new GreenNode.EnumeratorImpl(GreenNode.None);

        // Act
        var result = enumerator.MoveNext();

        // Assert
        Assert.False(result);
    }

    [Fact(DisplayName = "The MoveNext() method should return false if the enumerator is at the end of the list")]
    public void Enumerator_MoveNextMethod_ShouldReturnFalse_IfEnumeratorIsAtTheEndOfTheList()
    {
        // Arrange
        var enumerator = _greenNodeList.GetEnumerator();

        enumerator.MoveNext();
        enumerator.MoveNext();
        enumerator.MoveNext();

        // Act
        var result = enumerator.MoveNext();

        // Assert
        Assert.False(result);
    }

    [Fact(DisplayName = "The MoveNext() method should return true if there is trivia in the list")]
    public void Enumerator_MoveNextMethod_ShouldReturnTrue_IfThereIsTriviaInTheList()
    {
        // Arrange
        var enumerator = _greenNodeList.GetEnumerator();

        // Act
        var actual1 = enumerator.MoveNext();
        var actual2 = enumerator.MoveNext();
        var actual3 = enumerator.MoveNext();

        // Assert
        Assert.True(actual1);
        Assert.True(actual2);
        Assert.True(actual3);
    }

    [Fact(DisplayName = "The MoveNext() method should return false if the underlying node is a single node")]
    public void Enumerator_MoveNextMethod_ShouldReturnFalse_IfUnderlyingNodeIsSingleNode()
    {
        // Arrange
        var greenNode = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        var enumerator = greenNode.GetEnumerator();

        // Act
        var actual = enumerator.MoveNext();

        // Assert
        Assert.False(actual);
    }

    [Fact(DisplayName = $"The {nameof(GreenNode.Enumerator.Current)} property must return the current syntax trivia when the underlying node is a list")]
    public void Enumerator_CurrentProperty_MustReturnCurrentSyntaxTrivia_WhenUnderlyingNodeIsList()
    {
        // Arrange
        var enumerator = _greenNodeList.GetEnumerator();

        // Act
        enumerator.MoveNext();
        var actual1 = enumerator.Current;

        enumerator.MoveNext();
        var actual2 = enumerator.Current;

        enumerator.MoveNext();
        var actual3 = enumerator.Current;

        // Assert
        Assert.Same(_greenNodeList.GetSlot(0), actual1);
        Assert.Same(_greenNodeList.GetSlot(1), actual2);
        Assert.Same(_greenNodeList.GetSlot(2), actual3);
    }

    [Fact(DisplayName =
        $"The {nameof(GreenNode.Enumerator.Current)} property must throw an {nameof(InvalidOperationException)} when the underlying node is a single node")]
    public void Enumerator_CurrentProperty_MustThrowInvalidOperationException_WhenUnderlyingNodeIsSingleNode()
    {
        // Arrange
        var greenNode = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        var enumerator = greenNode.GetEnumerator();

        // Act
        enumerator.MoveNext();
        void Action1() => _ = enumerator.Current;
        void Action2() => _ = (GreenNode)(enumerator as IEnumerator).Current;

        // Assert
        Assert.Throws<InvalidOperationException>(Action1);
        Assert.Throws<InvalidOperationException>(Action2);
    }

    [Fact(DisplayName = "The Reset() method should reset the enumerator")]
    public void Enumerator_ResetMethod_ShouldResetEnumerator()
    {
        // Arrange
        var enumerator = _greenNodeList.GetEnumerator();

        // Act
        enumerator.MoveNext();
        enumerator.Reset();
        enumerator.MoveNext();
        enumerator.MoveNext();
        var actual = enumerator.Current;

        // Assert
        Assert.Same(_greenNodeList.GetSlot(1), actual);
    }
}

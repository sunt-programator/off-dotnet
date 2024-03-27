// <copyright file="SyntaxList1Tests.Enumerator.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxTests;

using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

public class SyntaxList1EnumeratorTests
{
    [Fact(DisplayName = $"The {nameof(SyntaxList<GreenNode>.Enumerator)} struct must implement {nameof(IEnumerator<GreenNode>)}.")]
    public void Enumerator_MustImplementIEnumerator()
    {
        // Arrange
        var literalExpression1 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));

        // Act
        SyntaxList<GreenNode> syntaxList = new(literalExpression1);
        SyntaxList<GreenNode>.Enumerator enumerator = new(syntaxList);

        // Assert
        Assert.IsAssignableFrom<IEnumerator<GreenNode>>(enumerator);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxList<GreenNode>.Enumerator.Current)} property must return null.")]
    public void CurrentProperty_MustReturnNull()
    {
        // Arrange
        var literalExpression1 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        var literalExpression2 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword));
        var list = SyntaxFactory.List(literalExpression1, literalExpression2);

        SyntaxList<GreenNode> syntaxList = new(list);
        SyntaxList<GreenNode>.Enumerator enumerator = new(syntaxList);

        // Act
        var actualNode = enumerator.Current;

        // Assert
        Assert.Null(actualNode);
    }

    [Fact(DisplayName = "The MoveNext() method must return true at iteration 0.")]
    public void MoveNext_Iteration0_MustReturnTrue()
    {
        // Arrange
        var literalExpression1 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        var literalExpression2 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword));
        var list = SyntaxFactory.List(literalExpression1, literalExpression2);

        SyntaxList<GreenNode> syntaxList = new(list);
        SyntaxList<GreenNode>.Enumerator enumerator = new(syntaxList);

        // Act
        var canMoveNext = enumerator.MoveNext();

        // Assert
        Assert.True(canMoveNext);
    }

    [Fact(DisplayName = "The MoveNext() method must return true at iteration 1.")]
    public void MoveNext_Iteration1_MustReturnTrue()
    {
        // Arrange
        var literalExpression1 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        var literalExpression2 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword));
        var list = SyntaxFactory.List(literalExpression1, literalExpression2);

        SyntaxList<GreenNode> syntaxList = new(list);
        SyntaxList<GreenNode>.Enumerator enumerator = new(syntaxList);

        // Act
        enumerator.MoveNext();
        var canMoveNext = enumerator.MoveNext();

        // Assert
        Assert.True(canMoveNext);
    }

    [Fact(DisplayName = "The MoveNext() method must return true at iteration 2.")]
    public void MoveNext_Iteration2_MustReturnFalse()
    {
        // Arrange
        var literalExpression1 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        var literalExpression2 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword));
        var list = SyntaxFactory.List(literalExpression1, literalExpression2);

        SyntaxList<GreenNode> syntaxList = new(list);
        SyntaxList<GreenNode>.Enumerator enumerator = new(syntaxList);

        // Act
        enumerator.MoveNext();
        enumerator.MoveNext();
        var canMoveNext = enumerator.MoveNext();

        // Assert
        Assert.False(canMoveNext);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxList<GreenNode>.Enumerator.Current)} property must return the first element.")]
    public void CurrentProperty_Index0_MustReturnTheFirstElement()
    {
        // Arrange
        var literalExpression1 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        var literalExpression2 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword));
        var list = SyntaxFactory.List(literalExpression1, literalExpression2);

        SyntaxList<GreenNode> syntaxList = new(list);
        SyntaxList<GreenNode>.Enumerator enumerator = new(syntaxList);

        // Act
        enumerator.MoveNext();
        var actualNode = enumerator.Current;

        // Assert
        Assert.Equal(literalExpression1, actualNode);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxList<GreenNode>.Enumerator.Current)} property must return the second element.")]
    public void CurrentProperty_Index1_MustReturnTheSecondElement()
    {
        // Arrange
        var literalExpression1 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        var literalExpression2 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword));
        var list = SyntaxFactory.List(literalExpression1, literalExpression2);

        SyntaxList<GreenNode> syntaxList = new(list);
        SyntaxList<GreenNode>.Enumerator enumerator = new(syntaxList);

        // Act
        enumerator.MoveNext();
        enumerator.MoveNext();
        var actualNode = enumerator.Current;

        // Assert
        Assert.Equal(literalExpression2, actualNode);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxList<GreenNode>.Enumerator.Current)} property must return the last element if index is out of range.")]
    public void CurrentProperty_IndexOutOfRange_MustReturnTheLastElement()
    {
        // Arrange
        var literalExpression1 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        var literalExpression2 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword));
        var list = SyntaxFactory.List(literalExpression1, literalExpression2);

        SyntaxList<GreenNode> syntaxList = new(list);
        SyntaxList<GreenNode>.Enumerator enumerator = new(syntaxList);

        // Act
        enumerator.MoveNext();
        enumerator.MoveNext();
        enumerator.MoveNext();
        var actualNode = enumerator.Current;

        // Assert
        Assert.Equal(literalExpression2, actualNode);
    }

    [Fact(DisplayName = "The Reset() method must reset the enumerator.")]
    public void ResetMethod()
    {
        // Arrange
        var literalExpression1 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        var literalExpression2 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword));
        var list = SyntaxFactory.List(literalExpression1, literalExpression2);

        SyntaxList<GreenNode> syntaxList = new(list);
        SyntaxList<GreenNode>.Enumerator enumerator = new(syntaxList);

        // Act
        enumerator.MoveNext();
        enumerator.MoveNext();
        enumerator.Reset();
        var actualNode = enumerator.Current;

        // Assert
        Assert.Null(actualNode);
    }

    [Fact(DisplayName = "The Dispose() method must reset the enumerator.")]
    public void DisposeMethod()
    {
        // Arrange
        var literalExpression1 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        var literalExpression2 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword));
        var list = SyntaxFactory.List(literalExpression1, literalExpression2);

        SyntaxList<GreenNode> syntaxList = new(list);
        SyntaxList<GreenNode>.Enumerator enumerator = new(syntaxList);

        // Act
        enumerator.MoveNext();
        enumerator.MoveNext();
        enumerator.Dispose();
        var actualNode = enumerator.Current;

        // Assert
        Assert.Null(actualNode);
    }
}

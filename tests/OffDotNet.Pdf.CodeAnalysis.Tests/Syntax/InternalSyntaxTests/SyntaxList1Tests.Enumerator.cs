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
        LiteralExpressionSyntax literalExpression1 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));

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
        LiteralExpressionSyntax literalExpression1 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        LiteralExpressionSyntax literalExpression2 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword));
        GreenNode list = SyntaxFactory.List(literalExpression1, literalExpression2);

        SyntaxList<GreenNode> syntaxList = new(list);
        SyntaxList<GreenNode>.Enumerator enumerator = new(syntaxList);

        // Act
        GreenNode? actualNode = enumerator.Current;

        // Assert
        Assert.Null(actualNode);
    }

    [Fact(DisplayName = "The MoveNext() method must return true at iteration 0.")]
    public void MoveNext_Iteration0_MustReturnTrue()
    {
        // Arrange
        LiteralExpressionSyntax literalExpression1 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        LiteralExpressionSyntax literalExpression2 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword));
        GreenNode list = SyntaxFactory.List(literalExpression1, literalExpression2);

        SyntaxList<GreenNode> syntaxList = new(list);
        SyntaxList<GreenNode>.Enumerator enumerator = new(syntaxList);

        // Act
        bool canMoveNext = enumerator.MoveNext();

        // Assert
        Assert.True(canMoveNext);
    }

    [Fact(DisplayName = "The MoveNext() method must return true at iteration 1.")]
    public void MoveNext_Iteration1_MustReturnTrue()
    {
        // Arrange
        LiteralExpressionSyntax literalExpression1 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        LiteralExpressionSyntax literalExpression2 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword));
        GreenNode list = SyntaxFactory.List(literalExpression1, literalExpression2);

        SyntaxList<GreenNode> syntaxList = new(list);
        SyntaxList<GreenNode>.Enumerator enumerator = new(syntaxList);

        // Act
        enumerator.MoveNext();
        bool canMoveNext = enumerator.MoveNext();

        // Assert
        Assert.True(canMoveNext);
    }

    [Fact(DisplayName = "The MoveNext() method must return true at iteration 2.")]
    public void MoveNext_Iteration2_MustReturnFalse()
    {
        // Arrange
        LiteralExpressionSyntax literalExpression1 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        LiteralExpressionSyntax literalExpression2 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword));
        GreenNode list = SyntaxFactory.List(literalExpression1, literalExpression2);

        SyntaxList<GreenNode> syntaxList = new(list);
        SyntaxList<GreenNode>.Enumerator enumerator = new(syntaxList);

        // Act
        enumerator.MoveNext();
        enumerator.MoveNext();
        bool canMoveNext = enumerator.MoveNext();

        // Assert
        Assert.False(canMoveNext);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxList<GreenNode>.Enumerator.Current)} property must return the first element.")]
    public void CurrentProperty_Index0_MustReturnTheFirstElement()
    {
        // Arrange
        LiteralExpressionSyntax literalExpression1 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        LiteralExpressionSyntax literalExpression2 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword));
        GreenNode list = SyntaxFactory.List(literalExpression1, literalExpression2);

        SyntaxList<GreenNode> syntaxList = new(list);
        SyntaxList<GreenNode>.Enumerator enumerator = new(syntaxList);

        // Act
        enumerator.MoveNext();
        GreenNode actualNode = enumerator.Current;

        // Assert
        Assert.Equal(literalExpression1, actualNode);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxList<GreenNode>.Enumerator.Current)} property must return the second element.")]
    public void CurrentProperty_Index1_MustReturnTheSecondElement()
    {
        // Arrange
        LiteralExpressionSyntax literalExpression1 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        LiteralExpressionSyntax literalExpression2 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword));
        GreenNode list = SyntaxFactory.List(literalExpression1, literalExpression2);

        SyntaxList<GreenNode> syntaxList = new(list);
        SyntaxList<GreenNode>.Enumerator enumerator = new(syntaxList);

        // Act
        enumerator.MoveNext();
        enumerator.MoveNext();
        GreenNode actualNode = enumerator.Current;

        // Assert
        Assert.Equal(literalExpression2, actualNode);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxList<GreenNode>.Enumerator.Current)} property must return the last element if index is out of range.")]
    public void CurrentProperty_IndexOutOfRange_MustReturnTheLastElement()
    {
        // Arrange
        LiteralExpressionSyntax literalExpression1 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        LiteralExpressionSyntax literalExpression2 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword));
        GreenNode list = SyntaxFactory.List(literalExpression1, literalExpression2);

        SyntaxList<GreenNode> syntaxList = new(list);
        SyntaxList<GreenNode>.Enumerator enumerator = new(syntaxList);

        // Act
        enumerator.MoveNext();
        enumerator.MoveNext();
        enumerator.MoveNext();
        GreenNode actualNode = enumerator.Current;

        // Assert
        Assert.Equal(literalExpression2, actualNode);
    }

    [Fact(DisplayName = "The Reset() method must reset the enumerator.")]
    public void ResetMethod()
    {
        // Arrange
        LiteralExpressionSyntax literalExpression1 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        LiteralExpressionSyntax literalExpression2 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword));
        GreenNode list = SyntaxFactory.List(literalExpression1, literalExpression2);

        SyntaxList<GreenNode> syntaxList = new(list);
        SyntaxList<GreenNode>.Enumerator enumerator = new(syntaxList);

        // Act
        enumerator.MoveNext();
        enumerator.MoveNext();
        enumerator.Reset();
        GreenNode? actualNode = enumerator.Current;

        // Assert
        Assert.Null(actualNode);
    }

    [Fact(DisplayName = "The Dispose() method must reset the enumerator.")]
    public void DisposeMethod()
    {
        // Arrange
        LiteralExpressionSyntax literalExpression1 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        LiteralExpressionSyntax literalExpression2 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword));
        GreenNode list = SyntaxFactory.List(literalExpression1, literalExpression2);

        SyntaxList<GreenNode> syntaxList = new(list);
        SyntaxList<GreenNode>.Enumerator enumerator = new(syntaxList);

        // Act
        enumerator.MoveNext();
        enumerator.MoveNext();
        enumerator.Dispose();
        GreenNode? actualNode = enumerator.Current;

        // Assert
        Assert.Null(actualNode);
    }
}

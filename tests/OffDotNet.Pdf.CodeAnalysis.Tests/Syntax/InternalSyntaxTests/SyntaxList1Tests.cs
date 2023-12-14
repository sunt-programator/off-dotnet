// <copyright file="SyntaxList1Tests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using SyntaxTrivia = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxTrivia;

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxTests;

public class SyntaxList1Tests
{
    [Fact(DisplayName = $"The {nameof(SyntaxList<GreenNode>.Node)} property must be set from constructor.")]
    public void NodeProperty_MustBeSetFromConstructor()
    {
        // Arrange
        SyntaxTrivia trivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " ");
        LiteralExpressionSyntax literalExpression1 = new(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword, trivia, trivia));
        LiteralExpressionSyntax literalExpression2 = new(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword, trivia, trivia));
        GreenNode list = SyntaxFactory.List(literalExpression1, literalExpression2);

        // Act
        SyntaxList<GreenNode> syntaxList = new(list);
        GreenNode? actualNode = syntaxList.Node;

        // Assert
        Assert.Equal(actualNode, list);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxList<GreenNode>.Node)} property must be null if not passed in constructor.")]
    public void NodeProperty_MustBeNull()
    {
        // Arrange

        // Act
        SyntaxList<GreenNode> syntaxList = new(null);
        GreenNode? actualNode = syntaxList.Node;

        // Assert
        Assert.Null(actualNode);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxList<GreenNode>.Count)} property must be zero if node is null")]
    public void CountProperty_NullNode_MustBe0()
    {
        // Arrange

        // Act
        SyntaxList<GreenNode> syntaxList = new(null);
        int actualCount = syntaxList.Count;

        // Assert
        Assert.Equal(0, actualCount);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxList<GreenNode>.Count)} property must be equal to {nameof(GreenNode.SlotCount)} if the node is a list")]
    public void CountProperty_ListNode_MustReturnSlotCount()
    {
        // Arrange
        SyntaxTrivia trivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " ");
        LiteralExpressionSyntax literalExpression1 = new(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword, trivia, trivia));
        LiteralExpressionSyntax literalExpression2 = new(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword, trivia, trivia));
        GreenNode list = SyntaxFactory.List(literalExpression1, literalExpression2);

        // Act
        SyntaxList<GreenNode> syntaxList = new(list);
        int actualCount = syntaxList.Count;

        // Assert
        Assert.Equal(2, actualCount);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxList<GreenNode>.Count)} property must be equal to 1 if the node is not a list")]
    public void CountProperty_NonListNode_MustReturnSlotCount()
    {
        // Arrange
        SyntaxTrivia trivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " ");
        LiteralExpressionSyntax literalExpression1 = new(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword, trivia, trivia));

        // Act
        SyntaxList<GreenNode> syntaxList = new(literalExpression1);
        int actualCount = syntaxList.Count;

        // Assert
        Assert.Equal(1, actualCount);
    }

    [Fact(DisplayName = "The indexer must return null if the provided node is null")]
    public void IndexGet_NullNode_MustReturnNull()
    {
        // Arrange

        // Act
        SyntaxList<GreenNode> syntaxList = new(null);
        GreenNode? actualNode = syntaxList[0];

        // Assert
        Assert.Null(actualNode);
    }

    [Fact(DisplayName = "The indexer must return the slot at index=0 if the provided node is a list")]
    public void IndexGet_ListNodeIndex0_MustReturnSlot0()
    {
        // Arrange
        SyntaxTrivia trivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " ");
        LiteralExpressionSyntax literalExpression1 = new(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword, trivia, trivia));
        LiteralExpressionSyntax literalExpression2 = new(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword, trivia, trivia));
        GreenNode list = SyntaxFactory.List(literalExpression1, literalExpression2);

        // Act
        SyntaxList<GreenNode> syntaxList = new(list);
        GreenNode? actualNode = syntaxList[0];

        // Assert
        Assert.Equal(literalExpression1, actualNode);
    }

    [Fact(DisplayName = "The indexer must return the slot at index=1 if the provided node is a list")]
    public void IndexGet_ListNodeIndex1_MustReturnSlot1()
    {
        // Arrange
        SyntaxTrivia trivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " ");
        LiteralExpressionSyntax literalExpression1 = new(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword, trivia, trivia));
        LiteralExpressionSyntax literalExpression2 = new(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword, trivia, trivia));
        GreenNode list = SyntaxFactory.List(literalExpression1, literalExpression2);

        // Act
        SyntaxList<GreenNode> syntaxList = new(list);
        GreenNode? actualNode = syntaxList[1];

        // Assert
        Assert.Equal(literalExpression2, actualNode);
    }

    [Fact(DisplayName = "The indexer with index=0 must return the current node if the provided node is not a list")]
    public void IndexGet_NoListNodeIndex0_MustReturnTheCurrentNode()
    {
        // Arrange
        SyntaxTrivia trivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " ");
        LiteralExpressionSyntax literalExpression1 = new(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword, trivia, trivia));

        // Act
        SyntaxList<GreenNode> syntaxList = new(literalExpression1);
        GreenNode? actualNode = syntaxList[0];

        // Assert
        Assert.Equal(literalExpression1, actualNode);
    }

    [Fact(DisplayName = $"The indexer with index=1 must throw an {nameof(InvalidOperationException)} if the provided node is not a list")]
    public void IndexGet_NoListNodeIndex1_MustThrowInvalidOperationException()
    {
        // Arrange
        SyntaxTrivia trivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " ");
        LiteralExpressionSyntax literalExpression1 = new(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword, trivia, trivia));

        // Act
        SyntaxList<GreenNode> syntaxList = new(literalExpression1);

        GreenNode? ActualNodeFunc()
        {
            return syntaxList[1];
        }

        // Assert
        Assert.Throws<InvalidOperationException>(ActualNodeFunc);
    }

    [Fact(DisplayName = "The Any() method must return false if the provided node is null")]
    public void AnyMethod_NullNode_MustReturnFalse()
    {
        // Arrange

        // Act
        SyntaxList<GreenNode> syntaxList = new(null);
        bool hasAny = syntaxList.Any();

        // Assert
        Assert.False(hasAny);
    }

    [Fact(DisplayName = "The Any() method must return true if the provided node is not null")]
    public void AnyMethod_WithNode_MustReturnTrue()
    {
        // Arrange
        LiteralExpressionSyntax literalExpression1 = new(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));

        // Act
        SyntaxList<GreenNode> syntaxList = new(literalExpression1);
        bool hasAny = syntaxList.Any();

        // Assert
        Assert.True(hasAny);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxList<GreenNode>)} method must implement the {nameof(IEnumerable<GreenNode>)} interface")]
    public void SyntaxList_MustImplementIEnumerable()
    {
        // Arrange

        // Act
        SyntaxList<GreenNode> syntaxList = new(null);

        // Assert
        Assert.IsAssignableFrom<IEnumerable<GreenNode>>(syntaxList);
    }

    [Fact(DisplayName = "The Any() method must return false if the provided kind does not exist in the node")]
    public void AnyMethod_NonExistingKind_MustReturnFalse()
    {
        // Arrange
        LiteralExpressionSyntax literalExpression1 = new(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));

        // Act
        SyntaxList<GreenNode> syntaxList = new(literalExpression1);
        bool hasAny = syntaxList.Any(SyntaxKind.NullLiteralExpression);

        // Assert
        Assert.False(hasAny);
    }

    [Fact(DisplayName = "The Any() method must return true if the provided kind does not exist in the node")]
    public void AnyMethod_ExistingKind_MustReturnTrue()
    {
        // Arrange
        LiteralExpressionSyntax literalExpression1 = new(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));

        // Act
        SyntaxList<GreenNode> syntaxList = new(literalExpression1);
        bool hasAny = syntaxList.Any(SyntaxKind.TrueLiteralExpression);

        // Assert
        Assert.True(hasAny);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxList<GreenNode>.Nodes)} property must return the nodes array")]
    public void NodesProperty_MustReturnTheNodesArray()
    {
        // Arrange
        LiteralExpressionSyntax literalExpression1 = new(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        LiteralExpressionSyntax literalExpression2 = new(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword));
        GreenNode list = SyntaxFactory.List(literalExpression1, literalExpression2);

        // Act
        SyntaxList<GreenNode> syntaxList = new(list);
        GreenNode[] actualNodes = syntaxList.Nodes;

        // Assert
        Assert.Equal(2, actualNodes.Length);
        Assert.Equal(literalExpression1, actualNodes[0]);
        Assert.Equal(literalExpression2, actualNodes[1]);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxList<GreenNode>)} method must implement the {nameof(IEquatable<GreenNode>)} interface")]
    public void SyntaxList_MustImplementIEquatable()
    {
        // Arrange

        // Act
        SyntaxList<GreenNode> syntaxList = new(null);

        // Assert
        Assert.IsAssignableFrom<IEquatable<SyntaxList<GreenNode>>>(syntaxList);
    }

    [Fact(DisplayName = "The Equals() method must return false")]
    public void EqualsMethod_MustReturnFalse()
    {
        // Arrange
        LiteralExpressionSyntax literalExpression1 = new(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        LiteralExpressionSyntax literalExpression2 = new(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword));
        GreenNode list1 = SyntaxFactory.List(literalExpression1, literalExpression2);
        GreenNode list2 = SyntaxFactory.List(literalExpression1, literalExpression2);

        // Act
        SyntaxList<GreenNode> syntaxList1 = new(list1);
        SyntaxList<GreenNode> syntaxList2 = new(list2);
        bool equals1 = syntaxList1.Equals(syntaxList2);
        bool equals2 = syntaxList1.Equals((object?)syntaxList2);
        bool equals3 = syntaxList1.Equals(null);
        bool equals4 = syntaxList1 == syntaxList2;
        bool equals5 = syntaxList1 != syntaxList2;

        // Assert
        Assert.False(equals1);
        Assert.False(equals2);
        Assert.False(equals3);
        Assert.False(equals4);
        Assert.True(equals5);
    }

    [Fact(DisplayName = "The Equals() method must return true")]
    public void EqualsMethod_MustReturnTrue()
    {
        // Arrange
        LiteralExpressionSyntax literalExpression1 = new(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        LiteralExpressionSyntax literalExpression2 = new(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword));
        GreenNode list1 = SyntaxFactory.List(literalExpression1, literalExpression2);

        // Act
        SyntaxList<GreenNode> syntaxList1 = new(list1);
        SyntaxList<GreenNode> syntaxList2 = new(list1);
        bool equals1 = syntaxList1.Equals(syntaxList2);
        bool equals2 = syntaxList1.Equals((object?)syntaxList2);
        bool equals3 = syntaxList1 == syntaxList2;
        bool equals4 = syntaxList1 != syntaxList2;

        // Assert
        Assert.True(equals1);
        Assert.True(equals2);
        Assert.True(equals3);
        Assert.False(equals4);
    }

    [Fact(DisplayName = "The GetHashCode() method must return 0 if the node is null")]
    public void GetHashCodeMethod_NullNode_MustReturn0()
    {
        // Arrange

        // Act
        SyntaxList<GreenNode> syntaxList = new(null);
        int actualHashCode = syntaxList.GetHashCode();

        // Assert
        Assert.Equal(0, actualHashCode);
    }

    [Fact(DisplayName = "The GetHashCode() method must return the actual hash code if the node is not null")]
    public void GetHashCodeMethod_ExistingNode_MustReturnActualHashCode()
    {
        // Arrange
        LiteralExpressionSyntax literalExpression1 = new(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        LiteralExpressionSyntax literalExpression2 = new(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword));
        GreenNode list = SyntaxFactory.List(literalExpression1, literalExpression2);
        int expectedHashCode = list.GetHashCode();

        // Act
        SyntaxList<GreenNode> syntaxList = new(list);
        int actualHashCode = syntaxList.GetHashCode();

        // Assert
        Assert.Equal(expectedHashCode, actualHashCode);
    }
}

// <copyright file="SyntaxListTests.WithLotsOfChildren.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Collections;
using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using SyntaxTrivia = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxTrivia;

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxTests;

public class WithLotsOfChildrenTests
{
    [Fact(DisplayName = $"The {nameof(SyntaxList.SlotCount)} property must be equal to 256.")]
    public void SlotCountProperty_MustBeEqualTo256()
    {
        // Arrange
        const int slotCount = 256;

        LiteralExpressionSyntax literalExpressionSyntax = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        ArrayElement<GreenNode>[] children = new ArrayElement<GreenNode>[slotCount];

        for (int i = 0; i < slotCount; i++)
        {
            children[i].Value = literalExpressionSyntax;
        }

        GreenNode syntaxList = SyntaxFactory.List(children);

        // Act
        int actualSlotCount = syntaxList.SlotCount;

        // Assert
        Assert.Equal(slotCount, actualSlotCount);
    }

    [Fact(DisplayName = "The GetSlot() method with index 0 must return the first child.")]
    public void GetSlotMethod_Index0_MustReturnFirstObject()
    {
        // Arrange
        const int slotCount = 10;
        ArrayElement<GreenNode>[] children = new ArrayElement<GreenNode>[slotCount];

        children[0].Value = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        children[1].Value = SyntaxFactory.LiteralExpression(SyntaxKind.FalseLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword));
        children[2].Value = SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression, SyntaxFactory.Token(SyntaxKind.NullKeyword));

        LiteralExpressionSyntax literalExpressionSyntax = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));

        for (int i = 3; i < slotCount; i++)
        {
            children[i].Value = literalExpressionSyntax;
        }

        GreenNode syntaxList = SyntaxFactory.List(children);

        // Act
        GreenNode? actualSlot = syntaxList.GetSlot(0);

        // Assert
        Assert.Equal(children[0].Value, actualSlot);
    }

    [Fact(DisplayName = "The GetSlot() method with index 1 must return the second child.")]
    public void GetSlotMethod_Index1_MustReturnSecondObject()
    {
        // Arrange
        const int slotCount = 10;
        ArrayElement<GreenNode>[] children = new ArrayElement<GreenNode>[slotCount];

        children[0].Value = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        children[1].Value = SyntaxFactory.LiteralExpression(SyntaxKind.FalseLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword));
        children[2].Value = SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression, SyntaxFactory.Token(SyntaxKind.NullKeyword));

        LiteralExpressionSyntax literalExpressionSyntax = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));

        for (int i = 3; i < slotCount; i++)
        {
            children[i].Value = literalExpressionSyntax;
        }

        GreenNode syntaxList = SyntaxFactory.List(children);

        // Act
        GreenNode? actualSlot = syntaxList.GetSlot(1);

        // Assert
        Assert.Equal(children[1].Value, actualSlot);
    }

    [Fact(DisplayName = "The GetSlot() method with index 2 must return the third child.")]
    public void GetSlotMethod_Index2_MustReturnThirdObject()
    {
        // Arrange
        const int slotCount = 10;
        ArrayElement<GreenNode>[] children = new ArrayElement<GreenNode>[slotCount];

        children[0].Value = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        children[1].Value = SyntaxFactory.LiteralExpression(SyntaxKind.FalseLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword));
        children[2].Value = SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression, SyntaxFactory.Token(SyntaxKind.NullKeyword));

        LiteralExpressionSyntax literalExpressionSyntax = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));

        for (int i = 3; i < slotCount; i++)
        {
            children[i].Value = literalExpressionSyntax;
        }

        GreenNode syntaxList = SyntaxFactory.List(children);

        // Act
        GreenNode? actualSlot = syntaxList.GetSlot(2);

        // Assert
        Assert.Equal(children[2].Value, actualSlot);
    }

    [Fact(DisplayName = "The GetSlot() method with index 10 must return null.")]
    public void GetSlotMethod_Index3_MustReturnNull()
    {
        // Arrange
        const int slotCount = 10;
        ArrayElement<GreenNode>[] children = new ArrayElement<GreenNode>[slotCount];

        children[0].Value = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        children[1].Value = SyntaxFactory.LiteralExpression(SyntaxKind.FalseLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword));
        children[2].Value = SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression, SyntaxFactory.Token(SyntaxKind.NullKeyword));

        LiteralExpressionSyntax literalExpressionSyntax = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));

        for (int i = 3; i < slotCount; i++)
        {
            children[i].Value = literalExpressionSyntax;
        }

        GreenNode syntaxList = SyntaxFactory.List(children);

        // Act
        GreenNode? actualSlot = syntaxList.GetSlot(slotCount);

        // Assert
        Assert.Null(actualSlot);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.FullWidth)} property must consider all slots.")]
    public void FullWidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        const int slotCount = 10;
        ArrayElement<GreenNode>[] children = new ArrayElement<GreenNode>[slotCount];

        SyntaxTrivia trivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " ");
        children[0].Value = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword, trivia, trivia));
        children[1].Value = SyntaxFactory.LiteralExpression(SyntaxKind.FalseLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword, trivia, trivia));
        children[2].Value = SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression, SyntaxFactory.Token(SyntaxKind.NullKeyword, trivia, trivia));

        LiteralExpressionSyntax literalExpressionSyntax = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword, trivia, trivia));

        for (int i = 3; i < slotCount; i++)
        {
            children[i].Value = literalExpressionSyntax;
        }

        GreenNode syntaxList = SyntaxFactory.List(children);

        // Act
        int actualFullWidth = syntaxList.FullWidth;

        // Assert
        Assert.Equal(61, actualFullWidth);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.Width)} property must consider all slots.")]
    public void WidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        const int slotCount = 10;
        ArrayElement<GreenNode>[] children = new ArrayElement<GreenNode>[slotCount];

        SyntaxTrivia trivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " ");
        children[0].Value = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword, trivia, trivia));
        children[1].Value = SyntaxFactory.LiteralExpression(SyntaxKind.FalseLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword, trivia, trivia));
        children[2].Value = SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression, SyntaxFactory.Token(SyntaxKind.NullKeyword, trivia, trivia));

        LiteralExpressionSyntax literalExpressionSyntax = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword, trivia, trivia));

        for (int i = 3; i < slotCount; i++)
        {
            children[i].Value = literalExpressionSyntax;
        }

        GreenNode syntaxList = SyntaxFactory.List(children);

        // Act
        int actualWidth = syntaxList.Width;

        // Assert
        Assert.Equal(59, actualWidth);
    }

    [Theory(DisplayName = "The GetSlotOffset() method with specified index must return correct offset.")]
    [InlineData(0, 0)]
    [InlineData(1, 6)]
    [InlineData(2, 13)]
    [InlineData(3, 19)]
    [InlineData(4, 25)]
    [InlineData(5, 31)]
    [InlineData(6, 37)]
    [InlineData(7, 43)]
    [InlineData(8, 49)]
    [InlineData(9, 55)]
    [InlineData(10, 55)] // No more slots, so the offset is the same as the last slot.
    public void GetSlotOffsetMethod_MustReturnTheCorrectOffset(int index, int offset)
    {
        // Arrange
        const int slotCount = 10;
        ArrayElement<GreenNode>[] children = new ArrayElement<GreenNode>[slotCount];

        SyntaxTrivia trivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " ");
        children[0].Value = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword, trivia, trivia));
        children[1].Value = SyntaxFactory.LiteralExpression(SyntaxKind.FalseLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword, trivia, trivia));
        children[2].Value = SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression, SyntaxFactory.Token(SyntaxKind.NullKeyword, trivia, trivia));

        LiteralExpressionSyntax literalExpressionSyntax = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword, trivia, trivia));

        for (int i = 3; i < slotCount; i++)
        {
            children[i].Value = literalExpressionSyntax;
        }

        GreenNode syntaxList = SyntaxFactory.List(children);

        // Act
        int actualOffset = syntaxList.GetSlotOffset(index);

        // Assert
        Assert.Equal(offset, actualOffset);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxFactory)}.{nameof(SyntaxFactory.List)} method must return a {nameof(SyntaxList.WithLotsOfChildren)} type")]
    public void SyntaxFactory_List_MustBeAssignedToWithLotsOfChildren()
    {
        // Arrange
        const int slotCount = 10;

        LiteralExpressionSyntax literalExpressionSyntax = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        ArrayElement<GreenNode>[] children = new ArrayElement<GreenNode>[slotCount];

        for (int i = 0; i < slotCount; i++)
        {
            children[i].Value = literalExpressionSyntax;
        }

        // Act
        GreenNode syntaxList = SyntaxFactory.List(children);

        // Assert
        Assert.IsType<SyntaxList.WithLotsOfChildren>(syntaxList);
    }
}

// <copyright file="ArrayExpressionSyntaxTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using SyntaxToken = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxToken;
using SyntaxTrivia = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxTrivia;

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxTests;

public class ArrayExpressionSyntaxTests
{
    private readonly SyntaxToken openBracketToken;
    private readonly SyntaxList<CollectionElementSyntax> elements;
    private readonly SyntaxToken closeBracketToken;

    public ArrayExpressionSyntaxTests()
    {
        GreenNode trivia = SyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, " ");
        ArrayElementSyntax number =
            SyntaxFactory.ArrayElement(
                SyntaxFactory.LiteralExpression(
                    SyntaxKind.NumericLiteralExpression,
                    SyntaxFactory.Token(SyntaxKind.NumericLiteralToken, "549", 549, trivia, trivia)));

        ArrayElementSyntax @bool =
            SyntaxFactory.ArrayElement(
                SyntaxFactory.LiteralExpression(
                    SyntaxKind.FalseLiteralExpression,
                    SyntaxFactory.Token(SyntaxKind.FalseKeyword, trivia, trivia)));

        ArrayElementSyntax @string =
            SyntaxFactory.ArrayElement(
                SyntaxFactory.LiteralExpression(
                    SyntaxKind.StringLiteralExpression,
                    SyntaxFactory.Token(SyntaxKind.StringLiteralToken, "(Hello World!)", trivia, trivia)));

        ArrayElementSyntax name =
            SyntaxFactory.ArrayElement(
                SyntaxFactory.LiteralExpression(
                    SyntaxKind.NameLiteralExpression,
                    SyntaxFactory.Token(SyntaxKind.NameLiteralToken, "/SomeName", trivia, trivia)));

        SyntaxList<CollectionElementSyntax> listBuilder = new SyntaxListBuilder(4)
            .AddRange(new GreenNode[] { number, @bool, @string, name })
            .ToList<CollectionElementSyntax>();

        this.openBracketToken = SyntaxFactory.Token(SyntaxKind.LeftSquareBracketToken, trivia, trivia);
        this.closeBracketToken = SyntaxFactory.Token(SyntaxKind.RightSquareBracketToken, trivia, trivia);
        this.elements = listBuilder;
    }

    [Fact(DisplayName = $"The {nameof(CollectionExpressionSyntax.Kind)} property must be {nameof(SyntaxKind.CollectionExpression)}")]
    public void KindProperty_MustBeCollectionExpression()
    {
        // Arrange
        CollectionExpressionSyntax collectionExpressionSyntax = SyntaxFactory.CollectionExpression(this.openBracketToken, this.elements, this.closeBracketToken);

        // Act
        SyntaxKind actualKind = collectionExpressionSyntax.Kind;

        // Assert
        Assert.Equal(SyntaxKind.CollectionExpression, actualKind);
    }

    [Fact(DisplayName = $"The {nameof(CollectionExpressionSyntax.OpenToken)} property must be assigned from constructor.")]
    public void OpenTokenProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        CollectionExpressionSyntax collectionExpressionSyntax = SyntaxFactory.CollectionExpression(this.openBracketToken, this.elements, this.closeBracketToken);

        // Act
        SyntaxToken actualOpenBracketToken = collectionExpressionSyntax.OpenToken;

        // Assert
        Assert.Equal(this.openBracketToken, actualOpenBracketToken);
    }

    [Fact(DisplayName = $"The {nameof(CollectionExpressionSyntax.Elements)} property must be assigned from constructor.")]
    public void ElementsProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        CollectionExpressionSyntax collectionExpressionSyntax = SyntaxFactory.CollectionExpression(this.openBracketToken, this.elements, this.closeBracketToken);

        // Act
        GreenNode? actualElements = collectionExpressionSyntax.Elements;

        // Assert
        Assert.Equal(this.elements.Node, actualElements);
    }

    [Fact(DisplayName = $"The {nameof(CollectionExpressionSyntax.CloseToken)} property must be assigned from constructor.")]
    public void CloseTokenProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        CollectionExpressionSyntax collectionExpressionSyntax = SyntaxFactory.CollectionExpression(this.openBracketToken, this.elements, this.closeBracketToken);

        // Act
        SyntaxToken actualCloseBracketToken = collectionExpressionSyntax.CloseToken;

        // Assert
        Assert.Equal(this.closeBracketToken, actualCloseBracketToken);
    }

    [Fact(DisplayName = $"The {nameof(CollectionExpressionSyntax.SlotCount)} property must be equal to 3.")]
    public void SlotCountProperty_MustBeEqualTo3()
    {
        // Arrange
        CollectionExpressionSyntax collectionExpressionSyntax = SyntaxFactory.CollectionExpression(this.openBracketToken, this.elements, this.closeBracketToken);

        // Act
        int actualSlotCount = collectionExpressionSyntax.SlotCount;

        // Assert
        Assert.Equal(3, actualSlotCount);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 0 must return {nameof(CollectionExpressionSyntax.OpenToken)} property.")]
    public void GetSlotMethod_Index0_MustReturnOpenBracketToken()
    {
        // Arrange
        CollectionExpressionSyntax collectionExpressionSyntax = SyntaxFactory.CollectionExpression(this.openBracketToken, this.elements, this.closeBracketToken);

        // Act
        GreenNode? actualSlot = collectionExpressionSyntax.GetSlot(0);

        // Assert
        Assert.Equal(this.openBracketToken, actualSlot);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 1 must return {nameof(CollectionExpressionSyntax.Elements)} property.")]
    public void GetSlotMethod_Index1_MustReturnElements()
    {
        // Arrange
        CollectionExpressionSyntax collectionExpressionSyntax = SyntaxFactory.CollectionExpression(this.openBracketToken, this.elements, this.closeBracketToken);

        // Act
        GreenNode? actualSlot = collectionExpressionSyntax.GetSlot(1);

        // Assert
        Assert.Equal(this.elements.Node, actualSlot);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 2 must return {nameof(CollectionExpressionSyntax.CloseToken)} property.")]
    public void GetSlotMethod_Index2_MustReturnCloseBracketToken()
    {
        // Arrange
        CollectionExpressionSyntax collectionExpressionSyntax = SyntaxFactory.CollectionExpression(this.openBracketToken, this.elements, this.closeBracketToken);

        // Act
        GreenNode? actualSlot = collectionExpressionSyntax.GetSlot(2);

        // Assert
        Assert.Equal(this.closeBracketToken, actualSlot);
    }

    [Fact(DisplayName = "The GetSlot() method with index 3 must return null.")]
    public void GetSlotMethod_Index3_MustReturnNull()
    {
        // Arrange
        CollectionExpressionSyntax collectionExpressionSyntax = SyntaxFactory.CollectionExpression(this.openBracketToken, this.elements, this.closeBracketToken);

        // Act
        GreenNode? actualSlot = collectionExpressionSyntax.GetSlot(3);

        // Assert
        Assert.Null(actualSlot);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.Width)} property must consider all slots.")]
    public void WidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        CollectionExpressionSyntax collectionExpressionSyntax = SyntaxFactory.CollectionExpression(this.openBracketToken, this.elements, this.closeBracketToken);

        // Act
        int actualWidth = collectionExpressionSyntax.Width;

        // Assert
        Assert.Equal(40, actualWidth);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.FullWidth)} property must consider all slots.")]
    public void FullWidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        CollectionExpressionSyntax collectionExpressionSyntax = SyntaxFactory.CollectionExpression(this.openBracketToken, this.elements, this.closeBracketToken);

        // Act
        int actualFullWidth = collectionExpressionSyntax.FullWidth;

        // Assert
        Assert.Equal(42, actualFullWidth);
    }
}

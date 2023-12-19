// <copyright file="DictionaryExpressionSyntaxTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using SyntaxToken = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxToken;
using SyntaxTrivia = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxTrivia;

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxTests;

public class DictionaryExpressionSyntaxTests
{
    private readonly SyntaxToken openBracketToken;
    private readonly SyntaxList<DictionaryElementSyntax> elements;
    private readonly SyntaxToken closeBracketToken;

    public DictionaryExpressionSyntaxTests()
    {
        GreenNode trivia = SyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, " ");

        var key = SyntaxFactory.LiteralExpression(
            SyntaxKind.NameLiteralExpression,
            SyntaxFactory.Token(SyntaxKind.NameLiteralToken, "/SomeName", trivia, trivia));

        var value = SyntaxFactory.LiteralExpression(
            SyntaxKind.NumericLiteralExpression,
            SyntaxFactory.Token(SyntaxKind.NameLiteralToken, "123", 123, trivia, trivia));

        CollectionElementSyntax keyValue = SyntaxFactory.DictionaryElement(key, value);

        SyntaxList<DictionaryElementSyntax> listBuilder = new SyntaxListBuilder(2)
            .AddRange(new GreenNode[] { keyValue, keyValue })
            .ToList<DictionaryElementSyntax>();

        this.openBracketToken = SyntaxFactory.Token(SyntaxKind.LessThanLessThanToken, trivia, trivia);
        this.closeBracketToken = SyntaxFactory.Token(SyntaxKind.GreaterThanGreaterThanToken, trivia, trivia);
        this.elements = listBuilder;
    }

    [Fact(DisplayName = $"The {nameof(CollectionExpressionSyntax.Kind)} property must be {nameof(SyntaxKind.DictionaryExpression)}")]
    public void KindProperty_MustBeDictionaryExpression()
    {
        // Arrange
        DictionaryExpressionSyntax dictionaryExpressionSyntax = SyntaxFactory.DictionaryExpression(this.openBracketToken, this.elements, this.closeBracketToken);

        // Act
        SyntaxKind actualKind = dictionaryExpressionSyntax.Kind;

        // Assert
        Assert.Equal(SyntaxKind.DictionaryExpression, actualKind);
    }

    [Fact(DisplayName = $"The {nameof(CollectionExpressionSyntax.OpenToken)} property must be assigned from constructor.")]
    public void OpenTokenProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        DictionaryExpressionSyntax dictionaryExpressionSyntax = SyntaxFactory.DictionaryExpression(this.openBracketToken, this.elements, this.closeBracketToken);

        // Act
        SyntaxToken actualOpenBracketToken = dictionaryExpressionSyntax.OpenToken;

        // Assert
        Assert.Equal(this.openBracketToken, actualOpenBracketToken);
    }

    [Fact(DisplayName = $"The {nameof(CollectionExpressionSyntax.Elements)} property must be assigned from constructor.")]
    public void ElementsProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        DictionaryExpressionSyntax dictionaryExpressionSyntax = SyntaxFactory.DictionaryExpression(this.openBracketToken, this.elements, this.closeBracketToken);

        // Act
        GreenNode? actualElements = dictionaryExpressionSyntax.Elements;

        // Assert
        Assert.Equal(this.elements.Node, actualElements);
    }

    [Fact(DisplayName = $"The {nameof(CollectionExpressionSyntax.CloseToken)} property must be assigned from constructor.")]
    public void CloseTokenProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        DictionaryExpressionSyntax dictionaryExpressionSyntax = SyntaxFactory.DictionaryExpression(this.openBracketToken, this.elements, this.closeBracketToken);

        // Act
        SyntaxToken actualCloseBracketToken = dictionaryExpressionSyntax.CloseToken;

        // Assert
        Assert.Equal(this.closeBracketToken, actualCloseBracketToken);
    }

    [Fact(DisplayName = $"The {nameof(CollectionExpressionSyntax.SlotCount)} property must be equal to 3.")]
    public void SlotCountProperty_MustBeEqualTo3()
    {
        // Arrange
        DictionaryExpressionSyntax dictionaryExpressionSyntax = SyntaxFactory.DictionaryExpression(this.openBracketToken, this.elements, this.closeBracketToken);

        // Act
        int actualSlotCount = dictionaryExpressionSyntax.SlotCount;

        // Assert
        Assert.Equal(3, actualSlotCount);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 0 must return {nameof(CollectionExpressionSyntax.OpenToken)} property.")]
    public void GetSlotMethod_Index0_MustReturnOpenBracketToken()
    {
        // Arrange
        DictionaryExpressionSyntax dictionaryExpressionSyntax = SyntaxFactory.DictionaryExpression(this.openBracketToken, this.elements, this.closeBracketToken);

        // Act
        GreenNode? actualSlot = dictionaryExpressionSyntax.GetSlot(0);

        // Assert
        Assert.Equal(this.openBracketToken, actualSlot);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 1 must return {nameof(CollectionExpressionSyntax.Elements)} property.")]
    public void GetSlotMethod_Index1_MustReturnElements()
    {
        // Arrange
        DictionaryExpressionSyntax dictionaryExpressionSyntax = SyntaxFactory.DictionaryExpression(this.openBracketToken, this.elements, this.closeBracketToken);

        // Act
        GreenNode? actualSlot = dictionaryExpressionSyntax.GetSlot(1);

        // Assert
        Assert.Equal(this.elements.Node, actualSlot);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 2 must return {nameof(CollectionExpressionSyntax.CloseToken)} property.")]
    public void GetSlotMethod_Index2_MustReturnCloseBracketToken()
    {
        // Arrange
        DictionaryExpressionSyntax dictionaryExpressionSyntax = SyntaxFactory.DictionaryExpression(this.openBracketToken, this.elements, this.closeBracketToken);

        // Act
        GreenNode? actualSlot = dictionaryExpressionSyntax.GetSlot(2);

        // Assert
        Assert.Equal(this.closeBracketToken, actualSlot);
    }

    [Fact(DisplayName = "The GetSlot() method with index 3 must return null.")]
    public void GetSlotMethod_Index3_MustReturnNull()
    {
        // Arrange
        DictionaryExpressionSyntax dictionaryExpressionSyntax = SyntaxFactory.DictionaryExpression(this.openBracketToken, this.elements, this.closeBracketToken);

        // Act
        GreenNode? actualSlot = dictionaryExpressionSyntax.GetSlot(3);

        // Assert
        Assert.Null(actualSlot);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.Width)} property must consider all slots.")]
    public void WidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        DictionaryExpressionSyntax dictionaryExpressionSyntax = SyntaxFactory.DictionaryExpression(this.openBracketToken, this.elements, this.closeBracketToken);

        // Act
        int actualWidth = dictionaryExpressionSyntax.Width;

        // Assert
        Assert.Equal(34, actualWidth);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.FullWidth)} property must consider all slots.")]
    public void FullWidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        DictionaryExpressionSyntax dictionaryExpressionSyntax = SyntaxFactory.DictionaryExpression(this.openBracketToken, this.elements, this.closeBracketToken);

        // Act
        int actualFullWidth = dictionaryExpressionSyntax.FullWidth;

        // Assert
        Assert.Equal(36, actualFullWidth);
    }
}

// <copyright file="IndirectObjectSyntaxTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using SyntaxToken = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxToken;
using SyntaxTrivia = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxTrivia;

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxTests;

public class IndirectObjectSyntaxTests
{
    private readonly LiteralExpressionSyntax objectNumber;
    private readonly LiteralExpressionSyntax generationNumber;
    private readonly SyntaxToken objKeyword;
    private readonly GreenNode content;
    private readonly SyntaxToken endObjKeyword;

    public IndirectObjectSyntaxTests()
    {
        GreenNode trivia = SyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, " ");
        this.objectNumber = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Token(SyntaxKind.NumericLiteralToken, "12", 12, trivia, trivia));
        this.generationNumber = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Token(SyntaxKind.NumericLiteralToken, "0", 0, trivia, trivia));
        this.objKeyword = SyntaxFactory.Token(SyntaxKind.StartObjectKeyword, trivia, trivia);
        this.content = SyntaxFactory.Token(SyntaxKind.TrueKeyword, trivia, trivia);
        this.endObjKeyword = SyntaxFactory.Token(SyntaxKind.EndObjectKeyword, trivia, trivia);
    }

    [Fact(DisplayName = $"The {nameof(IndirectObjectSyntax.Kind)} property must be {nameof(SyntaxKind.IndirectObject)}")]
    public void KindProperty_MustBeIndirectReferenceExpression()
    {
        // Arrange
        IndirectObjectSyntax indirectObject = SyntaxFactory.Object(this.objectNumber, this.generationNumber, this.objKeyword, this.content, this.endObjKeyword);

        // Act
        SyntaxKind actualObjectNumber = indirectObject.Kind;

        // Assert
        Assert.Equal(SyntaxKind.IndirectObject, actualObjectNumber);
    }

    [Fact(DisplayName = $"The {nameof(IndirectObjectSyntax.ObjectNumber)} property must be assigned from constructor.")]
    public void ObjectNumberProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        IndirectObjectSyntax indirectObject = SyntaxFactory.Object(this.objectNumber, this.generationNumber, this.objKeyword, this.content, this.endObjKeyword);

        // Act
        LiteralExpressionSyntax actualObjectNumber = indirectObject.ObjectNumber;

        // Assert
        Assert.Equal(this.objectNumber, actualObjectNumber);
    }

    [Fact(DisplayName = $"The {nameof(IndirectObjectSyntax.GenerationNumber)} property must be assigned from constructor.")]
    public void GenerationNumberProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        IndirectObjectSyntax indirectObject = SyntaxFactory.Object(this.objectNumber, this.generationNumber, this.objKeyword, this.content, this.endObjKeyword);

        // Act
        LiteralExpressionSyntax actualGenerationNumber = indirectObject.GenerationNumber;

        // Assert
        Assert.Equal(this.generationNumber, actualGenerationNumber);
    }

    [Fact(DisplayName = $"The {nameof(IndirectObjectSyntax.ObjectKeyword)} property must be assigned from constructor.")]
    public void ObjectKeywordProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        IndirectObjectSyntax indirectObject = SyntaxFactory.Object(this.objectNumber, this.generationNumber, this.objKeyword, this.content, this.endObjKeyword);

        // Act
        SyntaxToken actualObjectKeyword = indirectObject.ObjectKeyword;

        // Assert
        Assert.Equal(this.objKeyword, actualObjectKeyword);
    }

    [Fact(DisplayName = $"The {nameof(IndirectObjectSyntax.Content)} property must be assigned from constructor.")]
    public void ContentProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        IndirectObjectSyntax indirectObject = SyntaxFactory.Object(this.objectNumber, this.generationNumber, this.objKeyword, this.content, this.endObjKeyword);

        // Act
        GreenNode actualContent = indirectObject.Content;

        // Assert
        Assert.Equal(this.content, actualContent);
    }

    [Fact(DisplayName = $"The {nameof(IndirectObjectSyntax.EndObjectKeyword)} property must be assigned from constructor.")]
    public void EndObjectKeywordProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        IndirectObjectSyntax indirectObject = SyntaxFactory.Object(this.objectNumber, this.generationNumber, this.objKeyword, this.content, this.endObjKeyword);

        // Act
        SyntaxToken actualEndObjectKeyword = indirectObject.EndObjectKeyword;

        // Assert
        Assert.Equal(this.endObjKeyword, actualEndObjectKeyword);
    }

    [Fact(DisplayName = $"The {nameof(IndirectObjectSyntax.SlotCount)} property must be equal to 5.")]
    public void SlotCountProperty_MustBeEqualTo3()
    {
        // Arrange
        IndirectObjectSyntax indirectObject = SyntaxFactory.Object(this.objectNumber, this.generationNumber, this.objKeyword, this.content, this.endObjKeyword);

        // Act
        int actualSlotCount = indirectObject.SlotCount;

        // Assert
        Assert.Equal(5, actualSlotCount);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 0 must return {nameof(IndirectObjectSyntax.ObjectNumber)} property.")]
    public void GetSlotMethod_Index0_MustReturnObjectNumber()
    {
        // Arrange
        IndirectObjectSyntax indirectObject = SyntaxFactory.Object(this.objectNumber, this.generationNumber, this.objKeyword, this.content, this.endObjKeyword);

        // Act
        GreenNode? actualSlot = indirectObject.GetSlot(0);

        // Assert
        Assert.Equal(this.objectNumber, actualSlot);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 1 must return {nameof(IndirectObjectSyntax.GenerationNumber)} property.")]
    public void GetSlotMethod_Index1_MustReturnObjectNumber()
    {
        // Arrange
        IndirectObjectSyntax indirectObject = SyntaxFactory.Object(this.objectNumber, this.generationNumber, this.objKeyword, this.content, this.endObjKeyword);

        // Act
        GreenNode? actualSlot = indirectObject.GetSlot(1);

        // Assert
        Assert.Equal(this.generationNumber, actualSlot);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 2 must return {nameof(IndirectObjectSyntax.ObjectKeyword)} property.")]
    public void GetSlotMethod_Index2_MustReturnObjectNumber()
    {
        // Arrange
        IndirectObjectSyntax indirectObject = SyntaxFactory.Object(this.objectNumber, this.generationNumber, this.objKeyword, this.content, this.endObjKeyword);

        // Act
        GreenNode? actualSlot = indirectObject.GetSlot(2);

        // Assert
        Assert.Equal(this.objKeyword, actualSlot);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 3 must return {nameof(IndirectObjectSyntax.Content)} property.")]
    public void GetSlotMethod_Index3_MustReturnObjectNumber()
    {
        // Arrange
        IndirectObjectSyntax indirectObject = SyntaxFactory.Object(this.objectNumber, this.generationNumber, this.objKeyword, this.content, this.endObjKeyword);

        // Act
        GreenNode? actualSlot = indirectObject.GetSlot(3);

        // Assert
        Assert.Equal(this.content, actualSlot);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 4 must return {nameof(IndirectObjectSyntax.EndObjectKeyword)} property.")]
    public void GetSlotMethod_Index4_MustReturnObjectNumber()
    {
        // Arrange
        IndirectObjectSyntax indirectObject = SyntaxFactory.Object(this.objectNumber, this.generationNumber, this.objKeyword, this.content, this.endObjKeyword);

        // Act
        GreenNode? actualSlot = indirectObject.GetSlot(4);

        // Assert
        Assert.Equal(this.endObjKeyword, actualSlot);
    }

    [Fact(DisplayName = "The GetSlot() method with index 5 must return null.")]
    public void GetSlotMethod_Index5_MustReturnNull()
    {
        // Arrange
        IndirectObjectSyntax indirectObject = SyntaxFactory.Object(this.objectNumber, this.generationNumber, this.objKeyword, this.content, this.endObjKeyword);

        // Act
        GreenNode? actualSlot = indirectObject.GetSlot(5);

        // Assert
        Assert.Null(actualSlot);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.Width)} property must consider all slots.")]
    public void WidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        IndirectObjectSyntax indirectObject = SyntaxFactory.Object(this.objectNumber, this.generationNumber, this.objKeyword, this.content, this.endObjKeyword);

        // Act
        int actualWidth = indirectObject.Width;

        // Assert
        Assert.Equal(24, actualWidth); // [12  0  obj  true  endobj]
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.FullWidth)} property must consider all slots.")]
    public void FullWidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        IndirectObjectSyntax indirectObject = SyntaxFactory.Object(this.objectNumber, this.generationNumber, this.objKeyword, this.content, this.endObjKeyword);

        // Act
        int actualFullWidth = indirectObject.FullWidth;

        // Assert
        Assert.Equal(26, actualFullWidth); // [ 12  0  obj  true  endobj ]
    }
}

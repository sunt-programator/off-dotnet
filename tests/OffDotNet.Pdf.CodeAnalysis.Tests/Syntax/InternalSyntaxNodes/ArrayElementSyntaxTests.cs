// <copyright file="ArrayElementSyntaxTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using LiteralExpressionSyntax = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.LiteralExpressionSyntax;
using SyntaxToken = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxToken;

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxTests;

public class ArrayElementSyntaxTests
{
    private readonly ExpressionSyntax expression;

    public ArrayElementSyntaxTests()
    {
        GreenNode trivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " ");
        SyntaxToken objectNumberToken = SyntaxFactory.Token(SyntaxKind.NumericLiteralToken, "123", 123, trivia, trivia);
        this.expression = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, objectNumberToken);
    }

    [Fact(DisplayName = $"The {nameof(ArrayElementSyntax.Kind)} property must be {nameof(SyntaxKind.ArrayElement)}")]
    public void KindProperty_MustBeArrayElement()
    {
        // Arrange
        ArrayElementSyntax arrayElement = SyntaxFactory.ArrayElement(this.expression);

        // Act
        SyntaxKind actualKind = arrayElement.Kind;

        // Assert
        Assert.Equal(SyntaxKind.ArrayElement, actualKind);
    }

    [Fact(DisplayName = $"The {nameof(ArrayElementSyntax.Expression)} property must be assigned from constructor.")]
    public void ExpressionProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        ArrayElementSyntax arrayElement = SyntaxFactory.ArrayElement(this.expression);

        // Act
        ExpressionSyntax actualReferenceKeyword = arrayElement.Expression;

        // Assert
        Assert.Equal(this.expression, actualReferenceKeyword);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.SlotCount)} property must be equal to 1.")]
    public void SlotCountProperty_MustBeEqualTo3()
    {
        // Arrange
        ArrayElementSyntax arrayElement = SyntaxFactory.ArrayElement(this.expression);

        // Act
        int actualSlotCount = arrayElement.SlotCount;

        // Assert
        Assert.Equal(1, actualSlotCount);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 0 must return {nameof(ArrayElementSyntax.Expression)} property.")]
    public void GetSlotMethod_Index0_MustReturnFirstObject()
    {
        // Arrange
        ArrayElementSyntax arrayElement = SyntaxFactory.ArrayElement(this.expression);

        // Act
        GreenNode? actualSlot = arrayElement.GetSlot(0);

        // Assert
        Assert.Equal(this.expression, actualSlot);
    }

    [Fact(DisplayName = "The GetSlot() method with index 1 must return null.")]
    public void GetSlotMethod_Index1_MustReturnNull()
    {
        // Arrange
        ArrayElementSyntax arrayElement = SyntaxFactory.ArrayElement(this.expression);

        // Act
        GreenNode? actualSlot = arrayElement.GetSlot(1);

        // Assert
        Assert.Null(actualSlot);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.Width)} property must consider all slots.")]
    public void WidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        ArrayElementSyntax arrayElement = SyntaxFactory.ArrayElement(this.expression);

        // Act
        int actualWidth = arrayElement.Width;

        // Assert
        Assert.Equal(3, actualWidth);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.FullWidth)} property must consider all slots.")]
    public void FullWidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        ArrayElementSyntax arrayElement = SyntaxFactory.ArrayElement(this.expression);

        // Act
        int actualFullWidth = arrayElement.FullWidth;

        // Assert
        Assert.Equal(5, actualFullWidth);
    }
}

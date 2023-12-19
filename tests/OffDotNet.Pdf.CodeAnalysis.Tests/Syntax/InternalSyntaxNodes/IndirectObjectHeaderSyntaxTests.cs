// <copyright file="IndirectObjectHeaderSyntaxTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using SyntaxToken = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxToken;
using SyntaxTrivia = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxTrivia;

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxTests;

public class IndirectObjectHeaderSyntaxTests
{
    private readonly LiteralExpressionSyntax objectNumber;
    private readonly LiteralExpressionSyntax generationNumber;
    private readonly SyntaxToken startObjectKeyword;

    public IndirectObjectHeaderSyntaxTests()
    {
        GreenNode trivia = SyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, " ");
        SyntaxToken objectNumberToken = SyntaxFactory.Token(SyntaxKind.NumericLiteralToken, "123", 123, trivia, trivia);
        SyntaxToken generationNumberToken = SyntaxFactory.Token(SyntaxKind.NumericLiteralToken, "32", 32, trivia, trivia);

        this.objectNumber = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, objectNumberToken);
        this.generationNumber = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, generationNumberToken);
        this.startObjectKeyword = SyntaxFactory.Token(SyntaxKind.StartObjectKeyword, trivia, trivia);
    }

    [Fact(DisplayName = $"The {nameof(IndirectObjectHeaderSyntax.Kind)} property must be {nameof(SyntaxKind.IndirectObjectHeader)}")]
    public void KindProperty_MustBeIndirectObjectHeader()
    {
        // Arrange
        IndirectObjectHeaderSyntax indirectObjectHeader = SyntaxFactory.IndirectObjectHeader(this.objectNumber, this.generationNumber, this.startObjectKeyword);

        // Act
        SyntaxKind actualKind = indirectObjectHeader.Kind;

        // Assert
        Assert.Equal(SyntaxKind.IndirectObjectHeader, actualKind);
    }

    [Fact(DisplayName = $"The {nameof(IndirectObjectHeaderSyntax.ObjectNumber)} property must be assigned from constructor.")]
    public void ObjectNumberProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        IndirectObjectHeaderSyntax indirectObjectHeader = SyntaxFactory.IndirectObjectHeader(this.objectNumber, this.generationNumber, this.startObjectKeyword);

        // Act
        LiteralExpressionSyntax actualObjectNumber = indirectObjectHeader.ObjectNumber;

        // Assert
        Assert.Equal(this.objectNumber, actualObjectNumber);
    }

    [Fact(DisplayName = $"The {nameof(IndirectObjectHeaderSyntax.GenerationNumber)} property must be assigned from constructor.")]
    public void GenerationNumberProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        IndirectObjectHeaderSyntax indirectObjectHeader = SyntaxFactory.IndirectObjectHeader(this.objectNumber, this.generationNumber, this.startObjectKeyword);

        // Act
        LiteralExpressionSyntax actualGenerationNumber = indirectObjectHeader.GenerationNumber;

        // Assert
        Assert.Equal(this.generationNumber, actualGenerationNumber);
    }

    [Fact(DisplayName = $"The {nameof(IndirectObjectHeaderSyntax.StartObjectKeyword)} property must be assigned from constructor.")]
    public void ReferenceKeywordProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        IndirectObjectHeaderSyntax indirectObjectHeader = SyntaxFactory.IndirectObjectHeader(this.objectNumber, this.generationNumber, this.startObjectKeyword);

        // Act
        SyntaxToken actualReferenceKeyword = indirectObjectHeader.StartObjectKeyword;

        // Assert
        Assert.Equal(this.startObjectKeyword, actualReferenceKeyword);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.SlotCount)} property must be equal to 3.")]
    public void SlotCountProperty_MustBeEqualTo3()
    {
        // Arrange
        IndirectObjectHeaderSyntax indirectObjectHeader = SyntaxFactory.IndirectObjectHeader(this.objectNumber, this.generationNumber, this.startObjectKeyword);

        // Act
        int actualSlotCount = indirectObjectHeader.SlotCount;

        // Assert
        Assert.Equal(3, actualSlotCount);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 0 must return {nameof(IndirectObjectHeaderSyntax.ObjectNumber)} property.")]
    public void GetSlotMethod_Index0_MustReturnFirstObject()
    {
        // Arrange
        IndirectObjectHeaderSyntax indirectObjectHeader = SyntaxFactory.IndirectObjectHeader(this.objectNumber, this.generationNumber, this.startObjectKeyword);

        // Act
        GreenNode? actualSlot = indirectObjectHeader.GetSlot(0);

        // Assert
        Assert.Equal(this.objectNumber, actualSlot);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 1 must return {nameof(IndirectObjectHeaderSyntax.GenerationNumber)} property.")]
    public void GetSlotMethod_Index1_MustReturnSecondObject()
    {
        // Arrange
        IndirectObjectHeaderSyntax indirectObjectHeader = SyntaxFactory.IndirectObjectHeader(this.objectNumber, this.generationNumber, this.startObjectKeyword);

        // Act
        GreenNode? actualSlot = indirectObjectHeader.GetSlot(1);

        // Assert
        Assert.Equal(this.generationNumber, actualSlot);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 2 must return {nameof(IndirectObjectHeaderSyntax.StartObjectKeyword)} property.")]
    public void GetSlotMethod_Index2_MustReturnThirdObject()
    {
        // Arrange
        IndirectObjectHeaderSyntax indirectObjectHeader = SyntaxFactory.IndirectObjectHeader(this.objectNumber, this.generationNumber, this.startObjectKeyword);

        // Act
        GreenNode? actualSlot = indirectObjectHeader.GetSlot(2);

        // Assert
        Assert.Equal(this.startObjectKeyword, actualSlot);
    }

    [Fact(DisplayName = "The GetSlot() method with index 3 must return null.")]
    public void GetSlotMethod_Index3_MustReturnNull()
    {
        // Arrange
        IndirectObjectHeaderSyntax indirectObjectHeader = SyntaxFactory.IndirectObjectHeader(this.objectNumber, this.generationNumber, this.startObjectKeyword);

        // Act
        GreenNode? actualSlot = indirectObjectHeader.GetSlot(3);

        // Assert
        Assert.Null(actualSlot);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.Width)} property must consider all slots.")]
    public void WidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        IndirectObjectHeaderSyntax indirectObjectHeader = SyntaxFactory.IndirectObjectHeader(this.objectNumber, this.generationNumber, this.startObjectKeyword);

        // Act
        int actualWidth = indirectObjectHeader.Width;

        // Assert
        Assert.Equal(12, actualWidth);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.FullWidth)} property must consider all slots.")]
    public void FullWidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        IndirectObjectHeaderSyntax indirectObjectHeader = SyntaxFactory.IndirectObjectHeader(this.objectNumber, this.generationNumber, this.startObjectKeyword);

        // Act
        int actualFullWidth = indirectObjectHeader.FullWidth;

        // Assert
        Assert.Equal(14, actualFullWidth);
    }
}

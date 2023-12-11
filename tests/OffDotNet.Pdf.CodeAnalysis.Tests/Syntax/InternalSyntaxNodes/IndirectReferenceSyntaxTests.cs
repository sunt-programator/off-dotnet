// <copyright file="IndirectReferenceSyntaxTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using SyntaxToken = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxToken;
using SyntaxTrivia = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxTrivia;

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxTests;

public class IndirectReferenceSyntaxTests
{
    private readonly LiteralExpressionSyntax objectNumber;
    private readonly LiteralExpressionSyntax generationNumber;
    private readonly SyntaxToken referenceKeyword;

    public IndirectReferenceSyntaxTests()
    {
        GreenNode trivia = SyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, " ");
        SyntaxToken objectNumberToken = SyntaxFactory.Token(SyntaxKind.NumericLiteralToken, "123", 123, trivia, trivia);
        SyntaxToken generationNumberToken = SyntaxFactory.Token(SyntaxKind.NumericLiteralToken, "32", 32, trivia, trivia);

        this.objectNumber = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, objectNumberToken);
        this.generationNumber = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, generationNumberToken);
        this.referenceKeyword = SyntaxFactory.Token(SyntaxKind.IndirectReferenceKeyword, trivia, trivia);
    }

    [Fact(DisplayName = $"The {nameof(IndirectReferenceSyntax.Kind)} property must be {nameof(SyntaxKind.IndirectReference)}")]
    public void KindProperty_MustBeIndirectReferenceExpression()
    {
        // Arrange
        IndirectReferenceSyntax indirectReference = SyntaxFactory.IndirectReference(this.objectNumber, this.generationNumber, this.referenceKeyword);

        // Act
        SyntaxKind actualObjectNumber = indirectReference.Kind;

        // Assert
        Assert.Equal(SyntaxKind.IndirectReference, actualObjectNumber);
    }

    [Fact(DisplayName = $"The {nameof(IndirectReferenceSyntax.ObjectNumber)} property must be assigned from constructor.")]
    public void ObjectNumberProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        IndirectReferenceSyntax indirectReference = SyntaxFactory.IndirectReference(this.objectNumber, this.generationNumber, this.referenceKeyword);

        // Act
        LiteralExpressionSyntax actualObjectNumber = indirectReference.ObjectNumber;

        // Assert
        Assert.Equal(this.objectNumber, actualObjectNumber);
    }

    [Fact(DisplayName = $"The {nameof(IndirectReferenceSyntax.GenerationNumber)} property must be assigned from constructor.")]
    public void GenerationNumberProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        IndirectReferenceSyntax indirectReference = SyntaxFactory.IndirectReference(this.objectNumber, this.generationNumber, this.referenceKeyword);

        // Act
        LiteralExpressionSyntax actualGenerationNumber = indirectReference.GenerationNumber;

        // Assert
        Assert.Equal(this.generationNumber, actualGenerationNumber);
    }

    [Fact(DisplayName = $"The {nameof(IndirectReferenceSyntax.ReferenceKeyword)} property must be assigned from constructor.")]
    public void ReferenceKeywordProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        IndirectReferenceSyntax indirectReference = SyntaxFactory.IndirectReference(this.objectNumber, this.generationNumber, this.referenceKeyword);

        // Act
        SyntaxToken actualReferenceKeyword = indirectReference.ReferenceKeyword;

        // Assert
        Assert.Equal(this.referenceKeyword, actualReferenceKeyword);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.SlotCount)} property must be equal to 3.")]
    public void SlotCountProperty_MustBeEqualTo3()
    {
        // Arrange
        IndirectReferenceSyntax indirectReference = SyntaxFactory.IndirectReference(this.objectNumber, this.generationNumber, this.referenceKeyword);

        // Act
        int actualSlotCount = indirectReference.SlotCount;

        // Assert
        Assert.Equal(3, actualSlotCount);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 0 must return {nameof(IndirectReferenceSyntax.ObjectNumber)} property.")]
    public void GetSlotMethod_Index0_MustReturnFirstObject()
    {
        // Arrange
        IndirectReferenceSyntax indirectReference = SyntaxFactory.IndirectReference(this.objectNumber, this.generationNumber, this.referenceKeyword);

        // Act
        GreenNode? actualSlot = indirectReference.GetSlot(0);

        // Assert
        Assert.Equal(this.objectNumber, actualSlot);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 1 must return {nameof(IndirectReferenceSyntax.GenerationNumber)} property.")]
    public void GetSlotMethod_Index1_MustReturnSecondObject()
    {
        // Arrange
        IndirectReferenceSyntax indirectReference = SyntaxFactory.IndirectReference(this.objectNumber, this.generationNumber, this.referenceKeyword);

        // Act
        GreenNode? actualSlot = indirectReference.GetSlot(1);

        // Assert
        Assert.Equal(this.generationNumber, actualSlot);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 2 must return {nameof(IndirectReferenceSyntax.ReferenceKeyword)} property.")]
    public void GetSlotMethod_Index2_MustReturnThirdObject()
    {
        // Arrange
        IndirectReferenceSyntax indirectReference = SyntaxFactory.IndirectReference(this.objectNumber, this.generationNumber, this.referenceKeyword);

        // Act
        GreenNode? actualSlot = indirectReference.GetSlot(2);

        // Assert
        Assert.Equal(this.referenceKeyword, actualSlot);
    }

    [Fact(DisplayName = "The GetSlot() method with index 3 must return null.")]
    public void GetSlotMethod_Index3_MustReturnNull()
    {
        // Arrange
        IndirectReferenceSyntax indirectReference = SyntaxFactory.IndirectReference(this.objectNumber, this.generationNumber, this.referenceKeyword);

        // Act
        GreenNode? actualSlot = indirectReference.GetSlot(3);

        // Assert
        Assert.Null(actualSlot);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.Width)} property must consider all slots.")]
    public void WidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        IndirectReferenceSyntax indirectReference = SyntaxFactory.IndirectReference(this.objectNumber, this.generationNumber, this.referenceKeyword);

        // Act
        int actualWidth = indirectReference.Width;

        // Assert
        Assert.Equal(10, actualWidth);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.FullWidth)} property must consider all slots.")]
    public void FullWidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        IndirectReferenceSyntax indirectReference = SyntaxFactory.IndirectReference(this.objectNumber, this.generationNumber, this.referenceKeyword);

        // Act
        int actualFullWidth = indirectReference.FullWidth;

        // Assert
        Assert.Equal(12, actualFullWidth);
    }
}

// <copyright file="XRefEntryExpressionSyntaxTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using SyntaxToken = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxToken;
using SyntaxTrivia = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxTrivia;

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxTests;

public class XRefEntryExpressionSyntaxTests
{
    private readonly LiteralExpressionSyntax offset;
    private readonly LiteralExpressionSyntax generationNumber;
    private readonly SyntaxToken entryTypeKeyword;

    public XRefEntryExpressionSyntaxTests()
    {
        GreenNode trivia = SyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, " ");
        SyntaxToken offsetToken = SyntaxFactory.Token(SyntaxKind.NumericLiteralToken, "0000000017", 17, trivia, trivia);
        SyntaxToken generationNumberToken = SyntaxFactory.Token(SyntaxKind.NumericLiteralToken, "00007", 7, trivia, trivia);

        this.offset = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, offsetToken);
        this.generationNumber = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, generationNumberToken);
        this.entryTypeKeyword = SyntaxFactory.Token(SyntaxKind.IndirectReferenceKeyword, trivia, trivia);
    }

    [Fact(DisplayName = $"The {nameof(XRefEntryExpressionSyntax.Kind)} property must be {nameof(SyntaxKind.IndirectReference)}")]
    public void KindProperty_MustBeIndirectReferenceExpression()
    {
        // Arrange
        XRefEntryExpressionSyntax xRefEntryExpression = SyntaxFactory.XRefEntry(this.offset, this.generationNumber, this.entryTypeKeyword);

        // Act
        SyntaxKind actualObjectNumber = xRefEntryExpression.Kind;

        // Assert
        Assert.Equal(SyntaxKind.IndirectReference, actualObjectNumber);
    }

    [Fact(DisplayName = $"The {nameof(XRefEntryExpressionSyntax.Offset)} property must be assigned from constructor.")]
    public void OffsetProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        XRefEntryExpressionSyntax xRefEntryExpression = SyntaxFactory.XRefEntry(this.offset, this.generationNumber, this.entryTypeKeyword);

        // Act
        LiteralExpressionSyntax actualOffset = xRefEntryExpression.Offset;

        // Assert
        Assert.Equal(this.offset, actualOffset);
    }

    [Fact(DisplayName = $"The {nameof(XRefEntryExpressionSyntax.GenerationNumber)} property must be assigned from constructor.")]
    public void GenerationNumberProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        XRefEntryExpressionSyntax xRefEntryExpression = SyntaxFactory.XRefEntry(this.offset, this.generationNumber, this.entryTypeKeyword);

        // Act
        LiteralExpressionSyntax actualGenerationNumber = xRefEntryExpression.GenerationNumber;

        // Assert
        Assert.Equal(this.generationNumber, actualGenerationNumber);
    }

    [Fact(DisplayName = $"The {nameof(XRefEntryExpressionSyntax.EntryType)} property must be assigned from constructor.")]
    public void ReferenceKeywordProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        XRefEntryExpressionSyntax xRefEntryExpression = SyntaxFactory.XRefEntry(this.offset, this.generationNumber, this.entryTypeKeyword);

        // Act
        SyntaxToken actualEntryType = xRefEntryExpression.EntryType;

        // Assert
        Assert.Equal(this.entryTypeKeyword, actualEntryType);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.SlotCount)} property must be equal to 3.")]
    public void SlotCountProperty_MustBeEqualTo3()
    {
        // Arrange
        XRefEntryExpressionSyntax xRefEntryExpression = SyntaxFactory.XRefEntry(this.offset, this.generationNumber, this.entryTypeKeyword);

        // Act
        int actualSlotCount = xRefEntryExpression.SlotCount;

        // Assert
        Assert.Equal(3, actualSlotCount);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 0 must return {nameof(XRefEntryExpressionSyntax.Offset)} property.")]
    public void GetSlotMethod_Index0_MustReturnFirstObject()
    {
        // Arrange
        XRefEntryExpressionSyntax xRefEntryExpression = SyntaxFactory.XRefEntry(this.offset, this.generationNumber, this.entryTypeKeyword);

        // Act
        GreenNode? actualSlot = xRefEntryExpression.GetSlot(0);

        // Assert
        Assert.Equal(this.offset, actualSlot);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 1 must return {nameof(XRefEntryExpressionSyntax.GenerationNumber)} property.")]
    public void GetSlotMethod_Index1_MustReturnSecondObject()
    {
        // Arrange
        XRefEntryExpressionSyntax xRefEntryExpression = SyntaxFactory.XRefEntry(this.offset, this.generationNumber, this.entryTypeKeyword);

        // Act
        GreenNode? actualSlot = xRefEntryExpression.GetSlot(1);

        // Assert
        Assert.Equal(this.generationNumber, actualSlot);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 2 must return {nameof(XRefEntryExpressionSyntax.EntryType)} property.")]
    public void GetSlotMethod_Index2_MustReturnThirdObject()
    {
        // Arrange
        XRefEntryExpressionSyntax xRefEntryExpression = SyntaxFactory.XRefEntry(this.offset, this.generationNumber, this.entryTypeKeyword);

        // Act
        GreenNode? actualSlot = xRefEntryExpression.GetSlot(2);

        // Assert
        Assert.Equal(this.entryTypeKeyword, actualSlot);
    }

    [Fact(DisplayName = "The GetSlot() method with index 3 must return null.")]
    public void GetSlotMethod_Index3_MustReturnNull()
    {
        // Arrange
        XRefEntryExpressionSyntax xRefEntryExpression = SyntaxFactory.XRefEntry(this.offset, this.generationNumber, this.entryTypeKeyword);

        // Act
        GreenNode? actualSlot = xRefEntryExpression.GetSlot(3);

        // Assert
        Assert.Null(actualSlot);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.Width)} property must consider all slots.")]
    public void WidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        XRefEntryExpressionSyntax xRefEntryExpression = SyntaxFactory.XRefEntry(this.offset, this.generationNumber, this.entryTypeKeyword);

        // Act
        int actualWidth = xRefEntryExpression.Width;

        // Assert
        Assert.Equal(20, actualWidth);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.FullWidth)} property must consider all slots.")]
    public void FullWidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        XRefEntryExpressionSyntax xRefEntryExpression = SyntaxFactory.XRefEntry(this.offset, this.generationNumber, this.entryTypeKeyword);

        // Act
        int actualFullWidth = xRefEntryExpression.FullWidth;

        // Assert
        Assert.Equal(22, actualFullWidth);
    }
}

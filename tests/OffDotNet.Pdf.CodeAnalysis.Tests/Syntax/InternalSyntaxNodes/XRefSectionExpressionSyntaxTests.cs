// <copyright file="XRefSectionExpressionSyntaxTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using SyntaxToken = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxToken;
using SyntaxTrivia = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxTrivia;

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxTests;

public class XRefSectionExpressionSyntaxTests
{
    private readonly SyntaxToken xRefKeyword;
    private readonly SyntaxList<XRefSubSectionExpressionSyntax> subSections;

    public XRefSectionExpressionSyntaxTests()
    {
        GreenNode trivia = SyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, " ");
        SyntaxToken firstObjectNumberToken = SyntaxFactory.Token(SyntaxKind.NumericLiteralToken, "28", 28, trivia, trivia);
        SyntaxToken numberOfEntriesToken = SyntaxFactory.Token(SyntaxKind.NumericLiteralToken, "2", 2, trivia, trivia);

        SyntaxToken offsetToken = SyntaxFactory.Token(SyntaxKind.NumericLiteralToken, "0000000017", 17, trivia, trivia);
        SyntaxToken generationNumberToken = SyntaxFactory.Token(SyntaxKind.NumericLiteralToken, "00007", 7, trivia, trivia);

        LiteralExpressionSyntax offset = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, offsetToken);
        LiteralExpressionSyntax generationNumber = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, generationNumberToken);
        SyntaxToken entryTypeKeyword = SyntaxFactory.Token(SyntaxKind.FreeXRefEntryKeyword, trivia, trivia);

        XRefEntryExpressionSyntax xRefEntryExpressionSyntax = SyntaxFactory.XRefEntry(offset, generationNumber, entryTypeKeyword);

        LiteralExpressionSyntax objectNumber = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, firstObjectNumberToken);
        LiteralExpressionSyntax numberOfEntries = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, numberOfEntriesToken);
        SyntaxList<XRefEntryExpressionSyntax> entries = new SyntaxListBuilder(2)
            .AddRange(new GreenNode[] { xRefEntryExpressionSyntax, xRefEntryExpressionSyntax })
            .ToList<XRefEntryExpressionSyntax>();

        XRefSubSectionExpressionSyntax xRefSectionExpression = SyntaxFactory.XRefSubSection(objectNumber, numberOfEntries, entries);

        this.xRefKeyword = SyntaxFactory.Token(SyntaxKind.XRefKeyword, trivia, trivia);
        this.subSections = new SyntaxListBuilder(2)
            .AddRange(new GreenNode[] { xRefSectionExpression, xRefSectionExpression })
            .ToList<XRefSubSectionExpressionSyntax>();
    }

    [Fact(DisplayName = $"The {nameof(XRefSectionExpressionSyntax.Kind)} property must be {nameof(SyntaxKind.XRefSection)}")]
    public void KindProperty_MustBeXRefSection()
    {
        // Arrange
        XRefSectionExpressionSyntax xRefSectionExpression = SyntaxFactory.XRefSection(this.xRefKeyword, this.subSections);

        // Act
        SyntaxKind actualKind = xRefSectionExpression.Kind;

        // Assert
        Assert.Equal(SyntaxKind.XRefSection, actualKind);
    }

    [Fact(DisplayName = $"The {nameof(XRefSectionExpressionSyntax.XRefKeyword)} property must be assigned from constructor.")]
    public void XRefKeywordProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        XRefSectionExpressionSyntax xRefSectionExpression = SyntaxFactory.XRefSection(this.xRefKeyword, this.subSections);

        // Act
        SyntaxToken actualXRefKeyword = xRefSectionExpression.XRefKeyword;

        // Assert
        Assert.Equal(this.xRefKeyword, actualXRefKeyword);
    }

    [Fact(DisplayName = $"The {nameof(XRefSectionExpressionSyntax.SubSections)} property must be assigned from constructor.")]
    public void SubSectionsProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        XRefSectionExpressionSyntax xRefSectionExpression = SyntaxFactory.XRefSection(this.xRefKeyword, this.subSections);

        // Act
        GreenNode? actualSubSections = xRefSectionExpression.SubSections;

        // Assert
        Assert.Equal(this.subSections.Node, actualSubSections);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.SlotCount)} property must be equal to 2.")]
    public void SlotCountProperty_MustBeEqualTo3()
    {
        // Arrange
        XRefSectionExpressionSyntax xRefSectionExpression = SyntaxFactory.XRefSection(this.xRefKeyword, this.subSections);

        // Act
        int actualSlotCount = xRefSectionExpression.SlotCount;

        // Assert
        Assert.Equal(2, actualSlotCount);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 0 must return {nameof(XRefSectionExpressionSyntax.XRefKeyword)} property.")]
    public void GetSlotMethod_Index0_MustReturnFirstObject()
    {
        // Arrange
        XRefSectionExpressionSyntax xRefSectionExpression = SyntaxFactory.XRefSection(this.xRefKeyword, this.subSections);

        // Act
        GreenNode? actualSlot = xRefSectionExpression.GetSlot(0);

        // Assert
        Assert.Equal(this.xRefKeyword, actualSlot);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 1 must return {nameof(XRefSectionExpressionSyntax.SubSections)} property.")]
    public void GetSlotMethod_Index1_MustReturnSecondObject()
    {
        // Arrange
        XRefSectionExpressionSyntax xRefSectionExpression = SyntaxFactory.XRefSection(this.xRefKeyword, this.subSections);

        // Act
        GreenNode? actualSlot = xRefSectionExpression.GetSlot(1);

        // Assert
        Assert.Equal(this.subSections.Node, actualSlot);
    }

    [Fact(DisplayName = "The GetSlot() method with index 2 must return null.")]
    public void GetSlotMethod_Index3_MustReturnNull()
    {
        // Arrange
        XRefSectionExpressionSyntax xRefSectionExpression = SyntaxFactory.XRefSection(this.xRefKeyword, this.subSections);

        // Act
        GreenNode? actualSlot = xRefSectionExpression.GetSlot(2);

        // Assert
        Assert.Null(actualSlot);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.Width)} property must consider all slots.")]
    public void WidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        XRefSectionExpressionSyntax xRefSectionExpression = SyntaxFactory.XRefSection(this.xRefKeyword, this.subSections);

        // Act
        int actualWidth = xRefSectionExpression.Width;

        // Assert
        Assert.Equal(106, actualWidth);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.FullWidth)} property must consider all slots.")]
    public void FullWidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        XRefSectionExpressionSyntax xRefSectionExpression = SyntaxFactory.XRefSection(this.xRefKeyword, this.subSections);

        // Act
        int actualFullWidth = xRefSectionExpression.FullWidth;

        // Assert
        Assert.Equal(108, actualFullWidth);
    }

    [Fact(DisplayName = "The ToString() method must not include the trivia.")]
    public void ToStringMethod_MustNotIncludeTrivia()
    {
        // Arrange
        const string expectedString = "xref  28  2  0000000017  00007  f  0000000017  00007  f  28  2  0000000017  00007  f  0000000017  00007  f";
        XRefSectionExpressionSyntax xRefSectionExpression = SyntaxFactory.XRefSection(this.xRefKeyword, this.subSections);

        // Act
        string actualString = xRefSectionExpression.ToString();

        // Assert
        Assert.Equal(expectedString, actualString);
    }

    [Fact(DisplayName = "The ToFullString() method must include the trivia.")]
    public void ToFullStringMethod_MustIncludeTrivia()
    {
        // Arrange
        const string expectedString = " xref  28  2  0000000017  00007  f  0000000017  00007  f  28  2  0000000017  00007  f  0000000017  00007  f ";
        XRefSectionExpressionSyntax xRefSectionExpression = SyntaxFactory.XRefSection(this.xRefKeyword, this.subSections);

        // Act
        string actualString = xRefSectionExpression.ToFullString();

        // Assert
        Assert.Equal(expectedString, actualString);
    }
}

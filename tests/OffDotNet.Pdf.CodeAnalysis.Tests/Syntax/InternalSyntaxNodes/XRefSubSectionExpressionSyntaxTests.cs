// <copyright file="XRefSubSectionExpressionSyntaxTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxTests;

using OffDotNet.Pdf.CodeAnalysis.Diagnostic;
using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using SyntaxToken = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxToken;
using SyntaxTrivia = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxTrivia;

public class XRefSubSectionExpressionSyntaxTests
{
    private readonly LiteralExpressionSyntax objectNumber;
    private readonly LiteralExpressionSyntax numberOfEntries;
    private readonly SyntaxList<XRefEntryExpressionSyntax> entries;

    public XRefSubSectionExpressionSyntaxTests()
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

        SyntaxList<XRefEntryExpressionSyntax> list = new SyntaxListBuilder(2)
            .AddRange(new GreenNode[] { xRefEntryExpressionSyntax, xRefEntryExpressionSyntax })
            .ToList<XRefEntryExpressionSyntax>();

        this.objectNumber = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, firstObjectNumberToken);
        this.numberOfEntries = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, numberOfEntriesToken);
        this.entries = list;
    }

    [Fact(DisplayName = $"The {nameof(XRefSubSectionExpressionSyntax.Kind)} property must be {nameof(SyntaxKind.XRefSubSection)}")]
    public void KindProperty_MustBeXRefSubSection()
    {
        // Arrange
        XRefSubSectionExpressionSyntax xRefSubSectionExpression = SyntaxFactory.XRefSubSection(this.objectNumber, this.numberOfEntries, this.entries);

        // Act
        SyntaxKind actualKind = xRefSubSectionExpression.Kind;

        // Assert
        Assert.Equal(SyntaxKind.XRefSubSection, actualKind);
    }

    [Fact(DisplayName = $"The {nameof(XRefSubSectionExpressionSyntax.ObjectNumber)} property must be assigned from constructor.")]
    public void ObjectNumberProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        XRefSubSectionExpressionSyntax xRefSubSectionExpression = SyntaxFactory.XRefSubSection(this.objectNumber, this.numberOfEntries, this.entries);

        // Act
        LiteralExpressionSyntax actualObjectNumber = xRefSubSectionExpression.ObjectNumber;

        // Assert
        Assert.Equal(this.objectNumber, actualObjectNumber);
    }

    [Fact(DisplayName = $"The {nameof(XRefSubSectionExpressionSyntax.NumberOfEntries)} property must be assigned from constructor.")]
    public void NumberOfEntriesProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        XRefSubSectionExpressionSyntax xRefSubSectionExpression = SyntaxFactory.XRefSubSection(this.objectNumber, this.numberOfEntries, this.entries);

        // Act
        LiteralExpressionSyntax actualNumberOfEntries = xRefSubSectionExpression.NumberOfEntries;

        // Assert
        Assert.Equal(this.numberOfEntries, actualNumberOfEntries);
    }

    [Fact(DisplayName = $"The {nameof(XRefSubSectionExpressionSyntax.Entries)} property must be assigned from constructor.")]
    public void EntriesProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        XRefSubSectionExpressionSyntax xRefSubSectionExpression = SyntaxFactory.XRefSubSection(this.objectNumber, this.numberOfEntries, this.entries);

        // Act
        GreenNode? actualEntries = xRefSubSectionExpression.Entries;

        // Assert
        Assert.Equal(this.entries.Node, actualEntries);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.SlotCount)} property must be equal to 3.")]
    public void SlotCountProperty_MustBeEqualTo3()
    {
        // Arrange
        XRefSubSectionExpressionSyntax xRefSubSectionExpression = SyntaxFactory.XRefSubSection(this.objectNumber, this.numberOfEntries, this.entries);

        // Act
        int actualSlotCount = xRefSubSectionExpression.SlotCount;

        // Assert
        Assert.Equal(3, actualSlotCount);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 0 must return {nameof(XRefSubSectionExpressionSyntax.ObjectNumber)} property.")]
    public void GetSlotMethod_Index0_MustReturnFirstObject()
    {
        // Arrange
        XRefSubSectionExpressionSyntax xRefSubSectionExpression = SyntaxFactory.XRefSubSection(this.objectNumber, this.numberOfEntries, this.entries);

        // Act
        GreenNode? actualSlot = xRefSubSectionExpression.GetSlot(0);

        // Assert
        Assert.Equal(this.objectNumber, actualSlot);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 1 must return {nameof(XRefSubSectionExpressionSyntax.NumberOfEntries)} property.")]
    public void GetSlotMethod_Index1_MustReturnSecondObject()
    {
        // Arrange
        XRefSubSectionExpressionSyntax xRefSubSectionExpression = SyntaxFactory.XRefSubSection(this.objectNumber, this.numberOfEntries, this.entries);

        // Act
        GreenNode? actualSlot = xRefSubSectionExpression.GetSlot(1);

        // Assert
        Assert.Equal(this.numberOfEntries, actualSlot);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 2 must return {nameof(XRefSubSectionExpressionSyntax.Entries)} property.")]
    public void GetSlotMethod_Index2_MustReturnThirdObject()
    {
        // Arrange
        XRefSubSectionExpressionSyntax xRefSubSectionExpression = SyntaxFactory.XRefSubSection(this.objectNumber, this.numberOfEntries, this.entries);

        // Act
        GreenNode? actualSlot = xRefSubSectionExpression.GetSlot(2);

        // Assert
        Assert.Equal(this.entries.Node, actualSlot);
    }

    [Fact(DisplayName = "The GetSlot() method with index 3 must return null.")]
    public void GetSlotMethod_Index3_MustReturnNull()
    {
        // Arrange
        XRefSubSectionExpressionSyntax xRefSubSectionExpression = SyntaxFactory.XRefSubSection(this.objectNumber, this.numberOfEntries, this.entries);

        // Act
        GreenNode? actualSlot = xRefSubSectionExpression.GetSlot(3);

        // Assert
        Assert.Null(actualSlot);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.Width)} property must consider all slots.")]
    public void WidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        XRefSubSectionExpressionSyntax xRefSubSectionExpression = SyntaxFactory.XRefSubSection(this.objectNumber, this.numberOfEntries, this.entries);

        // Act
        int actualWidth = xRefSubSectionExpression.Width;

        // Assert
        Assert.Equal(49, actualWidth);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.FullWidth)} property must consider all slots.")]
    public void FullWidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        XRefSubSectionExpressionSyntax xRefSubSectionExpression = SyntaxFactory.XRefSubSection(this.objectNumber, this.numberOfEntries, this.entries);

        // Act
        int actualFullWidth = xRefSubSectionExpression.FullWidth;

        // Assert
        Assert.Equal(51, actualFullWidth);
    }

    [Fact(DisplayName = "The ToString() method must not include the trivia.")]
    public void ToStringMethod_MustNotIncludeTrivia()
    {
        // Arrange
        const string expectedString = "28  2  0000000017  00007  f  0000000017  00007  f";
        XRefSubSectionExpressionSyntax xRefSubSectionExpression = SyntaxFactory.XRefSubSection(this.objectNumber, this.numberOfEntries, this.entries);

        // Act
        string actualString = xRefSubSectionExpression.ToString();

        // Assert
        Assert.Equal(expectedString, actualString);
    }

    [Fact(DisplayName = "The ToFullString() method must include the trivia.")]
    public void ToFullStringMethod_MustIncludeTrivia()
    {
        // Arrange
        const string expectedString = " 28  2  0000000017  00007  f  0000000017  00007  f ";
        XRefSubSectionExpressionSyntax xRefSubSectionExpression = SyntaxFactory.XRefSubSection(this.objectNumber, this.numberOfEntries, this.entries);

        // Act
        string actualString = xRefSubSectionExpression.ToFullString();

        // Assert
        Assert.Equal(expectedString, actualString);
    }

    [Fact(DisplayName = "The SetDiagnostics() method must set the diagnostics and return a new instance.")]
    public void SetDiagnosticsMethod_MustSetTheDiagnosticsAndReturnNewInstance()
    {
        // Arrange
        XRefSubSectionExpressionSyntax xRefSubSectionExpression = SyntaxFactory.XRefSubSection(this.objectNumber, this.numberOfEntries, this.entries);
        DiagnosticInfo expectedDiagnostic = new(Substitute.For<IMessageProvider>(), DiagnosticCode.ERR_InvalidPDF);
        DiagnosticInfo[] diagnostics = { expectedDiagnostic };

        // Act
        XRefSubSectionExpressionSyntax actualXRefSubSectionExpression = (XRefSubSectionExpressionSyntax)xRefSubSectionExpression.SetDiagnostics(diagnostics);

        // Assert
        Assert.NotEqual(xRefSubSectionExpression, actualXRefSubSectionExpression);
        Assert.Equal(diagnostics, actualXRefSubSectionExpression.GetDiagnostics());
        Assert.True(actualXRefSubSectionExpression.ContainsFlags(GreenNode.NodeFlags.ContainsDiagnostics));
    }
}

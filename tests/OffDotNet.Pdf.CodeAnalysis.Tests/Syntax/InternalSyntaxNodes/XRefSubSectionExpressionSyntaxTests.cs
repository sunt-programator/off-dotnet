// <copyright file="XRefSubSectionExpressionSyntaxTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxNodes;

using OffDotNet.Pdf.CodeAnalysis.Diagnostic;
using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntaxNodes;
using SyntaxFactory = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxFactory;
using SyntaxTrivia = CodeAnalysis.Syntax.InternalSyntax.SyntaxTrivia;

public class XRefSubSectionExpressionSyntaxTests
{
    private readonly LiteralExpressionSyntax _objectNumber;
    private readonly LiteralExpressionSyntax _numberOfEntries;
    private readonly SyntaxList<XRefEntryExpressionSyntax> _entries;

    public XRefSubSectionExpressionSyntaxTests()
    {
        GreenNode trivia = SyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, " ");
        var firstObjectNumberToken = SyntaxFactory.Token(SyntaxKind.NumericLiteralToken, "28", 28, trivia, trivia);
        var numberOfEntriesToken = SyntaxFactory.Token(SyntaxKind.NumericLiteralToken, "2", 2, trivia, trivia);

        var offsetToken = SyntaxFactory.Token(SyntaxKind.NumericLiteralToken, "0000000017", 17, trivia, trivia);
        var generationNumberToken = SyntaxFactory.Token(SyntaxKind.NumericLiteralToken, "00007", 7, trivia, trivia);

        var offset = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, offsetToken);
        var generationNumber = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, generationNumberToken);
        var entryTypeKeyword = SyntaxFactory.Token(SyntaxKind.FreeXRefEntryKeyword, trivia, trivia);

        var xRefEntryExpressionSyntax = SyntaxFactory.XRefEntry(offset, generationNumber, entryTypeKeyword);

        SyntaxList<XRefEntryExpressionSyntax> list = new SyntaxListBuilder(2)
            .AddRange(new GreenNode[] { xRefEntryExpressionSyntax, xRefEntryExpressionSyntax })
            .ToList<XRefEntryExpressionSyntax>();

        _objectNumber = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, firstObjectNumberToken);
        _numberOfEntries = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, numberOfEntriesToken);
        _entries = list;
    }

    [Fact(DisplayName = $"The {nameof(XRefSubSectionExpressionSyntax.Kind)} property must be {nameof(SyntaxKind.XRefSubSection)}")]
    public void KindProperty_MustBeXRefSubSection()
    {
        // Arrange
        var xRefSubSectionExpression = SyntaxFactory.XRefSubSection(_objectNumber, _numberOfEntries, _entries);

        // Act
        var actualKind = xRefSubSectionExpression.Kind;

        // Assert
        Assert.Equal(SyntaxKind.XRefSubSection, actualKind);
    }

    [Fact(DisplayName = $"The {nameof(XRefSubSectionExpressionSyntax.ObjectNumber)} property must be assigned from constructor.")]
    public void ObjectNumberProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        var xRefSubSectionExpression = SyntaxFactory.XRefSubSection(_objectNumber, _numberOfEntries, _entries);

        // Act
        var actualObjectNumber = xRefSubSectionExpression.ObjectNumber;

        // Assert
        Assert.Same(_objectNumber, actualObjectNumber);
    }

    [Fact(DisplayName = $"The {nameof(XRefSubSectionExpressionSyntax.NumberOfEntries)} property must be assigned from constructor.")]
    public void NumberOfEntriesProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        var xRefSubSectionExpression = SyntaxFactory.XRefSubSection(_objectNumber, _numberOfEntries, _entries);

        // Act
        var actualNumberOfEntries = xRefSubSectionExpression.NumberOfEntries;

        // Assert
        Assert.Same(_numberOfEntries, actualNumberOfEntries);
    }

    [Fact(DisplayName = $"The {nameof(XRefSubSectionExpressionSyntax.Entries)} property must be assigned from constructor.")]
    public void EntriesProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        var xRefSubSectionExpression = SyntaxFactory.XRefSubSection(_objectNumber, _numberOfEntries, _entries);

        // Act
        var actualEntries = xRefSubSectionExpression.Entries;

        // Assert
        Assert.Same(_entries.Node, actualEntries);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.Count)} property must be equal to 3.")]
    public void SlotCountProperty_MustBeEqualTo3()
    {
        // Arrange
        var xRefSubSectionExpression = SyntaxFactory.XRefSubSection(_objectNumber, _numberOfEntries, _entries);

        // Act
        var actualSlotCount = xRefSubSectionExpression.Count;

        // Assert
        Assert.Equal(3, actualSlotCount);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 0 must return {nameof(XRefSubSectionExpressionSyntax.ObjectNumber)} property.")]
    public void GetSlotMethod_Index0_MustReturnFirstObject()
    {
        // Arrange
        var xRefSubSectionExpression = SyntaxFactory.XRefSubSection(_objectNumber, _numberOfEntries, _entries);

        // Act
        var actualSlot = xRefSubSectionExpression.GetSlot(0);

        // Assert
        Assert.Same(_objectNumber, actualSlot);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 1 must return {nameof(XRefSubSectionExpressionSyntax.NumberOfEntries)} property.")]
    public void GetSlotMethod_Index1_MustReturnSecondObject()
    {
        // Arrange
        var xRefSubSectionExpression = SyntaxFactory.XRefSubSection(_objectNumber, _numberOfEntries, _entries);

        // Act
        var actualSlot = xRefSubSectionExpression.GetSlot(1);

        // Assert
        Assert.Same(_numberOfEntries, actualSlot);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 2 must return {nameof(XRefSubSectionExpressionSyntax.Entries)} property.")]
    public void GetSlotMethod_Index2_MustReturnThirdObject()
    {
        // Arrange
        var xRefSubSectionExpression = SyntaxFactory.XRefSubSection(_objectNumber, _numberOfEntries, _entries);

        // Act
        var actualSlot = xRefSubSectionExpression.GetSlot(2);

        // Assert
        Assert.Same(_entries.Node, actualSlot);
    }

    [Fact(DisplayName = "The GetSlot() method with index 3 must return null.")]
    public void GetSlotMethod_Index3_MustReturnNull()
    {
        // Arrange
        var xRefSubSectionExpression = SyntaxFactory.XRefSubSection(_objectNumber, _numberOfEntries, _entries);

        // Act
        var actualSlot = xRefSubSectionExpression.GetSlot(3);

        // Assert
        Assert.Null(actualSlot);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.Width)} property must consider all slots.")]
    public void WidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        var xRefSubSectionExpression = SyntaxFactory.XRefSubSection(_objectNumber, _numberOfEntries, _entries);

        // Act
        var actualWidth = xRefSubSectionExpression.Width;

        // Assert
        Assert.Equal(49, actualWidth);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.FullWidth)} property must consider all slots.")]
    public void FullWidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        var xRefSubSectionExpression = SyntaxFactory.XRefSubSection(_objectNumber, _numberOfEntries, _entries);

        // Act
        var actualFullWidth = xRefSubSectionExpression.FullWidth;

        // Assert
        Assert.Equal(51, actualFullWidth);
    }

    [Fact(DisplayName = "The ToString() method must not include the trivia.")]
    public void ToStringMethod_MustNotIncludeTrivia()
    {
        // Arrange
        const string ExpectedString = "28  2  0000000017  00007  f  0000000017  00007  f";
        var xRefSubSectionExpression = SyntaxFactory.XRefSubSection(_objectNumber, _numberOfEntries, _entries);

        // Act
        var actualString = xRefSubSectionExpression.ToString();

        // Assert
        Assert.Equal(ExpectedString, actualString);
    }

    [Fact(DisplayName = "The ToFullString() method must include the trivia.")]
    public void ToFullStringMethod_MustIncludeTrivia()
    {
        // Arrange
        const string ExpectedString = " 28  2  0000000017  00007  f  0000000017  00007  f ";
        var xRefSubSectionExpression = SyntaxFactory.XRefSubSection(_objectNumber, _numberOfEntries, _entries);

        // Act
        var actualString = xRefSubSectionExpression.ToFullString();

        // Assert
        Assert.Equal(ExpectedString, actualString);
    }

    [Fact(DisplayName = "The SetDiagnostics() method must set the diagnostics and return a new instance.")]
    public void SetDiagnosticsMethod_MustSetTheDiagnosticsAndReturnNewInstance()
    {
        // Arrange
        var xRefSubSectionExpression = SyntaxFactory.XRefSubSection(_objectNumber, _numberOfEntries, _entries);
        DiagnosticInfo expectedDiagnostic = new(Substitute.For<IMessageProvider>(), DiagnosticCode.ERR_InvalidPDF);
        DiagnosticInfo[] diagnostics = [expectedDiagnostic];

        // Act
        var actualXRefSubSectionExpression = (XRefSubSectionExpressionSyntax)xRefSubSectionExpression.SetDiagnostics(diagnostics);

        // Assert
        Assert.NotSame(xRefSubSectionExpression, actualXRefSubSectionExpression);
        Assert.Equal(diagnostics, actualXRefSubSectionExpression.GetDiagnostics());
        Assert.True(actualXRefSubSectionExpression.ContainsFlags(NodeFlags.ContainsDiagnostics));
    }
}

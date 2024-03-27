// <copyright file="XRefSectionExpressionSyntaxTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxTests;

using OffDotNet.Pdf.CodeAnalysis.Diagnostic;
using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using SyntaxToken = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxToken;
using SyntaxTrivia = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxTrivia;

public class XRefSectionExpressionSyntaxTests
{
    private readonly SyntaxToken _xRefKeyword;
    private readonly SyntaxList<XRefSubSectionExpressionSyntax> _subSections;

    public XRefSectionExpressionSyntaxTests()
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

        var objectNumber = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, firstObjectNumberToken);
        var numberOfEntries = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, numberOfEntriesToken);
        SyntaxList<XRefEntryExpressionSyntax> entries = new SyntaxListBuilder(2)
            .AddRange(new GreenNode[] { xRefEntryExpressionSyntax, xRefEntryExpressionSyntax })
            .ToList<XRefEntryExpressionSyntax>();

        var xRefSectionExpression = SyntaxFactory.XRefSubSection(objectNumber, numberOfEntries, entries);

        _xRefKeyword = SyntaxFactory.Token(SyntaxKind.XRefKeyword, trivia, trivia);
        _subSections = new SyntaxListBuilder(2)
            .AddRange(new GreenNode[] { xRefSectionExpression, xRefSectionExpression })
            .ToList<XRefSubSectionExpressionSyntax>();
    }

    [Fact(DisplayName = $"The {nameof(XRefSectionExpressionSyntax.Kind)} property must be {nameof(SyntaxKind.XRefSection)}")]
    public void KindProperty_MustBeXRefSection()
    {
        // Arrange
        var xRefSectionExpression = SyntaxFactory.XRefSection(_xRefKeyword, _subSections);

        // Act
        var actualKind = xRefSectionExpression.Kind;

        // Assert
        Assert.Equal(SyntaxKind.XRefSection, actualKind);
    }

    [Fact(DisplayName = $"The {nameof(XRefSectionExpressionSyntax.XRefKeyword)} property must be assigned from constructor.")]
    public void XRefKeywordProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        var xRefSectionExpression = SyntaxFactory.XRefSection(_xRefKeyword, _subSections);

        // Act
        var actualXRefKeyword = xRefSectionExpression.XRefKeyword;

        // Assert
        Assert.Equal(_xRefKeyword, actualXRefKeyword);
    }

    [Fact(DisplayName = $"The {nameof(XRefSectionExpressionSyntax.SubSections)} property must be assigned from constructor.")]
    public void SubSectionsProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        var xRefSectionExpression = SyntaxFactory.XRefSection(_xRefKeyword, _subSections);

        // Act
        var actualSubSections = xRefSectionExpression.SubSections;

        // Assert
        Assert.Equal(_subSections.Node, actualSubSections);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.SlotCount)} property must be equal to 2.")]
    public void SlotCountProperty_MustBeEqualTo3()
    {
        // Arrange
        var xRefSectionExpression = SyntaxFactory.XRefSection(_xRefKeyword, _subSections);

        // Act
        var actualSlotCount = xRefSectionExpression.SlotCount;

        // Assert
        Assert.Equal(2, actualSlotCount);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 0 must return {nameof(XRefSectionExpressionSyntax.XRefKeyword)} property.")]
    public void GetSlotMethod_Index0_MustReturnFirstObject()
    {
        // Arrange
        var xRefSectionExpression = SyntaxFactory.XRefSection(_xRefKeyword, _subSections);

        // Act
        var actualSlot = xRefSectionExpression.GetSlot(0);

        // Assert
        Assert.Equal(_xRefKeyword, actualSlot);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 1 must return {nameof(XRefSectionExpressionSyntax.SubSections)} property.")]
    public void GetSlotMethod_Index1_MustReturnSecondObject()
    {
        // Arrange
        var xRefSectionExpression = SyntaxFactory.XRefSection(_xRefKeyword, _subSections);

        // Act
        var actualSlot = xRefSectionExpression.GetSlot(1);

        // Assert
        Assert.Equal(_subSections.Node, actualSlot);
    }

    [Fact(DisplayName = "The GetSlot() method with index 2 must return null.")]
    public void GetSlotMethod_Index3_MustReturnNull()
    {
        // Arrange
        var xRefSectionExpression = SyntaxFactory.XRefSection(_xRefKeyword, _subSections);

        // Act
        var actualSlot = xRefSectionExpression.GetSlot(2);

        // Assert
        Assert.Null(actualSlot);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.Width)} property must consider all slots.")]
    public void WidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        var xRefSectionExpression = SyntaxFactory.XRefSection(_xRefKeyword, _subSections);

        // Act
        var actualWidth = xRefSectionExpression.Width;

        // Assert
        Assert.Equal(106, actualWidth);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.FullWidth)} property must consider all slots.")]
    public void FullWidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        var xRefSectionExpression = SyntaxFactory.XRefSection(_xRefKeyword, _subSections);

        // Act
        var actualFullWidth = xRefSectionExpression.FullWidth;

        // Assert
        Assert.Equal(108, actualFullWidth);
    }

    [Fact(DisplayName = "The ToString() method must not include the trivia.")]
    public void ToStringMethod_MustNotIncludeTrivia()
    {
        // Arrange
        const string ExpectedString = "xref  28  2  0000000017  00007  f  0000000017  00007  f  28  2  0000000017  00007  f  0000000017  00007  f";
        var xRefSectionExpression = SyntaxFactory.XRefSection(_xRefKeyword, _subSections);

        // Act
        var actualString = xRefSectionExpression.ToString();

        // Assert
        Assert.Equal(ExpectedString, actualString);
    }

    [Fact(DisplayName = "The ToFullString() method must include the trivia.")]
    public void ToFullStringMethod_MustIncludeTrivia()
    {
        // Arrange
        const string ExpectedString = " xref  28  2  0000000017  00007  f  0000000017  00007  f  28  2  0000000017  00007  f  0000000017  00007  f ";
        var xRefSectionExpression = SyntaxFactory.XRefSection(_xRefKeyword, _subSections);

        // Act
        var actualString = xRefSectionExpression.ToFullString();

        // Assert
        Assert.Equal(ExpectedString, actualString);
    }

    [Fact(DisplayName = "The SetDiagnostics() method must set the diagnostics and return a new instance.")]
    public void SetDiagnosticsMethod_MustSetTheDiagnosticsAndReturnNewInstance()
    {
        // Arrange
        var xRefSectionExpression = SyntaxFactory.XRefSection(_xRefKeyword, _subSections);
        DiagnosticInfo expectedDiagnostic = new(Substitute.For<IMessageProvider>(), DiagnosticCode.ERR_InvalidPDF);
        DiagnosticInfo[] diagnostics = [expectedDiagnostic];

        // Act
        var actualXRefSectionExpression = (XRefSectionExpressionSyntax)xRefSectionExpression.SetDiagnostics(diagnostics);

        // Assert
        Assert.NotSame(xRefSectionExpression, actualXRefSectionExpression);
        Assert.Equal(diagnostics, actualXRefSectionExpression.GetDiagnostics());
        Assert.True(actualXRefSectionExpression.ContainsFlags(GreenNode.NodeFlags.ContainsDiagnostics));
    }
}

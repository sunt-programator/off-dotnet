// <copyright file="XRefEntryExpressionSyntaxTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxNodes;

using OffDotNet.Pdf.CodeAnalysis.Diagnostic;
using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntaxNodes;
using SyntaxFactory = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxFactory;
using SyntaxToken = CodeAnalysis.Syntax.InternalSyntax.SyntaxToken;
using SyntaxTrivia = CodeAnalysis.Syntax.InternalSyntax.SyntaxTrivia;

public class XRefEntryExpressionSyntaxTests
{
    private readonly LiteralExpressionSyntax _offset;
    private readonly LiteralExpressionSyntax _generationNumber;
    private readonly SyntaxToken _entryTypeKeyword;

    public XRefEntryExpressionSyntaxTests()
    {
        GreenNode trivia = SyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, " ");
        var offsetToken = SyntaxFactory.Token(SyntaxKind.NumericLiteralToken, "0000000017", 17, trivia, trivia);
        var generationNumberToken = SyntaxFactory.Token(SyntaxKind.NumericLiteralToken, "00007", 7, trivia, trivia);

        _offset = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, offsetToken);
        _generationNumber = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, generationNumberToken);
        _entryTypeKeyword = SyntaxFactory.Token(SyntaxKind.FreeXRefEntryKeyword, trivia, trivia);
    }

    [Fact(DisplayName = $"The {nameof(XRefEntryExpressionSyntax.Kind)} property must be {nameof(SyntaxKind.XRefEntry)}")]
    public void KindProperty_MustBeXRefEntry()
    {
        // Arrange
        var xRefEntryExpression = SyntaxFactory.XRefEntry(_offset, _generationNumber, _entryTypeKeyword);

        // Act
        var actualKind = xRefEntryExpression.Kind;

        // Assert
        Assert.Equal(SyntaxKind.XRefEntry, actualKind);
    }

    [Fact(DisplayName = $"The {nameof(XRefEntryExpressionSyntax.Offset)} property must be assigned from constructor.")]
    public void OffsetProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        var xRefEntryExpression = SyntaxFactory.XRefEntry(_offset, _generationNumber, _entryTypeKeyword);

        // Act
        var actualOffset = xRefEntryExpression.Offset;

        // Assert
        Assert.Equal(_offset, actualOffset);
    }

    [Fact(DisplayName = $"The {nameof(XRefEntryExpressionSyntax.GenerationNumber)} property must be assigned from constructor.")]
    public void GenerationNumberProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        var xRefEntryExpression = SyntaxFactory.XRefEntry(_offset, _generationNumber, _entryTypeKeyword);

        // Act
        var actualGenerationNumber = xRefEntryExpression.GenerationNumber;

        // Assert
        Assert.Equal(_generationNumber, actualGenerationNumber);
    }

    [Fact(DisplayName = $"The {nameof(XRefEntryExpressionSyntax.EntryType)} property must be assigned from constructor.")]
    public void ReferenceKeywordProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        var xRefEntryExpression = SyntaxFactory.XRefEntry(_offset, _generationNumber, _entryTypeKeyword);

        // Act
        var actualEntryType = xRefEntryExpression.EntryType;

        // Assert
        Assert.Equal(_entryTypeKeyword, actualEntryType);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.SlotCount)} property must be equal to 3.")]
    public void SlotCountProperty_MustBeEqualTo3()
    {
        // Arrange
        var xRefEntryExpression = SyntaxFactory.XRefEntry(_offset, _generationNumber, _entryTypeKeyword);

        // Act
        var actualSlotCount = xRefEntryExpression.SlotCount;

        // Assert
        Assert.Equal(3, actualSlotCount);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 0 must return {nameof(XRefEntryExpressionSyntax.Offset)} property.")]
    public void GetSlotMethod_Index0_MustReturnFirstObject()
    {
        // Arrange
        var xRefEntryExpression = SyntaxFactory.XRefEntry(_offset, _generationNumber, _entryTypeKeyword);

        // Act
        var actualSlot = xRefEntryExpression.GetSlot(0);

        // Assert
        Assert.Equal(_offset, actualSlot);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 1 must return {nameof(XRefEntryExpressionSyntax.GenerationNumber)} property.")]
    public void GetSlotMethod_Index1_MustReturnSecondObject()
    {
        // Arrange
        var xRefEntryExpression = SyntaxFactory.XRefEntry(_offset, _generationNumber, _entryTypeKeyword);

        // Act
        var actualSlot = xRefEntryExpression.GetSlot(1);

        // Assert
        Assert.Equal(_generationNumber, actualSlot);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 2 must return {nameof(XRefEntryExpressionSyntax.EntryType)} property.")]
    public void GetSlotMethod_Index2_MustReturnThirdObject()
    {
        // Arrange
        var xRefEntryExpression = SyntaxFactory.XRefEntry(_offset, _generationNumber, _entryTypeKeyword);

        // Act
        var actualSlot = xRefEntryExpression.GetSlot(2);

        // Assert
        Assert.Equal(_entryTypeKeyword, actualSlot);
    }

    [Fact(DisplayName = "The GetSlot() method with index 3 must return null.")]
    public void GetSlotMethod_Index3_MustReturnNull()
    {
        // Arrange
        var xRefEntryExpression = SyntaxFactory.XRefEntry(_offset, _generationNumber, _entryTypeKeyword);

        // Act
        var actualSlot = xRefEntryExpression.GetSlot(3);

        // Assert
        Assert.Null(actualSlot);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.Width)} property must consider all slots.")]
    public void WidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        var xRefEntryExpression = SyntaxFactory.XRefEntry(_offset, _generationNumber, _entryTypeKeyword);

        // Act
        var actualWidth = xRefEntryExpression.Width;

        // Assert
        Assert.Equal(20, actualWidth);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.FullWidth)} property must consider all slots.")]
    public void FullWidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        var xRefEntryExpression = SyntaxFactory.XRefEntry(_offset, _generationNumber, _entryTypeKeyword);

        // Act
        var actualFullWidth = xRefEntryExpression.FullWidth;

        // Assert
        Assert.Equal(22, actualFullWidth);
    }

    [Fact(DisplayName = "The ToString() method must not include the trivia.")]
    public void ToStringMethod_MustNotIncludeTrivia()
    {
        // Arrange
        const string ExpectedString = "0000000017  00007  f";
        var xRefEntryExpression = SyntaxFactory.XRefEntry(_offset, _generationNumber, _entryTypeKeyword);

        // Act
        var actualString = xRefEntryExpression.ToString();

        // Assert
        Assert.Equal(ExpectedString, actualString);
    }

    [Fact(DisplayName = "The ToFullString() method must include the trivia.")]
    public void ToFullStringMethod_MustIncludeTrivia()
    {
        // Arrange
        const string ExpectedString = " 0000000017  00007  f ";
        var xRefEntryExpression = SyntaxFactory.XRefEntry(_offset, _generationNumber, _entryTypeKeyword);

        // Act
        var actualString = xRefEntryExpression.ToFullString();

        // Assert
        Assert.Equal(ExpectedString, actualString);
    }

    [Fact(DisplayName = "The SetDiagnostics() method must set the diagnostics and return a new instance.")]
    public void SetDiagnosticsMethod_MustSetDiagnosticsAndReturnNewInstance()
    {
        // Arrange
        var xRefEntryExpression = SyntaxFactory.XRefEntry(_offset, _generationNumber, _entryTypeKeyword);
        DiagnosticInfo expectedDiagnostic = new(Substitute.For<IMessageProvider>(), DiagnosticCode.ERR_InvalidPDF);
        DiagnosticInfo[] diagnostics = [expectedDiagnostic];

        // Act
        var actualXRefEntryExpression = (XRefEntryExpressionSyntax)xRefEntryExpression.SetDiagnostics(diagnostics);

        // Assert
        Assert.NotSame(xRefEntryExpression, actualXRefEntryExpression);
        Assert.Equal(diagnostics, actualXRefEntryExpression.GetDiagnostics());
        Assert.True(actualXRefEntryExpression.ContainsFlags(NodeFlags.ContainsDiagnostics));
    }
}

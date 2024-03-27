// <copyright file="IndirectObjectSyntaxTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxTests;

using OffDotNet.Pdf.CodeAnalysis.Diagnostic;
using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using SyntaxToken = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxToken;
using SyntaxTrivia = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxTrivia;

public class IndirectObjectSyntaxTests
{
    private readonly LiteralExpressionSyntax _objectNumber;
    private readonly LiteralExpressionSyntax _generationNumber;
    private readonly SyntaxToken _startObjectKeyword;
    private readonly GreenNode _content;
    private readonly SyntaxToken _endObjKeyword;

    public IndirectObjectSyntaxTests()
    {
        GreenNode trivia = SyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, " ");
        var objectNumberToken = SyntaxFactory.Token(SyntaxKind.NumericLiteralToken, "123", 123, trivia, trivia);
        var generationNumberToken = SyntaxFactory.Token(SyntaxKind.NumericLiteralToken, "32", 32, trivia, trivia);

        _objectNumber = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, objectNumberToken);
        _generationNumber = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, generationNumberToken);
        _startObjectKeyword = SyntaxFactory.Token(SyntaxKind.StartObjectKeyword, trivia, trivia);
        _content = SyntaxFactory.Token(SyntaxKind.TrueKeyword, trivia, trivia);
        _endObjKeyword = SyntaxFactory.Token(SyntaxKind.EndObjectKeyword, trivia, trivia);
    }

    [Fact(DisplayName = $"The {nameof(IndirectObjectSyntax.Kind)} property must be {nameof(SyntaxKind.IndirectObject)}")]
    public void KindProperty_MustBeIndirectObject()
    {
        // Arrange
        var indirectObject = SyntaxFactory.Object(_objectNumber, _generationNumber, _startObjectKeyword, _content, _endObjKeyword);

        // Act
        var actualKind = indirectObject.Kind;

        // Assert
        Assert.Equal(SyntaxKind.IndirectObject, actualKind);
    }

    [Fact(DisplayName = $"The {nameof(IndirectObjectSyntax.ObjectNumber)} property must be assigned from constructor.")]
    public void ObjectNumberProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        var indirectObject = SyntaxFactory.Object(_objectNumber, _generationNumber, _startObjectKeyword, _content, _endObjKeyword);

        // Act
        var actualObjectNumber = indirectObject.ObjectNumber;

        // Assert
        Assert.Equal(_objectNumber, actualObjectNumber);
    }

    [Fact(DisplayName = $"The {nameof(IndirectObjectSyntax.GenerationNumber)} property must be assigned from constructor.")]
    public void GenerationNumberProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        var indirectObject = SyntaxFactory.Object(_objectNumber, _generationNumber, _startObjectKeyword, _content, _endObjKeyword);

        // Act
        var actualGenerationNumber = indirectObject.GenerationNumber;

        // Assert
        Assert.Equal(_generationNumber, actualGenerationNumber);
    }

    [Fact(DisplayName = $"The {nameof(IndirectObjectSyntax.StartObjectKeyword)} property must be assigned from constructor.")]
    public void StartObjectKeywordProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        var indirectObject = SyntaxFactory.Object(_objectNumber, _generationNumber, _startObjectKeyword, _content, _endObjKeyword);

        // Act
        var actualStartObjectKeyword = indirectObject.StartObjectKeyword;

        // Assert
        Assert.Equal(_startObjectKeyword, actualStartObjectKeyword);
    }

    [Fact(DisplayName = $"The {nameof(IndirectObjectSyntax.Content)} property must be assigned from constructor.")]
    public void ContentProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        var indirectObject = SyntaxFactory.Object(_objectNumber, _generationNumber, _startObjectKeyword, _content, _endObjKeyword);

        // Act
        var actualContent = indirectObject.Content;

        // Assert
        Assert.Equal(_content, actualContent);
    }

    [Fact(DisplayName = $"The {nameof(IndirectObjectSyntax.EndObjectKeyword)} property must be assigned from constructor.")]
    public void EndObjectKeywordProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        var indirectObject = SyntaxFactory.Object(_objectNumber, _generationNumber, _startObjectKeyword, _content, _endObjKeyword);

        // Act
        var actualEndObjectKeyword = indirectObject.EndObjectKeyword;

        // Assert
        Assert.Equal(_endObjKeyword, actualEndObjectKeyword);
    }

    [Fact(DisplayName = $"The {nameof(IndirectObjectSyntax.SlotCount)} property must be equal to 5.")]
    public void SlotCountProperty_MustBeEqualTo5()
    {
        // Arrange
        var indirectObject = SyntaxFactory.Object(_objectNumber, _generationNumber, _startObjectKeyword, _content, _endObjKeyword);

        // Act
        var actualSlotCount = indirectObject.SlotCount;

        // Assert
        Assert.Equal(5, actualSlotCount);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 0 must return {nameof(IndirectObjectSyntax.ObjectNumber)} property.")]
    public void GetSlotMethod_Index0_MustReturnObjectNumber()
    {
        // Arrange
        var indirectObject = SyntaxFactory.Object(_objectNumber, _generationNumber, _startObjectKeyword, _content, _endObjKeyword);

        // Act
        var actualSlot = indirectObject.GetSlot(0);

        // Assert
        Assert.Equal(_objectNumber, actualSlot);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 1 must return {nameof(IndirectObjectSyntax.GenerationNumber)} property.")]
    public void GetSlotMethod_Index1_MustReturnGenerationNumber()
    {
        // Arrange
        var indirectObject = SyntaxFactory.Object(_objectNumber, _generationNumber, _startObjectKeyword, _content, _endObjKeyword);

        // Act
        var actualSlot = indirectObject.GetSlot(1);

        // Assert
        Assert.Equal(_generationNumber, actualSlot);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 2 must return {nameof(IndirectObjectSyntax.StartObjectKeyword)} property.")]
    public void GetSlotMethod_Index2_MustReturnStartObjectKeyword()
    {
        // Arrange
        var indirectObject = SyntaxFactory.Object(_objectNumber, _generationNumber, _startObjectKeyword, _content, _endObjKeyword);

        // Act
        var actualSlot = indirectObject.GetSlot(2);

        // Assert
        Assert.Equal(_startObjectKeyword, actualSlot);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 3 must return {nameof(IndirectObjectSyntax.Content)} property.")]
    public void GetSlotMethod_Index3_MustReturnContent()
    {
        // Arrange
        var indirectObject = SyntaxFactory.Object(_objectNumber, _generationNumber, _startObjectKeyword, _content, _endObjKeyword);

        // Act
        var actualSlot = indirectObject.GetSlot(3);

        // Assert
        Assert.Equal(_content, actualSlot);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 4 must return {nameof(IndirectObjectSyntax.EndObjectKeyword)} property.")]
    public void GetSlotMethod_Index4_MustReturnEndObjectKeyword()
    {
        // Arrange
        var indirectObject = SyntaxFactory.Object(_objectNumber, _generationNumber, _startObjectKeyword, _content, _endObjKeyword);

        // Act
        var actualSlot = indirectObject.GetSlot(4);

        // Assert
        Assert.Equal(_endObjKeyword, actualSlot);
    }

    [Fact(DisplayName = "The GetSlot() method with index 5 must return null.")]
    public void GetSlotMethod_Index5_MustReturnNull()
    {
        // Arrange
        var indirectObject = SyntaxFactory.Object(_objectNumber, _generationNumber, _startObjectKeyword, _content, _endObjKeyword);

        // Act
        var actualSlot = indirectObject.GetSlot(5);

        // Assert
        Assert.Null(actualSlot);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.Width)} property must consider all slots.")]
    public void WidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        var indirectObject = SyntaxFactory.Object(_objectNumber, _generationNumber, _startObjectKeyword, _content, _endObjKeyword);

        // Act
        var actualWidth = indirectObject.Width;

        // Assert
        Assert.Equal(26, actualWidth);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.FullWidth)} property must consider all slots.")]
    public void FullWidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        var indirectObject = SyntaxFactory.Object(_objectNumber, _generationNumber, _startObjectKeyword, _content, _endObjKeyword);

        // Act
        var actualFullWidth = indirectObject.FullWidth;

        // Assert
        Assert.Equal(28, actualFullWidth);
    }

    [Fact(DisplayName = "The ToString() method must not include the trivia.")]
    public void ToStringMethod_MustNotIncludeTrivia()
    {
        // Arrange
        const string ExpectedString = "123  32  obj  true  endobj";
        var indirectObject = SyntaxFactory.Object(_objectNumber, _generationNumber, _startObjectKeyword, _content, _endObjKeyword);

        // Act
        var actualString = indirectObject.ToString();

        // Assert
        Assert.Equal(ExpectedString, actualString);
    }

    [Fact(DisplayName = "The ToFullString() method must include the trivia.")]
    public void ToFullStringMethod_MustIncludeTrivia()
    {
        // Arrange
        const string ExpectedString = " 123  32  obj  true  endobj ";
        var indirectObject = SyntaxFactory.Object(_objectNumber, _generationNumber, _startObjectKeyword, _content, _endObjKeyword);

        // Act
        var actualString = indirectObject.ToFullString();

        // Assert
        Assert.Equal(ExpectedString, actualString);
    }

    [Fact(DisplayName = "The SetDiagnostics() method must set the diagnostics and return a new instance.")]
    public void SetDiagnosticsMethod_MustSetTheDiagnosticsAndReturnNewInstance()
    {
        // Arrange
        var indirectObject = SyntaxFactory.Object(_objectNumber, _generationNumber, _startObjectKeyword, _content, _endObjKeyword);
        DiagnosticInfo expectedDiagnostic = new(Substitute.For<IMessageProvider>(), DiagnosticCode.ERR_InvalidPDF);
        DiagnosticInfo[] diagnostics = [expectedDiagnostic];

        // Act
        var actualIndirectObject = (IndirectObjectSyntax)indirectObject.SetDiagnostics(diagnostics);

        // Assert
        Assert.NotSame(indirectObject, actualIndirectObject);
        Assert.Equal(diagnostics, actualIndirectObject.GetDiagnostics());
        Assert.True(actualIndirectObject.ContainsFlags(GreenNode.NodeFlags.ContainsDiagnostics));
    }
}

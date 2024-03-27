// <copyright file="ArrayElementSyntaxTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxTests;

using OffDotNet.Pdf.CodeAnalysis.Diagnostic;
using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using LiteralExpressionSyntax = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.LiteralExpressionSyntax;

public class ArrayElementSyntaxTests
{
    private readonly ExpressionSyntax _expression;

    public ArrayElementSyntaxTests()
    {
        GreenNode trivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " ");
        var objectNumberToken = SyntaxFactory.Token(SyntaxKind.NumericLiteralToken, "123", 123, trivia, trivia);
        _expression = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, objectNumberToken);
    }

    [Fact(DisplayName = $"The {nameof(ArrayElementSyntax.Kind)} property must be {nameof(SyntaxKind.ArrayElement)}")]
    public void KindProperty_MustBeArrayElement()
    {
        // Arrange
        var arrayElement = SyntaxFactory.ArrayElement(_expression);

        // Act
        var actualKind = arrayElement.Kind;

        // Assert
        Assert.Equal(SyntaxKind.ArrayElement, actualKind);
    }

    [Fact(DisplayName = $"The {nameof(ArrayElementSyntax.Expression)} property must be assigned from constructor.")]
    public void ExpressionProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        var arrayElement = SyntaxFactory.ArrayElement(_expression);

        // Act
        var actualReferenceKeyword = arrayElement.Expression;

        // Assert
        Assert.Equal(_expression, actualReferenceKeyword);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.SlotCount)} property must be equal to 1.")]
    public void SlotCountProperty_MustBeEqualTo3()
    {
        // Arrange
        var arrayElement = SyntaxFactory.ArrayElement(_expression);

        // Act
        var actualSlotCount = arrayElement.SlotCount;

        // Assert
        Assert.Equal(1, actualSlotCount);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 0 must return {nameof(ArrayElementSyntax.Expression)} property.")]
    public void GetSlotMethod_Index0_MustReturnFirstObject()
    {
        // Arrange
        var arrayElement = SyntaxFactory.ArrayElement(_expression);

        // Act
        var actualSlot = arrayElement.GetSlot(0);

        // Assert
        Assert.Equal(_expression, actualSlot);
    }

    [Fact(DisplayName = "The GetSlot() method with index 1 must return null.")]
    public void GetSlotMethod_Index1_MustReturnNull()
    {
        // Arrange
        var arrayElement = SyntaxFactory.ArrayElement(_expression);

        // Act
        var actualSlot = arrayElement.GetSlot(1);

        // Assert
        Assert.Null(actualSlot);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.Width)} property must consider all slots.")]
    public void WidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        var arrayElement = SyntaxFactory.ArrayElement(_expression);

        // Act
        var actualWidth = arrayElement.Width;

        // Assert
        Assert.Equal(3, actualWidth);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.FullWidth)} property must consider all slots.")]
    public void FullWidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        var arrayElement = SyntaxFactory.ArrayElement(_expression);

        // Act
        var actualFullWidth = arrayElement.FullWidth;

        // Assert
        Assert.Equal(5, actualFullWidth);
    }

    [Fact(DisplayName = "The ToString() method must not include the trivia.")]
    public void ToStringMethod_MustNotIncludeTrivia()
    {
        // Arrange
        const string ExpectedString = "123";
        var arrayElement = SyntaxFactory.ArrayElement(_expression);

        // Act
        var actualString = arrayElement.ToString();

        // Assert
        Assert.Equal(ExpectedString, actualString);
    }

    [Fact(DisplayName = "The ToFullString() method must include the trivia.")]
    public void ToFullStringMethod_MustIncludeTrivia()
    {
        // Arrange
        const string ExpectedString = " 123 ";
        var arrayElement = SyntaxFactory.ArrayElement(_expression);

        // Act
        var actualString = arrayElement.ToFullString();

        // Assert
        Assert.Equal(ExpectedString, actualString);
    }

    [Fact(DisplayName = "The SetDiagnostics() method must set the diagnostics and return a new instance.")]
    public void SetDiagnosticsMethod_MustSetDiagnosticsAndReturnNewInstance()
    {
        // Arrange
        var arrayElement = SyntaxFactory.ArrayElement(_expression);
        DiagnosticInfo expectedDiagnostic = new(Substitute.For<IMessageProvider>(), DiagnosticCode.ERR_InvalidPDF);
        DiagnosticInfo[] diagnostics = [expectedDiagnostic];

        // Act
        var actualNode = arrayElement.SetDiagnostics(diagnostics);

        // Assert
        Assert.NotSame(arrayElement, actualNode);
        Assert.NotEqual(arrayElement, actualNode);
        Assert.Equal(diagnostics, actualNode.GetDiagnostics());
    }
}

// <copyright file="DictionaryElementSyntaxTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Diagnostic;
using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using LiteralExpressionSyntax = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.LiteralExpressionSyntax;
using SyntaxToken = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxToken;

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxTests;

public class DictionaryElementSyntaxTests
{
    private readonly LiteralExpressionSyntax key;
    private readonly ExpressionSyntax value;

    public DictionaryElementSyntaxTests()
    {
        GreenNode trivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " ");
        SyntaxToken objectNumberToken = SyntaxFactory.Token(SyntaxKind.NumericLiteralToken, "123", 123, trivia, trivia);
        this.key = SyntaxFactory.LiteralExpression(SyntaxKind.NameLiteralExpression, SyntaxFactory.Token(SyntaxKind.NameLiteralToken, "/Name1", trivia, trivia));
        this.value = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, objectNumberToken);
    }

    [Fact(DisplayName = $"The {nameof(DictionaryElementSyntax.Kind)} property must be {nameof(SyntaxKind.DictionaryElement)}")]
    public void KindProperty_MustBeDictionaryElement()
    {
        // Arrange
        DictionaryElementSyntax dictionaryElement = SyntaxFactory.DictionaryElement(this.key, this.value);

        // Act
        SyntaxKind actualKind = dictionaryElement.Kind;

        // Assert
        Assert.Equal(SyntaxKind.DictionaryElement, actualKind);
    }

    [Fact(DisplayName = $"The {nameof(DictionaryElementSyntax.Key)} property must be assigned from constructor.")]
    public void KeyProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        DictionaryElementSyntax dictionaryElement = SyntaxFactory.DictionaryElement(this.key, this.value);

        // Act
        LiteralExpressionSyntax actualKey = dictionaryElement.Key;

        // Assert
        Assert.Equal(this.key, actualKey);
    }

    [Fact(DisplayName = $"The {nameof(DictionaryElementSyntax.Value)} property must be assigned from constructor.")]
    public void ValueProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        DictionaryElementSyntax dictionaryElement = SyntaxFactory.DictionaryElement(this.key, this.value);

        // Act
        ExpressionSyntax actualValue = dictionaryElement.Value;

        // Assert
        Assert.Equal(this.value, actualValue);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.SlotCount)} property must be equal to 2.")]
    public void SlotCountProperty_MustBeEqualTo2()
    {
        // Arrange
        DictionaryElementSyntax dictionaryElement = SyntaxFactory.DictionaryElement(this.key, this.value);

        // Act
        int actualSlotCount = dictionaryElement.SlotCount;

        // Assert
        Assert.Equal(2, actualSlotCount);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 0 must return {nameof(DictionaryElementSyntax.Key)} property.")]
    public void GetSlotMethod_Index0_MustReturnFirstObject()
    {
        // Arrange
        DictionaryElementSyntax dictionaryElement = SyntaxFactory.DictionaryElement(this.key, this.value);

        // Act
        GreenNode? actualSlot = dictionaryElement.GetSlot(0);

        // Assert
        Assert.Equal(this.key, actualSlot);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 1 must return {nameof(DictionaryElementSyntax.Value)} property.")]
    public void GetSlotMethod_Index0_MustReturnSecondObject()
    {
        // Arrange
        DictionaryElementSyntax dictionaryElement = SyntaxFactory.DictionaryElement(this.key, this.value);

        // Act
        GreenNode? actualSlot = dictionaryElement.GetSlot(1);

        // Assert
        Assert.Equal(this.value, actualSlot);
    }

    [Fact(DisplayName = "The GetSlot() method with index 2 must return null.")]
    public void GetSlotMethod_Index1_MustReturnNull()
    {
        // Arrange
        DictionaryElementSyntax dictionaryElement = SyntaxFactory.DictionaryElement(this.key, this.value);

        // Act
        GreenNode? actualSlot = dictionaryElement.GetSlot(2);

        // Assert
        Assert.Null(actualSlot);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.Width)} property must consider all slots.")]
    public void WidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        DictionaryElementSyntax dictionaryElement = SyntaxFactory.DictionaryElement(this.key, this.value);

        // Act
        int actualWidth = dictionaryElement.Width;

        // Assert
        Assert.Equal(11, actualWidth);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.FullWidth)} property must consider all slots.")]
    public void FullWidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        DictionaryElementSyntax dictionaryElement = SyntaxFactory.DictionaryElement(this.key, this.value);

        // Act
        int actualFullWidth = dictionaryElement.FullWidth;

        // Assert
        Assert.Equal(13, actualFullWidth);
    }

    [Fact(DisplayName = "The ToString() method must not include the trivia.")]
    public void ToStringMethod_MustNotIncludeTrivia()
    {
        // Arrange
        const string expectedString = "/Name1  123";
        DictionaryElementSyntax dictionaryElement = SyntaxFactory.DictionaryElement(this.key, this.value);

        // Act
        string actualString = dictionaryElement.ToString();

        // Assert
        Assert.Equal(expectedString, actualString);
    }

    [Fact(DisplayName = "The ToFullString() method must include the trivia.")]
    public void ToFullStringMethod_MustIncludeTrivia()
    {
        // Arrange
        const string expectedString = " /Name1  123 ";
        DictionaryElementSyntax dictionaryElement = SyntaxFactory.DictionaryElement(this.key, this.value);

        // Act
        string actualString = dictionaryElement.ToFullString();

        // Assert
        Assert.Equal(expectedString, actualString);
    }

    [Fact(DisplayName = "The SetDiagnostics() method must set the diagnostics and return a new instance.")]
    public void SetDiagnosticsMethod_MustSetDiagnosticsAndReturnNewInstance()
    {
        // Arrange
        DictionaryElementSyntax dictionaryElement = SyntaxFactory.DictionaryElement(this.key, this.value);
        DiagnosticInfo expectedDiagnostic = new(Substitute.For<IMessageProvider>(), DiagnosticCode.ERR_InvalidPDF);
        DiagnosticInfo[] diagnostics = { expectedDiagnostic };

        // Act
        DictionaryElementSyntax actualDictionaryElement = (DictionaryElementSyntax)dictionaryElement.SetDiagnostics(diagnostics);

        // Assert
        Assert.NotSame(dictionaryElement, actualDictionaryElement);
        Assert.Equal(diagnostics, actualDictionaryElement.GetDiagnostics());
        Assert.True(actualDictionaryElement.ContainsFlags(GreenNode.NodeFlags.ContainsDiagnostics));
    }
}

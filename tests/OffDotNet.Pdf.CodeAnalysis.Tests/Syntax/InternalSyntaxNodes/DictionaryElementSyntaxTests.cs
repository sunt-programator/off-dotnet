// <copyright file="DictionaryElementSyntaxTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxNodes;

using OffDotNet.Pdf.CodeAnalysis.Diagnostic;
using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntaxNodes;
using SyntaxFactory = CodeAnalysis.Syntax.InternalSyntax.SyntaxFactory;

public class DictionaryElementSyntaxTests
{
    private readonly LiteralExpressionSyntax _key;
    private readonly ExpressionSyntax _value;

    public DictionaryElementSyntaxTests()
    {
        GreenNode trivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " ");
        var objectNumberToken = SyntaxFactory.Token(SyntaxKind.NumericLiteralToken, "123", 123, trivia, trivia);
        _key = SyntaxFactory.LiteralExpression(SyntaxKind.NameLiteralExpression, SyntaxFactory.Token(SyntaxKind.NameLiteralToken, "/Name1", trivia, trivia));
        _value = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, objectNumberToken);
    }

    [Fact(DisplayName = $"The {nameof(DictionaryElementSyntax.Kind)} property must be {nameof(SyntaxKind.DictionaryElement)}")]
    public void KindProperty_MustBeDictionaryElement()
    {
        // Arrange
        var dictionaryElement = SyntaxFactory.DictionaryElement(_key, _value);

        // Act
        var actualKind = dictionaryElement.Kind;

        // Assert
        Assert.Equal(SyntaxKind.DictionaryElement, actualKind);
    }

    [Fact(DisplayName = $"The {nameof(DictionaryElementSyntax.Key)} property must be assigned from constructor.")]
    public void KeyProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        var dictionaryElement = SyntaxFactory.DictionaryElement(_key, _value);

        // Act
        var actualKey = dictionaryElement.Key;

        // Assert
        Assert.Same(_key, actualKey);
    }

    [Fact(DisplayName = $"The {nameof(DictionaryElementSyntax.Value)} property must be assigned from constructor.")]
    public void ValueProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        var dictionaryElement = SyntaxFactory.DictionaryElement(_key, _value);

        // Act
        var actualValue = dictionaryElement.Value;

        // Assert
        Assert.Same(_value, actualValue);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.Count)} property must be equal to 2.")]
    public void SlotCountProperty_MustBeEqualTo2()
    {
        // Arrange
        var dictionaryElement = SyntaxFactory.DictionaryElement(_key, _value);

        // Act
        var actualSlotCount = dictionaryElement.Count;

        // Assert
        Assert.Equal(2, actualSlotCount);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 0 must return {nameof(DictionaryElementSyntax.Key)} property.")]
    public void GetSlotMethod_Index0_MustReturnFirstObject()
    {
        // Arrange
        var dictionaryElement = SyntaxFactory.DictionaryElement(_key, _value);

        // Act
        var actualSlot = dictionaryElement.GetSlot(0);

        // Assert
        Assert.Same(_key, actualSlot);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 1 must return {nameof(DictionaryElementSyntax.Value)} property.")]
    public void GetSlotMethod_Index0_MustReturnSecondObject()
    {
        // Arrange
        var dictionaryElement = SyntaxFactory.DictionaryElement(_key, _value);

        // Act
        var actualSlot = dictionaryElement.GetSlot(1);

        // Assert
        Assert.Same(_value, actualSlot);
    }

    [Fact(DisplayName = "The GetSlot() method with index 2 must return null.")]
    public void GetSlotMethod_Index1_MustReturnNull()
    {
        // Arrange
        var dictionaryElement = SyntaxFactory.DictionaryElement(_key, _value);

        // Act
        var actualSlot = dictionaryElement.GetSlot(2);

        // Assert
        Assert.Null(actualSlot);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.Width)} property must consider all slots.")]
    public void WidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        var dictionaryElement = SyntaxFactory.DictionaryElement(_key, _value);

        // Act
        var actualWidth = dictionaryElement.Width;

        // Assert
        Assert.Equal(11, actualWidth);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.FullWidth)} property must consider all slots.")]
    public void FullWidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        var dictionaryElement = SyntaxFactory.DictionaryElement(_key, _value);

        // Act
        var actualFullWidth = dictionaryElement.FullWidth;

        // Assert
        Assert.Equal(13, actualFullWidth);
    }

    [Fact(DisplayName = "The ToString() method must not include the trivia.")]
    public void ToStringMethod_MustNotIncludeTrivia()
    {
        // Arrange
        const string ExpectedString = "/Name1  123";
        var dictionaryElement = SyntaxFactory.DictionaryElement(_key, _value);

        // Act
        var actualString = dictionaryElement.ToString();

        // Assert
        Assert.Equal(ExpectedString, actualString);
    }

    [Fact(DisplayName = "The ToFullString() method must include the trivia.")]
    public void ToFullStringMethod_MustIncludeTrivia()
    {
        // Arrange
        const string ExpectedString = " /Name1  123 ";
        var dictionaryElement = SyntaxFactory.DictionaryElement(_key, _value);

        // Act
        var actualString = dictionaryElement.ToFullString();

        // Assert
        Assert.Equal(ExpectedString, actualString);
    }

    [Fact(DisplayName = "The SetDiagnostics() method must set the diagnostics and return a new instance.")]
    public void SetDiagnosticsMethod_MustSetDiagnosticsAndReturnNewInstance()
    {
        // Arrange
        var dictionaryElement = SyntaxFactory.DictionaryElement(_key, _value);
        DiagnosticInfo expectedDiagnostic = new(Substitute.For<IMessageProvider>(), DiagnosticCode.ERR_InvalidPDF);
        DiagnosticInfo[] diagnostics = [expectedDiagnostic];

        // Act
        var actualDictionaryElement = (DictionaryElementSyntax)dictionaryElement.SetDiagnostics(diagnostics);

        // Assert
        Assert.NotSame(dictionaryElement, actualDictionaryElement);
        Assert.Equal(diagnostics, actualDictionaryElement.GetDiagnostics());
        Assert.True(actualDictionaryElement.ContainsFlags(NodeFlags.ContainsDiagnostics));
    }
}

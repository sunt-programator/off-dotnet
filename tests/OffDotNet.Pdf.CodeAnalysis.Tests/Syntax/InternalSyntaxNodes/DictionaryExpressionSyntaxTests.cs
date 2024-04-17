// <copyright file="DictionaryExpressionSyntaxTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxNodes;

using OffDotNet.Pdf.CodeAnalysis.Diagnostic;
using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntaxNodes;
using SyntaxFactory = CodeAnalysis.Syntax.InternalSyntax.SyntaxFactory;
using SyntaxToken = CodeAnalysis.Syntax.InternalSyntax.SyntaxToken;
using SyntaxTrivia = CodeAnalysis.Syntax.InternalSyntax.SyntaxTrivia;

public class DictionaryExpressionSyntaxTests
{
    private readonly SyntaxToken _openBracketToken;
    private readonly SyntaxList<DictionaryElementSyntax> _elements;
    private readonly SyntaxToken _closeBracketToken;

    public DictionaryExpressionSyntaxTests()
    {
        GreenNode trivia = SyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, " ");

        var key = SyntaxFactory.LiteralExpression(
            SyntaxKind.NameLiteralExpression,
            SyntaxFactory.Token(SyntaxKind.NameLiteralToken, "/SomeName", trivia, trivia));

        var value = SyntaxFactory.LiteralExpression(
            SyntaxKind.NumericLiteralExpression,
            SyntaxFactory.Token(SyntaxKind.NameLiteralToken, "123", 123, trivia, trivia));

        CollectionElementSyntax keyValue = SyntaxFactory.DictionaryElement(key, value);

        SyntaxList<DictionaryElementSyntax> listBuilder = new SyntaxListBuilder(2)
            .AddRange(new GreenNode[] { keyValue, keyValue })
            .ToList<DictionaryElementSyntax>();

        _openBracketToken = SyntaxFactory.Token(SyntaxKind.LessThanLessThanToken, trivia, trivia);
        _closeBracketToken = SyntaxFactory.Token(SyntaxKind.GreaterThanGreaterThanToken, trivia, trivia);
        _elements = listBuilder;
    }

    [Fact(DisplayName = $"The {nameof(CollectionExpressionSyntax.Kind)} property must be {nameof(SyntaxKind.DictionaryExpression)}")]
    public void KindProperty_MustBeDictionaryExpression()
    {
        // Arrange
        var dictionaryExpressionSyntax = SyntaxFactory.DictionaryExpression(_openBracketToken, _elements, _closeBracketToken);

        // Act
        var actualKind = dictionaryExpressionSyntax.Kind;

        // Assert
        Assert.Equal(SyntaxKind.DictionaryExpression, actualKind);
    }

    [Fact(DisplayName = $"The {nameof(CollectionExpressionSyntax.OpenToken)} property must be assigned from constructor.")]
    public void OpenTokenProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        var dictionaryExpressionSyntax = SyntaxFactory.DictionaryExpression(_openBracketToken, _elements, _closeBracketToken);

        // Act
        var actualOpenBracketToken = dictionaryExpressionSyntax.OpenToken;

        // Assert
        Assert.Equal(_openBracketToken, actualOpenBracketToken);
    }

    [Fact(DisplayName = $"The {nameof(CollectionExpressionSyntax.Elements)} property must be assigned from constructor.")]
    public void ElementsProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        var dictionaryExpressionSyntax = SyntaxFactory.DictionaryExpression(_openBracketToken, _elements, _closeBracketToken);

        // Act
        var actualElements = dictionaryExpressionSyntax.Elements;

        // Assert
        Assert.Equal(_elements.Node, actualElements);
    }

    [Fact(DisplayName = $"The {nameof(CollectionExpressionSyntax.CloseToken)} property must be assigned from constructor.")]
    public void CloseTokenProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        var dictionaryExpressionSyntax = SyntaxFactory.DictionaryExpression(_openBracketToken, _elements, _closeBracketToken);

        // Act
        var actualCloseBracketToken = dictionaryExpressionSyntax.CloseToken;

        // Assert
        Assert.Equal(_closeBracketToken, actualCloseBracketToken);
    }

    [Fact(DisplayName = $"The {nameof(CollectionExpressionSyntax.SlotCount)} property must be equal to 3.")]
    public void SlotCountProperty_MustBeEqualTo3()
    {
        // Arrange
        var dictionaryExpressionSyntax = SyntaxFactory.DictionaryExpression(_openBracketToken, _elements, _closeBracketToken);

        // Act
        var actualSlotCount = dictionaryExpressionSyntax.SlotCount;

        // Assert
        Assert.Equal(3, actualSlotCount);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 0 must return {nameof(CollectionExpressionSyntax.OpenToken)} property.")]
    public void GetSlotMethod_Index0_MustReturnOpenBracketToken()
    {
        // Arrange
        var dictionaryExpressionSyntax = SyntaxFactory.DictionaryExpression(_openBracketToken, _elements, _closeBracketToken);

        // Act
        var actualSlot = dictionaryExpressionSyntax.GetSlot(0);

        // Assert
        Assert.Equal(_openBracketToken, actualSlot);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 1 must return {nameof(CollectionExpressionSyntax.Elements)} property.")]
    public void GetSlotMethod_Index1_MustReturnElements()
    {
        // Arrange
        var dictionaryExpressionSyntax = SyntaxFactory.DictionaryExpression(_openBracketToken, _elements, _closeBracketToken);

        // Act
        var actualSlot = dictionaryExpressionSyntax.GetSlot(1);

        // Assert
        Assert.Equal(_elements.Node, actualSlot);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 2 must return {nameof(CollectionExpressionSyntax.CloseToken)} property.")]
    public void GetSlotMethod_Index2_MustReturnCloseBracketToken()
    {
        // Arrange
        var dictionaryExpressionSyntax = SyntaxFactory.DictionaryExpression(_openBracketToken, _elements, _closeBracketToken);

        // Act
        var actualSlot = dictionaryExpressionSyntax.GetSlot(2);

        // Assert
        Assert.Equal(_closeBracketToken, actualSlot);
    }

    [Fact(DisplayName = "The GetSlot() method with index 3 must return null.")]
    public void GetSlotMethod_Index3_MustReturnNull()
    {
        // Arrange
        var dictionaryExpressionSyntax = SyntaxFactory.DictionaryExpression(_openBracketToken, _elements, _closeBracketToken);

        // Act
        var actualSlot = dictionaryExpressionSyntax.GetSlot(3);

        // Assert
        Assert.Null(actualSlot);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.Width)} property must consider all slots.")]
    public void WidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        var dictionaryExpressionSyntax = SyntaxFactory.DictionaryExpression(_openBracketToken, _elements, _closeBracketToken);

        // Act
        var actualWidth = dictionaryExpressionSyntax.Width;

        // Assert
        Assert.Equal(34, actualWidth);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.FullWidth)} property must consider all slots.")]
    public void FullWidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        var dictionaryExpressionSyntax = SyntaxFactory.DictionaryExpression(_openBracketToken, _elements, _closeBracketToken);

        // Act
        var actualFullWidth = dictionaryExpressionSyntax.FullWidth;

        // Assert
        Assert.Equal(36, actualFullWidth);
    }

    [Fact(DisplayName = "The ToString() method must not include the trivia.")]
    public void ToStringMethod_MustNotIncludeTrivia()
    {
        // Arrange
        const string ExpectedString = "<<  /SomeName  123  /SomeName  123  >>";
        var dictionaryExpressionSyntax = SyntaxFactory.DictionaryExpression(_openBracketToken, _elements, _closeBracketToken);

        // Act
        var actualString = dictionaryExpressionSyntax.ToString();

        // Assert
        Assert.Equal(ExpectedString, actualString);
    }

    [Fact(DisplayName = "The ToFullString() method must include the trivia.")]
    public void ToFullStringMethod_MustIncludeTrivia()
    {
        // Arrange
        const string ExpectedString = " <<  /SomeName  123  /SomeName  123  >> ";
        var dictionaryExpressionSyntax = SyntaxFactory.DictionaryExpression(_openBracketToken, _elements, _closeBracketToken);

        // Act
        var actualString = dictionaryExpressionSyntax.ToFullString();

        // Assert
        Assert.Equal(ExpectedString, actualString);
    }

    [Fact(DisplayName = "The SetDiagnostics() method must set the diagnostics and return a new instance.")]
    public void SetDiagnosticsMethod_MustSetTheDiagnosticsAndReturnNewInstance()
    {
        // Arrange
        var dictionaryExpressionSyntax = SyntaxFactory.DictionaryExpression(_openBracketToken, _elements, _closeBracketToken);
        DiagnosticInfo expectedDiagnostic = new(Substitute.For<IMessageProvider>(), DiagnosticCode.ERR_InvalidPDF);
        DiagnosticInfo[] diagnostics = [expectedDiagnostic];

        // Act
        var actualDictionaryExpressionSyntax = (DictionaryExpressionSyntax)dictionaryExpressionSyntax.SetDiagnostics(diagnostics);

        // Assert
        Assert.NotSame(dictionaryExpressionSyntax, actualDictionaryExpressionSyntax);
        Assert.Equal(diagnostics, actualDictionaryExpressionSyntax.GetDiagnostics());
        Assert.True(actualDictionaryExpressionSyntax.ContainsFlags(NodeFlags.ContainsDiagnostics));
    }
}

// <copyright file="ArrayExpressionSyntaxTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxTests;

using OffDotNet.Pdf.CodeAnalysis.Diagnostic;
using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using SyntaxToken = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxToken;
using SyntaxTrivia = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxTrivia;

public class ArrayExpressionSyntaxTests
{
    private readonly SyntaxToken _openBracketToken;
    private readonly SyntaxList<ArrayElementSyntax> _elements;
    private readonly SyntaxToken _closeBracketToken;

    public ArrayExpressionSyntaxTests()
    {
        GreenNode trivia = SyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, " ");
        var number =
            SyntaxFactory.ArrayElement(
                SyntaxFactory.LiteralExpression(
                    SyntaxKind.NumericLiteralExpression,
                    SyntaxFactory.Token(SyntaxKind.NumericLiteralToken, "549", 549, trivia, trivia)));

        var @bool =
            SyntaxFactory.ArrayElement(
                SyntaxFactory.LiteralExpression(
                    SyntaxKind.FalseLiteralExpression,
                    SyntaxFactory.Token(SyntaxKind.FalseKeyword, trivia, trivia)));

        var @string =
            SyntaxFactory.ArrayElement(
                SyntaxFactory.LiteralExpression(
                    SyntaxKind.StringLiteralExpression,
                    SyntaxFactory.Token(SyntaxKind.StringLiteralToken, "(Hello World!)", trivia, trivia)));

        var name =
            SyntaxFactory.ArrayElement(
                SyntaxFactory.LiteralExpression(
                    SyntaxKind.NameLiteralExpression,
                    SyntaxFactory.Token(SyntaxKind.NameLiteralToken, "/SomeName", trivia, trivia)));

        SyntaxList<ArrayElementSyntax> listBuilder = new SyntaxListBuilder(4)
            .AddRange(new GreenNode[] { number, @bool, @string, name })
            .ToList<ArrayElementSyntax>();

        _openBracketToken = SyntaxFactory.Token(SyntaxKind.LeftSquareBracketToken, trivia, trivia);
        _closeBracketToken = SyntaxFactory.Token(SyntaxKind.RightSquareBracketToken, trivia, trivia);
        _elements = listBuilder;
    }

    [Fact(DisplayName = $"The {nameof(CollectionExpressionSyntax.Kind)} property must be {nameof(SyntaxKind.ArrayExpression)}")]
    public void KindProperty_MustBeArrayExpression()
    {
        // Arrange
        var arrayExpressionSyntax = SyntaxFactory.ArrayExpression(_openBracketToken, _elements, _closeBracketToken);

        // Act
        var actualKind = arrayExpressionSyntax.Kind;

        // Assert
        Assert.Equal(SyntaxKind.ArrayExpression, actualKind);
    }

    [Fact(DisplayName = $"The {nameof(CollectionExpressionSyntax.OpenToken)} property must be assigned from constructor.")]
    public void OpenTokenProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        var arrayExpressionSyntax = SyntaxFactory.ArrayExpression(_openBracketToken, _elements, _closeBracketToken);

        // Act
        var actualOpenBracketToken = arrayExpressionSyntax.OpenToken;

        // Assert
        Assert.Equal(_openBracketToken, actualOpenBracketToken);
    }

    [Fact(DisplayName = $"The {nameof(CollectionExpressionSyntax.Elements)} property must be assigned from constructor.")]
    public void ElementsProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        var arrayExpressionSyntax = SyntaxFactory.ArrayExpression(_openBracketToken, _elements, _closeBracketToken);

        // Act
        var actualElements = arrayExpressionSyntax.Elements;

        // Assert
        Assert.Equal(_elements.Node, actualElements);
    }

    [Fact(DisplayName = $"The {nameof(CollectionExpressionSyntax.CloseToken)} property must be assigned from constructor.")]
    public void CloseTokenProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        var arrayExpressionSyntax = SyntaxFactory.ArrayExpression(_openBracketToken, _elements, _closeBracketToken);

        // Act
        var actualCloseBracketToken = arrayExpressionSyntax.CloseToken;

        // Assert
        Assert.Equal(_closeBracketToken, actualCloseBracketToken);
    }

    [Fact(DisplayName = $"The {nameof(CollectionExpressionSyntax.SlotCount)} property must be equal to 3.")]
    public void SlotCountProperty_MustBeEqualTo3()
    {
        // Arrange
        var arrayExpressionSyntax = SyntaxFactory.ArrayExpression(_openBracketToken, _elements, _closeBracketToken);

        // Act
        var actualSlotCount = arrayExpressionSyntax.SlotCount;

        // Assert
        Assert.Equal(3, actualSlotCount);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 0 must return {nameof(CollectionExpressionSyntax.OpenToken)} property.")]
    public void GetSlotMethod_Index0_MustReturnOpenBracketToken()
    {
        // Arrange
        var arrayExpressionSyntax = SyntaxFactory.ArrayExpression(_openBracketToken, _elements, _closeBracketToken);

        // Act
        var actualSlot = arrayExpressionSyntax.GetSlot(0);

        // Assert
        Assert.Equal(_openBracketToken, actualSlot);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 1 must return {nameof(CollectionExpressionSyntax.Elements)} property.")]
    public void GetSlotMethod_Index1_MustReturnElements()
    {
        // Arrange
        var arrayExpressionSyntax = SyntaxFactory.ArrayExpression(_openBracketToken, _elements, _closeBracketToken);

        // Act
        var actualSlot = arrayExpressionSyntax.GetSlot(1);

        // Assert
        Assert.Equal(_elements.Node, actualSlot);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 2 must return {nameof(CollectionExpressionSyntax.CloseToken)} property.")]
    public void GetSlotMethod_Index2_MustReturnCloseBracketToken()
    {
        // Arrange
        var arrayExpressionSyntax = SyntaxFactory.ArrayExpression(_openBracketToken, _elements, _closeBracketToken);

        // Act
        var actualSlot = arrayExpressionSyntax.GetSlot(2);

        // Assert
        Assert.Equal(_closeBracketToken, actualSlot);
    }

    [Fact(DisplayName = "The GetSlot() method with index 3 must return null.")]
    public void GetSlotMethod_Index3_MustReturnNull()
    {
        // Arrange
        var arrayExpressionSyntax = SyntaxFactory.ArrayExpression(_openBracketToken, _elements, _closeBracketToken);

        // Act
        var actualSlot = arrayExpressionSyntax.GetSlot(3);

        // Assert
        Assert.Null(actualSlot);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.Width)} property must consider all slots.")]
    public void WidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        var arrayExpressionSyntax = SyntaxFactory.ArrayExpression(_openBracketToken, _elements, _closeBracketToken);

        // Act
        var actualWidth = arrayExpressionSyntax.Width;

        // Assert
        Assert.Equal(40, actualWidth);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.FullWidth)} property must consider all slots.")]
    public void FullWidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        var arrayExpressionSyntax = SyntaxFactory.ArrayExpression(_openBracketToken, _elements, _closeBracketToken);

        // Act
        var actualFullWidth = arrayExpressionSyntax.FullWidth;

        // Assert
        Assert.Equal(42, actualFullWidth);
    }

    [Fact(DisplayName = "The ToString() method must not include the trivia.")]
    public void ToStringMethod_MustNotIncludeTrivia()
    {
        // Arrange
        const string ExpectedString = "[  549  false  (Hello World!)  /SomeName  ]";
        var arrayExpressionSyntax = SyntaxFactory.ArrayExpression(_openBracketToken, _elements, _closeBracketToken);

        // Act
        var actualString = arrayExpressionSyntax.ToString();

        // Assert
        Assert.Equal(ExpectedString, actualString);
    }

    [Fact(DisplayName = "The ToFullString() method must include the trivia.")]
    public void ToFullStringMethod_MustIncludeTrivia()
    {
        // Arrange
        const string ExpectedString = " [  549  false  (Hello World!)  /SomeName  ] ";
        var arrayExpressionSyntax = SyntaxFactory.ArrayExpression(_openBracketToken, _elements, _closeBracketToken);

        // Act
        var actualString = arrayExpressionSyntax.ToFullString();

        // Assert
        Assert.Equal(ExpectedString, actualString);
    }

    [Fact(DisplayName = "The SetDiagnostics() method must set the diagnostics and return a new instance.")]
    public void SetDiagnosticsMethod_MustSetTheDiagnosticsAndReturnNewInstance()
    {
        // Arrange
        var arrayExpressionSyntax = SyntaxFactory.ArrayExpression(_openBracketToken, _elements, _closeBracketToken);
        DiagnosticInfo expectedDiagnostic = new(Substitute.For<IMessageProvider>(), DiagnosticCode.ERR_InvalidPDF);
        DiagnosticInfo[] diagnostics = [expectedDiagnostic];

        // Act
        var newArrayExpressionSyntax = (ArrayExpressionSyntax)arrayExpressionSyntax.SetDiagnostics(diagnostics);

        // Assert
        Assert.NotSame(arrayExpressionSyntax, newArrayExpressionSyntax);
        Assert.Equal(diagnostics, newArrayExpressionSyntax.GetDiagnostics());
        Assert.True(newArrayExpressionSyntax.ContainsFlags(GreenNode.NodeFlags.ContainsDiagnostics));
    }
}

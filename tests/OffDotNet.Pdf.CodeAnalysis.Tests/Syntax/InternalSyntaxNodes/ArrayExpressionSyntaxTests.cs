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
    private readonly SyntaxToken openBracketToken;
    private readonly SyntaxList<ArrayElementSyntax> elements;
    private readonly SyntaxToken closeBracketToken;

    public ArrayExpressionSyntaxTests()
    {
        GreenNode trivia = SyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, " ");
        ArrayElementSyntax number =
            SyntaxFactory.ArrayElement(
                SyntaxFactory.LiteralExpression(
                    SyntaxKind.NumericLiteralExpression,
                    SyntaxFactory.Token(SyntaxKind.NumericLiteralToken, "549", 549, trivia, trivia)));

        ArrayElementSyntax @bool =
            SyntaxFactory.ArrayElement(
                SyntaxFactory.LiteralExpression(
                    SyntaxKind.FalseLiteralExpression,
                    SyntaxFactory.Token(SyntaxKind.FalseKeyword, trivia, trivia)));

        ArrayElementSyntax @string =
            SyntaxFactory.ArrayElement(
                SyntaxFactory.LiteralExpression(
                    SyntaxKind.StringLiteralExpression,
                    SyntaxFactory.Token(SyntaxKind.StringLiteralToken, "(Hello World!)", trivia, trivia)));

        ArrayElementSyntax name =
            SyntaxFactory.ArrayElement(
                SyntaxFactory.LiteralExpression(
                    SyntaxKind.NameLiteralExpression,
                    SyntaxFactory.Token(SyntaxKind.NameLiteralToken, "/SomeName", trivia, trivia)));

        SyntaxList<ArrayElementSyntax> listBuilder = new SyntaxListBuilder(4)
            .AddRange(new GreenNode[] { number, @bool, @string, name })
            .ToList<ArrayElementSyntax>();

        this.openBracketToken = SyntaxFactory.Token(SyntaxKind.LeftSquareBracketToken, trivia, trivia);
        this.closeBracketToken = SyntaxFactory.Token(SyntaxKind.RightSquareBracketToken, trivia, trivia);
        this.elements = listBuilder;
    }

    [Fact(DisplayName = $"The {nameof(CollectionExpressionSyntax.Kind)} property must be {nameof(SyntaxKind.ArrayExpression)}")]
    public void KindProperty_MustBeArrayExpression()
    {
        // Arrange
        ArrayExpressionSyntax arrayExpressionSyntax = SyntaxFactory.ArrayExpression(this.openBracketToken, this.elements, this.closeBracketToken);

        // Act
        SyntaxKind actualKind = arrayExpressionSyntax.Kind;

        // Assert
        Assert.Equal(SyntaxKind.ArrayExpression, actualKind);
    }

    [Fact(DisplayName = $"The {nameof(CollectionExpressionSyntax.OpenToken)} property must be assigned from constructor.")]
    public void OpenTokenProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        ArrayExpressionSyntax arrayExpressionSyntax = SyntaxFactory.ArrayExpression(this.openBracketToken, this.elements, this.closeBracketToken);

        // Act
        SyntaxToken actualOpenBracketToken = arrayExpressionSyntax.OpenToken;

        // Assert
        Assert.Equal(this.openBracketToken, actualOpenBracketToken);
    }

    [Fact(DisplayName = $"The {nameof(CollectionExpressionSyntax.Elements)} property must be assigned from constructor.")]
    public void ElementsProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        ArrayExpressionSyntax arrayExpressionSyntax = SyntaxFactory.ArrayExpression(this.openBracketToken, this.elements, this.closeBracketToken);

        // Act
        GreenNode? actualElements = arrayExpressionSyntax.Elements;

        // Assert
        Assert.Equal(this.elements.Node, actualElements);
    }

    [Fact(DisplayName = $"The {nameof(CollectionExpressionSyntax.CloseToken)} property must be assigned from constructor.")]
    public void CloseTokenProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        ArrayExpressionSyntax arrayExpressionSyntax = SyntaxFactory.ArrayExpression(this.openBracketToken, this.elements, this.closeBracketToken);

        // Act
        SyntaxToken actualCloseBracketToken = arrayExpressionSyntax.CloseToken;

        // Assert
        Assert.Equal(this.closeBracketToken, actualCloseBracketToken);
    }

    [Fact(DisplayName = $"The {nameof(CollectionExpressionSyntax.SlotCount)} property must be equal to 3.")]
    public void SlotCountProperty_MustBeEqualTo3()
    {
        // Arrange
        ArrayExpressionSyntax arrayExpressionSyntax = SyntaxFactory.ArrayExpression(this.openBracketToken, this.elements, this.closeBracketToken);

        // Act
        int actualSlotCount = arrayExpressionSyntax.SlotCount;

        // Assert
        Assert.Equal(3, actualSlotCount);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 0 must return {nameof(CollectionExpressionSyntax.OpenToken)} property.")]
    public void GetSlotMethod_Index0_MustReturnOpenBracketToken()
    {
        // Arrange
        ArrayExpressionSyntax arrayExpressionSyntax = SyntaxFactory.ArrayExpression(this.openBracketToken, this.elements, this.closeBracketToken);

        // Act
        GreenNode? actualSlot = arrayExpressionSyntax.GetSlot(0);

        // Assert
        Assert.Equal(this.openBracketToken, actualSlot);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 1 must return {nameof(CollectionExpressionSyntax.Elements)} property.")]
    public void GetSlotMethod_Index1_MustReturnElements()
    {
        // Arrange
        ArrayExpressionSyntax arrayExpressionSyntax = SyntaxFactory.ArrayExpression(this.openBracketToken, this.elements, this.closeBracketToken);

        // Act
        GreenNode? actualSlot = arrayExpressionSyntax.GetSlot(1);

        // Assert
        Assert.Equal(this.elements.Node, actualSlot);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 2 must return {nameof(CollectionExpressionSyntax.CloseToken)} property.")]
    public void GetSlotMethod_Index2_MustReturnCloseBracketToken()
    {
        // Arrange
        ArrayExpressionSyntax arrayExpressionSyntax = SyntaxFactory.ArrayExpression(this.openBracketToken, this.elements, this.closeBracketToken);

        // Act
        GreenNode? actualSlot = arrayExpressionSyntax.GetSlot(2);

        // Assert
        Assert.Equal(this.closeBracketToken, actualSlot);
    }

    [Fact(DisplayName = "The GetSlot() method with index 3 must return null.")]
    public void GetSlotMethod_Index3_MustReturnNull()
    {
        // Arrange
        ArrayExpressionSyntax arrayExpressionSyntax = SyntaxFactory.ArrayExpression(this.openBracketToken, this.elements, this.closeBracketToken);

        // Act
        GreenNode? actualSlot = arrayExpressionSyntax.GetSlot(3);

        // Assert
        Assert.Null(actualSlot);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.Width)} property must consider all slots.")]
    public void WidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        ArrayExpressionSyntax arrayExpressionSyntax = SyntaxFactory.ArrayExpression(this.openBracketToken, this.elements, this.closeBracketToken);

        // Act
        int actualWidth = arrayExpressionSyntax.Width;

        // Assert
        Assert.Equal(40, actualWidth);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.FullWidth)} property must consider all slots.")]
    public void FullWidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        ArrayExpressionSyntax arrayExpressionSyntax = SyntaxFactory.ArrayExpression(this.openBracketToken, this.elements, this.closeBracketToken);

        // Act
        int actualFullWidth = arrayExpressionSyntax.FullWidth;

        // Assert
        Assert.Equal(42, actualFullWidth);
    }

    [Fact(DisplayName = "The ToString() method must not include the trivia.")]
    public void ToStringMethod_MustNotIncludeTrivia()
    {
        // Arrange
        const string expectedString = "[  549  false  (Hello World!)  /SomeName  ]";
        ArrayExpressionSyntax arrayExpressionSyntax = SyntaxFactory.ArrayExpression(this.openBracketToken, this.elements, this.closeBracketToken);

        // Act
        string actualString = arrayExpressionSyntax.ToString();

        // Assert
        Assert.Equal(expectedString, actualString);
    }

    [Fact(DisplayName = "The ToFullString() method must include the trivia.")]
    public void ToFullStringMethod_MustIncludeTrivia()
    {
        // Arrange
        const string expectedString = " [  549  false  (Hello World!)  /SomeName  ] ";
        ArrayExpressionSyntax arrayExpressionSyntax = SyntaxFactory.ArrayExpression(this.openBracketToken, this.elements, this.closeBracketToken);

        // Act
        string actualString = arrayExpressionSyntax.ToFullString();

        // Assert
        Assert.Equal(expectedString, actualString);
    }

    [Fact(DisplayName = "The SetDiagnostics() method must set the diagnostics and return a new instance.")]
    public void SetDiagnosticsMethod_MustSetTheDiagnosticsAndReturnNewInstance()
    {
        // Arrange
        ArrayExpressionSyntax arrayExpressionSyntax = SyntaxFactory.ArrayExpression(this.openBracketToken, this.elements, this.closeBracketToken);
        DiagnosticInfo expectedDiagnostic = new(Substitute.For<IMessageProvider>(), DiagnosticCode.ERR_InvalidPDF);
        DiagnosticInfo[] diagnostics = { expectedDiagnostic };

        // Act
        ArrayExpressionSyntax newArrayExpressionSyntax = (ArrayExpressionSyntax)arrayExpressionSyntax.SetDiagnostics(diagnostics);

        // Assert
        Assert.NotSame(arrayExpressionSyntax, newArrayExpressionSyntax);
        Assert.Equal(diagnostics, newArrayExpressionSyntax.GetDiagnostics());
        Assert.True(newArrayExpressionSyntax.ContainsFlags(GreenNode.NodeFlags.ContainsDiagnostics));
    }
}

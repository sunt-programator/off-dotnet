// <copyright file="SyntaxListTests.WithThreeChildren.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxTests;

using System.Diagnostics.CodeAnalysis;
using OffDotNet.Pdf.CodeAnalysis.Diagnostic;
using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:File name should match first type name", Justification = "Verified")]
public class WithThreeChildrenTests
{
    [Fact(DisplayName = $"The {nameof(SyntaxList.SlotCount)} property must be equal to 3.")]
    public void SlotCountProperty_MustBeEqualTo2()
    {
        // Arrange
        var literalExpression = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        var syntaxList = SyntaxFactory.List(literalExpression, literalExpression, literalExpression);

        // Act
        var actualSlotCount = syntaxList.SlotCount;

        // Assert
        Assert.Equal(3, actualSlotCount);
    }

    [Fact(DisplayName = "The GetSlot() method with index 0 must return the first child.")]
    public void GetSlotMethod_Index0_MustReturnFirstObject()
    {
        // Arrange
        var literalExpression1 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        var literalExpression2 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword));
        var literalExpression3 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.NullKeyword));
        var syntaxList = SyntaxFactory.List(literalExpression1, literalExpression2, literalExpression3);

        // Act
        var actualSlot = syntaxList.GetSlot(0);

        // Assert
        Assert.Equal(literalExpression1, actualSlot);
    }

    [Fact(DisplayName = "The GetSlot() method with index 1 must return the second child.")]
    public void GetSlotMethod_Index1_MustReturnSecondObject()
    {
        // Arrange
        var literalExpression1 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        var literalExpression2 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword));
        var literalExpression3 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.NullKeyword));
        var syntaxList = SyntaxFactory.List(literalExpression1, literalExpression2, literalExpression3);

        // Act
        var actualSlot = syntaxList.GetSlot(1);

        // Assert
        Assert.Equal(literalExpression2, actualSlot);
    }

    [Fact(DisplayName = "The GetSlot() method with index 2 must return the third child.")]
    public void GetSlotMethod_Index2_MustReturnThirdObject()
    {
        // Arrange
        var literalExpression1 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        var literalExpression2 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword));
        var literalExpression3 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.NullKeyword));
        var syntaxList = SyntaxFactory.List(literalExpression1, literalExpression2, literalExpression3);

        // Act
        var actualSlot = syntaxList.GetSlot(2);

        // Assert
        Assert.Equal(literalExpression3, actualSlot);
    }

    [Fact(DisplayName = "The GetSlot() method with index 3 must return null.")]
    public void GetSlotMethod_Index3_MustReturnNull()
    {
        // Arrange
        var literalExpression1 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        var literalExpression2 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword));
        var literalExpression3 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.NullKeyword));
        var syntaxList = SyntaxFactory.List(literalExpression1, literalExpression2, literalExpression3);

        // Act
        var actualSlot = syntaxList.GetSlot(3);

        // Assert
        Assert.Null(actualSlot);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.FullWidth)} property must consider all slots.")]
    public void FullWidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        var trivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " ");
        var literalExpression1 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword, trivia, trivia));
        var literalExpression2 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword, trivia, trivia));
        var literalExpression3 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.NullKeyword, trivia, trivia));
        var syntaxList = SyntaxFactory.List(literalExpression1, literalExpression2, literalExpression3);

        // Act
        var actualFullWidth = syntaxList.FullWidth;

        // Assert
        Assert.Equal(19, actualFullWidth);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.Width)} property must consider all slots.")]
    public void WidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        var trivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " ");
        var literalExpression1 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword, trivia, trivia));
        var literalExpression2 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword, trivia, trivia));
        var literalExpression3 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.NullKeyword, trivia, trivia));
        var syntaxList = SyntaxFactory.List(literalExpression1, literalExpression2, literalExpression3);

        // Act
        var actualWidth = syntaxList.Width;

        // Assert
        Assert.Equal(17, actualWidth);
    }

    [Theory(DisplayName = "The GetSlotOffset() method with specified index must return correct offset.")]
    [InlineData(0, 0)]
    [InlineData(1, 6)]
    [InlineData(2, 13)]
    [InlineData(3, 19)]
    [InlineData(4, 19)] // No more slots, so the offset is the same as the last slot.
    public void GetSlotOffsetMethod_MustReturnTheCorrectOffset(int index, int offset)
    {
        // Arrange
        var trivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " ");
        var literalExpression1 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword, trivia, trivia));
        var literalExpression2 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword, trivia, trivia));
        var literalExpression3 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.NullKeyword, trivia, trivia));
        var syntaxList = SyntaxFactory.List(literalExpression1, literalExpression2, literalExpression3);

        // Act
        var actualOffset = syntaxList.GetSlotOffset(index);

        // Assert
        Assert.Equal(offset, actualOffset);
    }

    [Fact(DisplayName = "The ToString() method must not include the trivia.")]
    public void ToStringMethod_MustNotIncludeTrivia()
    {
        // Arrange
        const string ExpectedString = "true  false  null";
        GreenNode trivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " ");
        var literalExpression1 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword, trivia, trivia));
        var literalExpression2 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword, trivia, trivia));
        var literalExpression3 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.NullKeyword, trivia, trivia));

        // Act
        var syntaxList = SyntaxFactory.List(literalExpression1, literalExpression2, literalExpression3);
        var actualString = syntaxList.ToString();

        // Assert
        Assert.Equal(ExpectedString, actualString);
    }

    [Fact(DisplayName = "The ToFullString() method must include the trivia.")]
    public void ToFullStringMethod_MustIncludeTrivia()
    {
        // Arrange
        const string ExpectedString = " true  false  null ";
        GreenNode trivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " ");
        var literalExpression1 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword, trivia, trivia));
        var literalExpression2 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword, trivia, trivia));
        var literalExpression3 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.NullKeyword, trivia, trivia));

        // Act
        var syntaxList = SyntaxFactory.List(literalExpression1, literalExpression2, literalExpression3);
        var actualString = syntaxList.ToFullString();

        // Assert
        Assert.Equal(ExpectedString, actualString);
    }

    [Fact(DisplayName = "The SetDiagnostics() method must set the diagnostics and return a new instance.")]
    public void SetDiagnosticsMethod_MustSetTheDiagnosticsAndReturnANewInstance()
    {
        // Arrange
        var trivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " ");
        var literalExpression1 = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword, trivia, trivia));
        var literalExpression2 = SyntaxFactory.LiteralExpression(SyntaxKind.FalseLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword, trivia, trivia));
        var literalExpression3 = SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression, SyntaxFactory.Token(SyntaxKind.NullKeyword, trivia, trivia));
        var syntaxList = SyntaxFactory.List(literalExpression1, literalExpression2, literalExpression3);

        DiagnosticInfo expectedDiagnostic = new(Substitute.For<IMessageProvider>(), DiagnosticCode.ERR_InvalidPDF);
        DiagnosticInfo[] diagnostics = [expectedDiagnostic];

        // Act
        var actualSyntaxList = syntaxList.SetDiagnostics(diagnostics);

        // Assert
        Assert.NotSame(syntaxList, actualSyntaxList);
        Assert.Equal(diagnostics, actualSyntaxList.GetDiagnostics());
        Assert.True(actualSyntaxList.ContainsFlags(GreenNode.NodeFlags.ContainsDiagnostics));
    }
}

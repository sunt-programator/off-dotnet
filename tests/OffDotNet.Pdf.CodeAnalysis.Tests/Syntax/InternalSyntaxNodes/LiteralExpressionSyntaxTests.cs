// <copyright file="LiteralExpressionSyntaxTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxNodes;

using OffDotNet.Pdf.CodeAnalysis.Diagnostic;
using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntaxNodes;
using SyntaxFactory = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxFactory;

public class LiteralExpressionSyntaxTests
{
    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.Token)} property must be assigned from constructor.")]
    public void KeywordProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        var keyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        var literalExpression = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, keyword);

        // Act
        var actualToken = literalExpression.Token;

        // Assert
        Assert.Equal(keyword, actualToken);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.SlotCount)} property must be equal to 1.")]
    public void SlotCountProperty_MustBeEqualTo1()
    {
        // Arrange
        var keyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        var literalExpression = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, keyword);

        // Act
        var actualSlotCount = literalExpression.SlotCount;

        // Assert
        Assert.Equal(1, actualSlotCount);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 0 must return {nameof(LiteralExpressionSyntax.Token)} property.")]
    public void GetSlotMethod_Index0_MustReturnKeywordProperty()
    {
        // Arrange
        var keyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        var literalExpression = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, keyword);

        // Act
        var actualSlot = literalExpression.GetSlot(0);

        // Assert
        Assert.Equal(keyword, actualSlot);
    }

    [Fact(DisplayName = "The GetSlot() method with index 1 must return null.")]
    public void GetSlotMethod_Index1_MustReturnNull()
    {
        // Arrange
        var keyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        var literalExpression = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, keyword);

        // Act
        var actualSlot = literalExpression.GetSlot(1);

        // Assert
        Assert.Null(actualSlot);
    }

    [Fact(DisplayName = "The ToString() method must not include the trivia.")]
    public void ToStringMethod_MustNotIncludeTrivia()
    {
        // Arrange
        const string ExpectedString = "true";
        var trivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " ");
        var keyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword, trivia, trivia);
        var literalExpression = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, keyword);

        // Act
        var actualString = literalExpression.ToString();

        // Assert
        Assert.Equal(ExpectedString, actualString);
    }

    [Fact(DisplayName = "The ToFullString() method must include the trivia.")]
    public void ToFullStringMethod_MustIncludeTrivia()
    {
        // Arrange
        const string ExpectedString = " true ";
        var trivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " ");
        var keyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword, trivia, trivia);
        var literalExpression = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, keyword);

        // Act
        var actualString = literalExpression.ToFullString();

        // Assert
        Assert.Equal(ExpectedString, actualString);
    }

    [Fact(DisplayName = "The SetDiagnostics() method must set the diagnostics and return a new instance.")]
    public void SetDiagnosticsMethod_MustSetDiagnosticsAndReturnNewInstance()
    {
        // Arrange
        var keyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        var literalExpression = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, keyword);
        DiagnosticInfo expectedDiagnostic = new(Substitute.For<IMessageProvider>(), DiagnosticCode.ERR_InvalidPDF);
        DiagnosticInfo[] diagnostics = [expectedDiagnostic];

        // Act
        var actualNode = (LiteralExpressionSyntax)literalExpression.SetDiagnostics(diagnostics);

        // Assert
        Assert.NotSame(literalExpression, actualNode);
        Assert.Equal(diagnostics, actualNode.GetDiagnostics());
        Assert.True(actualNode.ContainsFlags(NodeFlags.ContainsDiagnostics));
    }
}
